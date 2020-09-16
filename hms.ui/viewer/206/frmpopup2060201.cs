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
using weCare.Core.Entity;

namespace Hms.Ui
{
    public partial class frmpopup2060201 : frmBasePopup
    {
        public frmpopup2060201(EntityDietRecord _dietRecord)
        {
            InitializeComponent();
            dietRecord = _dietRecord;
        }

        #region var
        List<EntityClientInfo> lstClient { get; set; }
        List<EntityDietRecord> lstDietRecord { get; set; }
        List<EntityDietDetails> lstDietDetails { get; set; }
        List<EntityDicDientIngredient> lstDicDietIngrediet { get; set; }
        //膳食方案记录
        EntityDietRecord dietRecord { get; set; }
        #endregion

        #region methods

        #region Init
        void Init()
        {
            if (lstDietDetails == null)
                lstDietDetails = new List<EntityDietDetails>();
            if (lstDietRecord == null)
                lstDietRecord = new List<EntityDietRecord>();
            using (ProxyHms proxy = new ProxyHms())
            {
                lstDicDietIngrediet = proxy.Service.GetDicDietIngredient();
            }

            if (dietRecord != null)
            {
                List<EntityClientInfo> lstClientTemp = new List<EntityClientInfo>();
                lstDietRecord.Add(dietRecord);
                List<EntityParm> parms = new List<EntityParm>();
                string search = dietRecord.clientNo;
                EntityParm vo = new EntityParm();
                vo.key = "search";
                vo.value = search;
                parms.Add(vo);
                string clientNoStr = string.Empty;

                using (ProxyHms proxy = new ProxyHms())
                {
                    if (!string.IsNullOrEmpty(search))
                    {
                        lstClientTemp = proxy.Service.GetClientInfoAndRpt(parms);
                    }
                }
                if (lstClientTemp != null)
                {
                    lstClient = lstClientTemp.FindAll(r => r.regTimes == dietRecord.regTimes);
                    gcSelectClient.DataSource = lstClient;
                    gcSelectClient.RefreshDataSource();
                }

                string dietRecId = "('" + dietRecord.recId.ToString() + "')";
                using (ProxyHms proxy = new ProxyHms())
                {
                    lstDietDetails = proxy.Service.GetDietDetails(dietRecId);
                }

                if (lstDietDetails.Any(r => r.day == 1))
                    chkDay1.Checked = true;
                else
                    chkDay1.Checked = false;
                if (lstDietDetails.Any(r => r.day == 2))
                    chkDay2.Checked = true;
                else
                    chkDay2.Checked = false;
                if (lstDietDetails.Any(r => r.day == 3))
                    chkDay3.Checked = true;
                else
                    chkDay3.Checked = false;
                if (lstDietDetails.Any(r => r.day == 4))
                    chkDay4.Checked = true;
                else
                    chkDay4.Checked = false;
                if (lstDietDetails.Any(r => r.day == 5))
                    chkDay5.Checked = true;
                else
                    chkDay5.Checked = false;
                if (lstDietDetails.Any(r => r.day == 6))
                    chkDay6.Checked = true;
                else
                    chkDay6.Checked = false;
                if (lstDietDetails.Any(r => r.day == 7))
                    chkDay7.Checked = true;
                else
                    chkDay7.Checked = false;

                if (lstDietDetails.Any(r => r.day == 1))
                {
                    cboDays.SelectedIndex = 1;
                    gridControl.DataSource = lstDietDetails.FindAll(r => r.day == 1);
                    gridControl.RefreshDataSource();
                }
                else if (lstDietDetails.Any(r => r.day == 2))
                {
                    cboDays.SelectedIndex = 2;
                    gridControl.DataSource = lstDietDetails.FindAll(r => r.day == 2);
                    gridControl.RefreshDataSource();
                }
                else if (lstDietDetails.Any(r => r.day == 3))
                {
                    cboDays.SelectedIndex = 3;
                    gridControl.DataSource = lstDietDetails.FindAll(r => r.day == 3);
                    gridControl.RefreshDataSource();
                }
                else if (lstDietDetails.Any(r => r.day == 4))
                {
                    cboDays.SelectedIndex = 4;
                    gridControl.DataSource = lstDietDetails.FindAll(r => r.day == 4);
                    gridControl.RefreshDataSource();
                }
                else if (lstDietDetails.Any(r => r.day == 5))
                {
                    cboDays.SelectedIndex = 5;
                    gridControl.DataSource = lstDietDetails.FindAll(r => r.day == 5);
                    gridControl.RefreshDataSource();
                }
                else if (lstDietDetails.Any(r => r.day == 6))
                {
                    cboDays.SelectedIndex = 6;
                    gridControl.DataSource = lstDietDetails.FindAll(r => r.day == 6);
                    gridControl.RefreshDataSource();
                }
                else if (lstDietDetails.Any(r => r.day == 7))
                {
                    cboDays.SelectedIndex = 7;
                    gridControl.DataSource = lstDietDetails.FindAll(r => r.day == 7);
                    gridControl.RefreshDataSource();
                }
            }
        }
        #endregion

