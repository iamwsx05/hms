using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Hms.Ui
{
    public partial class frm20602 : frmBaseMdi
    {
        public frm20602()
        {
            InitializeComponent();
        }


        #region override

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        public override void New()
        {
            frmpopup2060201 frm = new frmpopup2060201();
            frm.ShowDialog();
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑
        /// </summary>
        public override void Edit()
        {
            
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
            
        }
        #endregion

        #endregion

    }
}
