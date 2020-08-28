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
using System.Reflection;

namespace Hms.Biz
{
    public class Biz205 : IDisposable
    {
        #region 高血压

        #region 人员列表
        /// <summary>
        /// 人员列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityGxyRecord> GetGxyPatients(List<EntityParm> parms)
        {
            List<EntityGxyRecord> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select a.recId,a.regNo,
                           b.clientNo,
                           b.clientName,
                           a.regTimes,
                           b.gender,
                           b.birthday,
                           b.gradeName,
                           b.company,
                           manageLevel,
                           sfTimes ,
                           pgTimes ,
                           a.beginDate,
                           a.nextSfDate,
                           0 as planTimes
                      from gxyRecord a
                     inner join V_ClientInfo b
                        on a.clientNo = b.clientNo  and a.regTimes = b.regTimes
                    where a.recid >= 0 ";
            string subStr = string.Empty;
            List<IDataParameter> lstParm = new List<IDataParameter>();
            if (parms != null)
            {
                foreach (var po in parms)
                {
                    switch (po.key)
                    {
                        case "queryDate":
                            IDataParameter[] param = svc.CreateParm(2);
                            param[0].Value = po.value.Split('|')[0] + " 00:00:00";
                            param[1].Value = po.value.Split('|')[1] + " 23:59:59";
                            subStr += " and a.recordDate between ? and ?";
                            lstParm.AddRange(param);
                            break;
                        case "clientNo":
                            subStr += " and a.clientNo = '" + po.value + "'";
                            break;
                        case "regTimes":
                            subStr += " and a.regTimes = '" + po.value + "'";
                            break;
                        default:
                            break;
                    }
                }
            }
            Sql += subStr;
            DataTable dt = svc.GetDataTable(Sql, lstParm);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityGxyRecord>();
                EntityGxyRecord vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityGxyRecord();
                    vo.recId = Function.Dec(dr["recId"]);
                    vo.regNo = dr["regNo"].ToString();
                    vo.regTimes = Function.Int(dr["regTimes"]);
                    vo.clientNo = dr["clientNo"].ToString();
                    vo.clientName = dr["clientName"].ToString();
                    string gender = dr["gender"].ToString();
                    if (gender == "1")
                        vo.sex = "男";
                    else if (gender == "2")
                        vo.sex = "女";
                    vo.age = dr["birthday"] == DBNull.Value ? "" : Function.CalcAge(Function.Datetime(dr["birthday"]));
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.company = dr["company"].ToString();
                    vo.beginDateStr = dr["beginDate"] == DBNull.Value ? "" : Function.Datetime(dr["beginDate"]).ToString("yyyy-MM-dd HH:mm");
                    vo.manageLevel = dr["manageLevel"].ToString();
                    if (vo.manageLevel == "1")
                        vo.manageLevel = "一级管理";
                    if (vo.manageLevel == "2")
                        vo.manageLevel = "二级管理";
                    if (vo.manageLevel == "3")
                        vo.manageLevel = "三级管理";

                    vo.sfNextDateStr = dr["nextSfDate"] == DBNull.Value ? "" : Function.Datetime(dr["nextSfDate"]).ToString("yyyy-MM-dd HH:mm");
                    vo.sfTimes = Function.Dec(dr["sfTimes"]);
                    vo.pgTimes = Function.Dec(dr["pgTimes"]);

                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 保存人员记录
        /// <summary>
        /// 保存人员记录
        /// </summary>
        /// <param name="gxyRecord"></param>
        /// <param name="recId"></param>
        /// <returns></returns>
        public int SaveGxyRecord(EntityGxyRecord gxyRecord, out decimal recId)
        {
            int affectRows = 0;
            recId = 0;
            string Sql = string.Empty;
            SqlHelper svc = null;
            try
            {
                if (gxyRecord == null)
                    return -1;
                decimal id = 0;
                List<DacParm> lstParm = new List<DacParm>();
                svc = new SqlHelper(EnumBiz.onlineDB);

                if (gxyRecord.recId <= 0)
                {
                    string sql = @"insert into gxyRecord(recid,clientno,regtimes,regno,beginDate,recorder,recorddate,status) values (?,?,?,?,?,?,?,?)";
                    id = svc.GetNextID("gxyRecord", "recId");
                    gxyRecord.recordDate = DateTime.Now;
                    gxyRecord.beginDate = DateTime.Now;
                    gxyRecord.recorder = "00";
                    IDataParameter[] param = svc.CreateParm(8);
                    param[0].Value = id;
                    param[1].Value = gxyRecord.clientNo;
                    param[2].Value = gxyRecord.regTimes;
                    param[3].Value = gxyRecord.regNo;
                    param[4].Value = gxyRecord.beginDate;
                    param[5].Value = gxyRecord.recorder;
                    param[6].Value = gxyRecord.recordDate;
                    param[7].Value = gxyRecord.status;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param));
                }
                else
                {
                    string sql = @"update gxyRecord set beginDate = ?, manageLevel = ?, nextSfDate = ? where recId = ?";
                    IDataParameter[] param = svc.CreateParm(4);
                    param[0].Value = gxyRecord.beginDate;
                    param[1].Value = gxyRecord.manageLevel;
                    param[2].Value = gxyRecord.nextSfDate;
                    param[3].Value = gxyRecord.recId;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param));
                }
                recId = id;

