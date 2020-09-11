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
        List<EntityDietDetails> lstDietDetails { get; set; }
        //膳食方案记录
        EntityDietRecord dietRecord { get; set; }

        //Dictionary<string, int> dicDayIndex = new Dictionary<string, int>() { { "第一天", 1 }, { "第二天", 1 } };
        #endregion

        #region methods
        void Init()
        {
            this.chkDay1.Checked = true;
            this.chkDay2.Checked = true;
            lstDietDetails = new List<EntityDietDetails>();
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
            if (cvSelectClient.FocusedRowHandle >= 0)
            {
                EntityClientInfo vo = cvSelectClient.GetRow(cvSelectClient.FocusedRowHandle) as EntityClientInfo;
                if (vo != null)
                {
                    lstClient.Remove(vo);
                    gcSelectClient.DataSource = lstClient;
                    gcSelectClient.RefreshDataSource();
                }
            }
        }

        private void btnAddBreakfast_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDays.Text))
            {

                DialogBox.Msg("请选择天数！");
                return;
            }

            frmpopup2060203 frm = new frmpopup2060203(EnumMeals.breakfast, cboDays.SelectedIndex);
            frm.ShowDialog();

            if (frm.isSave)
            {
                if (frm.lstCaiDiet != null)
                {
                    List<EntityDietDetails> lstTemp = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex && r.mealId == (int)EnumMeals.breakfast);
                    if (lstTemp != null)
                    {
                        foreach (var tmp in lstTemp)
                            lstDietDetails.Remove(tmp);
                    }

                    lstDietDetails.AddRange(frm.lstCaiDiet);
                }

                gridControl.DataSource = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex);
                gridControl.RefreshDataSource();
            }
        }

        private void btnAddLunch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDays.Text))
            {

                DialogBox.Msg("请选择天数！");
                return;
            }
            frmpopup2060203 frm = new frmpopup2060203(EnumMeals.lunch, cboDays.SelectedIndex);
            frm.ShowDialog();

            if (frm.isSave)
            {
                if (frm.lstCaiDiet != null)
                {
                    List<EntityDietDetails> lstTemp = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex && r.mealId == (int)EnumMeals.lunch);
                    if (lstTemp != null)
                    {
                        foreach (var tmp in lstTemp)
                            lstDietDetails.Remove(tmp);
                    }

                    lstDietDetails.AddRange(frm.lstCaiDiet);
                }

                gridControl.DataSource = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex);
                gridControl.RefreshDataSource();
            }
        }

        private void btnAddDinner_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDays.Text))
            {
                DialogBox.Msg("请选择天数！");
                return;
            }
            frmpopup2060203 frm = new frmpopup2060203(EnumMeals.diner, cboDays.SelectedIndex);
            frm.ShowDialog();

            if (frm.isSave)
            {
                if (frm.lstCaiDiet != null)
                {
                    List<EntityDietDetails> lstTemp = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex && r.mealId == (int)EnumMeals.diner);
                    if(lstTemp != null)
                    {
                        foreach (var tmp in lstTemp)
                            lstDietDetails.Remove(tmp);
                    }

                    lstDietDetails.AddRange(frm.lstCaiDiet);
                }
                gridControl.DataSource = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex);
                gridControl.RefreshDataSource();
            }
        }

        private void frmpopup2060201_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void btnSaveDietCai_Click(object sender, EventArgs e)
        {
            if (lstClient == null)
            {
                DialogBox.Msg("客户为空，请选择！");
                return;
            }
            if (lstDietRecord == null)
                lstDietRecord = new List<EntityDietRecord>();

            foreach (var client in lstClient)
            {
                if(lstDietRecord.Any(r=>r.clientNo == client.clientNo && r.regTimes == client.regTimes))
                {
                    
                }
                else
                {
                    EntityDietRecord dietRecord = new EntityDietRecord();
                    dietRecord.clientNo = client.clientNo;
                    dietRecord.regTimes = client.regTimes;

                    lstDietRecord.Add(dietRecord);
                }
            }

            if (lstDietDetails != null)
            {

            }
        }

        private void btnDelCai_Click(object sender, EventArgs e)
        {

        }

        private void cboDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(cboDays.Text))
            {
                if(lstDietDetails != null)
                {
                    gridControl.DataSource = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex);
                    gridControl.RefreshDataSource();
                }
            }
        }

        #endregion
    }
}
