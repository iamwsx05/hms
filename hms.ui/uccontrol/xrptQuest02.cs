using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hms.Entity;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using System.Xml;

namespace Hms.Ui
{
    public partial class xrptQuest02 : DevExpress.XtraReports.UI.XtraReport
    {
        public xrptQuest02(EntityQnRecord qnRecord)
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
                        xmlData = list[2].OuterXml;
                        dicData = Function.ReadXML(xmlData);
                    }
                }
                using (ProxyHms proxy = new ProxyHms())
                {
                    lstCtrlLocation = proxy.Service.GetQnCtrlLocation(lstQuest[1], qnRecord.qnId);
                    lstTopic = new List<EntityDicQnSetting>();
                    lstItems = new List<EntityDicQnSetting>();
                    proxy.Service.GetQnCustom(1, out lstTopic, out lstItems);
                }

                int locationX = 0;
                int locationY = 0;

                int F35Count = 0;
                int F35Row = (lstTopic.FindAll(r => r.fieldId.Contains("F035")).Count) / 6;
                int F35Y = 0;
                List<EntityDicQnSetting> lstChildSettings = new List<EntityDicQnSetting>();
                if (lstTopic != null && lstTopic.Count > 0)
                {
                    for (int i = 0; i < lstTopic.Count; i++)
                    {
                        EntityDicQnSetting item = lstTopic[i];
                        if (item.questName == lstQuest[1])
                        {
                            if (item.fieldId.Contains("F035"))
                            {
                                DevExpress.XtraReports.UI.XRCheckBox chkAns = new DevExpress.XtraReports.UI.XRCheckBox();
                                chkAns.Text = item.fieldName;
                                chkAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                chkAns.Name = item.fieldId;
                                EntityCtrlLocation ctrLocat = lstCtrlLocation.FindAll(r => r.name.Contains("F035"))[F35Count];
                                locationX = ctrLocat.locationX;
                                locationY = ctrLocat.locationY;
                                F35Y += locationY;
                                chkAns.Width = ctrLocat.width;
                                chkAns.Height = ctrLocat.height;
                                chkAns.Location = new System.Drawing.Point(locationX, locationY);
                                if (dicData.ContainsKey(item.fieldId))
                                {
                                    string value = dicData[item.fieldId];
                                    chkAns.Checked = value == "0" ? false : true;
                                }
                                this.Detail.Controls.Add(chkAns);
                                F35Count++;
                            }
                        }
                    }
                    F35Y -= 80;
                    for (int i2 = 0; i2 < lstTopic.Count; i2++)
                    {
                        EntityDicQnSetting itemVo = lstTopic[i2];
                        if (itemVo.questName == lstQuest[1])
                        {
                            if (string.IsNullOrEmpty(itemVo.parentFieldId) && !itemVo.fieldId.Contains("F035"))
                            {
                                DevExpress.XtraReports.UI.XRLabel lblTopic = new DevExpress.XtraReports.UI.XRLabel();
                                lblTopic.Name = itemVo.fieldId;
                                lblTopic.Text = itemVo.fieldName;
                                lblTopic.Font = new System.Drawing.Font("宋体", 9.5F);
                                EntityCtrlLocation ctrLocat = lstCtrlLocation.Find(r => r.name == itemVo.fieldId);
                                locationX = ctrLocat.locationX;
                                locationY = ctrLocat.locationY - F35Y ;
                                lblTopic.Location = new System.Drawing.Point(locationX, locationY);
                                this.Detail.Controls.Add(lblTopic);

                                lstChildSettings = lstTopic.FindAll(r => r.parentFieldId == itemVo.fieldId);
                                if (lstChildSettings.Count > 0)
                                {
                                    foreach (var childVo in lstChildSettings)
                                    {
                                        DevExpress.XtraReports.UI.XRCheckBox chkAns = new DevExpress.XtraReports.UI.XRCheckBox();
                                        string strEndWith = childVo.fieldId.Substring(4, 2);
                                        chkAns.Text = childVo.fieldName;
                                        chkAns.Font = new System.Drawing.Font("宋体", 9.5F);
                                        chkAns.Name = childVo.fieldId;
                                        EntityCtrlLocation ctrLocatChild = lstCtrlLocation.Find(r => r.name == childVo.fieldId);
                                        locationX = ctrLocatChild.locationX;
                                        locationY = ctrLocatChild.locationY - F35Y ;
                                        chkAns.Width = ctrLocatChild.width;
                                        chkAns.Height = ctrLocatChild.height;
                                        chkAns.Location = new System.Drawing.Point(locationX, locationY);
                                        if (dicData.ContainsKey(childVo.fieldId))
                                        {
                                            string value = dicData[childVo.fieldId];
                                            chkAns.Checked = value == "0" ? false : true;
                                        }
                                        this.Detail.Controls.Add(chkAns);
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
                        DevExpress.XtraReports.UI.XRLabel lblTopic = new DevExpress.XtraReports.UI.XRLabel();
                        lblTopic.Name = clVo.name;
                        lblTopic.Text = clVo.text;
                        lblTopic.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
                        locationX = clVo.locationX;
                        if (clVo.name == "FM0201")
                        {
                            locationY = clVo.locationY ;
                        }
                        else if (clVo.name.Contains("FM"))
                        {
                            locationY = clVo.locationY - F35Y  ;
                        }
                        else
                            continue;

                        lblTopic.Location = new System.Drawing.Point(locationX, locationY);
                       this.Detail.Controls.Add(lblTopic);
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
