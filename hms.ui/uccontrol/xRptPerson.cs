using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Windows.Forms;
using Hms.Entity;

namespace Hms.Ui
{
    public partial class xRptPerson : DevExpress.XtraReports.UI.XtraReport
    {
        public xRptPerson(EntityClientReport rpt = null)
        {
            InitializeComponent();
            SetDataBind(rpt);
        }
        private void SetDataBind(EntityClientReport report)//绑定数据源  
        {
            try
            {
                decimal totalDf = 0;
                string moreHighFx = string.Empty;
                string highFx = string.Empty;
                string lowFx = string.Empty;
                string moreLowFx = string.Empty;

                if (report == null)
                    return;

                pic02.Image = report.image02;
                pic03.Image = report.image03;
                pic04.Image = report.image04;
                pic05.Image = report.image05;
                
                pic07.Image = report.image07;
                picGxyTip.Image = report.imageTip;
                picGzssTip.Image = report.imageTip;
                picAzhmzTip.Image = report.imageTip;
                picFaTip.Image = report.imageTip;
                picGaTip.Image = report.imageTip;
                picGxbTip.Image = report.imageTip;
                picQlxaTip.Image = report.imageTip;
                picTnbTip.Image = report.imageTip;
                picWaTip.Image = report.imageTip;
                picXzycTip.Image = report.imageTip;
                picNzzTip.Image = report.imageTip;
                picFpzTip.Image = report.imageTip;

                lblPageHeader.Text = report.reportNo;
                lblClientName.Text = report.clientName;
                lblClientNo.Text = report.clientNo;
                lblCompany.Text = report.company;
                lblReportDate.Text = report.reportDate;
                lblSex.Text = report.sex;
                lblAge.Text = report.age;
                lblQnDate2.Text = report.qnDate;
                lblQnDate.Text = report.qnDate;
                lblTjDate.Text = report.reportDate;
                #region 健康汇总
                lblTjSumup.Text = string.IsNullOrEmpty(report.tjSumup.Trim()) ? "" : report.tjSumup;
                #endregion

                #region  重要指标
                this.picMainIdicate.DataBindings.Add("Image", report.lstMainItem, "pic");
                this.cellItem.DataBindings.Add("Text", report.lstMainItem, "itemName");//
                this.cellResult.DataBindings.Add("Text", report.lstMainItem, "itemValue");//
                this.cellRange.DataBindings.Add("Text", report.lstMainItem, "itemRefrange");//
                this.cellUnit.DataBindings.Add("Text", report.lstMainItem, "itemUnits");//
                this.dtRptMainIndicate.DataSource = report.lstMainItem;
                #endregion

                #region 高血压
                this.dtRptGxy.Visible = false;
                EntityRptModelAcess gxyMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 1);
                if(gxyMdAccess != null)
                {
                    if(gxyMdAccess.lstModelParam != null)
                    {
                        if(gxyMdAccess.lstModelParam.Count > 0)
                        {
                            this.dtRptGxy.Visible = true;
                            this.picGxy.DataBindings.Add("Image", gxyMdAccess.lstModelParam, "pic");
                            this.cellGxyItem.DataBindings.Add("Text", gxyMdAccess.lstModelParam, "itemName");//
                            this.cellGxyResult.DataBindings.Add("Text", gxyMdAccess.lstModelParam, "result");//
                            this.cellGxyRange.DataBindings.Add("Text", gxyMdAccess.lstModelParam, "range");//
                            this.cellGxyUnit.DataBindings.Add("Text", gxyMdAccess.lstModelParam, "unit");//
                            this.dtRptGxyGroup.DataSource = gxyMdAccess.lstModelParam;
                            this.lblGxyDf.Text = gxyMdAccess.df.ToString("0.00");
                            this.lblGxyAbasableDf.Text = gxyMdAccess.reduceDf.ToString("0.00");
                            this.lblGxyBestDf.Text = gxyMdAccess.bestDf.ToString("0.00");

                            this.picGxyFx01.Image = gxyMdAccess.imgFx01;
                            this.picGxyFx02.Image = gxyMdAccess.imgFx02;
                            this.picGxyFx03.Image = gxyMdAccess.imgFx03;
                            this.picGxyFx04.Image = gxyMdAccess.imgFx04;

                            if (gxyMdAccess.df > 5 && gxyMdAccess.df < 18)
                                this.lblGxyTip.Text = "【说明】您在未来5~10年高血压的患病风险为" + this.lblGxyDf.Text + "%，有一定风险，但仍然低于同年龄、同性别人群的平均水平。";
                            if (gxyMdAccess.df <= 5)
                                this.lblGxyTip.Text = "【说明】您在未来5~10年高血压的患病风险为" + this.lblGxyDf.Text + "%，远低于同年龄、同性别人群的平均水平。";
                            if (gxyMdAccess.df >= 18 && gxyMdAccess.df < 50)
                                this.lblGxyTip.Text = "【说明】您在未来5~10年高血压的患病风险为" + this.lblGxyDf.Text + "%，高于同年龄、同性别人群的平均水平。";
                            if (gxyMdAccess.df >= 50)
                                this.lblGxyTip.Text = "【说明】您在未来5~10年高血压的患病风险为" + this.lblGxyDf.Text + "%，远高于同年龄、同性别人群的平均水平。";

                            this.xrChartGxy.DataSource = gxyMdAccess.lstEvaluate;
                            this.xrChartGxy.Series[0].SetDataMembers("evaluationName", "result");
                            if (gxyMdAccess.lstPoint != null)
                            {
                                for (int i = 0; i < gxyMdAccess.lstPoint.Count; i++)
                                {
                                    if (i == 0)
                                        this.lblGxyPoint1.Text = string.IsNullOrEmpty(gxyMdAccess.lstPoint[0]) ? "" : "1、" + gxyMdAccess.lstPoint[0];
                                    if (i == 1)
                                        this.lblGxyPoint2.Text = string.IsNullOrEmpty(gxyMdAccess.lstPoint[1]) ? "" : "2、" + gxyMdAccess.lstPoint[1];
                                    if (i == 2)
                                        this.lblGxyPoint3.Text = string.IsNullOrEmpty(gxyMdAccess.lstPoint[2]) ? "" : "3、" + gxyMdAccess.lstPoint[2];
                                    if (i == 3)
                                        this.lblGxyPoint4.Text = string.IsNullOrEmpty(gxyMdAccess.lstPoint[3]) ? "" : "4、" + gxyMdAccess.lstPoint[3];
                                    if (i == 4)
                                        this.lblGxyPoint5.Text = string.IsNullOrEmpty(gxyMdAccess.lstPoint[4]) ? "" : "5、" + gxyMdAccess.lstPoint[4];
                                    if (i == 5)
                                        this.lblGxyPoint6.Text = string.IsNullOrEmpty(gxyMdAccess.lstPoint[5]) ? "" : "6、" + gxyMdAccess.lstPoint[5];
                                    if (i == 6)
                                        this.lblGxyPoint7.Text = string.IsNullOrEmpty(gxyMdAccess.lstPoint[6]) ? "" : "7、" + gxyMdAccess.lstPoint[6];
                                    if (i == 7)
                                        this.lblGxyPoint8.Text = string.IsNullOrEmpty(gxyMdAccess.lstPoint[7]) ? "" : "8、" + gxyMdAccess.lstPoint[7];
                                }
                            }
                            totalDf += gxyMdAccess.df;

                            if (gxyMdAccess.resultStr == "很高危")
                                moreHighFx += "高血压、";
                            if (gxyMdAccess.resultStr == "高危")
                                highFx += "高血压、";
                            if (gxyMdAccess.resultStr == "低危")
                                lowFx += "高血压、";
                            if (gxyMdAccess.resultStr == "很低危")
                                moreLowFx += "高血压、";
                        }
                    } 
                }
                #endregion

                #region 糖尿病
                this.dtRptTnb.Visible = false;
                EntityRptModelAcess tnbMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 2);
                if (tnbMdAccess != null)
                {
                    if(tnbMdAccess.lstModelParam != null)
                    {
                        if(tnbMdAccess.lstModelParam.Count > 0)
                        {
                            this.dtRptTnb.Visible = true;
                            this.picHintTnb.DataBindings.Add("Image", tnbMdAccess.lstModelParam, "pic");
                            this.cellTnbItem.DataBindings.Add("Text", tnbMdAccess.lstModelParam, "itemName");//
                            this.cellTnbResult.DataBindings.Add("Text", tnbMdAccess.lstModelParam, "result");//
                            this.cellTnbRange.DataBindings.Add("Text", tnbMdAccess.lstModelParam, "range");//
                            this.cellTnbUnit.DataBindings.Add("Text", tnbMdAccess.lstModelParam, "unit");//
                            this.dtRptTnbGroup.DataSource = tnbMdAccess.lstModelParam;
                            this.lblTnbDf.Text = tnbMdAccess.df.ToString("0.00");
                            this.lblTnbAbasableDf.Text = tnbMdAccess.reduceDf.ToString("0.00");
                            this.lblTnbBestDf.Text = tnbMdAccess.bestDf.ToString("0.00");

                            this.picTnbFx01.Image = tnbMdAccess.imgFx01;
                            this.picTnbFx02.Image = tnbMdAccess.imgFx02;
                            this.picTnbFx03.Image = tnbMdAccess.imgFx03;
                            this.picTnbFx04.Image = tnbMdAccess.imgFx04;

                            if (tnbMdAccess.df > 5 && tnbMdAccess.df < 18)
                                this.lblTnbTip.Text = "【说明】您在未来5~10年糖尿病的患病风险为" + this.lblTnbDf.Text + "%，有一定风险，但仍然低于同年龄、同性别人群的平均水平。";
                            if (tnbMdAccess.df <= 5)
                                this.lblTnbTip.Text = "【说明】您在未来5~10年糖尿病的患病风险为" + this.lblTnbDf.Text + "%，远低于同年龄、同性别人群的平均水平。";
                            if (tnbMdAccess.df >= 18 && tnbMdAccess.df < 50)
                                this.lblTnbTip.Text = "【说明】您在未来5~10年糖尿病的患病风险为" + this.lblTnbDf.Text + "%，高于同年龄、同性别人群的平均水平。";
                            if (tnbMdAccess.df >= 50)
                                this.lblTnbTip.Text = "【说明】您在未来5~10年糖尿病的患病风险为" + this.lblTnbDf.Text + "%，远高于同年龄、同性别人群的平均水平。";

                            this.xrChartTnb.Series[0].SetDataMembers("evaluationName", "result");
                            this.xrChartTnb.Series[0].DataSource = tnbMdAccess.lstEvaluate[0];
                            this.xrChartTnb.Series[1].SetDataMembers("evaluationName", "result");
                            this.xrChartTnb.Series[1].DataSource = tnbMdAccess.lstEvaluate[1];
                            this.xrChartTnb.Series[2].SetDataMembers("evaluationName", "result");
                            this.xrChartTnb.Series[2].DataSource = tnbMdAccess.lstEvaluate[2];

                            if (tnbMdAccess.lstPoint != null)
                            {
                                for (int i = 0; i < tnbMdAccess.lstPoint.Count; i++)
                                {
                                    if (i == 0)
                                        this.lblTnbPoint1.Text = string.IsNullOrEmpty(tnbMdAccess.lstPoint[0]) ? "" : "1、" + tnbMdAccess.lstPoint[0];
                                    if (i == 1)
                                        this.lblTnbPoint2.Text = string.IsNullOrEmpty(tnbMdAccess.lstPoint[1]) ? "" : "2、" + tnbMdAccess.lstPoint[1];
                                    if (i == 2)
                                        this.lblTnbPoint3.Text = string.IsNullOrEmpty(tnbMdAccess.lstPoint[2]) ? "" : "3、" + tnbMdAccess.lstPoint[2];
                                    if (i == 3)
                                        this.lblTnbPoint4.Text = string.IsNullOrEmpty(tnbMdAccess.lstPoint[3]) ? "" : "4、" + tnbMdAccess.lstPoint[3];
                                    if (i == 4)
                                        this.lblTnbPoint5.Text = string.IsNullOrEmpty(tnbMdAccess.lstPoint[4]) ? "" : "5、" + tnbMdAccess.lstPoint[4];
                                    if (i == 5)
                                        this.lblTnbPoint6.Text = string.IsNullOrEmpty(tnbMdAccess.lstPoint[5]) ? "" : "6、" + tnbMdAccess.lstPoint[5];
                                    if (i == 6)
                                        this.lblTnbPoint7.Text = string.IsNullOrEmpty(tnbMdAccess.lstPoint[6]) ? "" : "7、" + tnbMdAccess.lstPoint[6];
                                    if (i == 7)
                                        this.lblTnbPoint8.Text = string.IsNullOrEmpty(tnbMdAccess.lstPoint[7]) ? "" : "8、" + tnbMdAccess.lstPoint[7];
                                }
                            }
                            totalDf += tnbMdAccess.df;
                            if (tnbMdAccess.resultStr == "很高危")
                                moreHighFx += "糖尿病、";
                            if (tnbMdAccess.resultStr == "高危")
                                highFx += "糖尿病、";
                            if (tnbMdAccess.resultStr == "低危")
                                lowFx += "糖尿病、";
                            if (tnbMdAccess.resultStr == "很低危")
                                moreLowFx += "糖尿病、";
                        }
                    }
                    
                }
                #endregion

