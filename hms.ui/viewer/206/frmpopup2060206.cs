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
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class frmPopup2060206 : frmBasePopup
    {
        public frmPopup2060206()
        {
            InitializeComponent();
        }
        #region var
        List<EntityDietTemplate> lstDietTemplate { get; set; }
        List<EntityDietTemplatetype> lstDietTemplatetype { get; set; }
        public List<EntityDietTemplateDetails> lstDietTemplateDetails { get; set; }
        public bool isRefresh = false;
        #endregion

        #region methods
        void Init()
        {
            lstDietTemplate = null;
            lstDietTemplatetype = null;
            using (ProxyHms proxy = new ProxyHms())
            {
                lstDietTemplate = proxy.Service.GetDietTemplate();
                lstDietTemplatetype = proxy.Service.GetDietTemplatetype();
            }
            this.gcType.DataSource = this.lstDietTemplatetype;
            this.gcType.RefreshDataSource();
            if(lstDietTemplate!= null)
            {
                this.gcData.DataSource = this.lstDietTemplate.FindAll(r => r.typeid == lstDietTemplatetype[0].typeId);
                this.gcData.RefreshDataSource();
            }
        }
        

        #endregion

        #region events
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name = this.txtName.Text;
            int days = Function.Int(this.txtDays.Text);
            List<EntityDietTemplate> dataTemp = lstDietTemplate;
            List<string> search = new List<string>();
            if (!string.IsNullOrEmpty(name))
                search.Add("name");
            if (days > 0)
                search.Add("days");

            if (search.Count > 0)
            {
                foreach (var str in search)
                {
                    string parm = str;
                    switch (parm)
                    {
                        case "name":
                            dataTemp = dataTemp.FindAll(r => r.templateName.Contains(name));
                            break;
                        case "days":
                            dataTemp = dataTemp.FindAll(r => r.days == days);
                            break;
                        default:
                            break;
                    }
                }
            }

            this.gcData.DataSource = dataTemp;
            this.gcData.RefreshDataSource();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.gvData.FocusedRowHandle >= 0)
            {
                EntityDietTemplate temp = this.gvData.GetRow(this.gvData.FocusedRowHandle) as EntityDietTemplate;

                if(temp != null)
                {
                    using (ProxyHms proxy = new ProxyHms())
                    {
                        lstDietTemplateDetails = proxy.Service.GetDietTemplateDetails(temp.templateId);
                    }
                }
            }

            if(lstDietTemplateDetails != null)
            {
                isRefresh = true;
                this.Close();
            }
        }
        

        private void gvType_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                EntityDietTemplatetype type = this.gvType.GetRow(e.RowHandle) as EntityDietTemplatetype;
                if(lstDietTemplate != null)
                {
                    this.gcData.DataSource = this.lstDietTemplate.FindAll(r => r.typeid == type.typeId);
                    this.gcData.RefreshDataSource();
                } 
            }
        }
        private void frmPopup2060206_Load(object sender, EventArgs e)
        {
            Init();
        }
        #endregion
    }
}
