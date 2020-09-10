using Common.Controls;
using Hms.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace Hms.Ui
{
    public partial class frmpopup2060202 : frmBasePopup
    {
        public frmpopup2060202(List<EntityClientInfo> _lstClientSelect)
        {
            InitializeComponent();
            lstClientSelect = _lstClientSelect;
        }

        public List<EntityClientInfo> lstClientSelect { get; set; }

        #region events
        private void blbClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
       

        private void blbSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void blbAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstClientSelect == null)
                lstClientSelect = new List<EntityClientInfo>();
            if (gvData.FocusedRowHandle >= 0 )
            {
                EntityClientInfo vo = gvData.GetRow(gvData.FocusedRowHandle) as EntityClientInfo;

                if(vo != null)
                {
                    if(lstClientSelect.Any(r=>r.clientNo == vo.clientNo && r.regTimes== vo.regTimes))
                    {
                        DialogBox.Msg("已添加");
                    }
                    else
                    {
                        lstClientSelect.Add(vo);
                    }
                }
            }
        }
        #endregion
    }
}
