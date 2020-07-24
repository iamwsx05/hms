using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using weCare.Core.Entity;

namespace Hms.Entity
{
    /// <summary>
    /// EntityClientModelResult
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "clientModelResult")]
    public class EntityClientModelResult : BaseDataContract
    {

        /// <summary>
        /// clientId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "clientId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.String clientId { get; set; }

        /// <summary>
        /// reportId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "reportId", DbType = DbType.AnsiString, IsPK = true, IsSeq = false, SerNo = 3)]
        public System.String reportId { get; set; }

        /// <summary>
        /// qnRecId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "qnRecId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 4)]
        public System.Decimal qnRecId { get; set; }

        /// <summary>
        /// psychologcaId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "psychologcaId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.String psychologcaId { get; set; }

        /// <summary>
        /// tcmphysiqueId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "tcmphysiqueId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String tcmphysiqueId { get; set; }

        /// <summary>
        /// modelId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modelId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 7)]
        public System.Decimal modelId { get; set; }

        /// <summary>
        /// modelResult
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modelResult", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.String modelResult { get; set; }

        /// <summary>
        /// modelScore
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modelScore", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 9)]
        public System.Decimal? modelScore { get; set; }

        /// <summary>
        /// resultDescription
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "resultDescription", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.String resultDescription { get; set; }

        /// <summary>
        /// bakField1
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bakField1", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 11)]
        public System.String bakField1 { get; set; }

        /// <summary>
        /// bakField2
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bakField2", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 12)]
        public System.String bakField2 { get; set; }

        /// <summary>
        /// createDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "createDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 13)]
        public System.DateTime? createDate { get; set; }

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
            public string reportId = "reportId";
            public string qnRecId = "qnRecId";
            public string psychologcaId = "psychologcaId";
            public string tcmphysiqueId = "tcmphysiqueId";
            public string modelId = "modelId";
            public string modelResult = "modelResult";
            public string modelScore = "modelScore";
            public string resultDescription = "resultDescription";
            public string bakField1 = "bakField1";
            public string bakField2 = "bakField2";
            public string createDate = "createDate";
        }


    }
}
