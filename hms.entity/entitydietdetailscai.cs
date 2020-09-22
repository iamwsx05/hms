using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    public class EntityDietdetailsCai : BaseDataContract
    {
        [DataMember]
        public decimal recId { get; set; }
        [DataMember]
        public decimal day { get; set; }
        [DataMember]
        public decimal mealId { get; set; }
        [DataMember]
        public string caiId { get; set; }
        [DataMember]
        public string caiName { get; set; }
        [DataMember]
        public decimal weight { get; set; }
        [DataMember]
        public List<EntityDietDetails> lstDietdetailsIngrediet { get; set; }
    }
}
