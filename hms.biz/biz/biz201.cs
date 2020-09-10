using Common.Entity;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Transactions;
using Hms.Entity;

namespace Hms.Biz
{
    public class Biz201 : IDisposable
    {
        #region 我的客户

        #region 客户列表
        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityClientInfo> GetClientInfoAndRpt(List<EntityParm> parms)
        {
            List<EntityClientInfo> data = null;
            List<EntityReportRecorde> lstReportRecord = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select distinct
                            b.reportId,
                            a.clientNo,
                            a.clientName,
                            a.gender,
                            a.mobile,
                            a.birthday,
                            a.cardno,
                            a.gradeName,
                            a.company,
                            a.address,
                            a.regTimes,
                            a.createDate
                            from V_ClientInfo a
                            left join V_RportRecord b
                            on a.clientNo = b.clientNo and a.regTimes = b.regTimes
                             where (a.clientNo is not null or a.clientNo <> '')";
            List<IDataParameter> lstParm = new List<IDataParameter>();
            string strSub = string.Empty;
            if (parms != null)
            {
                foreach (var po in parms)
                {
                    switch (po.key)
                    {
                        case "search":
                            strSub += " and (a.clientName like '%" + po.value + "%' or a.clientNo like '" + po.value + "%' )";
                            break;
                        case "genDate":
                            IDataParameter parm1 = svc.CreateParm();
                            parm1.Value = po.value.Split('|')[0] ;
                            lstParm.Add(parm1);
                            IDataParameter parm2 = svc.CreateParm();
                            parm2.Value = po.value.Split('|')[1];
                            lstParm.Add(parm2);
                            strSub += " and (a.createDate between ? and ? )";
                            break;
                        case "dw":
                            strSub += " and (a.company like '%" + po.value + "%' )";
                            break;
                        default:
                            break;
                    }
                }
            }
            Sql += strSub;
            DataTable dt = svc.GetDataTable(Sql, lstParm.ToArray());
            string strClinetNo = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityClientInfo>();
                EntityClientInfo vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityClientInfo();
                    //vo.id = dr["id"].ToString();
                    //vo.gradeId = dr["gradeId"].ToString();
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.clientNo = dr["clientNo"].ToString();
                    vo.clientName = dr["clientName"].ToString();
                    vo.age = Function.CalcAge(Function.Datetime(dr["birthday"]));
                    vo.gender = Function.Int(dr["gender"]);
                    if (vo.gender == 1)
                        vo.sex = "男";
                    if (vo.gender == 2)
                        vo.sex = "女";
                    vo.birthday = dr["birthday"].ToString();
                    vo.mobile = dr["mobile"].ToString();
                    //vo.telephone = dr["telephone"].ToString();
                    //vo.email = dr["email"].ToString();
                    //vo.qq = dr["qq"].ToString();
                    vo.company = dr["company"].ToString();
                    //vo.regionId = dr["regionId"].ToString();
                    vo.address = dr["address"].ToString();
                    ///vo.booldType = Function.Int(dr["booldType"]);
                    //vo.profession = Function.Int(dr["profession"]);
                    //vo.marriage = Function.Int(dr["marriage"]);
                    //vo.ehtnicGroup = Function.Int(dr["ehtnicGroup"]);
                    //vo.eduationLevel = Function.Int(dr["eduationLevel"]);
                    //vo.clientTag = dr["clientTag"].ToString();
                    //vo.contactName = dr["contactName"].ToString();
                    //vo.contactNameMobile = dr["contactNameMobile"].ToString();
                    //vo.clientRemarks = dr["clientRemarks"].ToString();
                    //vo.dataSource = dr["dataSource"].ToString();
                    //vo.upTag = dr["upTag"].ToString();
                    //vo.serverDate = Function.Datetime(dr["serverDate"]);
                    //vo.bakfileld1 = dr["bakfileld1"].ToString();
                    //vo.bakfileld2 = dr["bakfileld2"].ToString();
                    vo.createDate = Function.Datetime(dr["createDate"]);
                    //vo.creatorId = dr["creatorId"].ToString();
                    //vo.createName = dr["createName"].ToString();
                    //vo.modifyDate = Function.Datetime(dr["modifyDate"]);
                    //vo.modifyId = dr["modifyId"].ToString();
                    //vo.modifyName = dr["modifyName"].ToString();
                    vo.regNo = dr["reportId"].ToString();
                    vo.regTimes = Function.Int(dr["regTimes"]);
                    vo.reportCount = Function.Int(dr["regTimes"]);

                    //strClinetNo += "'" + vo.clientNo + "',";
                    data.Add(vo);
                }
            }

