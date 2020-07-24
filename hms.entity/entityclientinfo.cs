using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    [DataContract,Serializable]
    public class EntityClientInfo : BaseDataContract
    {
        [DataMember]
        public string  id { get; set; }
        [DataMember]
        public string gradeId { get; set; }
        [DataMember]
        public string clientNo { get; set; }
        [DataMember]
        public string clientName { get; set; }
        [DataMember]
        public int gender { get; set; }
        [DataMember]
        public string birthday { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string telephone { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string qq { get; set; }
        [DataMember]
        public string cardNo { get; set; }
        [DataMember]
        public string company { get; set; }
        [DataMember]
        public string regionId { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public int booldType { get; set; }
        [DataMember]
        public int profession { get; set; }
        [DataMember]
        public int marriage { get; set; }
        [DataMember]
        public int ehtnicGroup { get; set; }
        [DataMember]
        public int eduationLevel { get; set; }
        [DataMember]
        public string clientTag { get; set; }
        [DataMember]
        public string contactName { get; set; }
        [DataMember]
        public string contactNameMobile { get; set; }
        [DataMember]
        public string clientRemarks { get; set; }
        [DataMember]
        public string dataSource { get; set; }
        [DataMember]
        public string upTag { get; set; }
        [DataMember]
        public DateTime serverDate { get; set; }
        [DataMember]
        public string bakfileld1 { get; set; }
        [DataMember]
        public string bakfileld2 { get; set; }
        [DataMember]
        public DateTime createDate { get; set; }
        [DataMember]
        public string creatorId { get; set; }
        [DataMember]
        public string createName { get; set; }
        public int reportCount { get; set; }
        [DataMember]
        public int conventionCount { get; set; }
        [DataMember]
        public string gradeName { get; set; }
        [DataMember]
        public string age { get; set; }
        [DataMember]
        public string sex { get; set; }
        [DataMember]
        public string strBirthday { get; set; }

        [DataMember]
        public int regTimes { get; set; }

        
    }
}
