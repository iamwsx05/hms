using Common.Controls;
using Common.Entity;
using Hms.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace Hms.Ui
{
    /// <summary>
    /// 普通问卷
    /// </summary>
    public partial class frmPopup2090101 : frmBasePopup
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmPopup2090101()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="_qnVo"></param>
        public frmPopup2090101(EntityDicQnMain _qnVo)
        {
            InitializeComponent();
            this.QnVo = _qnVo;
        }
        #endregion

        #region var/property

        public EntityDicQnMain QnVo { get; set; }

        int idx = 0;
        Dictionary<int, IQuest> dicQuestCtrl { get; set; }

        public bool IsRequireRefresh { get; set; }

        #endregion

        #region method

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            try
            {
                List<EntityDicQnDetail> lstDetails = null;
                List<EntityDicQnSetting> lstQnSettings = null;
                if (this.QnVo != null)
                {
                    this.txtQNname.Text = this.QnVo.qnName;
                    this.txtQNdesc.Text = this.QnVo.qnDesc;
                    this.cboStatus.SelectedIndex = this.QnVo.status;
                    lstDetails = (new ProxyHms().Service.GetQnDetail(this.QnVo.qnId));
                    lstQnSettings = (new ProxyHms().Service.GetDicQnSetting(this.QnVo.qnId));
                }

                uiHelper.BeginLoading(this);
                dicQuestCtrl = new Dictionary<int, IQuest>();
                dicQuestCtrl.Add(0, new Hms.Ui.Quest01());
                dicQuestCtrl.Add(1, new Hms.Ui.Quest02());
                dicQuestCtrl.Add(2, new Hms.Ui.Quest03());
                dicQuestCtrl.Add(3, new Hms.Ui.Quest04());
                dicQuestCtrl.Add(4, new Hms.Ui.Quest05());
                dicQuestCtrl.Add(5, new Hms.Ui.Quest06());
                dicQuestCtrl.Add(6, new Hms.Ui.Quest07());
                dicQuestCtrl.Add(7, new Hms.Ui.Quest08());
                dicQuestCtrl.Add(8, new Hms.Ui.Quest09());
                dicQuestCtrl.Add(9, new Hms.Ui.Quest10());
                foreach (KeyValuePair<int, IQuest> kvp in dicQuestCtrl)
                {
                    kvp.Value.SetQnCtrls(lstDetails);
                }

                AddQuestCtrl();

            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region AddQuestCtrl
        /// <summary>
        /// AddQuestCtrl
        /// </summary>
        void AddQuestCtrl()
        {
            try
            {
                uiHelper.BeginLoading(this);
                this.plUserCtrl.Controls.Clear();
                this.plUserCtrl.Controls.Add(dicQuestCtrl[idx] as UserControl);
                this.plUserCtrl.Height = (dicQuestCtrl[idx] as UserControl).Height;
                this.plContent.Height = this.plTitle.Height + this.plUserCtrl.Height + 50;

            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        void Save()
        {
            EntityDicQnMain vo = new EntityDicQnMain();
            vo.qnName = this.txtQNname.Text.Trim();
            vo.qnDesc = this.txtQNdesc.Text.Trim();
            vo.status = this.cboStatus.SelectedIndex;
            vo.classId = 1;     // 普通问卷
            vo.creatorId = GlobalLogin.objLogin.EmpNo;
            vo.creatDate = DateTime.Now;
            if (this.QnVo != null && this.QnVo.qnId > 0)
            {
                vo.qnId = this.QnVo.qnId;
            }
            // 明细缓
            List<EntityDicQnDetail> lstDet = new List<EntityDicQnDetail>();
            List<EntityDicQnCtlLocation> lstLocation = new List<EntityDicQnCtlLocation>();
            List<EntityDicQnSetting> lstSettings = new List<EntityDicQnSetting>();
            foreach (KeyValuePair<int, IQuest> kvp in dicQuestCtrl)
            {
                lstDet.AddRange(kvp.Value.GetQnCtrls());
                lstLocation.Add(kvp.Value.GetQnCtrlsLocation());
                lstSettings.AddRange(kvp.Value.GetQnSettings());
            }

            decimal qnId = 0;
            using (ProxyHms proxy = new ProxyHms())
            {
                if (proxy.Service.SaveQNnormal(vo, lstDet, out qnId,lstLocation, lstSettings) > 0)
                {
                    if (this.QnVo == null)
                    {
                        this.QnVo = new EntityDicQnMain() { qnId = qnId };
                    }
                    this.IsRequireRefresh = true;
                    DialogBox.Msg("保存问卷成功！");
                }
                else
                {
                    DialogBox.Msg("保存问卷失败。");
                }
            }
        }
        #endregion

        #endregion

        #region event

        private void frmPopup2090101_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void blbiPrePage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (idx > 0)
            {
                --idx;
                AddQuestCtrl();
            }
        }

        private void blbiNextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (idx < 9)
            {
                ++idx;
                AddQuestCtrl();
            }
        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Save();
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #region LableClick Event

        private void lblQ01_Click(object sender, EventArgs e)
        {
            idx = 0;
            AddQuestCtrl();
        }

        private void lblQ02_Click(object sender, EventArgs e)
        {
            idx = 1;
            AddQuestCtrl();
        }

        private void lblQ03_Click(object sender, EventArgs e)
        {
            idx = 1;
            AddQuestCtrl();
        }

        private void lblQ04_Click(object sender, EventArgs e)
        {
            idx = 2;
            AddQuestCtrl();
        }

        private void lblQ05_Click(object sender, EventArgs e)
        {
            idx = 2;
            AddQuestCtrl();
        }

        private void lblQ06_Click(object sender, EventArgs e)
        {
            idx = 3;
            AddQuestCtrl();
        }

        private void lblQ07_Click(object sender, EventArgs e)
        {
            idx = 3;
            AddQuestCtrl();
        }

        private void lblQ08_Click(object sender, EventArgs e)
        {
            idx = 4;
            AddQuestCtrl();
        }

        private void lblQ09_Click(object sender, EventArgs e)
        {
            idx = 4;
            AddQuestCtrl();
        }

        private void lblQ10_Click(object sender, EventArgs e)
        {
            idx = 5;
            AddQuestCtrl();
        }

        private void lblQ11_Click(object sender, EventArgs e)
        {
            idx = 6;
            AddQuestCtrl();
        }

        private void lblQ12_Click(object sender, EventArgs e)
        {
            idx = 7;
            AddQuestCtrl();

        }

        private void lblQ13_Click(object sender, EventArgs e)
        {
            idx = 8;
            AddQuestCtrl();
        }

        private void lblQ14_Click(object sender, EventArgs e)
        {
            idx = 9;
            AddQuestCtrl();
        }
        #endregion

        #endregion

    }

    #region IQuest

    public interface IQuest
    {
        List<EntityDicQnDetail> GetQnCtrls();
        void SetQnCtrls(List<EntityDicQnDetail> lstCtrls);
        EntityDicQnCtlLocation GetQnCtrlsLocation();
        List<EntityDicQnSetting> GetQnSettings();
        //void SetQnSettings(List<EntityDicQnSetting> lstQnsettings);
    }

    #endregion

}
