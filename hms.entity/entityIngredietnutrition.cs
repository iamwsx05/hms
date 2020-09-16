using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    public class EntityIngredietNutrition : BaseDataContract
    {
        public string itemName { get; set; }
        public string recoJ { get; set; }
        public string proJ { get; set; }
    }
}
