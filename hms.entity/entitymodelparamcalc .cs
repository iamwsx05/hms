using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using weCare.Core.Entity;

namespace Hms.Entity
{
    /// <summary>
    /// EntityModelParamCalc
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "modelParamCalc")]
    public class EntityModelParamCalc : BaseDataContract
    {

        /// <summary>
        /// modelId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modelId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.Decimal modelId { get; set; }

        /// <summary>
        /// clientId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "clientId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.String clientId { get; set; }

        /// <summary>
        /// regNo
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "regNo", DbType = DbType.AnsiString, IsPK = true, IsSeq = false, SerNo = 3)]
        public System.String regNo { get; set; }

        /// <summary>
        /// qnRecId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "qnRecId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 4)]
        public decimal qnRecId { get; set; }

        /// <summary>
        /// psychologyId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "psychologyId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.String psychologyId { get; set; }

        /// <summary>
        /// traMedicalId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "traMedicalId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String traMedicalId { get; set; }

        /// <summary>
        /// paramNo
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "paramNo", DbType = DbType.AnsiString, IsPK = true, IsSeq = false, SerNo = 7)]
        public System.String paramNo { get; set; }

        /// <summary>
        /// paramValue
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "paramValue", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.String paramValue { get; set; }

        /// <summary>
        /// calcScore
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "calcScore", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 9)]
        public System.Decimal? calcScore { get; set; }

        /// <summary>
        /// recordDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.DateTime? recordDate { get; set; }

        /// <summary>
        /// paramName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "paramName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 11)]
        public System.String paramName { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string modelId = "modelId";
            public string clientId = "clientId";
            public string regNo = "regNo";
            public string qnRecId = "qnRecId";
            public string psychologyId = "psychologyId";
            public string traMedicalId = "traMedicalId";
            public string paramNo = "paramNo";
            public string paramValue = "paramValue";
            public string calcScore = "calcScore";
            public string recordDate = "recordDate";
            public string paramName = "paramName";
        }

    }
}
