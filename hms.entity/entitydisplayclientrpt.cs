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


       
    }
}
