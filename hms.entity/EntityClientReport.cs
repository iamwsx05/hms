using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;
using System.Drawing;

namespace Hms.Entity
{
    public class EntityClientReport : BaseDataContract
    {
        [DataMember]
        public string reportNo { get; set; }
        [DataMember]
        public string clientName { get; set; }
        [DataMember]
        public string clientNo { get; set; }
        [DataMember]
        public string company { get; set; }
        [DataMember]
        public string reportDate { get; set; }
        [DataMember]
        public string qnDate { get; set; }
        [DataMember]
        public string sex { get; set; }
        [DataMember]
        public string age { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        [DataMember]
        public string tjSumup { get; set; }
        /// <summary>
        /// 现患疾病
        /// </summary>
        [DataMember]
        public string illness { get; set; }
        /// <summary>
        /// 家族病史
        /// </summary>
        [DataMember]
        public string familyIllness { get; set; }

        /// <summary>
        /// 重要指标
        /// </summary>
        public  List<EntityReportMainItem> lstMainItem { get; set; }

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
        public Image imageTip { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<EntityRptModelAcess>  lstRptModelAcess { get; set; }


        #endregion  
    }
}
