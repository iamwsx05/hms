using Common.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hms.Entity;

namespace Hms.Ui
{
    public partial class frmPopup2060101 : frmBasePopup
    {
        public frmPopup2060101(EntityDietPrinciple _dietPrinciple = null)
        {
            InitializeComponent();
            dietPrinciple = _dietPrinciple;
        }

        #region var/property

        public EntityDietPrinciple dietPrinciple = null;

        /// <summary>
        /// 是否要求刷新
        /// </summary>
        public bool IsRequireRefresh { get; set; }

        #endregion

      
        private void frmPopup2060101_Load(object sender, EventArgs e)
        {
            if(dietPrinciple != null)
            {
                this.Text = "查看/编辑膳食原则";
                txtName.Text = dietPrinciple.principleName;
                memContents.Text = dietPrinciple.contents;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int affect = -1;
            string id = string.Empty;
            if (dietPrinciple == null)
                dietPrinciple = new EntityDietPrinciple();
            else if (string.IsNullOrEmpty(dietPrinciple.principleId))
                dietPrinciple = new EntityDietPrinciple();

            dietPrinciple.principleName = this.txtName.Text;
            dietPrinciple.contents = this.memContents.Text;

            using (ProxyHms proxy = new ProxyHms())
            {
                affect =  proxy.Service.SaveDietPrinciple(dietPrinciple,out id);
            }

            if(affect < 0)
            {
                dietPrinciple.principleId = "";
                DialogBox.Msg("保存失败 !");
            }
            else
            {
                dietPrinciple.principleId = id;
                this.IsRequireRefresh = true;
            }
        }
    }
}
