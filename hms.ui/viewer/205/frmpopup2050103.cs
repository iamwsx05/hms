using Common.Controls;
using Common.Utils;
using System;
using System.Collections.Generic;
using weCare.Core.Entity;
using weCare.Core.Utils;
using System.Text;
using System.Windows.Forms;
using Hms.Entity;
using System.Data;

namespace Hms.Ui
{
    /// <summary>
    /// 高血压-添加人员
    /// </summary>
    public partial class frmPopup2050103 : frmBasePopup
    {
        public frmPopup2050103()
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
        }

        #region var
        public bool isRefresh; 
        #endregion

        #region methods
        internal EntityClientGxyResult GetRowObject()
        {
            if (this.gvData.FocusedRowHandle < 0) return null;
            return gvData.GetRow(gvData.FocusedRowHandle) as EntityClientGxyResult;
        }
        #endregion

        #region event

        #region 查询
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blbiQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<EntityParm> parms = new List<EntityParm>();
            string search = this.txtClientName.Text;
            EntityParm vo = new EntityParm();
            vo.key = "search";
            vo.value = search;
            parms.Add(vo);
            List<EntityClientInfo> lstClient = null;
            string clientNoStr = string.Empty;
            if (!string.IsNullOrEmpty(search))
            {
                using (ProxyHms proxy = new ProxyHms())
                {
                    lstClient = proxy.Service.GetClientInfoAndRpt(parms);
                }
            }

            if(lstClient != null)
            {
                foreach(var client in lstClient)
                {
                    clientNoStr += "'" + client.clientNo + "',";
                }
            }

            if(!string.IsNullOrEmpty(clientNoStr))
            {
                List<EntityClientGxyResult> lstGxyResult = null;
                clientNoStr =  "(" + clientNoStr.TrimEnd(',') + ")";
                using (ProxyHms proxy = new ProxyHms())
                {
                    lstGxyResult = proxy.Service.GetClientGxyResults(clientNoStr);
                }

                if(lstGxyResult != null)
                {
                    for (int i = 0;i<lstGxyResult.Count;i++)
                    {
                        EntityClientGxyResult gxyResult = lstGxyResult[i];
                        EntityClientInfo clientVo = lstClient.Find(r => r.clientNo == gxyResult.clientNo && r.regTimes == gxyResult.regTimes);

                        if (clientVo == null)
                            continue;
                        gxyResult = Function.MapperToModel(gxyResult, clientVo);
                        if(!string.IsNullOrEmpty(gxyResult.gxy))
                        {
                            gxyResult.gxy += " mmHg";
                        }
                    }

                    this.gcData.DataSource = lstGxyResult;
                    this.gcData.RefreshDataSource();
                }
            }
        }
        #endregion

        #region 添加
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blbiAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EntityClientGxyResult gxyResult = GetRowObject();
            if (gxyResult == null)
                return;
            List<EntityParm> parms = new List<EntityParm>();
            EntityParm vo1 = new EntityParm();
            vo1.key = "clientNo";
            vo1.value = gxyResult.clientNo;
            parms.Add(vo1);

            EntityParm vo2 = new EntityParm();
            vo2.key = "regTimes";
            vo2.value = gxyResult.regTimes.ToString();
            parms.Add(vo2);

            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityGxyRecord> lstRecord = proxy.Service.GetGxyPatients(parms);

                if (lstRecord != null)
                {
                    DialogBox.Msg("人员已添加，请重新选择！");
                    return;
                }
            }

            EntityGxyRecord gxyRecorde = new EntityGxyRecord();
            gxyRecorde.clientNo = gxyResult.clientNo;
            gxyRecorde.regNo = gxyResult.regNo;
            gxyRecorde.beginDate = DateTime.Now;
            gxyRecorde.regTimes = gxyResult.regTimes;
            gxyRecorde.status = 0;
            decimal recId = 0;
            using (ProxyHms proxy = new ProxyHms())
            {
                int affect = proxy.Service.SaveGxyRecord(gxyRecorde, out recId);
                if(affect > 0)
                {
                    isRefresh = true;
                    gxyRecorde.recId = recId;
                    DialogBox.Msg("添加成功！");
                }
                else
                {
                    DialogBox.Msg("添加失败！");
                }
            }
        }
        #endregion

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
