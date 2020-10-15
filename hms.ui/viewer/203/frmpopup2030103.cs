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
    /// 个人报告-添加人员
    /// </summary>
    public partial class frmPopup2030103 : frmBasePopup
    {
        public frmPopup2030103()
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
        }

        #region var
        public bool isRefresh; 
        #endregion

        #region methods
        internal EntityClientInfo GetRowObject()
        {
            if (this.gvData.FocusedRowHandle < 0) return null;
            return gvData.GetRow(gvData.FocusedRowHandle) as EntityClientInfo;
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

            this.gcData.DataSource = lstClient;
            this.gcData.RefreshDataSource();
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
            EntityClientInfo client = GetRowObject();
            if (client == null)
                return;
            List<EntityParm> parms = new List<EntityParm>();
            EntityParm vo1 = new EntityParm();
            vo1.key = "clientNo";
            vo1.value = client.clientNo;
            parms.Add(vo1);

            EntityParm vo2 = new EntityParm();
            vo2.key = "regTimes";
            vo2.value = client.regTimes.ToString();
            parms.Add(vo2);

            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntitymModelAccessRecord> lstRecord = proxy.Service.GetModelAccessRec(parms);

                if (lstRecord != null)
                {
                    DialogBox.Msg("人员已添加，请重新选择！");
                    return;
                }
            }

            EntitymModelAccessRecord mdAccessRec = new EntitymModelAccessRecord();
            mdAccessRec.clientNo = client.clientNo;
            mdAccessRec.regNo = client.regNo;
            mdAccessRec.recordDate = DateTime.Now;
            mdAccessRec.regTimes = client.regTimes;
            mdAccessRec.status = 0;
            decimal recId = 0;
            using (ProxyHms proxy = new ProxyHms())
            {
                int affect = proxy.Service.SaveMdAccessRecord(mdAccessRec, out recId);
                if(affect > 0)
                {
                    isRefresh = true;
                    mdAccessRec.recId = recId;
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
