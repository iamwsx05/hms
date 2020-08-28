using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace weCare.Core.Entity
{
    /// <summary>
    /// EntityTnbPg
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "tnbPg")]
    public class EntityTnbPg : BaseDataContract
    {
        /// <summary>
        /// pgId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "pgId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.Decimal pgId { get; set; }

        /// <summary>
        /// recId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.Decimal recId { get; set; }

        /// <summary>
        /// dangerLevel
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "dangerLevel", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String dangerLevel { get; set; }

        /// <summary>
        /// manageLevel
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "manageLevel", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String manageLevel { get; set; }

        /// <summary>
        /// evaluator
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "evaluator", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.String evaluator { get; set; }

        /// <summary>
        /// evaDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "evaDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.DateTime evaDate { get; set; }

        /// <summary>
        /// status
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "status", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.Decimal status { get; set; }

        /// <summary>
        /// recordDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.DateTime? recordDate { get; set; }

        [DataMember]
        public string clientName { get; set; }
        [DataMember]
        public string clientNo { get; set; }
        [DataMember]
        public string sex { get; set; }
        [DataMember]
        public string age { get; set; }
        [DataMember]
        public string company { get; set; }
        [DataMember]
        public string birthday { get; set; }
        [DataMember]
        public string gradeName { get; set; }
        [DataMember]
        public string evaDateStr { get; set; }
        [DataMember]
        public string pgData { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string pgId = "pgId";
            public string recId = "recId";
            public string dangerLevel = "dangerLevel";
            public string manageLevel = "manageLevel";
            public string evaluator = "evaluator";
            public string evaDate = "evaDate";
            public string status = "status";
            public string recordDate = "recordDate";
        }
    }
}
