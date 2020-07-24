using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    [DataContract, Serializable]
    [EntityAttribute(TableName = "modelAccessRecord")]
    public class EntitymModelAccessRecord : BaseDataContract
    {
        /// <summary>
        /// clientId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "clientId", DbType = DbType.AnsiString, IsPK = true, IsSeq = false, SerNo = 2)]
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
        /// recorder
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recorder", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.String recorder { get; set; }

        /// <summary>
        /// recordDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.DateTime recordDate { get; set; }

        /// <summary>
        /// status
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "status", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.Decimal? status { get; set; }

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
            public string recorder = "recorder";
            public string recordDate = "recordDate";
            public string status = "status";
        }


    }
}
