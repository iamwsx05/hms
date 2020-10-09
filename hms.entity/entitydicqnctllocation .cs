using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using weCare.Core.Entity;

namespace Hms.Entity
{
    /// <summary>
    /// EntityDicQnCtlLocation
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "dicQnCtlLocation")]
    public class EntityDicQnCtlLocation : BaseDataContract
    {
        /// <summary>
        /// qnCtlId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "qnCtlFiledId", DbType = DbType.String, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.String qnCtlFiledId { get; set; }

        /// <summary>
        /// xmlData
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "xmlData", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.String xmlData { get; set; }

        /// <summary>
        /// qnId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "qnId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 3)]
        public decimal qnId { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string qnCtlFiledId = "qnCtlFiledId";
            public string xmlData = "xmlData";
            public string qnId = "qnId";
        }
    }
}
