using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace weCare.Core.Entity
{
    /// <summary>
    /// EntityGxyRecord
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "gxyRecord")]
    public class EntityGxyRecord : BaseDataContract
    {
        /// <summary>
        /// recId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 1)]
        public System.Decimal recId { get; set; }

        /// <summary>
        /// clientNo
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "clientNo", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.String clientNo { get; set; }

        /// <summary>
        /// regTimes
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "regTimes", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 3)]
        public int regTimes { get; set; }

        /// <summary>
        /// regNo
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "regNo", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String regNo { get; set; }

        /// <summary>
        /// beginDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "beginDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.DateTime? beginDate { get; set; }

        /// <summary>
        /// manageLevel
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "manageLevel", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String manageLevel { get; set; }

        /// <summary>
        /// nextSfDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "nextSfDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.DateTime? nextSfDate { get; set; }

        /// <summary>
        /// recorder
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recorder", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.String recorder { get; set; }

        /// <summary>
        /// recordDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 9)]
        public System.DateTime recordDate { get; set; }

        /// <summary>
        /// status
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "status", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.Decimal status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 11)]
        public decimal sfId { get; set; }
        /// <summary>
        /// sfTimes
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfTimes", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 12)]
        public System.Decimal? sfTimes { get; set; }

        /// <summary>
        /// pgTimes
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "pgTimes", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 13)]
        public System.Decimal? pgTimes { get; set; }

        [DataMember]
        public string clientName { get; set; }
        [DataMember]
        public string sex { get; set; }
        [DataMember]
        public string age { get; set; }
        [DataMember]
        public string gradeName { get; set; }
        [DataMember]
        public string beginDateStr { get; set; }
        [DataMember]
        public string sfNextDateStr { get; set; }
        [DataMember]
        public string company { get; set; }
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
            public string clientNo = "clientNo";
            public string regTimes = "regTimes";
            public string regNo = "regNo";
            public string beginDate = "beginDate";
            public string manageLevel = "manageLevel";
            public string nextSfDate = "nextSfDate";
            public string recorder = "recorder";
            public string recordDate = "recordDate";
            public string status = "status";
            public string sfId = "sfId";
            public string sfTimes = "sfTimes";
            public string pgTimes = "pgTimes";
        }

    }
}
