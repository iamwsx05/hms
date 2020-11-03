using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using weCare.Core.Entity;

namespace Hms.Entity
{
    /// <summary>
    /// EntityDicQnSummary
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "dicQnSummary")]
    public class EntityDicQnSummary : BaseDataContract, IComparable
    {
        /// <summary>
        /// fieldId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "fieldId", DbType = DbType.AnsiString, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.String fieldId { get; set; }

        /// <summary>
        /// qnClassId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "qnClassId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.Decimal qnClassId { get; set; }

        /// <summary>
        /// typeId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "typeId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String typeId { get; set; }

        /// <summary>
        /// fieldName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "fieldName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String fieldName { get; set; }

        /// <summary>
        /// isParent
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "isParent", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.Decimal isParent { get; set; }

        /// <summary>
        /// parentFieldId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "parentFieldId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String parentFieldId { get; set; }

        /// <summary>
        /// isEssential
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "isEssential", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 7)]
        public int isEssential { get; set; }

        /// <summary>
        /// status
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "status", DbType = DbType.UInt16, IsPK = false, IsSeq = false, SerNo = 8)]
        public int status { get; set; }

        /// <summary>
        /// sortNo
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sortNo", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 9)]
        public int sortNo { get; set; }

        /// <summary>
        /// comment
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "comment", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.String comment { get; set; }

        [DataMember]
        public string pyCode { get; set; }
        [DataMember]
        public string wbCode { get; set; }


        [DataMember]
        public Int32 isMultipe
        {
            get
            {
                return typeId == "2" ? 1 : 0;
            }
            set {; }
        }

        [DataMember]
        public string typeName
        {
            get
            {
                if (typeId == "1")
                    return "单选题";
                else if (typeId == "2")
                    return "多选题";
                else if (typeId == "3")
                    return "填空题";
                else
                    return "未知类型";
            }
            set {; }
        }


        [DataMember]
        public string qnItemsDesc { get; set; }

        [DataMember]
        public string essentialName
        {
            get
            {
                return status == 1 ? "是" : "否";
            }
            set {; }
        }

        [DataMember]
        public string statusName
        {
            get
            {
                return status == 1 ? "启用" : "停用";
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
            public string fieldId = "fieldId";
            public string qnClassId = "qnClassId";
            public string typeId = "typeId";
            public string fieldName = "fieldName";
            public string isParent = "isParent";
            public string parentFieldId = "parentFieldId";
            public string isEssential = "isEssential";
            public string status = "status";
            public string sortNo = "sortNo";
            public string comment = "comment";
        }

        /// <summary>
        /// 比较方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is EntityDicQnSummary)
            {
                return this.sortNo.CompareTo(((EntityDicQnSummary)obj).sortNo);
            }
            return 0;
        }
    }
}
