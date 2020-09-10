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

namespace Hms.Ui
{
    public partial class frmpopup2060201 : frmBasePopup
    {
        public frmpopup2060201()
        {
            InitializeComponent();
        }

        #region var
        List<EntityClientInfo> lstClient { get; set; }
        List<EntityDietRecord> lstDietRecord { get; set; }
        #endregion

        #region methods
        void Init()
        {
            this.chkDay1.Checked = true;
            this.chkDay2.Checked = true;
            List<EntityCaiDiet> lstCaiDiet = new List<EntityCaiDiet>();
            EntityCaiDiet vo1 = new EntityCaiDiet();
            vo1.mealType = "早餐";
            vo1.caiName = "炒红薯叶";
            vo1.caiIngrediet = "白薯叶";
            vo1.weihgt = 60.0M;
            lstCaiDiet.Add(vo1);

            EntityCaiDiet vo2 = new EntityCaiDiet();
            vo2.mealType = "午餐";
            vo2.caiName = "炒红薯叶";
            vo2.caiIngrediet = "白薯叶";
            vo2.weihgt = 60.0M;
            lstCaiDiet.Add(vo2);

            EntityCaiDiet vo3 = new EntityCaiDiet();
            vo3.mealType = "午餐";
            vo3.caiName = "炒白菜";
            vo3.caiIngrediet = "白菜";
            vo3.weihgt = 60.0M;
            lstCaiDiet.Add(vo3);

            gridControl.DataSource = lstCaiDiet;
        }


        #endregion

        #region events

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmpopup2060202 frm = new frmpopup2060202(lstClient);
            frm.ShowDialog();

            lstClient = frm.lstClientSelect;
            gcSelectClient.DataSource = lstClient;
            gcSelectClient.RefreshDataSource();
        }
        
        private void btnDelPerson_Click(object sender, EventArgs e)
        {
            if(cvSelectClient.FocusedRowHandle >= 0)
            {
                EntityClientInfo vo = cvSelectClient.GetRow(cvSelectClient.FocusedRowHandle) as EntityClientInfo;
                if(vo != null)
                {
                    lstClient.Remove(vo);
                    gcSelectClient.DataSource = lstClient;
                    gcSelectClient.RefreshDataSource();
                }
            }
        }

        private void btnAddDays_Click(object sender, EventArgs e)
        {

        }

        private void btnAddBreakfast_Click(object sender, EventArgs e)
        {
            frmpopup2060203 frm = new frmpopup2060203(EnumMeals.breakfast);
            frm.ShowDialog();
        }

        private void btnAddLunch_Click(object sender, EventArgs e)
        {
            frmpopup2060203 frm = new frmpopup2060203(EnumMeals.lunch);
            frm.ShowDialog();
        }

        private void btnAddDinner_Click(object sender, EventArgs e)
        {
            frmpopup2060203 frm = new frmpopup2060203(EnumMeals.diner);
            frm.ShowDialog();
        }

        private void frmpopup2060201_Load(object sender, EventArgs e)
        {
            Init();
        }

        #endregion
    }
}
