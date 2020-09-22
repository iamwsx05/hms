using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    public class EntityDietdetailsIngrediet :BaseDataContract
    {
        [DataMember]
        public decimal recId { get; set; }
        [DataMember]
        public int day { get; set; }
        [DataMember]
        public int mealId { get; set; }
        [DataMember]
        public string caiId { get; set; }

        /// <summary>
        /// caiIngrediet
        /// </summary>
        [DataMember]
        public System.String caiIngrediet { get; set; }

        /// <summary>
        /// caiIngredietId
        /// </summary>
        [DataMember]
        public System.String caiIngredietId { get; set; }

        /// <summary>
        /// weihgt
        /// </summary>
        [DataMember]
        public System.Decimal weight { get; set; }
    }
}
