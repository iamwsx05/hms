using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Hms.Entity
{
    public class EntityRptModelAcess
    {    
        [DataMember]
        public decimal modelId { get; set; }
        /// <summary>
         /// 参数
         /// </summary>
        [DataMember]
        public List<EntityRptModelParam> lstModelParam { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        [DataMember]
        public decimal df { get; set; }
        //最佳得分
        [DataMember]
        public decimal bestDf { get; set; }
        //可降低得分
        [DataMember]
        public decimal reduceDf { get; set; }

        //评估项目
        public List<EntityEvaluateResult> lstEvaluate { get; set; }
        //风险图片
        [DataMember]
        public Image imgFx01 { get; set; }
        [DataMember]
        public Image imgFx02 { get; set; }
        [DataMember]
        public Image imgFx03 { get; set; }
        [DataMember]
        public Image imgFx04 { get; set; }

        /// <summary>
        /// 评估结果
        /// </summary>
        [DataMember]
        public string resultStr { get; set; }

        //预防要点
        public List<string> lstPoint { get; set; }

    }
}
