using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Itf;
using weCare.Core.Utils;
using Hms.Entity;

namespace Hms.Itf
{
    [ServiceContract]
    public interface ItfHms : IWcf, IDisposable
    {
        #region 201 客户管理
        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetClientInfoAndRpt")]
        List<EntityClientInfo> GetClientInfoAndRpt(List<EntityParm> parms);

        /// <summary>
        /// 类别列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetUserGrades")]
        List<EntityUserGrade> GetUserGrades();

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetClientInfos")]
        List<EntityClientInfo> GetClientInfos(string search = null);
        #endregion

        #region 202 健康档案
        /// <summary>
        /// /
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTjReports")]
        List<EntityDisplayClientRpt> GetTjReports(List<EntityParm> parms);

        /// <summary>
        /// 体检报告项目结果
        /// </summary>
        /// <param name="regNo"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTjResult")]
        Dictionary<string, List<EntityTjResult>> GetTjResult(string regNo, out List<EntityTjResult> dataResult, out List<EntityTjResult> xjResult, out EntityTjjljy tjjljyVo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetQnControl")]
        List<EntitySetQnControl> GetQnControl(List<EntityParm> parms);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetQnCtrlLocation")]
        List<EntityCtrlLocation> GetQnCtrlLocation(string qnCtlFiledId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qnRecord"></param>
        /// <param name="qnData"></param>
        /// <param name="recId"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveQnRecord")]
        int SaveQnRecord(EntityQnRecord qnRecord, EntityQnData qnData, out decimal recId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetQnRecords")]
        List<EntityQnRecord> GetQnRecords(List<EntityParm> parms);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qnRecords"></param>
        /// <returns></returns>
        [OperationContract(Name = "DelQnRecord")]
        int DelQnRecord(List<EntityQnRecord> qnRecords);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetQnMain")]
        List<EntityDicQnMain> GetQnMain(List<EntityParm> parms);

        #endregion

        #region 203 健康报告
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetClientReports")]
        List<EntityDisplayClientRpt> GetClientReports(List<EntityParm> parms);

        /// <summary>
        /// 重要指标字典
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetReportMainItemConfig")]
        List<EntityReportMainItemConfig> GetReportMainItemConfig(List<EntityParm> parms = null);

        /// <summary>
        /// 模型参数
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelParam")]
        List<EntityModelParam> GetModelParam(List<EntityParm> parms = null);

        /// <summary>
        /// 疾病模型结果及名项得分
        /// </summary>
        /// <param name="lstMdResult"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveModelResultAndParamCalc")]
        int SaveModelResultAndParamCalc(EntitymModelAccessRecord mdAccessRecord, List<EntityClientModelResult> lstMdResult, List<EntityModelParamCalc> lstMdParamCalc,List<EntityRiskFactorsResult> lstRiskFactorsResult);

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="mdAccessRecord"></param>
        /// <returns></returns>
        [OperationContract(Name = "UnConfirmRpt")]
        int UnConfirmRpt(EntitymModelAccessRecord mdAccessRecord);

        /// <summary>
        /// 疾病模型分析要点
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelAnalysisPoint")]
        List<EntityModelAnalysisPoint> GetModelAnalysisPoint(List<EntityParm> parms = null);

        /// <summary>
        /// 疾病模型
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelAccess")]
        List<EntityModelAccess> GetModelAccess(List<EntityParm> parms = null);

        /// <summary>
        /// 疾病模型参数
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelGroup")]
        List<EntityModelGroupItem> GetModelGroup(string modelId = null);

        /// <summary>
        /// 危险因素
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetRiskFactor")]
        List<EntityRiskFactor> GetRiskFactor();

        /// <summary>
        /// 问卷家庭疾病史
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetQnFamilyDease")]
        List<EntityQnFamilyDease> GetQnFamilyDease();
        #endregion

        #region 204 健康干预
        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPromotionTemplates")]
        List<EntityPromotionTemplate> GetPromotionTemplates(List<EntityParm> parms);
        /// <summary>
        /// 获取模板配置
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPromotionTemplateConfigs")]
        List<EntityPromotionTemplateConfig> GetPromotionTemplateConfigs(List<EntityParm> parms);

        /// <summary>
        /// 保存计划
        /// </summary>
        /// <param name="promotionPlans"></param>
        /// <returns></returns>
        [OperationContract(Name = "SavePromotionPan")]
        int SavePromotionPan(List<EntityPromotionPlan> promotionPlans);

        /// <summary>
        /// 干预计划转记录
        /// </summary>
        /// <param name="promotionPlan"></param>
        /// <returns></returns>
        [OperationContract(Name = "SavePromotionPan")]
        int SavePromotionRecord(EntityPromotionPlan promotionPlan);

        /// <summary>
        /// 干预计划审核
        /// </summary>
        /// <param name="lstPlan"></param>
        /// <returns></returns>
        [OperationContract(Name = "ConfirmPromotionRecord")]
        int ConfirmPromotionRecord(List<EntityPromotionPlan> lstPlan);

        /// <summary>
        /// 干预形式
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPromotionWayConfigs")]
        List<EntityPromotionWayConfig> GetPromotionWayConfigs();

        /// <summary>
        /// 干预内容
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPromotionContentConfigs")]
        List<EntityPromotionContentConfig> GetPromotionContentConfigs();

        /// <summary>
        /// 待执行计划
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetPromotionPlans")]
        List<EntityDisplayPromotionPlan> GetPromotionPlans(List<EntityParm> dicParm);

        /// <summary>
        /// 干预记录
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetPromotionPlanRecords")]
        List<EntityDisplayPromotionPlan> GetPromotionPlanRecords(List<EntityParm> dicParm);

        /// <summary>
        /// 健康管理报告评估分数
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDisplayClientModelAcess")]
        List<EntityDisplayClientModelAcess> GetDisplayClientModelAcess(List<EntityParm> parms);

        /// <summary>
        /// 获取重要指标
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetReportMainItem")]
        List<EntityReportMainItem> GetReportMainItem(string reportId);

        /// <summary>
        /// 体检项目列表
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetReportItems")]
        List<EntityReportItem> GetReportItems(string reportId);

        /// <summary>
        /// 危险因素结果
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        List<entitydisplayriskresult> GetRiskFactorsResult(List<EntityParm> parms);
        #endregion

        #region 205 慢病管理

        #region 高血压

        /// <summary>
        /// 人员列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetGxyPatients")]
        List<EntityGxyRecord> GetGxyPatients(List<EntityParm> parms);

        /// <summary>
        /// 添加人员记录
        /// </summary>
        /// <param name="gxyRecord"></param>
        /// <param name="recId"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveGxyRecord")]
        int SaveGxyRecord(EntityGxyRecord gxyRecord, out decimal recId);

        /// <summary>
        /// 随访记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetGxySfRecords")]
        List<EntityGxySf> GetGxySfRecords(List<EntityParm> parms);

        /// <summary>
        /// 随访记录-保存
        /// </summary>
        /// <param name="sfData"></param>
        /// <param name="sfId"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveGxySfRecord")]
        int SaveGxySfRecord(EntityGxyRecord gxyRecord, EntityGxySf gxySf, EntityGxySfData sfData, out decimal sfId);

        /// <summary>
        /// 评估记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetGxyPgRecords")]
        List<EntityGxyPg> GetGxyPgRecords(List<EntityParm> parms);

        /// <summary>
        /// 评估记录-保存
        /// </summary>
        /// <param name="pgData"></param>
        /// <param name="pgId"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveGxyPgRecord")]
        int SaveGxyPgRecord(EntityGxyRecord gxyRecord, EntityGxyPg gxyPg, EntityGxyPgData pgData, out decimal pgId);

        /// <summary>
        /// 体检结果 血压
        /// </summary>
        /// <param name="clientNoStr"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetClientGxyResults")]
        List<EntityClientGxyResult> GetClientGxyResults(string clientNoStr);

        /// <summary>
        /// 体检结果 血糖
        /// </summary>
        /// <param name="clientNoStr"></param>
        /// <returns></returns>
        List<EntityClientTnbResult> GetClientTnbResults(string clientNoStr);

        #endregion

        #region 糖尿病

        /// <summary>
        /// 人员列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTnbPatients")]
        List<EntityTnbRecord> GetTnbPatients(List<EntityParm> parms);

        /// <summary>
        /// 添加人员记录
        /// </summary>
        /// <param name="gxyRecord"></param>
        /// <param name="recId"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveTnbRecord")]
        int SaveTnbRecord(EntityTnbRecord gxyRecord, out decimal recId);

        /// <summary>
        /// 随访记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTnbSfRecords")]
        List<EntityTnbSf> GetTnbSfRecords(List<EntityParm> parms);

        /// <summary>
        /// 随访记录-保存
        /// </summary>
        /// <param name="sfData"></param>
        /// <param name="sfId"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveTnbSfRecord")]
        int SaveTnbSfRecord(EntityTnbRecord tnbRecord, EntityTnbSf tnbSf, EntityTnbSfData sfData, out decimal sfId);

        /// <summary>
        /// 评估记录-获取
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTnbPgRecords")]
        List<EntityTnbPg> GetTnbPgRecords(List<EntityParm> parms);

        /// <summary>
        /// 评估记录-保存
        /// </summary>
        /// <param name="pgData"></param>
        /// <param name="pgId"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveTnbPgRecord")]
        int SaveTnbPgRecord(EntityTnbRecord tnbRecord, EntityTnbPg tnbPg, EntityTnbPgData pgData, out decimal pgId);

        #endregion

        #endregion

        #region 206 膳食管理

        #region 膳食原则
        /// <summary>
        /// 获取膳食原则列表
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetDietPrinciple")]
        List<EntityDietPrinciple> GetDietPrinciple();

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dietPrinciple"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveDietPrinciple")]
        int SaveDietPrinciple(ref EntityDietPrinciple dietPrinciple);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeleteDietPrinciple")]
        int DeleteDietPrinciple(List<EntityDietPrinciple> data);
        #endregion

        #region 膳食方案
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="lstDietRecord"></param>
        /// <param name="lstDietDetails"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveDietCai")]
        int SaveDietCai(List<EntityDietRecord> lstDietRecord, List<EntityDietDetails> lstDietDetails, out Dictionary<string, decimal> dicRecId);

        /// <summary>
        /// 获取食谱
        /// </summary>
        /// <param name="dietRecIdStr"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDietDetails")]
        List<EntityDietDetails> GetDietDetails(string dietRecIdStr);

        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDietRecords")]
        List<EntityDietRecord> GetDietRecords(List<EntityParm> parms);

        #endregion

        #region 饮食菜谱模板
        /// <summary>
        /// 模板类型
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetDietTemplatetype")]
        List<EntityDietTemplatetype> GetDietTemplatetype();

        /// <summary>
        /// 模板列表
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetDietTemplate")]
        List<EntityDietTemplate> GetDietTemplate();

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dietTemplate"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveDietTemplate")]
        int SaveDietTemplate(ref EntityDietTemplate dietTemplate);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        [OperationContract(Name = "DeleteDietTemplate")]
        int DeleteDietTemplate(List<EntityDietTemplate> data);
        #endregion

