using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    /// <summary>
    /// EntityDietDetails
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "dietDetails")]
    public class EntityDietDetails : BaseDataContract
    {
        /// <summary>
        /// recId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 1)]
        public System.Decimal recId { get; set; }

        /// <summary>
        /// day
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "day", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.Int32 day { get; set; }

        /// <summary>
        /// mealId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "mealId", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.Int32 mealId { get; set; }

        /// <summary>
        /// mealType
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "mealType", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String mealType { get; set; }

        /// <summary>
        /// caiId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "caiId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.String caiId { get; set; }

        /// <summary>
        /// caiName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "caiName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String caiName { get; set; }

        /// <summary>
        /// caiIngrediet
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "caiIngrediet", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.String caiIngrediet { get; set; }

        /// <summary>
        /// caiIngredietId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "caiIngredietId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.String caiIngredietId { get; set; }

        /// <summary>
        /// weihgt
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "weight", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 9)]
        public System.Decimal weight { get; set; }

        /// <summary>
        /// realWeight
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "realWeight", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.Decimal realWeight { get; set; }

        /// <summary>
        /// caiWeight
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "caiWeight", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 11)]
        public System.Decimal caiWeight { get; set; }

        /// <summary>
        /// per
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "per", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 12)]
        public System.Decimal per { get; set; }

        [DataMember]
        public List<EntityDietdetailsCai> lstDetailsCai { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string recId = "recId";
            public string day = "day";
            public string mealId = "mealId";
            public string mealType = "mealType";
            public string caiId = "caiId";
            public string caiName = "caiName";
            public string caiIngrediet = "caiIngrediet";
            public string caiIngredietId = "caiIngredietId";
            public string weight = "weight";
            public string realWeight = "realWeight";
            public string caiWeight = "caiWeight";
            public string per = "per";
        }
    }
}
