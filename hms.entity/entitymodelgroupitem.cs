using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using weCare.Core.Entity;

namespace Hms.Entity
{
    /// <summary>
    /// EntityModelGroupItem
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "modelGroupItem")]
    public class EntityModelGroupItem : BaseDataContract
    {
        /// <summary>
        /// id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "id", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 1)]
        public System.Int32 id { get; set; }

        /// <summary>
        /// modelId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modelId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.Decimal modelId { get; set; }

        /// <summary>
        /// paramType
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "paramType", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.Decimal paramType { get; set; }

        /// <summary>
        /// paramNo
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "paramNo", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String paramNo { get; set; }

        /// <summary>
        /// paramName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "paramName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.String paramName { get; set; }

        /// <summary>
        /// orderNum
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "orderNum", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.Int32? orderNum { get; set; }

        /// <summary>
        /// isMain
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "isMain", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 11)]
        public System.Int32 isMain { get; set; }

        /// <summary>
        /// pointId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "pointId", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 12)]
        public System.Int32 pointId { get; set; }

        /// <summary>
        /// isFamily
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "isFamily", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 13)]
        public int isFamily { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string id = "id";
            public string modelId = "modelId";
            public string paramType = "paramType";
            public string paramNo = "paramNo";
            public string paramName = "paramName";
            public string orderNum = "orderNum";
            public string isMain = "isMain";
            public string pointId = "pointId";
            public string isFamily = "isFamily";
        }
    }
}