        #region 成品菜
        /// <summary>
        /// 菜谱列表
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetDicCaiRecipe")]
        List<EntityDisplayDicCaiRecipe> GetDicCaiRecipe();

        /// <summary>
        /// 菜 详细
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetDicCai")]
        List<EntityDicCai> GetDicCai();

        /// <summary>
        /// 菜原料
        /// </summary>
        /// <param name="caiId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCaiIngredient")]
        List<EntityDicCaiIngredient> GetCaiIngredient(string caiId);

        /// <summary>
        /// 菜 原料字典
        /// </summary>
        /// <param name="caiId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDicDietIngredient")]
        List<EntityDicDientIngredient> GetDicDietIngredient();

        /// <summary>
        /// 原料分类
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetIngredientClassify")]
        List<EntityDicIngredientClassify> GetIngredientClassify();

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="cai"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveDicCai")]
        int SaveDicCai(ref EntityDicCai cai);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="cai"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeleteDicCai")]
        int DeleteDicCai(EntityDicCai cai);

        #endregion

        #region 菜原料
        /// <summary>
        /// 原料 分类
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetDicIngredientClassify")]
        List<EntityDicIngredientClassify> GetDicIngredientClassify();
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dicDientIngredient"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveDicIngredient")]
        int SaveDicIngredient(ref EntityDicDientIngredient dicDientIngredient);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dicDientIngredienti"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeleteDicIngredient")]
        int DeleteDicIngredient(EntityDicDientIngredient dicDientIngredienti);
        #endregion

        #region 中医食疗
        [OperationContract(Name = "GetDietTreatment")]
        List<EntityDietTreatment> GetDietTreatment();
        #endregion

        #endregion

        #region 207 服务预约

        #endregion

        #region 208 体检维护

        /// <summary>
        /// 获取体检项目列表
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetPeItems")]
        List<EntityDicPeItem> GetPeItems();

        [OperationContract(Name = "SavePeItem")]
        int SavePeItem(EntityDicPeItem vo, out string itemId);

        [OperationContract(Name = "DeletePeItem")]
        int DeletePeItem(string itemId);

        #region  体检报告模板
        /// <summary>
        /// 获取体检报告模板
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetReportTemplate")]
        List<EntityReportTemplate> GetReportTemplate();

        /// <summary>
        /// 获取体检分类详细项目
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetReportTemplateDetail")]
        List<EntityDisplaypeitem> GetReportTemplateDetail();

        #endregion

        #endregion

        #region 209 问卷维护

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="lstDet"></param>
        /// <param name="qnId"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveQNnormal")]
        int SaveQNnormal(EntityDicQnMain vo, List<EntityDicQnDetail> lstDet, out decimal qnId, List<EntityDicQnCtlLocation> lstLaction = null, List<EntityDicQnSetting> lstSettings = null);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="qnId"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeleteQNnormal")]
        int DeleteQNnormal(decimal qnId);

        [OperationContract(Name = "GetQnDetail")]
        List<EntityDicQnDetail> GetQnDetail(decimal qnId);

        /// <summary>
        /// GetQnSetting
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetQnSetting")]
        List<EntityQnSetting> GetQnSetting();

        [OperationContract(Name = "GetQnCustom")]
        void GetQnCustom(decimal qnId, out List<EntityDicQnSetting> lstTopic, out List<EntityDicQnSetting> lstItems);

        [OperationContract(Name = "GetQnList")]
        List<EntityDicQnSetting> GetQnList();

        [OperationContract(Name = "GetTopics")]
        List<EntityDicQnSetting> GetTopics();

        [OperationContract(Name = "GetTopicItems")]
        List<EntityDicQnSetting> GetTopicItems(string fieldId);

        [OperationContract(Name = "DeleteQnTopic")]
        int DeleteQnTopic(string fieldId);

        [OperationContract(Name = "SaveQnTopic")]
        int SaveQnTopic(EntityDicQnSetting mainVo, List<EntityDicQnSetting> lstSub, out string fieldId);

        [OperationContract(Name = "SaveHazards")]
        int SaveHazards(EntityDicHazards vo, out decimal hId);

        [OperationContract(Name = "DeleteHazards")]
        int DeleteHazards(decimal hId);

        #endregion

        #region 210 知识库

        #region 运动模板
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveSportItem")]
        int SaveSportItem(EntityDicSportItem vo, out decimal templateId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeleteSportItem")]
        int DeleteSportItem(decimal templateId);
        #endregion

        #region 短信模板
        /// <summary>
        /// 保存短信模板
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveMessageTemplate")]
        int SaveMessageTemplate(EntityDicMessageContent vo, out decimal templateId);

        /// <summary>
        /// 删除短信模板
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeleteMessageTemplate")]
        int DeleteMessageTemplate(decimal templateId);
        #endregion

        #endregion

        #region 211 统计分析

        #endregion

    }
}
