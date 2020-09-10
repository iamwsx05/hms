using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    public class EntityCaiDiet : BaseDataContract
    {
        [DataMember]
        public string mealType { get; set; }
        [DataMember]
        public string caiName { get; set; }
        [DataMember]
        public string caiIngrediet { get; set; }
        [DataMember]
        public decimal weihgt { get; set; }
    }
}
