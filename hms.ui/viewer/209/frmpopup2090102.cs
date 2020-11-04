using Common.Controls;
using Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;
using Hms.Entity;
using weCare.Core.Utils;

namespace Hms.Ui
{
    /// <summary>
    /// 自定义问卷
    /// </summary>
    public partial class frmPopup2090102 : frmBasePopup
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmPopup2090102()
        {
            InitializeComponent();
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="_qnVo"></param>
        public frmPopup2090102(EntityDicQnMain _qnVo)
        {
            InitializeComponent();
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.QnVo = _qnVo;
        }
        #endregion

        #region var/property

        public EntityDicQnMain QnVo { get; set; }

        List<EntityQnSetting> DataSource { get; set; }
        List<EntityDicQnSetting> lstDicQnSetting { get; set; }
        List<EntityDicQnSummary> lstQnSummary { get; set; }

        public bool IsRequireRefresh { get; set; }

        #endregion

        #region method

        #region init
        /// <summary>
        /// init
        /// </summary>
        void Init()
        {
            try
            {
                uiHelper.BeginLoading(this);
                List<EntityDicQnDetail> lstDetails = null;
                using (ProxyHms proxy = new ProxyHms())
                {
                    DataSource = proxy.Service.GetQnSettingFromSummary();
                    lstQnSummary = proxy.Service.GetDicQnSummary();
                    lstDetails = proxy.Service.GetQnDetail(this.QnVo.qnId);
                    if (DataSource != null && DataSource.Count > 0 && lstDetails != null && lstDetails.Count > 0)
                    {
                        foreach (EntityDicQnDetail item in lstDetails)
                        {
                            if (DataSource.Any(t => t.fieldId == item.fieldId))
                            {
                                (DataSource.FirstOrDefault(t => t.fieldId == item.fieldId)).isCheck = 1;
                            }
                        }
                    }
                }
                this.gridControl.DataSource = DataSource;

                if (this.QnVo != null)
                {
                    this.txtQnName.Text = this.QnVo.qnName;
                    this.txtQnDesc.Text = this.QnVo.qnDesc;
                    this.cboStatus.SelectedIndex = this.QnVo.status;
                }
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region save 
        /// <summary>
        /// Save
        /// </summary>
        void Save()
        {
            EntityDicQnMain vo = new EntityDicQnMain();
            vo.qnName = this.txtQnName.Text.Trim();
            vo.qnDesc = this.txtQnDesc.Text.Trim();
            vo.status = this.cboStatus.SelectedIndex;
            vo.classId = 2;     // 自定义问卷
            vo.creatorId = GlobalLogin.objLogin.EmpNo;
            vo.creatDate = DateTime.Now;
            if (this.QnVo != null && this.QnVo.qnId > 0)
            {
                vo.qnId = this.QnVo.qnId;
            }
            // 明细缓
            this.gridView.CloseEditor();
            lstDicQnSetting = new List<EntityDicQnSetting>();
            List<EntityQnSetting> data = this.gridControl.DataSource as List<EntityQnSetting>;
            List<EntityDicQnDetail> lstDet = new List<EntityDicQnDetail>();
            foreach (EntityQnSetting item in data)
            {
                if (item.isCheck == 1)
                {
                    lstDet.Add(new EntityDicQnDetail() { fieldId = item.fieldId,qnId=item.qnId });
                    EntityDicQnSummary qnSummaryVo = lstQnSummary.Find(r=>r.fieldId == item.fieldId );
                    if(qnSummaryVo != null)
                    {
                        EntityDicQnSetting setVo = new EntityDicQnSetting();
                        setVo = Function.MapperToModel(setVo, qnSummaryVo);
                        setVo.qnId = item.qnId;
                        lstDicQnSetting.Add(setVo);
                    }

                    List<EntityDicQnSummary> lstSub = lstQnSummary.FindAll(r => r.parentFieldId == item.fieldId);
                    if(lstSub != null)
                    {
                        foreach(var summaryVo in lstSub)
                        {
                            EntityDicQnSetting setVo = new EntityDicQnSetting();
                            setVo = Function.MapperToModel(setVo, summaryVo);
                            setVo.qnId = item.qnId;
                            lstDicQnSetting.Add(setVo);
                        }
                    }
                }
            }

            decimal qnId = 0;
            using (ProxyHms proxy = new ProxyHms())
            {
                if (proxy.Service.SaveQNnormal(vo, lstDet, out qnId,null, lstDicQnSetting) > 0)
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

        private void frmPopup2090102_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Save();
        }

        private void blbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uiHelper.Print(this.gridControl);
        }

        private void blbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uiHelper.ExportToXls(this.gridView);
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion

    }

}
