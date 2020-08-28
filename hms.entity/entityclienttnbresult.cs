using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    public class EntityClientTnbResult : BaseDataContract
    {
        public string clientNo { get; set; }
        [DataMember]
        public string clientName { get; set; }
        [DataMember]
        public string regNo { get; set; }
        [DataMember]
        public int regTimes { get; set; }
        [DataMember]
        public int gender { get; set; }
        [DataMember]
        public string birthday { get; set; }
        [DataMember]
        public string company { get; set; }
        [DataMember]
        public string gradeName { get; set; }
        [DataMember]
        public string age { get; set; }
        [DataMember]
        public string sex { get; set; }
        [DataMember]
        public string tnb { get; set; }
        [DataMember]
        public string tnbYc { get; set; }
        [DataMember]
        public string isTnb { get; set; }
    }
}
