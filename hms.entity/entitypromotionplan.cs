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
    /// EntityPromotionPlan
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "promotionPlan")]
    public class EntityPromotionPlan : BaseDataContract
    {
        /// <summary>
        /// id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "id", DbType = DbType.AnsiString, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.String id { get; set; }

        /// <summary>
        /// clientId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "clientId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String clientId { get; set; }

        /// <summary>
        /// planType
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "planType", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String planType { get; set; }

        /// <summary>
        /// planDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "planDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.DateTime? planDate { get; set; }

        /// <summary>
        /// executeTime
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "executeTime", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.DateTime? executeTime { get; set; }

        /// <summary>
        /// executeUserId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "executeUserId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.String executeUserId { get; set; }

        /// <summary>
        /// executeUserName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "executeUserName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.String executeUserName { get; set; }

        /// <summary>
        /// planState
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "planState", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 9)]
        public System.String planState { get; set; }

        /// <summary>
        /// auditState
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "auditState", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.String auditState { get; set; }

        /// <summary>
        /// planWay
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "planWay", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 11)]
        public System.String planWay { get; set; }

        /// <summary>
        /// planContent
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "planContent", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 12)]
        public System.String planContent { get; set; }

        /// <summary>
        /// planRemind
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "planRemind", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 13)]
        public System.String planRemind { get; set; }

        /// <summary>
        /// recordWay
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordWay", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 14)]
        public System.String recordWay { get; set; }

        /// <summary>
        /// recordContent
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordContent", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 15)]
        public System.String recordContent { get; set; }

        /// <summary>
        /// ignorPlan
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "ignorPlan", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 16)]
        public System.String ignorPlan { get; set; }

        /// <summary>
        /// planSource
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "planSource", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 17)]
        public System.String planSource { get; set; }

        /// <summary>
        /// planTemplateName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "planTemplateName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 18)]
        public System.String planTemplateName { get; set; }

        /// <summary>
        /// specialSituation
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "specialSituation", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 19)]
        public System.String specialSituation { get; set; }

        /// <summary>
        /// planVisitRecord
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "planVisitRecord", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 20)]
        public System.String planVisitRecord { get; set; }

        /// <summary>
        /// planAssortLevel
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "planAssortLevel", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 21)]
        public System.String planAssortLevel { get; set; }

        /// <summary>
        /// planPleasedLevel
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "planPleasedLevel", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 22)]
        public System.String planPleasedLevel { get; set; }

        /// <summary>
        /// audioType
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "audioType", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 23)]
        public System.String audioType { get; set; }

        /// <summary>
        /// audioTag
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "audioTag", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 24)]
        public System.String audioTag { get; set; }

        /// <summary>
        /// upTag
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "upTag", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 25)]
        public System.String upTag { get; set; }

        /// <summary>
        /// bakfield1
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bakfield1", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 26)]
        public System.String bakfield1 { get; set; }

        /// <summary>
        /// bakfield2
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bakfield2", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 27)]
        public System.String bakfield2 { get; set; }

        /// <summary>
        /// createDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "createDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 28)]
        public System.DateTime? createDate { get; set; }

        /// <summary>
        /// createId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "createId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 29)]
        public System.String createId { get; set; }

        /// <summary>
        /// createName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "createName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 30)]
        public System.String createName { get; set; }

        /// <summary>
        /// modifyDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modifyDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 31)]
        public System.DateTime? modifyDate { get; set; }

        /// <summary>
        /// modifyId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modifyId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 32)]
        public System.String modifyId { get; set; }

        /// <summary>
        /// modifyName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "modifyName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 33)]
        public System.String modifyName { get; set; }

        /// <summary>
        /// reportId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "regTimes", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 34)]
        public int regTimes { get; set; }

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
            public string clientId = "clientId";
            public string planType = "planType";
            public string planDate = "planDate";
            public string executeTime = "executeTime";
            public string executeUserId = "executeUserId";
            public string executeUserName = "executeUserName";
            public string planState = "planState";
            public string auditState = "auditState";
            public string planWay = "planWay";
            public string planContent = "planContent";
            public string planRemind = "planRemind";
            public string recordWay = "recordWay";
            public string recordContent = "recordContent";
            public string ignorPlan = "ignorPlan";
            public string planSource = "planSource";
            public string planTemplateName = "planTemplateName";
            public string specialSituation = "specialSituation";
            public string planVisitRecord = "planVisitRecord";
            public string planAssortLevel = "planAssortLevel";
            public string planPleasedLevel = "planPleasedLevel";
            public string audioType = "audioType";
            public string audioTag = "audioTag";
            public string upTag = "upTag";
            public string bakfield1 = "bakfield1";
            public string bakfield2 = "bakfield2";
            public string createDate = "createDate";
            public string createId = "createId";
            public string createName = "createName";
            public string modifyDate = "modifyDate";
            public string modifyId = "modifyId";
            public string modifyName = "modifyName";
            public string regTimes = "regTimes";
        }
    }

}
