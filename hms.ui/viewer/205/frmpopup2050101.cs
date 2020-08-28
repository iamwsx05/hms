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
    /// 高血压随访
    /// </summary>
    public partial class frmPopup2050101 : frmBasePopup
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmPopup2050101(EntityGxySf _sfVo)
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            if (!DesignMode)
            {
                this.lueSfOper.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueSfOper.LookAndFeel.SkinName = "Black";
                this.lueSfRecorder.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueSfRecorder.LookAndFeel.SkinName = "Black";
                this.gxySf = _sfVo;
            }
        }

        public frmPopup2050101(EntityGxyRecord _gxyRecord)
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            if (!DesignMode)
            {
                this.lueSfOper.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueSfOper.LookAndFeel.SkinName = "Black";
                this.lueSfRecorder.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueSfRecorder.LookAndFeel.SkinName = "Black";
                gxyRecord = _gxyRecord;
            }
        }
        #endregion

        #region var/property
        public EntityHmsSF sfVo { get; set; }
        public EntityGxySf gxySf { get; set; }
        EntityGxySfData sfData { get; set; }
        public EntityGxyRecord gxyRecord { get; set; }

        public bool IsRequireRefresh { get; set; }

        // 单选数组
        List<List<DevExpress.XtraEditors.CheckEdit>> lstSingleCheck { get; set; }

        // 多选数组
        List<List<DevExpress.XtraEditors.CheckEdit>> lstMultiCheck { get; set; }
        #endregion

        #region method

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            #region LUE

            // lueSfOper
            this.lueSfOper.Properties.PopupWidth = 140;
            this.lueSfOper.Properties.PopupHeight = 350;
            this.lueSfOper.Properties.ValueColumn = EntityCodeOperator.Columns.operCode;
            this.lueSfOper.Properties.DisplayColumn = EntityCodeOperator.Columns.operName;
            this.lueSfOper.Properties.Essential = false;
            this.lueSfOper.Properties.IsShowColumnHeaders = true;
            this.lueSfOper.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operCode, 60);
            this.lueSfOper.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operName, 80);
            this.lueSfOper.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operCode, "工号");
            this.lueSfOper.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operName, "姓名");
            this.lueSfOper.Properties.ShowColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName;
            this.lueSfOper.Properties.IsUseShowColumn = true;
            this.lueSfOper.Properties.FilterColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName + "|" + EntityCodeOperator.Columns.pyCode + "|" + EntityCodeOperator.Columns.wbCode;
            if (Common.Entity.GlobalDic.DataSourceEmployee != null)
            {
                this.lueSfOper.Properties.DataSource = Common.Entity.GlobalDic.DataSourceEmployee.ToArray();
                this.lueSfOper.Properties.SetSize();
            }

            // lueSfRecorder
            this.lueSfRecorder.Properties.PopupWidth = 140;
            this.lueSfRecorder.Properties.PopupHeight = 350;
            this.lueSfRecorder.Properties.ValueColumn = EntityCodeOperator.Columns.operCode;
            this.lueSfRecorder.Properties.DisplayColumn = EntityCodeOperator.Columns.operName;
            this.lueSfRecorder.Properties.Essential = false;
            this.lueSfRecorder.Properties.IsShowColumnHeaders = true;
            this.lueSfRecorder.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operCode, 60);
            this.lueSfRecorder.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operName, 80);
            this.lueSfRecorder.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operCode, "工号");
            this.lueSfRecorder.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operName, "姓名");
            this.lueSfRecorder.Properties.ShowColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName;
            this.lueSfRecorder.Properties.IsUseShowColumn = true;
            this.lueSfRecorder.Properties.FilterColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName + "|" + EntityCodeOperator.Columns.pyCode + "|" + EntityCodeOperator.Columns.wbCode;
            if (Common.Entity.GlobalDic.DataSourceEmployee != null)
            {
                this.lueSfRecorder.Properties.DataSource = Common.Entity.GlobalDic.DataSourceEmployee.ToArray();
                this.lueSfRecorder.Properties.SetSize();
            }
            #endregion

            lstSingleCheck = new List<List<DevExpress.XtraEditors.CheckEdit>>();
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkSffs01, chkSffs02, chkSffs03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkSmokeY, chkSmokeN });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkDrinkY, chkDrinkN });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkSaltL01, chkSaltM01, chkSaltW01 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkSaltL02, chkSaltM02, chkSaltW02 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkMindWell, chkMindNormal, chkMindDiff });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkFollowDoctWell, chkFollowDoctNormal, chkFollowDoctDiff });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkUseMedLaw, chkUseMedGap, chkUseMedNo });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkUseMedADRNo, chkUseMedADRYes });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkSfClass01, chkSfClass02, chkSfClass03, chkSfClass04 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkPatFit01, chkPatFit02, chkPatFit03, chkPatFit04, chkPatFit05 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkManageLevel01, chkManageLevel02, chkManageLevel03 });

            lstMultiCheck = new List<List<DevExpress.XtraEditors.CheckEdit>>();
            lstMultiCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkZz01, chkZz02, chkZz03, chkZz04, chkZz05, chkZz06, chkZz07, chkZz08 });

            SetCheckedChanged();

            if (this.gxyRecord != null)
            {
                this.txtPatName.Text = this.gxyRecord.clientName;
                this.txtClientNo.Text = this.gxyRecord.clientNo;
                this.txtSex.Text = this.gxyRecord.sex;
                this.txtAge.Text = this.gxyRecord.age;

                //查询体检项目结果
            }

            if (this.gxySf != null)
            {
                this.dteSfDate.Text = gxySf.sfDate.ToString("yyyy-MM-dd HH:mm");
                this.txtPatName.Text = this.gxySf.clientName;
                this.txtClientNo.Text = this.gxySf.clientNo;
                this.txtSex.Text = this.gxySf.sex;
                this.txtAge.Text = this.gxySf.age;
                this.SetData(gxySf.sfData);
            }
            else
            {
                this.dteSfDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
        }
        #endregion

        #region SetCheckedChanged
        /// <summary>
        /// SetCheckedChanged
        /// </summary>
        void SetCheckedChanged()
        {
            foreach (List<DevExpress.XtraEditors.CheckEdit> checkboxs in lstSingleCheck)
            {
                foreach (DevExpress.XtraEditors.CheckEdit chk in checkboxs)
                {
                    chk.CheckedChanged += Chk_CheckedChanged;
                }
            }

            foreach (List<DevExpress.XtraEditors.CheckEdit> checkboxs in lstMultiCheck)
            {
                foreach (DevExpress.XtraEditors.CheckEdit chk in checkboxs)
                {
                    chk.CheckedChanged += Chk_CheckedChanged;
                }
            }
        }
        #endregion

        #region GetData
        /// <summary>
        /// GetData
        /// </summary>
        /// <returns></returns>
        string GetData()
        {
            List<EntitySfControl> lstControls = new List<EntitySfControl>();
            StringBuilder xmlData = new StringBuilder();
            string fieldName = string.Empty;
            foreach (Control ctrl in this.plContent.Controls)
            {
                if (ctrl is Common.Controls.LookUpEdit)
                {
                    fieldName = (ctrl as Common.Controls.LookUpEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        lstControls.Add(new EntitySfControl()
                        {
                            FieldName = fieldName,
                            Value = (ctrl as Common.Controls.LookUpEdit).Text.Trim(),
                            TabIndex = (ctrl as Common.Controls.LookUpEdit).TabIndex
                        });
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.TextEdit)
                {
                    fieldName = (ctrl as DevExpress.XtraEditors.TextEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        lstControls.Add(new EntitySfControl()
                        {
                            FieldName = fieldName,
                            Value = (ctrl as DevExpress.XtraEditors.TextEdit).Text.Trim(),
                            TabIndex = (ctrl as DevExpress.XtraEditors.TextEdit).TabIndex
                        });
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.CheckEdit)
                {
                    fieldName = (ctrl as DevExpress.XtraEditors.CheckEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        lstControls.Add(new EntitySfControl()
                        {
                            FieldName = fieldName,
                            Value = (ctrl as DevExpress.XtraEditors.CheckEdit).Checked ? "1" : "0",
                            TabIndex = (ctrl as DevExpress.XtraEditors.CheckEdit).TabIndex
                        });
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.DateEdit)
                {
                    fieldName = (ctrl as DevExpress.XtraEditors.DateEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        lstControls.Add(new EntitySfControl()
                        {
                            FieldName = fieldName,
                            Value = (ctrl as DevExpress.XtraEditors.DateEdit).Text.Trim(),
                            TabIndex = (ctrl as DevExpress.XtraEditors.DateEdit).TabIndex
                        });
                    }
                }
            }
            lstControls.Sort();
            xmlData.AppendLine("<FormData>");
            foreach (EntitySfControl item in lstControls)
            {
                xmlData.AppendLine(string.Format("<{0}>{1}</{2}>", item.FieldName, item.Value, item.FieldName));
            }
            xmlData.AppendLine("</FormData>");

            return xmlData.ToString();
        }
        #endregion

        #region SetData
        /// <summary>
        /// SetData
        /// </summary>
        /// <param name="xmlData"></param>
        void SetData(string xmlData)
        {
            if (string.IsNullOrEmpty(xmlData)) return;
            Dictionary<string, string> dicData = Function.ReadXmlNodes(xmlData, "FormData");
            if (dicData != null && dicData.Count > 0)
            {
                string fieldName = string.Empty;
                foreach (Control ctrl in this.plContent.Controls)
                {
                    if (ctrl is Common.Controls.LookUpEdit)
                    {
                        fieldName = (ctrl as Common.Controls.LookUpEdit).Properties.AccessibleName;
                        if (!string.IsNullOrEmpty(fieldName) && dicData.ContainsKey(fieldName))
                        {
                            (ctrl as Common.Controls.LookUpEdit).SetDisplayText<EntityCodeOperator>(dicData[fieldName]);
                        }
                    }
                    else if (ctrl is DevExpress.XtraEditors.TextEdit)
                    {
                        fieldName = (ctrl as DevExpress.XtraEditors.TextEdit).Properties.AccessibleName;
                        if (!string.IsNullOrEmpty(fieldName) && dicData.ContainsKey(fieldName))
                        {
                            (ctrl as DevExpress.XtraEditors.TextEdit).Text = dicData[fieldName];
                        }
                    }
                    else if (ctrl is DevExpress.XtraEditors.CheckEdit)
                    {
                        fieldName = (ctrl as DevExpress.XtraEditors.CheckEdit).Properties.AccessibleName;
                        if (!string.IsNullOrEmpty(fieldName) && dicData.ContainsKey(fieldName))
                        {
                            (ctrl as DevExpress.XtraEditors.CheckEdit).Checked = Function.Int(dicData[fieldName]) == 1 ? true : false;
                        }
                    }
                    else if (ctrl is DevExpress.XtraEditors.DateEdit)
                    {
                        fieldName = (ctrl as DevExpress.XtraEditors.DateEdit).Properties.AccessibleName;
                        if (!string.IsNullOrEmpty(fieldName) && dicData.ContainsKey(fieldName))
                        {
                            (ctrl as DevExpress.XtraEditors.DateEdit).Text = dicData[fieldName];
                        }
                    }
                }
            }
        }
        #endregion

        #region SaveData
        /// <summary>
        /// SaveData
        /// </summary>
        /// <returns></returns>
        void SaveData()
        {
            if (gxySf == null)
            {
                gxySf = new EntityGxySf();
                gxySf.recId = gxyRecord.recId;
            }

            if (gxyRecord == null)
            {
                gxyRecord = new EntityGxyRecord();
                gxyRecord.recId = gxySf.recId;
            }

            gxySf.sfDate = Function.Datetime(dteSfDate.Text);
            gxySf.sfMethod = string.Empty;
            gxySf.sfClass = string.Empty;
            gxyRecord.manageLevel = string.Empty;
            if (chkSffs01.Checked == true)
                gxySf.sfMethod = "1";
            if (chkSffs02.Checked == true)
                gxySf.sfMethod = "2";
            if (chkSffs03.Checked == true)
                gxySf.sfClass = "3";
            if (chkSfClass01.Checked == true)
                gxySf.sfClass = "1";
            if (chkSfClass02.Checked == true)
                gxySf.sfClass = "2";
            if (chkSfClass03.Checked == true)
                gxySf.sfClass = "3";
            if (chkSfClass04.Checked == true)
                gxySf.sfClass = "4";
            if (chkManageLevel01.Checked == true)
                gxyRecord.manageLevel = "1";
            if (chkManageLevel02.Checked == true)
                gxyRecord.manageLevel = "2";
            if (chkManageLevel03.Checked == true)
                gxyRecord.manageLevel = "3";
            if (string.IsNullOrEmpty(gxySf.sfMethod))
            {
                DialogBox.Msg("随访形式必选 ！");
                return;
            }
            if (string.IsNullOrEmpty(gxySf.sfClass))
            {
                DialogBox.Msg("随访分类必选 ！");
                return;
            }
            if (string.IsNullOrEmpty(gxyRecord.manageLevel))
            {
                DialogBox.Msg("管理等级必选 ！");
                return;
            }

            gxySf.sfRecorder = this.lueSfRecorder.EditValue.ToString();
            gxySf.sfStatus = 1;
            if (this.sfData == null)
                this.sfData = new EntityGxySfData();
            this.sfData.sfId = gxySf.sfId;
            this.sfData.xmlData = this.GetData();

            if (!string.IsNullOrEmpty(dteNextSfDate.Text))
                gxyRecord.nextSfDate = Function.Datetime(dteNextSfDate.Text);
           
            decimal sfId = 0;
            bool isNew = this.sfData.sfId <= 0 ? true : false;
            using (ProxyHms proxy = new ProxyHms())
            {
                if (proxy.Service.SaveGxySfRecord(this.gxyRecord, this.gxySf, this.sfData, out sfId) > 0)
                {
                    this.IsRequireRefresh = true;
                    if (isNew)
                    {
                        this.gxySf.sfId = sfId;
                        this.sfData.sfId = sfId;
                        this.gxyRecord.sfId = sfId;
                    }

                    DialogBox.Msg("保存成功！");
                }
                else
                {
                    DialogBox.Msg("保存失败。");
                }
            }
        }
        #endregion

        #endregion

        #region event

        #region Chk_CheckedChanged
        /// <summary>
        /// Chk_CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chk = sender as DevExpress.XtraEditors.CheckEdit;
            if (chk.Checked)
            {
                chk.ForeColor = System.Drawing.Color.Crimson;
                List<DevExpress.XtraEditors.CheckEdit> lstCurr = null;
                foreach (List<DevExpress.XtraEditors.CheckEdit> checkboxs in lstSingleCheck)
                {
                    foreach (DevExpress.XtraEditors.CheckEdit chk2 in checkboxs)
                    {
                        if (chk == chk2)
                        {
                            lstCurr = checkboxs;
                            break;
                        }
                    }
                    if (lstCurr != null)
                        break;
                }
                if (lstCurr != null)
                {
                    foreach (DevExpress.XtraEditors.CheckEdit chk3 in lstCurr)
                    {
                        if (chk3 != chk)
                            chk3.Checked = false;
                    }
                }
            }
            else
            {
                chk.ForeColor = System.Drawing.Color.Black;
            }
        }
        #endregion

        private void frmPopup2050101_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SaveData();
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion

    }

    #region EntitySfControl
    /// <summary>
    /// EntitySfControl
    /// </summary>
    public class EntitySfControl : IComparable
    {
        public string FieldName { get; set; }

        public string Value { get; set; }

        public int TabIndex { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is EntitySfControl)
            {
                return this.TabIndex.CompareTo(((EntitySfControl)obj).TabIndex);
            }
            return 0;
        }
    }
    #endregion

}
