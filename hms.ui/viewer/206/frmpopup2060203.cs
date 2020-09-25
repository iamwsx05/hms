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
    public partial class frmpopup2060203 : frmBasePopup
    {
        public frmpopup2060203(EnumMeals _enumMeals, int _day)
        {
            InitializeComponent();
            enumMeals = _enumMeals;
            day = _day;
        }

        #region var/propery
        public List<EntityDisplayDicCaiRecipe> lstCaiRecipe { get; set; }
        public List<EntityDicCai> lstCai { get; set; }
        public List<EntityDicCaiIngredient> lstCaiIngredient { get; set; }

        public List<EntityDietDetails> lstCaiDiet { get; set; }
        public bool isSave { get; set; }

        List<DevExpress.XtraEditors.CheckEdit> lstSingleCheck { get; set; }
        public EnumMeals enumMeals { get; set; }
        public int day { get; set; }
        #endregion

        #region methods
        void Init()
        {
            uiHelper.BeginLoading(this);
            isSave = false;
            if (enumMeals == EnumMeals.breakfast)
            {
                this.Text += "--早餐";
            }
            if (enumMeals == EnumMeals.lunch)
            {
                this.Text += "--午餐";
            }
            if (enumMeals == EnumMeals.diner)
            {
                this.Text += "--晚餐";
            }

            lstSingleCheck = new List<DevExpress.XtraEditors.CheckEdit>();
            lstSingleCheck.Add(chkAll);
            lstSingleCheck.Add(chkBre);
            lstSingleCheck.Add(chkLunch);
            lstSingleCheck.Add(chkDinner);
            using (ProxyHms proxy = new ProxyHms())
            {
                lstCaiRecipe = proxy.Service.GetDicCaiRecipe();
                lstCai = proxy.Service.GetDicCai();
            }
            this.gcCaiRecipe.DataSource = this.lstCaiRecipe;
            this.gcData.RefreshDataSource();
            this.gcData.DataSource = this.lstCai;
            this.gcData.RefreshDataSource();
            uiHelper.CloseLoading(this);
        }


        #region GetRowObject
        /// <summary>
        /// GetRowObject
        /// </summary>
        /// <returns></returns>
        EntityDisplayDicCaiRecipe GetRowObject()
        {
            if (this.gvCaiRecipe.FocusedRowHandle < 0) return null;
            return this.gvCaiRecipe.GetRow(this.gvCaiRecipe.FocusedRowHandle) as EntityDisplayDicCaiRecipe;
        }
        #endregion

        #region GetCaiRowObject
        /// <summary>
        /// GetCaiRowObject
        /// </summary>
        /// <returns></returns>
        EntityDicCai GetCaiRowObject()
        {
            if (this.gvData.FocusedRowHandle < 0) return null;
            return this.gvData.GetRow(this.gvData.FocusedRowHandle) as EntityDicCai;
        }


        #endregion

        
        private void gvCaiRecipe_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            EntityDisplayDicCaiRecipe caiRecipe = GetRowObject();
            if (caiRecipe != null)
            {
                gcData.DataSource = this.lstCai.FindAll(r => r.lstCaiSlaveId.Contains(caiRecipe.caiSlaveId));
                this.gcData.RefreshDataSource();
            }
        }

        private void gvData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (gvData.IsRowSelected(e.RowHandle))
                    gvData.UnselectRow(e.RowHandle);
                else
                {
                    gvData.SelectRow(e.RowHandle);
                }
                    
            }
        }

        string lastCaiStr = string.Empty;
        string caiStr = string.Empty;
        private void timer_Tick(object sender, EventArgs e)
        {
            caiStr = string.Empty;
            if (gvData.RowCount > 0)
            {
                for (int i = 0; i < this.gvData.RowCount; i++)
                {
                    if (this.gvData.IsRowSelected(i))
                    {
                        EntityDicCai vo = this.gvData.GetRow(i) as EntityDicCai;

                        if (vo != null)
                        {
                            caiStr += vo.names + Environment.NewLine;
                        }
                    }
                }
                if(lastCaiStr != caiStr)
                {
                    lastCaiStr = caiStr;
                    this.memCai.Text = caiStr;
                }
            }
        }

        private void frmpopup2060203_Load(object sender, EventArgs e)
        {
            Init();
        }
      
        private void btnQuery_Click(object sender, EventArgs e)
        {
            List<EntityDicCai> lstCaiTemp = new List<EntityDicCai>() ;
            string name = this.txtName.Text;
            lstCaiTemp = lstCai;
            if (!string.IsNullOrEmpty(name))
            {
                lstCaiTemp  = this.lstCai.FindAll(r => r.names.Contains(name));
            }

            if(chkBre.Checked == true)
            {
                lstCaiTemp = lstCaiTemp.FindAll(r => r.breakfast == "1");
            }
            if (chkLunch.Checked == true)
            {
                lstCaiTemp = lstCaiTemp.FindAll(r => r.lunch == "1");
            }
            if (chkDinner.Checked == true)
            {
                lstCaiTemp = lstCaiTemp.FindAll(r => r.dinner == "1");
            }

            if(lstCaiTemp != null && lstCaiTemp.Count > 0)
            {
                gcData.DataSource = lstCaiTemp;
                this.gcData.RefreshDataSource();
            }
        }


        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chkVo = sender as DevExpress.XtraEditors.CheckEdit;

            if (lstSingleCheck != null)
            {
                foreach (DevExpress.XtraEditors.CheckEdit chk in lstSingleCheck)
                {
                    if (chkVo != chk)
                        chk.Checked = false;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lstCaiDiet == null)
                lstCaiDiet = new List<EntityDietDetails>();
            if (gvData.RowCount > 0)
            {
                List<EntityDietDetails> data2 = new List<EntityDietDetails>();
                for (int i = 0; i < this.gvData.RowCount; i++)
                {
                    if (this.gvData.IsRowSelected(i))
                    {
                        EntityDicCai vo = this.gvData.GetRow(i) as EntityDicCai;

                        if (vo != null)
                        {
                            using (ProxyHms proxy = new ProxyHms())
                            {
                                lstCaiIngredient = proxy.Service.GetCaiIngredient(vo.id);
                            }    

                            if(lstCaiIngredient != null)
                            {
                                foreach (var ingreDiet in lstCaiIngredient)
                                {
                                    EntityDietDetails caiDiet = new EntityDietDetails();
                                    if (enumMeals == EnumMeals.breakfast)
                                    {
                                        caiDiet.mealId = 1;
                                        caiDiet.mealType = "早餐";
                                    }

                                    if (enumMeals == EnumMeals.lunch)
                                    {
                                        caiDiet.mealId = 2;
                                        caiDiet.mealType = "午餐";
                                    }

                                    if (enumMeals == EnumMeals.diner)
                                    {
                                        caiDiet.mealId = 3;
                                        caiDiet.mealType = "晚餐";
                                    }

                                    caiDiet.caiId = vo.id;
                                    caiDiet.caiName = vo.names;
                                    caiDiet.caiIngredietId = ingreDiet.ingredietId;
                                    caiDiet.caiIngrediet = ingreDiet.ingredietName;
                                    caiDiet.weight = ingreDiet.weight;
                                    caiDiet.day = day;
                                    caiDiet.per = Math.Round(ingreDiet.weight / lstCaiIngredient.FindAll(r => r.caiId == vo.id).Sum(c => c.weight), 1); 

                                    if (lstCaiDiet.Any(r => r.day == caiDiet.day && r.mealId == caiDiet.mealId))
                                    {
                                        EntityDietDetails voDietClone = lstCaiDiet.Find(r => r.day == caiDiet.day && r.mealId == caiDiet.mealId);

                                        if (!voDietClone.lstDetailsCai.Any((u => u.day == caiDiet.day && u.mealId == caiDiet.mealId && u.caiId == caiDiet.caiId)))
                                        {
                                            EntityDietdetailsCai voC = new EntityDietdetailsCai();
                                            voC.day = caiDiet.day;
                                            voC.mealId = caiDiet.mealId;
                                            voC.caiId = caiDiet.caiId;
                                            voC.caiName = caiDiet.caiName;
                                            voDietClone.lstDetailsCai.Add(voC);
                                        }
                                    }
                                    else
                                    {
                                        caiDiet.lstDetailsCai = new List<EntityDietdetailsCai>();

                                        lstCaiDiet.Add(caiDiet);
                                    }

                                    data2.Add(caiDiet);
                                }
                            }

                            isSave = true;
                        }
                    }
                }

                if (lstCaiDiet != null)
                {
                    foreach (var temp in lstCaiDiet)
                    {
                        if (temp.lstDetailsCai != null)
                        {
                            foreach (var caiTemp in temp.lstDetailsCai)
                            {
                                List<EntityDietDetails> details = data2.FindAll(r => r.recId == caiTemp.recId && r.day == caiTemp.day && r.mealId == caiTemp.mealId && r.caiId == caiTemp.caiId);
                                if (details != null)
                                {
                                    caiTemp.lstDietdetailsIngrediet = new List<EntityDietDetails>();

                                    foreach (var temp2 in details)
                                    {
                                        EntityDietDetails Ingrediet = new EntityDietDetails();
                                        Ingrediet = temp2;
                                        caiTemp.lstDietdetailsIngrediet.Add(Ingrediet);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
