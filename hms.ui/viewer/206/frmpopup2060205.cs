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

namespace Hms.Ui
{
    public partial class frmPopup2060205 : frmBasePopup
    {
        public frmPopup2060205(List<EntityDietTreatment> _lstChoose)
        {
            InitializeComponent();
            lstChoose = _lstChoose;
        }

        #region var
        public List<EntityDietTreatment> data { get; set; }
        public List<EntityDietTreatment> lstChoose { get; set; }

        public bool isRefresh { get; set; }

        #endregion

        #region methods
        void Init()
        {
            data = null;
            using (ProxyHms proxy = new ProxyHms())
            {
                data = proxy.Service.GetDietTreatment();
            }
            this.gcData.DataSource = data;
            this.gcData.RefreshDataSource();

            if (lstChoose != null && gvData.RowCount > 0)
            {
                foreach (var vo in lstChoose)
                {
                    for (int i = 0; i < gvData.RowCount; i++)
                    {
                        EntityDietTreatment dp = gvData.GetRow(i) as EntityDietTreatment;
                        if (dp.id == vo.id)
                        {
                            gvData.SelectRow(i);
                        }
                    }
                }
            }
        }
        #endregion

        #region events
        private void frmPopup2060205_Load(object sender, EventArgs e)
        {
            Init();
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            lstChoose = new List<EntityDietTreatment>();
            if (gvData.RowCount > 0)
            {
                for (int i = 0; i < gvData.RowCount; i++)
                {
                    if (gvData.IsRowSelected(i))
                    {
                        lstChoose.Add(gvData.GetRow(i) as EntityDietTreatment);
                    }
                }
                isRefresh = true;
            }
        }

        #endregion
    }
}
