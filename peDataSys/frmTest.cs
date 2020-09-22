using Common.Controls;
using DevExpress.XtraGrid;
using Hms.Biz;
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
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            this.gvDietCai.ViewCaption = "菜";
            this.gvIngrediet.ViewCaption = "原料";

            ////构建GridLevelNode
            //var topNode = new GridLevelNode();
            //topNode.LevelTemplate = this.gvDietCai;           //这里是对应的视图
            //topNode.RelationName = "lstDetailsCai";   //这里对应集合的属性名称

            ////构建GridLevelNode
            //var secondNode = new GridLevelNode();
            //secondNode.LevelTemplate = this.gvIngrediet;        //这里是对应的视图
            //secondNode.RelationName = "lstDietdetailsIngrediet";//这里对应集合的属性名称

            ////需要添加节点的层级关系，类似Tree节点处理
            //topNode.Nodes.Add(secondNode);
            ////最后添加节点到集合里面
            //this.gcData.LevelTree.Nodes.Add(topNode);


            Biz206 biz206 = new Biz206();

            string dietRecIdStr = "(14)";
            List<EntityDietdetailsCai> lstDietDetailCai = null;
            List<EntityDietdetailsIngrediet> lstDietdetailsIngrediet = null;
            //List<EntityDietDetails> lstDietRecord = biz206.GetDietDetails(dietRecIdStr,out lstDietDetailCai,out lstDietdetailsIngrediet);

            //this.gcData.DataSource = lstDietRecord;
            this.gcData.RefreshDataSource();

        }

        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {




        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridLookUpEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string content = gridLookUpEdit1.Text.Trim();

                if (string.IsNullOrEmpty(content))
                {
                    return;
                }
                Biz201 biz = new Biz201();
                List<EntityClientInfo> lstClientInfo = biz.GetClientInfos(content);
                gridLookUpEdit1.Properties.DataSource = lstClientInfo;
                gridLookUpEdit1.ShowPopup();

                gridLookUpEdit1.Properties.DataSource = lstClientInfo;
                gridLookUpEdit1.ShowPopup();
            }
        }


        // lstClientInfo = proxy.Service.GetClientInfos(search);
    }
}
