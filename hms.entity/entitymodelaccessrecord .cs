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
        /// recId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.Decimal recId { get; set; }

        /// <summary>
        /// clientId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "clientNo", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.String clientNo { get; set; }

        /// <summary>
        /// regNo
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "regNo", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String regNo { get; set; }

        /// <summary>
        /// regTimes
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "regTimes", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.Int32 regTimes { get; set; }

        /// <summary>
        /// qnRecId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "qnRecId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.Decimal qnRecId { get; set; }

        /// <summary>
        /// recorder
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recorder", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String recorder { get; set; }

        /// <summary>
        /// recordDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.DateTime recordDate { get; set; }

        /// <summary>
        /// modifyDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modifyDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.DateTime? modifyDate { get; set; }

        /// <summary>
        /// status
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "status", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 9)]
        public System.Decimal? status { get; set; }

        /// <summary>
        /// confirmDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "confirmDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.DateTime? confirmDate { get; set; }

        [DataMember]
        public string clientName { get; set; }
        [DataMember]
        public string sex { get; set; }
        [DataMember]
        public string age { get; set; }
        [DataMember]
        public string gradeName { get; set; }
        [DataMember]
        public string company { get; set; }
        [DataMember]
        public string reportDateStr { get; set; }
        [DataMember]
        public string confirmDateStr { get; set; }
        [DataMember]
        public string strQnDate { get; set; }
        [DataMember]
        public string qnName { get; set; }
        [DataMember]
        public decimal qnId { get; set; }
        [DataMember]
        public string strQnSource { get; set; }
        [DataMember]
        public string qnData { get; set; }

        public string confirmState
        {
            get
            {
                if(status == 1)
                {
                    return "已审核";
                }

                return "";
            }
        }

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
            public string regNo = "regNo";
            public string regTimes = "regTimes";
            public string qnRecId = "qnRecId";
            public string recorder = "recorder";
            public string recordDate = "recordDate";
            public string modifyDate = "modifyDate";
            public string status = "status";
            public string confirmDate = "confirmDate";
        }
    }
}