        #region 计算成份
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstDietDetails"></param>
        /// <returns></returns>
        List<EntityIngredietNutrition> CalcNutrition(List<EntityDietDetails> lstDietDetails)
        {
            decimal kcal = 0;
            decimal proteint = 0;
            decimal fat = 0;
            decimal cho = 0;
            decimal brxxw = 0;
            decimal dgc = 0;
            decimal vitaminsA = 0;
            decimal vitaminsD = 0;
            decimal vitaminsE = 0;
            decimal vitaminsB1 = 0;
            decimal vitaminsB2 = 0;
            decimal vitaminsC = 0;
            decimal ca = 0;
            decimal fe = 0;
            List <EntityIngredietNutrition> data = null;
            EntityIngredietNutrition idNut = null;

            if (lstDietDetails != null && lstDietDetails.Count > 0)
            {
                data = new List<EntityIngredietNutrition>();
                foreach (var vo in lstDietDetails)
                {
                    EntityDicDientIngredient dietIngrediet = lstDicDietIngrediet.Find(r=>r.ingredientId == vo.caiIngredietId);
                    if (dietIngrediet == null)
                        continue;
                    kcal += dietIngrediet.kCal * vo.weight;
                    proteint += dietIngrediet.proteint * vo.weight;
                    fat += dietIngrediet.fat * vo.weight;
                    cho += dietIngrediet.cho * vo.weight;
                    brxxw += dietIngrediet.brxxw * vo.weight;
                    dgc += dietIngrediet.dgc * vo.weight;
                    vitaminsA += dietIngrediet.vitaminsA * vo.weight;
                    vitaminsD += dietIngrediet.vitaminsD * vo.weight;
                    vitaminsE += dietIngrediet.vitaminsE * vo.weight;
                    vitaminsB1 += dietIngrediet.thiamin * vo.weight;
                    vitaminsB2 += dietIngrediet.riboflavin * vo.weight;
                    vitaminsC += dietIngrediet.vitaminsC * vo.weight;
                    ca += dietIngrediet.ca * vo.weight;
                    fe += dietIngrediet.fe * vo.weight;
                }
                idNut = new EntityIngredietNutrition();
                idNut.itemName = "kcal";
                idNut.recoJ = (kcal / 100).ToString("0.00") + " Kcal";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "proteint";
                idNut.recoJ = (proteint / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "fat";
                idNut.recoJ = (fat / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "cho";
                idNut.recoJ = (cho / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "brxxw";
                idNut.recoJ = (brxxw / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "dgc";
                idNut.recoJ = (dgc / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "vitaminsA";
                idNut.recoJ = (vitaminsA / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "vitaminsD";
                idNut.recoJ = (vitaminsD / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "vitaminsE";
                idNut.recoJ = (vitaminsE / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "vitaminsB1";
                idNut.recoJ = (vitaminsB1 / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "vitaminsB2";
                idNut.recoJ = (vitaminsB2 / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "vitaminsC";
                idNut.recoJ = (vitaminsC / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "ca";
                idNut.recoJ = (ca / 100).ToString("0.00") + " g";
                data.Add(idNut);

                idNut = new EntityIngredietNutrition();
                idNut.itemName = "fe";
                idNut.recoJ = (fe / 100).ToString("0.00") + " g";
                data.Add(idNut);
            }
            return data;
        }
        #endregion

        #region GetRowDietDetail
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal EntityDietDetails GetRowDietDetail()
        {
            EntityDietDetails vo = null;
            if (this.gridView.FocusedRowHandle >= 0)
                vo = this.gridView.GetRow(this.gridView.FocusedRowHandle) as EntityDietDetails;

            return vo;
        }
        #endregion

        #region RefreshDetails
        void RefreshDetails()
        {
            if (lstDietDetails == null)
            {
                gridControl.DataSource = null;
                gridControl.RefreshDataSource();
                return;
            }

            if (lstDietDetails.Any(r => r.day == 1))
                chkDay1.Checked = true;
            else
                chkDay1.Checked = false;
            if (lstDietDetails.Any(r => r.day == 2))
                chkDay2.Checked = true;
            else
                chkDay2.Checked = false;
            if (lstDietDetails.Any(r => r.day == 3))
                chkDay3.Checked = true;
            else
                chkDay3.Checked = false;
            if (lstDietDetails.Any(r => r.day == 4))
                chkDay4.Checked = true;
            else
                chkDay4.Checked = false;
            if (lstDietDetails.Any(r => r.day == 5))
                chkDay5.Checked = true;
            else
                chkDay5.Checked = false;
            if (lstDietDetails.Any(r => r.day == 6))
                chkDay6.Checked = true;
            else
                chkDay6.Checked = false;
            if (lstDietDetails.Any(r => r.day == 7))
                chkDay7.Checked = true;
            else
                chkDay7.Checked = false;

            gridControl.DataSource = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex);
            gridControl.RefreshDataSource();
        }
        #endregion

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
                RefreshDetails();
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

                RefreshDetails();
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
                    if (lstTemp != null)
                    {
                        foreach (var tmp in lstTemp)
                            lstDietDetails.Remove(tmp);
                    }

                    lstDietDetails.AddRange(frm.lstCaiDiet);
                }
                RefreshDetails();
            }
        }

        private void frmpopup2060201_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void btnSaveDietCai_Click(object sender, EventArgs e)
        {
            int affect = 0;
            Dictionary<string, decimal> dicRecId = null;
            if (lstClient == null)
            {
                DialogBox.Msg("客户为空，请选择！");
                return;
            }
            if (lstDietRecord == null)
                lstDietRecord = new List<EntityDietRecord>();

            foreach (var client in lstClient)
            {
                EntityDietRecord tempVo = null;
                if (lstDietRecord != null)
                    tempVo = lstDietRecord.Find(r => r.clientNo == client.clientNo && r.regTimes == client.regTimes);
                if (tempVo == null || tempVo.recId <= 0)
                {
                    EntityDietRecord dietRecord = new EntityDietRecord();
                    dietRecord.clientNo = client.clientNo;
                    dietRecord.regTimes = client.regTimes;

                    lstDietRecord.Add(dietRecord);
                }
            }

            if (lstDietRecord != null)
            {
                foreach (var dietR in lstDietRecord)
                {
                    if (lstDietDetails.Any(r => r.day == 1))
                        dietR.day1 = 1;
                    if (lstDietDetails.Any(r => r.day == 2))
                        dietR.day2 = 1;
                    if (lstDietDetails.Any(r => r.day == 3))
                        dietR.day3 = 1;
                    if (lstDietDetails.Any(r => r.day == 4))
                        dietR.day4 = 1;
                    if (lstDietDetails.Any(r => r.day == 5))
                        dietR.day5 = 1;
                    if (lstDietDetails.Any(r => r.day == 6))
                        dietR.day6 = 1;
                    if (lstDietDetails.Any(r => r.day == 7))
                        dietR.day7 = 1;
                }
                using (ProxyHms proxy = new ProxyHms())
                {
                    affect = proxy.Service.SaveDietCai(lstDietRecord, lstDietDetails, out dicRecId);
                }
            }

            if (affect > 0)
            {
                foreach (var recVo in lstDietRecord)
                {
                    recVo.recId = dicRecId[recVo.clientNo];
                }
                DialogBox.Msg("保存成功 ！");
            }
            else
            {
                DialogBox.Msg("保存失败 ！");
            }
        }

        private void btnDelCai_Click(object sender, EventArgs e)
        {
            EntityDietDetails vo = GetRowDietDetail();

            if (vo != null)
            {
                List<EntityDietDetails> lstDel = lstDietDetails.FindAll(r=>r.day == vo.day && r.mealId == vo.mealId && r.caiId == vo.caiId);
                if(lstDel != null)
                {   
                    for(int i = 0;i<lstDel.Count;i++)
                    {
                        lstDietDetails.Remove(lstDel[i]);
                    }
                }

                RefreshDetails();
            }
        }

        private void cboDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<EntityDietDetails> lstDietDetailsTemp = null;
            if (!string.IsNullOrEmpty(cboDays.Text))
            {
                if (lstDietDetails != null)
                {
                    lstDietDetailsTemp  = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex);
                    gridControl.DataSource = lstDietDetailsTemp;
                    gridControl.RefreshDataSource();
                }
            }

            if(lstDietDetailsTemp != null)
            {
                this.gcDietNutrtion.DataSource = CalcNutrition(lstDietDetailsTemp) ;
                gcDietNutrtion.RefreshDataSource();
            }
            else
            {
                this.gcDietNutrtion.DataSource = null;
                gcDietNutrtion.RefreshDataSource();
            }
        }

        #endregion
    }
}
