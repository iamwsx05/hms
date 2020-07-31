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
                EntityRptModelAcess gxyMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 1);
                if(gxyMdAccess != null)
                {
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
                else
                {
                    this.dtRptGxy.Visible = false;
                }


                #endregion

                #region 糖尿病
                EntityRptModelAcess tnbMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 2);
                if (tnbMdAccess != null)
                {
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
                else
                {
                    this.dtRptTnb.Visible = false;
                }

                #endregion

                #region 冠心病
                EntityRptModelAcess gxbMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 3);
                if (tnbMdAccess != null)
                {
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
                else
                {
                    this.dtRptGxb.Visible = false;
                }

                #endregion

                #region 血脂异常
                EntityRptModelAcess xzycMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 3);
                if (xzycMdAccess != null)
                {
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
                else
                {
                    this.dtRptXzyc.Visible = false;
                }


                #endregion

                #region 肥胖症
                EntityRptModelAcess fpzMdAccess = report.lstRptModelAcess.Find(r => r.modelId == 1);
                if (gxyMdAccess != null)
                {
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
                else
                {
                    this.dtRptFpz.Visible = false;
                }

                #endregion

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
