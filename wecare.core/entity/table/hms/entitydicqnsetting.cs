using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace weCare.Core.Entity
{
    /// <summary>
    /// EntityDicQnSetting
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "dicQnSetting")]
    public class EntityDicQnSetting : BaseDataContract, IComparable
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
        public System.Int32 qnClassId { get; set; }

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
        public System.Int32 isParent { get; set; }

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
        [EntityAttribute(FieldName = "isEssential", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.Int32 isEssential { get; set; }

        /// <summary>
        /// status
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "status", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.Int32 status { get; set; }

        /// <summary>
        /// sortNo
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sortNo", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 9)]
        public System.Int32 sortNo { get; set; }

        /// <summary>
        /// comment
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "comment", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.String comment { get; set; }

        /// <summary>
        /// qnId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "qnId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 11)]
        public decimal qnId { get; set; }

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

        [DataMember]
        public string pyCode { get; set; }

        [DataMember]
        public string wbCode { get; set; }

        [DataMember]
        public string questName { get; set; }

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
            public string isMultipe = "isMultipe";
            public string typeName = "typeName";
            public string qnItemsDesc = "qnItemsDesc";
            public string essentialName = "essentialName";
            public string statusName = "statusName";
            public string pyCode = "pyCode";
            public string wbCode = "wbCode";
            public string qnId = "qnId";
        }

        /// <summary>
        /// 比较方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is EntityDicQnSetting)
            {
                return this.sortNo.CompareTo(((EntityDicQnSetting)obj).sortNo);
            }
            return 0;
        }
    }

    [DataContract, Serializable]
    public class EntityQnSetting : BaseDataContract
    {
        [DataMember]
        public decimal qnId { get; set; }
        [DataMember]
        public int isCheck { get; set; }

        
        [DataMember]
        public string className { get; set; }

        [DataMember]
        public string fieldId { get; set; }

        [DataMember]
        public string fieldName { get; set; }

        [DataMember]
        public string qnDesc { get; set; }

        [DataMember]
        public int sortNo { get; set; }

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
            public string isCheck = "isCheck";
            public string className = "className";
            public string fieldId = "fieldId";
            public string fieldName = "fieldName";
            public string qnDesc = "qnDesc";
            public string sortNo = "sortNo";
        }
    }
}
