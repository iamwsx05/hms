﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    [DataContract,Serializable]
    public class EntityDisplayPromotionPlan :BaseDataContract
    {
        [DataMember]
        public string clientName { get; set; }
        [DataMember]
        public string clientNo { get; set; }
        [DataMember]
        public int gender { get; set; }
        [DataMember]
        public string birthday { get; set; }
        [DataMember]
        public string company { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string gradeName { get; set; }
        [DataMember]
        public string planWay { get; set; }
        [DataMember]
        public string planContent { get; set; }
        [DataMember]
        public string planRemind { get; set; }
        [DataMember]
        public string planDate { get; set; }
        [DataMember]
        public string executeTime { get; set; }
        [DataMember]
        public string createName { get; set; }

        [DataMember]
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
        [DataMember]
        public string age { get; set; }
    }
}