using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace weCare.Core.Entity
{
    /// <summary>
    /// EntityTnbSf
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "tnbSf")]
    public class EntityTnbSf : BaseDataContract
    {
        /// <summary>
        /// sfId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.Decimal sfId { get; set; }

        /// <summary>
        /// recId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.Decimal recId { get; set; }

        /// <summary>
        /// sfMethod
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfMethod", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String sfMethod { get; set; }

        /// <summary>
        /// sfClass
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfClass", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String sfClass { get; set; }

        /// <summary>
        /// sfDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.DateTime sfDate { get; set; }

        /// <summary>
        /// sfRecorder
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfRecorder", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String sfRecorder { get; set; }

        /// <summary>
        /// sfStatus
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfStatus", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.Decimal sfStatus { get; set; }

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
        public string sfDateStr { get; set; }
        [DataMember]
        public string sfData { get; set; }


        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string sfId = "sfId";
            public string recId = "recId";
            public string sfMethod = "sfMethod";
            public string sfClass = "sfClass";
            public string sfDate = "sfDate";
            public string sfRecorder = "sfRecorder";
            public string sfStatus = "sfStatus";
            public string recordDate = "recordDate";
        }
    }
}