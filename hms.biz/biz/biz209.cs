using Common.Entity;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Transactions;
using Hms.Entity;

namespace Hms.Biz
{
    /// <summary>
    /// 问卷
    /// </summary>
    public class Biz209 : IDisposable
    {
        #region 常规

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="lstDet"></param>
        /// <param name="qnId"></param>
        /// <returns></returns>
        public int SaveQNnormal(EntityDicQnMain vo, List<EntityDicQnDetail> lstDet, out decimal qnId, List<EntityDicQnCtlLocation> lstLaction = null,List<EntityDicQnSetting> lstSettings = null)
        {
            int affectRows = 0;
            qnId = 0;
            string Sql = string.Empty;
            SqlHelper svc = null;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                if (vo.qnId <= 0)
                {
                    Sql = @"select max(t.qnId) as maxId from dicQnMain t";
                    DataTable dt = svc.GetDataTable(Sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["maxId"] != DBNull.Value)
                        {
                            vo.qnId = Convert.ToDecimal(dt.Rows[0]["maxId"]) + 1;
                        }
                    }
                    if (vo.qnId <= 0)
                        vo.qnId = 1;
                }
                if (lstDet != null)
                {
                    foreach (EntityDicQnDetail item in lstDet)
                    {
                        item.qnId = vo.qnId;
                    }
                }
                if(lstLaction != null)
                {
                    foreach (EntityDicQnCtlLocation item in lstLaction)
                    {
                        item.qnId = vo.qnId;
                    }
                }
                if(lstSettings != null)
                {
                    foreach (EntityDicQnSetting item in lstSettings)
                    {
                        item.qnId = vo.qnId;
                    }
                }

                List<DacParm> lstParm = new List<DacParm>();
                List<DacParm> lstParam2 = new List<DacParm>(); 
                lstParm.Add(svc.GetDelParmByPk(vo));
                lstParm.Add(svc.GetInsertParm(vo));
                if (lstDet != null && lstDet.Count > 0)
                {
                    lstParam2.Add(svc.GetDelParm(lstDet[0], EntityDicQnDetail.Columns.qnId));
                    lstParam2.Add(svc.GetInsertParm(lstDet.ToArray()));
                }

