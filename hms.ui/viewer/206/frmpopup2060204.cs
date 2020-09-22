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
    public partial class frmPopup2060204 : frmBasePopup
    {
        public frmPopup2060204(List<EntityDietPrinciple> _lstChoose)
        {
            InitializeComponent();
            lstChoose = _lstChoose;
        }

        #region var
        public List<EntityDietPrinciple> data { get; set; }
        public List<EntityDietPrinciple> lstChoose { get;set; }
        
        public bool isRefresh { get; set; }

        #endregion

        #region methods
        void Init()
        {
            data = null;
            using (ProxyHms proxy = new ProxyHms())
            {
                data = proxy.Service.GetDietPrinciple();
            }
            this.gcData.DataSource = data;
            this.gcData.RefreshDataSource();

            if(lstChoose != null && gvData.RowCount > 0)
            {
                foreach(var vo in lstChoose)
                {
                    for(int i = 0;i<gvData.RowCount;i++)
                    {
                        EntityDietPrinciple dp = gvData.GetRow(i) as EntityDietPrinciple;

                        if(dp.principleId == vo.principleId)
                        {
                            gvData.SelectRow(i);
                        }
                    }
                }
            }
        }

        #endregion

        #region events
        

        private void frmpopup2060204_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            lstChoose = new List<EntityDietPrinciple>();
            if (gvData.RowCount > 0)
            {
                for (int i = 0;i<gvData.RowCount;i++)
                {
                    if(gvData.IsRowSelected(i))
                    {
                        lstChoose.Add(gvData.GetRow(i) as EntityDietPrinciple);
                    }
                }
                isRefresh = true;
            }
        }

        #endregion
    }
}
