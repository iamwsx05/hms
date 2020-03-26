﻿using Common.Controls;
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
    /// 糖尿病随访
    /// </summary>
    public partial class frmPopup2050201 : frmBasePopup
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmPopup2050201(EntityHmsSF _sfVo)
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            if (!DesignMode)
            {
                this.lueSfOper.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueSfOper.LookAndFeel.SkinName = "Black";
                this.lueSfRecorder.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueSfRecorder.LookAndFeel.SkinName = "Black";
                this.sfVo = _sfVo;
            }
        }
        #endregion

        #region var/property

        BindingSource bindingSource { get; set; }

        List<EntityTNBUseMed> dataSourceUseMed { get; set; }

        // 单选数组
        List<List<DevExpress.XtraEditors.CheckEdit>> lstSingleCheck { get; set; }

        // 多选数组
        List<List<DevExpress.XtraEditors.CheckEdit>> lstMultiCheck { get; set; }

        public EntityHmsSF sfVo { get; set; }

        public bool IsRequireRefresh { get; set; } 

        EntityTnbSfData sfData { get; set; }

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

            this.bindingSource = new BindingSource();
            this.gcUseMed.DataSource = this.bindingSource;
            dataSourceUseMed = new List<EntityTNBUseMed>();
            this.bindingSource.DataSource = dataSourceUseMed;

            lstSingleCheck = new List<List<DevExpress.XtraEditors.CheckEdit>>();
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkSffs01, chkSffs02, chkSffs03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkSmokeY, chkSmokeN });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkDrinkY, chkDrinkN });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkMindWell, chkMindNormal, chkMindDiff });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkFollowDoctWell, chkFollowDoctNormal, chkFollowDoctDiff });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkUseMedLaw, chkUseMedGap, chkUseMedNo });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkUseMedADRNo, chkUseMedADRYes });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkHypoglycemia01, chkHypoglycemia02, chkHypoglycemia03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkSfClass01, chkSfClass02, chkSfClass03, chkSfClass04 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkPatFit01, chkPatFit02, chkPatFit03, chkPatFit04, chkPatFit05 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkManageLevel01, chkManageLevel02, chkManageLevel03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkFootArtery01, chkFootArtery02, chkFootArtery03, chkFootArtery04 });

            lstMultiCheck = new List<List<DevExpress.XtraEditors.CheckEdit>>();
            lstMultiCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkZz01, chkZz02, chkZz03, chkZz04, chkZz05, chkZz06, chkZz07, chkZz08, chkZzqt });

            SetCheckedChanged();
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

        #region GetDataTable
        /// <summary>
        /// GetDataTable
        /// </summary>
        /// <returns></returns>
        string GetDataTable()
        {
            StringBuilder xmlData = new StringBuilder();
            this.gvUseMed.CloseEditor();

            List<EntityTNBUseMed> data = this.bindingSource.DataSource as List<EntityTNBUseMed>;
            if (data != null && data.Count > 0)
            {
                string fieldName = string.Empty;
                xmlData.AppendLine("<FormData>");
                foreach (EntityTNBUseMed item in data)
                {
                    xmlData.AppendLine("<Row>");
                    fieldName = "fMedName"; xmlData.AppendLine(string.Format("<{0}>{1}</{2}>", fieldName, item.fMedName, fieldName));
                    fieldName = "fBeginDate"; xmlData.AppendLine(string.Format("<{0}>{1}</{2}>", fieldName, string.IsNullOrEmpty(item.fBeginDate) ? "" : Function.Datetime(item.fBeginDate).ToString("yyyy-MM-dd"), fieldName));
                    fieldName = "fEndDate"; xmlData.AppendLine(string.Format("<{0}>{1}</{2}>", fieldName, string.IsNullOrEmpty(item.fEndDate) ? "" : Function.Datetime(item.fEndDate).ToString("yyyy-MM-dd"), fieldName));
                    fieldName = "fTimesOfDay"; xmlData.AppendLine(string.Format("<{0}>{1}</{2}>", fieldName, item.fTimesOfDay, fieldName));
                    fieldName = "fAmount"; xmlData.AppendLine(string.Format("<{0}>{1}</{2}>", fieldName, item.fAmount, fieldName));
                    xmlData.AppendLine("<Row>");
                }
                xmlData.AppendLine("</FormData>");
            }
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
            using (ProxyHms proxy = new ProxyHms())
            {
                if (this.sfData == null)
                    this.sfData = new EntityTnbSfData();
                if (this.sfVo != null)
                    this.sfData.sfId = Function.Dec(this.sfVo.sfId);                
                this.sfData.xmlData = this.GetData();
                decimal sfId = 0;
                bool isNew = this.sfData.sfId <= 0 ? true : false;
                if (proxy.Service.SaveTnbSfRecord(this.sfData, out sfId) > 0)
                {
                    this.IsRequireRefresh = true;
                    if (isNew)
                        this.sfData.sfId = sfId;
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

        private void frmPopup2050201_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.bindingSource.AddNew();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.gvUseMed.FocusedRowHandle >= 0)
            {
                this.bindingSource.RemoveAt(this.gvUseMed.FocusedRowHandle);
            }
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

    #region EntityTNBUseMed
    /// <summary>
    /// EntityTNBUseMed
    /// </summary>
    public class EntityTNBUseMed
    {
        public string fMedName { get; set; }

        public string fBeginDate { get; set; }

        public string fEndDate { get; set; }

        public string fTimesOfDay { get; set; }

        public string fAmount { get; set; }
    }
    #endregion

}
