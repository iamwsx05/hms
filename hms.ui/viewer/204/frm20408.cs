using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hms.Entity;
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class frm20408 : frmBaseMdi
    {
        public frm20408()
        {
            InitializeComponent();
        }

        #region var/property
        List<EntityDisplayPromotionPlan> lstPromotionPlan { get; set; }
        #endregion


        #region methods
        void Init()
        {
            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> dicParm = new List<EntityParm>();
                dicParm.Add(Function.GetParm("auditState",""));
                lstPromotionPlan = proxy.Service.GetPromotionPlans(dicParm);
                gridControl.DataSource = lstPromotionPlan;
                gridControl.RefreshDataSource();
            }
        }
        #endregion

        private void frm20408_Load(object sender, EventArgs e)
        {
            Init();
        }
    }
}
