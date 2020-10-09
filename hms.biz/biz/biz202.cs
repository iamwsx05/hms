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
    public class Biz202 : IDisposable
    {

        #region 列表
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityDisplayClientRpt> GetTjReports(List<EntityParm> parms = null)
        {
            List<EntityDisplayClientRpt> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select clientName,
                           clientNo,
                           gender,
                           age,
                           company,
                           gradeName,
                           reportNo,
	                       regTimes,
	                       idcard,
	                       reportDate
                       from v_tjxx a where clientNo is not null ";
            List<IDataParameter> lstParm = new List<IDataParameter>();
            string strSub = string.Empty;
            if (parms != null)
            {
                foreach (var po in parms)
                {
                    switch (po.key)
                    {
                        case "search":
                            strSub += " and (a.clientName like '%" + po.value + "%' or a.clientNo like '" + po.value + "%' or a.reportNo like '%" + po.value + "%' )";
                            break;
                        case "reportDate":
                            IDataParameter parm1 = svc.CreateParm();
                            parm1.Value = po.value.Split('|')[0];
                            lstParm.Add(parm1);
                            IDataParameter parm2 = svc.CreateParm();
                            parm2.Value = po.value.Split('|')[1];
                            lstParm.Add(parm2);
                            strSub += " and (a.reportDate between ? and ? )";
                            break;
                        default:
                            break;
                    }
                }
            }

            Sql += strSub;
            Sql += " order by reportDate";

            DataTable dt = svc.GetDataTable(Sql, lstParm.ToArray());

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityDisplayClientRpt>();
                EntityDisplayClientRpt vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityDisplayClientRpt();
                    vo.clientName = dr["clientName"].ToString();
                    vo.clientNo = dr["clientNo"].ToString();
                    vo.gender = Function.Int(dr["gender"]);
                    if (vo.gender == 1)
                        vo.sex = "男";
                    if (vo.gender == 2)
                        vo.sex = "女";
                    vo.reportNo = dr["reportNo"].ToString();
                    vo.reportDate = Function.Datetime(dr["reportDate"]).ToString("yyyy-MM-dd");
                    vo.company = dr["company"].ToString();
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.age = dr["age"].ToString();
                    vo.reportCount = Function.Int(dr["regTimes"]);

                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 体检项目结果名细
        /// <summary>
        /// 
        /// </summary>
        /// <param name="regNo"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public Dictionary<string, List<EntityTjResult>> GetTjResult(string regNo, out List<EntityTjResult> dataResult, out List<EntityTjResult> xjResult,  out EntityTjjljy tjjljyVo)
        {
            dataResult = null;
            tjjljyVo = null;
            xjResult = null;
            if (string.IsNullOrEmpty(regNo))
                return null;

            Dictionary<string, List<EntityTjResult>> dicDataResult = new Dictionary<string, List<EntityTjResult>>();
            List<string> lstExaminatino = new List<string>();
            bool pFlag = false;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            IDataParameter param = null;
            string Sql = string.Empty;
            Sql = @"select a.pat_name AS clientName,
	                    a.reg_no AS regNo,
                        a.pFlag,
	                    a.sex,
	                    a.examination_no AS examination,
                        a.ttop,
	                    a.comb_code AS itemCode,
	                    a.comb_name AS itemName,
	                    a.result AS itemResult,
                        a.hint,
	                    a.bound AS range,
	                    a.unit,
	                    a.doct_name AS doctName,
	                    a.rec_date AS regDate
                    FROM V_TJBG a
                    WHERE a.reg_no = ?";
            param = svc.CreateParm();
            param.Value = regNo;
            
            DataTable dt = svc.GetDataTable(Sql,param);

            if (dt != null && dt.Rows.Count > 0)
            {
                dataResult = new List<EntityTjResult>();
                xjResult = new List<EntityTjResult>();
                EntityTjResult vo = null;
                lstExaminatino = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityTjResult();
                    vo.ttop = dr["ttop"].ToString();
                    vo.clientName = dr["clientName"].ToString();
                    vo.regNo = dr["regNo"].ToString();
                    vo.pFlag = dr["pFlag"].ToString();
                    pFlag = dr["pFlag"].ToString().Trim() == "R" ? true : false;
                    vo.sex = dr["sex"].ToString();
                    vo.itemName = dr["itemName"].ToString();
                    vo.itemCode = dr["itemCode"].ToString();
                    vo.itemResult = dr["itemResult"].ToString() + " " + dr["hint"].ToString();
                    vo.range = dr["range"].ToString();
                    vo.unit = dr["unit"].ToString();
                    vo.doctName = dr["doctName"].ToString();
                    vo.regDate = dr["regDate"].ToString();
                    vo.examinationNo = dr["examination"].ToString();
                    dataResult.Add(vo);
                    if (string.IsNullOrEmpty(vo.examinationNo) || vo.ttop == "1")
                        xjResult.Add(vo);
                    if (string.IsNullOrEmpty(vo.examinationNo) || lstExaminatino.Contains(vo.examinationNo))
                        continue;
                    lstExaminatino.Add(vo.examinationNo);
                }

                if(lstExaminatino.Count > 0)
                {
                    foreach(var examination in lstExaminatino)
                    {
                        dicDataResult.Add(examination, dataResult.FindAll(r => r.examinationNo == examination));
                    }
                }
            }

            string sqlPt = @"select results,sumup,suggTag from V_TJPTJLJY where regNo = ?";
            string sqlZyb = @"select results,sumup,suggTag from V_TJZYBJLJY where regNo = ?";

            param = svc.CreateParm();
            param.Value = regNo;
            DataTable dtTjjl = null;
            if(pFlag)
                dtTjjl = svc.GetDataTable(sqlZyb, param);
            else
                dtTjjl = svc.GetDataTable(sqlPt, param);

            if (dtTjjl != null && dtTjjl.Rows.Count > 0)
            {
                tjjljyVo = new EntityTjjljy();
                tjjljyVo.results = dtTjjl.Rows[0]["results"].ToString();
                tjjljyVo.sumup = dtTjjl.Rows[0]["sumup"].ToString();
                tjjljyVo.suggTage = dtTjjl.Rows[0]["suggTag"].ToString();
            }

            return dicDataResult;
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="regNo"></param>
        ///// <param name="deptName"></param>
        ///// <returns></returns>
        //public List<EntityTjResult>  GetTjResult(string regNo)
        //{
        //    //dataResult = null;
        //    //tjjljyVo = null;
        //    //xjResult = null;
        //    if (string.IsNullOrEmpty(regNo))
        //        return null;

        //    Dictionary<string, List<EntityTjResult>> dicDataResult = new Dictionary<string, List<EntityTjResult>>();
        //    List<string> lstExaminatino = new List<string>();
        //    bool pFlag = false;
        //    SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
        //    IDataParameter param = null;
        //    string Sql = string.Empty;
        //    Sql = @"select a.pat_name AS clientName,
	       //             a.reg_no AS regNo,
        //                a.pFlag,
	       //             a.sex,
	       //             a.examination_no AS examination,
        //                a.ttop,
	       //             a.comb_code AS itemCode,
	       //             a.comb_name AS itemName,
	       //             a.result AS itemResult,
        //                a.hint,
	       //             a.bound AS range,
	       //             a.unit,
	       //             a.doct_name AS doctName,
	       //             a.rec_date AS regDate
        //            FROM V_TJBG a
        //            WHERE a.reg_no = ?";
        //    param = svc.CreateParm();
        //    param.Value = regNo;

        //    DataTable dt = svc.GetDataTable(Sql, param);

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        //dataResult = new List<EntityTjResult>();
        //        //xjResult = new List<EntityTjResult>();
        //        EntityTjResult vo = null;
        //        lstExaminatino = new List<string>();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            vo = new EntityTjResult();
        //            vo.ttop = dr["ttop"].ToString();
        //            vo.clientName = dr["clientName"].ToString();
        //            vo.regNo = dr["regNo"].ToString();
        //            vo.pFlag = dr["pFlag"].ToString();
        //            pFlag = dr["pFlag"].ToString().Trim() == "R" ? true : false;
        //            vo.sex = dr["sex"].ToString();
        //            vo.itemName = dr["itemName"].ToString();
        //            vo.itemCode = dr["itemCode"].ToString();
        //            vo.itemResult = dr["itemResult"].ToString() + " " + dr["hint"].ToString();
        //            vo.range = dr["range"].ToString();
        //            vo.unit = dr["unit"].ToString();
        //            vo.doctName = dr["doctName"].ToString();
        //            vo.regDate = dr["regDate"].ToString();
        //            vo.examinationNo = dr["examination"].ToString();
        //            dataResult.Add(vo);
        //            if (string.IsNullOrEmpty(vo.examinationNo) || vo.ttop == "1")
        //                xjResult.Add(vo);
        //            if (string.IsNullOrEmpty(vo.examinationNo) || lstExaminatino.Contains(vo.examinationNo))
        //                continue;
        //            lstExaminatino.Add(vo.examinationNo);
        //        }

        //        if (lstExaminatino.Count > 0)
        //        {
        //            foreach (var examination in lstExaminatino)
        //            {
        //                dicDataResult.Add(examination, dataResult.FindAll(r => r.examinationNo == examination));
        //            }
        //        }
        //    }

        //    string sqlPt = @"select results,sumup,suggTag from V_TJPTJLJY where regNo = ?";
        //    string sqlZyb = @"select results,sumup,suggTag from V_TJZYBJLJY where regNo = ?";

        //    param = svc.CreateParm();
        //    param.Value = regNo;
        //    DataTable dtTjjl = null;
        //    if (pFlag)
        //        dtTjjl = svc.GetDataTable(sqlZyb, param);
        //    else
        //        dtTjjl = svc.GetDataTable(sqlPt, param);

        //    if (dtTjjl != null && dtTjjl.Rows.Count > 0)
        //    {
        //        tjjljyVo = new EntityTjjljy();
        //        tjjljyVo.results = dtTjjl.Rows[0]["results"].ToString();
        //        tjjljyVo.sumup = dtTjjl.Rows[0]["sumup"].ToString();
        //        tjjljyVo.suggTage = dtTjjl.Rows[0]["suggTag"].ToString();
        //    }

        //    return dicDataResult;
        //}
        #endregion

        #region  获取表单控件
        /// <summary>
        /// 获取表单控件
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntitySetQnControl> GetQnControl(List<EntityParm> parms = null)
        {
            List<EntitySetQnControl> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select a.qnId,
                            a.qnName,
                            a.classId,
                            b.fieldId as parentFieldId,
                            c.fieldName as parentFieldName,
                            c.typeId,
                            d.fieldId as childFieldId,
                            d.fieldName as childFieldName
                            from dicQnMain a 
                            left join dicQnDetail b
                            on a.qnId = b.qnId
                            left join dicQnSetting c
                            on b.fieldId = c.fieldId
                            left join dicQnSetting d
                            on b.fieldId = d.parentFieldId
                            where a.qnId = 2 order by c.sortNo,d.sortNo ";
            DataTable dt = svc.GetDataTable(Sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntitySetQnControl>();
                EntitySetQnControl vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntitySetQnControl();
                    vo.qnId = dr["qnId"].ToString();
                    vo.qnName = dr["qnName"].ToString();
                    vo.classId = dr["classId"].ToString();
                    vo.parentFieldId = dr["parentFieldId"].ToString();
                    vo.parentFieldName = dr["parentFieldName"].ToString();
                    vo.typeId = dr["typeId"].ToString();
                    vo.childFieldId = dr["childFieldId"].ToString();
                    vo.childFieldName = dr["childFieldName"].ToString();

                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 获取原始表单控件信息
        /// <summary>
        /// 获取原始表单控件信息
        /// </summary>
        /// <param name="qnCtlFiledId"></param>
        /// <returns></returns>
        public List<EntityCtrlLocation> GetQnCtrlLocation(string qnCtlFiledId,decimal qnId)
        {
            if (string.IsNullOrEmpty(qnCtlFiledId))
                return null;
            List<EntityCtrlLocation> data = null;
            
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select * from dicQnCtlLocation a where a.qnCtlFiledId = ? and a.qnId = ?" ;
            IDataParameter[] param = svc.CreateParm(2);
            param[0].Value = qnCtlFiledId;
            param[1].Value = qnId;
            DataTable dt = svc.GetDataTable(Sql,param);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityCtrlLocation>();
                EntityCtrlLocation vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    string xml = dr["xmlData"].ToString();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNodeList nodelist = doc["eflayout"].GetElementsByTagName("ctrl");
                    if (nodelist != null && nodelist.Count > 0)
                    {
                        foreach (XmlNode node in nodelist)
                        {
                            vo = new EntityCtrlLocation();
                            vo.name = node.Attributes["ctrlname"].Value;
                            vo.text = node.Attributes["ctrlText"].Value;
                            vo.width = Function.Int(node.Attributes["width"].Value);
                            vo.height = Function.Int(node.Attributes["height"].Value);
                            vo.locationX = Function.Int( node.Attributes["locationX"].Value);
                            vo.locationY = Function.Int(node.Attributes["locationY"].Value);
                            string type = node.Attributes["ctrltype"].Value;
                            if (type.Contains("LabelControl"))
                                vo.type = 1;
                            if (type.Contains("CheckEdit"))
                                vo.type = 2;

                            data.Add(vo);
                        }
                    }
                }
            }
            return data.OrderBy(r=>r.name).ToList();
        }
        #endregion

        #region 问卷记录
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityQnRecord> GetQnRecords(List<EntityParm> parms)
        {
            List<EntityQnRecord> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select a.recId,
                           b.clientNo,
                           b.clientName,
                           b.gender,
						   b.gradeName,
                           b.birthday,
                           a.qnName,
						   a.qnType,
                           a.qnDate,
                           a.qnSource,
                           a.qnId,
                           d.oper_name as recorder,
                           q.xmlData
                        from qnRecord a
                     left join qnData q
                        on a.recId = q.recId
                     inner join v_clientInfo b
                        on a.clientNo = b.clientNo
                      left join code_operator d
                        on a.recorder = d.oper_code 
                            where a.status != -1";

            string sub = string.Empty;
            if(parms != null)
            {
                foreach(EntityParm pa in parms)
                {
                    string key = pa.key;

                    switch (key)
                    {
                        case "qnType":
                            sub += " and a.qnType = '" + pa.value + "'" ;
                            break;
                        case "clientNo":
                            sub += " and a.clientNo = '" + pa.value + "'";
                            break;
                        case "search":
                            sub += "and a.clientNo like '%" + pa.value + "%' or a.clientName like '%" + pa.value + "%'";
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(sub))
                Sql += sub;

            DataTable dt = svc.GetDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityQnRecord>();
                EntityQnRecord vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityQnRecord();
                    vo.recId = Function.Dec(dr["recId"]) ;
                    vo.gender = Function.Int(dr["gender"]);
                    if(vo.gender == 1)
                        vo.sex = "男";
                    if (vo.gender == 2)
                        vo.sex = "女";
                     vo.clientNo = dr["clientNo"].ToString();
                    vo.clientName = dr["clientName"].ToString();
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.age = dr["birthday"] == DBNull.Value ? "" : CalcAge.GetAge(Function.Datetime(dr["birthday"]));
                    vo.qnName = dr["qnName"].ToString();
                    vo.qnId = Function.Dec(dr["qnId"]);
                    vo.qnSource = Function.Dec(dr["qnSource"]);
                    if (vo.qnSource == 1)
                        vo.strQnSource = "采集系统";
                    vo.qnDate = Function.Datetime(dr["qnDate"]);
                    vo.strQnDate = Function.Datetime(dr["qnDate"]).ToString("yyyy-MM-dd");
                    vo.recorder = dr["recorder"].ToString();
                    vo.xmlData = dr["xmlData"].ToString();
                    if (data.Any(r => r.recId == vo.recId))
                        continue;
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 问卷-保存
        /// <summary>
        /// 常规问卷-保存
        /// </summary>
        /// <param name="sfData"></param>
        /// <param name="sfId"></param>
        /// <returns></returns>
        public int SaveQnRecord(EntityQnRecord qnRecord,EntityQnData qnData, out decimal recId)
        {
            int affectRows = 0;
            recId = 0;
            string Sql = string.Empty;
            SqlHelper svc = null;
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                svc = new SqlHelper(EnumBiz.onlineDB);
                if (qnRecord.recId <= 0)
                {
                    recId = svc.GetNextID("qnRecord", "recId");
                    qnRecord.recId = recId;
                    qnRecord.recorder = "00";
                    qnRecord.recordDate = DateTime.Now;
                    qnRecord.status = 0;
                    qnData.recId = recId;
                    
                    lstParm.Add(svc.GetInsertParm(qnRecord));
                    lstParm.Add(svc.GetInsertParm(qnData));
                }
                else
                {
                    recId = qnRecord.recId;
                    lstParm.Add(svc.GetUpdateParm(qnRecord, 
                        new List<string> { EntityQnRecord.Columns.clientNo,
                            EntityQnRecord.Columns.qnType,
                            EntityQnRecord.Columns.qnSource}, 
                        new List<string> { EntityQnRecord.Columns.recId }));
                    lstParm.Add(svc.GetUpdateParm(qnData, 
                        new List<string> { EntityQnData.Columns.xmlData},
                        new List<string> { EntityQnData.Columns.recId }));
                }

                if(lstParm.Count > 0)
                {
                    affectRows = svc.Commit(lstParm);
                }
               
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

        #region 问卷-删除
        /// <summary>
        /// 问卷-删除
        /// </summary>
        /// <param name="sfData"></param>
        /// <param name="sfId"></param>
        /// <returns></returns>
        public int DelQnRecord(List<EntityQnRecord> qnRecords)
        {
            int affectRows = -1;
            string Sql = string.Empty;
            SqlHelper svc = null;
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                svc = new SqlHelper(EnumBiz.onlineDB);
                if (qnRecords == null )
                    return affectRows;

                Sql = @"update qnRecord set status = -1 where recId = ?";

                foreach (var vo in qnRecords)
                {
                    IDataParameter [] param = svc.CreateParm(1);
                    param[0].Value = vo.recId;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql,Sql, param));
                }

                if (lstParm.Count > 0)
                {
                    affectRows = svc.Commit(lstParm);
                }

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

        #region  获取自定义表单
        /// <summary>
        /// 获取自定义表单
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityDicQnMain> GetQnMain(List<EntityParm> parms)
        {
            List<EntityDicQnMain> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select a.qnid,a.qnName,a.classId,a.qnDesc from dicQnMain a where a.qnid > -1 ";
            string sub = string.Empty;
            if (parms != null)
            {
                foreach (EntityParm pa in parms)
                {
                    string key = pa.key;

                    switch (key)
                    {
                        case "class":
                            sub += " and a.classId = '" + pa.value + "'";
                            break;
                        default:
                            break;
                    }
                }
            }
            Sql += sub;
            DataTable dt = svc.GetDataTable(Sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityDicQnMain>();
                EntityDicQnMain vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityDicQnMain();
                    vo = Function.Row2Model<EntityDicQnMain>(dr);
                    data.Add(vo);
                }
            }
            return data;
        }
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
