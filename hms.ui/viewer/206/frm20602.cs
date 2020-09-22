using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using weCare.Core.Utils;
using Hms.Entity;

namespace Hms.Ui
{
    public partial class frm20602 : frmBaseMdi
    {
        public frm20602()
        {
            InitializeComponent();
        }

        #region var
        EntityDietRecord dietRecord { get; set; }
        #endregion

        #region override

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        public override void New()
        {
            frmPopup2060201 frm = new frmPopup2060201(null);
            frm.ShowDialog();
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑
        /// </summary>
        public override void Edit()
        {
            dietRecord = GetRowObject();

            if(dietRecord != null)
            {
                frmPopup2060201 frm = new frmPopup2060201(dietRecord);
                frm.ShowDialog();
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        public override void Delete()
        {
            
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        public override void Search()
        {
            Query();
        }
        #endregion

        #endregion

        #region methods

        #region Init
        void Init()
        {
            DateTime dateTime = DateTime.Now;
            this.dteBegin.Text = dateTime.AddDays(-30).ToString("yyyy-MM-dd") ;
            this.dteEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Query(); 
        }
        #endregion

        #region Query
        /// <summary>
        /// 
        /// </summary>
        void Query()
        {
            List<EntityParm> dicParm = new List<EntityParm>();
            string beginDate = string.Empty;
            string endDate = string.Empty;
            beginDate = dteBegin.Text.Trim();
            endDate = dteEnd.Text.Trim();

            if (beginDate != string.Empty && endDate != string.Empty)
            {
                if (Function.Datetime(beginDate + " 00:00:00") > Function.Datetime(endDate + " 00:00:00"))
                {
                    DialogBox.Msg("开始时间不能大于结束时间。");
                    return;
                }
                dicParm.Add(Function.GetParm("queryDate", beginDate + "|" + endDate));
            }

            if (this.txtClientName.Text.Trim() != string.Empty)
            {
                dicParm.Add(Function.GetParm("clientName", this.txtClientName.Text.Trim()));
            }
            if (this.txtClientNo.Text.Trim() != string.Empty)
            {
                dicParm.Add(Function.GetParm("clientNo", this.txtClientNo.Text.Trim()));
            }

            try
            {
                uiHelper.BeginLoading(this);
                if (dicParm.Count > 0)
                {
                    using (ProxyHms proxy = new ProxyHms())
                    {
                        this.gcData.DataSource = proxy.Service.GetDietRecords(dicParm);
                        this.gcData.RefreshDataSource();
                    }
                }
                else
                {
                    DialogBox.Msg("请输入查询条件。");
                }
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal EntityDietRecord GetRowObject()
        {
            EntityDietRecord vo = null;
            if (this.gvData.FocusedRowHandle >= 0)
                vo = this.gvData.GetRow(this.gvData.FocusedRowHandle) as EntityDietRecord;
            return vo;
        }
        #endregion

        #region events
        private void frm20602_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void gcData_DoubleClick(object sender, EventArgs e)
        {
            this.Edit();
        }
        #endregion
    }
}
