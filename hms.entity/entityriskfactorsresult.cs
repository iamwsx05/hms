using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using weCare.Core.Entity;

namespace Hms.Entity
{
    /// <summary>
    /// EntityRiskFactorsResult
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "riskFactorsResult")]
    public class EntityRiskFactorsResult : BaseDataContract
    {
        /// <summary>
        /// clientId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "clientId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String clientId { get; set; }

        /// <summary>
        /// questionId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "questionId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 4)]
        public System.Decimal questionId { get; set; }

        /// <summary>
        /// factorsId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "factorsId", DbType = DbType.AnsiString, IsPK = true, IsSeq = false, SerNo = 5)]
        public System.String factorsId { get; set; }

        /// <summary>
        /// organFactorsId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "organFactorsId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String organFactorsId { get; set; }

        /// <summary>
        /// isFamilyDisease
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "isFamilyDisease", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.String isFamilyDisease { get; set; }

        /// <summary>
        /// isHand
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "isHand", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.String isHand { get; set; }

        /// <summary>
        /// happenDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "happenDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 9)]
        public System.DateTime? happenDate { get; set; }

        /// <summary>
        /// advise
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "advise", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.String advise { get; set; }

        /// <summary>
        /// supplyExplian
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "supplyExplian", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 11)]
        public System.String supplyExplian { get; set; }

        /// <summary>
        /// orderId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "orderId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 12)]
        public System.Decimal? orderId { get; set; }

        /// <summary>
        /// recordDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 13)]
        public System.DateTime? recordDate { get; set; }

        /// <summary>
        /// recordId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 14)]
        public System.String recordId { get; set; }

        /// <summary>
        /// modifyDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modifyDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 15)]
        public System.DateTime? modifyDate { get; set; }

        /// <summary>
        /// modifyId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modifyId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 16)]
        public System.String modifyId { get; set; }

        /// <summary>
        /// filedId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "filedId", DbType = DbType.AnsiString, IsPK = true, IsSeq = false, SerNo = 17)]
        public System.String filedId { get; set; }

        /// <summary>
        /// filedName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "filedName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 18)]
        public System.String filedName { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string clientId = "clientId";
            public string questionId = "questionId";
            public string factorsId = "factorsId";
            public string organFactorsId = "organFactorsId";
            public string isFamilyDisease = "isFamilyDisease";
            public string isHand = "isHand";
            public string happenDate = "happenDate";
            public string advise = "advise";
            public string supplyExplian = "supplyExplian";
            public string orderId = "orderId";
            public string recordDate = "recordDate";
            public string recordId = "recordId";
            public string modifyDate = "modifyDate";
            public string modifyId = "modifyId";
            public string filedId = "filedId";
            public string filedName = "filedName";
        }
    }
}
