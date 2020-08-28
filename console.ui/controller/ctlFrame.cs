using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;
using DevExpress.XtraBars;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Console.Ui
{
    /// <summary>
    /// ctlFrame
    /// </summary>
    public class ctlFrame : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmFrame Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmFrame)child;
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// rm
        /// </summary>
        internal System.Resources.ResourceManager rm { get; set; }

        internal bool IsLoadform { get; set; }

        List<EntityFunction> SysFunctions { get; set; }

        EntityAccount DefaultModule { get; set; }

        DevExpress.XtraEditors.PictureEdit PatPic { get; set; }

        Dictionary<string, List<EntitySysModule>> FuncItems { get; set; }

        internal bool isMaximized { get; set; }

        /// <summary>
        /// 模块功能ID
        /// </summary>
        string ModuleFuncId { get; set; }

        #endregion

        #region 方法

        #region SetFuncs

        #region SetFuncs1
        /// <summary>
        /// SetFuncsBiz
        /// </summary>
        private void SetFuncsBiz()
        {
            // 默认：暂时只用2级菜单
            List<EntityAccount> lst1 = new List<EntityAccount>();
            Dictionary<string, List<EntityAccount>> lst2 = new Dictionary<string, List<EntityAccount>>();
            foreach (EntityAccount item in GlobalAppConfig.AccountFuncs)
            {
                if (item.IsLeaf == false) lst1.Add(item);
            }
            foreach (EntityAccount item in GlobalAppConfig.AccountFuncs)
            {
                if (item.IsLeaf && lst1.Exists(t => t.FuncId.ToString() == item.ParentCode))
                {
                    if (lst2.ContainsKey(item.ParentCode))
                    {
                        lst2[item.ParentCode].Add(item);
                    }
                    else
                    {
                        List<EntityAccount> tmp = new List<EntityAccount>();
                        tmp.Add(item);
                        lst2.Add(item.ParentCode, tmp);
                    }
                }
            }
            int i1 = 0;
            int i2 = 0;
            DevExpress.XtraBars.BarButtonItem barItem = null;
            DevExpress.XtraBars.BarSubItem barSubItem = null;
            foreach (EntityAccount item in lst1)
            {
                barSubItem = new DevExpress.XtraBars.BarSubItem();
                barSubItem.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                barSubItem.Name = item.FuncCode;
                barSubItem.Caption = Convert.ToString(++i1) + ". " + item.FuncName;
                if (!string.IsNullOrEmpty(item.ImageSource)) barSubItem.Glyph = (rm.GetObject(item.ImageSource) as Image);
                Viewer.pmModules.ItemLinks.Add(barSubItem, true);
                if (lst2.ContainsKey(item.FuncId.ToString()))
                {
                    i2 = 0;
                    foreach (EntityAccount item2 in lst2[item.FuncId.ToString()])
                    {
                        barItem = new DevExpress.XtraBars.BarButtonItem();
                        barItem.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        barItem.Name = item2.FuncCode;
                        barItem.Caption = i1.ToString() + "." + Convert.ToString(++i2) + " " + item2.FuncName;
                        barItem.Tag = item2;
                        barItem.ItemClick += new ItemClickEventHandler(bbiItem_ItemClick);
                        if (!string.IsNullOrEmpty(item2.ImageSource)) barItem.Glyph = (rm.GetObject(item2.ImageSource) as Image);
                        barSubItem.ItemLinks.Add(barItem, true);
                        if (GlobalParm.dicSysMenu.ContainsKey(item2.FuncName) == false)
                        {
                            GlobalParm.dicSysMenu.Add(item2.FuncName, item2);
                        }
                    }
                }
            }

            DefaultModule = new EntityAccount() { FuncId = 88888888, FuncCode = "Hms.Ui.frmAccess", FuncName = "导航图...", FuncFile = "hms.ui.dll", OperName = "Show" };
            Viewer.timer.Enabled = true;

        }
        #endregion

        #region SetFuncs2
        /// <summary>
        /// SetFuncs2
        /// </summary>
        /// <param name="funcID"></param>
        private void SetFuncs2(int funcID)
        {
            List<EntitySysModule> data = null;
            if (FuncItems.ContainsKey(funcID.ToString()))
            {
                data = FuncItems[funcID.ToString()];
            }
            else
            {
                using (ProxyLogin proxy = new ProxyLogin())
                {
                    data = proxy.Service.GetAccount(GlobalLogin.objLogin.EmpNo, funcID, 1);
                    FuncItems.Add(funcID.ToString(), data);
                }
            }
        }
        #endregion

        #region SetFuncs3
        /// <summary>
        /// SetFuncs3
        /// </summary>
        /// <param name="className"></param>
        internal void GetFormFuncButton(string className)
        {
            if (string.IsNullOrEmpty(className)) return;
            Viewer.rpgToolbarForm.Visible = false;
            Viewer.rpgToolbarUc.Visible = false;
            Viewer.rpgToolbarPlus.Visible = false;
            Viewer.rpgToolbarForm.ItemLinks.Clear();
            Viewer.rpgToolbarUc.ItemLinks.Clear();
            Viewer.rpgToolbarPlus.ItemLinks.Clear();

            List<EntitySysModule> data = GetFuncButton(className);
            if (data != null && data.Count > 0)
            {
                AddItemButton(data, Viewer.rpgToolbarForm);
            }
            SetDefaultButton();
        }
        #endregion

        #region SetFuncs4
        /// <summary>
        /// SetFuncs4
        /// </summary>
        private void SetDefaultButton()
        {
            EntitySysModule vo = null;
            List<EntitySysModule> lstVo = new List<EntitySysModule>();

            // 1.折叠
            vo = new EntitySysModule();
            vo.OperName = "fold";
            vo.FuncName = "折叠";
            vo.ImageSource = "Zoom2";
            lstVo.Add(vo);

            // 2.关闭
            vo = new EntitySysModule();
            vo.OperName = "close";
            vo.FuncName = "关闭";
            vo.ImageSource = "Close";
            lstVo.Add(vo);

            AddItemButton(lstVo, Viewer.rpgToolbarPlus);
        }
        #endregion

        #region SetFuncs5
        /// <summary>
        /// LoadDefaultModule
        /// </summary>
        internal void LoadDefaultModule()
        {
            Viewer.timer.Enabled = false;
            if (DefaultModule != null)
            {
                LoadForm(DefaultModule);
            }
        }
        #endregion

        #region AddItemButton
        /// <summary>
        /// AddItemButton
        /// </summary>
        /// <param name="lstVo"></param>
        private void AddItemButton(List<EntitySysModule> lstVo, DevExpress.XtraBars.Ribbon.RibbonPageGroup rpg)
        {
            DevExpress.XtraBars.BarButtonItem barItem = null;
            foreach (EntitySysModule vo1 in lstVo)
            {
                barItem = new DevExpress.XtraBars.BarButtonItem();
                barItem.Name = vo1.OperName;
                barItem.Caption = (vo1.FuncName.IndexOf("-") > 0 ? vo1.FuncName.Substring(vo1.FuncName.IndexOf("-") + 1) : vo1.FuncName);
                barItem.LargeGlyph = (rm.GetObject(vo1.ImageSource) as Image);
                barItem.ItemClick += new ItemClickEventHandler(bbiItemButton_ItemClick);
                rpg.ItemLinks.Add(barItem);
                //rpg.ItemLinks.Add(barItem, true);   // 加组 | 竖条 
            }
            rpg.Visible = true;
            if (Viewer.ActiveMdiChild != null && rpg == Viewer.rpgToolbarForm) Viewer.ActiveMdiChild.Tag = lstVo;
        }
        #endregion

        #region bbiItemButton.ItemClick
        /// <summary>
        /// bbiItemButton.ItemClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiItemButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Viewer.ActiveMdiChild == null || !(Viewer.ActiveMdiChild is frmBaseMdi))
            {
                return;
            }
            switch (e.Item.Name.ToLower())
            {
                case "new":
                    (Viewer.ActiveMdiChild as frmBaseMdi).New();
                    break;
                case "copy":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Copy();
                    break;
                case "paste":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Paste();
                    break;
                case "delete":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Delete();
                    break;
                case "insert":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Insert();
                    break;
                case "design":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Design();
                    break;
                case "eform":
                    (Viewer.ActiveMdiChild as frmBaseMdi).EformDesign();
                    break;
                case "edit":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Edit();
                    break;
                case "refresh":
                    (Viewer.ActiveMdiChild as frmBaseMdi).RefreshData();
                    break;
                case "save":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Save();
                    break;
                case "put":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Put();
                    break;
                case "stop":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Stop();
                    break;
                case "complete":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Complete();
                    break;
                case "search":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Search();
                    break;
                case "confirm":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Confirm();
                    break;
                case "unconfirm":
                    (Viewer.ActiveMdiChild as frmBaseMdi).UnConfirm();
                    break;
                case "cancel":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Cancel();
                    break;
                case "load":
                    (Viewer.ActiveMdiChild as frmBaseMdi).LoadData();
                    break;
                case "template":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Template();
                    break;
                case "preview":
                case "print":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Preview();
                    break;
                case "reloaddic":
                    (Viewer.ActiveMdiChild as frmBaseMdi).ReloadDic();
                    break;
                case "customform":
                    (Viewer.ActiveMdiChild as frmBaseMdi).CustomForm();
                    break;
                case "defineitem":
                    (Viewer.ActiveMdiChild as frmBaseMdi).DefineItem();
                    break;
                case "export":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Export();
                    break;
                case "stat":
                case "statistics":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Statistics();
                    break;
                case "remind":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Remind();
                    break;
                case "child":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Child();
                    break;
                case "navpatientchange":
                    (Viewer.ActiveMdiChild as frmBaseMdi).NavPatientChange();
                    break;
                case "cpin":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Cpin();
                    break;
                case "cpmgt":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Cpmgt();
                    break;
                case "register":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Register();
                    break;
                case "bedmgr":
                    (Viewer.ActiveMdiChild as frmBaseMdi).BedMgr();
                    break;
                case "refrence":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Refrence();
                    break;
                case "previous":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Previous();
                    break;
                case "next":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Next();
                    break;
                case "checkin":
                    (Viewer.ActiveMdiChild as frmBaseMdi).CheckIn();
                    break;
                case "barcode":
                    (Viewer.ActiveMdiChild as frmBaseMdi).BarCode();
                    break;
                case "consent":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Consent();
                    break;
                case "find":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Find();
                    break;
                case "capture":
                    (Viewer.ActiveMdiChild as frmBaseMdi).Capture();
                    break;
                case "fold":
                    SendKeys.Send("^{F1}");
                    break;
                case "close":
                    if (Viewer.ActiveMdiChild.Name.IndexOf("frmAccess") < 0)
                        Viewer.ActiveMdiChild.Close();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ResetToolBar
        /// <summary>
        /// ResetToolBar
        /// </summary>
        /// <param name="isLoad"></param>
        internal void ResetToolBar(string className, bool isLoad)
        {
            if (isLoad)
            {
                GetFormFuncButton(className);
            }
            else
            {
                Viewer.rpgToolbarForm.ItemLinks.Clear();
                Viewer.rpgToolbarUc.ItemLinks.Clear();
                Viewer.rpgToolbarPlus.ItemLinks.Clear();
                Viewer.rpgToolbarForm.Visible = false;
                Viewer.rpgToolbarUc.Visible = false;
                Viewer.rpgToolbarPlus.Visible = false;
            }
        }

        /// <summary>
        /// ResetToolBar
        /// </summary>
        /// <param name="ucName"></param>
        internal void ResetToolBar(string ucName)
        {
            Viewer.rpgToolbarUc.ItemLinks.Clear();
            Viewer.rpgToolbarUc.Visible = false;

            List<EntitySysModule> data = GetFuncButton(ucName);
            if (data != null && data.Count > 0)
            {
                AddItemButton(data, Viewer.rpgToolbarUc);
            }
        }
        #endregion

        #region GetFuncButton
        /// <summary>
        /// GetFormFuncButton
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        internal List<EntitySysModule> GetFuncButton(string className)
        {
            List<EntitySysModule> data = null;
            if (FuncItems.ContainsKey(className))
            {
                data = FuncItems[className];
            }
            else
            {
                using (ProxyLogin proxy = new ProxyLogin())
                {
                    data = proxy.Service.GetFormFuncButton(GlobalLogin.objLogin.EmpNo, className);
                    FuncItems.Add(className, data);
                }
            }
            return data;
        }
        #endregion

        #endregion

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            #region 资源 
            // 资源
            rm = new System.Resources.ResourceManager(typeof(Properties.Resources));
            uiHelper.MdiParent = Viewer;
            #endregion

            #region 系统名称
            Viewer.Width = Screen.PrimaryScreen.WorkingArea.Width;
            Viewer.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (Viewer.Tag != null)
            {
                string captionTxt = string.Empty;
                ModuleFuncId = Viewer.Tag.ToString();

                ///// 
                ///// 

                captionTxt = "健康管理信息平台";
                Viewer.lblSysWelcome.Text = "欢迎使用健康管理信息平台!";
                Viewer.ribbonControl.ApplicationButtonText = "weCare健康管理信息平台";

                Viewer.Text = captionTxt;
            }
            #endregion

            #region 功能模块
            this.FuncItems = new Dictionary<string, List<EntitySysModule>>();
            this.SetFuncsBiz();
            #endregion

            #region 主题
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(Viewer.rgbiSkins, true);
            Viewer.defaultLookAndFeel.LookAndFeel.StyleChanged += new EventHandler(LookAndFeel_StyleChanged);

            string blankStart = " ";
            string blankEnd = "   ";
            Viewer.bsiHospital.Caption += blankStart + GlobalHospital.HospitalName + blankEnd;
            Viewer.bsiLoginer.Caption += blankStart + GlobalLogin.objLogin.EmpName + blankEnd;
            Viewer.bsiLoginDept.Caption += blankStart + GlobalLogin.objLogin.DeptName + blankEnd;
            Viewer.bsiLoginDate.Caption += blankStart + Common.Utils.Utils.ServerTime().ToString("yy年MM月dd日 HH时mm分") + blankEnd;
            Viewer.bsiLocalIP.Caption += blankStart + Function.LocalIP() + blankEnd;
            if (GlobalAppConfig.RunningMode == 3)
                Viewer.bsiService.Caption += blankStart + GlobalAppConfig.MidderServerIP + blankEnd;
            else
                Viewer.bsiService.Caption += blankStart + "两层模式" + blankEnd;

            // 主题           
            string skinName = Function.ReadLocalSettingValue("Main|skinName", "value");
            if (!string.IsNullOrEmpty(skinName)) GlobalLogin.SkinName = skinName;
            Viewer.defaultLookAndFeel.LookAndFeel.SkinName = GlobalLogin.SkinName;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(GlobalLogin.SkinName);
            GlobalLogin.SkinMaskColorValue = Function.ReadLocalSettingValue("Main|skinMaskColor", "value");
            GlobalLogin.SkinMaskColorValue2 = Function.ReadLocalSettingValue("Main|skinMaskColor2", "value");
            if (!string.IsNullOrEmpty(GlobalLogin.SkinMaskColorValue))
            {
                Viewer.defaultLookAndFeel.LookAndFeel.SkinMaskColor = GlobalLogin.SkinMaskColor;
                Viewer.defaultLookAndFeel.LookAndFeel.SkinMaskColor2 = GlobalLogin.SkinMaskColor2;
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinMaskColors(GlobalLogin.SkinMaskColor, GlobalLogin.SkinMaskColor2);
            }
            #endregion

        }
        #endregion

        #region LookAndFeel_StyleChanged
        /// <summary>
        /// LookAndFeel_StyleChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LookAndFeel_StyleChanged(object sender, EventArgs e)
        {
            if (GlobalParm.IsPopupOpening || this.IsLoadform || GlobalLogin.SkinName == Viewer.defaultLookAndFeel.LookAndFeel.SkinName)
            {
                return;
            }
            GlobalLogin.SkinName = Viewer.defaultLookAndFeel.LookAndFeel.SkinName;
            // 主题
            string skinName = Function.ReadLocalSettingValue("Main|skinName", "value");
            if (!string.IsNullOrEmpty(skinName))
            {
                Function.SetLocalSettingValue("Main|skinName", "value", GlobalLogin.SkinName);
            }
            else
            {
                EntityLocalSetting vo = new EntityLocalSetting();
                vo.EmpNo = GlobalLogin.objLogin.EmpNo;
                vo.Type = 3;
                vo.Parent = "Common";
                vo.Node = "SkinName";
                vo.Value = GlobalLogin.SkinName;
                ProxyFrame proxyFrame = new ProxyFrame();
                proxyFrame.Service.UpdateLocalSetting(vo);
            }
            if (Viewer.ActiveMdiChild != null && Viewer.ActiveMdiChild is frmBaseMdi)
            {
                ((frmBaseMdi)Viewer.ActiveMdiChild).SetNavBarStyleView();
            }
        }
        #endregion

        #region PauseRedraw/StartRedraw
        /// <summary>
        /// PauseRedraw
        /// </summary>
        internal void PauseRedraw()
        {
            Function.SuspendLayout(Viewer.Handle);
        }
        /// <summary>
        /// StartRedraw
        /// </summary>
        internal void StartRedraw()
        {
            GlobalLogin.SkinName = Viewer.defaultLookAndFeel.LookAndFeel.SkinName;
            Function.ResumeLayout(Viewer.Handle);
            Viewer.Refresh();
        }
        #endregion

        #region pic.MouseEvent
        /// <summary>
        /// pic_MouseEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_MouseEnter(object sender, EventArgs e)
        {
            (sender as DevExpress.XtraEditors.PictureEdit).BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
        }
        /// <summary>
        /// pic_MouseEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_MouseLeave(object sender, EventArgs e)
        {
            (sender as DevExpress.XtraEditors.PictureEdit).BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        }
        #endregion

        #region SetCaption
        /// <summary>
        /// SetCaption
        /// </summary>
        internal void SetCaption(int flag)
        {
            if (flag == 0)
            {
                if (isMaximized) return;
                Viewer.Location = new Point(0, 0);
                Viewer.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
                Viewer.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            }
            else if (flag == 1)
            {
                if (Viewer.FormBorderStyle == System.Windows.Forms.FormBorderStyle.Sizable)
                {
                    isMaximized = false;
                    Viewer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    Viewer.Location = new Point(0, 0);
                    Viewer.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
                    Viewer.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
                }
                else
                {
                    isMaximized = true;
                    Viewer.Text = string.Empty;
                    Viewer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                    Viewer.ControlBox = false;
                }
            }
            GlobalParm.MdiParentSubH = Viewer.ribbonControl.Height;
        }
        #endregion

        #region ToolbarItemImage
        /// <summary>
        /// ToolbarItemImage
        /// </summary>
        /// <param name="funcCode"></param>
        /// <returns></returns>
        internal Image ToolbarItemImage(string funcCode)
        {
            object obj = null;
            switch (funcCode)
            {
                case "addItem":
                    obj = rm.GetObject("AddItem");
                    break;
                case "apply":
                    obj = rm.GetObject("Apply");
                    break;
                case "cancel":
                    obj = rm.GetObject("Cancel");
                    break;
                case "close":
                    obj = rm.GetObject("Close");
                    break;
                case "delete":
                    obj = rm.GetObject("Delete");
                    break;
                case "deleteItem":
                    obj = rm.GetObject("DeleteItem");
                    break;
                case "design":
                    obj = rm.GetObject("Design");
                    break;
                case "new":
                    obj = rm.GetObject("New");
                    break;
                case "refresh":
                    obj = rm.GetObject("Refresh");
                    break;
                case "chart":
                    obj = rm.GetObject("Chart");
                    break;
                case "copy":
                    obj = rm.GetObject("Copy");
                    break;
                case "customization":
                    obj = rm.GetObject("Customization");
                    break;
                case "cut":
                    obj = rm.GetObject("Cut");
                    break;
                case "edit":
                    obj = rm.GetObject("Edit");
                    break;
                case "image":
                    obj = rm.GetObject("Image");
                    break;
                case "paste":
                    obj = rm.GetObject("Paste");
                    break;
                case "print":
                    obj = rm.GetObject("Print");
                    break;
                case "filter":
                    obj = rm.GetObject("Filter");
                    break;
                case "find":
                    obj = rm.GetObject("Find");
                    break;
                case "redo":
                    obj = rm.GetObject("Redo");
                    break;
                case "undo":
                    obj = rm.GetObject("Undo");
                    break;
                case "preview":
                    obj = rm.GetObject("Preview");
                    break;
                case "pie":
                    obj = rm.GetObject("Pie");
                    break;
                case "save":
                    obj = rm.GetObject("Save");
                    break;
                case "exportToDoc":
                    obj = rm.GetObject("ExportToDOC");
                    break;
                case "bookmark":
                    obj = rm.GetObject("Bookmark");
                    break;
                case "table":
                    obj = rm.GetObject("Table");
                    break;
                case "monthView":
                    obj = rm.GetObject("MonthView");
                    break;
                case "lock":
                    obj = rm.GetObject("lockL");
                    break;
                case "complete":
                    obj = rm.GetObject("HistoryItem");//("completeL");
                    break;
                case "stop":
                    obj = rm.GetObject("stopL");
                    break;
                case "template":
                    obj = rm.GetObject("Template");
                    break;
                case "ebill":
                    obj = rm.GetObject("Article");
                    break;
                case "previous":
                    obj = rm.GetObject("Left");
                    break;
                case "next":
                    obj = rm.GetObject("Right");
                    break;
                default:
                    obj = rm.GetObject("AddItem");
                    break;
            }
            return obj as Image;
        }
        #endregion

        #region FuncItemImage
        /// <summary>
        /// FuncItemImage
        /// </summary>
        /// <param name="funcCode"></param>
        /// <returns></returns>
        internal Image FuncItemImage(string funcName, string imgName)
        {
            Bitmap img = Properties.Resources.bg;//.l;
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Bitmap funcImg = rm.GetObject(imgName) as Bitmap;
            if (funcImg != null) g.DrawImage(funcImg, 24, 10);//22, 10);
            Point p = new Point(10, 50);
            if (funcName.Length <= 5)
            {
                if (funcName.Length < 4) p = new Point(15, 50);
                else if (funcName.Length == 5) p = new Point(2, 50);
                g.DrawString(funcName, new Font("宋体", 12f, FontStyle.Regular), new Pen(Color.White).Brush, p);
            }
            else
            {
                p = new Point(2, 42);
                g.DrawString(funcName.Substring(0, 5), new Font("宋体", 12f, FontStyle.Regular), new Pen(Color.White).Brush, p);
                p = new Point(2, 58);
                g.DrawString(funcName.Substring(5), new Font("宋体", 12f, FontStyle.Regular), new Pen(Color.White).Brush, p);
            }

            return img;
        }
        #endregion

        #region LoadForm
        /// <summary>
        /// LoadForm
        /// </summary>
        /// <param name="vo"></param>
        internal void LoadForm(EntityAccount vo)
        {
            try
            {
                if (vo == null || string.IsNullOrEmpty(vo.FuncCode)) return;
                this.IsLoadform = true;
                string strPath = Application.StartupPath + "\\" + vo.FuncFile.Trim();
                string className = vo.FuncCode.Trim();
                if (className.IndexOf("|") > 0) className = className.Substring(0, className.IndexOf("|"));
                Assembly objAsm = Assembly.LoadFrom(strPath);
                object obj = objAsm.CreateInstance(className, true);
                Type objType = obj.GetType();
                MethodInfo objMth;

                int n = 0;
                foreach (Form frm in Viewer.MdiChildren)
                {
                    if (frm is frmBaseMdi)
                    {
                        n++;
                    }
                }
                if (n >= 10)
                {
                    DialogBox.Msg("界面设定为最多打开10个，请关闭暂时不需要的界面。", MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(vo.OperName)) vo.OperName = "Show";
                string strMethod = vo.OperName;
                object[] objParams = null;
                int intIndex = strMethod.IndexOf("(");
                if (intIndex != -1) //带参
                {
                    string strParam = strMethod.Substring(intIndex + 1, strMethod.Length - intIndex - 2);
                    objParams = strParam.Split(',');
                    strMethod = strMethod.Substring(0, intIndex);
                    objMth = objType.GetMethod(strMethod);
                }
                else
                {
                    objMth = objType.GetMethod(vo.OperName, new Type[0]);
                }
                if (objMth == null)
                {
                    DialogBox.Msg("自动创建模块失败。", MessageBoxIcon.Exclamation, Viewer);
                }
                if (obj is Form)
                {
                    if (!FormExisted(obj.GetType(), vo.OperName))
                    {
                        //if (vo.OperName.ToLower().Equals("show")) uiHelper.BeginLoading(Viewer);
                        if (vo.OperName.ToLower().Contains("show")) uiHelper.BeginLoading(Viewer);
                        GetFormFuncButton(vo.FuncCode);
                        ((Form)obj).AccessibleName = vo.FuncCode;
                        ((Form)obj).AccessibleDescription = vo.FuncName;
                        MakeMdiForm(obj, vo.OperName);
                        objMth.Invoke(obj, objParams);
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException(e);
                DialogBox.Msg(e.Message, MessageBoxIcon.Exclamation, Viewer);
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
                this.IsLoadform = false;
            }
        }
        #endregion

        internal bool FormExisted(Type objType, string operName)
        {
            string strExist = string.Empty;
            string strCurr = string.Empty;
            foreach (Form frm in Viewer.MdiChildren)
            {
                if (frm is frmBaseMdi)
                {
                    strExist = frm.GetType().Name + ((frmBaseMdi)frm).FormOperName;
                    strCurr = objType.Name + operName;
                }
                else
                {
                    strExist = frm.GetType().Name;
                    strCurr = objType.Name;
                }

                if (strExist == strCurr)
                {
                    frm.Activate();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 设置窗体mdi关系
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="operName"></param>
        internal void MakeMdiForm(object obj, string operName)
        {
            Form frm = obj as Form;
            Type objType = obj.GetType();

            if (frm is frmBaseMdi)
            {
                ((frmBaseMdi)frm).FormOperName = operName;
            }

            if (operName.ToLower() == "showdialog")
            {
                frm.WindowState = FormWindowState.Normal;
            }
            else
            {
                frm.MdiParent = Viewer;
                if (frm is frmBaseMdi)
                {
                    ((frmBaseMdi)frm).ReflectOpen = true;
                }
            }
        }
        #endregion

        #region ToolbarOrder
        /// <summary>
        /// ToolbarOrder
        /// </summary>
        internal void ToolbarOrder()
        {
            EntityFunction vo = null;
            List<EntityFunction> lstFunc = new List<EntityFunction>();

            vo = new EntityFunction();
            vo.Funccode = "addItem";
            vo.Funcname = "增加";
            lstFunc.Add(vo);

            vo = new EntityFunction();
            vo.Funccode = "deleteItem";
            vo.Funcname = "删除";
            lstFunc.Add(vo);

            vo = new EntityFunction();
            vo.Funccode = "save";
            vo.Funcname = "保存";
            lstFunc.Add(vo);

            vo = new EntityFunction();
            vo.Funccode = "copy";
            vo.Funcname = "复制";
            lstFunc.Add(vo);

            vo = new EntityFunction();
            vo.Funccode = "template";
            vo.Funcname = "模板";
            lstFunc.Add(vo);

            vo = new EntityFunction();
            vo.Funccode = "exportToDoc";
            vo.Funcname = "导出";
            lstFunc.Add(vo);

            vo = new EntityFunction();
            vo.Funccode = "print";
            vo.Funcname = "打印";
            lstFunc.Add(vo);

            vo = new EntityFunction();
            vo.Funccode = "close";
            vo.Funcname = "关闭";
            lstFunc.Add(vo);

            SetToolbar(lstFunc);
        }

        /// <summary>
        /// ToolbarRecipe
        /// </summary>
        internal void ToolbarRecipe()
        {
            EntityFunction vo = null;
            List<EntityFunction> lstFunc = new List<EntityFunction>();

            vo = new EntityFunction();
            vo.Funccode = "addItem";
            vo.Funcname = "增加";
            lstFunc.Add(vo);

            vo = new EntityFunction();
            vo.Funccode = "deleteItem";
            vo.Funcname = "删除";
            lstFunc.Add(vo);

            vo = new EntityFunction();
            vo.Funccode = "save";
            vo.Funcname = "保存";
            lstFunc.Add(vo);

            vo = new EntityFunction();
            vo.Funccode = "copy";
            vo.Funcname = "复制";
            lstFunc.Add(vo);

            vo = new EntityFunction();
            vo.Funccode = "close";
            vo.Funcname = "关闭";
            lstFunc.Add(vo);

            SetToolbar(lstFunc);
        }

        #endregion

        #region SetToolbar
        /// <summary>
        /// SetToolbar
        /// </summary>
        /// <param name="lstFunc"></param>
        internal void SetToolbar(List<EntityFunction> lstFunc)
        {
            Viewer.rpgToolbarForm.ItemLinks.Clear();
            DevExpress.XtraBars.BarButtonItem barItem = null;
            foreach (EntityFunction code in lstFunc)
            {
                barItem = new DevExpress.XtraBars.BarButtonItem();
                barItem.Name = code.Funcid.ToString();//.Funccode;
                barItem.Caption = code.Funcname;
                barItem.LargeGlyph = ToolbarItemImage(code.Funccode);
                barItem.ItemClick += new ItemClickEventHandler(bbiItem_ItemClick);
                Viewer.rpgToolbarForm.ItemLinks.Add(barItem);
            }
            Viewer.rpgToolbarForm.Visible = lstFunc.Count > 0 ? true : false;
        }

        #endregion

        #region Click.Event
        /// <summary>
        /// bbiItem_ItemClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item != null && e.Item.Tag != null)
            {
                LoadForm(e.Item.Tag as EntityAccount);
            }
        }
        #endregion

        #region Navigation
        /// <summary>
        /// Navigation
        /// </summary>
        internal void Navigation()
        {
            if (this.DefaultModule != null)
            {
                this.LoadForm(this.DefaultModule);
            }
        }
        #endregion

        #region ChangePwd
        /// <summary>
        /// ChangePwd
        /// </summary>
        internal void ChangePwd()
        {
            using (frmPassWord frm = new frmPassWord())
            {
                frm.ShowDialog();
            }
        }
        #endregion

        #region Logout
        /// <summary>
        /// isLogout
        /// </summary>
        bool isLogout { get; set; }
        /// <summary>
        /// Logout
        /// </summary>
        internal void Logout()
        {
            isLogout = false;
            if (DialogBox.Msg("请确认是否注销当前用户？", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                isLogout = true;
                Viewer.Close();
                System.Diagnostics.Process.Start(Application.StartupPath + @"\hms.exe", "relogin");
            }
        }
        #endregion

        #region Halt
        /// <summary>
        /// isHalt
        /// </summary>
        bool isHalting { get; set; }
        /// <summary>
        /// Halt
        /// </summary>
        internal void Halt(FormClosingEventArgs e)
        {
            if (isHalting) return;
            isHalting = true;
            if (isLogout)
            {
                try
                {
                    if (Viewer.MdiChildren.Length > 0)
                    {
                        foreach (Form frm in Viewer.MdiChildren)
                        {
                            frm.Close();
                            if (frm is frmBase && ((frmBase)frm).isCancelExit)
                            {
                                return;
                            }
                        }
                    }
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    DialogBox.Msg(ex.Message);
                    e.Cancel = true;
                }
                finally
                {
                    isHalting = false;
                }
            }
            else
            {
                if (DialogBox.Msg("请确认是否退出系统？", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (Viewer.MdiChildren.Length > 0)
                        {
                            foreach (Form frm in Viewer.MdiChildren)
                            {
                                frm.Close();
                                if (frm is frmBase && ((frmBase)frm).isCancelExit)
                                {
                                    return;
                                }
                            }
                        }
                        Application.Exit();
                    }
                    catch (Exception ex)
                    {
                        DialogBox.Msg(ex.Message);
                        e.Cancel = true;
                    }
                    finally
                    {
                        isHalting = false;
                    }
                }
                else
                {
                    isHalting = false;
                    if (e != null) e.Cancel = true;
                }
            }
        }
        #endregion

        #region SendMessage

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        static extern int SendMessage(
        int hWnd,  //  handle  to  destination  window  
        int Msg,  //  message  
        int wParam,  //  first  message  parameter  
        ref COPYDATASTRUCT lParam  //  second  message  parameter  
        );
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        static extern int FindWindow(string lpClassName, string lpWindowName);

        void SendMessage(string funcCode)
        {
            //int WINDOW_HANDLER = FindWindow(null, Viewer.ExternalWinName);
            //if (WINDOW_HANDLER == 0)
            //{
            //    //MessageBox.Show("iCare未找到");
            //    SendKeys.Send("%{Tab}");
            //}
            //else
            //{
            //    SendKeys.Send("%{Tab}"); 
            //}
        }

        #endregion

    }
}
