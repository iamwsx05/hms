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
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class frmPopup2020702 : frmBasePopup
    {
        public frmPopup2020702(EntityDicQnMain _qnVo)
        {
            InitializeComponent();
            qnVo = _qnVo;
        }

        public frmPopup2020702(EntityQnRecord _qnRecordVo)
        {
            InitializeComponent();
            qnRecordVo = _qnRecordVo;
        }

        #region var
        EntityDicQnMain qnVo { get; set; }
        EntityClientInfo clientInfo { get; set; }
        EntityQnRecord qnRecordVo { get; set; }
        EntityQnData qnData { get; set; }
        public List<EntityClientInfo> lstClientInfo { get; set; }
        List<DevExpress.XtraEditors.CheckEdit> lstCheck = new List<DevExpress.XtraEditors.CheckEdit>();

        public bool IsRequireRefresh = false;
        #endregion

        #region methods

        #region InitComponent
        /// <summary>
        /// InitComponent
        /// </summary>
        public void InitComponent()
        {
            try
            {
                List<EntityDicQnSetting> lstTopic = null;
                List<EntityDicQnSetting> lstItems = null;
                dteQuestDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.glueClient.Properties.ValueMember = "clientNo";
                this.glueClient.Properties.DisplayMember = "clientName";
                this.glueClient.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                this.glueClient.Properties.View.BestFitColumns();
                this.glueClient.Properties.ShowFooter = false;
                this.glueClient.Properties.View.OptionsView.ShowAutoFilterRow = true;
                this.glueClient.Properties.AutoComplete = false;
                this.glueClient.Properties.ImmediatePopup = true;
                this.glueClient.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                this.glueClient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

                using (ProxyHms svc = new ProxyHms())
                {
                    if(qnVo != null)
                        svc.Service.GetQnCustom(qnVo.qnId, out lstTopic, out lstItems);
                    else 
                        svc.Service.GetQnCustom(qnRecordVo.qnId, out lstTopic, out lstItems);
                }

                int intY = 10;
                if (lstTopic != null && lstTopic.Count > 0)
                {
                    if (this.qnVo != null)
                    {
                        lblTitle.Text = this.qnVo.qnName;
                       
                    }
                    else
                    {
                        lblTitle.Text = this.qnRecordVo.qnName;
                        this.glueClient.Enabled = false;
                        this.txtSearch.Enabled = false;
                    }

                    EntityDicQnSetting item = null;
                    EntityDicQnSetting item2 = null;
                    for (int i = 0; i < lstTopic.Count; i++)
                    {
                        item = lstTopic[i];
                        DevExpress.XtraEditors.LabelControl lblTopic = new DevExpress.XtraEditors.LabelControl();
                        lblTopic.Name = item.fieldId;
                        lblTopic.Text = Convert.ToString(i + 1) + "、" + item.fieldName;
                        lblTopic.Font = new System.Drawing.Font("宋体", 9.5F);
                        lblTopic.Location = new Point(80, intY);
                        this.plUserCtrl.Controls.Add(lblTopic);

                        List<EntityDicQnSetting> lstCtrls = lstItems.FindAll(t => t.parentFieldId == item.fieldId);
                        if (lstCtrls != null && lstCtrls.Count > 0)
                        {
                            intY += 40;
                            int tmpX = 0;
                            int tmpY = intY;
                            for (int j = 0; j < lstCtrls.Count; j++)
                            {
                                tmpY = intY + 40 * (j / 5);
                                tmpX = 100 + (j % 5) * 150;

                                item2 = lstCtrls[j];
                                if(item2.typeId == "3")
                                {
                                    DevExpress.XtraEditors.TextEdit txtAns = new DevExpress.XtraEditors.TextEdit();
                                    txtAns.Properties.AccessibleName = item2.fieldId;
                                    txtAns.Location = new Point(100, intY);
                                    txtAns.Width = 600;
                                    txtAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                    txtAns.ForeColor = Color.Blue;
                                    this.plUserCtrl.Controls.Add(txtAns);
                                }
                                else
                                {
                                    DevExpress.XtraEditors.CheckEdit chkAns = new DevExpress.XtraEditors.CheckEdit();
                                    chkAns.Name = item2.fieldId;
                                    chkAns.Text = item2.fieldName;
                                    chkAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                    chkAns.Width = item2.fieldName.Length + 85;
                                    chkAns.Location = new Point(tmpX, tmpY);
                                    chkAns.CheckedChanged += new EventHandler(iChk_CheckedChanged);
                                    if (!string.IsNullOrEmpty(item2.parentFieldId) && item.typeId == "1")
                                    {
                                        chkAns.Properties.AccessibleName = item2.parentFieldId;
                                        lstCheck.Add(chkAns);
                                    }
                                    this.plUserCtrl.Controls.Add(chkAns);
                                }              
                            }
                            intY = tmpY;
                            intY += 60;
                        }
                        else
                        {
                            if (item.typeId == "3")
                            {
                                intY += 30;
                                DevExpress.XtraEditors.TextEdit txtAns = new DevExpress.XtraEditors.TextEdit();
                                txtAns.Properties.AccessibleName = item2.fieldId;
                                txtAns.Location = new Point(100, intY);
                                txtAns.Width = 600;
                                txtAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                txtAns.ForeColor = Color.Blue;
                                this.plUserCtrl.Controls.Add(txtAns);
                            }
                            else
                            {
                                intY += 60;
                            }
                        }
                    }
                }
                plUserCtrl.Height = intY + 100;
                plContent.Height = intY + 200;
                this.Height = intY + 230;

                if (qnRecordVo != null)
                    SetData(qnRecordVo.xmlData);
            }
            catch (Exception ex)
            {
                DialogBox.Msg(ex.Message);
            }
            finally
            {
                
            }
        }
        #endregion

        #region iChk_CheckedChanged
        /// <summary>
        /// iChk_CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iChk_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit iChk = sender as DevExpress.XtraEditors.CheckEdit;
            if (iChk.Checked)
            {
                // 根据分组属性控制只选一个
                foreach (DevExpress.XtraEditors.CheckEdit chk in lstCheck)
                {
                    if (chk != iChk && chk.Properties.AccessibleName == iChk.Properties.AccessibleName)
                    {
                        chk.Checked = false;
                        ((DevExpress.XtraEditors.CheckEdit)sender).Properties.Appearance.ForeColor = Color.Black;
                        ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                        ((DevExpress.XtraEditors.CheckEdit)sender).Update();
                    }
                }
                ((DevExpress.XtraEditors.CheckEdit)sender).Properties.Appearance.ForeColor = Color.Blue;
                ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                ((DevExpress.XtraEditors.CheckEdit)sender).Update();
            }
            else
            {
                ((DevExpress.XtraEditors.CheckEdit)sender).Properties.Appearance.ForeColor = Color.Black;
                ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                ((DevExpress.XtraEditors.CheckEdit)sender).Update();
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
            List<EntityControl> lstControls = new List<EntityControl>();
            StringBuilder xmlData = new StringBuilder();
            string fieldName = string.Empty;
            xmlData.AppendLine("<FormData>");
            foreach (Control ctrl in plUserCtrl.Controls)
            {
                if (ctrl is Common.Controls.LookUpEdit)
                {
                    fieldName = (ctrl as Common.Controls.LookUpEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        lstControls.Add(new EntityControl()
                        {
                            FieldName = fieldName,
                            Value = (ctrl as Common.Controls.LookUpEdit).Text.Trim(),
                            TabIndex = (ctrl as Common.Controls.LookUpEdit).TabIndex
                        });
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.GridLookUpEdit)
                {
                    fieldName = (ctrl as DevExpress.XtraEditors.GridLookUpEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        lstControls.Add(new EntityControl()
                        {
                            FieldName = fieldName,
                            Value = (ctrl as DevExpress.XtraEditors.GridLookUpEdit).Text.Trim(),
                            TabIndex = (ctrl as DevExpress.XtraEditors.GridLookUpEdit).TabIndex
                        });
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.TextEdit)
                {
                    fieldName = (ctrl as DevExpress.XtraEditors.TextEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        lstControls.Add(new EntityControl()
                        {
                            FieldName = fieldName,
                            Value = (ctrl as DevExpress.XtraEditors.TextEdit).Text.Trim(),
                            TabIndex = (ctrl as DevExpress.XtraEditors.TextEdit).TabIndex
                        });
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.CheckEdit)
                {
                    fieldName =  (ctrl as DevExpress.XtraEditors.CheckEdit).Name;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        lstControls.Add(new EntityControl()
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
                        lstControls.Add(new EntityControl()
                        {
                            FieldName = fieldName,
                            Value = (ctrl as DevExpress.XtraEditors.DateEdit).Text.Trim(),
                            TabIndex = (ctrl as DevExpress.XtraEditors.DateEdit).TabIndex
                        });
                    }
                }
            }

            foreach (Control ctrl in plTitle.Controls)
            {
                if (ctrl is Common.Controls.LookUpEdit)
                {
                    fieldName = (ctrl as Common.Controls.LookUpEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        lstControls.Add(new EntityControl()
                        {
                            FieldName = fieldName,
                            Value = (ctrl as Common.Controls.LookUpEdit).Text.Trim(),
                            TabIndex = (ctrl as Common.Controls.LookUpEdit).TabIndex
                        });
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.GridLookUpEdit)
                {
                    fieldName = (ctrl as DevExpress.XtraEditors.GridLookUpEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        lstControls.Add(new EntityControl()
                        {
                            FieldName = fieldName,
                            Value = (ctrl as DevExpress.XtraEditors.GridLookUpEdit).Text.Trim(),
                            TabIndex = (ctrl as DevExpress.XtraEditors.GridLookUpEdit).TabIndex
                        });
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.TextEdit)
                {
                    fieldName = (ctrl as DevExpress.XtraEditors.TextEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        lstControls.Add(new EntityControl()
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
                        lstControls.Add(new EntityControl()
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
                        lstControls.Add(new EntityControl()
                        {
                            FieldName = fieldName,
                            Value = (ctrl as DevExpress.XtraEditors.DateEdit).Text.Trim(),
                            TabIndex = (ctrl as DevExpress.XtraEditors.DateEdit).TabIndex
                        });
                    }
                }
            }
            lstControls.Sort();
            
            foreach (EntityControl item in lstControls)
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
            string fieldName = string.Empty;
            Dictionary<string, string> dicData = Function.ReadXmlNodes(xmlData, "FormData");

            foreach (Control ctrl in plUserCtrl.Controls)
            {
                if (ctrl is Common.Controls.LookUpEdit)
                {
                    fieldName = (ctrl as Common.Controls.LookUpEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName) && dicData.ContainsKey(fieldName))
                    {
                        (ctrl as Common.Controls.LookUpEdit).SetDisplayText<EntityCodeOperator>(dicData[fieldName]);
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.GridLookUpEdit)
                {
                    fieldName = (ctrl as DevExpress.XtraEditors.GridLookUpEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName) && dicData.ContainsKey(fieldName))
                    {
                        (ctrl as DevExpress.XtraEditors.GridLookUpEdit).Properties.NullText = dicData[fieldName];
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
                    fieldName = (ctrl as DevExpress.XtraEditors.CheckEdit).Name;
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

            foreach (Control ctrl in plTitle.Controls)
            {
                if (ctrl is Common.Controls.LookUpEdit)
                {
                    fieldName = (ctrl as Common.Controls.LookUpEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName) && dicData.ContainsKey(fieldName))
                    {
                        (ctrl as Common.Controls.LookUpEdit).SetDisplayText<EntityCodeOperator>(dicData[fieldName]);
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.GridLookUpEdit)
                {
                    fieldName = (ctrl as DevExpress.XtraEditors.GridLookUpEdit).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName) && dicData.ContainsKey(fieldName))
                    {
                        (ctrl as DevExpress.XtraEditors.GridLookUpEdit).Properties.NullText = dicData[fieldName];
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
        #endregion

        #endregion


        #region event
        private void frmpopup2020702_Load(object sender, EventArgs e)
        {
            InitComponent();
        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (ProxyHms proxy = new ProxyHms())
            {
                this.qnData = new EntityQnData();
                if (this.qnRecordVo == null)
                {
                    this.qnRecordVo = new EntityQnRecord();
                    qnRecordVo.clientNo = clientInfo.clientNo;
                }

                if (this.qnRecordVo != null)
                    this.qnData.recId = qnRecordVo.recId;
                qnData.xmlData = GetData();
                qnRecordVo.qnType = 2;
                qnRecordVo.qnSource = 1;
                qnRecordVo.qnDate = Function.Datetime(dteQuestDate.Text);
                qnRecordVo.xmlData = GetData();
                if(qnVo != null)
                {
                    qnRecordVo.qnName = qnVo.qnName;
                    qnRecordVo.qnId = qnVo.qnId;
                }
                    
                decimal recId = 0;
                bool isNew = this.qnRecordVo.recId <= 0 ? true : false;
                if (proxy.Service.SaveQnRecord(this.qnRecordVo, this.qnData, out recId) > 0)
                {
                    this.IsRequireRefresh = true;
                    if (isNew)
                    {
                        this.qnRecordVo.recId = recId;
                        this.qnData.recId = recId;
                    }

                    DialogBox.Msg("保存成功！");
                }
                else
                {
                    DialogBox.Msg("保存失败。");
                }
            }
        }


        private void blbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPopup2020202 frm = new frmPopup2020202(qnRecordVo);
            frm.ShowDialog();
        }
        


        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void glueClient_EditValueChanged(object sender, EventArgs e)
        {
            if (qnRecordVo != null)
                return;
            string clientNo = this.glueClient.EditValue.ToString();
            clientInfo = lstClientInfo.Find(r => r.clientNo == clientNo);
            if (clientInfo != null)
            {
                this.txtSex.Text = clientInfo.sex;
                this.dteBirthday.Text = clientInfo.strBirthday;
                this.txtClientNo.Text = clientInfo.clientNo;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string search = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(search))
                {
                    return;
                }
                using (ProxyHms proxy = new ProxyHms())
                {
                    lstClientInfo = proxy.Service.GetClientInfos(search);
                }
                glueClient.Properties.DataSource = lstClientInfo;
                glueClient.ShowPopup();
            }
        }
        #endregion
    }
}
