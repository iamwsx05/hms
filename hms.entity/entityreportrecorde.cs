using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Hms.Entity
{
    [DataContract, Serializable]
    [Entity(TableName = "reportRecorde")]
    public class EntityReportRecorde : BaseDataContract
    {
        [DataMember]
        [Entity(FieldName = "id", DbType = DbType.String, IsPK = true, IsSeq = false, SerNo = 1)]
        public string id { get; set; }
        [DataMember]
        [Entity(FieldName = "reportName", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 2)]
        public string reportName { get; set; }
        [DataMember]
        [Entity(FieldName = "clientId", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 3)]
        public string clientId { get; set; }
        [DataMember]
        [Entity(FieldName = "reportId", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 4)]
        public string reportId { get; set; }
        [DataMember]
        [Entity(FieldName = "conventionId", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 5)]
        public string conventionId { get; set; }
        [DataMember]
        [Entity(FieldName = "psychologcaId", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 6)]
        public string psychologcaId { get; set; }
        [DataMember]
        [Entity(FieldName = "tcmpysiqueId", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 7)]
        public string tcmpysiqueId { get; set; }
        [DataMember]
        [Entity(FieldName = "suditState", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 8)]
        public string suditState { get; set; }
        [DataMember]
        [Entity(FieldName = "reportStatc", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 9)]
        public string reportStatc { get; set; }
        [DataMember]
        [Entity(FieldName = "needTime", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 10)]
        public string needTime { get; set; }
        [DataMember]
        [Entity(FieldName = "upTag", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 11)]
        public string upTag { get; set; }
        [DataMember]
        [Entity(FieldName = "bakfield1", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 12)]
        public string bakfield1 { get; set; }
        [DataMember]
        [Entity(FieldName = "bakfield2", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 13)]
        public string bakfield2 { get; set; }
        [DataMember]
        [Entity(FieldName = "createDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 14)]
        public DateTime?  createDate { get; set; }
        [DataMember]
        [Entity(FieldName = "creator", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 15)]
        public string creator { get; set; }
        [DataMember]
        [Entity(FieldName = "createName", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 16)]
        public string createName { get; set; }
        [DataMember]
        [Entity(FieldName = "regTimes", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 17)]
        public int regTimes { get; set; }

        public static EnumCols Columns = new EnumCols();

        public class EnumCols
        {
            public string id = "id";
            public string reportName = "reportName";
            public string clientId = "clientId";
            public string reportId = "reportId";
            public string conventionId = "conventionId";
            public string psychologcaId = "psychologcaId";
            public string tcmpysiqueId = "tcmpysiqueId";
            public string suditState = "suditState";
            public string reportStatc = "reportStatc";
            public string needTime = "needTime";
            public string upTag = "upTag";
            public string bakfield1 = "bakfield1";
            public string bakfield2 = "bakfield2";
            public string createDate = "createDate";
            public string creator = "creator";
            public string createName = "createName";
            public string regTimes = "regTimes";

        }
    }
}