                if (lstParm.Count > 0)
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

        #region 随访记录-获取
        /// <summary>
        /// 随访记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityGxySf> GetGxySfRecords(List<EntityParm> parms)
        {
            List<EntityGxySf> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @" select a.recId,
                           b.clientNo,
                           b.clientName,
                           b.gender,
                           b.birthday,
                           b.gradeName,
                          a.sfId,
                           a.sfDate,
                           a.sfMethod,
                           a.sfClass,
                           e.xmlData,
                           d.oper_name as sfRecorder
                      from gxySf a
                     left join  gxyRecord c
                        on a.recId = c.recId 
											inner join V_ClientInfo  b
                        on c.clientNo = b.clientNo and c.regTimes = b.regTimes
                     left join gxySfData e
						       on a.sfId =  e.sfId
                      left join code_operator d
                        on a.sfRecorder = d.oper_code
								where a.sfStatus = 1 ";

            DataTable dt = svc.GetDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityGxySf>();
                EntityGxySf vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityGxySf();
                    vo.recId = Function.Dec(dr["recId"]);
                    vo.sfId = Function.Int(dr["sfId"]);
                    vo.sfDateStr = dr["sfDate"] == DBNull.Value ? "" : Function.Datetime(dr["sfDate"]).ToString("yyyy-MM-dd HH:mm");
                    SetClientInfo(ref vo, dr);
                    int sfMethod = Function.Int(dr["sfMethod"]);
                    if (sfMethod == 1)
                        vo.sfMethod = "上门";
                    if (sfMethod == 2)
                        vo.sfMethod = "电话";
                    if (sfMethod == 3)
                        vo.sfMethod = "门诊";
                    vo.sfClass = (dr["sfClass"].ToString() == "01" ? "普通" : "其他");
                    vo.sfData = dr["xmlData"].ToString();
                    vo.sfRecorder = dr["sfRecorder"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 随访记录-保存
        /// <summary>
        /// 随访记录-保存
        /// </summary>
        /// <param name="sfData"></param>
        /// <param name="sfId"></param>
        /// <returns></returns>
        public int SaveGxySfRecord(EntityGxyRecord gxyRecord, EntityGxySf gxySf, EntityGxySfData sfData, out decimal sfId)
        {
            int affectRows = 0;
            sfId = 0;
            string Sql = string.Empty;
            SqlHelper svc = null;
            decimal id = 0;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
                svc = new SqlHelper(EnumBiz.onlineDB);
                if (gxySf.sfId <= 0)
                {
                    id = svc.GetNextID("gxySf", "sfId");
                    gxySf.sfId = id;
                    gxySf.sfStatus = 1;
                    gxySf.recordDate = DateTime.Now;
                    gxyRecord.sfId = id;
                    gxyRecord.sfTimes += 1;
                    sfData.sfId = id;
                    lstParm.Add(svc.GetInsertParm(gxySf));
                }
                else
                {
                    lstParm.Add(svc.GetUpdateParm(gxySf,
                      new List<string> { EntityGxySf.Columns.sfMethod,
                          EntityGxySf.Columns.sfClass,
                          EntityGxySf.Columns.sfDate },
                      new List<string> { EntityGxySf.Columns.sfId }));
                }
                //随访数据
                lstParm.Add(svc.GetDelParm(sfData, EntityGxySfData.Columns.sfId));
                lstParm.Add(svc.GetInsertParm(sfData));
                if (gxyRecord.sfTimes > 0)
                {
                    //高血压下次随访数据
                    string sql = @"update gxyRecord set manageLevel = ?, nextSfDate = ?, sfId = ?,sfTimes = ? where recId = ?";
                    IDataParameter[] param = svc.CreateParm(5);
                    param[0].Value = gxyRecord.manageLevel;
                    param[1].Value = gxyRecord.nextSfDate;
                    param[2].Value = gxyRecord.sfId;
                    param[3].Value = gxyRecord.sfTimes;
                    param[4].Value = gxyRecord.recId;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param));
                }
                else
                {
                    //高血压下次随访数据
                    string sql = @"update gxyRecord set manageLevel = ?, nextSfDate = ?, sfId = ? where recId = ?";
                    IDataParameter[] param = svc.CreateParm(4);
                    param[0].Value = gxyRecord.manageLevel;
                    param[1].Value = gxyRecord.nextSfDate;
                    param[2].Value = gxyRecord.sfId;
                    param[3].Value = gxyRecord.recId;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param));
                }

                if (lstParm.Count > 0)
                    affectRows = svc.Commit(lstParm);
                sfId = id;
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

        #region 评估记录-获取
        /// <summary>
        /// 评估记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityGxyPg> GetGxyPgRecords(List<EntityParm> parms)
        {
            List<EntityGxyPg> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @" select a.recId,
                           b.clientNo,
                           b.clientName,
                           b.gender,
                           b.birthday,
                           b.gradeName,
                           a.pgId,
                           a.bloodPressLevel,
                           a.dangerLevel,
                           a.manageLevel,
                           a.evaDate,
                           e.xmlData,
                           d.oper_name as pgRecorder
                      from gxyPg a
                     inner join gxyRecord c
                        on a.recId = c.recId 
					inner join V_ClientInfo  b
                        on c.clientNo = b.clientNo and c.regTimes = b.regTimes
                    left join gxyPgData e
						on a.pgId =  e.pgId
                    left join code_operator d
                        on a.evaluator = d.oper_code
                    where a.status = 1 ";

            DataTable dt = svc.GetDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityGxyPg>();
                EntityGxyPg vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityGxyPg();
                    vo.pgId = Function.Dec(dr["pgId"]);
                    vo.recId = Function.Dec(dr["recId"]);
                    SetClientInfo(ref vo, dr);
                    vo.evaDateStr = dr["evaDate"] == DBNull.Value ? "" : Function.Datetime(dr["evaDate"]).ToString("yyyy-MM-dd");
                    vo.bloodPressLevel = dr["bloodPressLevel"].ToString();
                    if (vo.bloodPressLevel == "1")
                        vo.bloodPressLevel = "正常血压";
                    if (vo.bloodPressLevel == "2")
                        vo.bloodPressLevel = "正常高值";
                    if (vo.bloodPressLevel == "3")
                        vo.bloodPressLevel = "一级高血压";
                    if (vo.bloodPressLevel == "4")
                        vo.bloodPressLevel = "二级高血压";
                    if (vo.bloodPressLevel == "5")
                        vo.bloodPressLevel = "三级高血压";
                    if (vo.bloodPressLevel == "6")
                        vo.bloodPressLevel = "单纯收缩期高血压";
                    vo.dangerLevel = dr["dangerLevel"].ToString();
                    if (vo.dangerLevel == "1")
                        vo.dangerLevel = "低危";
                    if (vo.dangerLevel == "2")
                        vo.dangerLevel = "中危";
                    if (vo.dangerLevel == "3")
                        vo.dangerLevel = "高危";
                    vo.manageLevel = dr["manageLevel"].ToString();
                    if (vo.manageLevel == "1")
                        vo.manageLevel = "一级管理";
                    if (vo.manageLevel == "2")
                        vo.manageLevel = "二级管理";
                    if (vo.manageLevel == "3")
                        vo.manageLevel = "三级管理";
                    vo.evaluator = dr["pgRecorder"].ToString();
                    vo.pgData = dr["xmlData"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 评估记录-保存
        /// <summary>
        /// 评估记录-保存
        /// </summary>
        /// <param name="pgData"></param>
        /// <param name="pgId"></param>
        /// <returns></returns>
        public int SaveGxyPgRecord(EntityGxyRecord gxyRecord, EntityGxyPg gxyPg, EntityGxyPgData pgData, out decimal pgId)
        {
            int affectRows = 0;
            pgId = 0;
            string Sql = string.Empty;
            SqlHelper svc = null;
            decimal id = 0;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
                svc = new SqlHelper(EnumBiz.onlineDB);
                if (gxyPg.pgId <= 0)
                {
                    id = svc.GetNextID("gxyPg", "pgId");
                    gxyPg.status = 1;
                    gxyPg.pgId = id;
                    gxyPg.recordDate = DateTime.Now;
                    gxyRecord.pgTimes += 1;
                    lstParm.Add(svc.GetInsertParm(gxyPg));

                    //高血压下次随访数据
                    string sql = @"update gxyRecord set pgTimes = ? where recId = ?";
                    IDataParameter[] param = svc.CreateParm(2);
                    param[0].Value = gxyRecord.pgTimes;
                    param[1].Value = gxyRecord.recId;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param));
                }
                else
                {
                    lstParm.Add(svc.GetUpdateParm(gxyPg,
                      new List<string> { EntityGxyPg.Columns.bloodPressLevel,
                          EntityGxyPg.Columns.dangerLevel,
                          EntityGxyPg.Columns.manageLevel,
                          EntityGxyPg.Columns.evaDate,},
                      new List<string> { EntityGxyPg.Columns.pgId }));
                }
                pgData.pgId = id;
                //评估数据
                lstParm.Add(svc.GetDelParm(pgData, EntityGxyPgData.Columns.pgId));
                lstParm.Add(svc.GetInsertParm(pgData));
                if (lstParm.Count > 0)
                    affectRows = svc.Commit(lstParm);
                pgId = id;
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

        #region 体检结果 血压
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientNo"></param>
        /// <returns></returns>
        public List<EntityClientGxyResult> GetClientGxyResults(string clientNoStr)
        {
            if (string.IsNullOrEmpty(clientNoStr))
                return null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            List<EntityClientGxyResult> data = null;

            string sql = @"select a.clientNo,a.regTimes,b.reg_no,b.reg_time,b.comb_code,b.comb_name,b.unit, b.result 
                            from  V_RportRecord a 
                            left join V_TJBG b 
                            on a.reportId = b.reg_no
                            where b.comb_code in('10001 ','10002 ','10003 ','10056 ','10050 ','230010','010014','010013')
                            and a.clientNo in {0}
                            order by b.reg_time desc ";
            sql = string.Format(sql, clientNoStr);
            DataTable dt = svc.GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityClientGxyResult>();
                EntityClientGxyResult vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    string clientNo = dr["clientNo"].ToString();
                    string regNo = dr["reg_no"].ToString();
                    string comb_code = dr["comb_code"].ToString();
                    string result = dr["result"].ToString();
                    if (data.Any(r => r.regNo == regNo))
                    {
                        EntityClientGxyResult voClone = data.Find(r => r.regNo == regNo);
                        if (result.Contains("高血压"))
                            voClone.gxyYc = result;
                        if (comb_code == "010014")
                        {
                            if (string.IsNullOrEmpty(voClone.gxy))
                            {
                                voClone.gxy = result;
                            }
                            else
                            {
                                voClone.gxy = voClone.gxy + result + " mmHg";
                            }
                        }
                        if (comb_code == "010013")
                        {
                            if (string.IsNullOrEmpty(voClone.gxy))
                            {
                                voClone.gxy = result + "/";
                            }
                            else
                            {
                                voClone.gxy = result + "/" + voClone.gxy + " mmHg";
                            }
                        }
                    }
                    else
                    {
                        vo = new EntityClientGxyResult();
                        vo.clientNo = clientNo;
                        vo.regNo = regNo;
                        vo.regTimes = Function.Int(dr["regTimes"]);
                        if (result.Contains("高血压"))
                            vo.gxyYc = result;
                        if (comb_code == "010014")
                        {
                            if (string.IsNullOrEmpty(vo.gxy))
                            {
                                vo.gxy = result;
                            }
                            else
                            {
                                vo.gxy = vo.gxy + result;
                            }
                        }
                        if (comb_code == "010013")
                        {
                            if (string.IsNullOrEmpty(vo.gxy))
                            {
                                vo.gxy = result + "/";
                            }
                            else
                            {
                                vo.gxy = result + "/" + vo.gxy;
                            }
                        }
                        data.Add(vo);
                    }
                }
            }
            return data;
        }


        #endregion

        #endregion

        #region 糖尿病

        #region 人员列表
        /// <summary>
        /// 人员列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityTnbRecord> GetTnbPatients(List<EntityParm> parms)
        {
            List<EntityTnbRecord> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select a.recId,a.regNo,
                           b.clientNo,
                           b.clientName,
                           a.regTimes,
                           b.gender,
                           b.birthday,
                           b.gradeName,
                           b.company,
                           manageLevel,
                           sfTimes ,
                           pgTimes ,
                           a.beginDate,
                           a.nextSfDate,
                           0 as planTimes
                      from tnbRecord a
                     inner join V_ClientInfo b
                        on a.clientNo = b.clientNo  and a.regTimes = b.regTimes
                    where a.recid >= 0 ";
            string subStr = string.Empty;
            List<IDataParameter> lstParm = new List<IDataParameter>();
            if (parms != null)
            {
                foreach (var po in parms)
                {
                    switch (po.key)
                    {
                        case "queryDate":
                            IDataParameter[] param = svc.CreateParm(2);
                            param[0].Value = po.value.Split('|')[0] + " 00:00:00";
                            param[1].Value = po.value.Split('|')[1] + " 23:59:59";
                            subStr += " and a.recordDate between ? and ?";
                            lstParm.AddRange(param);
                            break;
                        case "clientNo":
                            subStr += " and a.clientNo = '" + po.value + "'";
                            break;
                        case "regTimes":
                            subStr += " and a.regTimes = '" + po.value + "'";
                            break;
                        default:
                            break;
                    }
                }
            }
            Sql += subStr;
            DataTable dt = svc.GetDataTable(Sql, lstParm);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityTnbRecord>();
                EntityTnbRecord vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityTnbRecord();
                    vo.recId = Function.Dec(dr["recId"]);
                    vo.regNo = dr["regNo"].ToString();
                    vo.regTimes = Function.Int(dr["regTimes"]);
                    vo.clientNo = dr["clientNo"].ToString();
                    vo.clientName = dr["clientName"].ToString();
                    string gender = dr["gender"].ToString();
                    if (gender == "1")
                        vo.sex = "男";
                    else if (gender == "2")
                        vo.sex = "女";
                    vo.age = dr["birthday"] == DBNull.Value ? "" : Function.CalcAge(Function.Datetime(dr["birthday"]));
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.company = dr["company"].ToString();
                    vo.beginDateStr = dr["beginDate"] == DBNull.Value ? "" : Function.Datetime(dr["beginDate"]).ToString("yyyy-MM-dd HH:mm");
                    vo.manageLevel = dr["manageLevel"].ToString();
                    if (vo.manageLevel == "1")
                        vo.manageLevel = "一级管理";
                    if (vo.manageLevel == "2")
                        vo.manageLevel = "二级管理";
                    if (vo.manageLevel == "3")
                        vo.manageLevel = "三级管理";
                    vo.sfNextDateStr = dr["nextSfDate"] == DBNull.Value ? "" : Function.Datetime(dr["nextSfDate"]).ToString("yyyy-MM-dd HH:mm");
                    vo.sfTimes = Function.Dec(dr["sfTimes"]);
                    vo.pgTimes = Function.Dec(dr["pgTimes"]);

                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 保存人员记录
        /// <summary>
        /// 保存人员记录
        /// </summary>
        /// <param name="tnbRecord"></param>
        /// <param name="recId"></param>
        /// <returns></returns>
        public int SaveTnbRecord(EntityTnbRecord tnbRecord, out decimal recId)
        {
            int affectRows = 0;
            recId = 0;
            string Sql = string.Empty;
            SqlHelper svc = null;
            try
            {
                if (tnbRecord == null)
                    return -1;
                decimal id = 0;
                List<DacParm> lstParm = new List<DacParm>();
                svc = new SqlHelper(EnumBiz.onlineDB);

                if (tnbRecord.recId <= 0)
                {
                    string sql = @"insert into tnbRecord(recid,clientno,regtimes,regno,beginDate,recorder,recorddate,status) values (?,?,?,?,?,?,?,?)";
                    id = svc.GetNextID("tnbRecord", "recId");
                    tnbRecord.recordDate = DateTime.Now;
                    tnbRecord.beginDate = DateTime.Now;
                    tnbRecord.recorder = "00";
                    IDataParameter[] param = svc.CreateParm(8);
                    param[0].Value = id;
                    param[1].Value = tnbRecord.clientNo;
                    param[2].Value = tnbRecord.regTimes;
                    param[3].Value = tnbRecord.regNo;
                    param[4].Value = tnbRecord.beginDate;
                    param[5].Value = tnbRecord.recorder;
                    param[6].Value = tnbRecord.recordDate;
                    param[7].Value = tnbRecord.status;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param));
                }
                else
                {
                    string sql = @"update tnbRecord set beginDate = ?, manageLevel = ?, nextSfDate = ? where recId = ?";
                    IDataParameter[] param = svc.CreateParm(4);
                    param[0].Value = tnbRecord.beginDate;
                    param[1].Value = tnbRecord.manageLevel;
                    param[2].Value = tnbRecord.nextSfDate;
                    param[3].Value = tnbRecord.recId;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param));
                }
                recId = id;

                if (lstParm.Count > 0)
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

        #region 随访记录-获取
        /// <summary>
        /// 随访记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityTnbSf> GetTnbSfRecords(List<EntityParm> parms)
        {
            List<EntityTnbSf> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"  select a.recId,
                           b.clientNo,
                           b.clientName,
                           b.gender,
                           b.birthday,
                           b.gradeName,
                           a.sfId,
                           a.sfDate,
                           a.sfMethod,
                           a.sfClass,
                           e.xmlData,
                           d.oper_name as sfRecorder
                      from tnbSf a
						left join tnbRecord c
                        on a.recId = c.recId
                     inner join V_ClientInfo  b
                        on c.clientNo = b.clientNo and c.regTimes = b.regTimes
                     left join tnbSfData e
						on a.sfId =  e.sfId
                      left join code_operator d
                        on a.sfRecorder = d.oper_code 
                        where a.sfStatus = 1";

            DataTable dt = svc.GetDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityTnbSf>();
                EntityTnbSf vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityTnbSf();
                    vo.recId = Function.Dec(dr["recId"]);
                    vo.sfId = Function.Int(dr["sfId"]);
                    vo.sfDateStr = dr["sfDate"] == DBNull.Value ? "" : Function.Datetime(dr["sfDate"]).ToString("yyyy-MM-dd HH:mm");
                    SetClientInfo(ref vo, dr);
                    int sfMethod = Function.Int(dr["sfMethod"]);
                    if (sfMethod == 1)
                        vo.sfMethod = "上门";
                    if (sfMethod == 2)
                        vo.sfMethod = "电话";
                    if (sfMethod == 3)
                        vo.sfMethod = "门诊";
                    vo.sfClass = (dr["sfClass"].ToString() == "01" ? "普通" : "其他");
                    vo.sfData = dr["xmlData"].ToString();
                    vo.sfRecorder = dr["sfRecorder"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 随访记录-保存
        /// <summary>
        /// 随访记录-保存
        /// </summary>
        /// <param name="sfData"></param>
        /// <param name="sfId"></param>
        /// <returns></returns>
        public int SaveTnbSfRecord(EntityTnbRecord tnbRecord, EntityTnbSf tnbSf, EntityTnbSfData sfData, out decimal sfId)
        {
            int affectRows = 0;
            sfId = 0;
            string Sql = string.Empty;
            SqlHelper svc = null;
            decimal id = 0;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
                svc = new SqlHelper(EnumBiz.onlineDB);
                if (tnbSf.sfId <= 0)
                {
                    id = svc.GetNextID("tnbSf", "sfId");
                    tnbSf.sfId = id;
                    tnbSf.sfStatus = 1;
                    tnbSf.recordDate = DateTime.Now;
                    tnbRecord.sfId = id;
                    tnbRecord.sfTimes += 1;
                    sfData.sfId = id;
                    lstParm.Add(svc.GetInsertParm(tnbSf));
                }
                else
                {
                    lstParm.Add(svc.GetUpdateParm(tnbSf,
                      new List<string> { EntityTnbSf.Columns.sfMethod,
                          EntityTnbSf.Columns.sfClass,
                          EntityTnbSf.Columns.sfDate },
                      new List<string> { EntityTnbSf.Columns.sfId }));
                }
                //随访数据
                lstParm.Add(svc.GetDelParm(sfData, EntityTnbSfData.Columns.sfId));
                lstParm.Add(svc.GetInsertParm(sfData));
                if (tnbRecord.sfTimes > 0)
                {
                    //高血压下次随访数据
                    string sql = @"update tnbRecord set manageLevel = ?, nextSfDate = ?, sfId = ?,sfTimes = ? where recId = ?";
                    IDataParameter[] param = svc.CreateParm(5);
                    param[0].Value = tnbRecord.manageLevel;
                    param[1].Value = tnbRecord.nextSfDate;
                    param[2].Value = tnbRecord.sfId;
                    param[3].Value = tnbRecord.sfTimes;
                    param[4].Value = tnbRecord.recId;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param));
                }
                else
                {
                    //高血压下次随访数据
                    string sql = @"update tnbRecord set manageLevel = ?, nextSfDate = ?, sfId = ? where recId = ?";
                    IDataParameter[] param = svc.CreateParm(4);
                    param[0].Value = tnbRecord.manageLevel;
                    param[1].Value = tnbRecord.nextSfDate;
                    param[2].Value = tnbRecord.sfId;
                    param[3].Value = tnbRecord.recId;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param));
                }

                if (lstParm.Count > 0)
                    affectRows = svc.Commit(lstParm);
                sfId = id;
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

        #region 评估记录-获取
        /// <summary>
        /// 评估记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityTnbPg> GetTnbPgRecords(List<EntityParm> parms)
        {
            List<EntityTnbPg> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @" select a.recId,
                           b.clientNo,
                           b.clientName,
                           b.gender,
                           b.birthday,
                           b.gradeName,
                           a.pgId,
                           a.dangerLevel,
                           a.manageLevel,
                           a.evaDate,
                           e.xmlData,
                           d.oper_name as pgRecorder
                      from tnbPg a
                     left join tnbRecord c
                        on a.recId = c.recId
					 inner join V_ClientInfo  b
                        on c.clientNo = b.clientNo and c.regTimes = b.regTimes
                     left join tnbPgData e
						on a.pgId =  e.pgId
                     left join code_operator d
                        on a.evaluator = d.oper_code
					where a.status = 1";

            DataTable dt = svc.GetDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityTnbPg>();
                EntityTnbPg vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityTnbPg();
                    vo.pgId = Function.Dec(dr["pgId"]);
                    vo.recId = Function.Dec(dr["recId"]);
                    SetClientInfo(ref vo, dr);
                    vo.dangerLevel = dr["dangerLevel"].ToString();
                    if (vo.dangerLevel == "1")
                        vo.dangerLevel = "低危";
                    if (vo.dangerLevel == "2")
                        vo.dangerLevel = "中危";
                    if (vo.dangerLevel == "3")
                        vo.dangerLevel = "高危";
                    vo.manageLevel = dr["manageLevel"].ToString();
                    if (vo.manageLevel == "1")
                        vo.manageLevel = "一级管理";
                    if (vo.manageLevel == "2")
                        vo.manageLevel = "二级管理";
                    if (vo.manageLevel == "3")
                        vo.manageLevel = "三级管理";
                    vo.evaluator = dr["pgRecorder"].ToString();
                    vo.pgData = dr["xmlData"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 评估记录-保存
        /// <summary>
        /// 评估记录-保存
        /// </summary>
        /// <param name="pgData"></param>
        /// <param name="pgId"></param>
        /// <returns></returns>
        public int SaveTnbPgRecord(EntityTnbRecord tnbRecord, EntityTnbPg tnbPg, EntityTnbPgData pgData, out decimal pgId)
        {
            int affectRows = 0;
            pgId = 0;
            string Sql = string.Empty;
            SqlHelper svc = null;
            decimal id = 0;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
                svc = new SqlHelper(EnumBiz.onlineDB);
                if (tnbPg.pgId <= 0)
                {
                    id = svc.GetNextID("tnbPg", "pgId");
                    tnbPg.status = 1;
                    tnbPg.pgId = id;
                    tnbPg.recordDate = DateTime.Now;
                    tnbRecord.pgTimes += 1;
                    lstParm.Add(svc.GetInsertParm(tnbPg));

                    //糖尿病下次随访数据
                    string sql = @"update tnbRecord set pgTimes = ? where recId = ?";
                    IDataParameter[] param = svc.CreateParm(2);
                    param[0].Value = tnbRecord.pgTimes;
                    param[1].Value = tnbRecord.recId;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param));
                }
                else
                {
                    lstParm.Add(svc.GetUpdateParm(tnbPg,
                      new List<string> { 
                          EntityTnbPg.Columns.dangerLevel,
                          EntityTnbPg.Columns.manageLevel,
                          EntityTnbPg.Columns.evaDate,},
                      new List<string> { EntityTnbPg.Columns.pgId }));
                }
                pgData.pgId = id;
                //评估数据
                lstParm.Add(svc.GetDelParm(pgData, EntityTnbPgData.Columns.pgId));
                lstParm.Add(svc.GetInsertParm(pgData));
                if (lstParm.Count > 0)
                    affectRows = svc.Commit(lstParm);
                pgId = id;
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

        #region 体检结果 空腹血糖
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientNo"></param>
        /// <returns></returns>
        public List<EntityClientTnbResult> GetClientTnbResults(string clientNoStr)
        {
            if (string.IsNullOrEmpty(clientNoStr))
                return null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            List<EntityClientTnbResult> data = null;

            string sql = @"select a.clientNo,a.regTimes,b.reg_no,b.reg_time,b.comb_code,b.comb_name,b.result ,b.unit
                            from  V_RportRecord a 
                            left join V_TJBG b 
                            on a.reportId = b.reg_no
                            where b.comb_code in('60152 ','060075')
                            and a.clientNo in {0}
                            order by b.reg_time desc ";
            sql = string.Format(sql, clientNoStr);
            DataTable dt = svc.GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityClientTnbResult>();
                EntityClientTnbResult vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    string clientNo = dr["clientNo"].ToString();
                    string regNo = dr["reg_no"].ToString();
                    string comb_code = dr["comb_code"].ToString();
                    string result = dr["result"].ToString() + " " + dr["unit"].ToString(); ;
                    if (data.Any(r => r.regNo == regNo))
                    {
                        EntityClientTnbResult voClone = data.Find(r => r.regNo == regNo);
                        if (result.Contains("血糖"))
                            voClone.tnbYc = result;
                        if (comb_code == "060075")
                            voClone.tnb = result;
                    }
                    else
                    {
                        vo = new EntityClientTnbResult();
                        vo.clientNo = clientNo;
                        vo.regNo = regNo;
                        vo.regTimes = Function.Int(dr["regTimes"]);
                        if (result.Contains("血糖"))
                            vo.tnbYc = result;
                        if (comb_code == "060075")
                            vo.tnb = result;

                        data.Add(vo);
                    }
                }
            }
            return data;
        }

        #endregion

        #endregion

        #region 患者信息赋值
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="obj"></param>
        /// <param name="dr"></param>
        public void SetClientInfo<E>(ref E obj, DataRow dr)
        {
            string sex = string.Empty;
            string age = string.Empty;
            string company = string.Empty;
            string clientName = string.Empty;
            string clientNo = string.Empty;
            string gradeName = string.Empty;
            if (dr.Table.Columns.Contains("gender"))
            {
                if (dr["gender"].ToString() == "1")
                    sex = "男";
                else if (dr["gender"].ToString() == "2")
                    sex = "女";
                else
                    return;
                PropertyInfo pi = obj.GetType().GetProperty("sex");
                if (pi != null)
                {
                    pi.SetValue(obj, sex, null);
                }
            }

            if (dr.Table.Columns.Contains("birthday"))
            {
                if (dr["birthday"] != null)
                    age = Function.CalcAge(Function.Datetime(dr["birthday"]));
                PropertyInfo pi = obj.GetType().GetProperty("age");
                if (pi != null)
                {
                    pi.SetValue(obj, age, null);
                }
            }

            if (dr.Table.Columns.Contains("company"))
            {
                company = dr["company"].ToString();
                PropertyInfo pi = obj.GetType().GetProperty("company");
                if (pi != null)
                {
                    pi.SetValue(obj, company, null);
                }
            }

            if (dr.Table.Columns.Contains("clientName"))
            {
                clientName = dr["clientName"].ToString();
                PropertyInfo pi = obj.GetType().GetProperty("clientName");
                if (pi != null)
                {
                    pi.SetValue(obj, clientName, null);
                }
            }

            if (dr.Table.Columns.Contains("clientNo"))
            {
                clientNo = dr["clientNo"].ToString();
                PropertyInfo pi = obj.GetType().GetProperty("clientNo");
                if (pi != null)
                {
                    pi.SetValue(obj, clientNo, null);
                }
            }

            if (dr.Table.Columns.Contains("gradeName"))
            {
                gradeName = dr["gradeName"].ToString();
                PropertyInfo pi = obj.GetType().GetProperty("gradeName");
                if (pi != null)
                {
                    pi.SetValue(obj, gradeName, null);
                }
            }
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
