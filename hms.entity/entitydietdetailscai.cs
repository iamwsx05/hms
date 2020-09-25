using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    [DataContract, Serializable]
    public class EntityDietdetailsCai : BaseDataContract
    {
        public EntityDietdetailsCai ()
        {
            lstDietdetailsIngrediet = new List<EntityDietDetails>();
            lstDietTemplateDetails = new List<EntityDietTemplateDetails>(); 
        }
        [DataMember]
        public decimal recId { get; set; }
        [DataMember]
        public string templateId { get; set; }
        [DataMember]
        public int day{ get; set; }
        [DataMember]
        public int mealId { get; set; }
        [DataMember]
        public string caiId { get; set; }
        [DataMember]
        public string caiName { get; set; }
        [DataMember]
        public decimal weight { get; set; }
        public List<EntityDietDetails> lstDietdetailsIngrediet { get; set; }

        public List<EntityDietTemplateDetails> lstDietTemplateDetails{ get; set; }
    }
}
