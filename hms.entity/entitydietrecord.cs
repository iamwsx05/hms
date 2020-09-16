using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{/// <summary>
 /// EntityDietRecord
 /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "dietRecord")]
    public class EntityDietRecord : BaseDataContract
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
        [EntityAttribute(FieldName = "regTimes", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.Int32? regTimes { get; set; }

        /// <summary>
        /// regNo
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "regNo", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String regNo { get; set; }

        /// <summary>
        /// day1
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "day1", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 5)]
        public int day1 { get; set; }
        /// <summary>
        /// day2
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "day2", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 6)]
        public int day2 { get; set; }
        /// <summary>
        /// day3
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "day3", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 7)]
        public int day3 { get; set; }
        /// <summary>
        /// day4
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "day4", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 8)]
        public int day4 { get; set; }
        /// <summary>
        /// day5
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "day5", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 9)]
        public int day5 { get; set; }
        /// <summary>
        /// day6
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "day6", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 10)]
        public int day6 { get; set; }

        /// <summary>
        /// day7
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "day7", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 11)]
        public int day7 { get; set; }

        /// <summary>
        /// recorder
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recorder", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 12)]
        public System.String recorder { get; set; }

        /// <summary>
        /// recordDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 13)]
        public System.DateTime recordDate { get; set; }

        /// <summary>
        /// status
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "status", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 14)]
        public System.Decimal status { get; set; }
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
        public string recordDateStr { get; set; }


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
            public string day1 = "day1";
            public string day2 = "day2";
            public string day3 = "day3";
            public string day4 = "day4";
            public string day5 = "day5";
            public string day6 = "day6";
            public string day7 = "day7";
            public string recorder = "recorder";
            public string recordDate = "recordDate";
            public string status = "status";
        }
    }
}
