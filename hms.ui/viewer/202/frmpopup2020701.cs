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
    public partial class frmpopup2020701 : frmBasePopup
    {
        public frmpopup2020701()
        {
            InitializeComponent();
        }

        #region var
        List<EntityClientInfo> lstClientInfo { get; set; }
        List<EntityDicQnMain> lstQnMain { get; set; }

        #endregion

        #region methods
        void Init()
        {
            int classId = -1;
            if (rdgQn.SelectedIndex == 2)
                classId = 2;
            else if (rdgQn.SelectedIndex == 1)
                classId = 3;
            else if (rdgQn.SelectedIndex == 0)
                classId = 4;

            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> parms = new List<EntityParm>();
                EntityParm vo = new EntityParm();
                vo.key = "class";
                vo.value = classId.ToString();
                parms.Add(vo);
                lstQnMain = proxy.Service.GetQnMain(parms);
            }

            this.gridControl.DataSource = lstQnMain;
        }

        public EntityDicQnMain GetRowObject()
        {
            EntityDicQnMain vo = null;
            if (this.cardView.FocusedRowHandle >= 0)
                vo = this.cardView.GetRow(this.cardView.FocusedRowHandle) as EntityDicQnMain;

            return vo;
        }
        #endregion


        #region events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPopup2020702 frm = new frmPopup2020702(GetRowObject());
            frm.ShowDialog();
            this.Close();
        }

        private void frmpopup2020701_Load(object sender, EventArgs e)
        {
            Init();
        }
        #endregion

        private void rdgQn_SelectedIndexChanged(object sender, EventArgs e)
        {
            int classId = -1;
            if(rdgQn.SelectedIndex== 2)
                classId = 2;
            else if(rdgQn.SelectedIndex == 1)
                classId = 3;
            else if (rdgQn.SelectedIndex == 0)
                classId = 4;

            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> parms = new List<EntityParm>();
                EntityParm vo = new EntityParm();
                vo.key = "class";
                vo.value = classId.ToString();
                parms.Add(vo);
                lstQnMain = proxy.Service.GetQnMain(parms);
            }

            this.gridControl.DataSource = lstQnMain;
        }
    }
}
