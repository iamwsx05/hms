using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    public class entitydisplayriskresult : BaseDataContract
    {
        [DataMember]
        public string clientId { get; set; }
        [DataMember]
        public string questionId { get; set; }
        [DataMember]
        public string factorsId { get; set; }
        [DataMember]
        public string showSort { get; set; }
        [DataMember]
        public string riskFactor { get; set; }
        [DataMember]
        public string isFamilyDisease { get; set; }
        [DataMember]
        public string filedId { get; set; }
        [DataMember]
        public string filedName { get; set; }
        [DataMember]
        public string advise { get; set; }
    }
}
