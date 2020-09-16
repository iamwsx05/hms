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
    public partial class frmPopup206040102 : frmBasePopup
    {
        public frmPopup206040102(List<EntityDicDientIngredient> _lstDientIngrdient = null)
        {
            InitializeComponent();
            lstDientIngrdient = _lstDientIngrdient;
        }

        List<EntityDicDientIngredient> lstDientIngrdient { get; set; }


        void CalcNutrition(List<EntityDicDientIngredient> lstDientIngrdient)
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
            if (lstDientIngrdient != null && lstDientIngrdient.Count > 0)
            {
                foreach(var vo in lstDientIngrdient)
                {
                    kcal += vo.kCal * vo.weight;
                    proteint += vo.proteint * vo.weight;
                    fat += vo.fat * vo.weight;
                    cho += vo.cho * vo.weight;
                    brxxw += vo.brxxw * vo.weight;
                    dgc += vo.dgc * vo.weight;
                    vitaminsA += vo.vitaminsA * vo.weight;
                    vitaminsD += vo.vitaminsD * vo.weight;
                    vitaminsE += vo.vitaminsE * vo.weight;
                    vitaminsB1 += vo.thiamin * vo.weight;
                    vitaminsB2 += vo.riboflavin * vo.weight;
                    vitaminsC += vo.vitaminsC * vo.weight;
                    ca += vo.ca * vo.weight;
                    fe += vo.fe * vo.weight;
                }

                lblKcal.Text = (kcal / 100).ToString("0.00") + " Kcal" ;
                lblProteint.Text = (proteint / 100).ToString("0.00") + " g";
                lblFat.Text = (fat / 100).ToString("0.00") + "  g";
                lblCho.Text = (cho / 100).ToString("0.00") + "  g";
                lblBrxxw.Text = (brxxw / 100).ToString("0.00") + " g";
                lblDgc.Text = (dgc / 100).ToString("0.00") + " g";
                lblVitaminsA.Text = (vitaminsA / 100).ToString("0.00") + " mg";
                lblVitaminsD.Text = (vitaminsD / 100).ToString("0.00") + " mg";
                lblVitaminsE.Text = (vitaminsE / 100).ToString("0.00") + " mg";
                lblVitaminsB1.Text = (vitaminsB1 / 100).ToString("0.00") + " mg";
                lblVitaminsB2.Text = (vitaminsB2 / 100).ToString("0.00") + " mg";
                lblVitaminsC.Text = (vitaminsC / 100).ToString("0.00") + " mg";
                lblCa.Text = (ca / 100).ToString("0.00") + " mg";
                lblFe.Text = (fe / 100).ToString("0.00") + " mg";
            }
        }

        private void frmPopup206040102_Load(object sender, EventArgs e)
        {
            CalcNutrition(lstDientIngrdient);
        }
    }
}
