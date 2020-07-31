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
    public class Biz204 : IDisposable
    {
        #region 模板
        /// <summary>
        /// 模板
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityPromotionTemplate> GetPromotionTemplates(List<EntityParm> parms = null)
        {
            List<EntityPromotionTemplate> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select  id,
                            templateName,
                            templateCondition,
                            isEnabled,
                            bakField1,
                            bakField2,
                            createDate,
                            cretateId,
                            creator,
                            modifyDate,
                            modifyId,
                            modifyName
                      from promotionTemplate  ";
            DataTable dt = svc.GetDataTable(Sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityPromotionTemplate>();
                EntityPromotionTemplate vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityPromotionTemplate();
                    vo.id = dr["id"].ToString();
                    vo.templateName = dr["templateName"].ToString();
                    vo.templateCondition = dr["templateCondition"].ToString();
                    vo.isEnabled = dr["isEnabled"].ToString();
                    vo.bakField1 = dr["bakField1"].ToString();
                    vo.bakField2 = dr["bakField2"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 模板配置
        /// <summary>
        /// 模板配置
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityPromotionTemplateConfig> GetPromotionTemplateConfigs(List<EntityParm> parms = null)
        {
            List<EntityPromotionTemplateConfig> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select  a.id,
                            templateId,
                            planMonth,
                            planDay,
                            b.planWay,
                            c.planContent,
                            planRemind,
                            isEnabled,
                            bakField1,
                            bakField2,
                            createDate,
                            createId,
                            creator,
                            modifyDate,
                            modifyId,
                            modifyName
                      from promotionTemplateConfig a
                            left join promotionWayConfig b
                            on a.planWay = b.id
                            left join promotionContentConfig c
                            on a.planContent = c.id  order by planMonth,planDay";
            DataTable dt = svc.GetDataTable(Sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityPromotionTemplateConfig>();
                EntityPromotionTemplateConfig vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityPromotionTemplateConfig();
                    vo.id = dr["id"].ToString();
                    vo.templateId = dr["templateId"].ToString();
                    vo.planMonth = dr["planMonth"].ToString();
                    vo.planPeriod = (DateTime.Now.AddMonths(Function.Int(dr["planMonth"])-1).AddDays(Function.Int(dr["planDay"]))).ToString("yyyy-MM-dd"); ;
                    vo.planDay = dr["planDay"].ToString();
                    vo.planWay = dr["planWay"].ToString();
                    vo.planContent = dr["planContent"].ToString();
                    vo.planRemind = dr["planRemind"].ToString();
                    vo.isEnabled = dr["isEnabled"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region  干预形式
        /// <summary>
        /// 干预形式
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityPromotionWayConfig> GetPromotionWayConfigs()
        {
            List<EntityPromotionWayConfig> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select  id,
                            planWay
                      from promotionWayConfig  ";
            DataTable dt = svc.GetDataTable(Sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityPromotionWayConfig>();
                EntityPromotionWayConfig vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityPromotionWayConfig();
                    vo.id = dr["id"].ToString();
                    vo.planWay = dr["planWay"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region  主要内容
        /// <summary>
        /// 主要内容
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityPromotionContentConfig> GetPromotionContentConfigs()
        {
            List<EntityPromotionContentConfig> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select  id,
                            planContent
                      from promotionContentConfig  ";
            DataTable dt = svc.GetDataTable(Sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityPromotionContentConfig>();
                EntityPromotionContentConfig vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityPromotionContentConfig();
                    vo.id = dr["id"].ToString();
                    vo.planContent = dr["planContent"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region  待执行计划
        /// <summary>
        /// 待执行计划
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityDisplayPromotionPlan> GetPromotionPlans(List<EntityParm> dicParm = null)
        {
            List<EntityDisplayPromotionPlan> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string strSub = string.Empty;
            string Sql = string.Empty;

            Sql = @"select a.clientId, b.clientName,
                            b.clientNo,
                            b.gender,
                            b.birthday,
                            b.company,
                            b.mobile,
                            b.gradename,
                            d.planWay,
                            e.planContent,
                            a.planRemind,
                            a.planDate,
                            a.auditState,
                            a.createDate,
                            a.createName,
                            a.executeTime,
                            a.createName
                            from promotionPlan a
                            left join v_clientinfo b
                            on a.clientId = b.clientNo and a.regTimes = b.regTimes
                            left join promotionWayConfig d
                            on a.planWay = d.id
                            left join promotionContentConfig e
                            on a.planContent = e.id 
							where  a.planState = 2 
							and b.clientNo is not null  ";

            List<IDataParameter> lstParm = new List<IDataParameter>();

            if (dicParm != null)
            {
                foreach (EntityParm po in dicParm)
                {
                    switch (po.key)
                    {
                        case "clientNo":
                            IDataParameter parm = svc.CreateParm();
                            parm.Value = po.value;
                            lstParm.Add(parm);
                            strSub += " and b.clientNo = ?";
                            break;
                        default:
                            break;
                    }
                }
            }

            Sql += strSub;
            Sql += " order by b.clientNo,a.planDate";

            DataTable dt = svc.GetDataTable(Sql,lstParm);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityDisplayPromotionPlan>();
                EntityDisplayPromotionPlan vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityDisplayPromotionPlan();
                    vo.clientId = dr["clientId"].ToString();
                    vo.clientName = dr["clientName"].ToString();
                    vo.clientNo = dr["clientNo"].ToString();
                    vo.gender = Function.Int(dr["gender"]);
                    vo.age = Function.CalcAge(Function.Datetime(dr["birthday"]));
                    vo.company = dr["company"].ToString();
                    vo.mobile = dr["mobile"].ToString();
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.planWay = dr["planWay"].ToString();
                    vo.planContent = dr["planContent"].ToString();
                    vo.planRemind = dr["planRemind"].ToString();
                    vo.planDate = Function.Datetime(dr["planDate"]).ToString("yyyy-MM-dd");
                    vo.auditState = dr["auditState"].ToString();
                    if(dr["executeTime"] != DBNull.Value)
                        vo.executeTime = Function.Datetime(dr["executeTime"]).ToString("yyyy-MM-dd");
                    vo.createName = dr["createName"].ToString();
                    vo.createDate = Function.Datetime(dr["createDate"]).ToString("yyyy-MM-dd");
                    if (vo.gender == 1)
                        vo.sex = "男";
                    if (vo.gender == 2)
                        vo.sex = "女";
                    if (vo.auditState == "1")
                        vo.strAuditState = "无需审核";
                    if (vo.auditState == "2")
                        vo.strAuditState = "审核通过";
                    if (vo.auditState == "3")
                        vo.strAuditState = "等待审核";
                    if (vo.auditState == "4")
                        vo.strAuditState = "已分配审核";
                    if (vo.auditState == "5")
                        vo.strAuditState = "审核不通过";
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region  干预记录
        /// <summary>
        /// 干预记录
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityDisplayPromotionPlan> GetPromotionPlanRecords(List<EntityParm> dicParm = null)
        {
            List<EntityDisplayPromotionPlan> data = null;
            string strSub = string.Empty;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select  a.clientId,
                            b.clientName,
                            b.clientNo,
                            b.gender,
                            b.birthday,
                            b.company,
                            b.mobile,
                            c.gradeName,
                            d.planWay,
                            e.planContent,
                            a.planRemind,
                            a.planDate,
                            a.planVisitRecord,
                            a.executeTime,
                            a.executeUserName,
                            a.createName
                            from promotionPlan a
                            left join clientInfo b
                            on a.clientId = b.id
                            left join userGrade c
                            on b.gradeId = c.id
                            left join promotionWayConfig d
                            on a.planWay = d.id
                            left join promotionContentConfig e
                            on a.planContent = e.id
                            where a.planState = 1
                            and a.clientId is not null ";

            List<IDataParameter> lstParm = new List<IDataParameter>();

            if (dicParm != null)
            {
                foreach (EntityParm po in dicParm)
                {
                    switch (po.key)
                    {
                        case "clientNo":
                            IDataParameter parm = svc.CreateParm();
                            parm.Value = po.value;
                            lstParm.Add(parm);
                            strSub += " and b.clientNo = ?";
                            break;
                        default:
                            break;
                    }
                }
            }

            Sql += strSub;
            Sql += " order by b.clientNo,a.planDate";
            DataTable dt = svc.GetDataTable(Sql,lstParm);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityDisplayPromotionPlan>();
                EntityDisplayPromotionPlan vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityDisplayPromotionPlan();
                    vo.clientId = dr["clientId"].ToString();
                    vo.clientName = dr["clientName"].ToString();
                    vo.clientNo = dr["clientNo"].ToString();
                    vo.gender = Function.Int(dr["gender"]);
                    vo.age = Function.CalcAge(Function.Datetime(dr["birthday"]));
                    vo.company = dr["company"].ToString();
                    vo.mobile = dr["mobile"].ToString();
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.planWay = dr["planWay"].ToString();
                    vo.planContent = dr["planContent"].ToString();
                    vo.planRemind = dr["planRemind"].ToString();
                    vo.planVisitRecord = dr["planVisitRecord"].ToString();
                    vo.executeUserName = dr["executeUserName"].ToString();
                    vo.planDate = Function.Datetime(dr["planDate"]).ToString("yyyy-MM-dd");
                    if (dr["executeTime"] != DBNull.Value)
                        vo.executeTime = Function.Datetime(dr["executeTime"]).ToString("yyyy-MM-dd");
                    vo.executeUserName = dr["executeUserName"].ToString();
                    vo.createName = dr["createName"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region  保存干预计划
        /// <summary>
        /// 
        /// </summary>
        /// <param name="promotionPlans"></param>
        /// <returns></returns>
        public int SavePromotionPan(List<EntityPromotionPlan> promotionPlans)
        {
            int affect = -1;
            SqlHelper svc = null;

            try
            {
                if (promotionPlans != null)
                {
                    svc = new SqlHelper(EnumBiz.onlineDB);
                    List<DacParm> lstParm = new List<DacParm>();
                    foreach (var plans in promotionPlans)
                    {
                        if (string.IsNullOrEmpty(plans.id))
                        {
                            plans.id = DateTime.Now.ToString("yyyyMMddHHss") + svc.GetNextID("promotionPlan", "id").ToString().PadLeft(4, '0');
                            plans.createDate = DateTime.Now;
                            plans.createId = "00";
                            lstParm.Add(svc.GetInsertParm(plans));
                        }
                        else
                        {
                            plans.modifyDate = DateTime.Now;
                            lstParm.Add(svc.GetUpdateParm(plans, new List<string>() {
                        EntityPromotionPlan.Columns.clientId,
                        EntityPromotionPlan.Columns.planType,
                        EntityPromotionPlan.Columns.planDate,
                        EntityPromotionPlan.Columns.planState,
                        EntityPromotionPlan.Columns.planWay,
                        EntityPromotionPlan.Columns.planContent,
                        EntityPromotionPlan.Columns.planRemind,
                        EntityPromotionPlan.Columns.recordWay,
                        EntityPromotionPlan.Columns.recordContent,
                        EntityPromotionPlan.Columns.ignorPlan,
                        EntityPromotionPlan.Columns.planSource,
                        EntityPromotionPlan.Columns.planTemplateName,
                        EntityPromotionPlan.Columns.modifyId,
                        EntityPromotionPlan.Columns.modifyDate },
                            new List<string>() { EntityPromotionPlan.Columns.id }));
                        }
                    }

                    if (lstParm.Count > 0)
                        affect = svc.Commit(lstParm);
                }

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

        #region 健康管理报告评估分数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityDisplayClientModelAcess> GetDisplayClientModelAcess(List<EntityParm> parms)
        {
            List<EntityDisplayClientModelAcess> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string strSub = string.Empty;
            string Sql = string.Empty;
            Sql = @"select 
                                    a.reportId,
                                    a.modelId,
                                    a.modelResult,
                                    a.modelScore,
                                    b.modelName 
                                    from clientModelResult a  
                                    left join modelAccess b
                                    on a.modelId = b.modelId
                                    where a.reportId is not null  ";

            List<IDataParameter> lstParm = new List<IDataParameter>();

            if (parms != null)
            {
                foreach (EntityParm po in parms)
                {
                    switch (po.key)
                    {
                        case "clientId":
                            IDataParameter parm = svc.CreateParm();
                            parm.Value = po.value;
                            lstParm.Add(parm);
                            strSub += " and a.clientId = ?";
                            break;
                        default:
                            break;
                    }
                }
            }

            Sql += strSub;
            Sql += " order by b.orderId";
            DataTable dt = svc.GetDataTable(Sql, lstParm);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityDisplayClientModelAcess>();
                EntityDisplayClientModelAcess vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityDisplayClientModelAcess();
                    vo.modelName = dr["modelName"].ToString();
                    vo.modelScore = dr["modelScore"].ToString();
                    if (Function.Dec(dr["modelScore"]) == 0)
                        vo.modelScore = "-";
                    vo.modelResult = dr["modelResult"].ToString();
                    vo.modelScoreAndResult = vo.modelScore + "|" + vo.modelResult;

                    if (data.Any(r=>r.modelName == vo.modelName))
                    {
                        EntityDisplayClientModelAcess voClone = data.Find(r => r.modelName == vo.modelName);
                        string[] arrStr = voClone.modelScoreAndResult.Split('|');
                        string strScore = arrStr[0];
                        strScore += "," + vo.modelScore;
                        string strTmp = arrStr[1];
                        strTmp += "," + vo.modelResult;
                        voClone.modelScoreAndResult = strScore + "|" + strTmp;

                    }
                    else 
                        data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 体检报告重要指标
        /// <summary>
        /// 获取重要指标
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public List<EntityReportMainItem> GetReportMainItem(string reportId)
        {
            List<EntityReportMainItem> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            if (string.IsNullOrEmpty(reportId))
                return null;
            string Sql = @"select a.pat_name AS clientName,
	                                a.reg_no AS reportId,
                                    a.dept_name as sectionName,
                                    a.hint as isNormal,
	                                a.comb_code AS itemCode,
	                                a.comb_name AS itemName,
	                                a.result AS itemValue,
	                                a.bound AS itemRefrange,
	                                a.unit as itemUnits,
	                                a.doct_name AS createName,
	                                a.rec_date AS createDate
                                FROM V_TJBG a
                                WHERE a.reg_no = ? and a.comb_code in (select itemCode from reportMainItemConfig)";
            if (string.IsNullOrEmpty(reportId))
                return null;
            IDataParameter parm = svc.CreateParm();
            parm.Value = reportId;
            DataTable dt = svc.GetDataTable(Sql,parm);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityReportMainItem>();
                EntityReportMainItem vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityReportMainItem();
                    //vo.id = dr["id"].ToString();
                    //vo.clientId = dr["clientId"].ToString();
                    vo.reportId = dr["reportId"].ToString();
                    vo.sectionName = dr["sectionName"].ToString();
                    vo.itemName = dr["itemName"].ToString();
                    vo.itemValue = dr["itemValue"].ToString();
                    vo.itemUnits = dr["itemUnits"].ToString();
                    vo.itemRefrange = dr["itemRefrange"].ToString();
                    vo.isNormal = dr["isNormal"].ToString();
                    //vo.minValue = dr["minValue"].ToString();
                    //vo.maxValue = dr["maxValue"].ToString();
                    //vo.orderId = Function.Int(dr["orderId"]) ;
                    //vo.bakField1 = dr["bakField1"].ToString();
                    //vo.bakField2 = dr["bakField2"].ToString();
                    vo.createDate = Function.Datetime(dr["createDate"]);
                    //vo.createId = dr["createId"].ToString();
                    vo.createName = dr["createName"].ToString();
                    //vo.modifyDate = Function.Datetime(dr["modifyDate"]) ;
                    //vo.modifyId = dr["modifyId"].ToString();
                    //vo.modifyName = dr["modifyName"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }

        #endregion

        #region 体检项目列表
        /// <summary>
        /// 体检项目列表
        /// </summary>
        /// <returns></returns>
        public List<EntityReportItem> GetReportItems(string reportId)
        {
            List<EntityReportItem> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select a.reportId,
                           a.sectionName,
                           a.itemName,
                           a.itemValue,
                           a.maxValue,
                           a.minValue,
                           a.itemUnit,
                           a.refRange
                      from reportItem a where a.reportId = ? order by a.sectionName";

            if (string.IsNullOrEmpty(reportId))
                return null;

            IDataParameter parm = svc.CreateParm();
            parm.Value = reportId;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityReportItem>();
                EntityReportItem vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityReportItem();
                    vo.reportId = dr["reportId"].ToString();
                    vo.sectionName = dr["sectionName"].ToString();
                    vo.itemName = dr["itemName"].ToString();
                    vo.itemValue = dr["itemValue"].ToString();
                    vo.maxValue = Function.Dec(dr["maxValue"]);
                    vo.minValue = Function.Dec(dr["minValue"]);
                    vo.itemUnit = dr["itemUnit"].ToString();
                    vo.refRange = dr["refRange"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 获取疾病模型
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EntityModelAccess> GetModelAccesses()
        {
            List<EntityModelAccess> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select a.modelId,
                           a.modelName
                      from modelAccess a ";
            DataTable dt = svc.GetDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityModelAccess>();
                EntityModelAccess vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityModelAccess();
                    vo.modelId = Function.Int(dr["modelId"]);
                    vo.modelName = dr["modelName"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }

        #endregion

        #region 危险要素
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<entitydisplayriskresult> GetRiskFactorsResult(List<EntityParm> parms)
        {
            List<entitydisplayriskresult> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string strSub = string.Empty;
            string Sql = string.Empty;
            Sql = @"select a.clientId,
                            a.questionId,
                            a.factorsId,
                            b.showSort,
                            b.riskFactor,
                            a.isFamilyDisease,
                            a.filedId,
                            a.filedName,
                            a.advise 
                            from riskFactorsResult a 
                            left join riskFactor b
                            on a.factorsId = b.id  where a.clientId <> 'T' ";

            List<IDataParameter> lstParm = new List<IDataParameter>();

            if (parms != null)
            {
                foreach (EntityParm po in parms)
                {
                    switch (po.key)
                    {
                        case "clientId":
                            IDataParameter parm = svc.CreateParm();
                            parm.Value = po.value;
                            lstParm.Add(parm);
                            strSub += " and a.clientId = ?";
                            break;
                        default:
                            break;
                    }
                }
            }

            Sql += strSub;
            DataTable dt = svc.GetDataTable(Sql, lstParm);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<entitydisplayriskresult>();
                entitydisplayriskresult vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new entitydisplayriskresult();

                    vo.clientId = dr["clientId"].ToString();
                    vo.questionId = dr["questionId"].ToString() ;
                    vo.factorsId = dr["factorsId"].ToString();
                    vo.showSort = dr["showSort"].ToString();
                    vo.riskFactor = dr["riskFactor"].ToString();
                    vo.isFamilyDisease = dr["isFamilyDisease"].ToString();
                    vo.filedId = dr["filedId"].ToString();
                    vo.filedName = dr["filedName"].ToString();
                    if (!string.IsNullOrEmpty(vo.filedName))
                        vo.riskFactor = vo.filedName;
                    vo.advise = dr["advise"].ToString();
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
