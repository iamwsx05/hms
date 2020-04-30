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

namespace Hms.Ui
{
    public partial class Quest08 : UserControl, IQuest
    {
        public Quest08()
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
                        data.Add(new EntityDicQnDetail() { fieldId = ((DevExpress.XtraEditors.CheckEdit)ctr).Properties.AccessibleName });
                    }
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


            return data;
        }

        public EntityDicQnCtlLocation GetQnCtrlsLocation()
        {
            EntityDicQnCtlLocation vo = new EntityDicQnCtlLocation();
            vo.qnCtlFiledId = "quest08";
            StringBuilder xml = new StringBuilder();
            xml.Append("<eflayout>");
            foreach (Control ctrl in plBack.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.LabelControl)
                {
                    if (ctrl.Name.Contains("F"))
                    {
                        
                        xml.Append("<ctrl ctrlname=\"" + ctrl.Name +
                                   "\" ctrlText=\"" + ctrl.Text +
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
