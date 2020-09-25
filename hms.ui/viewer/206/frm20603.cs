using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hms.Entity;
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class frm20603 : frmBaseMdi
    {
        public frm20603()
        {
            InitializeComponent();
        }

        #region var/property
        List<EntityDietTemplate> lstDietTemplate = null;
        List<EntityDietTemplatetype> lstDietTemplatetype = null;
        #endregion

        #region override
        /// <summary>
        /// 查询
        /// </summary>
        public override void Search()
        {
            string name = this.txtName.Text;
            int days = Function.Int(this.txtDays.Text);
            List<EntityDietTemplate> dataTemp = lstDietTemplate;
            List<string> search = new List<string>();
            if (!string.IsNullOrEmpty(name))
                search.Add("name");
            if(days > 0)
                search.Add("days");

            if(search.Count > 0)
            {
                foreach(var str in search)
                {
                    string parm = str;
                    switch(parm)
                    {
                        case "name":
                            dataTemp = dataTemp.FindAll(r=>r.templateName.Contains(name));
                            break;
                        case "days":
                            dataTemp = dataTemp.FindAll(r=>r.days == days);
                            break;
                        default:
                            break;
                    }
                }
            }

            this.gcData.DataSource = dataTemp;
            this.gcData.RefreshDataSource();
        }

        /// <summary>
        /// 添加
        /// </summary>
        public override void New()
        {
            frmPopup2060301 frm = new frmPopup2060301();
            frm.ShowDialog();
            if (frm.IsRequireRefresh)
            {
                this.RefreshData();
                //this.ScrollRow(frm.dietPrinciple.principleId);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// 
        public override void Edit()
        {
            if (this.gvData.SelectedRowsCount > 0)
            {
                frmPopup2060301 frm = new frmPopup2060301(this.gvData.GetRow(this.gvData.GetSelectedRows()[0]) as EntityDietTemplate);
                frm.ShowDialog();
                if (frm.IsRequireRefresh)
                {
                    this.RefreshData();
                    //this.ScrollRow(frm.dietPrinciple.principleId);
                }
            }
            else
            {
                DialogBox.Msg("请选择要编辑的记录.");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// 
        public override void Delete()
        {
            if (this.gvData.SelectedRowsCount > 0)
            {
                if (DialogBox.Msg("是否删除所选行记录？", MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                bool isRequireRefresh = false;
                int affect = -1;
                List<EntityDietTemplate> delData = new List<EntityDietTemplate>();
                for (int i = this.gvData.RowCount - 1; i >= 0; i--)
                {
                    if (this.gvData.IsRowSelected(i))
                    {
                        delData.Add((this.gvData.GetRow(i) as EntityDietTemplate));
                    }
                }

                if (delData.Count > 0)
                {
                    using (ProxyHms proxy = new ProxyHms())
                    {
                        affect = proxy.Service.DeleteDietTemplate(delData);
                    }

                    if (affect < 0)
                    {
                        DialogBox.Msg("删除记录失败。");
                    }
                    else
                    {
                        isRequireRefresh = true;
                    }
                }
                if (isRequireRefresh)
                    this.RefreshData();
            }
            else
            {
                DialogBox.Msg("请选择需要删除的记录。");
            }
        }

        /// <summary>
        /// 查看
        /// </summary>
        public override void LoadData()
        {
            if (this.gvData.SelectedRowsCount > 0)
            {
                frmPopup2060201 frm = new frmPopup2060201(this.gvData.GetRow(this.gvData.GetSelectedRows()[0]) as EntityDietTemplate);
                frm.ShowDialog();
            }
        }
        #endregion

        #region method

        #region Init
        internal void Init()
        {
            try
            {
                uiHelper.BeginLoading(this);
                RefreshData();
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region RefreshData
        /// <summary>
        /// RefreshData
        /// </summary>
        public override void RefreshData()
        {
            uiHelper.BeginLoading(this);
            this.LoadQnDataSource();
            this.gcType.DataSource = this.lstDietTemplatetype;
            this.gcType.RefreshDataSource();
            this.gcData.DataSource = this.lstDietTemplate.FindAll(r=>r.typeid == lstDietTemplatetype[0].typeId);
            this.gcData.RefreshDataSource();
            uiHelper.CloseLoading(this);
        }
        #endregion

        #region LoadQnDataSource
        /// <summary>
        /// LoadQnDataSource
        /// </summary>
        void LoadQnDataSource()
        {
            lstDietTemplate = null;
            lstDietTemplatetype = null;
            using (ProxyHms proxy = new ProxyHms())
            {
                lstDietTemplate = proxy.Service.GetDietTemplate();
                lstDietTemplatetype = proxy.Service.GetDietTemplatetype();
            }
        }
        #endregion

        #endregion

        #region event
        private void frm20603_Load(object sender, EventArgs e)
        {
            Init();
        }
        
        private void gvType_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if(e.RowHandle >= 0)
            {
                EntityDietTemplatetype type = this.gvType.GetRow(e.RowHandle) as EntityDietTemplatetype;
                this.gcData.DataSource = this.lstDietTemplate.FindAll(r => r.typeid == type.typeId);
                this.gcData.RefreshDataSource();
            }
        }

        private void gvData_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if(e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
        
        private void gvData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (gvData.IsRowSelected(e.RowHandle))
                    gvData.UnselectRow(e.RowHandle);
                else
                    gvData.SelectRow(e.RowHandle);
            }
        }
        #endregion
    }
}