            //if (string.IsNullOrEmpty(strClinetNo))
            //    return data;

            //strClinetNo = "(" + strClinetNo.TrimEnd(',') + ")";

            //Sql = @"select  reportId,
            //                 regTimes,
            //                 clientNo
	           //             from V_RportRecord a  where a.clientNo in " + strClinetNo;
            //DataTable dtReport = svc.GetDataTable(Sql);
            //if (dtReport != null && dtReport.Rows.Count > 0)
            //{
            //    lstReportRecord = new List<EntityReportRecorde>();
            //    EntityReportRecorde vo = null;
            //    foreach (DataRow dr in dtReport.Rows)
            //    {
            //        vo = new EntityReportRecorde();
            //        vo.clientId = dr["clientNo"].ToString();
            //        vo.reportId = dr["reportId"].ToString();
            //        lstReportRecord.Add(vo);
            //    }
            //}
            //if(lstReportRecord != null)
            //{
            //    foreach (var vo in data)
            //    {
            //        vo.reportCount = lstReportRecord.FindAll(r => r.clientId == vo.clientNo).Count;
                    
            //    }
            //}
            
            return data;
        }
        #endregion

        #region
        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityClientInfo> GetClientInfos(string search = null)
        {
            List<EntityClientInfo> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;

            Sql = @"select distinct
                            a.clientNo,
                            a.clientName,
                            a.gender,
                            a.birthday,
                            a.cardno,
                            a.gradeName,
                            a.company,
                            a.address,
                            a.createDate
                            from V_ClientInfo a ";

            if(!string.IsNullOrEmpty(search))
            {
                string strSub = " where (a.clientName like '%" + search + "%' or a.clientNo like '" + search + "%' )";
                Sql += strSub;
            }
            DataTable dt = svc.GetDataTable(Sql);
            
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityClientInfo>();
                EntityClientInfo vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityClientInfo();
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.clientNo = dr["clientNo"].ToString();
                    vo.clientName = dr["clientName"].ToString();
                    vo.age = Function.CalcAge(Function.Datetime(dr["birthday"]));
                    vo.gender = Function.Int(dr["gender"]);
                    if (vo.gender == 1)
                        vo.sex = "男";
                    if (vo.gender == 2)
                        vo.sex = "女";
                    vo.birthday = dr["birthday"].ToString();
                    vo.company = dr["company"].ToString();
                    vo.address = dr["address"].ToString();
                    vo.cardNo = dr["cardno"].ToString();
                    vo.createDate = Function.Datetime(dr["createDate"]);

                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 类别列表
        /// <summary>
        /// 类别列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityUserGrade> GetUserGrades()
        {
            List<EntityUserGrade> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select  a.id,
                            a.gradeName,
                            a.reportName,
                            a.severPrice,
                            a.serverTime,
                            a.description
                            
                      from userGrade a ";
            DataTable dt = svc.GetDataTable(Sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityUserGrade>();
                EntityUserGrade vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityUserGrade();
                    vo.id = dr["id"].ToString();
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.reportName = dr["reportName"].ToString();
                    vo.severPrice = dr["severPrice"].ToString();
                    vo.serverTime = dr["serverTime"].ToString();
                    vo.description = dr["description"].ToString();
                    
                    data.Add(vo);
                } 
            }
            return data;
        }
        #endregion

        #endregion

        #region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
