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

        #region 列表
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityDisplayClientRpt> GetClientReports(List<EntityParm> parms)
        {
            List<EntityDisplayClientRpt> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = string.Empty;
            string sql1 = string.Empty;
            string sql2 = string.Empty;
            sql1 = @"SELECT  a.clientName,
	                        a.clientNo,
	                        a.gender,
	                        a.age,
	                        a.company,
	                        a.gradeName,
	                        a.reportNo,
	                        a.regTimes,
	                        a.idcard,
	                        b.qnRecId,
	                        a.reportDate,
	                        c.recId,
	                        c.qnName,
	                        c.qnType,
	                        c.qnDate,
	                        c.qnSource,
	                        c.qnId,
                            c.qnDate,
                            b.status,
	                        d.xmlData,
                            b.recordDate as confirmDate
                        FROM 
	                        v_tjxx a
                        LEFT JOIN modelAccessRecord b ON a.reportNo = b.reportId and b.status > -1
                        LEFT JOIN qnRecord c ON b.qnRecId = c.recId
                        LEFT JOIN qnData d ON b.qnRecId = d.recId
                        WHERE a.clientNo IS NOT NULL and b.qnRecId = null ";

            sql2 = @"SELECT  a.clientName,
	                                    a.clientNo,
	                                    a.gender,
	                                    a.age,
	                                    a.company,
	                                    a.gradeName,
	                                    a.reportNo,
	                                    a.regTimes,
	                                    a.idcard,
	                                    b.qnRecId,
	                                    a.reportDate,
	                                    c.recId,
	                                    c.qnName,
	                                    c.qnType,
	                                    c.qnDate,
	                                    c.qnSource,
	                                    c.qnId,
                                        c.qnDate,
                                        b.status,
	                                    d.xmlData,
                                        b.recordDate as confirmDate
                                    FROM 
	                                    v_tjxx a
                                    LEFT JOIN modelAccessRecord b ON a.reportNo = b.reportId and b.status > -1
                                    LEFT JOIN qnRecord c ON b.qnRecId = c.recId
                                    LEFT JOIN qnData d ON b.qnRecId = d.recId
                                    WHERE a.clientNo IS NOT NULL  ";
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
                            strSub1 += " and  a.reportDate between ? and ? ";

                            IDataParameter parm3 = svc.CreateParm();
                            parm3.Value = po.value.Split('|')[0];
                            lstParm.Add(parm3);
                            IDataParameter parm4 = svc.CreateParm();
                            parm4.Value = po.value.Split('|')[1];
                            lstParm.Add(parm4);
                            strSub2 += " and  b.recordDate between ? and ? ";
                            break;
                        case "clientNo":
                            strSub += " and  a.clientNo = '" + po.value + "'";
                            break;
                        default:
                            break;
                    }
                }
            }

            sql1 += strSub + strSub1 + Environment.NewLine;
            sql2 += strSub + strSub2;
            
            string strClientNo = string.Empty;
            sql = sql1 + "union all" + Environment.NewLine + sql2;
            DataTable dt = svc.GetDataTable(sql, lstParm.ToArray());
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
                    vo.reportNo = dr["reportNo"].ToString();
                    if (vo.gender == 1)
                        vo.sex = "男";
                    if (vo.gender == 2)
                        vo.sex = "女";
                    vo.reportDate = Function.Datetime(dr["reportDate"]).ToString("yyyy-MM-dd");
                    vo.company = dr["company"].ToString();
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.age = dr["age"].ToString();
                    vo.reportCount = Function.Int(dr["regTimes"]);
                    vo.status = Function.Int(dr["status"]);
                    if(vo.status== 1)
                    {
                        vo.confirmState = "已审核";
                    }
                    if(dr["confirmDate"] != DBNull.Value)
                    {
                        vo.recordDateStr = Function.Datetime(dr["confirmDate"]).ToString("yyyy-MM-dd") ;
                    }
                    if (!string.IsNullOrEmpty(dr["recId"].ToString()))
                    {
                        vo.strQnDate = Function.Datetime(dr["qnDate"]).ToString("yyyy-MM-dd");
                        vo.qnRecord = new EntityQnRecord();
                        vo.qnRecord.recId = Function.Dec(dr["recId"]);
                        vo.qnRecord.gender = Function.Int(dr["gender"]);
                        if (vo.qnRecord.gender == 1)
                            vo.qnRecord.sex = "男";
                        if (vo.qnRecord.gender == 2)
                            vo.qnRecord.sex = "女";
                        vo.qnRecord.clientNo = dr["clientNo"].ToString();
                        vo.qnRecord.clientName = dr["clientName"].ToString();
                        vo.qnRecord.gradeName = dr["gradeName"].ToString();
                        vo.qnRecord.age = dr["age"].ToString();
                        vo.qnRecord.qnName = dr["qnName"].ToString();
                        vo.qnRecord.qnId = Function.Dec(dr["qnId"]);
                        vo.qnRecord.qnSource = Function.Dec(dr["qnSource"]);
                        if (vo.qnRecord.qnSource == 1)
                            vo.qnRecord.strQnSource = "采集系统";
                        vo.qnRecord.qnDate = Function.Datetime(dr["qnDate"]);
                        vo.qnRecord.strQnDate = Function.Datetime(dr["qnDate"]).ToString("yyyy-MM-dd");
                        vo.qnRecord.xmlData = dr["xmlData"].ToString();
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
            string sql = "update modelAccessRecord set status = 0 where reportId = ? and qnRecId = ?";
            IDataParameter[] param = svc.CreateParm(2);
            param[0].Value = mdAccessRecord.reportId;
            param[1].Value = mdAccessRecord.qnRecId;
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
        public List<EntityReportMainItemConfig> GetReportMainItemConfig(List<EntityParm> parms = null)
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
        public List<EntityModelParam> GetModelParam(List<EntityParm> parms = null)
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
        public List<EntityModelAnalysisPoint> GetModelAnalysisPoint(List<EntityParm> parms = null)
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
        public List<EntityModelAccess> GetModelAccess(List<EntityParm> parms = null)
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
        public List<EntityModelGroupItem> GetModelGroup(string modelId = null)
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
