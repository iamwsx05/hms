using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using weCare.Core.Entity;
using weCare.Core.Utils;
using System.Collections.Generic;
using Hms.Entity;
using System.Xml;

namespace Hms.Ui
{
    public partial class xrptQuest01 : DevExpress.XtraReports.UI.XtraReport
    {
        public xrptQuest01(EntityQnRecord qnRecord)
        {
            InitializeComponent();
            AddQuestCtrl(qnRecord);
        }
        List<string> lstQuest = new List<string>() { "quest01", "quest02", "quest03", "quest04", "quest05", "quest06", "quest07", "quest08", "quest09", "quest10" };
        string xmlData = string.Empty;
        Dictionary<string, string> dicData = new Dictionary<string, string>();
        List<EntityDicQnSetting> lstTopic = null;
        List<EntityCtrlLocation> lstCtrlLocation = null;
        List<EntityDicQnSetting> lstItems = null;

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
                        xmlData = list[1].OuterXml;
                        dicData = Function.ReadXML(xmlData);
                    }
                }
                using (ProxyHms proxy = new ProxyHms())
                {
                    lstCtrlLocation = proxy.Service.GetQnCtrlLocation(lstQuest[0],qnRecord.qnId);
                }

                using (ProxyHms proxy = new ProxyHms())
                {
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
                        if (clVo.name.Contains("FT"))
                        {
                            DevExpress.XtraReports.UI.XRLabel lblTopic = new DevExpress.XtraReports.UI.XRLabel();
                            lblTopic.Name = clVo.name;
                            lblTopic.Text = clVo.text;
                            lblTopic.Font = new System.Drawing.Font("宋体", 9.5F);
                            locationX = clVo.locationX;
                            locationY = clVo.locationY;
                            lblTopic.LocationFloat = new DevExpress.Utils.PointFloat(locationX, locationY);
                            this.Detail.Controls.Add(lblTopic);
                        }
                        if (clVo.name.Contains("FM"))
                        {
                            DevExpress.XtraReports.UI.XRLabel lblTopic = new DevExpress.XtraReports.UI.XRLabel();
                            lblTopic.Name = clVo.name;
                            lblTopic.Text = clVo.text;
                            lblTopic.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
                            locationX = clVo.locationX;
                            locationY = clVo.locationY;
                            lblTopic.LocationFloat = new DevExpress.Utils.PointFloat(locationX, locationY);
                            this.Detail.Controls.Add(lblTopic);
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
                        if (item.questName == lstQuest[0])
                        {
                            if (string.IsNullOrEmpty(item.parentFieldId))
                            {
                                DevExpress.XtraReports.UI.XRLabel lblTopic = new DevExpress.XtraReports.UI.XRLabel();
                                lblTopic.Name = item.fieldId;
                                lblTopic.Text = lstTopic.Find(r => r.fieldId == item.fieldId).fieldName;
                                lblTopic.Font = new System.Drawing.Font("宋体", 9.5F);
                                EntityCtrlLocation ctrLocat = lstCtrlLocation.FindAll(r => r.type == 1)[parentCount];
                                locationX = ctrLocat.locationX;
                                locationY = ctrLocat.locationY;
                                lblTopic.LocationFloat = new DevExpress.Utils.PointFloat(locationX, locationY);
                                this.Detail.Controls.Add(lblTopic);
                                lstChildSettings = lstTopic.FindAll(r => r.parentFieldId == item.fieldId);
                                if (lstChildSettings.Count > 0)
                                {
                                    foreach (var childVo in lstChildSettings)
                                    {
                                        DevExpress.XtraReports.UI.XRCheckBox chkAns = new DevExpress.XtraReports.UI.XRCheckBox();
                                        string strEndWith = childVo.fieldId.Substring(4, 2);
                                        chkAns.Text = "";
                                        chkAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                        chkAns.Name = childVo.fieldId;
                                        EntityCtrlLocation ctrLocatChild = lstCtrlLocation.Find(r => r.name.Contains(ctrLocat.name) && r.type == 2 && r.name.EndsWith(strEndWith));
                                        locationX = ctrLocatChild.locationX;
                                        locationY = ctrLocatChild.locationY;
                                        chkAns.Dpi = 100F;
                                        chkAns.LocationFloat = new DevExpress.Utils.PointFloat(locationX, locationY);
                                        chkAns.SizeF = new System.Drawing.SizeF(15.625F, 23F);
                                        if(dicData.ContainsKey(childVo.fieldId))
                                        {
                                            string value = dicData[childVo.fieldId];
                                            chkAns.Checked = value == "0" ? false : true;
                                        }
                                        
                                        this.Detail.Controls.Add(chkAns);
                                    }
                                }
                                parentCount++;
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
