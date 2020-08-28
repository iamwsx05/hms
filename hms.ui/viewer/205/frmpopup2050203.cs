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
    /// 糖尿病-添加人员
    /// </summary>
    public partial class frmPopup2050203 : frmBasePopup
    {
        public frmPopup2050203()
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
        }


        #region var
        public bool isRefresh;
        #endregion

        #region methods
        internal EntityClientTnbResult GetRowObject()
        {
            if (this.gvData.FocusedRowHandle < 0) return null;
            return gvData.GetRow(gvData.FocusedRowHandle) as EntityClientTnbResult;
        }
        #endregion

        #region events

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

            if (lstClient != null)
            {
                foreach (var client in lstClient)
                {
                    clientNoStr += "'" + client.clientNo + "',";
                }
            }

            if (!string.IsNullOrEmpty(clientNoStr))
            {
                List<EntityClientTnbResult> lstTnbResult = null;
                clientNoStr = "(" + clientNoStr.TrimEnd(',') + ")";
                using (ProxyHms proxy = new ProxyHms())
                {
                    lstTnbResult = proxy.Service.GetClientTnbResults(clientNoStr);
                }

                foreach(var clientVo in lstClient)
                {
                    EntityClientTnbResult tnbResult = null;
                    if (lstTnbResult != null)
                        tnbResult = lstTnbResult.Find(r => r.clientNo == clientVo.clientNo && r.regTimes == clientVo.regTimes);
                    else
                        lstTnbResult = new List<EntityClientTnbResult>();
                    if (tnbResult == null)
                    {
                        tnbResult = new EntityClientTnbResult();
                        lstTnbResult.Add(tnbResult);
                    }
                    tnbResult = Function.MapperToModel(tnbResult, clientVo);
                }
                this.gcData.DataSource = lstTnbResult;
                this.gcData.RefreshDataSource();
            }
        }

        private void blbiAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EntityClientTnbResult gxyResult = GetRowObject();
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
                List<EntityTnbRecord> lstRecord = proxy.Service.GetTnbPatients(parms);

                if (lstRecord != null)
                {
                    DialogBox.Msg("人员已添加，请重新选择！");
                    return;
                }
            }

            EntityTnbRecord tnbRecorde = new EntityTnbRecord();
            tnbRecorde.clientNo = gxyResult.clientNo;
            tnbRecorde.regNo = gxyResult.regNo;
            tnbRecorde.beginDate = DateTime.Now;
            tnbRecorde.regTimes = gxyResult.regTimes;
            tnbRecorde.status = 0;
            decimal recId = 0;
            using (ProxyHms proxy = new ProxyHms())
            {
                int affect = proxy.Service.SaveTnbRecord(tnbRecorde, out recId);
                if (affect > 0)
                {
                    isRefresh = true;
                    tnbRecorde.recId = recId;
                    DialogBox.Msg("添加成功！");
                }
                else
                {
                    DialogBox.Msg("添加失败！");
                }
            }
        }
        #endregion
    }
}
