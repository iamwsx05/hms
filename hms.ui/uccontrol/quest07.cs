﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using Hms.Entity;
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class Quest07 : UserControl, IQuest
    {
        public Quest07()
        {
            InitializeComponent();
        }

        public List<EntityDicQnDetail> GetQnCtrls()
        {
            List<EntityDicQnDetail> data = new List<EntityDicQnDetail>();
            foreach (Control ctr in plBack.Controls)
            {
                if (ctr is DevExpress.XtraEditors.CheckEdit)
                {
                    if (!string.IsNullOrEmpty(((DevExpress.XtraEditors.CheckEdit)ctr).Properties.AccessibleName) && ((DevExpress.XtraEditors.CheckEdit)ctr).Checked)
                    {
                        data.Add(new EntityDicQnDetail() { fieldId = ((DevExpress.XtraEditors.CheckEdit)ctr).Properties.AccessibleName, questName = "quest07" });
                    }
                }
                if (ctr is DevExpress.XtraEditors.LabelControl)
                {
                    data.Add(new EntityDicQnDetail() { fieldId = ctr.Name, questName = "quest07" });
                }

                if (ctr is DevExpress.XtraEditors.TextEdit)
                {
                    data.Add(new EntityDicQnDetail() { fieldId = ctr.Name, questName = "quest07" });
                }
            }
            return data;
        }

        public void SetQnCtrls(List<EntityDicQnDetail> lstCtrls)
        {
            if (lstCtrls == null) return;
            string accessibleName = string.Empty;
            foreach (Control ctr in plBack.Controls)
            {
                if (ctr is DevExpress.XtraEditors.CheckEdit)
                {
                    if (!string.IsNullOrEmpty(((DevExpress.XtraEditors.CheckEdit)ctr).Properties.AccessibleName))
                    {
                        accessibleName = ((DevExpress.XtraEditors.CheckEdit)ctr).Properties.AccessibleName;
                        if (lstCtrls.Any(t => t.fieldId == accessibleName))
                        {
                            ((DevExpress.XtraEditors.CheckEdit)ctr).Checked = true;
                        }
                    }
                }
            }
        }

        public List<EntityDicQnSetting> GetQnSettings()
        {
            List<EntityDicQnSetting> data = new List<EntityDicQnSetting>();
            EntityDicQnSetting vo = null;
            foreach (Control ctr in plBack.Controls)
            {
                if (ctr is DevExpress.XtraEditors.CheckEdit)
                {
                    if (!string.IsNullOrEmpty(((DevExpress.XtraEditors.CheckEdit)ctr).Properties.AccessibleName) && ((DevExpress.XtraEditors.CheckEdit)ctr).Checked)
                    {
                        vo = new EntityDicQnSetting();
                        vo.fieldId = ((DevExpress.XtraEditors.CheckEdit)ctr).Properties.AccessibleName;
                        vo.qnClassId = 1;
                        if (ctr.Text.Contains("多选"))
                            vo.typeId = "2";
                        else
                            vo.typeId = "1";
                        vo.fieldName = ctr.Text;
                        vo.parentFieldId = "";
                        vo.status = 1;
                        vo.sortNo = Function.Int(vo.fieldId.Replace('F', ' ').Trim());
                        data.Add(vo);
                    }

                    if (!string.IsNullOrEmpty(((DevExpress.XtraEditors.CheckEdit)ctr).Properties.AccessibleName) && !((DevExpress.XtraEditors.CheckEdit)ctr).Checked)
                    {
                        vo = new EntityDicQnSetting();
                        vo.fieldId = ((DevExpress.XtraEditors.CheckEdit)ctr).Properties.AccessibleName;
                        vo.qnClassId = 1;
                        if (ctr.Text.Contains("多选"))
                            vo.typeId = "2";
                        else
                            vo.typeId = "1";
                        vo.fieldName = ctr.Text;
                        vo.parentFieldId = "";
                        vo.status = 0;
                        vo.sortNo = Function.Int(vo.fieldId.Replace('F', ' ').Trim());
                        data.Add(vo);
                    }
                }
            }

            foreach (Control ctr in plBack.Controls)
            {
                if (ctr.Name.Contains("FM"))
                    continue;

                if (ctr is DevExpress.XtraEditors.LabelControl)
                {
                    if (data.Any(r => r.fieldId == ctr.Name.Substring(0, 4) && r.status == 1))
                    {
                        vo = new EntityDicQnSetting();
                        vo.fieldId = ctr.Name;
                        vo.qnClassId = 1;
                        vo.typeId = "1";
                        vo.fieldName = ctr.Text;
                        vo.parentFieldId = vo.fieldId.Substring(0, 4);
                        vo.status = 1;
                        vo.sortNo = Function.Int(vo.fieldId.Replace('F', ' ').Trim());
                        data.Add(vo);
                    }
                }
                if (ctr is DevExpress.XtraEditors.TextEdit)
                {
                    if (!string.IsNullOrEmpty(((DevExpress.XtraEditors.TextEdit)ctr).Properties.AccessibleName))
                    {
                        if (data.Any(r => r.fieldId == ctr.Name.Substring(0, 4) && r.status == 1))
                        {
                            vo = new EntityDicQnSetting();
                            vo.fieldId = ((DevExpress.XtraEditors.TextEdit)ctr).Properties.AccessibleName;
                            vo.qnClassId = 1;
                            vo.typeId = "3";
                            vo.fieldName = ctr.Text;
                            vo.parentFieldId = vo.fieldId.Substring(0, 4);
                            vo.status = 1;
                            vo.sortNo = Function.Int(vo.fieldId.Replace('F', ' ').Trim());
                            data.Add(vo);
                        }
                    }

                }
            }

            return data;
        }

        public EntityDicQnCtlLocation GetQnCtrlsLocation()
        {
            EntityDicQnCtlLocation vo = new EntityDicQnCtlLocation();
            vo.qnCtlFiledId = "quest07";
            StringBuilder xml = new StringBuilder();
            xml.Append("<eflayout>");
            foreach (Control ctrl in plBack.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.LabelControl)
                {
                    if (ctrl.Name.Contains("F"))
                    {

                        xml.Append("<ctrl ctrlname=\"" + ctrl.Name +
                                   "\" ctrlText=\"" + ctrl.Text.Replace("<", "&lt;").Replace(">", "&gt;") +
                                    "\" ctrltype=\"" + ctrl.GetType() +
                                   "\" top=\"" + ctrl.Top.ToString() +
                                   "\" left=\"" + ctrl.Left.ToString() +
                                   "\" width=\"" + (Function.Int(ctrl.Width) + 20).ToString() +
                                   "\" height=\"" + ctrl.Height.ToString() +
                                   "\" locationX=\"" + ctrl.Location.X +
                                   "\" locationY=\"" + ctrl.Location.Y + "\"/>");


                    }
                }
                if (ctrl is DevExpress.XtraEditors.CheckEdit)
                {
                    if (!string.IsNullOrEmpty(((DevExpress.XtraEditors.CheckEdit)ctrl).Properties.AccessibleName))
                    {
                        xml.Append("<ctrl ctrlname=\"" + ((DevExpress.XtraEditors.CheckEdit)ctrl).Properties.AccessibleName +
                                "\" ctrlText=\"" + ctrl.Text.Replace("<", "&lt;").Replace(">", "&gt;") +
                                "\" ctrltype=\"" + ctrl.GetType() +
                                "\" top=\"" + ctrl.Top.ToString() +
                                "\" left=\"" + ctrl.Left.ToString() +
                                "\" width=\"" + ctrl.Width.ToString() +
                                "\" height=\"" + ctrl.Height.ToString() +
                                "\" locationX=\"" + ctrl.Location.X +
                                "\" locationY=\"" + ctrl.Location.Y + "\"/>");
                    }
                }

                if (ctrl is DevExpress.XtraEditors.TextEdit)
                {
                    if (ctrl.Name.Contains("F"))
                    {
                        xml.Append("<ctrl ctrlname=\"" + ctrl.Name +
                                "\" ctrlText=\"" + ctrl.Text.Replace("<", "&lt;").Replace(">", "&gt;") +
                                "\" ctrltype=\"" + ctrl.GetType() +
                                "\" top=\"" + ctrl.Top.ToString() +
                                "\" left=\"" + ctrl.Left.ToString() +
                                "\" width=\"" + ctrl.Width.ToString() +
                                "\" height=\"" + ctrl.Height.ToString() +
                                "\" locationX=\"" + ctrl.Location.X +
                                "\" locationY=\"" + ctrl.Location.Y + "\"/>");
                    }
                }
            }
            xml.Append("</eflayout>");
            xml.Append(System.Environment.NewLine);
            vo.xmlData = xml.ToString();
            return vo;
        }

    }
}
