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
    /// EntityRiskFactor
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "riskFactor")]
    public class EntityRiskFactor : BaseDataContract
    {
        /// <summary>
        /// id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "id", DbType = DbType.AnsiString, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.String id { get; set; }

        /// <summary>
        /// showSort
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "showSort", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.String showSort { get; set; }

        /// <summary>
        /// riskFactor
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "riskFactor", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String riskFactor { get; set; }

        /// <summary>
        /// inCondition
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "inCondition", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String inCondition { get; set; }

        /// <summary>
        /// advice
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "advice", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.String advice { get; set; }

        /// <summary>
        /// remarks
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "remarks", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String remarks { get; set; }

        /// <summary>
        /// remarks
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "jugeValue", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 7)]
        public string jugeValue { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string id = "id";
            public string showSort = "showSort";
            public string riskFactor = "riskFactor";
            public string inCondition = "inCondition";
            public string advice = "advice";
            public string remarks = "remarks";
            public string jugeValue = "jugeValue";
        }

    }
}
