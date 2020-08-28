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
    /// 糖尿病评估
    /// </summary>
    public partial class frmPopup2050202 : frmBasePopup
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmPopup2050202(EntityTnbRecord _tnbRecord)
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            if (!DesignMode)
            {
                this.lueEnaOper.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueEnaOper.LookAndFeel.SkinName = "Black";
                this.lueRecorder.LookAndFeel.UseDefaultLookAndFeel = false;
                this.lueRecorder.LookAndFeel.SkinName = "Black";
                this.tnbRecord = _tnbRecord;
            }
        }

        public frmPopup2050202(EntityTnbPg _pgVo)
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
        #endregion

        #region var/property

        public EntityTnbPg pgVo { get; set; }
        public EntityTnbRecord tnbRecord { get; set; }
        public EntityTnbPgData pgData { get; set; }

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
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkKfxt01, chkKfxt02, chkKfxt03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkCh2hxt01, chkCh2hxt02, chkCh2hxt03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkThxhdb01, chkThxhdb02, chkThxhdb03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkXy01, chkXy02, chkXy03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkTzzs01, chkTzzs02, chkTzzs03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkZdgc01, chkZdgc02, chkZdgc03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkGysz01, chkGysz02, chkGysz03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkDmdzdbdgc01, chkDmdzdbdgc02, chkDmdzdbdgc03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkGmdzdbdgc01, chkGmdzdbdgc02, chkGmdzdbdgc03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkShfs01, chkShfs02, chkShfs03 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkBfz01, chkBfz02, chkBfz03, chkBfz04, chkBfz05 });
            lstSingleCheck.Add(new List<DevExpress.XtraEditors.CheckEdit>() { chkManageLevel01, chkManageLevel02, chkManageLevel03 });

            lstMultiCheck = new List<List<DevExpress.XtraEditors.CheckEdit>>();
            SetCheckedChanged();

            if (tnbRecord != null)
            {
                this.txtPatName.Text = this.tnbRecord.clientName;
                this.txtClientNo.Text = this.tnbRecord.clientNo;
                this.txtSex.Text = this.tnbRecord.sex;
                this.txtAge.Text = this.tnbRecord.age;
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
                pgVo = new EntityTnbPg();
                pgVo.recId = tnbRecord.recId;
            }
            if (tnbRecord == null)
            {
                tnbRecord = new EntityTnbRecord();
                tnbRecord.recId = pgVo.recId;
            }
           
            if (chkManageLevel01.Checked == true)
                pgVo.manageLevel = "1";
            if (chkManageLevel02.Checked == true)
                pgVo.manageLevel = "2";
            if (chkManageLevel03.Checked == true)
                pgVo.manageLevel = "3";
            pgVo.evaluator = lueEnaOper.EditValue.ToString();
            pgVo.evaDate = Function.Datetime(dteEnaDate.Text);
            pgData = new EntityTnbPgData();
            pgData.xmlData = GetData();
            decimal pgId = 0;
            bool isNew = this.pgVo.pgId <= 0 ? true : false;
            using (ProxyHms proxy = new ProxyHms())
            {
                if (proxy.Service.SaveTnbPgRecord(this.tnbRecord, this.pgVo, this.pgData, out pgId) > 0)
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

        private void frmPopup2050202_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void blbiImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show(this.GetData());
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
