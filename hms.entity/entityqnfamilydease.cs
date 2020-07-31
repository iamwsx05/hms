using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    /// <summary>
    /// EntityQnFamilyDease
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "qnFamilyDease")]

    public class EntityQnFamilyDease : BaseDataContract
    {
        /// <summary>
        /// fieldId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "fieldId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 1)]
        public System.String fieldId { get; set; }

        /// <summary>
        /// fieldName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "fieldName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.String fieldName { get; set; }

        /// <summary>
        /// parentFieldId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "parentFieldId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String parentFieldId { get; set; }

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
            public string fieldName = "fieldName";
            public string parentFieldId = "parentFieldId";
        }

    }
}
