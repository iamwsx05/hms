﻿using Common.Entity;
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
    public class Biz203 :IDisposable
    {
        #region 个人报告

        #region 列表
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityDisplayClientRpt> GetClientReports(List<EntityParm> parms = null)
        {
            List<EntityDisplayClientRpt> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select b.id,
                            b.clientNo, 
                            b.clientName,
                            b.birthday,
                            b.gender,
                            c.gradeName,
                            b.company,
                            d.id,
                            d.reportNo,
                            d.reportDate,
                            a.reportStatc ,
                            a.suditState 
                            from reportRecorde a 
                            left join clientInfo b 
                            on a.clientId = b.id
                            left join userGrade c
                            on b.gradeId = c.id
                            left join reportInfo d
                            on a.reportId = d.id  ";

            DataTable dt = svc.GetDataTable(Sql);
           
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityDisplayClientRpt>();
                EntityDisplayClientRpt vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityDisplayClientRpt();
                    vo.clientName = dr["clientName"].ToString();
                    vo.clientNo = dr["clientNo"].ToString();
                    vo.gender = Function.Int(dr["gender"]);
                    vo.reportNo = dr["reportNo"].ToString();
                    vo.reportDate = Function.Datetime(dr["reportDate"]).ToString("yyyy-MM-dd");
                    vo.company = dr["company"].ToString();
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.age = Function.CalcAge(Function.Datetime(dr["birthday"]));
                    vo.reportStatc = Function.Int(dr["reportStatc"]);
                    vo.suditState = Function.Int(dr["suditState"]);
                    vo.reportCount = 1;
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