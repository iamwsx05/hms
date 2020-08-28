using Common.Controls;
using Common.Utils;
using System;
using System.Collections.Generic;
using weCare.Core.Entity;
using weCare.Core.Utils;
using System.Text;
using System.Windows.Forms;
using Hms.Entity;

namespace Hms.Ui
{
    /// <summary>
    /// 高血压评估
    /// </summary>
    public partial class frmPopup2050102 : frmBasePopup
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmPopup2050102(EntityGxyPg _pgVo)
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            if (!DesignMode)
            {
                this.lueEnaOper.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueEnaOper.LookAndFeel.SkinName = "Black";
                this.lueRecorder.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueRecorder.LookAndFeel.SkinName = "Black";
                this.pgVo = _pgVo;
            }
        }

        public frmPopup2050102(EntityGxyRecord _gxyRecord)
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            if (!DesignMode)
            {
                this.lueEnaOper.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueEnaOper.LookAndFeel.SkinName = "Black";
                this.lueRecorder.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueRecorder.LookAndFeel.SkinName = "Black";
                this.gxyRecord = _gxyRecord;
            }
        }
        #endregion

        #region var/property

        public EntityGxyPg pgVo { get; set; }
        public EntityGxyPgData pgData { get; set; }
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
            this.lueEnaOper.Properties.PopupWidth = 140;
            this.lueEnaOper.Properties.PopupHeight = 350;
            this.lueEnaOper.Properties.ValueColumn = EntityCodeOperator.Columns.operCode;
            this.lueEnaOper.Properties.DisplayColumn = EntityCodeOperator.Columns.operName;
            this.lueEnaOper.Properties.Essential = false;
            this.lueEnaOper.Properties.IsShowColumnHeaders = true;
            this.lueEnaOper.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operCode, 60);
            this.lueEnaOper.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operName, 80);
            this.lueEnaOper.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operCode, "工号");
            this.lueEnaOper.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operName, "姓名");
            this.lueEnaOper.Properties.ShowColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName;
            this.lueEnaOper.Properties.IsUseShowColumn = true;
            this.lueEnaOper.Properties.FilterColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName + "|" + EntityCodeOperator.Columns.pyCode + "|" + EntityCodeOperator.Columns.wbCode;
            if (Common.Entity.GlobalDic.DataSourceEmployee != null)
            {
                this.lueEnaOper.Properties.DataSource = Common.Entity.GlobalDic.DataSourceEmployee.ToArray();
                this.lueEnaOper.Properties.SetSize();
            }

            // lueSfRecorder
            this.lueRecorder.Properties.PopupWidth = 140;
            this.lueRecorder.Properties.PopupHeight = 350;
            this.lueRecorder.Properties.ValueColumn = EntityCodeOperator.Columns.operCode;
            this.lueRecorder.Properties.DisplayColumn = EntityCodeOperator.Columns.operName;
            this.lueRecorder.Properties.Essential = false;
            this.lueRecorder.Properties.IsShowColumnHeaders = true;
            this.lueRecorder.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operCode, 60);
            this.lueRecorder.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operName, 80);
            this.lueRecorder.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operCode, "工号");
            this.lueRecorder.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operName, "姓名");
            this.lueRecorder.Properties.ShowColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName;
            this.lueRecorder.Properties.IsUseShowColumn = true;
            this.lueRecorder.Properties.FilterColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName + "|" + EntityCodeOperator.Columns.pyCode + "|" + EntityCodeOperator.Columns.wbCode;
            if (Common.Entity.GlobalDic.DataSourceEmployee != null)
            {
                this.lueRecorder.Properties.DataSource = Common.Entity.GlobalDic.DataSourceEmployee.ToArray();
                this.lueRecorder.Properties.SetSize();
            }
            #endregion

            lstSingleCheck = new List<List<DevExpress.XtraEditors.CheckEdit>>();
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkBfz01, chkBfz02 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkXyfj01, chkXyfj02, chkXyfj03, chkXyfj04, chkXyfj05, chkXyfj06 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkWxfc01, chkWxfc02, chkWxfc03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkManageLevel01, chkManageLevel02, chkManageLevel03 });

            lstMultiCheck = new List<List<DevExpress.XtraEditors.CheckEdit>>();
            lstMultiCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkWxys01, chkWxys02, chkWxys03, chkWxys04, chkWxys05, chkWxys06 });
            lstMultiCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkBqgss01, chkBqgss02, chkBqgss03 });
            lstMultiCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkLcbh01, chkLcbh02, chkLcbh03, chkLcbh04, chkLcbh05, chkLcbh06 });

            SetCheckedChanged();

            if(gxyRecord != null)
            {
                this.txtPatName.Text = this.gxyRecord.clientName;
                this.txtClientNo.Text = this.gxyRecord.clientNo;
                this.txtSex.Text = this.gxyRecord.sex;
                this.txtAge.Text = this.gxyRecord.age;
                this.dteEnaDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            if (this.pgVo != null)
            {
                this.txtPatName.Text = this.pgVo.clientName;
                this.txtClientNo.Text = this.pgVo.clientNo;
                this.txtSex.Text = this.pgVo.sex;
                this.txtAge.Text = this.pgVo.age;
                this.dteEnaDate.Text = this.pgVo.evaDateStr;
                this.SetData(pgVo.pgData);

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
            foreach (Control ctrl in this.plContent.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.TextEdit)
                {
                    if (!string.IsNullOrEmpty((ctrl as DevExpress.XtraEditors.TextEdit).Properties.AccessibleName))
                    {
                        lstControls.Add(new EntitySfControl()
                        {
                            FieldName = (ctrl as DevExpress.XtraEditors.TextEdit).Properties.AccessibleName,
                            Value = (ctrl as DevExpress.XtraEditors.TextEdit).Text.Trim(),
                            TabIndex = (ctrl as DevExpress.XtraEditors.TextEdit).TabIndex
                        });
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.CheckEdit)
                {
                    if (!string.IsNullOrEmpty((ctrl as DevExpress.XtraEditors.CheckEdit).Properties.AccessibleName))
                    {
                        lstControls.Add(new EntitySfControl()
                        {
                            FieldName = (ctrl as DevExpress.XtraEditors.CheckEdit).Properties.AccessibleName,
                            Value = (ctrl as DevExpress.XtraEditors.CheckEdit).Checked ? "1" : "0",
                            TabIndex = (ctrl as DevExpress.XtraEditors.CheckEdit).TabIndex
                        });
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.DateEdit)
                {
                    if (!string.IsNullOrEmpty((ctrl as DevExpress.XtraEditors.DateEdit).Properties.AccessibleName))
                    {
                        lstControls.Add(new EntitySfControl()
                        {
                            FieldName = (ctrl as DevExpress.XtraEditors.DateEdit).Properties.AccessibleName,
                            Value = (ctrl as DevExpress.XtraEditors.DateEdit).Text.Trim(),
                            TabIndex = (ctrl as DevExpress.XtraEditors.DateEdit).TabIndex
                        });
                    }
                }
                else if (ctrl is Common.Controls.LookUpEdit)
                {
                    if (!string.IsNullOrEmpty((ctrl as Common.Controls.LookUpEdit).Properties.AccessibleName))
                    {
                        lstControls.Add(new EntitySfControl()
                        {
                            FieldName = (ctrl as Common.Controls.LookUpEdit).Properties.AccessibleName,
                            Value = (ctrl as Common.Controls.LookUpEdit).Text.Trim(),
                            TabIndex = (ctrl as Common.Controls.LookUpEdit).TabIndex
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
            if (pgVo == null)
            {
                pgVo = new EntityGxyPg();
                pgVo.recId = gxyRecord.recId;
            }
            if (gxyRecord == null)
            {
                gxyRecord = new EntityGxyRecord();
                gxyRecord.recId = pgVo.recId;
            }
            if (chkXyfj01.Checked == true)
                pgVo.bloodPressLevel = "1";
            if (chkXyfj02.Checked == true)
                pgVo.bloodPressLevel = "2";
            if (chkXyfj03.Checked == true)
                pgVo.bloodPressLevel = "3";
            if (chkXyfj04.Checked == true)
                pgVo.bloodPressLevel = "4";
            if (chkXyfj05.Checked == true)
                pgVo.bloodPressLevel = "5";
            if (chkXyfj06.Checked == true)
                pgVo.bloodPressLevel = "6";

            if (chkWxfc01.Checked == true)
                pgVo.dangerLevel = "1";
            if (chkWxfc02.Checked == true)
                pgVo.dangerLevel = "2";
            if (chkWxfc03.Checked == true)
                pgVo.dangerLevel = "3";

            if (chkManageLevel01.Checked == true)
                pgVo.manageLevel = "1";
            if (chkManageLevel02.Checked == true)
                pgVo.manageLevel = "2";
            if (chkManageLevel03.Checked == true)
                pgVo.manageLevel = "3";
            pgVo.evaluator = lueEnaOper.EditValue.ToString();
            pgVo.evaDate = Function.Datetime(dteEnaDate.Text);
            pgData = new EntityGxyPgData();
            pgData.xmlData = GetData();
            decimal pgId = 0;
            bool isNew = this.pgVo.pgId <= 0 ? true : false;
            using (ProxyHms proxy = new ProxyHms())
            {
                if (proxy.Service.SaveGxyPgRecord(this.gxyRecord,this.pgVo, this.pgData, out pgId) > 0)
                {
                    this.IsRequireRefresh = true;
                    if (isNew)
                    {
                        this.pgVo.pgId = pgId;
                        this.pgData.pgId = pgId;
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

        private void frmPopup2050102_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void blbiImportData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
