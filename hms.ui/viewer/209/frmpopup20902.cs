using Common.Controls;
using Hms.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Hms.Ui
{
    /// <summary>
    /// 题库题目
    /// </summary>
    public partial class frmPopup20902 : frmBasePopup
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmPopup20902()
        {
            InitializeComponent();
            this.Text += "-题目";
        }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="_topicVo"></param>
        public frmPopup20902(EntityDicQnSummary _topicVo)
        {
            InitializeComponent();
            TopicVo = _topicVo;
            this.Text += "-【" + _topicVo.fieldName + "】";
        }
        #endregion

        #region var/property

        public EntityDicQnSummary TopicVo { get; set; }

        BindingSource gvDataBindingSourceItems { get; set; }

        public bool IsRequireRefresh { get; set; }

        #endregion

        #region method

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            this.gvDataBindingSourceItems = new BindingSource();
            this.gvDataBindingSourceItems.DataSource = new List<EntityDicQnSummary>();
            this.gcItems.DataSource = this.gvDataBindingSourceItems;

            if (TopicVo != null)
            {
                this.txtFieldName.Text = this.TopicVo.fieldName;
                this.txtComment.Text = this.TopicVo.comment;
                this.rdoEssential.SelectedIndex = this.TopicVo.isEssential;
                this.rdoStatus.SelectedIndex = this.TopicVo.status;
                this.txtSortNo.Text = this.TopicVo.sortNo.ToString();
                this.cboType.SelectedIndex = Function.Int(this.TopicVo.typeId) - 1;

                if (TopicVo.typeId == "1" || TopicVo.typeId == "2")
                {
                    List<EntityDicQnSummary> items = (new ProxyHms()).Service.GetTopicItems(this.TopicVo.fieldId);
                    this.gvDataBindingSourceItems.DataSource = items;
                }
            }
        }
        #endregion

        #region DeleteRow
        /// <summary>
        /// DeleteRow
        /// </summary>
        /// <param name="rowHandle"></param>
        void DeleteRow(int rowHandle)
        {
            if (rowHandle < 0) return;
            this.gvItems.CloseEditor();
            this.gvItems.SelectRow(rowHandle);
            if (this.gvDataBindingSourceItems.Count > 0) this.gvDataBindingSourceItems.RemoveAt(rowHandle);
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        void Save()
        {
            EntityDicQnSummary mainVo = new EntityDicQnSummary();
            mainVo.qnClassId = 2;
            mainVo.typeId = Convert.ToString(this.cboType.SelectedIndex + 1);
            mainVo.isParent = 1;
            mainVo.fieldName = this.txtFieldName.Text.Trim();
            mainVo.comment = this.txtComment.Text.Trim();
            mainVo.isEssential = this.rdoEssential.SelectedIndex;
            mainVo.status = this.rdoStatus.SelectedIndex;
            mainVo.sortNo = Function.Int(this.txtSortNo.Text);
            mainVo.parentFieldId = " ";

            if (this.TopicVo != null)
            {
                mainVo.fieldId = this.TopicVo.fieldId;
            }
            if (string.IsNullOrEmpty(mainVo.fieldName))
            {
                DialogBox.Msg("题目名称不能为空，请填写。");
                this.txtFieldName.Focus();
                return;
            }

            this.gvItems.CloseEditor();
            List<EntityDicQnSummary> lstSub = new List<EntityDicQnSummary>();
            if (this.gvItems.RowCount > 0)
            {
                lstSub = this.gvDataBindingSourceItems.DataSource as List<EntityDicQnSummary>;
            }
            string fieldId = string.Empty;
            using (ProxyHms proxy = new ProxyHms())
            {
                if (proxy.Service.SaveQnTopic(mainVo, lstSub, out fieldId) > 0)
                {
                    if (this.TopicVo == null)
                    {
                        this.TopicVo = new EntityDicQnSummary() { fieldId = fieldId };
                    }
                    this.IsRequireRefresh = true;
                    DialogBox.Msg("保存题目成功！");
                }
                else
                {
                    DialogBox.Msg("保存题目失败。");
                }
            }
        }
        #endregion

        #endregion

        #region event

        private void frmPopup2090104_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Save();
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.gvDataBindingSourceItems.AddNew();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            this.DeleteRow(this.gvItems.FocusedRowHandle);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int rowHandle = this.gvItems.FocusedRowHandle;
            string fieldName = "fieldName";
            if (rowHandle < this.gvItems.RowCount - 1)
            {
                object value1 = this.gvItems.GetRowCellValue(rowHandle, fieldName);
                object value2 = this.gvItems.GetRowCellValue(rowHandle + 1, fieldName);
                this.gvItems.SetRowCellValue(rowHandle + 1, fieldName, value1);
                this.gvItems.SetRowCellValue(rowHandle, fieldName, value2);
                this.gvItems.FocusedRowHandle = rowHandle + 1;
                this.gvItems.SelectRow(this.gvItems.FocusedRowHandle);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int rowHandle = this.gvItems.FocusedRowHandle;
            string fieldName = "fieldName";
            if (rowHandle > 0)
            {
                object value1 = this.gvItems.GetRowCellValue(rowHandle, fieldName);
                object value2 = this.gvItems.GetRowCellValue(rowHandle - 1, fieldName);
                this.gvItems.SetRowCellValue(rowHandle - 1, fieldName, value1);
                this.gvItems.SetRowCellValue(rowHandle, fieldName, value2);
                this.gvItems.FocusedRowHandle = rowHandle - 1;
                this.gvItems.SelectRow(this.gvItems.FocusedRowHandle);
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedIndex == 2)
            {
                this.btnAdd.Enabled = false;
                this.btnDel.Enabled = false;
                this.btnUp.Enabled = false;
                this.btnDown.Enabled = false;
                this.gvDataBindingSourceItems.Clear();
            }
            else
            {
                this.btnAdd.Enabled = true;
                this.btnDel.Enabled = true;
                this.btnUp.Enabled = true;
                this.btnDown.Enabled = true;
            }
        }

        #endregion

    }
}
