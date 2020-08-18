using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace Hms.Entity
{
    /// <summary>
    /// EntityHmsSF
    /// </summary>
    [DataContract, Serializable]
    public class EntityHmsSF : weCare.Core.Entity.BaseDataContract
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        [DataMember]
        public decimal recId { get; set; }
        /// <summary>
        /// 病人ID
        /// </summary>
        [DataMember]
        public string patId { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        [DataMember]
        public string clientNo { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        [DataMember]
        public string patName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public string sex { get; set; }
        /// <summary>
        /// 性别描述
        /// </summary>
        [DataMember]
        public string sexCH
        {
            get
            {
                if (sex == "1")
                    return "男";
                else if (sex == "2")
                    return "女";
                else
                    return "未知";
            }
            set {; }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        [DataMember]
        public string age { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        [DataMember]
        public string patClass { get; set; }
        /// <summary>
        /// 管理开始时间
        /// </summary>
        [DataMember]
        public string manageBeginDate { get; set; }
        /// <summary>
        /// 管理等级
        /// </summary>
        [DataMember]
        public string manageLevel { get; set; }
        /// <summary>
        /// 随访ID
        /// </summary>
        [DataMember]
        public string sfId { get; set; }
        /// <summary>
        /// 随访次数
        /// </summary>
        [DataMember]
        public string sfTimes { get; set; }
        /// <summary>
        /// 评估次数
        /// </summary>
        [DataMember]
        public string evaTimes { get; set; }
        /// <summary>
        /// 下次随访时间
        /// </summary>
        [DataMember]
        public string sfNextDate { get; set; }
        /// <summary>
        /// 计划次数
        /// </summary>
        [DataMember]
        public string planTimes { get; set; }
        /// <summary>
        /// 随访时间
        /// </summary>
        [DataMember]
        public string sfDate { get; set; }
        /// <summary>
        /// 随访方式
        /// </summary>
        [DataMember]
        public string sfMethod { get; set; }
        /// <summary>
        /// 随访分类
        /// </summary>
        [DataMember]
        public string sfClass { get; set; }
        /// <summary>
        /// 随访人员
        /// </summary>
        [DataMember]
        public string sfRecorder { get; set; }
        /// <summary>
        /// 评估ID
        /// </summary>
        [DataMember]
        public string pgId { get; set; }
        /// <summary>
        /// 血压分级
        /// </summary>
        [DataMember]
        public string bloodPressLevel { get; set; }
        /// <summary>
        /// 危险分层
        /// </summary>
        [DataMember]
        public string dangerLevel { get; set; }
        /// <summary>
        /// 评估日期
        /// </summary>
        [DataMember]
        public string evaDate { get; set; }
        /// <summary>
        /// 评估人
        /// </summary>
        [DataMember]
        public string evaluator { get; set; }

    }
}
