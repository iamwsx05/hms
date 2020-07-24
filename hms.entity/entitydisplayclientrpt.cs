using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Drawing;
using weCare.Core.Entity;

namespace Hms.Entity
{
    [DataContract,Serializable]
    public class EntityDisplayClientRpt : BaseDataContract
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string clientName { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public string clientNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public int gender { get; set; }
        /// <summary>
        /// 体检编号
        /// </summary>
        [DataMember]
        public string reportNo { get; set; }

        [DataMember]
        public string reportId { get; set; }
        /// <summary>
        /// 报告日期
        /// </summary>
        [DataMember]
        public string reportDate { get; set; }
        /// <summary>
        /// 未配备异常
        /// </summary>
        [DataMember]
        public int unMatch { get; set; }
        /// <summary>
        /// 打印状态
        /// </summary>
        [DataMember]
        public int reportStatc { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        [DataMember]
        public int status { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public string confirmState { get; set; }
 
        /// <summary>
        /// 报告份数
        /// </summary>
        [DataMember]
        public int reportCount { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        [DataMember]
        public string gradeName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [DataMember]
        public string age { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [DataMember]
        public string company { get; set; }
        [DataMember]
        public string sex { get; set; }
        /// <summary>
        /// 问卷日期
        /// </summary>
        public string strQnDate { get; set; }
        /// <summary>
        /// 问卷
        /// </summary>
        public EntityQnRecord qnRecord { get; set; }

        [DataMember]
        public string examinationOrgan { get; set; }
        [DataMember]
        public string dataSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recordDateStr { get; set; }


        #region  个人报告内容
        [DataMember]
        public Image image01 { get; set; }
        [DataMember]
        public Image image02 { get; set; }
        [DataMember]
        public Image image03 { get; set; }
        [DataMember]
        public Image image04 { get; set; }
        [DataMember]
        public Image image05 { get; set; }
        [DataMember]
        public Image image06 { get; set; }
        [DataMember]
        public Image image07 { get; set; }
        [DataMember]
        public Image image08 { get; set; }
        [DataMember]
        public Image image09 { get; set; }
        [DataMember]
        public Image image10 { get; set; }
        [DataMember]
        public Image image11 { get; set; }
        [DataMember]
        public Image image12 { get; set; }
        [DataMember]
        public Image imageFx { get; set; }
        [DataMember]
        public Image imageTip { get; set; }
        
        /// <summary>
        /// 重要指标
        /// </summary>
        public List<EntityRptModelParam> lstMainIndicate { get; set; }
        /// <summary>
        /// 高血压
        /// </summary>
        public List<EntityRptModelParam> lstGxy { get; set; }
        /// <summary>
        /// 高血压风险评估 
        /// </summary>
        public List<EntityEvaluateResult> lstEvaluateGxy { get; set; }
        /// <summary>
        /// 糖尿病
        /// </summary>
        public List<EntityRptModelParam> lstTnb { get; set; }
        /// <summary>
        /// 糖尿病风险评估 
        /// </summary>
        public List<EntityEvaluateResult> lstEvaluateGxb { get; set; }
        /// <summary>
        /// 冠心病
        /// </summary>
        public List<EntityRptModelParam> lstGxb { get; set; }
        /// <summary>
        /// 冠心病风险评估 
        /// </summary>
        public List<EntityEvaluateResult> lstEvaluateTnb { get; set; }
        /// <summary>
        /// 血脂异常
        /// </summary>
        public List<EntityRptModelParam> lstXzyc { get; set; }
        /// <summary>
        /// 血脂异常风险评估 
        /// </summary>
        public List<EntityEvaluateResult> lstEvaluateXzyc { get; set; }
        /// <summary>
        /// 肥胖症
        /// </summary>
        public List<EntityRptModelParam>lstFpz { get; set; }
        /// <summary>
        /// 肥胖症风险评估 
        /// </summary>
        public List<EntityEvaluateResult> lstEvaluateFpz { get; set; }
        /// <summary>
        /// 脑卒中
        /// </summary>
        public List<EntityRptModelParam>lstNzz { get; set; }
        /// <summary>
        /// 脑卒中风险评估 
        /// </summary>
        public List<EntityEvaluateResult> lstEvaluateNzz { get; set; }
        /// <summary>
        /// 阿尔茨海默病
        /// </summary>
        public List<EntityRptModelParam> lstAzhmz { get; set; }
        /// <summary>
        /// 阿尔茨海默病风险评估 
        /// </summary>
        public List<EntityEvaluateResult> lstEvaluateAzhmz { get; set; }

        /// <summary>
        /// 肺癌
        /// </summary>
        public List<EntityRptModelParam> lstFa { get; set; }
        /// <summary>
        /// 肺癌风险评估 
        /// </summary>
        public List<EntityEvaluateResult> lstEvaluateFa { get; set; }

        /// <summary>
        /// 肝癌
        /// </summary>
        public List<EntityRptModelParam> lstGa { get; set; }
        /// <summary>
        /// 肝癌风险评估 
        /// </summary>
        public List<EntityEvaluateResult> lstEvaluateGa { get; set; }

        /// <summary>
        /// 胃癌
        /// </summary>
        public List<EntityRptModelParam> lstWa { get; set; }
        /// <summary>
        /// 胃癌风险评估 
        /// </summary>
        public List<EntityEvaluateResult> lstEvaluateWa { get; set; }

        /// <summary>
        /// 前列腺癌
        /// </summary>
        public List<EntityRptModelParam> lstQlxa { get; set; }
        /// <summary>
        ///前列腺癌风险评估 
        /// </summary>
        public List<EntityEvaluateResult> lstEvaluateQlxa { get; set; }
        public List<EntityAdPeItem> lstAdPeItemBse { get; set; }
        public List<EntityAdPeItem> lstAdPeItemSpecial { get; set; }
        public List<EntityMedicalAdvicecs> lstMedAdvices { get; set; }

        #endregion  
    }
}
