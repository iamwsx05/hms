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
    [Entity(TableName = "dicDietIngredient")]
    public class EntityDicDientIngredient : BaseDataContract
    {
        [DataMember]
        [Entity(FieldName = "id", DbType = DbType.String, IsPK = true, IsSeq = false, SerNo = 1)]
        public string id { get; set; }
        [DataMember]
        [Entity(FieldName = "foodSort", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 2)]
        public string foodSort { get; set; }
        [DataMember]
        [Entity(FieldName = "ingredientId", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 3)]
        public string ingredientId { get; set; }
        [DataMember]
        [Entity(FieldName = "names", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 4)]
        public string names { get; set; }
        [DataMember]
        [Entity(FieldName = "otherName", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 5)]
        public string otherName { get; set; }
        [DataMember]
        [Entity(FieldName = "remaks", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 6)]
        public string remaks { get; set; }
        [DataMember]
        [Entity(FieldName = "eatPercent", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 7)]
        public decimal eatPercent { get; set; }
        [DataMember]
        [Entity(FieldName = "water", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 8)]
        public decimal water { get; set; }
        [DataMember]
        [Entity(FieldName = "kCal", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 9)]
        public decimal kCal { get; set; }
        [DataMember]
        [Entity(FieldName = "kj", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 10)]
        public decimal kj { get; set; }
        [DataMember]
        [Entity(FieldName = "proteint", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 11)]
        public decimal proteint { get; set; }
        [DataMember]
        [Entity(FieldName = "fat", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 12)]
        public decimal fat { get; set; }
        [DataMember]
        [Entity(FieldName = "cho", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 13)]
        public decimal cho { get; set; }
        [DataMember]
        [Entity(FieldName = "brxxw", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 14)]
        public decimal brxxw { get; set; }
        [DataMember]
        [Entity(FieldName = "dgc", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 15)]
        public decimal dgc { get; set; }
        [DataMember]
        [Entity(FieldName = "ash", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 16)]
        public decimal ash { get; set; }
        [DataMember]
        [Entity(FieldName = "vitaminsA", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 17)]
        public decimal vitaminsA { get; set; }
        [DataMember]
        [Entity(FieldName = "thiamin", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 18)]
        public decimal thiamin { get; set; }
        [DataMember]
        [Entity(FieldName = "riboflavin", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 19)]
        public decimal riboflavin { get; set; }
        [DataMember]
        [Entity(FieldName = "niacin", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 20)]
        public decimal niacin { get; set; }
        [DataMember]
        [Entity(FieldName = "vitaminsC", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 21)]
        public decimal vitaminsC { get; set; }
        [DataMember]
        [Entity(FieldName = "vitaminsE", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 22)]
        public decimal vitaminsE { get; set; }
        [DataMember]
        [Entity(FieldName = "ca", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 23)]
        public decimal ca { get; set; }
        [DataMember]
        [Entity(FieldName = "p", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 24)]
        public decimal p { get; set; }
        [DataMember]
        [Entity(FieldName = "k", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 25)]
        public decimal k { get; set; }
        [DataMember]
        [Entity(FieldName = "na", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 26)]
        public decimal na { get; set; }
        [DataMember]
        [Entity(FieldName = "id", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 27)]
        public decimal mg { get; set; }
        [DataMember]
        [Entity(FieldName = "fe", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 28)]
        public decimal fe { get; set; }
        [DataMember]
        [Entity(FieldName = "zn", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 29)]
        public decimal zn { get; set; }
        [DataMember]
        [Entity(FieldName = "se", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 30)]
        public decimal se { get; set; }
        [DataMember]
        [Entity(FieldName = "cu", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 31)]
        public decimal cu { get; set; }
        [DataMember]
        [Entity(FieldName = "mn", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 32)]
        public decimal mn { get; set; }
        [DataMember]
        [Entity(FieldName = "i", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 33)]
        public decimal i { get; set; }
        [DataMember]
        [Entity(FieldName = "f", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 34)]
        public decimal f { get; set; }
        [DataMember]
        [Entity(FieldName = "cr", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 35)]
        public decimal cr { get; set; }
        [DataMember]
        [Entity(FieldName = "mu", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 36)]
        public decimal mu { get; set; }
        [DataMember]
        [Entity(FieldName = "id", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 37)]
        public decimal vitaminsD { get; set; }
        [DataMember]
        [Entity(FieldName = "vitaminsB6", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 38)]
        public decimal vitaminsB6 { get; set; }
        [DataMember]
        [Entity(FieldName = "vitaminsB12", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 39)]
        public decimal vitaminsB12 { get; set; }
        [DataMember]
        [Entity(FieldName = "vitaminsB5", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 40)]
        public decimal vitaminsB5 { get; set; }
        [DataMember]
        [Entity(FieldName = "vitaminsB9", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 41)]
        public decimal vitaminsB9 { get; set; }
        [DataMember]
        [Entity(FieldName = "danjian", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 42)]
        public decimal danjian { get; set; }
        [DataMember]
        [Entity(FieldName = "vitaminsH", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 43)]
        public decimal vitaminsH { get; set; }
        [DataMember]
        [Entity(FieldName = "bakfield1", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 44)]
        public string bakfield1 { get; set; }
        [DataMember]
        [Entity(FieldName = "bakfield2", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 45)]
        public string bakfield2 { get; set; }

        public string ingredietName
        {
            get
            {
                return names;
            }
        }

        /// <summary>
        /// 重量
        /// </summary>
        [DataMember]
        public decimal weight { get; set; }

        /// <summary>
        /// 原料分类
        /// </summary>
        [DataMember]
        public List<int> lstClassify { get; set; }

        public static EnumCols Columns = new EnumCols();

        public class EnumCols
        {
            public string id = "id";
            public string foodSort = "foodSort";
            public string ingredientId = "ingredientId";
            public string names = "names";
            public string otherName = "otherName";
            public string remaks = "remaks";
            public string eatPercent = "eatPercent";
            public string water = "water";
            public string kCal = "kCal";
            public string kj = "kj";
            public string proteint = "proteint";
            public string fat = "fat";
            public string cho = "cho";
            public string brxxw = "brxxw";
            public string dgc = "dgc";
            public string ash = "ash";
            public string vitaminsA = "vitaminsA";
            public string thiamin = "thiamin";
            public string riboflavin = "riboflavin";
            public string niacin = "niacin";
            public string vitaminsC = "vitaminsC";
            public string vitaminsE = "vitaminsE";
            public string ca = "ca";
            public string p = "p";
            public string k = "k";
            public string na = "na";
            public string mg = "mg";
            public string fe = "fe";
            public string zn = "zn";
            public string se = "se";
            public string cu = "cu";
            public string mn = "mn";
            public string i = "i";
            public string f = "f";
            public string cr = "cr";
            public string mu = "mu";
            public string vitaminsD = "vitaminsD";
            public string vitaminsB6 = "vitaminsB6";
            public string vitaminsB12 = "vitaminsB12";
            public string vitaminsB5 = "vitaminsB5";
            public string vitaminsB9 = "vitaminsB9";
            public string danjian = "danjian";
            public string vitaminsH = "vitaminsH";
            public string bakfield1 = "bakfield1";
            public string bakfield2 = "bakfield2";
        }
    }
}
