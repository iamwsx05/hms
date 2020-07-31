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
    /// EntityModelAvgRisk
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "modelAvgRisk")]
    public class EntityModelAvgRisk : BaseDataContract
    {
        /// <summary>
        /// id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "id", DbType = DbType.Int32, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.Int32 id { get; set; }

        /// <summary>
        /// modelId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modelId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.Decimal modelId { get; set; }

        /// <summary>
        /// minAge
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "minAge", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.Decimal minAge { get; set; }

        /// <summary>
        /// maxAge
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "maxAge", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.Decimal maxAge { get; set; }

        /// <summary>
        /// defaultRisk
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "defaultRisk", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.Decimal defaultRisk { get; set; }

        /// <summary>
        /// configRiskMan
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "configRiskMan", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.Decimal configRiskMan { get; set; }

        /// <summary>
        /// configRiskWoman
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "configRiskWoman", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.Decimal configRiskWoman { get; set; }

        /// <summary>
        /// isUse
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "isUse", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 9)]
        public int isUse { get; set; }

        /// <summary>
        /// recordDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.DateTime recordDate { get; set; }

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
            public string modelId = "modelId";
            public string minAge = "minAge";
            public string maxAge = "maxAge";
            public string defaultRisk = "defaultRisk";
            public string configRiskMan = "configRiskMan";
            public string configRiskWoman = "configRiskWoman";
            public string isUse = "isUse";
            public string recordDate = "recordDate";
        }

    }
}
