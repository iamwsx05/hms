using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Common.Entity;
using Hms.Biz;
using Hms.Itf;
using Hms.Entity;

namespace Hms.Svc
{
    public class SvcHms : Hms.Itf.ItfHms
    {
        #region 201 客户管理
        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityClientInfo> GetClientInfoAndRpt(List<EntityParm> parms )
        {
            using (Biz201 biz = new Biz201())
            {
                return biz.GetClientInfoAndRpt(parms);
            }
        }

        /// <summary>
        /// 类别列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityUserGrade> GetUserGrades()
        {
            using (Biz201 biz = new Biz201())
            {
                return biz.GetUserGrades();
            }
        }

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<EntityClientInfo> GetClientInfos(string search = null)
        {
            using (Biz201 biz = new Biz201())
            {
                return biz.GetClientInfos(search);
            }
        }
        #endregion

        #region 202 健康档案
        /// <summary>
        /// 体检报告
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityDisplayClientRpt> GetTjReports(List<EntityParm> parms = null)
        {
            using (Biz202 biz = new Biz202())
            {
                return biz.GetTjReports(parms);
            }
        }

        /// <summary>
        /// 体检报告结果
        /// </summary>
        /// <param name="regNo"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public Dictionary<string, List<EntityTjResult>> GetTjResult(string regNo, out List<EntityTjResult> dataResult, out List<EntityTjResult> xjResult, out EntityTjjljy tjjljyVo)
        {
            using (Biz202 biz = new Biz202())
            {
                return biz.GetTjResult(regNo,out dataResult,out xjResult, out tjjljyVo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntitySetQnControl> GetQnControl(List<EntityParm> parms = null)
        {
            using (Biz202 biz = new Biz202())
            {
                return biz.GetQnControl(parms);
            }
        }

        /// <summary>
        /// 原始表单位置
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityCtrlLocation> GetQnCtrlLocation(string qnCtlFiledId)
        {
            using (Biz202 biz = new Biz202())
            {
                return biz.GetQnCtrlLocation(qnCtlFiledId);
            }
        }

        /// <summary>
        /// 常规问卷--保存
        /// </summary>
        /// <param name="qnRecord"></param>
        /// <param name="qnData"></param>
        /// <param name="recId"></param>
        /// <returns></returns>
        public int SaveQnRecord(EntityQnRecord qnRecord, EntityQnData qnData, out decimal recId)
        {
            using (Biz202 biz = new Biz202())
            {
                return biz.SaveQnRecord(qnRecord, qnData, out recId);
            }
        }

        /// <summary>
        /// 常规问卷记录
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityQnRecord> GetQnRecords(List<EntityParm> parms)
        {
            using (Biz202 biz = new Biz202())
            {
                return biz.GetQnRecords(parms);
            }
        }

        /// <summary>
        /// 常规问卷--删除
        /// </summary>
        /// <param name="qnRecords"></param>
        /// <returns></returns>
        public int DelQnRecord(List<EntityQnRecord> qnRecords)
        {
            using (Biz202 biz = new Biz202())
            {
                return biz.DelQnRecord(qnRecords);
            }
        }

        /// <summary>
        /// 获取自定义问卷
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityDicQnMain> GetQnMain(List<EntityParm> parms )
        {
            using (Biz202 biz = new Biz202())
            {
                return biz.GetQnMain(parms);
            }
        }
        #endregion

        #region 203 健康报告

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityDisplayClientRpt> GetClientReports(List<EntityParm> parms)
        {
            using (Biz203 biz = new Biz203())
            {
                return biz.GetClientReports(parms);
            }
        }

        /// <summary>
        /// 重要指标字典
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityReportMainItemConfig> GetReportMainItemConfig(List<EntityParm> parms = null)
        {
            using (Biz203 biz = new Biz203())
            {
                return biz.GetReportMainItemConfig(parms);
            }
        }


        /// <summary>
        /// 疾病模型结果及名项得分
        /// </summary>
        /// <param name="lstMdParamCalc"></param>
        /// <returns></returns>
        public int SaveModelResultAndParamCalc(EntitymModelAccessRecord mdAccessRecord, List<EntityClientModelResult> lstMdResult, List<EntityModelParamCalc> lstMdParamCalc, List<EntityRiskFactorsResult> lstRiskFactorsResult)
        {
            using (Biz203 biz = new Biz203())
            {
                return biz.SaveModelResultAndParamCalc(mdAccessRecord, lstMdResult, lstMdParamCalc, lstRiskFactorsResult);
            }
        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="mdAccessRecord"></param>
        /// <returns></returns>
        public int UnConfirmRpt(EntitymModelAccessRecord mdAccessRecord)
        {
            using (Biz203 biz = new Biz203())
            {
                return biz.UnConfirmRpt(mdAccessRecord);
            }
        }

        /// <summary>
        /// 模型参数
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityModelParam> GetModelParam(List<EntityParm> parms = null)
        {
            using (Biz203 biz = new Biz203())
            {
                return biz.GetModelParam(parms);
            }
        }

        /// <summary>
        /// 疾病模型分析要点
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityModelAnalysisPoint> GetModelAnalysisPoint(List<EntityParm> parms = null)
        {
            using (Biz203 biz = new Biz203())
            {
                return biz.GetModelAnalysisPoint(parms);
            }
        }

        /// <summary>
        /// 疾病模型
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityModelAccess> GetModelAccess(List<EntityParm> parms = null)
        {
            using (Biz203 biz = new Biz203())
            {
                return biz.GetModelAccess(parms);
            }
        }

        /// <summary>
        /// 疾病模型参数
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public List<EntityModelGroupItem> GetModelGroup(string modelId)
        {
            using (Biz203 biz = new Biz203())
            {
                return biz.GetModelGroup(modelId);
            }
        }
        /// <summary>
        /// 危险因素
        /// </summary>
        /// <returns></returns>
        public List<EntityRiskFactor> GetRiskFactor()
        {
            using (Biz203 biz = new Biz203())
            {
                return biz.GetRiskFactor();
            }
        }
        /// <summary>
        /// 问卷家族疾病史
        /// </summary>
        /// <returns></returns>

        public List<EntityQnFamilyDease> GetQnFamilyDease()
        {
            using (Biz203 biz = new Biz203())
            {
                return biz.GetQnFamilyDease();
            }
        }
        #endregion

        #region 204 健康干预
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityPromotionTemplate> GetPromotionTemplates(List<EntityParm> parms = null)
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.GetPromotionTemplates(parms);
            }
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityPromotionTemplateConfig> GetPromotionTemplateConfigs(List<EntityParm> parms = null)
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.GetPromotionTemplateConfigs(parms);
            }
        }

        /// <summary>
        /// 保存计划
        /// </summary>
        /// <param name="promotionPlans"></param>
        /// <returns></returns>

        public int SavePromotionPan(List<EntityPromotionPlan> promotionPlans)
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.SavePromotionPan(promotionPlans);
            }
        }

