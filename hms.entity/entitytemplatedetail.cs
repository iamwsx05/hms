using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    [DataContract,Serializable]
    public class EntityTemplatedetail : BaseDataContract
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool isSelect { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary
        [DataMember]
        public string itemName { get; set; }
        /// <summary>
        /// 参考范围
        /// </summary>
        public string refRange { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [DataMember]
        public string itemUnit { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public int gender { get; set; }

        public string sex
        {
            get
            {
                if (gender == 1)
                    return "男";
                else if (gender == 2)
                    return "女";
                else
                    return "不限";
            }
        }
        /// <summary>
        /// 是否对比
        /// </summary>
        [DataMember]
        public int isCompare { get; set; }

        public string isCompareName
        {
            get
            {
                return isCompare == 1 ? "是" : "";
            }
        }
        /// <summary>
        /// 重要指标
        /// </summary>
        [DataMember]
        public int isMain { get; set; }
        public string isMainName
        {
            get
            {
                return isMain == 1 ? "是" : "";
            }
        }
    }
}