                #region 冠心病
                this.dtRptGxb.Visible = false;
                EntityRptModelAcess gxbMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 3);
                if (gxbMdAccess != null)
                {
                    if(gxbMdAccess.lstModelParam != null)
                    {
                        if(gxbMdAccess.lstModelParam.Count > 0)
                        {
                            this.dtRptGxb.Visible = true;
                            this.picHintGxb.DataBindings.Add("Image", gxbMdAccess.lstModelParam, "pic");
                            this.cellGxbItem.DataBindings.Add("Text", gxbMdAccess.lstModelParam, "itemName");//
                            this.cellGxbResult.DataBindings.Add("Text", gxbMdAccess.lstModelParam, "result");//
                            this.cellGxbRange.DataBindings.Add("Text", gxbMdAccess.lstModelParam, "range");//
                            this.cellGxbUnit.DataBindings.Add("Text", gxbMdAccess.lstModelParam, "unit");//
                            this.dtRptGxbGroup.DataSource = gxbMdAccess.lstModelParam;
                            this.lblGxbDf.Text = gxbMdAccess.df.ToString("0.00");
                            this.lblGxbAbasableDf.Text = gxbMdAccess.reduceDf.ToString("0.00");
                            this.lblGxbBestDf.Text = gxbMdAccess.bestDf.ToString("0.00");

                            this.picGxbFx01.Image = gxbMdAccess.imgFx01;
                            this.picGxbFx02.Image = gxbMdAccess.imgFx02;
                            this.picGxbFx03.Image = gxbMdAccess.imgFx03;
                            this.picGxbFx04.Image = gxbMdAccess.imgFx04;

                            if (gxbMdAccess.df > 5 && gxbMdAccess.df < 18)
                                this.lblGxbTip.Text = "【说明】您在未来5~10年冠心病的患病风险为" + this.lblGxbDf.Text + "%，有一定风险，但仍然低于同年龄、同性别人群的平均水平。";
                            if (gxbMdAccess.df <= 5)
                                this.lblGxbTip.Text = "【说明】您在未来5~10年冠心病的患病风险为" + this.lblGxbDf.Text + "%，远低于同年龄、同性别人群的平均水平。";
                            if (gxbMdAccess.df >= 18 && gxbMdAccess.df < 50)
                                this.lblGxbTip.Text = "【说明】您在未来5~10年冠心病的患病风险为" + this.lblGxbDf.Text + "%，高于同年龄、同性别人群的平均水平。";
                            if (gxbMdAccess.df >= 50)
                                this.lblGxbTip.Text = "【说明】您在未来5~10年冠心病的患病风险为" + this.lblGxbDf.Text + "%，远高于同年龄、同性别人群的平均水平。";

                            this.xrChartGxb.Series[0].SetDataMembers("evaluationName", "result");
                            this.xrChartGxb.Series[0].DataSource = gxbMdAccess.lstEvaluate[0];
                            this.xrChartGxb.Series[1].SetDataMembers("evaluationName", "result");
                            this.xrChartGxb.Series[1].DataSource = gxbMdAccess.lstEvaluate[1];
                            this.xrChartGxb.Series[2].SetDataMembers("evaluationName", "result");
                            this.xrChartGxb.Series[2].DataSource = gxbMdAccess.lstEvaluate[2];

                            if (gxbMdAccess.lstPoint != null)
                            {
                                for (int i = 0; i < gxbMdAccess.lstPoint.Count; i++)
                                {
                                    if (i == 0)
                                        this.lblGxbPoint1.Text = string.IsNullOrEmpty(gxbMdAccess.lstPoint[0]) ? "" : "1、" + gxbMdAccess.lstPoint[0];
                                    if (i == 1)
                                        this.lblGxbPoint2.Text = string.IsNullOrEmpty(gxbMdAccess.lstPoint[1]) ? "" : "2、" + gxbMdAccess.lstPoint[1];
                                    if (i == 2)
                                        this.lblGxbPoint3.Text = string.IsNullOrEmpty(gxbMdAccess.lstPoint[2]) ? "" : "3、" + gxbMdAccess.lstPoint[2];
                                    if (i == 3)
                                        this.lblGxbPoint4.Text = string.IsNullOrEmpty(gxbMdAccess.lstPoint[3]) ? "" : "4、" + gxbMdAccess.lstPoint[3];
                                    if (i == 4)
                                        this.lblGxbPoint5.Text = string.IsNullOrEmpty(gxbMdAccess.lstPoint[4]) ? "" : "5、" + gxbMdAccess.lstPoint[4];
                                    if (i == 5)
                                        this.lblGxbPoint6.Text = string.IsNullOrEmpty(gxbMdAccess.lstPoint[5]) ? "" : "6、" + gxbMdAccess.lstPoint[5];
                                    if (i == 6)
                                        this.lblGxbPoint7.Text = string.IsNullOrEmpty(gxbMdAccess.lstPoint[6]) ? "" : "7、" + gxbMdAccess.lstPoint[6];
                                    if (i == 7)
                                        this.lblGxbPoint8.Text = string.IsNullOrEmpty(gxbMdAccess.lstPoint[7]) ? "" : "8、" + gxbMdAccess.lstPoint[7];
                                }
                            }
                            totalDf += gxbMdAccess.df;
                            if (gxbMdAccess.resultStr == "很高危")
                                moreHighFx += "冠心病、";
                            if (gxbMdAccess.resultStr == "高危")
                                highFx += "冠心病、";
                            if (gxbMdAccess.resultStr == "低危")
                                lowFx += "冠心病、";
                            if (gxbMdAccess.resultStr == "很低危")
                                moreLowFx += "冠心病、";
                        }
                    }
                    
                }
                #endregion

