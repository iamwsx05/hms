using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace weCare.Core.Entity
{
    /// <summary>
    /// EntityDicQnMain
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "dicQnMain")]
    public class EntityDicQnMain : BaseDataContract
    {
        /// <summary>
        /// qnId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "qnId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.Decimal qnId { get; set; }

        /// <summary>
        /// qnName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "qnName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.String qnName { get; set; }

        /// <summary>
        /// classId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "classId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.Int32 classId { get; set; }

        /// <summary>
        /// qnDesc
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "qnDesc", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String qnDesc { get; set; }

        /// <summary>
        /// creatorId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "creatorId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.String creatorId { get; set; }

        /// <summary>
        /// creatDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "creatDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.DateTime creatDate { get; set; }

        /// <summary>
        /// hazardFlag
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "hazardFlag", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.Int32 hazardFlag { get; set; }

        /// <summary>
        /// status
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "status", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.Int32 status { get; set; }

        public string className
        {
            get
            {
                if (classId == 1)
                    return "常规问卷";
                else if (classId == 2)
                    return "自定义问卷";
                else
                    return "其他问卷";
            }
            set {; }
        }

        public string statusName
        {
            get
            {
                if (status == 1)
                    return "启用";
                else
                    return "停用";
            }
            set {; }
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
            public string qnId = "qnId";
            public string qnName = "qnName";
            public string classId = "classId";
            public string qnDesc = "qnDesc";
            public string creatorId = "creatorId";
            public string creatDate = "creatDate";
            public string hazardFlag = "hazardFlag";
            public string status = "status";

            public string className = "className";
            public string statusName = "statusName";
        }
    }
}