        /// <summary>
        /// 干预计划转为干预记录
        /// </summary>
        /// <param name="promotionPlan"></param>
        /// <returns></returns>
        public int SavePromotionRecord(EntityPromotionPlan promotionPlan)
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.SavePromotionRecord(promotionPlan);
            }
        }

        /// <summary>
        /// 干预计划审核
        /// </summary>
        /// <param name="lstPlan"></param>
        /// <returns></returns>
        public int ConfirmPromotionRecord(List<EntityPromotionPlan> lstPlan)
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.ConfirmPromotionRecord(lstPlan);
            }
        }

        /// <summary>
        /// 干预内容 
        /// </summary>
        /// <returns></returns>
        public List<EntityPromotionContentConfig> GetPromotionContentConfigs()
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.GetPromotionContentConfigs();
            }
        }

        /// <summary>
        /// 干预形式
        /// </summary>
        /// <returns></returns>
        public List<EntityPromotionWayConfig> GetPromotionWayConfigs()
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.GetPromotionWayConfigs();
            }
        }

        /// <summary>
        /// 待执行计划
        /// </summary>
        /// <returns></returns>
        public List<EntityDisplayPromotionPlan> GetPromotionPlans(List<EntityParm> dicParm = null)
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.GetPromotionPlans(dicParm);
            }
        }

        /// <summary>
        /// 干预记录
        /// </summary>
        /// <returns></returns>
        public List<EntityDisplayPromotionPlan> GetPromotionPlanRecords(List<EntityParm> dicParm = null)
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.GetPromotionPlanRecords(dicParm);
            }
        }

        /// <summary>
        /// 健康管理报告评估分数
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityDisplayClientModelAcess> GetDisplayClientModelAcess(List<EntityParm> parms)
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.GetDisplayClientModelAcess(parms);
            }
        }

        /// <summary>
        /// 获取重要指标
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public List<EntityReportMainItem> GetReportMainItem(string reportId)
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.GetReportMainItem(reportId);
            }
        }

        /// <summary>
        /// 体检报告项目
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public List<EntityReportItem> GetReportItems(string reportId)
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.GetReportItems(reportId);
            }
        }

        /// <summary>
        /// 获取疾病模型
        /// </summary>
        /// <returns></returns>
        public List<EntityModelAccess> GetModelAccesses()
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.GetModelAccesses();
            }
        }

        /// <summary>
        /// 危险因素结果
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<entitydisplayriskresult> GetRiskFactorsResult(List<EntityParm> parms)
        {
            using (Biz204 biz = new Biz204())
            {
                return biz.GetRiskFactorsResult(parms);
            }
        }

        #endregion

        #region 205 慢病管理

        #region 高血压

        #region 人员列表
        /// <summary>
        /// 人员列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityGxyRecord> GetGxyPatients(List<EntityParm> parms)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.GetGxyPatients(parms);
            }
        }
        #endregion

        #region 添加人员记录
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gxyRecord"></param>
        /// <param name="recId"></param>
        /// <returns></returns>
        public int SaveGxyRecord(EntityGxyRecord gxyRecord, out decimal recId)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.SaveGxyRecord(gxyRecord,out recId);
            }
        }
        #endregion

        #region 随访记录
        /// <summary>
        /// 随访记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityGxySf> GetGxySfRecords(List<EntityParm> parms)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.GetGxySfRecords(parms);
            }
        }
        /// <summary>
        /// 随访记录-保存
        /// </summary>
        /// <param name="sfData"></param>
        /// <param name="sfId"></param>
        /// <returns></returns>
        public int SaveGxySfRecord(EntityGxyRecord gxyRecord, EntityGxySf gxySf, EntityGxySfData sfData, out decimal sfId)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.SaveGxySfRecord(gxyRecord, gxySf, sfData, out sfId);
            }
        }
        #endregion

        #region 评估记录
        /// <summary>
        /// 评估记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityGxyPg> GetGxyPgRecords(List<EntityParm> parms)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.GetGxyPgRecords(parms);
            }
        }
        /// <summary>
        /// 评估记录-保存
        /// </summary>
        /// <param name="pgData"></param>
        /// <param name="pgId"></param>
        /// <returns></returns>
        public int SaveGxyPgRecord(EntityGxyRecord gxyRecord, EntityGxyPg gxyPg, EntityGxyPgData pgData, out decimal pgId)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.SaveGxyPgRecord(gxyRecord, gxyPg, pgData, out pgId);
            }
        }
        #endregion

        #region 体检结果 血压
        /// <summary>
        /// 体检结果 血压
        /// </summary>
        /// <param name="clientNoStr"></param>
        /// <returns></returns>
        public List<EntityClientGxyResult> GetClientGxyResults(string clientNoStr)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.GetClientGxyResults(clientNoStr);
            }
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
            using (Biz205 biz = new Biz205())
            {
                return biz.GetTnbPatients(parms);
            }
        }
        #endregion

        #region 添加人员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tnbRecord"></param>
        /// <param name="recId"></param>
        /// <returns></returns>
        public int SaveTnbRecord(EntityTnbRecord tnbRecord, out decimal recId)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.SaveTnbRecord(tnbRecord,out recId);
            }
        }
        #endregion

        #region 随访记录
        /// <summary>
        /// 随访记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityTnbSf> GetTnbSfRecords(List<EntityParm> parms)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.GetTnbSfRecords(parms);
            }
        }

        /// <summary>
        /// 随访记录-保存
        /// </summary>
        /// <param name="tnbRecord"></param>
        /// <param name="tnbSf"></param>
        /// <param name="sfData"></param>
        /// <param name="sfId"></param>
        /// <returns></returns>
        public int SaveTnbSfRecord(EntityTnbRecord tnbRecord, EntityTnbSf tnbSf, EntityTnbSfData sfData, out decimal sfId)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.SaveTnbSfRecord(tnbRecord, tnbSf, sfData, out sfId);
            }
        }
        #endregion

        #region 评估记录
        /// <summary>
        /// 评估记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityTnbPg> GetTnbPgRecords(List<EntityParm> parms)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.GetTnbPgRecords(parms);
            }
        }
        /// <summary>
        /// 评估记录-保存
        /// </summary>
        /// <param name="pgData"></param>
        /// <param name="pgId"></param>
        /// <returns></returns>
        public int SaveTnbPgRecord(EntityTnbRecord tnbRecord, EntityTnbPg tnbPg, EntityTnbPgData pgData, out decimal pgId)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.SaveTnbPgRecord(tnbRecord, tnbPg, pgData, out pgId);
            }
        }
        #endregion

        #region 体检结果-血糖
        /// <summary>
        /// 体检结果 血糖
        /// </summary>
        /// <param name="clientNoStr"></param>
        /// <returns></returns>
        public List<EntityClientTnbResult> GetClientTnbResults(string clientNoStr)
        {
            using (Biz205 biz = new Biz205())
            {
                return biz.GetClientTnbResults(clientNoStr);
            }
        }
        #endregion

        #endregion

        #endregion

        #region 206 膳食管理

        #region 20601 膳食原则
        /// <summary>
        /// 获取膳食原则列表
        /// </summary>
        /// <returns></returns>
        public List<EntityDietPrinciple> GetDietPrinciple()
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.GetDietPrinciple();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dietPrinciple"></param>
        /// <returns></returns>
        public int SaveDietPrinciple(ref EntityDietPrinciple dietPrinciple)
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.SaveDietPrinciple(ref dietPrinciple);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int DeleteDietPrinciple(List<EntityDietPrinciple> data)
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.DeleteDietPrinciple(data);
            }
        }
        #endregion

        #region 20602 饮食菜谱模板
        /// <summary>
        /// 获取饮食菜谱模板类型列表
        /// </summary>
        /// <returns></returns>
        public List<EntityDietTemplatetype> GetDietTemplatetype()
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.GetDietTemplatetype();
            }
        }

        /// <summary>
        /// 获取饮食菜谱模板列表
        /// </summary>
        /// <returns></returns>
        public List<EntityDietTemplate> GetDietTemplate()
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.GetDietTemplate();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dietTemplate"></param>
        /// <returns></returns>
        public int SaveDietTemplate(ref EntityDietTemplate dietTemplate)
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.SaveDietTemplate(ref dietTemplate);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int DeleteDietTemplate(List<EntityDietTemplate> data)
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.DeleteDietTemplate(data);
            }
        }
        #endregion

        #region 成品菜
        /// <summary>
        /// 菜谱列表
        /// </summary>
        /// <returns></returns>
        public List<EntityDisplayDicCaiRecipe> GetDicCaiRecipe()
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.GetDicCaiRecipe();
            }
        }

        /// <summary>
        /// 菜 详细
        /// </summary>
        /// <returns></returns>
        public List<EntityDicCai> GetDicCai()
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.GetDicCai();
            }
        }
        /// <summary>
        /// 菜原料
        /// </summary>
        /// <param name="caiId"></param>
        /// <returns></returns>
        public List<EntityDicCaiIngredient> GetCaiIngredient(string caiId)
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.GetCaiIngredient(caiId);
            }
        }
        /// <summary>
        /// 原料分类
        /// </summary>
        /// <returns></returns>
        public List<EntityDicIngredientClassify> GetIngredientClassify()
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.GetIngredientClassify();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int SaveDicCai(ref EntityDicCai cai)
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.SaveDicCai(ref cai);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public int DeleteDicCai(EntityDicCai cai)
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.DeleteDicCai(cai);
            }
        }

        #endregion

        #region 原料

        /// <summary>
        /// 原料分类
        /// </summary>
        /// <returns></returns>
        public List<EntityDicIngredientClassify> GetDicIngredientClassify()
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.GetDicIngredientClassify();
            }
        }

        /// <summary>
        /// 菜 原料 字典 
        /// </summary>
        /// <param name="caiId"></param>
        /// <returns></returns>
        public List<EntityDicDientIngredient> GetDicDietIngredient()
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.GetDicDietIngredient();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dicDientIngredient"></param>
        /// <returns></returns>
        public int SaveDicIngredient(ref EntityDicDientIngredient dicDientIngredient)
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.SaveDicIngredient(ref dicDientIngredient);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dicDientIngredienti"></param>
        /// <returns></returns>
        public int DeleteDicIngredient(EntityDicDientIngredient dicDientIngredienti)
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.DeleteDicIngredient(dicDientIngredienti);
            }
        }

        #endregion

        #region 中医食疗

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public List<EntityDietTreatment> GetDietTreatment()
        {
            using (Biz206 biz = new Biz206())
            {
                return biz.GetDietTreatment();
            }
        }
        #endregion

        #endregion

        #region 207 服务预约
        
        #endregion

        #region 208 体检维护

        #region 体检项目

        #region 获取体检项目列表
        /// <summary>
        /// 获取体检项目列表
        /// </summary>
        /// <returns></returns>
        public List<EntityDicPeItem> GetPeItems()
        {
            using (Biz208 biz = new Biz208())
            {
                return biz.GetPeItems();
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public int SavePeItem(EntityDicPeItem vo, out string itemId)
        {
            using (Biz208 biz = new Biz208())
            {
                return biz.SavePeItem(vo, out itemId);
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public int DeletePeItem(string itemId)
        {
            using (Biz208 biz = new Biz208())
            {
                return biz.DeletePeItem(itemId);
            }
        }
        #endregion

        #region 20801 体检报告模板

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <returns></returns>
        public List<EntityReportTemplate> GetReportTemplate()
        {
            using (Biz208 biz = new Biz208())
            {
                return biz.GetReportTemplate();
            }
        }

        /// <summary>
        /// 获取体检项目详细信息
        /// </summary>
        /// <returns></returns>
        public List<EntityDisplaypeitem> GetReportTemplateDetail()
        {
            using (Biz208 biz = new Biz208())
            {
                return biz.GetReportTemplateDetail();
            }
        }
        #endregion

        #endregion

        #endregion

        #region 209 问卷维护

        #region 普通问卷

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
            using (Biz209 biz = new Biz209())
            {
                return biz.SaveQNnormal(vo, lstDet, out qnId, lstLaction, lstSettings);
            }
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
            using (Biz209 biz = new Biz209())
            {
                return biz.DeleteQNnormal(qnId);
            }
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
            using (Biz209 biz = new Biz209())
            {
                return biz.GetQnDetail(qnId);
            }
        }
        #endregion

        #region GetQnSetting
        /// <summary>
        /// GetQnSetting
        /// </summary>
        /// <returns></returns>
        public List<EntityQnSetting> GetQnSetting()
        {
            using (Biz209 biz = new Biz209())
            {
                return biz.GetQnSetting();
            }
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
            using (Biz209 biz = new Biz209())
            {
                biz.GetQnCustom(qnId, out lstTopic, out lstItems);
            }
        }
        #endregion

        #region GetQnList
        /// <summary>
        /// GetQnList
        /// </summary>
        /// <returns></returns>
        public List<EntityDicQnSetting> GetQnList()
        {
            using (Biz209 biz = new Biz209())
            {
                return biz.GetQnList();
            }
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
            using (Biz209 biz = new Biz209())
            {
                return biz.DeleteQnTopic(fieldId);
            }
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
        public int SaveQnTopic(EntityDicQnSetting mainVo, List<EntityDicQnSetting> lstSub, out string fieldId)
        {
            using (Biz209 biz = new Biz209())
            {
                return biz.SaveQnTopic(mainVo, lstSub, out fieldId);
            }
        }
        #endregion

        #region GetTopics
        /// <summary>
        /// GetTopics
        /// </summary>
        /// <returns></returns>
        public List<EntityDicQnSetting> GetTopics()
        {
            using (Biz209 biz = new Biz209())
            {
                return biz.GetTopics();
            }
        }
        #endregion

        #region GetTopicItems
        /// <summary>
        /// GetTopicItems
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public List<EntityDicQnSetting> GetTopicItems(string fieldId)
        {
            using (Biz209 biz = new Biz209())
            {
                return biz.GetTopicItems(fieldId);
            }
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
            using (Biz209 biz = new Biz209())
            {
                return biz.SaveHazards(vo, out hId);
            }
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
            using (Biz209 biz = new Biz209())
            {
                return biz.DeleteHazards(hId);
            }
        }
        #endregion

        #endregion


        #endregion

        #region 210 知识库

        #region 运动模板

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public int SaveSportItem(EntityDicSportItem vo, out decimal templateId)
        {
            using (Biz210 biz = new Biz210())
            {
                return biz.SaveSportItem(vo, out templateId);
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public int DeleteSportItem(decimal templateId)
        {
            using (Biz210 biz = new Biz210())
            {
                return biz.DeleteSportItem(templateId);
            }
        }
        #endregion

        #endregion

        #region 短信模板

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public int SaveMessageTemplate(EntityDicMessageContent vo, out decimal templateId)
        {
            using (Biz210 biz = new Biz210())
            {
                return biz.SaveMessageTemplate(vo, out templateId);
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public int DeleteMessageTemplate(decimal templateId)
        {
            using (Biz210 biz = new Biz210())
            {
                return biz.DeleteMessageTemplate(templateId);
            }
        }
        #endregion

        #endregion

        #endregion

        #region 211 统计分析

        #endregion

        #region Verify
        /// <summary>
        /// Verify
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        { return true; }
        #endregion

        #region IDispose
        /// <summary>
        /// IDispose
        /// </summary>
        public void Dispose()
        { GC.SuppressFinalize(this); }
        #endregion
    }
}