                if(lstLaction != null && lstLaction.Count > 0)
                {
                    lstParm.Add(svc.GetDelParmByPk(lstLaction.ToArray()));
                    lstParm.Add(svc.GetInsertParm(lstLaction.ToArray()));
                }
                if(lstSettings != null && lstSettings.Count > 0)
                {
                    lstParm.Add(svc.GetDelParm(lstSettings[0], EntityDicQnSetting.Columns.qnId));
                    lstParm.Add(svc.GetInsertParm(lstSettings.FindAll(r=>r.status==1).ToArray()));
                }
                if(lstParam2.Count > 0)
                {
                    affectRows = svc.Commit(lstParam2);
                }
                if(lstParm.Count > 0)
                    affectRows = svc.Commit(lstParm);
                qnId = vo.qnId;
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException(e);
                affectRows = -1;
                qnId = -1; 
            }
            finally
            {
                svc = null;
            }
            return affectRows;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="qnId"></param>
        /// <returns></returns>
        public int DeleteQNnormal(decimal qnId)
        {
            int affectRows = 0;
            SqlHelper svc = null;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
                lstParm.Add(svc.GetDelParmByPk(new EntityDicQnMain() { qnId = qnId }));
                lstParm.Add(svc.GetDelParmByPk(new EntityDicQnDetail() { qnId = qnId }));
                affectRows = svc.Commit(lstParm);
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException(e);
                affectRows = -1;
            }
            finally
            {
                svc = null;
            }
            return affectRows;
        }
        #endregion

        #region GetQnDetail
        /// <summary>
        /// GetQnDetail
        /// </summary>
        /// <param name="qnId"></param>
        /// <returns></returns>
        public List<EntityDicQnDetail> GetQnDetail(decimal qnId)
        {
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            return EntityTools.ConvertToEntityList<EntityDicQnDetail>(svc.Select(new EntityDicQnDetail() { qnId = qnId }, EntityDicQnDetail.Columns.qnId));
        }
        #endregion

        #region EntityDicQnSetting
        /// <summary>
        /// EntityDicQnSetting
        /// </summary>
        /// <param name="qnId"></param>
        /// <returns></returns>
        public List<EntityDicQnSetting> GetDicQnSetting(decimal qnId)
        {
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            return EntityTools.ConvertToEntityList<EntityDicQnSetting>(svc.Select(new EntityDicQnSetting() { qnId = qnId }, EntityDicQnSetting.Columns.qnId));
        }
        #endregion

        #endregion

        #region 自定义

        #region GetQnSetting
        /// <summary>
        /// GetQnSetting
        /// </summary>
        /// <returns></returns>
        public List<EntityQnSetting> GetQnSetting(decimal qnId)
        {
            List<EntityQnSetting> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            List<EntityDicQnSetting> lstSet = EntityTools.ConvertToEntityList<EntityDicQnSetting>(svc.Select(new EntityDicQnSetting() { qnId = qnId }, EntityDicQnSetting.Columns.qnId));

            if (lstSet != null && lstSet.Count > 0)
            {
                data = new List<EntityQnSetting>();
                // 获取题目
                List<EntityDicQnSetting> lstTopic = lstSet.FindAll(t => t.isParent == 1);
                if (lstTopic != null && lstTopic.Count > 0)
                {
                    EntityQnSetting qnVo = null;
                    List<EntityDicQnSetting> lstItems = null;
                    foreach (EntityDicQnSetting item in lstTopic)
                    {
                        string qnDesc = "    ";
                        lstItems = lstSet.FindAll(t => !string.IsNullOrEmpty(t.parentFieldId) && t.parentFieldId.Trim() != "" && t.parentFieldId == item.fieldId);
                        if (lstItems != null && lstItems.Count > 0)
                        {
                            lstItems.Sort();
                            int i = 0;
                            foreach (EntityDicQnSetting item2 in lstItems)
                            {
                                qnDesc += Convert.ToString(++i) + "、" + item2.fieldName + "     ";
                            }
                        }
                        qnVo = new EntityQnSetting();
                        qnVo.qnId = item.qnId;
                        qnVo.isCheck = 0;
                        if (item.typeId == "1")
                            qnVo.className = "单选题";
                        else if (item.typeId == "2")
                            qnVo.className = "多选题";
                        else if (item.typeId == "3")
                            qnVo.className = "填空题";
                        else
                            qnVo.className = "其他";
                        qnVo.fieldId = item.fieldId;
                        qnVo.fieldName = "  " + item.fieldName;
                        qnVo.sortNo = item.sortNo;
                        qnVo.qnDesc = qnDesc;
                        data.Add(qnVo);
                    }
                }
            }
            return data;
        }
        #endregion

        #region GetQnSetting
        /// <summary>
        /// GetQnSetting
        /// </summary>
        /// <returns></returns>
        public List<EntityQnSetting> GetQnSettingFromSummary()
        {
            List<EntityQnSetting> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            List<EntityDicQnSummary> lstSet = EntityTools.ConvertToEntityList<EntityDicQnSummary>(svc.Select(new EntityDicQnSummary()));

            if (lstSet != null && lstSet.Count > 0)
            {
                data = new List<EntityQnSetting>();
                // 获取题目
                List<EntityDicQnSummary> lstTopic = lstSet.FindAll(t => t.isParent == 1);
                if (lstTopic != null && lstTopic.Count > 0)
                {
                    EntityQnSetting qnVo = null;
                    List<EntityDicQnSummary> lstItems = null;
                    foreach (EntityDicQnSummary item in lstTopic)
                    {
                        string qnDesc = "    ";
                        lstItems = lstSet.FindAll(t => !string.IsNullOrEmpty(t.parentFieldId) && t.parentFieldId.Trim() != "" && t.parentFieldId == item.fieldId);
                        if (lstItems != null && lstItems.Count > 0)
                        {
                            lstItems.Sort();
                            int i = 0;
                            foreach (EntityDicQnSummary item2 in lstItems)
                            {
                                qnDesc += Convert.ToString(++i) + "、" + item2.fieldName + "     ";
                            }
                        }
                        qnVo = new EntityQnSetting();
                        qnVo.isCheck = 0;
                        if (item.typeId == "1")
                            qnVo.className = "单选题";
                        else if (item.typeId == "2")
                            qnVo.className = "多选题";
                        else if (item.typeId == "3")
                            qnVo.className = "填空题";
                        else
                            qnVo.className = "其他";
                        qnVo.fieldId = item.fieldId;
                        qnVo.fieldName = "  " + item.fieldName;
                        qnVo.sortNo = item.sortNo;
                        qnVo.qnDesc = qnDesc;
                        data.Add(qnVo);
                    }
                }
            }
            return data;
        }
        #endregion

        #region GetQnCustom
        /// <summary>
        /// GetQnCustom
        /// </summary>
        /// <param name="qnId"></param>
        /// <param name="lstTopic"></param>
        /// <param name="lstItems"></param>
        public void GetQnCustom(decimal qnId, out List<EntityDicQnSetting> lstTopic, out List<EntityDicQnSetting> lstItems)
        {
            lstTopic = null;
            lstItems = null;
            string Sql = string.Empty;
            DataTable dt = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            IDataParameter[] parm = null;

            // 题目(标题)
            Sql = @"select c.qnClassId,
                           c.typeId,
                           c.fieldId,
                           c.fieldName,
                           c.isEssential,
                           c.parentFieldId,
                           b.questName
                      from dicQnMain a
                     inner join dicQnDetail b
                        on a.qnId = b.qnId
                     inner join dicQnSetting c
                        on b.fieldId = c.fieldId and a.qnId = c.qnId 
                     where a.qnId = ? 
                       and c.status = 1 ";

            parm = svc.CreateParm(1);
            parm[0].Value = qnId;
            dt = svc.GetDataTable(Sql, parm);
            if (dt != null && dt.Rows.Count > 0)
            {
                lstTopic = new List<EntityDicQnSetting>();
                string fieldIds = string.Empty;
                string parentFieldId = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    lstTopic.Add(new EntityDicQnSetting()
                    {
                        qnClassId = Function.Int(dr["qnClassId"]),
                        typeId = dr["typeId"].ToString(),
                        fieldId = dr["fieldId"].ToString(),
                        fieldName = dr["fieldName"].ToString(),
                        parentFieldId = dr["parentFieldId"].ToString(),
                        isEssential = Function.Int(dr["isEssential"]),
                        questName = dr["questName"].ToString()
                    });
                    parentFieldId = dr["parentFieldId"].ToString().Trim();
                    if (string.IsNullOrEmpty(parentFieldId))
                        fieldIds += "'" + dr["fieldId"].ToString() + "',";
                    if (fieldIds.Contains(parentFieldId))
                        continue;
                    fieldIds += "'" + dr["parentFieldId"].ToString() + "',";
                }
                fieldIds = fieldIds.TrimEnd(',');
                if (fieldIds != string.Empty)
                {
                    // 题目选项
                    lstItems = new List<EntityDicQnSetting>();
                    Sql = @"select c.qnClassId,
                                   c.typeId,
                                   c.fieldId,
                                   c.fieldName,
                                   c.isEssential,
                                   c.parentFieldId
                              from dicQnSetting c
                             where c.qnId = {0}
                                and c.parentFieldId in ({1}) 
                               and c.status = 1 ";
                    dt = svc.GetDataTable(string.Format(Sql, qnId, fieldIds));
                    foreach (DataRow dr in dt.Rows)
                    {
                        lstItems.Add(new EntityDicQnSetting()
                        {
                            qnClassId = Function.Int(dr["qnClassId"]),
                            typeId = dr["typeId"].ToString(),
                            fieldId = dr["fieldId"].ToString(),
                            fieldName = dr["fieldName"].ToString(),
                            parentFieldId = dr["parentFieldId"].ToString(),
                            isEssential = Function.Int(dr["isEssential"])
                        });
                    }
                }
            }
        }
        #endregion

        #region GetQnList
        /// <summary>
        /// GetQnList
        /// </summary>
        /// <returns></returns>
        public List<EntityDicQnSummary> GetQnList()
        {
            List<EntityDicQnSummary> dataSource = new List<EntityDicQnSummary>();
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            List<EntityDicQnSummary> tmpData = EntityTools.ConvertToEntityList<EntityDicQnSummary>(svc.Select(new EntityDicQnSummary()));
            foreach (EntityDicQnSummary item in tmpData)
            {
                if (item.isParent == 1)
                    dataSource.Add(item);
            }
            List<EntityDicQnSummary> subData = null;
            foreach (EntityDicQnSummary item in dataSource)
            {
                subData = tmpData.FindAll(t => t.parentFieldId == item.fieldId);
                if (subData != null && subData.Count > 0)
                {
                    int num = 0;
                    foreach (EntityDicQnSummary item2 in subData)
                    {
                        item.qnItemsDesc += Convert.ToString(++num) + "、" + item2.fieldName + "; ";
                    }
                    item.qnItemsDesc = item.qnItemsDesc.Trim().TrimEnd(';');
                }
            }
            return dataSource;
        }
        #endregion


        #region GetDicQnSummary
        /// <summary>
        /// GetDicQnSummary
        /// </summary>
        /// <returns></returns>
        public List<EntityDicQnSummary> GetDicQnSummary()
        {
            List<EntityDicQnSummary> dataSource = new List<EntityDicQnSummary>();
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            List<EntityDicQnSummary> tmpData = EntityTools.ConvertToEntityList<EntityDicQnSummary>(svc.Select(new EntityDicQnSummary()));
            foreach (EntityDicQnSummary item in tmpData)
            {
                dataSource.Add(item);
            }
            return dataSource;
        }
        #endregion

        #region GetTopics
        /// <summary>
        /// GetTopics
        /// </summary>
        /// <returns></returns>
        public List<EntityDicQnSetting> GetTopics()
        {
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            List<EntityDicQnSetting> data = EntityTools.ConvertToEntityList<EntityDicQnSetting>(svc.Select(new EntityDicQnSetting() { isParent = 1, status = 1 }, new List<string>() { EntityDicQnSetting.Columns.isParent, EntityDicQnSetting.Columns.status }));
            if (data != null)
            {
                data.Sort();
                foreach (EntityDicQnSetting item in data)
                {
                    item.pyCode = SpellCodeHelper.GetPyCode(item.fieldName);
                    item.wbCode = SpellCodeHelper.GetWbCode(item.fieldName);
                }
            }
            return data;
        }
        #endregion

        #region GetTopicItems
        /// <summary>
        /// GetTopicItems
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public List<EntityDicQnSummary> GetTopicItems(string fieldId)
        {
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            List<EntityDicQnSummary> data = EntityTools.ConvertToEntityList<EntityDicQnSummary>(svc.Select(new EntityDicQnSummary() { parentFieldId = fieldId }, EntityDicQnSummary.Columns.parentFieldId));
            if (data != null)
            {
                data.Sort();
                foreach (EntityDicQnSummary item in data)
                {
                    item.pyCode = SpellCodeHelper.GetPyCode(item.fieldName);
                    item.wbCode = SpellCodeHelper.GetWbCode(item.fieldName);
                }
            }
            return data;
        }
        #endregion

        #region DeleteQnTopic
        /// <summary>
        /// DeleteQnTopic
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public int DeleteQnTopic(string fieldId)
        {
            int affectRows = 0;
            SqlHelper svc = null;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();

                EntityDicQnSetting vo = new EntityDicQnSetting();
                vo.fieldId = fieldId;
                vo.parentFieldId = fieldId;
                lstParm.Add(svc.GetDelParm(vo, EntityDicQnSetting.Columns.fieldId));
                lstParm.Add(svc.GetDelParm(vo, EntityDicQnSetting.Columns.parentFieldId));

                EntityDicQnDetail vo2 = new EntityDicQnDetail();
                vo2.fieldId = fieldId;
                lstParm.Add(svc.GetDelParm(vo2, EntityDicQnDetail.Columns.fieldId));

                affectRows = svc.Commit(lstParm);
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException(e);
                affectRows = -1;
            }
            finally
            {
                svc = null;
            }
            return affectRows;
        }
        #endregion

        #region SaveQnTopic
        /// <summary>
        /// SaveQnTopic
        /// </summary>
        /// <param name="mainVo"></param>
        /// <param name="lstSub"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public int SaveQnTopic(EntityDicQnSummary mainVo, List<EntityDicQnSummary> lstSub, out string fieldId)
        {
            int affectRows = 0;
            SqlHelper svc = null;
            fieldId = string.Empty;
            try
            {
                fieldId = mainVo.fieldId;
                svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
                int num = 0;

                if (string.IsNullOrEmpty(fieldId))
                {
                    int nextId = svc.GetNextID("dicQnSummary", "fieldId");
                    fieldId = "T" + nextId.ToString().PadLeft(6, '0');
                    mainVo.fieldId = fieldId;
                    if (mainVo.sortNo == 0)
                        mainVo.sortNo = nextId;

                    if (lstSub != null)
                    {
                        foreach (EntityDicQnSummary item in lstSub)
                        {
                            item.fieldId = fieldId + Convert.ToString(++num).PadLeft(2, '0');
                        }
                    }
                }
                else
                {
                    EntityDicQnSummary vo = new EntityDicQnSummary();
                    vo.fieldId = fieldId;
                    vo.parentFieldId = fieldId;
                    lstParm.Add(svc.GetDelParm(vo, EntityDicQnSummary.Columns.fieldId));
                    lstParm.Add(svc.GetDelParm(vo, EntityDicQnSummary.Columns.parentFieldId));
                }
                if (lstSub != null)
                {
                    num = 0;
                    foreach (EntityDicQnSummary item in lstSub)
                    {
                        item.qnClassId = mainVo.qnClassId;
                        item.typeId = mainVo.typeId;
                        item.fieldId = fieldId + num;
                        item.parentFieldId = fieldId;
                        item.isEssential = mainVo.isEssential;
                        item.isParent = 0;
                        item.status = mainVo.status;
                        item.sortNo = ++num;
                        item.fieldId = fieldId + num.ToString().PadLeft(3,'0');
                    }
                }

                lstParm.Add(svc.GetInsertParm(mainVo));
                if (lstSub != null && lstSub.Count > 0)
                {
                    lstParm.Add(svc.GetInsertParm(lstSub.ToArray()));
                }
                affectRows = svc.Commit(lstParm);
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException(e);
                affectRows = -1;
            }
            finally
            {
                svc = null;
            }
            return affectRows;
        }
        #endregion

        #endregion

        #region 危险因素

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="hId"></param>
        /// <returns></returns>
        public int SaveHazards(EntityDicHazards vo, out decimal hId)
        {
            int affectRows = 0;
            hId = 0;
            string Sql = string.Empty;
            SqlHelper svc = null;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                if (vo.hId <= 0)
                {
                    Sql = @"select max(t.hId) as maxId from dicHazards t";
                    DataTable dt = svc.GetDataTable(Sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["maxId"] != DBNull.Value)
                        {
                            vo.hId = Convert.ToDecimal(dt.Rows[0]["maxId"]) + 1;
                        }
                    }
                    if (vo.hId <= 0)
                        vo.hId = 1;
                }
                if (vo.sortNo == 0)
                    vo.sortNo = (int)vo.hId;
                List<DacParm> lstParm = new List<DacParm>();
                lstParm.Add(svc.GetDelParmByPk(vo));
                lstParm.Add(svc.GetInsertParm(vo));
                affectRows = svc.Commit(lstParm);
                hId = vo.hId;
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException(e);
                affectRows = -1;
            }
            finally
            {
                svc = null;
            }
            return affectRows;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="hId"></param>
        /// <returns></returns>
        public int DeleteHazards(decimal hId)
        {
            int affectRows = 0;
            SqlHelper svc = null;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                affectRows = svc.Commit(svc.GetDelParmByPk(new EntityDicHazards() { hId = hId }));
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException(e);
                affectRows = -1;
            }
            finally
            {
                svc = null;
            }
            return affectRows;
        }
        #endregion

        #endregion

        #region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