                #region 血脂异常
                this.dtRptXzyc.Visible = false;
                EntityRptModelAcess xzycMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 4);
                if (xzycMdAccess != null)
                {
                    if(xzycMdAccess.lstModelParam != null)
                    {
                        if(xzycMdAccess.lstModelParam.Count > 0)
                        {
                            this.dtRptXzyc.Visible = true;
                            this.picHintXzyc.DataBindings.Add("Image", xzycMdAccess.lstModelParam, "pic");
                            this.cellXzycItem.DataBindings.Add("Text", xzycMdAccess.lstModelParam, "itemName");//
                            this.cellXzycResult.DataBindings.Add("Text", xzycMdAccess.lstModelParam, "result");//
                            this.cellXzycRange.DataBindings.Add("Text", xzycMdAccess.lstModelParam, "range");//
                            this.cellXzycUnit.DataBindings.Add("Text", xzycMdAccess.lstModelParam, "unit");//
                            this.dtRptXzycGroup.DataSource = xzycMdAccess.lstModelParam;
                            this.lblXzycDf.Text = xzycMdAccess.df.ToString("0.00");
                            this.lblXzycAbasableDf.Text = xzycMdAccess.reduceDf.ToString("0.00");
                            this.lblXzycBestDf.Text = xzycMdAccess.bestDf.ToString("0.00");

                            this.picXzycFx01.Image = xzycMdAccess.imgFx01;
                            this.picXzycFx02.Image = xzycMdAccess.imgFx02;
                            this.picXzycFx03.Image = xzycMdAccess.imgFx03;
                            this.picXzycFx04.Image = xzycMdAccess.imgFx04;

                            if (xzycMdAccess.df > 5 && xzycMdAccess.df < 18)
                                this.lblXzycTip.Text = "【说明】您在未来5~10年血脂异常的患病风险为" + this.lblXzycDf.Text + "%，有一定风险，但仍然低于同年龄、同性别人群的平均水平。";
                            if (xzycMdAccess.df <= 5)
                                this.lblXzycTip.Text = "【说明】您在未来5~10年血脂异常的患病风险为" + this.lblXzycDf.Text + "%，远低于同年龄、同性别人群的平均水平。";
                            if (xzycMdAccess.df >= 18 && xzycMdAccess.df < 50)
                                this.lblXzycTip.Text = "【说明】您在未来5~10年血脂异常的患病风险为" + this.lblXzycDf.Text + "%，高于同年龄、同性别人群的平均水平。";
                            if (xzycMdAccess.df >= 50)
                                this.lblXzycTip.Text = "【说明】您在未来5~10年血脂异常的患病风险为" + this.lblXzycDf.Text + "%，远高于同年龄、同性别人群的平均水平。";

                            this.xrChartXzyc.Series[0].SetDataMembers("evaluationName", "result");
                            this.xrChartXzyc.Series[0].DataSource = xzycMdAccess.lstEvaluate[0];
                            this.xrChartXzyc.Series[1].SetDataMembers("evaluationName", "result");
                            this.xrChartXzyc.Series[1].DataSource = xzycMdAccess.lstEvaluate[1];
                            this.xrChartXzyc.Series[2].SetDataMembers("evaluationName", "result");
                            this.xrChartXzyc.Series[2].DataSource = xzycMdAccess.lstEvaluate[2];

                            if (xzycMdAccess.lstPoint != null)
                            {
                                for (int i = 0; i < xzycMdAccess.lstPoint.Count; i++)
                                {
                                    if (i == 0)
                                        this.lblXzycPoint1.Text = string.IsNullOrEmpty(xzycMdAccess.lstPoint[0]) ? "" : "1、" + xzycMdAccess.lstPoint[0];
                                    if (i == 1)
                                        this.lblXzycPoint2.Text = string.IsNullOrEmpty(xzycMdAccess.lstPoint[1]) ? "" : "2、" + xzycMdAccess.lstPoint[1];
                                    if (i == 2)
                                        this.lblXzycPoint3.Text = string.IsNullOrEmpty(xzycMdAccess.lstPoint[2]) ? "" : "3、" + xzycMdAccess.lstPoint[2];
                                    if (i == 3)
                                        this.lblXzycPoint4.Text = string.IsNullOrEmpty(xzycMdAccess.lstPoint[3]) ? "" : "4、" + xzycMdAccess.lstPoint[3];
                                    if (i == 4)
                                        this.lblXzycPoint5.Text = string.IsNullOrEmpty(xzycMdAccess.lstPoint[4]) ? "" : "5、" + xzycMdAccess.lstPoint[4];
                                    if (i == 5)
                                        this.lblXzycPoint6.Text = string.IsNullOrEmpty(xzycMdAccess.lstPoint[5]) ? "" : "6、" + xzycMdAccess.lstPoint[5];
                                    if (i == 6)
                                        this.lblXzycPoint7.Text = string.IsNullOrEmpty(xzycMdAccess.lstPoint[6]) ? "" : "7、" + xzycMdAccess.lstPoint[6];
                                    if (i == 7)
                                        this.lblXzycPoint8.Text = string.IsNullOrEmpty(xzycMdAccess.lstPoint[7]) ? "" : "8、" + xzycMdAccess.lstPoint[7];
                                }
                            }
                            totalDf += xzycMdAccess.df;
                            if (xzycMdAccess.resultStr == "很高危")
                                moreHighFx += "血脂异常、";
                            if (xzycMdAccess.resultStr == "高危")
                                highFx += "血脂异常、";
                            if (xzycMdAccess.resultStr == "低危")
                                lowFx += "血脂异常、";
                            if (xzycMdAccess.resultStr == "很低危")
                                moreLowFx += "血脂异常、";
                        }
                    }
                }

                #endregion

                #region 肥胖症
                this.dtRptFpz.Visible = false;
                EntityRptModelAcess fpzMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 5);
                if (gxyMdAccess != null)
                {
                    if(fpzMdAccess.lstModelParam != null)
                    {
                        if(fpzMdAccess.lstModelParam.Count > 0)
                        {
                            this.dtRptFpz.Visible = true;
                            this.picHintFpz.DataBindings.Add("Image", fpzMdAccess.lstModelParam, "pic");
                            this.cellFpzItem.DataBindings.Add("Text", fpzMdAccess.lstModelParam, "itemName");//
                            this.cellFpzResult.DataBindings.Add("Text", fpzMdAccess.lstModelParam, "result");//
                            this.cellFpzRange.DataBindings.Add("Text", fpzMdAccess.lstModelParam, "range");//
                            this.cellFpzUnit.DataBindings.Add("Text", fpzMdAccess.lstModelParam, "unit");//
                            this.dtRptFpzGroup.DataSource = fpzMdAccess.lstModelParam;
                            this.lblFpzDf.Text = fpzMdAccess.df.ToString("0.00");
                            this.lblFpzAbasableDf.Text = fpzMdAccess.reduceDf.ToString("0.00");
                            this.lblFpzBestDf.Text = fpzMdAccess.bestDf.ToString("0.00");

                            this.picFpzFx01.Image = fpzMdAccess.imgFx01;
                            this.picFpzFx02.Image = fpzMdAccess.imgFx02;
                            this.picFpzFx03.Image = fpzMdAccess.imgFx03;
                            this.picFpzFx04.Image = fpzMdAccess.imgFx04;

                            if (fpzMdAccess.df > 5 && fpzMdAccess.df < 18)
                                this.lblFpzTip.Text = "【说明】您在未来5~10年肥胖症的患病风险为" + this.lblFpzDf.Text + "%，有一定风险，但仍然低于同年龄、同性别人群的平均水平。";
                            if (fpzMdAccess.df <= 5)
                                this.lblFpzTip.Text = "【说明】您在未来5~10年肥胖症的患病风险为" + this.lblFpzDf.Text + "%，远低于同年龄、同性别人群的平均水平。";
                            if (fpzMdAccess.df >= 18 && fpzMdAccess.df < 50)
                                this.lblFpzTip.Text = "【说明】您在未来5~10年肥胖症的患病风险为" + this.lblFpzDf.Text + "%，高于同年龄、同性别人群的平均水平。";
                            if (fpzMdAccess.df >= 50)
                                this.lblFpzTip.Text = "【说明】您在未来5~10年肥胖症的患病风险为" + this.lblFpzDf.Text + "%，远高于同年龄、同性别人群的平均水平。";

                            this.xrChartFpz.Series[0].SetDataMembers("evaluationName", "result");
                            this.xrChartFpz.Series[0].DataSource = fpzMdAccess.lstEvaluate[0];
                            this.xrChartFpz.Series[1].SetDataMembers("evaluationName", "result");
                            this.xrChartFpz.Series[1].DataSource = fpzMdAccess.lstEvaluate[1];
                            this.xrChartFpz.Series[2].SetDataMembers("evaluationName", "result");
                            this.xrChartFpz.Series[2].DataSource = fpzMdAccess.lstEvaluate[2];

                            if (fpzMdAccess.lstPoint != null)
                            {
                                for (int i = 0; i < fpzMdAccess.lstPoint.Count; i++)
                                {
                                    if (i == 0)
                                        this.lblFpzPoint1.Text = string.IsNullOrEmpty(fpzMdAccess.lstPoint[0]) ? "" : "1、" + fpzMdAccess.lstPoint[0];
                                    if (i == 1)
                                        this.lblFpzPoint2.Text = string.IsNullOrEmpty(fpzMdAccess.lstPoint[1]) ? "" : "2、" + fpzMdAccess.lstPoint[1];
                                    if (i == 2)
                                        this.lblFpzPoint3.Text = string.IsNullOrEmpty(fpzMdAccess.lstPoint[2]) ? "" : "3、" + fpzMdAccess.lstPoint[2];
                                    if (i == 3)
                                        this.lblFpzPoint4.Text = string.IsNullOrEmpty(fpzMdAccess.lstPoint[3]) ? "" : "4、" + fpzMdAccess.lstPoint[3];
                                    if (i == 4)
                                        this.lblFpzPoint5.Text = string.IsNullOrEmpty(fpzMdAccess.lstPoint[4]) ? "" : "5、" + fpzMdAccess.lstPoint[4];
                                    if (i == 5)
                                        this.lblFpzPoint6.Text = string.IsNullOrEmpty(fpzMdAccess.lstPoint[5]) ? "" : "6、" + fpzMdAccess.lstPoint[5];
                                    if (i == 6)
                                        this.lblFpzPoint7.Text = string.IsNullOrEmpty(fpzMdAccess.lstPoint[6]) ? "" : "7、" + fpzMdAccess.lstPoint[6];
                                    if (i == 7)
                                        this.lblFpzPoint8.Text = string.IsNullOrEmpty(fpzMdAccess.lstPoint[7]) ? "" : "8、" + fpzMdAccess.lstPoint[7];
                                }
                            }
                            totalDf += fpzMdAccess.df;
                            if (fpzMdAccess.resultStr == "很高危")
                                moreHighFx += "肥胖症、";
                            if (fpzMdAccess.resultStr == "高危")
                                highFx += "肥胖症、";
                            if (fpzMdAccess.resultStr == "低危")
                                lowFx += "肥胖症、";
                            if (fpzMdAccess.resultStr == "很低危")
                                moreLowFx += "肥胖症、";
                        }
                    }
                    
                }
                #endregion

                #region 脑卒中
                this.dtRptNzz.Visible = false;
                EntityRptModelAcess nzzMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 6);
                if (nzzMdAccess != null)
                {
                    if (nzzMdAccess.lstModelParam != null)
                    {
                        if (nzzMdAccess.lstModelParam.Count > 0)
                        {
                            this.dtRptNzz.Visible = true;
                            this.picHintNzz.DataBindings.Add("Image", nzzMdAccess.lstModelParam, "pic");
                            this.cellNzzItem.DataBindings.Add("Text", nzzMdAccess.lstModelParam, "itemName");//
                            this.cellNzzResult.DataBindings.Add("Text", nzzMdAccess.lstModelParam, "result");//
                            this.cellNzzRange.DataBindings.Add("Text", nzzMdAccess.lstModelParam, "range");//
                            this.cellNzzUnit.DataBindings.Add("Text", nzzMdAccess.lstModelParam, "unit");//
                            this.dtRptNzzGroup.DataSource = nzzMdAccess.lstModelParam;
                            this.lblNzzDf.Text = nzzMdAccess.df.ToString("0.00");
                            this.lblNzzAbasableDf.Text = nzzMdAccess.reduceDf.ToString("0.00");
                            this.lblNzzBestDf.Text = nzzMdAccess.bestDf.ToString("0.00");

                            this.picNzzFx01.Image = nzzMdAccess.imgFx01;
                            this.picNzzFx02.Image = nzzMdAccess.imgFx02;
                            this.picNzzFx03.Image = nzzMdAccess.imgFx03;
                            this.picNzzFx04.Image = nzzMdAccess.imgFx04;

                            if (nzzMdAccess.df > 5 && nzzMdAccess.df < 18)
                                this.lblNzzTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblNzzDf.Text + "%，有一定风险，但仍然低于同年龄、同性别人群的平均水平。";
                            if (nzzMdAccess.df <= 5)
                                this.lblNzzTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblNzzDf.Text + "%，远低于同年龄、同性别人群的平均水平。";
                            if (nzzMdAccess.df >= 18 && nzzMdAccess.df < 50)
                                this.lblNzzTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblNzzDf.Text + "%，高于同年龄、同性别人群的平均水平。";
                            if (nzzMdAccess.df >= 50)
                                this.lblNzzTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblNzzDf.Text + "%，远高于同年龄、同性别人群的平均水平。";

                            this.xrChartNzz.DataSource = nzzMdAccess.lstEvaluate;
                            this.xrChartNzz.Series[0].SetDataMembers("evaluationName", "result");

                            if (nzzMdAccess.lstPoint != null)
                            {
                                for (int i = 0; i < nzzMdAccess.lstPoint.Count; i++)
                                {
                                    if (i == 0)
                                        this.lblNzzPoint1.Text = string.IsNullOrEmpty(nzzMdAccess.lstPoint[0]) ? "" : "1、" + nzzMdAccess.lstPoint[0];
                                    if (i == 1)
                                        this.lblNzzPoint2.Text = string.IsNullOrEmpty(nzzMdAccess.lstPoint[1]) ? "" : "2、" + nzzMdAccess.lstPoint[1];
                                    if (i == 2)
                                        this.lblNzzPoint3.Text = string.IsNullOrEmpty(nzzMdAccess.lstPoint[2]) ? "" : "3、" + nzzMdAccess.lstPoint[2];
                                    if (i == 3)
                                        this.lblNzzPoint4.Text = string.IsNullOrEmpty(nzzMdAccess.lstPoint[3]) ? "" : "4、" + nzzMdAccess.lstPoint[3];
                                    if (i == 4)
                                        this.lblNzzPoint5.Text = string.IsNullOrEmpty(nzzMdAccess.lstPoint[4]) ? "" : "5、" + nzzMdAccess.lstPoint[4];
                                    if (i == 5)
                                        this.lblNzzPoint6.Text = string.IsNullOrEmpty(nzzMdAccess.lstPoint[5]) ? "" : "6、" + nzzMdAccess.lstPoint[5];
                                    if (i == 6)
                                        this.lblNzzPoint7.Text = string.IsNullOrEmpty(nzzMdAccess.lstPoint[6]) ? "" : "7、" + nzzMdAccess.lstPoint[6];
                                    if (i == 7)
                                        this.lblNzzPoint8.Text = string.IsNullOrEmpty(nzzMdAccess.lstPoint[7]) ? "" : "8、" + nzzMdAccess.lstPoint[7];
                                }
                            }
                            totalDf += nzzMdAccess.df;
                            if (nzzMdAccess.resultStr == "很高危")
                                moreHighFx += "脑卒中、";
                            if (nzzMdAccess.resultStr == "高危")
                                highFx += "脑卒中、";
                            if (nzzMdAccess.resultStr == "低危")
                                lowFx += "脑卒中、";
                            if (nzzMdAccess.resultStr == "很低危")
                                moreLowFx += "脑卒中、";
                        }
                    }

                }
                #endregion

                #region 阿茨海默症
                this.dtRptAzhmz.Visible = false;
                EntityRptModelAcess azhmzMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 8);
                if (azhmzMdAccess != null)
                {
                    if (azhmzMdAccess.lstModelParam != null)
                    {
                        if (azhmzMdAccess.lstModelParam.Count > 0)
                        {
                            this.dtRptAzhmz.Visible = true;
                            this.picHintAzhmz.DataBindings.Add("Image", nzzMdAccess.lstModelParam, "pic");
                            this.cellAzhmzItem.DataBindings.Add("Text", azhmzMdAccess.lstModelParam, "itemName");//
                            this.cellAzhmzResult.DataBindings.Add("Text", azhmzMdAccess.lstModelParam, "result");//
                            this.cellAzhmzRange.DataBindings.Add("Text", azhmzMdAccess.lstModelParam, "range");//
                            this.cellAzhmzUnit.DataBindings.Add("Text", azhmzMdAccess.lstModelParam, "unit");//
                            this.dtRptAzhmzGroup.DataSource = azhmzMdAccess.lstModelParam;
                            this.lblAzhmzDf.Text = azhmzMdAccess.df.ToString("0.00");
                            this.lblAzhmzAbasableDf.Text = azhmzMdAccess.reduceDf.ToString("0.00");
                            this.lblAzhmzBestDf.Text = azhmzMdAccess.bestDf.ToString("0.00");

                            this.picAzhmzFx01.Image = azhmzMdAccess.imgFx01;
                            this.picAzhmzFx02.Image = azhmzMdAccess.imgFx02;
                            this.picAzhmzFx03.Image = azhmzMdAccess.imgFx03;
                            this.picAzhmzFx04.Image = azhmzMdAccess.imgFx04;

                            if (azhmzMdAccess.df > 5 && azhmzMdAccess.df < 18)
                                this.lblAzhmzTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblAzhmzDf.Text + "%，有一定风险，但仍然低于同年龄、同性别人群的平均水平。";
                            if (azhmzMdAccess.df <= 5)
                                this.lblAzhmzTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblAzhmzDf.Text + "%，远低于同年龄、同性别人群的平均水平。";
                            if (azhmzMdAccess.df >= 18 && azhmzMdAccess.df < 50)
                                this.lblAzhmzTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblAzhmzDf.Text + "%，高于同年龄、同性别人群的平均水平。";
                            if (azhmzMdAccess.df >= 50)
                                this.lblAzhmzTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblAzhmzDf.Text + "%，远高于同年龄、同性别人群的平均水平。";

                            this.xrChartAzhmz.DataSource = azhmzMdAccess.lstEvaluate;
                            this.xrChartAzhmz.Series[0].SetDataMembers("evaluationName", "result");

                            if (azhmzMdAccess.lstPoint != null)
                            {
                                for (int i = 0; i < azhmzMdAccess.lstPoint.Count; i++)
                                {
                                    if (i == 0)
                                        this.lblAzhmzPoint1.Text = string.IsNullOrEmpty(azhmzMdAccess.lstPoint[0]) ? "" : "1、" + azhmzMdAccess.lstPoint[0];
                                    if (i == 1)
                                        this.lblAzhmzPoint2.Text = string.IsNullOrEmpty(azhmzMdAccess.lstPoint[1]) ? "" : "2、" + azhmzMdAccess.lstPoint[1];
                                    if (i == 2)
                                        this.lblAzhmzPoint3.Text = string.IsNullOrEmpty(azhmzMdAccess.lstPoint[2]) ? "" : "3、" + azhmzMdAccess.lstPoint[2];
                                    if (i == 3)
                                        this.lblAzhmzPoint4.Text = string.IsNullOrEmpty(azhmzMdAccess.lstPoint[3]) ? "" : "4、" + azhmzMdAccess.lstPoint[3];
                                    if (i == 4)
                                        this.lblAzhmzPoint5.Text = string.IsNullOrEmpty(azhmzMdAccess.lstPoint[4]) ? "" : "5、" + azhmzMdAccess.lstPoint[4];
                                    if (i == 5)
                                        this.lblAzhmzPoint6.Text = string.IsNullOrEmpty(azhmzMdAccess.lstPoint[5]) ? "" : "6、" + azhmzMdAccess.lstPoint[5];
                                    if (i == 6)
                                        this.lblAzhmzPoint7.Text = string.IsNullOrEmpty(azhmzMdAccess.lstPoint[6]) ? "" : "7、" + azhmzMdAccess.lstPoint[6];
                                    if (i == 7)
                                        this.lblAzhmzPoint8.Text = string.IsNullOrEmpty(azhmzMdAccess.lstPoint[7]) ? "" : "8、" + azhmzMdAccess.lstPoint[7];
                                }
                            }
                            totalDf += azhmzMdAccess.df;
                            if (azhmzMdAccess.resultStr == "很高危")
                                moreHighFx += "脑卒中、";
                            if (azhmzMdAccess.resultStr == "高危")
                                highFx += "脑卒中、";
                            if (azhmzMdAccess.resultStr == "低危")
                                lowFx += "脑卒中、";
                            if (azhmzMdAccess.resultStr == "很低危")
                                moreLowFx += "脑卒中、";
                        }
                    }

                }
                #endregion

                #region 肺癌
                this.dtRptFa.Visible = false;
                EntityRptModelAcess faMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 51);
                if (faMdAccess != null)
                {
                    if (faMdAccess.lstModelParam != null)
                    {
                        if (faMdAccess.lstModelParam.Count > 0)
                        {
                            this.dtRptFa.Visible = true;
                            this.picHintFa.DataBindings.Add("Image", faMdAccess.lstModelParam, "pic");
                            this.cellFaItem.DataBindings.Add("Text", faMdAccess.lstModelParam, "itemName");//
                            this.cellFaResult.DataBindings.Add("Text", faMdAccess.lstModelParam, "result");//
                            this.cellFaRange.DataBindings.Add("Text", faMdAccess.lstModelParam, "range");//
                            this.cellFaUnit.DataBindings.Add("Text", faMdAccess.lstModelParam, "unit");//
                            this.dtRptFaGroup.DataSource = faMdAccess.lstModelParam;
                            this.lblFaDf.Text = faMdAccess.df.ToString("0.00");
                            this.lblFaAbasableDf.Text = faMdAccess.reduceDf.ToString("0.00");
                            this.lblFaBestDf.Text = faMdAccess.bestDf.ToString("0.00");

                            this.picFaFx01.Image = faMdAccess.imgFx01;
                            this.picFaFx02.Image = faMdAccess.imgFx02;
                            this.picFaFx03.Image = faMdAccess.imgFx03;
                            this.picFaFx04.Image = faMdAccess.imgFx04;

                            if (faMdAccess.df > 5 && faMdAccess.df < 18)
                                this.lblFaTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblFaDf.Text + "%，有一定风险，但仍然低于同年龄、同性别人群的平均水平。";
                            if (faMdAccess.df <= 5)
                                this.lblFaTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblFaDf.Text + "%，远低于同年龄、同性别人群的平均水平。";
                            if (faMdAccess.df >= 18 && faMdAccess.df < 50)
                                this.lblFaTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblFaDf.Text + "%，高于同年龄、同性别人群的平均水平。";
                            if (faMdAccess.df >= 50)
                                this.lblFaTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblFaDf.Text + "%，远高于同年龄、同性别人群的平均水平。";

                            this.xrChartFa.DataSource = faMdAccess.lstEvaluate;
                            this.xrChartFa.Series[0].SetDataMembers("evaluationName", "result");

                            if (faMdAccess.lstPoint != null)
                            {
                                for (int i = 0; i < faMdAccess.lstPoint.Count; i++)
                                {
                                    if (i == 0)
                                        this.lblFaPoint1.Text = string.IsNullOrEmpty(faMdAccess.lstPoint[0]) ? "" : "1、" + faMdAccess.lstPoint[0];
                                    if (i == 1)
                                        this.lblFaPoint2.Text = string.IsNullOrEmpty(faMdAccess.lstPoint[1]) ? "" : "2、" + faMdAccess.lstPoint[1];
                                    if (i == 2)
                                        this.lblFaPoint3.Text = string.IsNullOrEmpty(faMdAccess.lstPoint[2]) ? "" : "3、" + faMdAccess.lstPoint[2];
                                    if (i == 3)
                                        this.lblFaPoint4.Text = string.IsNullOrEmpty(faMdAccess.lstPoint[3]) ? "" : "4、" + faMdAccess.lstPoint[3];
                                    if (i == 4)
                                        this.lblFaPoint5.Text = string.IsNullOrEmpty(faMdAccess.lstPoint[4]) ? "" : "5、" + faMdAccess.lstPoint[4];
                                    if (i == 5)
                                        this.lblFaPoint6.Text = string.IsNullOrEmpty(faMdAccess.lstPoint[5]) ? "" : "6、" + faMdAccess.lstPoint[5];
                                    if (i == 6)
                                        this.lblFaPoint7.Text = string.IsNullOrEmpty(faMdAccess.lstPoint[6]) ? "" : "7、" + faMdAccess.lstPoint[6];
                                    if (i == 7)
                                        this.lblFaPoint8.Text = string.IsNullOrEmpty(faMdAccess.lstPoint[7]) ? "" : "8、" + faMdAccess.lstPoint[7];
                                }
                            }
                            totalDf += faMdAccess.df;
                            if (faMdAccess.resultStr == "很高危")
                                moreHighFx += "脑卒中、";
                            if (faMdAccess.resultStr == "高危")
                                highFx += "脑卒中、";
                            if (faMdAccess.resultStr == "低危")
                                lowFx += "脑卒中、";
                            if (faMdAccess.resultStr == "很低危")
                                moreLowFx += "脑卒中、";
                        }
                    }

                }
                #endregion

                #region 肝癌
                this.dtRptGa.Visible = false;
                EntityRptModelAcess gaMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 52);
                if (faMdAccess != null)
                {
                    if (gaMdAccess.lstModelParam != null)
                    {
                        if (gaMdAccess.lstModelParam.Count > 0)
                        {
                            this.dtRptGa.Visible = true;
                            this.picHintGa.DataBindings.Add("Image", gaMdAccess.lstModelParam, "pic");
                            this.cellGaItem.DataBindings.Add("Text", gaMdAccess.lstModelParam, "itemName");//
                            this.cellGaResult.DataBindings.Add("Text", gaMdAccess.lstModelParam, "result");//
                            this.cellGaRange.DataBindings.Add("Text", gaMdAccess.lstModelParam, "range");//
                            this.cellGaUnit.DataBindings.Add("Text", gaMdAccess.lstModelParam, "unit");//
                            this.dtRptGaGroup.DataSource = gaMdAccess.lstModelParam;
                            this.lblGaDf.Text = gaMdAccess.df.ToString("0.00");
                            this.lblGaAbasableDf.Text = gaMdAccess.reduceDf.ToString("0.00");
                            this.lblGaBestDf.Text = gaMdAccess.bestDf.ToString("0.00");

                            this.picGaFx01.Image = gaMdAccess.imgFx01;
                            this.picGaFx02.Image = gaMdAccess.imgFx02;
                            this.picGaFx03.Image = gaMdAccess.imgFx03;
                            this.picGaFx04.Image = gaMdAccess.imgFx04;

                            if (gaMdAccess.df > 5 && gaMdAccess.df < 18)
                                this.lblGaTip.Text = "【说明】您在未来5~10年肝癌的患病风险为" + this.lblGaDf.Text + "%，有一定风险，但仍然低于同年龄、同性别人群的平均水平。";
                            if (gaMdAccess.df <= 5)
                                this.lblGaTip.Text = "【说明】您在未来5~10年10年肝癌的患病风险为" + this.lblGaDf.Text + "%，远低于同年龄、同性别人群的平均水平。";
                            if (gaMdAccess.df >= 18 && gaMdAccess.df < 50)
                                this.lblGaTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblGaDf.Text + "%，高于同年龄、同性别人群的平均水平。";
                            if (gaMdAccess.df >= 50)
                                this.lblGaTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblGaDf.Text + "%，远高于同年龄、同性别人群的平均水平。";

                            this.xrChartGa.DataSource = gaMdAccess.lstEvaluate;
                            this.xrChartGa.Series[0].SetDataMembers("evaluationName", "result");

                            if (faMdAccess.lstPoint != null)
                            {
                                for (int i = 0; i < gaMdAccess.lstPoint.Count; i++)
                                {
                                    if (i == 0)
                                        this.lblGaPoint1.Text = string.IsNullOrEmpty(gaMdAccess.lstPoint[0]) ? "" : "1、" + gaMdAccess.lstPoint[0];
                                    if (i == 1)
                                        this.lblGaPoint2.Text = string.IsNullOrEmpty(gaMdAccess.lstPoint[1]) ? "" : "2、" + gaMdAccess.lstPoint[1];
                                    if (i == 2)
                                        this.lblGaPoint3.Text = string.IsNullOrEmpty(gaMdAccess.lstPoint[2]) ? "" : "3、" + gaMdAccess.lstPoint[2];
                                    if (i == 3)
                                        this.lblGaPoint4.Text = string.IsNullOrEmpty(gaMdAccess.lstPoint[3]) ? "" : "4、" + gaMdAccess.lstPoint[3];
                                    if (i == 4)
                                        this.lblGaPoint5.Text = string.IsNullOrEmpty(gaMdAccess.lstPoint[4]) ? "" : "5、" + gaMdAccess.lstPoint[4];
                                    if (i == 5)
                                        this.lblGaPoint6.Text = string.IsNullOrEmpty(gaMdAccess.lstPoint[5]) ? "" : "6、" + gaMdAccess.lstPoint[5];
                                    if (i == 6)
                                        this.lblGaPoint7.Text = string.IsNullOrEmpty(gaMdAccess.lstPoint[6]) ? "" : "7、" + gaMdAccess.lstPoint[6];
                                    if (i == 7)
                                        this.lblGaPoint8.Text = string.IsNullOrEmpty(gaMdAccess.lstPoint[7]) ? "" : "8、" + gaMdAccess.lstPoint[7];
                                }
                            }
                            totalDf += gaMdAccess.df;
                            if (gaMdAccess.resultStr == "很高危")
                                moreHighFx += "脑卒中、";
                            if (gaMdAccess.resultStr == "高危")
                                highFx += "脑卒中、";
                            if (gaMdAccess.resultStr == "低危")
                                lowFx += "脑卒中、";
                            if (gaMdAccess.resultStr == "很低危")
                                moreLowFx += "脑卒中、";
                        }
                    }

                }
                #endregion

                #region 胃癌
                this.dtRptWa.Visible = false;
                EntityRptModelAcess waMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 52);
                if (waMdAccess != null)
                {
                    if (waMdAccess.lstModelParam != null)
                    {
                        if (waMdAccess.lstModelParam.Count > 0)
                        {
                            this.dtRptWa.Visible = true;
                            this.picHintWa.DataBindings.Add("Image", waMdAccess.lstModelParam, "pic");
                            this.cellWaItem.DataBindings.Add("Text", waMdAccess.lstModelParam, "itemName");//
                            this.cellWaResult.DataBindings.Add("Text",waMdAccess.lstModelParam, "result");//
                            this.cellWaRange.DataBindings.Add("Text", waMdAccess.lstModelParam, "range");//
                            this.cellWaUnit.DataBindings.Add("Text", waMdAccess.lstModelParam, "unit");//
                            this.dtRptWaGroup.DataSource = waMdAccess.lstModelParam;
                            this.lblWaDf.Text = waMdAccess.df.ToString("0.00");
                            this.lblWaAbasableDf.Text = waMdAccess.reduceDf.ToString("0.00");
                            this.lblWaBestDf.Text = waMdAccess.bestDf.ToString("0.00");

                            this.picWaFx01.Image = waMdAccess.imgFx01;
                            this.picWaFx02.Image = waMdAccess.imgFx02;
                            this.picWaFx03.Image = waMdAccess.imgFx03;
                            this.picWaFx04.Image = waMdAccess.imgFx04;

                            if (waMdAccess.df > 5 && waMdAccess.df < 18)
                                this.lblWaTip.Text = "【说明】您在未来5~10年肝癌的患病风险为" + this.lblWaDf.Text + "%，有一定风险，但仍然低于同年龄、同性别人群的平均水平。";
                            if (waMdAccess.df <= 5)
                                this.lblWaTip.Text = "【说明】您在未来5~10年10年肝癌的患病风险为" + this.lblWaDf.Text + "%，远低于同年龄、同性别人群的平均水平。";
                            if (waMdAccess.df >= 18 && waMdAccess.df < 50)
                                this.lblWaTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblWaDf.Text + "%，高于同年龄、同性别人群的平均水平。";
                            if (waMdAccess.df >= 50)
                                this.lblWaTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblWaDf.Text + "%，远高于同年龄、同性别人群的平均水平。";

                            this.xrChartWa.DataSource = waMdAccess.lstEvaluate;
                            this.xrChartWa.Series[0].SetDataMembers("evaluationName", "result");

                            if (waMdAccess.lstPoint != null)
                            {
                                for (int i = 0; i < waMdAccess.lstPoint.Count; i++)
                                {
                                    if (i == 0)
                                        this.lblWaPoint1.Text = string.IsNullOrEmpty(waMdAccess.lstPoint[0]) ? "" : "1、" + gaMdAccess.lstPoint[0];
                                    if (i == 1)
                                        this.lblWaPoint2.Text = string.IsNullOrEmpty(waMdAccess.lstPoint[1]) ? "" : "2、" + gaMdAccess.lstPoint[1];
                                    if (i == 2)
                                        this.lblWaPoint3.Text = string.IsNullOrEmpty(waMdAccess.lstPoint[2]) ? "" : "3、" + gaMdAccess.lstPoint[2];
                                    if (i == 3)
                                        this.lblWaPoint4.Text = string.IsNullOrEmpty(waMdAccess.lstPoint[3]) ? "" : "4、" + gaMdAccess.lstPoint[3];
                                    if (i == 4)
                                        this.lblWaPoint5.Text = string.IsNullOrEmpty(waMdAccess.lstPoint[4]) ? "" : "5、" + gaMdAccess.lstPoint[4];
                                    if (i == 5)
                                        this.lblWaPoint6.Text = string.IsNullOrEmpty(waMdAccess.lstPoint[5]) ? "" : "6、" + gaMdAccess.lstPoint[5];
                                    if (i == 6)
                                        this.lblWaPoint7.Text = string.IsNullOrEmpty(waMdAccess.lstPoint[6]) ? "" : "7、" + gaMdAccess.lstPoint[6];
                                    if (i == 7)
                                        this.lblWaPoint8.Text = string.IsNullOrEmpty(waMdAccess.lstPoint[7]) ? "" : "8、" + gaMdAccess.lstPoint[7];
                                }
                            }
                            totalDf += waMdAccess.df;
                            if (waMdAccess.resultStr == "很高危")
                                moreHighFx += "脑卒中、";
                            if (waMdAccess.resultStr == "高危")
                                highFx += "脑卒中、";
                            if (waMdAccess.resultStr == "低危")
                                lowFx += "脑卒中、";
                            if (waMdAccess.resultStr == "很低危")
                                moreLowFx += "脑卒中、";
                        }
                    }

                }
                #endregion

                #region 前列腺癌
                this.dtRptQlxa.Visible = false;
                EntityRptModelAcess qlxaMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 52);
                if (qlxaMdAccess != null)
                {
                    if (qlxaMdAccess.lstModelParam != null)
                    {
                        if (qlxaMdAccess.lstModelParam.Count > 0)
                        {
                            this.dtRptQlxa.Visible = true;
                            this.picHintWa.DataBindings.Add("Image", qlxaMdAccess.lstModelParam, "pic");
                            this.cellQlxaItem.DataBindings.Add("Text", qlxaMdAccess.lstModelParam, "itemName");//
                            this.cellQlxaResult.DataBindings.Add("Text", qlxaMdAccess.lstModelParam, "result");//
                            this.cellQlxaRange.DataBindings.Add("Text", qlxaMdAccess.lstModelParam, "range");//
                            this.cellQlxaUnit.DataBindings.Add("Text", qlxaMdAccess.lstModelParam, "unit");//
                            this.dtRptQlxaGroup.DataSource = qlxaMdAccess.lstModelParam;
                            this.lblQlxaDf.Text = qlxaMdAccess.df.ToString("0.00");
                            this.lblQlxaAbasableDf.Text = qlxaMdAccess.reduceDf.ToString("0.00");
                            this.lblQlxaBestDf.Text = qlxaMdAccess.bestDf.ToString("0.00");

                            this.picQlxaFx01.Image = qlxaMdAccess.imgFx01;
                            this.picQlxaFx02.Image = qlxaMdAccess.imgFx02;
                            this.picQlxaFx03.Image = qlxaMdAccess.imgFx03;
                            this.picQlxaFx04.Image = qlxaMdAccess.imgFx04;

                            if (qlxaMdAccess.df > 5 && qlxaMdAccess.df < 18)
                                this.lblQlxaTip.Text = "【说明】您在未来5~10年肝癌的患病风险为" + this.lblQlxaDf.Text + "%，有一定风险，但仍然低于同年龄、同性别人群的平均水平。";
                            if (qlxaMdAccess.df <= 5)
                                this.lblQlxaTip.Text = "【说明】您在未来5~10年10年肝癌的患病风险为" + this.lblQlxaDf.Text + "%，远低于同年龄、同性别人群的平均水平。";
                            if (qlxaMdAccess.df >= 18 && qlxaMdAccess.df < 50)
                                this.lblQlxaTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblQlxaDf.Text + "%，高于同年龄、同性别人群的平均水平。";
                            if (qlxaMdAccess.df >= 50)
                                this.lblQlxaTip.Text = "【说明】您在未来5~10年脑卒中的患病风险为" + this.lblQlxaDf.Text + "%，远高于同年龄、同性别人群的平均水平。";

                            this.xrChartQlxa.DataSource = qlxaMdAccess.lstEvaluate;
                            this.xrChartQlxa.Series[0].SetDataMembers("evaluationName", "result");

                            if (qlxaMdAccess.lstPoint != null)
                            {
                                for (int i = 0; i < qlxaMdAccess.lstPoint.Count; i++)
                                {
                                    if (i == 0)
                                        this.lblQlxaPoint1.Text = string.IsNullOrEmpty(qlxaMdAccess.lstPoint[0]) ? "" : "1、" + qlxaMdAccess.lstPoint[0];
                                    if (i == 1)
                                        this.lblQlxaPoint2.Text = string.IsNullOrEmpty(qlxaMdAccess.lstPoint[1]) ? "" : "2、" + qlxaMdAccess.lstPoint[1];
                                    if (i == 2)
                                        this.lblQlxaPoint3.Text = string.IsNullOrEmpty(qlxaMdAccess.lstPoint[2]) ? "" : "3、" + qlxaMdAccess.lstPoint[2];
                                    if (i == 3)
                                        this.lblQlxaPoint4.Text = string.IsNullOrEmpty(qlxaMdAccess.lstPoint[3]) ? "" : "4、" + qlxaMdAccess.lstPoint[3];
                                    if (i == 4)
                                        this.lblQlxaPoint5.Text = string.IsNullOrEmpty(qlxaMdAccess.lstPoint[4]) ? "" : "5、" + qlxaMdAccess.lstPoint[4];
                                    if (i == 5)
                                        this.lblQlxaPoint6.Text = string.IsNullOrEmpty(qlxaMdAccess.lstPoint[5]) ? "" : "6、" + qlxaMdAccess.lstPoint[5];
                                    if (i == 6)
                                        this.lblQlxaPoint7.Text = string.IsNullOrEmpty(qlxaMdAccess.lstPoint[6]) ? "" : "7、" + qlxaMdAccess.lstPoint[6];
                                    if (i == 7)
                                        this.lblQlxaPoint8.Text = string.IsNullOrEmpty(qlxaMdAccess.lstPoint[7]) ? "" : "8、" + qlxaMdAccess.lstPoint[7];
                                }
                            }
                            totalDf += qlxaMdAccess.df;
                            if (qlxaMdAccess.resultStr == "很高危")
                                moreHighFx += "脑卒中、";
                            if (qlxaMdAccess.resultStr == "高危")
                                highFx += "脑卒中、";
                            if (qlxaMdAccess.resultStr == "低危")
                                lowFx += "脑卒中、";
                            if (qlxaMdAccess.resultStr == "很低危")
                                moreLowFx += "脑卒中、";
                        }
                    }

                }
                #endregion

                #region 汇总得分
                lblTotalDf.Text = totalDf.ToString("0.00");
                if (!string.IsNullOrEmpty(moreHighFx))
                {
                    moreHighFx = moreHighFx.TrimEnd('、');
                    lblMoreHigh.Text = moreHighFx;
                }
                else
                {
                    lblMoreHigh.Text = "暂无";
                }
                if (!string.IsNullOrEmpty(highFx))
                {
                    highFx = highFx.TrimEnd('、');
                    lblHigh.Text = highFx;
                }
                else
                {
                    lblHigh.Text = "暂无";
                }
                if (!string.IsNullOrEmpty(lowFx))
                {
                    lowFx = lowFx.TrimEnd('、');
                    lblLow.Text = lowFx;
                }
                else
                {
                    lblLow.Text = "暂无";
                }
                if (!string.IsNullOrEmpty(moreLowFx))
                {
                    moreLowFx = moreLowFx.TrimEnd('、');
                    lblMoreLow.Text = moreLowFx;
                }
                else
                {
                    lblMoreLow.Text = "暂无";
                }
                #endregion

                #region 就医检查建议
                //this.cellPeBseItem1.DataBindings.Add("Text", report.lstAdPeItemBse, "item1");//
                //this.cellPeBseItem2.DataBindings.Add("Text", report.lstAdPeItemBse, "item2");//
                //this.cellPeBseItem3.DataBindings.Add("Text", report.lstAdPeItemBse, "item3");//
                //this.dtRptPeBseGroup.DataSource = report.lstAdPeItemBse;

                //this.cellPeSpeialItem1.DataBindings.Add("Text", report.lstAdPeItemSpecial, "item1");//
                //this.cellPeSpeialItem2.DataBindings.Add("Text", report.lstAdPeItemSpecial, "item2");//
                //this.cellPeSpeialItem3.DataBindings.Add("Text", report.lstAdPeItemSpecial, "item3");//
                //this.dtRptPeSpecialGroup.DataSource = report.lstAdPeItemSpecial;

                //this.cellAdImportant.DataBindings.Add("Text", report.lstMedAdvices, "important");//
                //this.cellAdUnormal.DataBindings.Add("Text", report.lstMedAdvices, "unnormal");//
                //this.cellAdMedDate.DataBindings.Add("Text", report.lstMedAdvices, "medDate");//
                //this.cellAdRefferDept.DataBindings.Add("Text", report.lstMedAdvices, "refferDept");//
                //this.dtRptAdviceGroup.DataSource = report.lstMedAdvices;
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
