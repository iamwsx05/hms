using Hms.Biz;
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
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            this.gridLookUpEdit1.Properties.ValueMember = "clientNo";
            this.gridLookUpEdit1.Properties.DisplayMember = "clientName";
        }

        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {
            



        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void gridLookUpEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string content = gridLookUpEdit1.Text.Trim();

                if (string.IsNullOrEmpty(content))
                {
                    return;
                }
                Biz201 biz = new Biz201();
                List<EntityClientInfo> lstClientInfo = biz.GetClientInfos(content);
                gridLookUpEdit1.Properties.DataSource = lstClientInfo;
                gridLookUpEdit1.ShowPopup();

                gridLookUpEdit1.Properties.DataSource = lstClientInfo;
                gridLookUpEdit1.ShowPopup();
            }
        }


        // lstClientInfo = proxy.Service.GetClientInfos(search);
    }
}
