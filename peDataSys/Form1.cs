using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Hms.Entity;
using Hms.Ui;
using Oracle.DataAccess.Client;
using Hms.Biz;

namespace peDataSys
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }


       

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sysDicPeItem();
        }

        List<EntityClientInfo> lstClientSelect { get; set; }

        #region 同步体检项目
        /// <summary>
        /// 同步体检项目
        /// </summary>
        public void sysDicPeItem()
        {
            List<EntityDicPeItem> data = null;
            //SqlHelper svcPe = new SqlHelper(EnumBiz.peDB);
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            List<DacParm> lstParm = new List<DacParm>();
            string Sql = @"select b.item_code,
                              b.item_name,
                              a.cls_code,
                              b.ref_lower,
                              b.ref_upper,
                              ref_result ,
                              b.sex ,
                              0 as displayposition,
                              b.unit
                              from zdXmfl a 
                              left join zdXm b
                              on a.cls_code = b.cls_code where item_code is not null and item_code not in(select itemid from dicPeItem)";

            try
            {
                DataTable dt = svc.GetDataTable(Sql);
                //Sql = @"delete from dicpeitem";
                //svc.ExecSql(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int n = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDicPeItem vo = new EntityDicPeItem();
                        vo.itemId = dr["item_code"].ToString();
                        vo.itemName = dr["item_name"].ToString();
                        vo.deptId = dr["cls_code"].ToString();

                        if (dr["ref_lower"] != DBNull.Value)
                        {
                            if (Function.IsNumber(dr["ref_lower"].ToString()))
                                vo.minValue = Function.Dec(dr["ref_lower"]);
                        }

                        if (dr["ref_upper"] != DBNull.Value)
                        {
                            if (Function.IsNumber(dr["ref_upper"].ToString()))
                                vo.maxValue = Function.Dec(dr["ref_upper"]);
                        }

                        vo.refRange = dr["ref_result"].ToString();
                        if (dr["sex"].ToString().Contains("%") || dr["sex"] == DBNull.Value)
                            vo.gender = 3;
                        else if (dr["sex"].ToString().Trim() == "1")
                            vo.gender = 1;
                        else if (dr["sex"].ToString().Trim() == "2")
                            vo.gender = 2;
                        vo.unit = dr["unit"].ToString();
                        ++n;
                        vo.sortNo = n;
                        //data.Add(vo);


                        lstParm.Add(svc.GetInsertParm(vo));
                    }

                    if (svc.Commit(lstParm) > 0)
                    {
                        MessageBox.Show("success!");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
        }
        #endregion


        #region tab
        public void sysReportDetail()
        {
            SqlHelper svcPe = new SqlHelper(EnumBiz.peDB);
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            List<DacParm> lstParm = new List<DacParm>();
            string Sql = @"select a.clus_code,a.clus_name,e.item_code,e.item_name from zdTc a 
                        left join zdTcmx b 
                        on a.clus_code  = b.clus_code
                        left join zdZhxm c 
                        on b.comb_code = c.comb_code
                        left join zdXmfl d
                        on c.cls_code = d.cls_code
                        left join zdXm e 
                        on d.cls_code = e.cls_code where e.item_code is not null";
            try
            {
                DataTable dt = svcPe.GetDataTable(Sql);
                Sql = @"delete from dicRptTemplateConfig";
                svc.ExecSql(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int n = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityRpttemplateConfig vo = new EntityRpttemplateConfig();

                        //data.Add(vo);
                        vo.templateId = dr["clus_code"].ToString();
                        vo.itemCode = dr["item_code"].ToString();

                        //lstParm.Add(svc.GetInsertParm(vo));
                         Sql = @"insert into dicRptTemplateConfig (templateId,itemCode) values(?,?)";
                        IDataParameter [] parm = svc.CreateParm(2);
                        parm[0].Value = "1";
                        parm[1].Value = "22";
                        svc.ExecSql(Sql);
                    }


                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
                MessageBox.Show("complete!");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            sysReportDetail();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            SqlHelper svcOra = new SqlHelper(EnumBiz.interfaceDB);
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            try
            { 
                List<DacParm> lstParm = new List<DacParm>();
                int affectRows = -1;
                string Sql = @"select * from diet_principle";

                DataTable dt = svcOra.GetDataTable(Sql);
                Sql = @"delete from dietPrinciple";
                svc.ExecSql(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int n = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDietPrinciple vo = new EntityDietPrinciple();

                        vo.principleId = svc.GetNextID("dietPrinciple", "dietPrincipleId").ToString().PadLeft(6, '0');
                        vo.principleName = dr["names"].ToString();
                        vo.contents = dr["contents"].ToString();
                        vo.createDate = DateTime.Now;
                        vo.createUserId = "00";


                        lstParm.Add(svc.GetInsertParm(vo));
                    }
                    if(lstParm.Count > 0)
                        affectRows = svc.Commit(lstParm);

                    if(affectRows > 0)
                    {
                        MessageBox.Show("success!");
                    }


                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            SqlHelper svcOra = new SqlHelper(EnumBiz.interfaceDB);
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                int affectRows = -1;
                string Sql = @"select * from diet_template_type";

                DataTable dt = svcOra.GetDataTable(Sql);
                Sql = @"delete from dietTemplatetype";
                svc.ExecSql(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDietTemplatetype vo = new EntityDietTemplatetype();

                        vo.typeId = svc.GetNextID("dietTemplateType", "typeId").ToString().PadLeft(6, '0');
                        vo.typeName = dr["template_name"].ToString();
                        vo.createDate = DateTime.Now;
                        vo.creator = "00";
                        vo.createName = "系统管理员";

                        lstParm.Add(svc.GetInsertParm(vo));
                    }
                    if (lstParm.Count > 0)
                        affectRows = svc.Commit(lstParm);

                    if (affectRows > 0)
                    {
                        MessageBox.Show("success!");
                    }


                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            
            
            
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

            SqlHelper svcOra = new SqlHelper(EnumBiz.interfaceDB);
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                int affectRows = -1;
                string Sql = @"select * from cai";

                DataTable dt = svcOra.GetDataTable(Sql);
                Sql = @"delete from diccai";
                svc.ExecSql(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDicCai vo = new EntityDicCai();

                        vo.id = dr["id"].ToString();
                        vo.names = dr["names"].ToString();
                        vo.breakfast = dr["breakfast"].ToString();
                        vo.lunch = dr["lunch"].ToString();
                        vo.dinner = dr["dinner"].ToString();
                        vo.other = dr["other"].ToString();
                        vo.methods = dr["methods"].ToString();
                        vo.kCal = Function.Dec( dr["kCal"].ToString());
                        vo.KJ = Function.Dec(dr["KJ"].ToString());
                        vo.PROTEIN = Function.Dec(dr["PROTEIN"].ToString());
                        vo.FAT = Function.Dec(dr["FAT"].ToString());
                        vo.CHO = Function.Dec(dr["CHO"].ToString());
                        vo.BRXXW = Function.Dec(dr["BRXXW"].ToString());
                        vo.DGC = Function.Dec(dr["DGC"].ToString());
                        vo.ASH = Function.Dec(dr["ASH"].ToString());
                        vo.vitaminA = Function.Dec(dr["vitaminA"].ToString());
                        vo.THIAMIN = Function.Dec(dr["THIAMIN"].ToString());
                        vo.RIBOFLAVIN = Function.Dec(dr["RIBOFLAVIN"].ToString());
                        vo.NIACIN = Function.Dec(dr["NIACIN"].ToString());
                        vo.vitaminC = Function.Dec(dr["vitaminC"].ToString());
                        vo.vitaminE = Function.Dec(dr["vitaminE"].ToString());
                        vo.CA = Function.Dec(dr["CA"].ToString());
                        vo.P = Function.Dec(dr["P"].ToString());
                        vo.K = Function.Dec(dr["K"].ToString());
                        vo.NA = Function.Dec(dr["NA"].ToString());
                        vo.MG = Function.Dec(dr["MG"].ToString());
                        vo.FE = Function.Dec(dr["FE"].ToString());
                        vo.ZN = Function.Dec(dr["ZN"].ToString());
                        vo.SE = Function.Dec(dr["SE"].ToString());
                        vo.CU = Function.Dec(dr["CU"].ToString());
                        vo.MN = Function.Dec(dr["MN"].ToString());
                        vo.I = Function.Dec(dr["I"].ToString());
                        vo.F = Function.Dec(dr["F"].ToString());
                        vo.CR = Function.Dec(dr["CR"].ToString());
                        vo.MU = Function.Dec(dr["MU"].ToString());
                        vo.vitaminD = Function.Dec(dr["vitaminD"].ToString());
                        vo.vitaminB6 = Function.Dec(dr["vitaminB6"].ToString());
                        vo.vitaminB12 = Function.Dec(dr["vitaminB12"].ToString());
                        vo.vitaminB5 = Function.Dec(dr["vitaminB5"].ToString());
                        vo.vitaminB9 = Function.Dec(dr["vitaminB9"].ToString());
                        vo.DANJIAN = Function.Dec(dr["DANJIAN"].ToString());
                        vo.vitaminH = Function.Dec(dr["vitaminH"].ToString());
                        vo.bakfield1 = dr["bakfield1"].ToString();
                        vo.bakfield2 = dr["bakfield2"].ToString();
                        vo.createDate = Function.Datetime(dr["createDate"].ToString());
                        vo.creator = "00";
                        vo.creatorName = "系统管理员";
                        

                        lstParm.Add(svc.GetInsertParm(vo));
                    }
                    if (lstParm.Count > 0)
                        affectRows = svc.Commit(lstParm);

                    if (affectRows > 0)
                    {
                        MessageBox.Show("success!");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                int affectRows = -1;
                List<string> lstStr = new List<string>{"海带汤",
"烤茄串",
"酸辣乌鱼蛋汤",
"果汁肉干",
"麻茸鹌鹑蛋",
"豉油皇烙鸡肫",
"山楂冻",
"拌鱼丝",
"蚕豆松",
"奶油苹果",
"糯米蒸桃脯",
"川味回锅肉",
"梅汁泡河蚬",
"蛋蓉玉米羹",
"咸蛋黄拌豆腐",
"脆松糖",
"草莓果冻",
"芝麻红薯饼",
"辣拌芥蓝菜",
"辣油耳丝",
"藕香露",
"姜糖饮",
"大枣养血汤",
"红枣花生汁",
"韭菜生姜汁",
"咸蛋羹",
"葱白香菇人乳汤",
"白肉鱼泥",
"干荔枝煲粥",
"腌芥菜头",
"酥皮家常饼",
"金线吊葫芦",
"冰拌哈密瓜",
"糖拌葡萄",
"雪绵豆沙",
"五仁炸盒子",
"蛋黄茶碗蒸",
"苹果粥",
"甜豆浆",
"蔬菜云吞汤",
"黄金菇炖椰奶",
"红枣玉玉粥",
"蜇皮拌白菜",
"酱拌菠菜",
"菠菜拌猪肝",
"佛手海蜇",
"葱油豆腐",
"榨菜拌腐皮",
"番茄拌白菜心",
"糖拌西红柿",
"糖拌梨丝",
"青椒炒豆腐丁",
"红薯蒸板栗",
"夹沙豆腐",
"蜜汁红枣",
"葱白红枣汤",
"双雪汤",
"莲子淮山炖薏米",
"鲜莲汤",
"绿豆薏苡仁汤",
"卤味鲜海带",
"卤猪血糕",
"烫空心菜",
"芝麻南瓜饼",
"白灼鲜鱿鱼",
"豉椒牛柳",
"凉拌豆腐",
"葱油豌豆",
"菠萝水果冻",
"川味辣豆花",
"烩菠萝羹",
"核桃仁豌豆羹",
"鹌鹑蛋奶露",
"鸡蓉奶油汤",
"醋煮鸡蛋",
"红炖羊肉",
"鲍翅炖鸡",
"燕窝炖双鸭",
"马蹄炖羊肉",
"胡椒炖猪肚",
"清炖猪肚",
"椒酱肉",
"北菇炖鸽",
"白片羊肉",
"冬笋牛肉煲",
"牛肚煲(二)",
"大肠鸡红煲",
"虾鱼酿豆腐煲",
"五香大肉煲",
"火腿荔芋煲",
"南乳猪手煲",
"芋头猪手煲",
"发菜圆蹄煲",
"糖醋黄花鱼煲",
"腐竹鲩鱼煲",
"鲫鱼煲",
"姜葱鲤鱼煲",
"鱼球粉丝绍菜煲",
"白鳝豉汁煲",
"瑶柱蒜子煲",
"牛蹄筋煲",
"芥菜白肉片",
"子姜皮蛋",
"矿泉水腌脆条",
"油焖尖椒",
"冰糖三色瓜球",
"卤鸡三件",
"五柳炸鸡",
"竹笙炒鸡片",
"咸鸭蛋蒸豆腐",
"京糕山药卷",
"香面炒牛肉",
"抓炒鱼条",
"豆粉生鱼",
"怪味兔肉丝",
"白瓜咸蛋汤",
"珊瑚菊花",
"三鲜酿黄瓜",
"蜜汁糯米藕",
"胡萝卜香橙色拉",
"鲜虾蟹柳色拉",
"糖醋马蹄",
"拌蛏子",
"双色萝卜",
"鹑蛋紫菜汤",
"素菜泥汤",
"花生仁拌金笋干",
"清拌白菜心",
"海参桂圆粥",
"兰花泥肠",
"核桃牛奶饮",
"酸辣扁豆",
"回锅蛋",
"莲子桂圆汤",
"朝天耳丝",
"柠檬汁拌水果",
"豆芽面筋汤",
"清热鲜菇银芽汤",
"咖喱滚鱼片",
"槌鱼",
"酱味牛肚",
"真不同熏肉",
"黑椒烤猪肉",
"红果肉干",
"凤凰鸡煎牛里脊",
"新式牛肉干",
"牛干巴",
"片羊肉",
"五香卤鸭",
"炒羊肚片",
"冷甜菜汤",
"蜜枣青豆泥",
"熘排骨",
"酸菜肉丝",
"味菜牛柳",
"胡萝卜羊肝粥",
"酱油龙须菜",
"花生辣酱",
"熘变蛋",
"炸熘仔鸡",
"炸溜子鸡块",
"烩豆干丝",
"素炒黄花菜",
"大蒜炖生鱼",
"酸菜熬土豆",
"番薯麦米粥",
"粉葛煲鲮鱼",
"冰花合桃露",
"冰花炖雪耳",
"熟藕",
"肉茸榨菜粉皮",
"青龙卧雪",
"红净鸡",
"清汤卧果",
"黄甫蛋",
"冬笋炖鸡",
"咖喱火鸡",
"牛奶煮平菇",
"香酥腰花",
"银耳番茄羹",
"清拌茄条",
"双蛋彩拼",
"甜酱杏仁",
"广州腊羊蹄筋",
"胡萝卜拌鸡丝",
"清暑豆汤",
"冻粉鸡丝",
"黄瓜丝皮蛋",
"佛手蜇卷",
"油拌辣白菜",
"大酱拌芹菜",
"炝冬笋油菜",
"冰糖山药羹",
"清蒸山药段",
"酱羊腱子",
"黄瓜拌白肉",
"黄瓜拌皮丝",
"拌口条",
"黄瓜拌猪肝",
"黄瓜拌腰片",
"生菜拌腰片",
"凉拌白肚",
"黄瓜拌肚丝",
"麻酱拌肚丝",
"海参大虾拌鸡肉",
"韭菜烤鸭",
"豆腐拌咸鸭蛋",
"虾皮拌松花蛋",
"双腐拌双蛋",
"姜拌皮蛋",
"黄瓜拌虾片",
"青椒虾皮",
"海米拌雪菜",
"韭香海蜇",
"蛋皮海蜇",
"蜇米瓜菜",
"香菜拌海螺",
"糖豆",
"拌蚕豆沙",
"辣油豇豆",
"香菜芽拌豆腐",
"绿豆芽拌豆腐",
"花生米拌香干",
"核桃丁拌香干",
"白菜心拌干丝",
"酥仁干丁",
"酱豉香干",
"蜜汁土豆",
"酱拌土豆",
"酱拌白菜",
"芥末拌白菜",
"蒜辣白菜丁",
"香肠拌菠菜",
"糖醋芥菜丝",
"辣豆雪里蕻",
"糖拌萝卜丝",
"甜萝卜",
"花生拌酱萝卜",
"蒸酱茄子",
"姜末拌黄瓜",
"糖姜片",
"蒜酱笋片",
"双椒拌藕片",
"姜丝拌藕片",
"糖拌花生米",
"琥珀花生",
"脆甜花生米",
"拌西瓜瓤",
"冰淇淋双色丁",
"蜜桃果泥",
"冰糖蜜桃",
"糖拌杨梅",
"密瓜刨冰",
"雪菜炒黄豆芽",
"咸菜炒粉皮",
"核桃泥",
"麻酱拌白菜",
"大众蘸酱菜",
"拌瓜菜",
"凉拌冬瓜片",
"爽口西瓜盅",
"蘸茄子",
"香辣四季豆",
"榨菜拌腐竹",
"柠檬泡藕",
"八宝瓤西红柿",
"苹果煎蛋饼",
"三味酸菜",
"糖醋蒜",
"酸菜蒸豆腐",
"蜜汁蒸山药",
"龙眼蒸芋泥",
"红枣糯米饭",
"红枣花生汤",
"山楂麦芽汤",
"凤梨山楂汤",
"雪梨鲜奶炖木瓜",
"豆腐青葱汤",
"红苋豆腐汤",
"酸枣汤",
"莲子黑枣小麦汤",
"榨菜笋丝汤",
"蜜枣薏米煲",
"绿茶杏仁汤",
"卤水花生米",
"蒸臭豆腐",
"炝麻线鳝鱼",
"麻辣三丝",
"蒸猪肝",
"青蒸茶鲫鱼",
"白饭鱼蒸蛋",
"红枣煲栗子",
"雪梨莲藕汁",
"芝麻猪蹄糊",
"水果煎蛋",
"红烧五花肉",
"葡萄柠檬香醋饮",
"柠檬银耳冰醋饮",
"豆汁",
"炖鸭肾",
"梅菜扣肉煲",
"膏蟹煲",
"豆花鱼片",
"麻香口条",
"橙汁瓜脯",
"白切鸭",
"川味棒棒鸡",
"五味白肉",
"蜜汁鲜果",
"鲍鱼生菜",
"叉烤鸭",
"姜汁活蟹",
"核桃炖牛肉",
"青豆肉茸炒嫩蛋",
"油炒糖栗子",
"炖银耳肉",
"麻辣萝卜干",
"蒜茸蒸大竹蛏",
"凉拌皮蛋",
"松脆海蜇",
"肚丝拉皮",
"粉皮拌鸡丝",
"豆干牛肉丝",
"红椒皮蛋",
"拌鱿鱼丝",
"蒜茸海带",
"多味凉粉",
"凉拌芹菜叶",
"醋拌莴笋",
"糖拌莲菜",
"冰凉西瓜丁",
"香梨沙拉",
"糖拌双丝",
"拌金针菇",
"香干拌花生米",
"烧拌春笋",
"油汆臭豆腐",
"醋溜茭白",
"百合绿豆汤",
"百合汤",
"浸五香花生米",
"粉蒸芋球",
"凉拌莲藕",
"青蒜烧豆腐",
"辣乳拌腐竹",
"肉酱豆腐",
"煎荷包蛋",
"清蒸膏蟹",
"老厨白菜",
"煎熘腐皮卷",
"胡萝卜苹果醋蛋饮",
"绿茶番茄汤"
};

                string sqlSub = string.Empty;

                foreach(var str in lstStr)
                {
                    sqlSub += "'" + str + "',"; 
                }

                if (!string.IsNullOrEmpty(sqlSub))
                {
                    sqlSub = "(" + sqlSub.TrimEnd(',') + ")"; 
                }

                string Sql = @"select * from diccai a where a.names in " + sqlSub;

                DataTable dt = svc.GetDataTable(Sql);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDicCaiConfig vo = new EntityDicCaiConfig();
                        vo.caiSlaveId = "000022";
                        vo.caiId = dr["id"].ToString();

                        lstParm.Add(svc.GetInsertParm(vo));
                    }
                    if (lstParm.Count > 0)
                        affectRows = svc.Commit(lstParm);

                    if (affectRows > 0)
                    {
                        MessageBox.Show("success!");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
        }

        #region 客户列表
        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EntityClientInfo> GetClientInfo(List<EntityParm> parms = null)
        {
            List<EntityClientInfo> data = null;
            List<EntityReportRecorde> lstReportRecord = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select  a.id,
                            a.gradeId,
                            b.gradeName,
                            a.clientNo,
                            a.clientName,
                            a.gender,
                            a.birthday,
                            a.mobile,
                            a.telephone,
                            a.email,
                            a.qq,
                            a.cardNo,
                            a.company,
                            a.regionId,
                            a.address,
                            a.booldType,
                            a.profession,
                            a.marriage,
                            a.ehtnicGroup,
                            a.eduationLevel,
                            a.clientTag,
                            a.contactName,
                            a.contactNameMobile,
                            a.clientRemarks,
                            a.dataSource,
                            a.upTag,
                            a.serverDate,
                            a.bakfileld1,
                            a.bakfileld2,
                            a.createDate,
                            a.creatorId,
                            a.createName,
                            a.modifyDate,
                            a.modifyId,
                            a.modifyName
                      from clientInfo a 
                        left join userGrade b
                            on a.gradeId = b.id where a.clientNo in(180928250101,
180928250101,
180516150063,
180403150054,
180421650002,
180813650054,
180515150606,
180612150105,
180711150012,
180628150036,
180709100032,
180410150145,
180306150176,
180526150015,
180823600104,
180312150163,
180123150087

)";

            DataTable dt = svc.GetDataTable(Sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityClientInfo>();
                EntityClientInfo vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityClientInfo();
                    vo.id = dr["id"].ToString();
                    vo.gradeId = dr["gradeId"].ToString();
                    vo.gradeName = dr["gradeName"].ToString();
                    vo.clientNo = dr["clientNo"].ToString();
                    vo.clientName = dr["clientName"].ToString();
                    vo.age = Function.CalcAge(Function.Datetime(dr["birthday"]));
                    vo.gender = Function.Int(dr["gender"]);
                    vo.birthday = dr["birthday"].ToString();
                    vo.mobile = dr["mobile"].ToString();
                    vo.telephone = dr["telephone"].ToString();
                    vo.email = dr["email"].ToString();
                    vo.qq = dr["qq"].ToString();
                    vo.company = dr["company"].ToString();
                    vo.regionId = dr["regionId"].ToString();
                    vo.address = dr["address"].ToString();
                    vo.booldType = Function.Int(dr["booldType"]);
                    vo.profession = Function.Int(dr["profession"]);
                    vo.marriage = Function.Int(dr["marriage"]);
                    vo.ehtnicGroup = Function.Int(dr["ehtnicGroup"]);
                    vo.eduationLevel = Function.Int(dr["eduationLevel"]);
                    vo.clientTag = dr["clientTag"].ToString();
                    vo.contactName = dr["contactName"].ToString();
                    vo.contactNameMobile = dr["contactNameMobile"].ToString();
                    vo.clientRemarks = dr["clientRemarks"].ToString();
                    vo.dataSource = dr["dataSource"].ToString();
                    vo.upTag = dr["upTag"].ToString();
                    vo.serverDate = Function.Datetime(dr["serverDate"]);
                    vo.bakfileld1 = dr["bakfileld1"].ToString();
                    vo.bakfileld2 = dr["bakfileld2"].ToString();
                    vo.createDate = Function.Datetime(dr["createDate"]);
                    vo.creatorId = dr["creatorId"].ToString();
                    vo.createName = dr["createName"].ToString();


                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        public List<EntityDisplayDicCaiRecipe> GetDicCaiRecipe()
        {
            List<EntityDisplayDicCaiRecipe> data = null;

            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select c.caiMasterName,c.caiMasterId,a.caiSlaveId,a.caiSlaveName 
                            from dicCaiRecipeSlave a 
                            left join dicCaiRecipeConfig b
                            on a.caiSlaveId = b.caiSlaveId
                            left join dicCaiRecipeMaster c
                            on b.caiMasterId= c.caiMasterId  order by c.caiMasterId  ";

            DataTable dt = svc.GetDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityDisplayDicCaiRecipe>();
                EntityDisplayDicCaiRecipe vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityDisplayDicCaiRecipe();
                    vo.caiMasterId = Function.Int(dr["caiMasterId"]);
                    vo.caiMasterName = dr["caiMasterName"].ToString();
                    vo.caiSlaveId = dr["caiSlaveId"].ToString();
                    vo.caiSlaveName = dr["caiSlaveName"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                int affectRows = -1;
                int classifyId = 21;
                List<string> lstStr = new List<string>{"蜂蛹",
"蜂胚，蜂仔",
"蜂蛹（油蜂）",
"蜂蛹（大黄蜂）",
"葛根",
"花椒叶（干）",
"花粉（油菜）",
"花粉（油松）",
"松花粉",
"花粉（荞麦）",
"牛蛙",
"仙人掌（食用）",
"仙人掌果",
"仙桃",
"仙人掌籽",
"高丽参"};

                string sqlSub = string.Empty;

                foreach (var str in lstStr)
                {
                    sqlSub += "'" + str + "',";
                }

                if (!string.IsNullOrEmpty(sqlSub))
                {
                    sqlSub = "(" + sqlSub.TrimEnd(',') + ")";
                }

                string Sql = @"select * from dicDietIngredient a where a.names in " + sqlSub;

                DataTable dt = svc.GetDataTable(Sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDicIngredientConfig vo = new EntityDicIngredientConfig();
                        vo.classifyId = classifyId;
                        vo.ingredientId = dr["id"].ToString();

                        lstParm.Add(svc.GetInsertParm(vo));
                    }
                    if (lstParm.Count > 0)
                        affectRows = svc.Commit(lstParm);

                    if (affectRows > 0)
                    {
                        MessageBox.Show("success!");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
        }

        private void gridLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void gridLookUpEdit_Click(object sender, EventArgs e)
        {
            //if (gridLookUpEdit1View.RowCount > 0)
            //{
            //    MessageBox.Show("gridLookUpEdit1View.RowCount");
            //}
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            SqlHelper svcOra = new SqlHelper(EnumBiz.interfaceDB);
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                int affectRows = -1;
                string Sql = @"select * from diet_treatment";

                DataTable dt = svcOra.GetDataTable(Sql);
                Sql = @"delete from dicdiettreatment";
                svc.ExecSql(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDietTreatment vo = new EntityDietTreatment();

                        vo.id = dr["id"].ToString();
                        vo.names = dr["names"].ToString();
                        vo.configs = dr["configs"].ToString();
                        vo.fuctions = dr["fuctions"].ToString();
                        vo.usage = dr["usage"].ToString();

                        vo.attention = dr["attention"].ToString();
                        vo.methods = dr["methods"].ToString();
                        vo.bakfield1 = dr["BAK_FIELD1"].ToString();
                        vo.bakfield2 = dr["BAK_FIELD2"].ToString();
                        vo.createDate = DateTime.Now;
                        vo.createName = "系统管理员";
                        vo.creator = "00";

                        lstParm.Add(svc.GetInsertParm(vo));
                    }
                    if (lstParm.Count > 0)
                        affectRows = svc.Commit(lstParm);

                    if (affectRows > 0)
                    {
                        MessageBox.Show("success!");
                    }


                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            SqlHelper svcOra = new SqlHelper(EnumBiz.interfaceDB);
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                int affectRows = -1;
                string Sql = @"select * from client_info where id in ('00f36fe0880945989ad4433fae2a9041',
'08a074e5b1f04858b5d6546f0671add7',
'0b51637cbff04eb0a134e173552b7607',
'130220c56d4146ee85d75d695e751de4',
'1d39ef79907d45c9ada09d8272595cae',
'23998962c6ea4785a25da2a16fabd6ba',
'2bad7b3e32f84890969b292900f15c05',
'30da13fc3f3e476786b615e4e83fb16e',
'3681275eab3d42c98fdbcc6323708370',
'3b74c277e75f42a2b2d52b417594e7e1',
'42ceaff67fd848a3bd2ae4b2dc4e2a72',
'4668b8430e4d48339a5379f8043edfc3',
'49d24e89680148b995e23015abf89bd8',
'5cbead8267d34730a64887006036195d',
'6603220f6cdd464283ac6e7adaece453',
'73073186bb034b93b4e48f878b874161',
'83ed1e74f2ed4d4faf62452b21b1fd7f',
'85d4e580b5b7454081bf8c4ef13308fe',
'957a6714c0af4c85b35993c1640d80c4',
'9a49b34697664c51b60a7f7f600195dc',
'9b29a3ef40a24438b4ae9ac8a5076410',
'ac0ae7b0ed8245dc827a9e3fea5ff96e',
'ad57e4a0d74f4f9bafc7cdbb45471706',
'af6b9be8e4a244d295181768da2c6732',
'b15950e07bdd4c3c97ba9e59f7024eeb',
'b9b3671751ba485a91757a9d84b90dee',
'c09bb70b90ad481baff2302ab4a74e72',
'cc382767fb6041819b6f0f64cbfd92e0',
'ccc9287126db4e858b59bd97a5b3c281',
'dded91191df349d9a22c4877e9b2152f',
'deaa3b3c47f74ad89894532a0e3ff55c',
'df4e2122e017498f9caab6cc0a098c8b',
'dfb161d7ac4d42da872b57b21a46b60d',
'e24a4b4f3b844d798837ee2429238ace',
'ea85655e95874e52a30bb328807b7a6d',
'f0977ebf7096402ea9a5baa74e7d5294',
'fb397b5d21d947fabfa56a3e3be8a56a',
'fdbb7a6a88294ef5b8d52fa102afdb71')";

                DataTable dt = svcOra.GetDataTable(Sql);
                Sql = @"delete from clientInfo where id in ('00f36fe0880945989ad4433fae2a9041',
'08a074e5b1f04858b5d6546f0671add7',
'0b51637cbff04eb0a134e173552b7607',
'130220c56d4146ee85d75d695e751de4',
'1d39ef79907d45c9ada09d8272595cae',
'23998962c6ea4785a25da2a16fabd6ba',
'2bad7b3e32f84890969b292900f15c05',
'30da13fc3f3e476786b615e4e83fb16e',
'3681275eab3d42c98fdbcc6323708370',
'3b74c277e75f42a2b2d52b417594e7e1',
'42ceaff67fd848a3bd2ae4b2dc4e2a72',
'4668b8430e4d48339a5379f8043edfc3',
'49d24e89680148b995e23015abf89bd8',
'5cbead8267d34730a64887006036195d',
'6603220f6cdd464283ac6e7adaece453',
'73073186bb034b93b4e48f878b874161',
'83ed1e74f2ed4d4faf62452b21b1fd7f',
'85d4e580b5b7454081bf8c4ef13308fe',
'957a6714c0af4c85b35993c1640d80c4',
'9a49b34697664c51b60a7f7f600195dc',
'9b29a3ef40a24438b4ae9ac8a5076410',
'ac0ae7b0ed8245dc827a9e3fea5ff96e',
'ad57e4a0d74f4f9bafc7cdbb45471706',
'af6b9be8e4a244d295181768da2c6732',
'b15950e07bdd4c3c97ba9e59f7024eeb',
'b9b3671751ba485a91757a9d84b90dee',
'c09bb70b90ad481baff2302ab4a74e72',
'cc382767fb6041819b6f0f64cbfd92e0',
'ccc9287126db4e858b59bd97a5b3c281',
'dded91191df349d9a22c4877e9b2152f',
'deaa3b3c47f74ad89894532a0e3ff55c',
'df4e2122e017498f9caab6cc0a098c8b',
'dfb161d7ac4d42da872b57b21a46b60d',
'e24a4b4f3b844d798837ee2429238ace',
'ea85655e95874e52a30bb328807b7a6d',
'f0977ebf7096402ea9a5baa74e7d5294',
'fb397b5d21d947fabfa56a3e3be8a56a',
'fdbb7a6a88294ef5b8d52fa102afdb71')";
                svc.ExecSql(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityClientInfo vo = new EntityClientInfo();;
                        vo.id = dr["ID"].ToString();
                        vo.gradeId = dr["GRADE_ID"].ToString();
                        vo.clientNo = dr["CLIENT_NO"].ToString();
                        vo.clientName = dr["CLIENT_NAME"].ToString();
                        vo.gender = Function.Int( dr["GENDER"]);
                        vo.birthday = dr["BIRTHDAY"].ToString();
                        vo.mobile = dr["MOBILE"].ToString();
                        vo.telephone = dr["TELEPHONE"].ToString();
                        vo.email = dr["EMAIL"].ToString();
                        vo.qq = dr["QQ"].ToString();
                        vo.cardNo = dr["CARDNO"].ToString();
                        vo.company = dr["COMPANY"].ToString();
                        vo.regionId = dr["REGION_ID"].ToString();
                        vo.address = dr["ADDRESS"].ToString();
                        vo.booldType = Function.Int(dr["BOOLD_TYPE"]);
                        vo.profession = Function.Int(dr["PROFESSION"]);
                        vo.marriage = Function.Int(dr["MARRIAGE"]);
                        vo.ehtnicGroup = Function.Int(dr["ETHNIC_GROUP"]);
                        vo.eduationLevel = Function.Int(dr["EDUCATION_LEVEL"]);
                        vo.clientTag = dr["CLIENT_TAG"].ToString();
                        vo.contactName = dr["CONTACT_NAME"].ToString();
                        vo.contactNameMobile = dr["CONTACT_NAME_MOBILE"].ToString();
                        vo.clientRemarks = dr["CLIENT_REMARKS"].ToString();
                        vo.dataSource = dr["DATA_SOURCE"].ToString();
                        vo.upTag = dr["UP_TAG"].ToString();
                        vo.serverDate = Function.Datetime(dr["SERVER_DATE"]);
                        vo.bakfileld1 = dr["BAK_FIELD1"].ToString();
                        vo.bakfileld2 = dr["BAK_FIELD2"].ToString();
                        vo.createDate = Function.Datetime(dr["CREATE_ON"]);
                        vo.creatorId = "00";
                        vo.createName = "系统管理员";
                        lstParm.Add(svc.GetInsertParm(vo));
                    }
                    if (lstParm.Count > 0)
                        affectRows = svc.Commit(lstParm);

                    if (affectRows > 0)
                    {
                        MessageBox.Show("success!");
                    }


                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
        }


        EntityClientInfo GetRowObject()
        {
            if (this.gvClient.FocusedRowHandle < 0) return null;
            return this.gvClient.GetRow(this.gvClient.FocusedRowHandle) as EntityClientInfo;
        }

        private void gvClient_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                EntityClientInfo clientInfo = GetRowObject();
                if (gvClient.IsRowSelected(e.RowHandle))
                {
                    gvClient.UnselectRow(e.RowHandle);
                    //this.imageListBoxControl.Items.Remove(clientInfo);
                    lstClientSelect.Add(clientInfo);
                    this.gridControl1.DataSource = lstClientSelect;
                    gridControl1.RefreshDataSource();

                }
                else
                {
                    gvClient.SelectRow(e.RowHandle);
                    if (clientInfo != null)
                    {
                        //this.imageListBoxControl.Items.Add(clientInfo);
                        lstClientSelect.Remove(clientInfo);
                        this.gridControl1.DataSource = lstClientSelect;
                        gridControl1.RefreshDataSource();
                    }
                }
            }
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            GetProcDataSet();
        }


        public void GetProcDataSet()
        {

            try
            {
                OracleParameter[] parms = {
                                        new OracleParameter("v_noofinpat", OracleDbType.Int32),
                                        new OracleParameter("o_result", OracleDbType.RefCursor),
                                        new OracleParameter("o_result1", OracleDbType.RefCursor),
                                        new OracleParameter("o_result2", OracleDbType.RefCursor),
                                        new OracleParameter("o_result3", OracleDbType.RefCursor),

                                     };

                parms[0].Value = 45927349;
                parms[1].Direction = ParameterDirection.Output;
                parms[2].Direction = ParameterDirection.Output;
                parms[3].Direction = ParameterDirection.Output; ;
                parms[4].Direction = ParameterDirection.Output; ;

                string connString = "User ID=emr;Password=emr;Data Source=TJGL;";
                string sql = @"IEM_MAIN_PAGE_SX.usp_getieminfo_sx";

                DataSet ds = OracleHelper.OracleHelper.ExecuteDataset(connString, sql, parms);
                if (ds != null)
                {
                    MessageBox.Show("1111");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnSysMainItem_Click(object sender, EventArgs e)
        {
            SqlHelper svcOra = new SqlHelper(EnumBiz.interfaceDB);
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                int affectRows = -1;
                string Sql = @"select * from REPORT_MAIN_ITEM";

                DataTable dt = svcOra.GetDataTable(Sql);
                Sql = @"delete from reportMainItem";
                svc.ExecSql(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityReportMainItem vo = new EntityReportMainItem();

                        vo.id = dr["ID"].ToString();
                        vo.clientId = dr["CLIENT_ID"].ToString();
                        vo.reportId = dr["REPORT_ID"].ToString();
                        vo.sectionName = dr["SECTION_NAME"].ToString();
                        vo.itemName = dr["ITEM_NAME"].ToString();
                        vo.itemValue = dr["ITEM_VALUE"].ToString();
                        vo.itemUnits = dr["ITEM_UNITS"].ToString();
                        vo.itemRefrange = dr["ITEM_REFRANGE"].ToString();
                        vo.isNormal = dr["IS_NORMAL"].ToString();
                        vo.minValue = dr["MIN_VALUE"].ToString();
                        vo.maxValue = dr["MAX_VALUE"].ToString();
                        vo.orderId = Function.Dec( dr["ORDER_ID"]);
                        vo.bakField1 = dr["BAK_FIELD1"].ToString();
                        vo.bakField2 = dr["BAK_FIELD2"].ToString();
                        vo.createDate = Function.Datetime(dr["CREATE_ON"]);
                        vo.createId = dr["CREATE_USER_ID"].ToString();
                        vo.createName = dr["CREATE_BY"].ToString();
                        vo.modifyDate = Function.Datetime(dr["MODIFED_ON"]);
                        vo.modifyId = dr["MODIFED_USER_ID"].ToString();
                        vo.modifyName = dr["MODIFED_BY"].ToString();

                        string sql = @"insert into reportMainItem (id,clientId,reportId,sectionName,itemName,itemValue,
                                                                    itemUnits,itemRefrange,isNormal,minValue,
                                                                    maxValue,orderId,bakField1,bakField2,createDate,createId,
                                                                    createName,modifyDate,modifyId,modifyName) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                        IDataParameter[] parm = svc.CreateParm(20);
                        parm[0].Value = vo.id;
                        parm[1].Value = vo.clientId;
                        parm[2].Value = vo.reportId;
                        parm[3].Value = vo.sectionName;
                        parm[4].Value = vo.itemName;
                        parm[5].Value = vo.itemValue;
                        parm[6].Value = vo.itemUnits;
                        parm[7].Value = vo.itemRefrange;
                        parm[8].Value = vo.isNormal;
                        parm[9].Value = vo.minValue;
                        parm[10].Value = vo.maxValue;
                        parm[11].Value = vo.orderId;
                        parm[12].Value = vo.bakField1;
                        parm[13].Value = vo.bakField2;
                        parm[14].Value = vo.createDate;
                        parm[15].Value = vo.createId;
                        parm[16].Value = vo.createName;
                        parm[17].Value = vo.modifyDate;
                        parm[18].Value = vo.modifyId;
                        parm[19].Value = vo.modifyName;

                        affectRows = svc.ExecSql(sql,parm);
                    }

                    if (affectRows > 0)
                    {
                        MessageBox.Show("success!");
                    }


                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
        }

        private void btnSysReportItem_Click(object sender, EventArgs e)
        {
            SqlHelper svcOra = new SqlHelper(EnumBiz.interfaceDB);
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                int affectRows = -1;
                string Sql = string.Empty;

                Sql = @"select distinct reportId from reportRecorde";
                DataTable dtRpt = svc.GetDataTable(Sql);
                if(dtRpt != null && dtRpt.Rows.Count > 0)
                {
                    foreach (DataRow drr in dtRpt.Rows)
                    {
                        string reportId = drr["reportId"].ToString();
                        Sql = @"select * from REPORT_ITEM where report_id = '" + reportId + "'" ;
                        DataTable dt = svcOra.GetDataTable(Sql);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                EntityReportItem vo = new EntityReportItem();

                                vo.reportId = dr["REPORT_ID"].ToString();
                                vo.sectionName = dr["SECTION_NAME"].ToString();
                                vo.itemName = dr["ITEM_NAME"].ToString();
                                vo.itemValue = dr["ITEM_VALUE"].ToString();
                                vo.itemUnit = dr["ITEM_UNIT"].ToString();
                                vo.memo = dr["MEMO"].ToString();
                                vo.refRange = dr["REF_RANGE"].ToString();
                                vo.minValue = Function.Dec(dr["MIN_VALUE"]);
                                vo.maxValue = Function.Dec(dr["MAX_VALUE"]);


                                string sql = @"insert into reportItem (reportId,sectionName,itemName,itemValue,
                                                                    itemUnit,memo,refRange,minValue,
                                                                    maxValue) values (?,?,?,?,?,?,?,?,?)";
                                IDataParameter[] parm = svc.CreateParm(9);

                                parm[0].Value = vo.reportId;
                                parm[1].Value = vo.sectionName;
                                parm[2].Value = vo.itemName;
                                parm[3].Value = vo.itemValue;
                                parm[4].Value = vo.itemUnit;
                                parm[5].Value = vo.memo;
                                parm[6].Value = vo.refRange;
                                parm[7].Value = vo.minValue;
                                parm[8].Value = vo.maxValue;

                                affectRows = svc.ExecSql(sql, parm);
                            } 
                        }
                    }

                    if (affectRows > 0)
                    {
                        MessageBox.Show("success!");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
        }

        #endregion


        List<EntityModelParamTop> lstTopic = null;
        List<EntityModelParam> lstItems = null;
        Dictionary<string, decimal> dicModelAccess = new Dictionary<string, decimal>();

        private void Form1_Load(object sender, EventArgs e)
        {
            labelControl1.BackColor = Color.FromArgb(0, Color.Transparent);

            //this.gridLookUpEdit.Properties.DataSource = GetDicCaiRecipe();
            //using (Biz202 biz = new Biz202())
            //{

            //    List<EntityQnRecord> lstQnRecords = biz.GetQnRecords();

            //    xrptQuest02 xr2 = new xrptQuest02(lstQnRecords[0]);
            //    xr2.CreateDocument();//创建报表
            //    xrptQuest01 xr1 = new xrptQuest01(lstQnRecords[0]);
            //    xr1.CreateDocument();//创建报表
            //    xrptQn xr = new xrptQn();
            //    xr.xrptQuest02.ReportSource = xr2;
            //    xr.xrptQuest01.ReportSource = xr1;
            //    xr.CreateDocument();//创建报表
            //    this.documentViewer1.PrintingSystem = xr.PrintingSystem;
            //}

            //labelControl1.Text = this.labelControl2.Text.Length.ToString();
            //labelControl3.Text = this.checkEdit1.Text.Length.ToString();
            //labelControl4.Text = this.checkEdit1.Width.ToString();


            //GetQnCustom(1, out lstTopic, out lstItems);
            //if (lstTopic != null)
            //{
            //    gcPromotionTemplate.DataSource = lstTopic;
            //    gcPromotionTemplate.RefreshDataSource();
            //}

            //List<EntityModelAccess> lstModelAccess = GetModelAccesses();
            //if (lstModelAccess != null)
            //{
            //    foreach (var vo in lstModelAccess)
            //    {
            //        cboModel.Properties.Items.Add(vo.modelName);
            //        dicModelAccess.Add(vo.modelName, vo.modelId);
            //    }
            //}





        }

        #region tab5

        #region 获取疾病模型
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EntityModelAccess> GetModelAccesses()
        {
            List<EntityModelAccess> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select a.modelId,
                           a.modelName
                      from modelAccess a ";
            DataTable dt = svc.GetDataTable(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityModelAccess>();
                EntityModelAccess vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityModelAccess();
                    vo.modelId = Function.Int(dr["modelId"]);
                    vo.modelName = dr["modelName"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }

        #endregion

        #region GetQnCustom
        /// <summary>
        /// GetQnCustom
        /// </summary>
        /// <param name="qnId"></param>
        /// <param name="lstTopic"></param>
        /// <param name="lstItems"></param>
        public void GetQnCustom(decimal qnId, out List<EntityModelParamTop> lstTopic, out List<EntityModelParam> lstItems)
        {
            lstTopic = null;
            lstItems = null;
            string Sql = string.Empty;
            DataTable dt = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            IDataParameter[] parm = null;

            // 题目(标题)
            Sql = @"select c.qnClassId,
                           c.typeId,
                           c.fieldId,
                           c.fieldName,
                           c.isEssential,
                           c.parentFieldId,
                           b.questName
                      from dicQnMain a
                     inner join dicQnDetail b
                        on a.qnId = b.qnId
                     inner join dicQnSetting c
                        on b.fieldId = c.fieldId
                     where a.qnId = ? 
                       and c.status = 1  
                     order by c.sortNo ";

            parm = svc.CreateParm(1);
            parm[0].Value = qnId;
            dt = svc.GetDataTable(Sql, parm);
            if (dt != null && dt.Rows.Count > 0)
            {
                lstTopic = new List<EntityModelParamTop>();
                string fieldIds = string.Empty;
                string parentFieldId = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    lstTopic.Add(new EntityModelParamTop()
                    {
                        filedId = dr["fieldId"].ToString(),
                        fieldName = dr["fieldName"].ToString()
                    });
                    parentFieldId = dr["parentFieldId"].ToString();
                    if (string.IsNullOrEmpty(parentFieldId))
                        fieldIds += "'" + dr["fieldId"].ToString() + "',";
                    if (fieldIds.Contains(parentFieldId))
                        continue;
                    fieldIds += "'" + dr["parentFieldId"].ToString() + "',";
                }
                fieldIds = fieldIds.TrimEnd(',');
                if (fieldIds != string.Empty)
                {
                    // 题目选项
                    lstItems = new List<EntityModelParam>();
                    Sql = @"select c.qnClassId,
                                   c.typeId,
                                   c.fieldId,
                                   c.fieldName,
                                   c.isEssential,
                                   c.parentFieldId
                              from dicQnSetting c
                             where c.parentFieldId in ({0}) 
                               and c.status = 1 ";
                    dt = svc.GetDataTable(string.Format(Sql, fieldIds));
                    foreach (DataRow dr in dt.Rows)
                    {
                        lstItems.Add(new EntityModelParam()
                        {
                            parentFieldId = dr["parentFieldId"].ToString(),
                            paramNo = dr["fieldId"].ToString(),
                            judgeType = 2,
                            judgeValue = 1,
                            judgeRange = string.IsNullOrEmpty(dr["fieldName"].ToString()) ? "是" : dr["fieldName"].ToString().Replace("1、","").Replace("2、", "").Replace("3、", "").Replace("4、", "").Replace("5、", "")
                        });
                    }
                }
            }
        }


        #endregion

        #endregion

        private void gvPromotionTemplate_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if(e.RowHandle >= 0)
            {
                EntityModelParamTop vo = gvPromotionTemplate.GetRow(e.RowHandle) as EntityModelParamTop;

                if(vo !=null)
                {
                    gcPromotionTemplateConfig.DataSource = lstItems.FindAll(r=>r.parentFieldId == vo.filedId);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            IDataParameter[] parm = null;
            string sql = @"INSERT INTO modelParam( modelId, 
                            judgeType,
                            paramType,
                            gender, 
                            isChange, 
                            paramNo, 
                            paramName, 
                            judgeValue, 
                            judgeRange, 
                            score, 
                            modulus,parentFieldId ) VALUES 
                            (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?) ";
            if(gvPromotionTemplateConfig.RowCount > 0)
            {
                for(int i = 0;i< gvPromotionTemplateConfig.RowCount;i++)
                {
                    if(gvPromotionTemplateConfig.IsRowSelected(i))
                    {
                        EntityModelParam vo = gvPromotionTemplateConfig.GetRow(i) as EntityModelParam;
                        if (string.IsNullOrEmpty(cboModel.Text))
                        {
                            MessageBox.Show("疾病类型为空！");
                            return;
                        }
                            
                        vo.modelId = dicModelAccess[cboModel.Text];
                        vo.gender = 0;
                        vo.paramType = 3;
                        vo.isChange = 1;
                        parm = svc.CreateParm(12);
                        parm[0].Value = vo.modelId;
                        parm[1].Value = vo.judgeType;
                        parm[2].Value = vo.paramType;
                        parm[3].Value = vo.gender;
                        parm[4].Value = vo.isChange;
                        parm[5].Value = vo.paramNo;
                        parm[6].Value = vo.paramName;
                        parm[7].Value = vo.judgeValue;
                        parm[8].Value = vo.judgeRange;
                        parm[9].Value = vo.score;
                        parm[10].Value = vo.modulus;
                        parm[11].Value = vo.paramNo.Substring(0, 4);
                        svc.ExecSql(sql, parm);
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text) )
            {
                gcPromotionTemplate.DataSource = lstTopic;
                gcPromotionTemplate.RefreshDataSource();
            }
            else
            {
                gcPromotionTemplate.DataSource = lstTopic.FindAll(r=>r.fieldName.Contains(txtSearch.Text));
                gcPromotionTemplate.RefreshDataSource();
            }
        }
    }

    public class EntityModelParamTop
    {
        public string filedId { get; set; }
        public string fieldName { get; set; }
        public string paramName { get; set; }
    }
}
