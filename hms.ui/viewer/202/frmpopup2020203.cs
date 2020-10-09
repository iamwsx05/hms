using Common.Controls;
using Common.Utils;
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
    public partial class frmPopup2020203 : frmBasePopup
    {
        public frmPopup2020203()
        {
            InitializeComponent();
        }

        #region var/property

        /// <summary>
        /// 问卷数据源
        /// </summary>
        List<EntityDicQnMain> dataSourceQN { get; set; }

        public bool isSelect { get; set; }
        public EntityDicQnMain dicQn { get; set; }

        #endregion

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            try
            {
                uiHelper.BeginLoading(this);
                dataSourceQN = null;
                List<EntityParm> parms = new List<EntityParm>();
                EntityParm parm = new EntityParm();
                parm.value = "1";
                parm.key = "class";
                parms.Add(parm);
                using (ProxyHms proxy = new ProxyHms())
                {
                    dataSourceQN = proxy.Service.GetQnMain(parms);
                }
                this.gridControl.DataSource = this.dataSourceQN;
                this.gridControl.RefreshDataSource();
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region events
        private void frmPopup2020203_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = this.txtSearch.Text;

            if(!string.IsNullOrEmpty(search))
            {
                if(dataSourceQN != null)
                {
                    this.gridControl.DataSource = this.dataSourceQN.FindAll(r=>r.qnName.Contains(search) ) ;
                    this.gridControl.RefreshDataSource();
                }
            }
        }
        #endregion

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(this.gridView.RowCount > 0)
            {
                dicQn = this.gridView.GetRow(this.gridView.FocusedRowHandle) as EntityDicQnMain;
                if(dicQn != null)
                {
                    this.isSelect = true;
                    this.Close();
                }
            }
        }
    }
}
