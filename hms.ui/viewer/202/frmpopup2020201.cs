using Common.Controls;
using Common.Entity;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hms.Entity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using weCare.Core.Entity;
using weCare.Core.Utils;
using System.Linq;

namespace Hms.Ui
{
    /// <summary>
    /// 普通问卷
    /// </summary>
    public partial class frmPopup2020201 : frmBasePopup
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmPopup2020201(List<EntityClientInfo> _lstClientInfo)
        {
            InitializeComponent();
            lstClientInfo = _lstClientInfo;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="_qnVo"></param>
        public frmPopup2020201(EntityQnRecord _qnRecordVo, List<EntityClientInfo> _lstClientInfo)
        {
            InitializeComponent();
            this.qnRecordVo = _qnRecordVo;
            lstClientInfo = _lstClientInfo;
        }

        public frmPopup2020201(EntityQnRecord _qnRecordVo)
        {
            InitializeComponent();
            this.qnRecordVo = _qnRecordVo;
        }
        #endregion

        #region var/property

        EntityClientInfo clientInfo { get; set; }
        public EntityQnRecord qnRecordVo { get; set; }
        public EntityQnData qnData { get; set; }
        enum EnumDataType
        {
            Base = 0,
            Data = 1
        }

        int idx = 0;
        List<string> lstQuest = new List<string>() {"quest01","quest02","quest03","quest04","quest05", "quest06","quest07","quest08","quest09","quest10"};
        Dictionary<int, string> dicXmlData = new Dictionary<int, string>();
        Dictionary<int, IQuest> dicQuestCtrl { get; set; }

        public List<EntityClientInfo> lstClientInfo { get; set; }

        List<EntityDicQnSetting> lstTopic = null;
        List<EntityCtrlLocation> lstCtrlLocation = null;
        List<EntityDicQnSetting> lstItems = null;
        List<DevExpress.XtraEditors.CheckEdit> lstCheck = new List<DevExpress.XtraEditors.CheckEdit>();
        public bool IsRequireRefresh { get; set; }

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
                this.dteQuestDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

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
                

                dicQuestCtrl = new Dictionary<int, IQuest>();
                dicQuestCtrl.Add(0, new Hms.Ui.Quest01());
                dicQuestCtrl.Add(1, new Hms.Ui.Quest02());
                dicQuestCtrl.Add(2, new Hms.Ui.Quest03());
                dicQuestCtrl.Add(3, new Hms.Ui.Quest04());
                dicQuestCtrl.Add(4, new Hms.Ui.Quest05());
                dicQuestCtrl.Add(5, new Hms.Ui.Quest06());
                dicQuestCtrl.Add(6, new Hms.Ui.Quest07());
                dicQuestCtrl.Add(7, new Hms.Ui.Quest08());
                dicQuestCtrl.Add(8, new Hms.Ui.Quest09());
                dicQuestCtrl.Add(9, new Hms.Ui.Quest10());
                if (!dicXmlData.ContainsKey(0))
                    dicXmlData.Add(0, "<FormIdx0></FormIdx0>");
                if (!dicXmlData.ContainsKey(1))
                    dicXmlData.Add(1, "<FormIdx1></FormIdx1>");
                if (!dicXmlData.ContainsKey(2))
                    dicXmlData.Add(2, "<FormIdx2></FormIdx2>");
                if (!dicXmlData.ContainsKey(3))
                    dicXmlData.Add(3, "<FormIdx3></FormIdx3>");
                if (!dicXmlData.ContainsKey(4))
                    dicXmlData.Add(4, "<FormIdx4></FormIdx4>");
                if (!dicXmlData.ContainsKey(5))
                    dicXmlData.Add(5, "<FormIdx5></FormIdx5>");
                if (!dicXmlData.ContainsKey(6))
                    dicXmlData.Add(6, "<FormIdx6></FormIdx6>");
                if (!dicXmlData.ContainsKey(7))
                    dicXmlData.Add(7, "<FormIdx7></FormIdx7>");
                if (!dicXmlData.ContainsKey(8))
                    dicXmlData.Add(8, "<FormIdx8></FormIdx8>");
                if (!dicXmlData.ContainsKey(9))
                    dicXmlData.Add(9, "<FormIdx9></FormIdx9>");

                AddQuestCtrl();

                if (qnRecordVo != null)
                {
                    if (!string.IsNullOrEmpty(qnRecordVo.xmlData))
                    {
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(qnRecordVo.xmlData);
                        XmlNodeList list = document["FormData"].ChildNodes;
                        SetData(EnumDataType.Base, list[0].OuterXml, -1);

                        for (int i = 0; i < dicXmlData.Count; i++)
                        {
                            dicXmlData[i] = list[i + 1].OuterXml;
                            SetData(EnumDataType.Data, dicXmlData[i], i);
                        }
                    }

                    this.glueClient.Enabled = false;
                    this.txtSearch.Enabled = false;
                }
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region AddQuestCtrl
        /// <summary>
        /// AddQuestCtrl
        /// </summary>
        void AddQuestCtrl()
        {
            try
            {
                uiHelper.BeginLoading(this);
                this.timer.Enabled = false;
                this.plUserCtrl.Controls.Clear();
                InitComponent();
            }
            finally
            {
                uiHelper.CloseLoading(this);
                this.timer.Enabled = true;
            }
        }
        #endregion


        #region InitComponent
        /// <summary>
        /// InitComponent
        /// </summary>
        public void InitComponent()
        {
            try
            {
                this.SuspendLayout();
                lstCheck.Clear();
                using (ProxyHms proxy = new ProxyHms())
                {
                    lstCtrlLocation = proxy.Service.GetQnCtrlLocation(lstQuest[idx]);
                }

                using (ProxyHms proxy = new ProxyHms())
                {
                    lstTopic = new List<EntityDicQnSetting>();
                    lstItems = new List<EntityDicQnSetting>();
                    proxy.Service.GetQnCustom(1, out lstTopic, out lstItems);
                }
                int locationX = 0;
                int locationY = 0;
                
                #region quest01
                if (idx == 0)
                {
                    if (lstCtrlLocation != null && lstCtrlLocation.Count > 0)
                    {
                        foreach (var clVo in lstCtrlLocation)
                        {
                            if (clVo.name.Contains("FT"))
                            {
                                DevExpress.XtraEditors.LabelControl lblTopic = new DevExpress.XtraEditors.LabelControl();
                                lblTopic.Name = clVo.name;
                                lblTopic.Text = clVo.text;
                                lblTopic.Font = new System.Drawing.Font("宋体", 9.5F);
                                locationX = clVo.locationX;
                                locationY = clVo.locationY;
                                lblTopic.Location = new System.Drawing.Point(locationX, locationY);
                                this.plUserCtrl.Controls.Add(lblTopic);
                            }
                            if (clVo.name.Contains("FM"))
                            {
                                DevExpress.XtraEditors.LabelControl lblTopic = new DevExpress.XtraEditors.LabelControl();
                                lblTopic.Name = clVo.name;
                                lblTopic.Text = clVo.text;
                                lblTopic.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
                                locationX = clVo.locationX;
                                locationY = clVo.locationY;
                                lblTopic.Location = new System.Drawing.Point(locationX, locationY);
                                this.plUserCtrl.Controls.Add(lblTopic);
                            }
                        }
                    }
                    int parentCount = 0;
                    List<EntityDicQnSetting> lstChildSettings = new List<EntityDicQnSetting>();
                    if (lstTopic != null && lstTopic.Count > 0)
                    {
                        for (int i = 0; i < lstTopic.Count; i++)
                        {
                            EntityDicQnSetting item = lstTopic[i];

                            #region quest01
                            if (item.questName == "quest01")
                            {
                                if (string.IsNullOrEmpty(item.parentFieldId))
                                {
                                    DevExpress.XtraEditors.LabelControl lblTopic = new DevExpress.XtraEditors.LabelControl();
                                    lblTopic.Name = item.fieldId;
                                    lblTopic.Text = lstTopic.Find(r => r.fieldId == item.fieldId).fieldName;
                                    lblTopic.Font = new System.Drawing.Font("宋体", 9.5F);
                                    EntityCtrlLocation ctrLocat = lstCtrlLocation.FindAll(r => r.type == 1)[parentCount];
                                    locationX = ctrLocat.locationX;
                                    locationY = ctrLocat.locationY;
                                    lblTopic.Location = new System.Drawing.Point(locationX, locationY);
                                    this.plUserCtrl.Controls.Add(lblTopic);
                                    
                                    lstChildSettings = lstTopic.FindAll(r => r.parentFieldId == item.fieldId);
                                    if (lstChildSettings.Count > 0)
                                    {
                                        foreach (var childVo in lstChildSettings)
                                        {
                                            DevExpress.XtraEditors.CheckEdit chkAns = new DevExpress.XtraEditors.CheckEdit();
                                            string strEndWith = childVo.fieldId.Substring(4, 2);
                                            chkAns.Text = "";
                                            chkAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                            chkAns.Name = childVo.fieldId;
                                            chkAns.Properties.AccessibleName = childVo.fieldId;
                                            EntityCtrlLocation ctrLocatChild = lstCtrlLocation.Find(r => r.name.Contains(ctrLocat.name) && r.type == 2 && r.name.EndsWith(strEndWith));
                                            locationX = ctrLocatChild.locationX;
                                            locationY = ctrLocatChild.locationY;
                                            chkAns.Width = ctrLocatChild.width;
                                            chkAns.Height = ctrLocatChild.height;
                                            chkAns.Location = new System.Drawing.Point(locationX, locationY);
                                            this.plUserCtrl.Controls.Add(chkAns);
                                        }
                                    }

                                    parentCount++;
                                }
                            }
                            #endregion
                        }
                    }
                    this.plUserCtrl.Height = 680;
                    this.plContent.Height = (this.plUserCtrl.Height + 200);
                }
                #endregion

                #region quest02
                if (idx == 1)
                {
                    int F35Count = 0;
                    int F35Row = (lstTopic.FindAll(r => r.fieldId.Contains("F035")).Count) / 6;
                    int F35Y = 0;
                    List<EntityDicQnSetting> lstChildSettings = new List<EntityDicQnSetting>();
                    if (lstTopic != null && lstTopic.Count > 0)
                    {
                        for (int i = 0; i < lstTopic.Count; i++)
                        {
                            EntityDicQnSetting item = lstTopic[i];
                            if (item.questName == lstQuest[idx])
                            {
                                if (item.fieldId.Contains("F035"))
                                {
                                    DevExpress.XtraEditors.CheckEdit chkAns = new DevExpress.XtraEditors.CheckEdit();
                                    chkAns.Text = item.fieldName;
                                    chkAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                    chkAns.Name = item.fieldId;
                                    chkAns.Properties.AccessibleName = item.fieldId;
                                    EntityCtrlLocation ctrLocat = lstCtrlLocation.FindAll(r => r.name.Contains("F035"))[F35Count];
                                    locationX = ctrLocat.locationX;
                                    locationY = ctrLocat.locationY;
                                    F35Y += locationY;
                                    chkAns.Width = ctrLocat.width;
                                    chkAns.Height = ctrLocat.height;
                                    chkAns.Location = new System.Drawing.Point(locationX, locationY);
                                    this.plUserCtrl.Controls.Add(chkAns);
                                    F35Count++;
                                }
                            }
                        }

                        for (int i2 = 0; i2 < lstTopic.Count; i2++)
                        {
                            EntityDicQnSetting itemVo = lstTopic[i2];
                            if (itemVo.questName == lstQuest[idx])
                            {
                                if (string.IsNullOrEmpty(itemVo.parentFieldId) && !itemVo.fieldId.Contains("F035"))
                                {
                                    DevExpress.XtraEditors.LabelControl lblTopic = new DevExpress.XtraEditors.LabelControl();
                                    lblTopic.Name = itemVo.fieldId;
                                    lblTopic.Text = itemVo.fieldName;
                                    lblTopic.Font = new System.Drawing.Font("宋体", 9.5F);
                                    EntityCtrlLocation ctrLocat = lstCtrlLocation.Find(r => r.name == itemVo.fieldId);
                                    locationX = ctrLocat.locationX;
                                    locationY = ctrLocat.locationY - F35Y + 80;
                                    lblTopic.Location = new System.Drawing.Point(locationX, locationY);
                                    this.plUserCtrl.Controls.Add(lblTopic);

                                    lstChildSettings = lstTopic.FindAll(r => r.parentFieldId == itemVo.fieldId);
                                    if (lstChildSettings.Count > 0)
                                    {
                                        foreach (var childVo in lstChildSettings)
                                        {
                                            DevExpress.XtraEditors.CheckEdit chkAns = new DevExpress.XtraEditors.CheckEdit();
                                            string strEndWith = childVo.fieldId.Substring(4, 2);
                                            chkAns.Text = childVo.fieldName;
                                            chkAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                            chkAns.Name = childVo.fieldId;
                                            chkAns.Properties.AccessibleName = childVo.fieldId;
                                            EntityCtrlLocation ctrLocatChild = lstCtrlLocation.Find(r => r.name == childVo.fieldId);
                                            locationX = ctrLocatChild.locationX;
                                            locationY = ctrLocatChild.locationY - F35Y + 80;
                                            chkAns.Width = ctrLocatChild.width;
                                            chkAns.Height = ctrLocatChild.height;
                                            chkAns.Location = new System.Drawing.Point(locationX, locationY);
                                            this.plUserCtrl.Controls.Add(chkAns);
                                        }
                                    }
                                }
                            }
                        }

                    }

                    if (lstCtrlLocation != null && lstCtrlLocation.Count > 0)
                    {
                        foreach (var clVo in lstCtrlLocation)
                        {
                            DevExpress.XtraEditors.LabelControl lblTopic = new DevExpress.XtraEditors.LabelControl();
                            lblTopic.Name = clVo.name;
                            lblTopic.Text = clVo.text;
                            lblTopic.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
                            locationX = clVo.locationX;
                            if (clVo.name == "FM0201")
                            {
                                locationY = clVo.locationY;
                            }
                            else if (clVo.name.Contains("FM"))
                            {
                                locationY = clVo.locationY - F35Y + 80;
                            }
                            else
                                continue;

                            lblTopic.Location = new System.Drawing.Point(locationX, locationY);
                            this.plUserCtrl.Controls.Add(lblTopic);
                        }
                    }

                    this.plUserCtrl.Height = 1000;
                    this.plContent.Height = (this.plUserCtrl.Height + 200);
                }

                #endregion

                #region quest03  quest04 quest05 quest06 quest07 quest08 quest09 quest10
                if (idx == 2 ||idx == 3 || idx == 4 || idx == 5 || idx == 6 || idx == 7 || idx == 8 || idx == 9)
                {
                    if (lstCtrlLocation != null && lstCtrlLocation.Count > 0)
                    {
                        foreach (var clVo in lstCtrlLocation)
                        {
                            if (clVo.name.Contains("FM"))
                            {
                                DevExpress.XtraEditors.LabelControl lblTopic = new DevExpress.XtraEditors.LabelControl();
                                lblTopic.Name = clVo.name;
                                lblTopic.Text = clVo.text;
                                if(clVo.name == "FM0902" || clVo.name == "FM0903")
                                {
                                    lblTopic.Font = new System.Drawing.Font("宋体", 9.5F);
                                    lblTopic.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                    lblTopic.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
                                locationX = clVo.locationX;
                                locationY = clVo.locationY;
                                lblTopic.Location = new System.Drawing.Point(locationX, locationY);
                                this.plUserCtrl.Controls.Add(lblTopic);
                            }
                        }
                    }

                    List<EntityDicQnSetting> lstChildSettings = new List<EntityDicQnSetting>();
                    if (lstTopic != null && lstTopic.Count > 0)
                    {
                        for (int i = 0; i < lstTopic.Count; i++)
                        {
                            EntityDicQnSetting item = lstTopic[i];
                            if (item.questName == lstQuest[idx])
                            {
                                if (string.IsNullOrEmpty(item.parentFieldId))
                                {
                                    DevExpress.XtraEditors.LabelControl lblTopic = new DevExpress.XtraEditors.LabelControl();
                                    lblTopic.Name = item.fieldId;
                                    lblTopic.Text = item.fieldName;
                                    lblTopic.Font = new System.Drawing.Font("宋体", 9.5F);
                                    EntityCtrlLocation ctrLocat = lstCtrlLocation.Find(r => r.name == item.fieldId);
                                    locationX = ctrLocat.locationX;
                                    locationY = ctrLocat.locationY;
                                    lblTopic.Location = new System.Drawing.Point(locationX, locationY);
                                    this.plUserCtrl.Controls.Add(lblTopic);

                                    lstChildSettings = lstTopic.FindAll(r => r.parentFieldId == item.fieldId);
                                    if (lstChildSettings.Count > 0)
                                    {
                                        foreach (var childVo in lstChildSettings)
                                        {
                                            if(childVo.typeId != "3")
                                            {
                                                DevExpress.XtraEditors.CheckEdit chkAns = new DevExpress.XtraEditors.CheckEdit();
                                                string strEndWith = childVo.fieldId.Substring(4, 2);
                                                chkAns.Text = childVo.fieldName;
                                                chkAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                                chkAns.Name = childVo.fieldId;
                                                chkAns.Properties.AccessibleName = childVo.fieldId;
                                                EntityCtrlLocation ctrLocatChild = lstCtrlLocation.Find(r => r.name == childVo.fieldId);
                                                locationX = ctrLocatChild.locationX;
                                                locationY = ctrLocatChild.locationY;
                                                chkAns.Width = ctrLocatChild.width;
                                                chkAns.Height = ctrLocatChild.height;
                                                chkAns.Location = new System.Drawing.Point(locationX, locationY);
                                                chkAns.CheckedChanged += new EventHandler(iChk_CheckedChanged);
                                                if (!string.IsNullOrEmpty(childVo.parentFieldId) && item.typeId == "1")
                                                {
                                                    chkAns.Properties.AccessibleName = childVo.parentFieldId;
                                                    lstCheck.Add(chkAns);
                                                }
                                                this.plUserCtrl.Controls.Add(chkAns);
                                            }
                                            else
                                            {
                                                DevExpress.XtraEditors.TextEdit txtAns = new DevExpress.XtraEditors.TextEdit();
                                                string strEndWith = childVo.fieldId.Substring(4, 2);
                                                txtAns.Text = childVo.fieldName;
                                                txtAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                                txtAns.Name = childVo.fieldId;
                                                txtAns.Properties.AccessibleName = childVo.fieldId;
                                                EntityCtrlLocation ctrLocatChild = lstCtrlLocation.Find(r => r.name == childVo.fieldId);
                                                locationX = ctrLocatChild.locationX;
                                                locationY = ctrLocatChild.locationY;
                                                txtAns.Width = ctrLocatChild.width;
                                                txtAns.Height = ctrLocatChild.height;
                                                txtAns.Location = new System.Drawing.Point(locationX, locationY);
                                                this.plUserCtrl.Controls.Add(txtAns);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    this.plUserCtrl.Height = 2000;
                    this.plContent.Height = (this.plUserCtrl.Height + 200);
                }
                #endregion
            }
            catch (Exception ex)
            {
                DialogBox.Msg(ex.Message);
            }
            finally
            {
                this.ResumeLayout();
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        void Save()
        {
            timer.Enabled = false;

            if( string.IsNullOrEmpty(dteBirthday.Text))
            {
                DialogBox.Msg("出生日期不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(cboSex.Text))
            {
                DialogBox.Msg("性别不能为空！");
                return;
            }

            using (ProxyHms proxy = new ProxyHms())
            {
                string xmlData = string.Empty;
                xmlData += "<FormData>" + Environment.NewLine;
                xmlData += GetData(EnumDataType.Base,-1);
                if (dicXmlData != null)
                {
                    foreach (var dic in dicXmlData)
                    {
                        xmlData += dic.Value + Environment.NewLine;
                    }
                }
                xmlData += "</FormData>";
                this.qnData = new EntityQnData();
                if (this.qnRecordVo == null)
                {
                    this.qnRecordVo = new EntityQnRecord();
                    qnRecordVo.clientNo = clientInfo.clientNo;
                }
                    
                if (this.qnRecordVo != null)
                    this.qnData.recId = qnRecordVo.recId;
                qnData.xmlData = xmlData;
                qnRecordVo.qnType = 1;
                qnRecordVo.qnName = "常规问卷";
                qnRecordVo.qnSource = 1; 
                qnRecordVo.qnDate = Function.Datetime(dteQuestDate.Text);
                qnRecordVo.xmlData = xmlData;
                qnRecordVo.qnId = 1;
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
            timer.Enabled = true;
        }
        #endregion

        #region GetData
        /// <summary>
        /// GetData
        /// </summary>
        /// <returns></returns>
        string GetData(EnumDataType dataType,int idx)
        {
            List<EntityControl> lstControls = new List<EntityControl>();
            StringBuilder xmlData = new StringBuilder();
            string fieldName = string.Empty;
            Panel panel;
            if (dataType == EnumDataType.Data)
                panel = this.plUserCtrl;
            else
                panel = this.plTitle;
            foreach (Control ctrl in panel.Controls)
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
                    fieldName = (ctrl as DevExpress.XtraEditors.CheckEdit).Name;
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
            if (idx == -1)
                xmlData.AppendLine("<FormBse>");
            if (idx== 0)
                xmlData.AppendLine("<FormIdx0>");
            if(idx ==  1)
                xmlData.AppendLine("<FormIdx1>");
            if(idx ==  2)
                xmlData.AppendLine("<FormIdx2>");
            if (idx == 3)
                xmlData.AppendLine("<FormIdx3>");
            if (idx == 4)
                xmlData.AppendLine("<FormIdx4>");
            if (idx == 5)
                xmlData.AppendLine("<FormIdx5>");
            if (idx == 6)
                xmlData.AppendLine("<FormIdx6>");
            if (idx == 7)
                xmlData.AppendLine("<FormIdx7>");
            if (idx == 8)
                xmlData.AppendLine("<FormIdx8>");
            if (idx == 9)
                xmlData.AppendLine("<FormIdx9>");

            foreach (EntityControl item in lstControls)
            {
                xmlData.AppendLine(string.Format("<{0}>{1}</{2}>", item.FieldName, item.Value, item.FieldName));
            }
            if (idx == -1)
                xmlData.AppendLine("</FormBse>");
            if (idx == 0)
                xmlData.AppendLine("</FormIdx0>");
            if (idx == 1)
                xmlData.AppendLine("</FormIdx1>");
            if (idx == 2)
                xmlData.AppendLine("</FormIdx2>");
            if (idx == 3)
                xmlData.AppendLine("</FormIdx3>");
            if (idx == 4)
                xmlData.AppendLine("</FormIdx4>");
            if (idx == 5)
                xmlData.AppendLine("</FormIdx5>");
            if (idx == 6)
                xmlData.AppendLine("</FormIdx6>");
            if (idx == 7)
                xmlData.AppendLine("</FormIdx7>");
            if (idx == 8)
                xmlData.AppendLine("</FormIdx8>");
            if (idx == 9)
                xmlData.AppendLine("</FormIdx9>");

            return xmlData.ToString();
        }
        #endregion

        #region SetData
        /// <summary>
        /// SetData
        /// </summary>
        /// <param name="xmlData"></param>
        void SetData(EnumDataType dataType, string xmlData,int idx)
        {
            if (string.IsNullOrEmpty(xmlData)) return;
            string nodeName = string.Empty;
            if (idx == -1)
                nodeName = "FormBse";
            if (idx == 0)
                nodeName = "FormIdx0";
            if (idx == 1)
                nodeName = "FormIdx1";
            if (idx == 2)
                nodeName = "FormIdx2";
            if (idx == 3)
                nodeName = "FormIdx3";
            if (idx == 4)
                nodeName = "FormIdx4";
            if (idx == 5)
                nodeName = "FormIdx5";
            if (idx == 6)
                nodeName = "FormIdx6";
            if (idx == 7)
                nodeName = "FormIdx7";
            if (idx == 8)
                nodeName = "FormIdx8";
            if (idx == 9)
                nodeName = "FormIdx9";

            Dictionary<string, string> dicData = Function.ReadXmlNodes(xmlData, nodeName);
            if (dicData != null && dicData.Count > 0)
            {
                string fieldName = string.Empty;
                Panel panel;
                if (dataType == EnumDataType.Data)
                    panel = this.plUserCtrl;
                else
                    panel = this.plTitle;
                foreach (Control ctrl in panel.Controls)
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
            }
        }
        #endregion

        #endregion

        #region event

        private void frmPopup2090101_Load(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.Init();
            this.timer.Enabled = true;
        }

        private void blbiPrePage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (idx > 0)
            {
                --idx;
                AddQuestCtrl();
                SetData(EnumDataType.Data, dicXmlData[idx],idx);
            }
        }

        private void blbiNextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (idx < 9)
            {
                ++idx;
                AddQuestCtrl();
                SetData(EnumDataType.Data, dicXmlData[idx], idx);
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

        #region LableClick Event

        private void lblQ01_Click(object sender, EventArgs e)
        {
            idx = 0;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ02_Click(object sender, EventArgs e)
        {
            idx = 1;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ03_Click(object sender, EventArgs e)
        {
            idx = 1;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ04_Click(object sender, EventArgs e)
        {
            idx = 2;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ05_Click(object sender, EventArgs e)
        {
            idx = 2;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ06_Click(object sender, EventArgs e)
        {
            idx = 3;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ07_Click(object sender, EventArgs e)
        {
            idx = 3;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ08_Click(object sender, EventArgs e)
        {
            idx = 4;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ09_Click(object sender, EventArgs e)
        {
            idx = 4;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ10_Click(object sender, EventArgs e)
        {
            idx = 5;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ11_Click(object sender, EventArgs e)
        {
            idx = 6;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ12_Click(object sender, EventArgs e)
        {
            idx = 7;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ13_Click(object sender, EventArgs e)
        {
            idx = 8;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void lblQ14_Click(object sender, EventArgs e)
        {
            idx = 9;
            AddQuestCtrl();
            SetData(EnumDataType.Data, dicXmlData[idx], idx);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!dicXmlData.ContainsKey(idx))
                dicXmlData.Add(idx, GetData(EnumDataType.Data, idx));
            else
                dicXmlData[idx] = GetData(EnumDataType.Data, idx);
        }

        #region iChk_CheckedChanged
        /// <summary>
        /// iChk_CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iChk_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit iChk = sender as DevExpress.XtraEditors.CheckEdit;
            // 同时勾选
            if (iChk.Checked)
            {
                // 根据分组属性控制只选一个
                foreach (DevExpress.XtraEditors.CheckEdit chk in lstCheck)
                {
                    if (chk != iChk && chk.Properties.AccessibleName == iChk.Properties.AccessibleName)
                    {
                        chk.Checked = false;
                        ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                        ((DevExpress.XtraEditors.CheckEdit)sender).Update();
                    }
                }
                ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                ((DevExpress.XtraEditors.CheckEdit)sender).Update();
            }
            else
            {
                ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                ((DevExpress.XtraEditors.CheckEdit)sender).Update();
            }
        }
        #endregion

        #endregion

        

        private void blbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPopup2020202 frm = new frmPopup2020202(qnRecordVo);
            frm.ShowDialog();
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

        private void glueClient_EditValueChanged(object sender, EventArgs e)
        {
            if (qnRecordVo != null)
                return;
            string clientNo = this.glueClient.EditValue.ToString();
            clientInfo = lstClientInfo.Find(r => r.clientNo == clientNo);
            if (clientInfo != null)
            {
                this.cboSex.Text = clientInfo.sex;
                this.txtMobile.Text = clientInfo.mobile;
                this.txtConnectName.Text = clientInfo.contactName;
                this.txtConnectPhone.Text = clientInfo.contactNameMobile;
                this.txtCompany.Text = clientInfo.company;
                this.txtTelephone.Text = clientInfo.telephone;
                this.dteBirthday.Text = clientInfo.strBirthday;
                this.txtIdCard.Text = clientInfo.cardNo;
                this.txtAddress.Text = clientInfo.address;

            }
        }
        #endregion
    }

    #region EntitySfControl
    /// <summary>
    /// EntitySfControl
    /// </summary>
    public class EntityControl : IComparable
    {
        public string FieldName { get; set; }

        public string Value { get; set; }

        public int TabIndex { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is EntitySfControl)
            {
                return this.TabIndex.CompareTo(((EntityControl)obj).TabIndex);
            }
            return 0;
        }
    }
    #endregion

}
