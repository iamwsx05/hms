using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Hms.Entity;

namespace Hms.Ui
{
    /// <summary>
    /// 危险因素
    /// </summary>
    public partial class frmPopup20903 : frmBasePopup
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmPopup20903()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="_hazardsVo"></param>
        public frmPopup20903(EntityDicHazards _hazardsVo)
        {
            InitializeComponent();
            HazardsVo = _hazardsVo;
        }
        #endregion

        #region var/property

        public EntityDicHazards HazardsVo { get; set; }

        public bool IsRequireRefresh { get; set; }

        List<EntityDicQnSetting> DataSourceTopics { get; set; }

        #endregion

        #region method

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            try
            {
                uiHelper.BeginLoading(this);
                using (ProxyHms proxy = new ProxyHms())
                {
                    DataSourceTopics = proxy.Service.GetTopics();
                }
                #region lue
                // lueTopic
                this.lueTopic.Properties.PopupWidth = this.lueTopic.Width;
                this.lueTopic.Properties.PopupHeight = 400;
                this.lueTopic.Properties.ValueColumn = EntityDicQnSetting.Columns.fieldId;
                this.lueTopic.Properties.DisplayColumn = EntityDicQnSetting.Columns.fieldName;
                this.lueTopic.Properties.Essential = false;
                this.lueTopic.Properties.IsShowColumnHeaders = true;
                this.lueTopic.Properties.ColumnWidth.Add(EntityDicQnSetting.Columns.fieldId, 70);
                this.lueTopic.Properties.ColumnWidth.Add(EntityDicQnSetting.Columns.fieldName, this.lueTopic.Width - 70);
                this.lueTopic.Properties.ColumnHeaders.Add(EntityDicQnSetting.Columns.fieldId, "编码");
                this.lueTopic.Properties.ColumnHeaders.Add(EntityDicQnSetting.Columns.fieldName, "名称");
                this.lueTopic.Properties.ShowColumn = EntityDicQnSetting.Columns.fieldId + "|" + EntityDicQnSetting.Columns.fieldName;
                this.lueTopic.Properties.IsUseShowColumn = true;
                this.lueTopic.Properties.FilterColumn = EntityDicQnSetting.Columns.fieldId + "|" + EntityDicQnSetting.Columns.fieldName + "|" + EntityDicQnSetting.Columns.pyCode + "|" + EntityDicQnSetting.Columns.wbCode;
                if (DataSourceTopics != null)
                {
                    this.lueTopic.Properties.DataSource = DataSourceTopics.ToArray();
                    this.lueTopic.Properties.SetSize();
                }

                this.lueField.Properties.PopupWidth = this.lueField.Width;
                this.lueField.Properties.PopupHeight = 200;
                this.lueField.Properties.ValueColumn = EntityDicQnSetting.Columns.fieldId;
                this.lueField.Properties.DisplayColumn = EntityDicQnSetting.Columns.fieldName;
                this.lueField.Properties.Essential = false;
                //this.lueField.Properties.IsShowColumnHeaders = true;
                this.lueField.Properties.ColumnWidth.Add(EntityDicQnSetting.Columns.fieldName, this.lueTopic.Width);
                this.lueField.Properties.ColumnHeaders.Add(EntityDicQnSetting.Columns.fieldName, "名称");
                this.lueField.Properties.ShowColumn = EntityDicQnSetting.Columns.fieldName;
                this.lueField.Properties.IsUseShowColumn = true;
                this.lueField.Properties.FilterColumn = EntityDicQnSetting.Columns.fieldId + "|" + EntityDicQnSetting.Columns.fieldName + "|" + EntityDicQnSetting.Columns.pyCode + "|" + EntityDicQnSetting.Columns.wbCode;
                #endregion

                if (this.HazardsVo != null)
                {
                    this.cboClass.SelectedIndex = this.HazardsVo.classId - 1;
                    this.txtSortNo.Text = this.HazardsVo.sortNo.ToString();
                    this.txtHazards.Text = this.HazardsVo.hazards;
                    this.txtSuggest.Text = this.HazardsVo.suggest;
                    if (!string.IsNullOrEmpty(this.HazardsVo.topicId) && this.HazardsVo.topicId != "")
                    {
                        this.lueTopic.SetDisplayText<EntityDicQnSetting>(this.HazardsVo.topicName);
                        this.FilterField(this.HazardsVo.topicId);
                        this.lblTopic.Tag = this.HazardsVo.topicId;
                    }
                    if (!string.IsNullOrEmpty(this.HazardsVo.fieldId) && this.HazardsVo.fieldId != "")
                    { 
                        this.lueField.SetDisplayText<EntityDicQnSetting>(this.HazardsVo.fieldName); 
                    }
                }
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        void Save()
        {
            EntityDicHazards hazaVo = new EntityDicHazards();
            hazaVo.classId = this.cboClass.SelectedIndex + 1;
            hazaVo.hazards = this.txtHazards.Text.Trim();
            if (this.lueTopic.Properties.DBRow != null)
            {
                hazaVo.topicId = (this.lueTopic.Properties.DBRow as EntityDicQnSetting).fieldId;
                hazaVo.topicName = (this.lueTopic.Properties.DBRow as EntityDicQnSetting).fieldName;
            }
            if (this.lueField.Properties.DBRow != null)
            {
                hazaVo.fieldId = (this.lueField.Properties.DBRow as EntityDicQnSetting).fieldId;
                hazaVo.fieldName = (this.lueField.Properties.DBRow as EntityDicQnSetting).fieldName;
            }
            hazaVo.suggest = this.txtSuggest.Text.Trim();
            hazaVo.sortNo = Function.Int(this.txtSortNo.Text);

            if (this.HazardsVo != null)
            {
                hazaVo.hId = this.HazardsVo.hId;
            }
            if (string.IsNullOrEmpty(hazaVo.hazards))
            {
                DialogBox.Msg("危险因素信息不能为空，请填写。");
                this.txtHazards.Focus();
                return;
            }

            decimal hId = 0;
            using (ProxyHms proxy = new ProxyHms())
            {
                if (proxy.Service.SaveHazards(hazaVo, out hId) > 0)
                {
                    if (this.HazardsVo == null)
                    {
                        this.HazardsVo = new EntityDicHazards() { hId = hId };
                    }
                    this.IsRequireRefresh = true;
                    DialogBox.Msg("保存危险因素成功！");
                }
                else
                {
                    DialogBox.Msg("保存危险因素失败。");
                }
            }
        }
        #endregion

        #region FilterField
        /// <summary>
        /// FilterField
        /// </summary>
        /// <param name="fieldId"></param>
        void FilterField(string fieldId)
        {
            if (string.IsNullOrEmpty(fieldId)) return;
            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityDicQnSummary> data = proxy.Service.GetTopicItems(fieldId);
                if (data != null)
                {
                    this.lueField.Properties.DataSource = data.ToArray();
                    this.lueField.Properties.SetSize();
                }
            }
        }
        #endregion

        #endregion

        #region event

        private void frmPopup2090105_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void lueTopic_HandleDBValueChanged(object sender)
        {
            if (this.lueTopic.Properties.DBRow != null)
            {
                string topicId = (this.lueTopic.Properties.DBRow as EntityDicQnSetting).fieldId;
                this.FilterField(topicId);
                if (this.lblTopic.Tag != null && this.lblTopic.Tag.ToString() == topicId)
                { }
                else
                {
                    this.lueField.Properties.DBRow = null;
                    this.lueField.Text = string.Empty;
                }
                this.lblTopic.Tag = topicId;
            }
        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Save();
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
