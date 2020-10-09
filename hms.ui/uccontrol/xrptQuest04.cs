using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using weCare.Core.Entity;
using Hms.Entity;
using System.Xml;
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class xrptQuest04 : DevExpress.XtraReports.UI.XtraReport
    {
        public xrptQuest04(EntityQnRecord qnRecord)
        {
            InitializeComponent();
            AddQuestCtrl(qnRecord);
        }
        List<string> lstQuest = new List<string>() { "quest01", "quest02", "quest03", "quest04", "quest05", "quest06", "quest07", "quest08", "quest09", "quest10" };
        Dictionary<string, string> dicData = new Dictionary<string, string>();
        List<EntityDicQnSetting> lstTopic = null;
        List<EntityCtrlLocation> lstCtrlLocation = null;
        List<EntityDicQnSetting> lstItems = null;
        string xmlData = string.Empty;

        #region AddQuestCtrl
        /// <summary>
        /// AddQuestCtrl
        /// </summary>
        void AddQuestCtrl(EntityQnRecord qnRecord)
        {
            try
            {
                if (qnRecord != null)
                {
                    if (!string.IsNullOrEmpty(qnRecord.xmlData))
                    {
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(qnRecord.xmlData);
                        XmlNodeList list = document["FormData"].ChildNodes;
                        xmlData = list[4].OuterXml;
                        dicData = Function.ReadXML(xmlData);
                    }
                }
                using (ProxyHms proxy = new ProxyHms())
                {
                    lstCtrlLocation = proxy.Service.GetQnCtrlLocation(lstQuest[3], qnRecord.qnId);
                    lstTopic = new List<EntityDicQnSetting>();
                    lstItems = new List<EntityDicQnSetting>();
                    proxy.Service.GetQnCustom(1, out lstTopic, out lstItems);
                }

                int locationX = 0;
                int locationY = 0;
                if (lstCtrlLocation != null && lstCtrlLocation.Count > 0)
                {
                    foreach (var clVo in lstCtrlLocation)
                    {
                        if (clVo.name.Contains("FM"))
                        {
                            DevExpress.XtraReports.UI.XRLabel lblTopic = new DevExpress.XtraReports.UI.XRLabel();
                            lblTopic.Name = clVo.name;
                            lblTopic.Text = clVo.text;
                            if (clVo.name == "FM0902" || clVo.name == "FM0903")
                            {
                                lblTopic.Font = new System.Drawing.Font("宋体", 9.5F);
                                lblTopic.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                                lblTopic.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
                            locationX = clVo.locationX;
                            locationY = clVo.locationY;
                            lblTopic.Width = clVo.width;
                            //lblTopic.Height = clVo.height;
                            lblTopic.Location = new System.Drawing.Point(locationX, locationY);
                            this.Detail.Controls.Add(lblTopic);
                        }
                    }
                }

                List<EntityDicQnSetting> lstChildSettings = new List<EntityDicQnSetting>();
                if (lstTopic != null && lstTopic.Count > 0)
                {
                    for (int i = 0; i < lstTopic.Count; i++)
                    {
                        EntityDicQnSetting item = lstTopic[i];
                        if (item.questName == lstQuest[3])
                        {
                            if (string.IsNullOrEmpty(item.parentFieldId))
                            {
                                DevExpress.XtraReports.UI.XRLabel lblTopic = new DevExpress.XtraReports.UI.XRLabel();
                                lblTopic.Name = item.fieldId;
                                lblTopic.Text = item.fieldName;
                                lblTopic.Font = new System.Drawing.Font("宋体", 9.5F);
                                EntityCtrlLocation ctrLocat = lstCtrlLocation.Find(r => r.name == item.fieldId);
                                locationX = ctrLocat.locationX;
                                locationY = ctrLocat.locationY;
                                lblTopic.WidthF = ctrLocat.width;
                                //lblTopic.HeightF = ctrLocat.height;
                                lblTopic.Location = new System.Drawing.Point(locationX, locationY);
                                this.Detail.Controls.Add(lblTopic);

                                lstChildSettings = lstTopic.FindAll(r => r.parentFieldId == item.fieldId);
                                if (lstChildSettings.Count > 0)
                                {
                                    foreach (var childVo in lstChildSettings)
                                    {
                                        if (childVo.typeId != "3")
                                        {
                                            DevExpress.XtraReports.UI.XRCheckBox chkAns = new DevExpress.XtraReports.UI.XRCheckBox();
                                            string strEndWith = childVo.fieldId.Substring(4, 2);
                                            chkAns.Text = childVo.fieldName;
                                            chkAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                            chkAns.Name = childVo.fieldId;
                                            EntityCtrlLocation ctrLocatChild = lstCtrlLocation.Find(r => r.name == childVo.fieldId);
                                            locationX = ctrLocatChild.locationX;
                                            locationY = ctrLocatChild.locationY;
                                            chkAns.Width = ctrLocatChild.width;
                                            //chkAns.Height = ctrLocatChild.height;
                                            chkAns.Location = new System.Drawing.Point(locationX, locationY);
                                            if (dicData.ContainsKey(childVo.fieldId))
                                            {
                                                string value = dicData[childVo.fieldId];
                                                chkAns.Checked = value == "0" ? false : true;
                                            }
                                            this.Detail.Controls.Add(chkAns);
                                        }
                                        else
                                        {
                                            DevExpress.XtraReports.UI.XRLabel txtAns = new DevExpress.XtraReports.UI.XRLabel();
                                            string strEndWith = childVo.fieldId.Substring(4, 2);
                                            txtAns.Text = childVo.fieldName;
                                            txtAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                            txtAns.Name = childVo.fieldId;
                                            EntityCtrlLocation ctrLocatChild = lstCtrlLocation.Find(r => r.name == childVo.fieldId);
                                            locationX = ctrLocatChild.locationX;
                                            locationY = ctrLocatChild.locationY;
                                            txtAns.Width = ctrLocatChild.width;
                                            //txtAns.Height = ctrLocatChild.height;
                                            txtAns.Location = new System.Drawing.Point(locationX, locationY);
                                            if (dicData.ContainsKey(childVo.fieldId))
                                            {
                                                string value = dicData[childVo.fieldId];
                                                txtAns.Text = value;
                                            }
                                            this.Detail.Controls.Add(txtAns);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {

            }
        }
        #endregion

    }
}
