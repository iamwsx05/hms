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
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class frmPopup2060301 : frmBasePopup
    {
        public frmPopup2060301(EntityDietTemplate _dietTemplate = null)
        {
            InitializeComponent();
            dietTemplate = _dietTemplate;
        }
        public frmPopup2060301(List<EntityDietDetails> _lstDietDetails )
        {
            InitializeComponent();
            lstDietDetails = _lstDietDetails;
        }

        #region var/propery
        public EntityDietTemplate dietTemplate = null;
        public List<EntityDietDetails> lstDietDetails { get; set; }
        public List<EntityDietTemplateDetails> lstDietTemplateDetails { get; set; }
        public List<EntityDietTemplatetype> lstDietTemplatetype = null;
        public bool IsRequireRefresh = false;
        #endregion

        #region method
        void Init()
        {
            lstDietTemplateDetails = new List<EntityDietTemplateDetails>();
            using (ProxyHms proxy = new ProxyHms())
            {
                lstDietTemplatetype = proxy.Service.GetDietTemplatetype();
            }

            if (lstDietTemplatetype.Count > 0)
            {
                foreach (var vo in lstDietTemplatetype)
                    this.cboType.Properties.Items.Add(vo.typeName);
            }

            if (dietTemplate != null)
            {
                this.Text = "查看/编辑饮食菜谱模板";
                this.txtName.Text = dietTemplate.templateName;
                this.cboType.Text = lstDietTemplatetype.FindAll(r => r.typeId == dietTemplate.typeid).FirstOrDefault().typeName;
                this.memDescriptions.Text = dietTemplate.descriptions;
            }

            if (lstDietDetails != null)
            {
                this.Text = "另存为模板";
            }
        }
        #endregion

        #region event
        private void frmPopup2060301_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstDietDetails == null)
            {
                return;
            }

            foreach(var diet in lstDietDetails)
            {
                if(diet.lstDetailsCai!=null)
                {
                    foreach(var detail in diet.lstDetailsCai)
                    {
                        if(detail.lstDietdetailsIngrediet != null)
                        {
                            foreach(var ingrediet in detail.lstDietdetailsIngrediet)
                            {
                                EntityDietTemplateDetails vo = new EntityDietTemplateDetails();
                                vo.caiId = ingrediet.caiId;
                                vo.caiIngrediet = ingrediet.caiIngrediet;
                                vo.caiIngredietId = ingrediet.caiIngredietId;
                                vo.caiName = ingrediet.caiName;
                                vo.caiWeight = ingrediet.caiWeight;
                                vo.day = ingrediet.day;
                                vo.mealId = ingrediet.mealId;
                                vo.mealType = ingrediet.mealType;
                                vo.realWeight = ingrediet.realWeight;
                                vo.weight = ingrediet.weight;
                                vo.per = ingrediet.per;
                                lstDietTemplateDetails.Add(vo);
                            }
                        }
                    }
                }
            }

            int affect = -1;
            string templateId = string.Empty;
            if (dietTemplate == null)
                dietTemplate = new EntityDietTemplate();
            else if (string.IsNullOrEmpty(dietTemplate.templateId))
                dietTemplate = new EntityDietTemplate();

            dietTemplate.templateName = this.txtName.Text;
            dietTemplate.descriptions = memDescriptions.Text;
            dietTemplate.typeid = lstDietTemplatetype.FindAll(r => r.typeName == cboType.Text).FirstOrDefault().typeId;
            dietTemplate.creator = "00";
            dietTemplate.createName = "系统管理员";

            using (ProxyHms proxy = new ProxyHms())
            {
                affect = proxy.Service.SaveDietTemplateDetails(dietTemplate,lstDietTemplateDetails, out templateId);
            }

            if (affect < 0)
            {
                dietTemplate.templateId = "";
                DialogBox.Msg("保存失败 !");
            }
            else
            {
                dietTemplate.templateId = templateId;
                this.IsRequireRefresh = true;
                DialogBox.Msg("保存成功 !");
                this.Close();
            }

        }
        #endregion
    }
}
