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
    public class Biz203 : IDisposable
    {
        #region 个人报告

        #region 人员列表
        /// <summary>
        /// 人员列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntitymModelAccessRecord> GetModelAccessRec(List<EntityParm> parms)
        {
            List<EntitymModelAccessRecord> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select a.recId,a.regNo,
                           b.clientNo,
                           b.clientName,
                           a.regTimes,
                           b.gender,
                           b.birthday,
                           b.gradeName,
                           b.company
                      from modelAccessRecord a
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
                data = new List<EntitymModelAccessRecord>();
                EntitymModelAccessRecord vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntitymModelAccessRecord();
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
        public int SaveMdAccessRecord(EntitymModelAccessRecord mdAccessRec, out decimal recId)
        {
            int affectRows = 0;
            recId = 0;
            string Sql = string.Empty;
            SqlHelper svc = null;
            try
            {
                if (mdAccessRec == null)
                    return -1;
                decimal id = 0;
                List<DacParm> lstParm = new List<DacParm>();
                svc = new SqlHelper(EnumBiz.onlineDB);

                if (mdAccessRec.recId <= 0)
                {
                    string sql = @"insert into modelAccessRecord(recid,clientno,regtimes,regno,recorder,recorddate,status) values (?,?,?,?,?,?,?)";
                    id = svc.GetNextID("modelAccessRecord", "recId");
                    mdAccessRec.recordDate = DateTime.Now;
                    mdAccessRec.recorder = "00";
                    IDataParameter[] param = svc.CreateParm(7);
                    param[0].Value = id;
                    param[1].Value = mdAccessRec.clientNo;
                    param[2].Value = mdAccessRec.regTimes;
                    param[3].Value = mdAccessRec.regNo;
                    param[4].Value = mdAccessRec.recorder;
                    param[5].Value = mdAccessRec.recordDate;
                    param[6].Value = mdAccessRec.status;
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

        #region 列表
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntitymModelAccessRecord> GetClientMdAccessRecord(List<EntityParm> parms)
        {
            List<EntitymModelAccessRecord> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = string.Empty;
            string sql1 = string.Empty;
            string sql2 = string.Empty;
            sql1 = @"SELECT
	                    a.recId,
	                    a.regNo,
                        a.confirmDate,
	                    b.clientNo,
	                    b.clientName,
	                    a.regTimes,
	                    b.gender,
	                    b.birthday,
	                    b.gradeName,
	                    b.company,
                        a.recordDate,
						t.reportDate,
	                    c.recId as qnRecid,
	                    c.qnName,
	                    c.qnType,
	                    c.qnDate,
	                    c.qnSource,
	                    c.qnId,
	                    d.xmlData,
                        a.status
                    FROM modelAccessRecord a
                    LEFT JOIN V_ClientInfo b 
                    ON a.clientNo = b.clientNo
                    AND a.regTimes = b.regTimes
                    left join V_TJXX t
                    on a.regNo = t.reportNo
                    LEFT JOIN qnRecord c 
                    ON a.qnRecId = c.recId
                    LEFT JOIN qnData d 
                    ON a.qnRecId = d.recId
                    WHERE a.status >= 0  ";
            string strSub = string.Empty;
            string strSub1 = string.Empty;
            string strSub2 = string.Empty;
            List<IDataParameter> lstParm = new List<IDataParameter>();
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
                            strSub += " and  a.recordDate between ? and ? ";

                            break;
                        case "clientNo":
                            strSub += " and  a.clientNo = '" + po.value + "'";
                            break;
                        default:
                            break;
                    }
                }
            }
            sql1 += strSub;
            DataTable dt = svc.GetDataTable(sql1, lstParm.ToArray());
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntitymModelAccessRecord>();
                EntitymModelAccessRecord vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntitymModelAccessRecord();
                    vo.recId = Function.Dec(dr["recId"]);
                    vo.regNo = dr["regNo"].ToString();
                    vo.recordDate = Function.Datetime(dr["recordDate"]);
                    Function.SetClientInfo(ref vo,dr);   
                    vo.reportDateStr = Function.Datetime(dr["reportDate"]).ToString("yyyy-MM-dd");
                    vo.regTimes = Function.Int(dr["regTimes"]);
                    vo.status = Function.Int(dr["status"]);

                    if(dr["confirmDate"] != DBNull.Value)
                    {
                        vo.confirmDateStr = Function.Datetime(dr["confirmDate"]).ToString("yyyy-MM-dd") ;
                    }
                    if (!string.IsNullOrEmpty(dr["qnRecid"].ToString()))
                    {
                        vo.strQnDate = Function.Datetime(dr["qnDate"]).ToString("yyyy-MM-dd");
                        vo.qnRecId = Function.Dec(dr["qnRecid"]);
                        vo.qnName = dr["qnName"].ToString();
                        vo.qnId = Function.Dec(dr["qnId"]);
                        vo.strQnSource = "采集系统";
                        vo.qnData = dr["xmlData"].ToString();
                    }

                    data.Add(vo);
                }
            }

            return data;
        }
        #endregion

        #region 疾病模型结果及各项得分
        /// <summary>
        /// 疾病模型结果及各项得分
        /// </summary>
        /// <param name="lstMdResult"></param>
        /// <returns></returns>

        public int SaveModelResultAndParamCalc(EntitymModelAccessRecord mdAccessRecord, List<EntityClientModelResult> lstMdResult, List<EntityModelParamCalc> lstMdParamCalc,List<EntityRiskFactorsResult> lstRiskFactorsResult)
        {
            SqlHelper svc = null;
            int affect = -1;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
                string sql = string.Empty;
                if (mdAccessRecord != null)
                {
                    lstParm.Add(svc.GetDelParmByPk(mdAccessRecord));
                    lstParm.Add(svc.GetInsertParm(mdAccessRecord));
                }

                if (lstMdResult != null)
                {
                    foreach (var mdVo in lstMdResult)
                    {
                        lstParm.Add(svc.GetDelParmByPk(mdVo));
                    }

                    foreach (var mdVo in lstMdResult)
                    {
                        lstParm.Add(svc.GetInsertParm(mdVo));
                    }

                }

                if (lstMdParamCalc != null)
                {
                    foreach (var mdVo in lstMdParamCalc)
                        lstParm.Add(svc.GetDelParmByPk(mdVo));

                    foreach (var mdVo in lstMdParamCalc)
                        lstParm.Add(svc.GetInsertParm(mdVo));
                }

                if(lstRiskFactorsResult != null)
                {
                    foreach (var rrVo in lstRiskFactorsResult)
                        lstParm.Add(svc.GetDelParmByPk(rrVo));

                    foreach (var rrVo in lstRiskFactorsResult)
                        lstParm.Add(svc.GetInsertParm(rrVo));
                }

                if (lstParm.Count > 0)
                    affect = svc.Commit(lstParm);
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
                affect = -1;
            }
            finally
            {
                svc = null;
            }

            return affect;
        }
        #endregion

        #region 取消审核
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mdAccessRecord"></param>
        /// <returns></returns>
        public int UnConfirmRpt(EntitymModelAccessRecord mdAccessRecord)
        {
            int affect = -1;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = "update modelAccessRecord set status = 0 where  qnRecId = ?";
            IDataParameter[] param = svc.CreateParm(1);
            param[0].Value = mdAccessRecord.qnRecId;
            affect = svc.ExecSql(sql, param);
            return affect;
        }

        #endregion

        #region dic

        /// <summary>
        /// 重要指标字典
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityReportMainItemConfig> GetReportMainItemConfig()
        {
            List<EntityReportMainItemConfig> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select itemCode,
                           itemName,
                           sort
                       from reportMainItemConfig  order by sort ";
            string strClientNo = string.Empty;

            DataTable dt = svc.GetDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityReportMainItemConfig>();
                EntityReportMainItemConfig vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityReportMainItemConfig();
                    vo.itemCode = dr["itemCode"].ToString();
                    vo.itemName = dr["itemName"].ToString();
                    data.Add(vo);
                }
            }

            return data;
        }

        /// <summary>
        /// modelParam 模型参数
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityModelParam> GetModelParam()
        {
            List<EntityModelParam> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select * from modelParam  order by parentFieldId asc ";
            string strClientNo = string.Empty;

            DataTable dt = svc.GetDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityModelParam>();
                EntityModelParam vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityModelParam();
                    vo.id = Function.Int(dr["id"]);
                    vo.modelId = Function.Dec(dr["modelId"]);
                    vo.judgeType = Function.Dec(dr["judgeType"]);
                    vo.paramType = Function.Dec(dr["paramType"]);
                    vo.gender = Function.Dec(dr["gender"]);
                    vo.isChange = Function.Dec(dr["isChange"]);
                    vo.paramNo = dr["paramNo"].ToString();
                    vo.paramName = dr["paramName"].ToString();
                    vo.judgeValue = Function.Dec(dr["judgeValue"]);
                    vo.judgeRange = dr["judgeRange"].ToString();
                    vo.score = Function.Dec(dr["score"]);
                    vo.modulus = Function.Dec(dr["modulus"]);
                    vo.remarks = dr["remarks"].ToString();
                    vo.parentFieldId = dr["parentFieldId"].ToString();
                    vo.isBest = dr["isBest"].ToString();
                    vo.isNormal = dr["isNormal"].ToString();
                    data.Add(vo);
                }
            }

            return data;
        }

        /// <summary>
        /// 疾病模型分析要点
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityModelAnalysisPoint> GetModelAnalysisPoint()
        {
            List<EntityModelAnalysisPoint> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select  id,
                        paramType,
                        paramNo,
                        paramName,
                        judgeWay,
                        judgeValue,
                        pintAdvice,
                        remarks,
                        bakField1,
                        bakField2
                        from modelAnalysisPoint  ";

            DataTable dt = svc.GetDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityModelAnalysisPoint>();
                EntityModelAnalysisPoint vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityModelAnalysisPoint();
                    vo.id = Function.Int(dr["id"]);
                    vo.paramType = Function.Dec(dr["paramType"]);
                    vo.paramNo = dr["paramType"].ToString();
                    vo.paramName = dr["paramNo"].ToString();
                    vo.judgeWay = dr["judgeWay"].ToString();
                    vo.judgeValue = dr["judgeValue"].ToString();
                    vo.pintAdvice = dr["pintAdvice"].ToString();
                    vo.remarks = dr["remarks"].ToString();
                    vo.bakField1 = dr["bakField1"].ToString();
                    vo.bakField2 = dr["bakField2"].ToString();
                    data.Add(vo);
                }
            }

            return data;
        }

        /// <summary>
        /// 疾病模型
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityModelAccess> GetModelAccess()
        {
            List<EntityModelAccess> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = @"select modelId,
                                  modelName ,
                                  modelIntro ,
                                  modelExplan ,
                                  modelAdvice ,
                                  lowDanger ,
                                  minAge ,
                                  maxAge ,
                                  modelSex ,
                                  isNeedQuestion from modelAccess ";
            DataTable dt = svc.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityModelAccess>();
                EntityModelAccess vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityModelAccess();
                    vo.modelId = Function.Int(dr["modelId"]);
                    vo.modelName = dr["modelName"].ToString();
                    vo.modelIntro = dr["modelIntro"].ToString();
                    vo.modelExplan = dr["modelExplan"].ToString();
                    vo.modelAdvice = dr["modelAdvice"].ToString();
                    vo.lowDanger = Function.Dec(dr["lowDanger"]);
                    vo.minAge = Function.Dec(dr["minAge"]);
                    vo.maxAge = Function.Dec(dr["maxAge"]);
                    vo.modelSex = Function.Dec(dr["modelSex"]);
                    data.Add(vo);
                }
            }

            return data;
        }

        #region 获取疾病模型参数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public List<EntityModelGroupItem> GetModelGroup()
        {
            List<EntityModelGroupItem> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = @"select id,
                            modelId,
                            paramType,
                            paramNo,
                            paramName,
                            orderNum,
                            isMain,isFamily,
                            pointId from modelGroupItem ";
            DataTable dt = svc.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityModelGroupItem>();
                EntityModelGroupItem vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityModelGroupItem();
                    vo.id = Function.Int(dr["id"]);
                    vo.modelId = Function.Dec(dr["modelId"]);
                    vo.paramType = Function.Dec(dr["paramType"]);
                    vo.paramNo = dr["paramNo"].ToString();
                    vo.paramName = dr["paramName"].ToString();
                    vo.orderNum = Function.Int(dr["orderNum"]);
                    vo.isMain = Function.Int(dr["isMain"]);
                    vo.isFamily = Function.Int(dr["isFamily"]);
                    vo.pointId = Function.Int(dr["pointId"]);
                    data.Add(vo);
                }
            }

            return data;
        }
        #endregion

        #region 评估模型平均风险配置表
        /// <summary>
        /// 评估模型平均风险配置表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityModelAvgRisk> GetModelAvgRisk()
        {
            List<EntityModelAvgRisk> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = @" select id,
                            modelId,
                            minAge,
                            maxAge,
                            defaultRisk,
                            configRiskMan,
                            configRiskWoman,
                            isUse,
                            recordDate from modelAvgRisk where isUse = 1";
            DataTable dt = svc.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityModelAvgRisk>();
                EntityModelAvgRisk vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityModelAvgRisk();
                    vo.id = Function.Int(dr["id"]);
                    vo.modelId = Function.Dec(dr["modelId"]);
                    vo.minAge = Function.Dec(dr["minAge"]);
                    vo.maxAge = Function.Dec(dr["maxAge"]);
                    vo.defaultRisk = Function.Dec(dr["defaultRisk"]);
                    vo.configRiskMan = Function.Dec(dr["configRiskMan"]);
                    vo.configRiskWoman = Function.Dec(dr["configRiskWoman"]);
                    vo.isUse = Function.Int(dr["isUse"]) ;
                    vo.recordDate = Function.Datetime(dr["recordDate"]);
                    data.Add(vo);
                }
            }

            return data;
        }
        #endregion

        #region 危险因素
        /// <summary>
        /// 危险因素
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityRiskFactor> GetRiskFactor()
        {
            List<EntityRiskFactor> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = @" select id,
                            showSort,
                            riskFactor,
                            inCondition,
                            advice,jugeValue,
                            remarks from riskFactor ";
            DataTable dt = svc.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityRiskFactor>();
                EntityRiskFactor vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityRiskFactor();
                    vo.id = dr["id"].ToString() ;
                    vo.showSort = dr["showSort"].ToString();
                    vo.riskFactor = dr["riskFactor"].ToString();
                    vo.inCondition = dr["inCondition"].ToString();
                    vo.advice = dr["advice"].ToString();
                    vo.remarks = dr["remarks"].ToString();
                    vo.jugeValue = dr["jugeValue"].ToString();

                    data.Add(vo);
                }
            }

            return data;
        }
        #endregion

        #region 问卷家族疾病史
        /// <summary>
        /// 问卷家族疾病史
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityQnFamilyDease> GetQnFamilyDease()
        {
            List<EntityQnFamilyDease> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = @" select fieldId,
                            fieldName,
                            parentFieldId
                            from qnFamilyDease ";
            DataTable dt = svc.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityQnFamilyDease>();
                EntityQnFamilyDease vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityQnFamilyDease();
                    vo.fieldId = dr["fieldId"].ToString();
                    vo.fieldName = dr["fieldName"].ToString();
                    vo.parentFieldId = dr["parentFieldId"].ToString();

                    data.Add(vo);
                }
            }

            return data;
        }
        #endregion

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
