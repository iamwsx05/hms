using Common.Entity;
using Common.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.BandedGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Utils;
using DevExpress.XtraPrinting;

namespace Common.Controls
{
    /// <summary>
    /// uiHelper
    /// </summary>
    public class uiHelper
    {
        #region 属性

        /// <summary>
        /// 单元格背景色
        /// </summary>
        public static Color CellBackColor = Color.FromArgb(184, 232, 171);

        /// <summary>
        /// clrCustomRow
        /// </summary>
        private static Color _GridRowCustomColor = Color.FromArgb(227, 239, 255); //Color.FromArgb(169, 178, 202);//Color.FromArgb(169, 178, 202);

        /// <summary>
        /// GridRowCustomColor
        /// </summary>
        public static Color GridRowCustomColor
        {
            get
            {
                return _GridRowCustomColor;
            }
        }

        public static frmBase MdiParent { get; set; }

        #endregion

        #region 外观
        /// <summary>
        /// 外观
        /// </summary>
        /// <param name="editor"></param>
        public static void SetSystemLookAndFeel(DevExpress.XtraEditors.Container.EditorContainer editor)
        {
            if (editor is DevExpress.XtraGrid.GridControl)
            {
                ((DevExpress.XtraGrid.GridControl)editor).LookAndFeel.UseDefaultLookAndFeel = false;
                ((DevExpress.XtraGrid.GridControl)editor).LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                ((DevExpress.XtraGrid.GridControl)editor).LookAndFeel.SkinName = "iCare Skin";

            }
            else if (editor is DevExpress.XtraTreeList.TreeList)
            {
                ((DevExpress.XtraTreeList.TreeList)editor).LookAndFeel.UseDefaultLookAndFeel = false;
                ((DevExpress.XtraTreeList.TreeList)editor).LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                ((DevExpress.XtraTreeList.TreeList)editor).LookAndFeel.SkinName = "iCare Skin";
            }
        }

        /// <summary>
        /// 外观
        /// </summary>
        /// <param name="editor"></param>
        public static void SetSystemLookAndFeel(DevExpress.XtraEditors.BaseEdit editor)
        {
            editor.LookAndFeel.UseDefaultLookAndFeel = false;
            editor.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            editor.LookAndFeel.SkinName = "iCare Skin";
        }

        /// <summary>
        /// 外观
        /// </summary>
        /// <param name="view"></param>
        public static void SetViewAppearance(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView view)
        {
            view.Appearance.HideSelectionRow.BackColor = uiHelper.GridRowCustomColor;
            view.Appearance.HideSelectionRow.ForeColor = Color.Black;
            view.Appearance.SelectedRow.BackColor = uiHelper.GridRowCustomColor;
            view.Appearance.SelectedRow.ForeColor = Color.Black;
            view.Appearance.FocusedRow.BackColor = uiHelper.GridRowCustomColor;
            view.Appearance.FocusedRow.ForeColor = Color.Black;
            view.Appearance.FocusedCell.BackColor = uiHelper.CellBackColor;
            view.Appearance.FocusedCell.ForeColor = Color.Black;
            view.RowHeight = 26;
            view.IndicatorWidth = 35;
            view.FixedLineWidth = 0;
        }

        /// <summary>
        /// 外观
        /// </summary>
        /// <param name="view"></param>
        public static void SetViewAppearance(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            view.Appearance.HideSelectionRow.BackColor = uiHelper.GridRowCustomColor;
            view.Appearance.HideSelectionRow.ForeColor = Color.Black;
            view.Appearance.SelectedRow.BackColor = uiHelper.GridRowCustomColor;
            view.Appearance.SelectedRow.ForeColor = Color.Black;
            view.Appearance.FocusedRow.BackColor = uiHelper.GridRowCustomColor;
            view.Appearance.FocusedRow.ForeColor = Color.Black;
            view.Appearance.FocusedCell.BackColor = uiHelper.CellBackColor;
            view.Appearance.FocusedCell.ForeColor = Color.Black;
            view.RowHeight = 26;
            view.IndicatorWidth = 35;
            view.FixedLineWidth = 0;
        }

        #endregion

        #region 等待动画界面
        /// <summary>
        /// Loading Form
        /// </summary>
        private static frmAnimation frmAniLoading = null;

        /// <summary>
        /// 开始Loading(动画)
        /// </summary>
        public static void BeginLoading(frmBase frm)
        {
            Application.DoEvents();
            //if (frmAniLoading == null)
            //{
            //    frmAniLoading = new frmAnimation();
            //    frmAniLoading.Height = 25;
            //}

            //frmAniLoading.TopLevel = true;
            //frmAniLoading.Show();

            //if (frm.Height > 700)
            //{
            frm.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            frm.marqueeProgressBarControl.Location = new Point((frm.Width - frm.marqueeProgressBarControl.Width) / 2, frm.Height / 2 - 30);
            frm.BeginLoading();
            Application.DoEvents();
            //}
        }

        /// <summary>
        /// 结束Loading(动画)
        /// </summary>
        public static void CloseLoading(frmBase frm)
        {
            //if (frmAniLoading != null)
            //{
            //    frmAniLoading.Close();
            //    frmAniLoading = null;
            //}
            //Application.DoEvents();

            frm.CloseLoading();
            Application.DoEvents();
            frm.Cursor = Cursors.Default;
        }

        #endregion

        #region Chen

        // Fields
        private static Font thisFont = new Font("宋体", 9.5f);

        // Methods
        private static void GetKeyIdByNode(TreeListNode tn, ref List<int> lstKeyId)
        {
            for (int i = 0; i < tn.Nodes.Count; i++)
            {
                TreeListNode node = tn.Nodes[i];
                if (!lstKeyId.Contains(node.Id))
                {
                    lstKeyId.Add(node.Id);
                    GetKeyIdByNode(node, ref lstKeyId);
                }
            }
        }
        /// <summary>
        /// 取所有可见的节点id
        /// </summary>
        public static List<int> GetKeyIdByVisibleIndex(TreeList tv)
        {
            List<int> lstKeyId = new List<int>();
            for (int i = 0; tv.GetNodeByVisibleIndex(i) != null; i++)
            {
                TreeListNode nodeByVisibleIndex = tv.GetNodeByVisibleIndex(i);
                if (!lstKeyId.Contains(nodeByVisibleIndex.Id))
                {
                    lstKeyId.Add(nodeByVisibleIndex.Id);
                    GetKeyIdByNode(nodeByVisibleIndex, ref lstKeyId);
                }
            }
            return lstKeyId;
        }

        public static void SetGridCol(GridView gv, string[] visibleFields, string[] visibleCaption, int[] visibleWidth)
        {
            gv.Columns.Clear();
            for (int i = 0; i < visibleFields.Length; i++)
            {
                GridColumn column = new GridColumn
                {
                    FieldName = visibleFields[i],
                    Caption = visibleCaption[i],
                    Width = visibleWidth[i],
                    Visible = true,
                    VisibleIndex = i
                };
                column.AppearanceHeader.Options.UseFont = true;
                column.AppearanceHeader.Font = thisFont;
                column.AppearanceCell.Options.UseFont = true;
                column.AppearanceCell.Font = thisFont;
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.AllowGroup = DefaultBoolean.False;
                column.OptionsColumn.AllowIncrementalSearch = false;
                column.OptionsFilter.AllowFilter = false;
                gv.Columns.Add(column);
            }
        }

        public static void SetGridCol(TreeList gv, string[] visibleFields, string[] visibleCaption, int[] visibleWidth)
        {
            SetGridColOnly(gv, visibleFields, visibleCaption, visibleWidth);
            gv.ExpandAll();
        }

        public static void SetGridCol(TreeList gv, string[] visibleFields, string[] visibleCaption, int[] visibleWidth, bool expandAll)
        {
            SetGridColOnly(gv, visibleFields, visibleCaption, visibleWidth);
            if (expandAll)
            {
                gv.ExpandAll();
            }
        }

        private static void SetGridColOnly(TreeList gv, string[] visibleFields, string[] visibleCaption, int[] visibleWidth)
        {
            gv.Columns.Clear();
            TreeListColumn[] columns = new TreeListColumn[visibleFields.Length];
            for (int i = 0; i < visibleFields.Length; i++)
            {
                columns[i] = new TreeListColumn();
                columns[i].Caption = visibleCaption[i];
                columns[i].FieldName = visibleFields[i];
                columns[i].Width = visibleWidth[i];
                columns[i].Visible = true;
                columns[i].VisibleIndex = i;
                columns[i].AppearanceHeader.Options.UseFont = true;
                columns[i].AppearanceHeader.Font = thisFont;
                columns[i].AppearanceCell.Options.UseFont = true;
                columns[i].AppearanceCell.Font = thisFont;
                columns[i].OptionsColumn.AllowEdit = false;
                columns[i].OptionsColumn.AllowMove = false;
                columns[i].OptionsColumn.AllowSort = false;
                if (i == 0)
                {
                    columns[i].AppearanceCell.Options.UseTextOptions = true;
                    columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                }
                columns[i].OptionsColumn.AllowSize = true;
            }
            gv.Columns.AddRange(columns);
        }
        /// <summary>
        /// 从源表移动选择的行到目标表
        /// </summary>
        public static void MoveGridViewSelectedRow(DevExpress.XtraGrid.Views.Grid.GridView p_gvSource, DevExpress.XtraGrid.Views.Grid.GridView p_gvTarget)
        {
            if (p_gvSource.SelectedRowsCount > 0 && p_gvTarget.DataSource != null)
            {
                int[] rowHandles = p_gvSource.GetSelectedRows();
                for (int i = rowHandles.Length - 1; i >= 0; i--)
                {
                    DataRow dr = p_gvSource.GetDataRow(rowHandles[i]);
                    ((DataView)p_gvTarget.DataSource).Table.ImportRow(dr);
                    dr.Delete();
                }
            }
        }
        /// <summary>
        /// 查找目录树
        /// </summary>
        /// <param name="p_lst">目录树</param>
        /// <param name="p_strFind">查找内容</param>
        /// <param name="p_strFormat">查找的表达式格式</param>
        /// <param name="p_blnFlags">找到的标志</param>
        /// <param name="p_intStartIndex">查找开始点</param>

        public static void TreeListFind(TreeList p_lst, string p_strFind, string p_strFormat, ref bool p_blnFlags, ref int p_intStartIndex)
        {
            string strFilter = p_strFind.Trim();// .GetFilter();
            List<int> lstKeyId = GetKeyIdByVisibleIndex(p_lst);
            DataTable dt = new DataTable();
            if (typeof(DataTable) == p_lst.DataSource.GetType())
            {
                dt = (DataTable)p_lst.DataSource;
            }
            else if (typeof(DataView) == p_lst.DataSource.GetType())
            {
                dt = ((DataView)p_lst.DataSource).ToTable();
            }
            else
            {
                DialogBox.Msg("未知数据源的类型!");
                return;
            }
            DataRow[] drs = dt.Select(string.Format(p_strFormat, strFilter));

            for (; p_intStartIndex < lstKeyId.Count; p_intStartIndex++)
            {
                TreeListNode tn = p_lst.FindNodeByID(lstKeyId[p_intStartIndex]);
                DataRow dr = ((DataRowView)p_lst.GetDataRecordByNode(tn)).Row;
                if (drs.Contains(dr, DataRowComparer.Default))
                {
                    p_lst.FocusedNode = tn;
                    p_intStartIndex = p_intStartIndex + 1;
                    p_blnFlags = true;
                    return;
                }
            }
            if (p_blnFlags)
            {
                DialogBox.Msg("查找到了最后一个匹配项。");
            }
            else
            {
                DialogBox.Msg("没有找到匹配项。");
            }
            p_blnFlags = false;
            p_intStartIndex = 0;
        }

        /// <summary>
        /// 数据改变时,更改背景色
        /// (用法定义事件gridView1.RowCellStyle +=;和GridViewRowStyle配合) 
        /// </summary>
        public static void GridViewRowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                try
                {
                    DevExpress.XtraGrid.Views.Grid.GridView gridview = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                    DataRow dr = gridview.GetDataRow(e.RowHandle);
                    if (dr != null && dr.Table.Columns.Contains(e.Column.FieldName))
                    {
                        if (dr.RowState == DataRowState.Modified && !dr[e.Column.FieldName].Equals(dr[e.Column.FieldName, DataRowVersion.Original]))
                            e.Appearance.BackColor = System.Drawing.Color.OrangeRed;
                    }
                }
                catch { }
            }
        }
        /// <summary>
        /// 数据改变时,更改背景色
        /// (用法定义事件gridView1.RowStyle +=;和GridViewRowCellStyle配合)        
        /// </summary>
        public static void GridViewRowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                try
                {
                    DevExpress.XtraGrid.Views.Grid.GridView gridview = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                    DataRow dr = gridview.GetDataRow(e.RowHandle);
                    if (dr != null && dr.RowState == DataRowState.Modified)
                        e.Appearance.BackColor = System.Drawing.Color.SkyBlue;
                    else if (dr != null && dr.RowState == DataRowState.Added)
                        e.Appearance.BackColor = System.Drawing.Color.SkyBlue;
                }
                catch { }
            }
        }
        #endregion

        #region SetGridViewColumn
        /// <summary>
        /// SetGridViewColumn
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="lst"></param>
        public static void SetGridViewColumn(GridView gv, List<weCare.Core.Entity.CommViewColumn> lst)
        {
            Font font = new Font("宋体", 9.5f);

            gv.Columns.Clear();
            GridColumn[] cols = new GridColumn[lst.Count];
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i] = new GridColumn();
                cols[i].FieldName = lst[i].FieldName;
                cols[i].Caption = lst[i].Caption;
                cols[i].Width = lst[i].Width;
                cols[i].Visible = true;
                cols[i].VisibleIndex = i;
                cols[i].AppearanceHeader.Options.UseFont = true;
                cols[i].AppearanceHeader.Font = font;
                cols[i].AppearanceCell.Options.UseFont = true;
                cols[i].AppearanceCell.Font = font;
                cols[i].OptionsColumn.AllowEdit = false;
                cols[i].OptionsColumn.AllowMove = false;
                cols[i].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
                cols[i].OptionsColumn.AllowIncrementalSearch = false;
                cols[i].OptionsFilter.AllowFilter = false;
            }
            gv.Columns.AddRange(cols);
        }
        #endregion

        #region SetTreeListColumn
        /// <summary>
        /// SetTreeListColumn
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="lst"></param>
        /// <param name="isExpand"></param>
        public static void SetTreeListColumn(TreeList tr, List<weCare.Core.Entity.CommViewColumn> lst, bool isExpand)
        {
            Font font = new Font("宋体", 9.5f);

            tr.Columns.Clear();
            TreeListColumn[] cols = new TreeListColumn[lst.Count];
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i] = new TreeListColumn();
                cols[i].FieldName = lst[i].FieldName;
                cols[i].Caption = lst[i].Caption;
                cols[i].Width = lst[i].Width;
                cols[i].Visible = true;
                cols[i].VisibleIndex = i;
                cols[i].AppearanceHeader.Options.UseFont = true;
                cols[i].AppearanceHeader.Font = font;
                cols[i].AppearanceCell.Options.UseFont = true;
                cols[i].AppearanceCell.Font = font;
                cols[i].OptionsColumn.AllowEdit = false;
                cols[i].OptionsColumn.AllowMove = false;
                cols[i].OptionsColumn.AllowSort = false;

                if (i == 0)
                {
                    cols[i].AppearanceCell.Options.UseTextOptions = true;
                    cols[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                }
            }

            tr.Columns.AddRange(cols);

            if (isExpand)
            {
                tr.ExpandAll();
            }
        }
        #endregion

        #region 是否是Pycode_vchr/Wbcode_vchr字符串
        /// <summary>
        /// 是否是Pycode_vchr/Wbcode_vchr字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPyWbStr(string str)
        {
            if (str.ToLower() == "pycode" || str.ToLower() == "wbcode")
                return true;
            else
                return false;
        }
        #endregion

        #region 查找框.过滤
        /// <summary>
        /// 获取Filter
        /// </summary>
        /// <param name="DataSource"></param>
        /// <param name="p_strFilter"></param>
        /// <returns></returns>
        public static string GetFilter(DataTable DataSource, string val, string findCols)
        {
            string strFilter = string.Empty;
            List<string> lstFind = new List<string>();
            if (!string.IsNullOrEmpty(findCols))
            {
                lstFind = findCols.ToLower().Trim().Split('|').ToList();
            }
            foreach (DataColumn col in DataSource.Columns)
            {
                if (lstFind.Count > 0)
                {
                    if (lstFind.IndexOf(col.ColumnName.ToLower()) < 0)
                    {
                        continue;
                    }
                }
                if (col.DataType == typeof(Int32) || col.DataType == typeof(Decimal))
                {
                    try
                    {
                        int.Parse(val);
                        strFilter += "(" + col.ColumnName + " = " + val + ") or ";
                    }
                    catch { }
                    //strFilter += col.ColumnName + " like '%" + p_strFilter + "%' or ";
                }
                else if (col.DataType == typeof(String))
                {
                    strFilter += "(" + col.ColumnName + " like '%" + val + "%') or ";
                }
            }

            //if (!blnPy) strFilter += "(pycode like '%" + val + "%') or ";

            if (strFilter != string.Empty)
            {
                strFilter = strFilter.Substring(0, strFilter.Length - 3);
            }

            return strFilter;
        }

        /// <summary>
        /// 构造过滤数据源
        /// </summary>
        /// <param name="DataSourceSort"></param>
        /// <param name="drr"></param>
        /// <param name="DisplayColumn"></param>
        /// <returns></returns>
        public static DataTable GetFilterDataSource(DataTable DataSourceSort, DataRow[] drr, string p_strFilter, string DisplayColumn, string HideColumn, bool IsSort)
        {
            DataRow dr = null;
            DataSourceSort.Clear();

            int count = 0;

            for (int i = 0; i < drr.Length; i++)
            {
                if (count > 800) break;

                dr = DataSourceSort.LoadDataRow(drr[i].ItemArray, true);

                if (dr["pycode"].ToString() == string.Empty)
                {
                    dr["pycode"] = weCare.Core.Utils.SpellCodeHelper.GetPyCode(dr[DisplayColumn].ToString());
                }

                if (dr["pycode"].ToString().IndexOf(p_strFilter) >= 0)
                {
                    dr["sort1"] = dr["pycode"].ToString().IndexOf(p_strFilter);
                }
                else if (dr[DisplayColumn].ToString().IndexOf(p_strFilter) >= 0)
                {
                    dr["sort1"] = dr[DisplayColumn].ToString().IndexOf(p_strFilter);
                }
                else
                {
                    dr["sort1"] = 99999;
                }
                dr["sort2"] = dr[DisplayColumn].ToString().Length;

                count++;
            }

            DataView dv = new DataView(DataSourceSort);
            if (IsSort)
            {
                dv.Sort = "sort2 asc, sort1 asc";
            }

            count = 0;
            DataTable dtSource = DataSourceSort.Clone();
            foreach (DataRowView drv in dv)
            {
                if (count > 100) break;

                dtSource.LoadDataRow(drv.Row.ItemArray, true);

                count++;
            }

            dtSource.Columns.Remove("pycode");
            if (dtSource.Columns.Contains("wbcoder"))
            {
                dtSource.Columns.Remove("wbcode");
            }
            dtSource.Columns.Remove("sort1");
            dtSource.Columns.Remove("sort2");

            //if (!string.IsNullOrEmpty( HideColumn))
            //{
            //    string[] cols = HideColumn.Split('|');
            //    foreach (string col in cols)
            //    {
            //        if (dtSource.Columns.Contains(col))
            //        {
            //            dtSource.Columns.Remove(col);
            //        }
            //    }
            //}

            return dtSource;
        }
        #endregion

        #region Export

        /// <summary>
        /// Export
        /// </summary>
        /// <param name="gc"></param>
        public static void Export(DevExpress.XtraGrid.GridControl gc)
        {
            SaveFileDialog sp = new SaveFileDialog();
            sp.InitialDirectory = @"c:\";
            sp.RestoreDirectory = true;
            sp.Filter = "Xls(*.xls)|*.xls|Xlsx(*.xlsx)|*.xlsx|Html(*.html)|*.html|Mht(*.mht)|*.mht|Pdf(*.pdf)|*.pdf|Rtf(*.rtf)|*.rtf|Txt(*.txt)|*.txt";
            sp.FilterIndex = 1;
            if (sp.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    switch (sp.FilterIndex)
                    {
                        case 0:
                            gc.ExportToXls(sp.FileName);
                            break;
                        case 1:
                            gc.ExportToXlsx(sp.FileName);
                            break;
                        case 2:
                            gc.ExportToHtml(sp.FileName);
                            break;
                        case 3:
                            gc.ExportToMht(sp.FileName);
                            break;
                        case 4:
                            gc.ExportToPdf(sp.FileName);
                            break;
                        case 5:
                            gc.ExportToRtf(sp.FileName);
                            break;
                        case 6:
                            gc.ExportToText(sp.FileName);
                            break;
                        default:
                            return;
                    }
                }
                catch (Exception ex)
                {
                    DialogBox.Msg(ex.Message);
                }
            }
        }

        /// <summary>
        /// Export
        /// </summary>
        /// <param name="gv"></param>
        public static void Export(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            SaveFileDialog sp = new SaveFileDialog();
            sp.InitialDirectory = @"c:\";
            sp.RestoreDirectory = true;
            sp.Filter = "Xls(*.xls)|*.xls|Xlsx(*.xlsx)|*.xlsx|Html(*.html)|*.html|Mht(*.mht)|*.mht|Pdf(*.pdf)|*.pdf|Rtf(*.rtf)|*.rtf|Txt(*.txt)|*.txt";
            sp.FilterIndex = 1;
            if (sp.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    gv.OptionsPrint.AutoWidth = false;
                    gv.OptionsPrint.ExpandAllGroups = true;
                    gv.OptionsPrint.ExpandAllDetails = true;

                    switch (sp.FilterIndex)
                    {
                        case 0:
                            DevExpress.XtraPrinting.XlsExportOptions opts = new DevExpress.XtraPrinting.XlsExportOptions();
                            opts.ExportHyperlinks = true;
                            opts.RawDataMode = true;
                            opts.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                            gv.ExportToXls(sp.FileName);
                            break;
                        case 1:
                            gv.ExportToXlsx(sp.FileName);
                            break;
                        case 2:
                            gv.ExportToHtml(sp.FileName);
                            break;
                        case 3:
                            gv.ExportToMht(sp.FileName);
                            break;
                        case 4:
                            gv.ExportToPdf(sp.FileName);
                            break;
                        case 5:
                            gv.ExportToRtf(sp.FileName);
                            break;
                        case 6:
                            gv.ExportToText(sp.FileName);
                            break;
                        default:
                            return;
                    }
                }
                catch (Exception ex)
                {
                    DialogBox.Msg(ex.Message);
                }
            }
        }
        /// <summary>
        /// Export
        /// </summary>
        /// <param name="gv"></param>
        public static void Export(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gv)
        {
            SaveFileDialog sp = new SaveFileDialog();
            sp.InitialDirectory = @"c:\";
            sp.RestoreDirectory = true;
            sp.Filter = "Xls(*.xls)|*.xls|Xlsx(*.xlsx)|*.xlsx|Html(*.html)|*.html|Mht(*.mht)|*.mht|Pdf(*.pdf)|*.pdf|Rtf(*.rtf)|*.rtf|Txt(*.txt)|*.txt";
            sp.FilterIndex = 1;
            if (sp.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    gv.OptionsPrint.AutoWidth = false;
                    gv.OptionsPrint.ExpandAllDetails = true;
                    switch (sp.FilterIndex)
                    {
                        case 0:
                            gv.ExportToXls(sp.FileName);
                            break;
                        case 1:
                            gv.ExportToXlsx(sp.FileName);
                            break;
                        case 2:
                            gv.ExportToHtml(sp.FileName);
                            break;
                        case 3:
                            gv.ExportToMht(sp.FileName);
                            break;
                        case 4:
                            gv.ExportToPdf(sp.FileName);
                            break;
                        case 5:
                            gv.ExportToRtf(sp.FileName);
                            break;
                        case 6:
                            gv.ExportToText(sp.FileName);
                            break;
                        default:
                            return;
                    }
                }
                catch (Exception ex)
                {
                    DialogBox.Msg(ex.Message);
                }
            }
        }

        /// <summary>
        /// Export
        /// </summary>
        /// <param name="xr"></param>
        public static void Export(DevExpress.XtraReports.UI.XtraReport xr)
        {
            if (xr != null) // && xr.DataSource != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog(); //创建一个打开对话
                saveFileDialog.Filter = "Execl文档(*.xls)|*.xls|Execl文档(*.xlsx)|*.xlsx|Pdf文档(*.pdf)|*.pdf|Rtf文档(*.rtf)|*.rtf|Text文档(*.txt)|*.txt";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true; //保存对话框是否记忆上次打开的目录 
                saveFileDialog.Title = "导出";
                saveFileDialog.FileName = xr.Name;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName.Trim() == "")
                    {
                        DialogBox.Msg("请输入要保存的文件名");
                        return;
                    }
                    if (saveFileDialog.FilterIndex == 1)
                    {
                        xr.ExportToXls(saveFileDialog.FileName);
                    }
                    else if (saveFileDialog.FilterIndex == 2)
                    {
                        xr.ExportToXlsx(saveFileDialog.FileName);
                    }
                    else if (saveFileDialog.FilterIndex == 3)
                    {
                        xr.ExportToPdf(saveFileDialog.FileName);
                    }
                    else if (saveFileDialog.FilterIndex == 4)
                    {
                        xr.ExportToRtf(saveFileDialog.FileName);
                    }
                    else if (saveFileDialog.FilterIndex == 5)
                    {
                        xr.ExportToText(saveFileDialog.FileName);
                    }
                    if (DialogBox.Msg("导出成功，是否现在打开导出的文档？", MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = saveFileDialog.FileName;
                        p.Start();
                    }
                }
            }
        }

        #endregion

        #region ExportToXls
        /// <summary>
        /// ExportToXls
        /// </summary>
        /// <param name="ExportView"></param>
        public static void ExportToXls(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            ExportToXls(gv, true);
        }
        /// <summary>
        /// ExportToXls
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="isShowHeader"></param>
        public static void ExportToXls(DevExpress.XtraGrid.Views.Grid.GridView gv, bool isShowHeader)
        {
            SaveFileDialog sp = new SaveFileDialog();
            sp.InitialDirectory = @"c:\";
            sp.RestoreDirectory = true;
            sp.Filter = "Xls(*.xls)|*.xls|Xls(*.xlsx)|*.xlsx";
            sp.FilterIndex = 1;
            if (sp.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    gv.OptionsPrint.AutoWidth = false;
                    gv.OptionsPrint.PrintHeader = isShowHeader;
                    DevExpress.XtraGrid.Views.Base.BaseView ExportView = gv;
                    ExportView.ExportToXls(sp.FileName);
                }
                catch (Exception ex)
                {
                    DialogBox.Msg(ex.Message);
                }
            }
        }
        /// <summary>
        /// ExportToXls
        /// </summary>
        /// <param name="gv"></param>
        public static void ExportToXls(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gv)
        {
            ExportToXls(gv, true);
        }
        /// <summary>
        /// ExportToXls
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="isShowHeader"></param>
        public static void ExportToXls(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gv, bool isShowHeader)
        {
            SaveFileDialog sp = new SaveFileDialog();
            sp.InitialDirectory = @"c:\";
            sp.RestoreDirectory = true;
            sp.Filter = "Xls(*.xls)|*.xls";
            sp.FilterIndex = 1;
            if (sp.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    gv.OptionsPrint.AutoWidth = false;
                    gv.OptionsPrint.PrintHeader = isShowHeader;
                    DevExpress.XtraGrid.Views.Base.BaseView ExportView = gv;
                    ExportView.ExportToXls(sp.FileName);
                }
                catch (Exception ex)
                {
                    DialogBox.Msg(ex.Message);
                }
            }
        }
        #endregion

        #region Print
        /// <summary>
        /// Print
        /// </summary>
        /// <param name="gc"></param>
        public static void Print(DevExpress.XtraGrid.GridControl gc)
        {
            Print(gc, true);
        }

        public static void Print(DevExpress.XtraGrid.GridControl gc, bool isShowHeader)
        {
            ((DevExpress.XtraGrid.Views.Grid.GridView)gc.MainView).OptionsPrint.AutoWidth = false;
            ((DevExpress.XtraGrid.Views.Grid.GridView)gc.MainView).OptionsPrint.PrintHeader = isShowHeader;
            PrintingSystem print = new PrintingSystem();
            PrintableComponentLink link = new PrintableComponentLink(print);
            print.Links.Add(link);
            link.Component = gc;//这里可以是可打印的部件
            link.Landscape = true;
            string _PrintHeader = gc.Text;
            PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
            phf.Header.Content.Clear();
            phf.Header.Content.AddRange(new string[] { "", _PrintHeader, "" });
            phf.Header.Font = new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Bold);
            phf.Header.LineAlignment = BrickAlignment.Center;
            link.CreateDocument(); //建立文档
            print.PreviewFormEx.Show();//进行预览
        }
        #endregion

        #region 数字录入

        #region 公用数字编辑KeyPress事件
        /// <summary>
        /// 公用数字编辑KeyPress事件--Numeric
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CellNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back)
            {
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 公用数字编辑KeyPress事件--Int
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CellInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {
            }
            else
            {
                e.Handled = true;
            }
        }
        #endregion

        #endregion

        #region MdiParentRef
        /// <summary>
        /// MdiParentRef
        /// </summary>
        /// <param name="operName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object MdiParentRef(string operName, object[] param)
        {
            if (MdiParent != null)
            {
                MethodInfo objMth = MdiParent.GetType().GetMethod(operName);
                return objMth.Invoke(MdiParent, param);
            }
            return null;
        }
        /// <summary>
        /// MdiParentRef
        /// </summary>
        /// <param name="frmParent"></param>
        /// <param name="operName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object MdiParentRef(System.Windows.Forms.Form frmParent, string operName, object[] param)
        {
            if (frmParent != null)
            {
                MethodInfo objMth = frmParent.GetType().GetMethod(operName);
                return objMth.Invoke(frmParent, param);
            }
            return null;
        }
        #endregion

        #region 原StaticMethod

        #region 所有frmBase(HIS)Mdi子窗体暂停重绘
        /// <summary>
        /// 所有frmBase(HIS)Mdi子窗体暂停重绘
        /// </summary>
        /// <param name="frm"></param>
        public static void MdiChildrenPauseRedraw(System.Windows.Forms.Form frm)
        {
            if (frm != null)
            {
                if (frm is frmBase)
                {
                    ((frmBase)frm).PauseRedraw();
                }
            }
            if (frm.MdiParent == null)
            {
                return;
            }
            else
            {
                if (frm.MdiParent is frmBase)
                {
                    ((frmBase)frm).PauseRedraw();
                }
            }

            Form[] frms = frm.MdiParent.MdiChildren;
            foreach (Form f in frms)
            {
                if (f is frmBase)
                {
                    ((frmBase)f).PauseRedraw();
                }
            }
        }
        #endregion

        #region 所有frmBase(HIS)Mdi子窗体开始重绘
        /// <summary>
        /// 所有frmBase(HIS)Mdi子窗体开始重绘
        /// </summary>
        /// <param name="frm"></param>
        public static void MdiChildrenStartRedraw(System.Windows.Forms.Form frm)
        {
            if (frm != null)
            {
                if (frm is frmBase)
                {
                    ((frmBase)frm).StartRedraw();
                }
                return;
            }
            if (frm.MdiParent == null)
            {
                return;
            }
            else
            {
                if (frm.MdiParent is frmBase)
                {
                    ((frmBase)frm).StartRedraw();
                }
            }
            Form[] frms = frm.MdiParent.MdiChildren;
            foreach (Form f in frms)
            {
                if (f is frmBase)
                {
                    ((frmBase)f).StartRedraw();
                }
            }
        }
        #endregion

        #endregion

        #region GetConfig
        /// <summary>
        /// GetConfig
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            if (GlobalDic.DataSourceDefConfiguration == null) return string.Empty;
            if (GlobalDic.DataSourceDefConfiguration.Any(t => t.configCode.Trim() == key.Trim()))
            {
                string ruleCode = GlobalDic.DataSourceDefConfiguration.FirstOrDefault(t => t.configCode.Trim() == key.Trim()).ruleCode;
                if (string.IsNullOrEmpty(ruleCode))
                    return string.Empty;
                else
                    return ruleCode.Trim().ToUpper();
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region 当前界面
        /// <summary>
        /// 当前Mdi界面
        /// </summary>
        public static frmBase frmCurr { get; set; }
        #endregion

        #region 获取引用列信息
        /// <summary>
        /// 获取引用列信息
        /// </summary>
        /// <param name="p_strCaseCode"></param>
        /// <param name="p_strColCode"></param>
        /// <param name="p_strColContent"></param>
        /// <returns></returns>
        public static string CaseColumnContent(string p_strCaseCode, string p_strColCode, string p_strColContent)
        {
            if (GlobalCaseColumnInfo.CaseColumnInfo != null && !string.IsNullOrEmpty(p_strCaseCode) && !string.IsNullOrEmpty(p_strColCode))
            {
                if (GlobalCaseColumnInfo.CaseColumnInfo.Exists(t => t.CaseCode == p_strCaseCode && t.ColCode == p_strColCode))
                {
                    EntityCaseColumnInfo dc = GlobalCaseColumnInfo.CaseColumnInfo.SingleOrDefault(t => t.CaseCode == p_strCaseCode && t.ColCode == p_strColCode);
                    if (dc != null)
                    {
                        if (p_strColContent.StartsWith(dc.ColDesc + ":"))
                        {
                            return p_strColContent.Remove(0, (dc.ColDesc + ":").Length);
                        }
                        else if (p_strColContent.StartsWith(dc.ColDesc + "："))
                        {
                            return p_strColContent.Remove(0, (dc.ColDesc + "：").Length);
                        }
                    }
                }
            }
            return p_strColContent;
        }
        #endregion

        #region 护理记录诊断特殊打印设置
        /// <summary>
        /// 护理记录诊断特殊打印设置
        /// </summary>
        public static EntityTabDiagSetting[] TabDiagSettingArr { get; set; }
        #endregion

        #region 病历压缩、解压缩

        #region 压缩字段
        /// <summary>
        /// 压缩字段
        /// </summary>
        /// <param name="p_lstCaseData"></param>
        public static void CompressCaseData(ref List<EntityEmrData> p_lstCaseData)
        {
            if (p_lstCaseData == null || p_lstCaseData.Count == 0)
                return;

            foreach (EntityEmrData obj in p_lstCaseData)
            {
                if (obj.FieldRtf != null)
                {
                    obj.FieldRtf = Compression.Zip(obj.FieldRtf);
                }
                if (obj.FieldPrtRtf != null)
                {
                    obj.FieldPrtRtf = Compression.Zip(obj.FieldPrtRtf);
                }
            }
        }
        #endregion

        #region 压缩病历存储数据
        /// <summary>
        /// 压缩病历存储数据
        /// </summary>
        /// <param name="p_intRegisterID"></param>
        /// <param name="p_lstCaseData"></param>
        /// <param name="p_lstCaseDataTrace"></param>
        /// <param name="p_lstSignature"></param>
        public static bool CompressCaseData(string registerID, ref List<EntityEmrData> p_lstCaseData, ref List<EntityUniversalCaseRecordTrace> p_lstCaseDataTrace, ref List<EntitySignature> p_lstSignature)
        {
            if (p_lstCaseData != null && p_lstCaseData.Count > 0)
            {
                // 进修、实习医师(护士)采用密码签名
                string strP29 = GlobalParm.dicSysParameter[29];
                string strSignature = string.Empty;
                bool blnKeySign = GlobalParm.dicSysParameter[3] == "1";
                if (blnKeySign && p_lstSignature != null && p_lstSignature.Count > 0)
                {
                    string strCaseCode = p_lstCaseData[0].CaseCode;
                    StringBuilder stbXML = new StringBuilder();

                    stbXML.Append("<casesignature>");
                    stbXML.Append(System.Environment.NewLine);
                    stbXML.Append("<registerid>" + registerID + "</registerid>");
                    stbXML.Append(System.Environment.NewLine);
                    stbXML.Append("<casecode>" + strCaseCode + "</casecode>");
                    stbXML.Append(System.Environment.NewLine);
                    stbXML.Append("<cols>");
                    stbXML.Append(System.Environment.NewLine);
                    foreach (EntityEmrData obj in p_lstCaseData)
                    {
                        if (obj.FieldPrtRtf == null)
                        {
                            stbXML.Append("<col code=\"" + obj.FieldName + "\" text=\"" + obj.FieldText + "\" type=\"0\"/>");
                        }
                        else
                        {
                            stbXML.Append("<col code=\"" + obj.FieldName + "\" text=\"" + Function.ConvertByteToObject(obj.FieldPrtRtf).ToString() + "\" type=\"1\"/>");
                        }
                    }
                    stbXML.Append("</cols>");
                    stbXML.Append(System.Environment.NewLine);
                    stbXML.Append("</casesignature>");
                    stbXML.Append(System.Environment.NewLine);
                    strSignature = stbXML.ToString();
                }

                foreach (EntityEmrData obj in p_lstCaseData)
                {
                    if (obj.FieldRtf != null)
                    {
                        obj.FieldRtf = Compression.Zip(obj.FieldRtf);
                    }
                    if (obj.FieldPrtRtf != null)
                    {
                        obj.FieldPrtRtf = Compression.Zip(obj.FieldPrtRtf);
                    }
                }

                if (p_lstSignature != null && p_lstSignature.Count > 0 && GlobalParm.dicSysParameter[3] != "0")
                {
                    List<string> lstEmpID = new List<string>();
                    foreach (EntitySignature obj in p_lstSignature)
                    {
                        if (obj.serNo > 0)
                        {
                        }
                        else
                        {
                            if (lstEmpID.IndexOf(obj.empId) < 0)
                            {
                                lstEmpID.Add(obj.empId);
                            }
                        }
                    }
                    if (lstEmpID.Count > 1)
                    {
                        DialogBox.Msg("提示：保存时只能单个医生(护士)独立签名，请确认。\r\n若多个医生(护士)同时签名，请删除别的签名信息后再次保存。", MessageBoxIcon.Information, uiHelper.frmCurr);
                        return false;
                    }

                    foreach (EntitySignature obj in p_lstSignature)
                    {
                        if (obj.serNo > 0)
                        {
                        }
                        else
                        {
                            if (GlobalDic.dicEmpRole.ContainsKey(obj.empId))
                            {
                                if (GlobalDic.dicEmpRole[obj.empId].IndexOf(strP29) > 0)
                                {
                                    continue;
                                }
                            }

                            obj.signContent = Compression.Zip(CA.GetSignContent(strSignature, obj.signKeyId));
                            if (!string.IsNullOrEmpty(strSignature) && obj.signContent == null)
                            {
                                DialogBox.Msg("签名失败。", MessageBoxIcon.Information, uiHelper.frmCurr);
                                return false;
                            }
                        }
                    }
                }
            }

            if (p_lstCaseDataTrace != null && p_lstCaseDataTrace.Count > 0)
            {
                foreach (EntityUniversalCaseRecordTrace obj in p_lstCaseDataTrace)
                {
                    if (obj.ColContentRtf != null)
                    {
                        obj.ColContentRtf = Compression.Zip(obj.ColContentRtf);
                    }
                }
            }
            return true;
        }
        #endregion

        #region 解压缩病历数据

        /// <summary>
        /// 解压缩病历数据
        /// </summary>
        /// <param name="p_bytDataArr"></param>
        /// <returns></returns>
        public static byte[] UnCompressCaseData(byte[] p_bytDataArr)
        {
            return Function.ConvertObjectToByte(Compression.UnZip(p_bytDataArr));
        }
        /// <summary>
        /// 解压缩病历数据
        /// </summary>
        /// <param name="p_lstCaseData"></param>
        public static void UnCompressCaseData(ref List<EntityEmrData> p_lstCaseData)
        {
            if (p_lstCaseData == null || p_lstCaseData.Count == 0)
                return;

            foreach (EntityEmrData obj in p_lstCaseData)
            {
                if (obj.FieldRtf != null)
                {
                    obj.FieldRtf = UnCompressCaseData(obj.FieldRtf);
                }
                if (obj.FieldPrtRtf != null)
                {
                    obj.FieldPrtRtf = UnCompressCaseData(obj.FieldPrtRtf);
                }
            }
        }
        /// <summary>
        /// 解压缩病历数据
        /// </summary>
        /// <param name="p_lstCaseDataTrace"></param>
        public static void UnCompressCaseData(ref List<EntityUniversalCaseRecordTrace> p_lstCaseDataTrace)
        {
            if (p_lstCaseDataTrace == null || p_lstCaseDataTrace.Count == 0)
                return;

            foreach (EntityUniversalCaseRecordTrace obj in p_lstCaseDataTrace)
            {
                if (obj.ColContentRtf != null)
                {
                    obj.ColContentRtf = UnCompressCaseData(obj.ColContentRtf);
                }
            }
        }
        /// <summary>
        /// 解压缩病历数据
        /// </summary>
        /// <param name="p_lstSignature"></param>
        public static void UnCompressCaseData(ref List<EntitySignature> p_lstSignature)
        {
            if (p_lstSignature == null || p_lstSignature.Count == 0)
                return;

            foreach (EntitySignature obj in p_lstSignature)
            {
                if (obj.signContent != null)
                {
                    obj.signContent = UnCompressCaseData(obj.signContent);
                }
            }
        }
        #endregion

        #region 压缩.解压缩病程
        /// <summary>
        /// 压缩病程数据
        /// </summary>
        /// <param name="p_intRegisterID"></param>
        /// <param name="p_strCaseCode"></param>
        /// <param name="p_lstEntityProgressContent"></param>
        /// <param name="p_lstEntityProgressContentTrace"></param>
        /// <param name="p_lstEntitySignature"></param>
        public static bool CompressCaseData(string registerID, string p_strCaseCode, ref List<EntityProgressNoteContent> p_lstEntityProgressContent, ref List<EntityProgressNoteTrace> p_lstEntityProgressContentTrace, ref List<EntitySignature> p_lstEntitySignature)
        {
            if (p_lstEntityProgressContent != null && p_lstEntityProgressContent.Count > 0)
            {
                // 进修、实习医师(护士)采用密码签名
                string strP29 = GlobalParm.dicSysParameter[29];
                int intP29 = 0;
                int.TryParse(strP29, out intP29);

                string strSignature = string.Empty;
                bool blnKeySign = GlobalParm.dicSysParameter[3] == "1";
                if (blnKeySign && p_lstEntitySignature != null && p_lstEntitySignature.Count > 0)
                {
                    StringBuilder stbXML = new StringBuilder();

                    stbXML.Append("<casesignature>");
                    stbXML.Append(System.Environment.NewLine);
                    stbXML.Append("<registerid>" + registerID + "</registerid>");
                    stbXML.Append(System.Environment.NewLine);
                    stbXML.Append("<casecode>" + p_strCaseCode + "</casecode>");
                    stbXML.Append(System.Environment.NewLine);
                    stbXML.Append("<cols>");
                    stbXML.Append(System.Environment.NewLine);
                    foreach (EntityProgressNoteContent obj in p_lstEntityProgressContent)
                    {
                        if (obj.colContentRtf == null)
                        {
                            stbXML.Append("<col code=\"" + obj.colCode + "\" text=\"" + obj.colContent + "\" type=\"0\"/>");
                        }
                        else
                        {
                            stbXML.Append("<col code=\"" + obj.colCode + "\" text=\"" + Function.ConvertByteToObject(obj.colContentPrtRtf).ToString() + "\" type=\"1\"/>");
                        }
                    }
                    stbXML.Append("</cols>");
                    stbXML.Append(System.Environment.NewLine);
                    stbXML.Append("</casesignature>");
                    stbXML.Append(System.Environment.NewLine);
                    strSignature = stbXML.ToString();
                }

                foreach (EntityProgressNoteContent obj in p_lstEntityProgressContent)
                {
                    if (obj.colContentRtf != null)
                    {
                        obj.colContentRtf = Compression.Zip(obj.colContentRtf);
                    }
                    if (obj.colContentPrtRtf != null)
                    {
                        obj.colContentPrtRtf = Compression.Zip(obj.colContentPrtRtf);
                    }
                }

                if (p_lstEntitySignature != null && p_lstEntitySignature.Count > 0 && GlobalParm.dicSysParameter[3] != "0")
                {
                    List<string> lstEmpID = new List<string>();
                    foreach (EntitySignature obj in p_lstEntitySignature)
                    {
                        if (obj.serNo != null && obj.serNo > 0)
                        {
                        }
                        else
                        {
                            if (lstEmpID.IndexOf(obj.empId) < 0)
                            {
                                lstEmpID.Add(obj.empId);
                            }
                        }
                    }
                    if (lstEmpID.Count > 1)
                    {
                        DialogBox.Msg("提示：保存时只能单个医生(护士)独立签名，请确认。\r\n若多个医生(护士)同时签名，请删除别的签名信息后再次保存。", MessageBoxIcon.Information, uiHelper.frmCurr);
                        return false;
                    }

                    foreach (EntitySignature obj in p_lstEntitySignature)
                    {
                        if (obj.serNo != null && obj.serNo > 0)
                        {
                        }
                        else
                        {
                            if (GlobalDic.dicEmpRole.ContainsKey(obj.empId))
                            {
                                if (GlobalDic.dicEmpRole[obj.empId].IndexOf(strP29) > 0)
                                {
                                    continue;
                                }
                            }
                            obj.signContent = Compression.Zip(CA.GetSignContent(strSignature, obj.signKeyId));
                            if (!string.IsNullOrEmpty(strSignature) && obj.signContent == null)
                            {
                                DialogBox.Msg("签名失败。", MessageBoxIcon.Information, uiHelper.frmCurr);
                                return false;
                            }
                        }
                    }
                }
            }

            if (p_lstEntityProgressContentTrace != null && p_lstEntityProgressContentTrace.Count > 0)
            {
                foreach (EntityProgressNoteTrace obj in p_lstEntityProgressContentTrace)
                {
                    if (obj.colContentRtf != null)
                    {
                        obj.colContentRtf = Compression.Zip(obj.colContentRtf);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 解压缩病程数据
        /// </summary>
        /// <param name="p_lstEntityProgressContent"></param>
        public static void UnCompressCaseData(ref List<EntityProgressNoteContent> p_lstEntityProgressContent)
        {
            if (p_lstEntityProgressContent == null || p_lstEntityProgressContent.Count == 0)
                return;

            foreach (EntityProgressNoteContent obj in p_lstEntityProgressContent)
            {
                if (obj.colContentRtf != null)
                {
                    obj.colContentRtf = UnCompressCaseData(obj.colContentRtf);
                }
                if (obj.colContentPrtRtf != null)
                {
                    obj.colContentPrtRtf = UnCompressCaseData(obj.colContentPrtRtf);
                }
            }
        }

        /// <summary>
        /// 解压缩病程数据
        /// </summary>
        /// <param name="p_lstEntityProgressContent"></param>
        public static void UnCompressCaseData(ref List<EntityProgressNoteTrace> p_lstEntityProgressContentTrace)
        {
            if (p_lstEntityProgressContentTrace == null || p_lstEntityProgressContentTrace.Count == 0)
                return;

            foreach (EntityProgressNoteTrace obj in p_lstEntityProgressContentTrace)
            {
                if (obj.colContentRtf != null)
                {
                    obj.colContentRtf = UnCompressCaseData(obj.colContentRtf);
                }
            }
        }
        #endregion

        #endregion

        #region NumOfWeek
        /// <summary>
        /// NumOfWeek
        /// </summary>
        /// <returns></returns>
        public static int NumOfWeek()
        {
            DateTime dtmNow = DateTime.Now;
            using (ProxyCommon proxy = new ProxyCommon())
            {
                dtmNow = proxy.Service.GetServerTime();
            }
            return NumOfWeek(dtmNow);
        }
        /// <summary>
        /// NumOfWeek
        /// </summary>
        /// <param name="dtmNow"></param>
        /// <returns></returns>
        public static int NumOfWeek(DateTime dtmNow)
        {
            switch (dtmNow.DayOfWeek.ToString())
            {
                case "Monday":
                    return 0;
                case "Tuesday":
                    return 1;
                case "Wednesday":
                    return 2;
                case "Thursday":
                    return 3;
                case "Friday":
                    return 4;
                case "Saturday":
                    return 5;
                case "Sunday":
                    return 6;
                default:
                    break;
            }
            return 0;
        }
        #endregion

        #region GridControl列头画checkbox
        /// <summary>
        /// 为列头绘制CheckBox
        /// </summary>
        /// <param name="view">GridView</param>
        /// <param name="checkItem">RepositoryItemCheckEdit</param>
        /// <param name="fieldName">需要绘制Checkbox的列名</param>
        /// <param name="e">ColumnHeaderCustomDrawEventArgs</param>
        public static void DrawHeaderCheckBox(GridView view, RepositoryItemCheckEdit checkItem, string fieldName, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column != null && e.Column.FieldName.Equals(fieldName))
            {
                e.Info.InnerElements.Clear();
                e.Painter.DrawObject(e.Info);
                DrawCheckBox(checkItem, e.Graphics, e.Bounds, getCheckedCount(view, fieldName) == view.DataRowCount);
                e.Handled = true;
            }
        }
        static void DrawCheckBox(RepositoryItemCheckEdit checkItem, Graphics g, Rectangle r, bool Checked)
        {
            CheckEditViewInfo _info;
            CheckEditPainter _painter;
            ControlGraphicsInfoArgs _args;
            _info = checkItem.CreateViewInfo() as CheckEditViewInfo;
            _painter = checkItem.CreatePainter() as CheckEditPainter;
            _info.EditValue = Checked;

            _info.Bounds = r;
            _info.PaintAppearance.ForeColor = Color.Black;
            _info.CalcViewInfo(g);
            _args = new ControlGraphicsInfoArgs(_info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            _painter.Draw(_args);
            _args.Cache.Dispose();
        }
        static int getCheckedCount(GridView view, string filedName)
        {
            int count = 0;
            for (int i = 0; i < view.DataRowCount; i++)
            {
                object _cellValue = view.GetRowCellValue(i, view.Columns[filedName]);
                if (_cellValue == null) continue;
                if (string.IsNullOrEmpty(_cellValue.ToString().Trim())) continue;
                bool _checkStatus = false;
                if (bool.TryParse(_cellValue.ToString(), out _checkStatus))
                {
                    if (_checkStatus)
                        count++;
                }
            }
            return count;
        }
        #endregion

        #region CreateSeries
        /// <summary>
        /// CreateSeries
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="viewType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Series CreateSeries(string caption, ViewType viewType, List<EntityChartDeptNum> data)
        {
            string argument = string.Empty;
            decimal val = 0;
            Series series = new Series(caption, viewType);
            foreach (EntityChartDeptNum item in data)
            {
                if (!string.IsNullOrEmpty(item.deptName))
                    argument = item.deptName;            // 参数名称
                else if (!string.IsNullOrEmpty(item.typeName))
                    argument = item.typeName;            // 参数名称
                else if (!string.IsNullOrEmpty(item.doctName))
                    argument = item.doctName;
                else
                    argument = "**";
                val = Convert.ToDecimal(item.nums); // 参数值
                series.Points.Add(new SeriesPoint(argument, val));
            }
            //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
            //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
            series.ArgumentScaleType = ScaleType.Qualitative;
            series.ValueScaleType = ScaleType.Numerical;
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
            return series;
        }
        /// <summary>
        /// CreateSeries
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="viewType"></param>
        /// <param name="data"></param>
        /// <param name="typeId">0 专科; 1 普通</param>
        /// <returns></returns>
        public static Series CreateSeries(string caption, ViewType viewType, List<EntityChartDeptNum> data, int typeId)
        {
            string argument = string.Empty;
            decimal val = 0;
            Series series = new Series(caption, viewType);
            foreach (EntityChartDeptNum item in data)
            {
                if (!string.IsNullOrEmpty(item.deptName))
                    argument = item.deptName;            // 参数名称
                else if (!string.IsNullOrEmpty(item.typeName))
                    argument = item.typeName;            // 参数名称
                else if (!string.IsNullOrEmpty(item.doctName))
                    argument = item.doctName;
                else
                    argument = "**";
                if (typeId == 0)
                    val = Convert.ToDecimal(item.numsZk); // 参数值
                else if (typeId == 1)
                    val = Convert.ToDecimal(item.numsPt); // 参数值
                series.Points.Add(new SeriesPoint(argument, val));
            }
            //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
            //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
            series.ArgumentScaleType = ScaleType.Qualitative;
            series.ValueScaleType = ScaleType.Numerical;
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签
            return series;
        }
        #endregion

        #region GetFieldValueStr
        /// <summary>
        /// GetFieldValueStr
        /// </summary>
        /// <param name="gridViewBanded"></param>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetFieldValueStr(BandedGridView gridViewBanded, int rowHandle, string fieldName)
        {
            if (gridViewBanded != null)
            {
                if (gridViewBanded.GetRowCellValue(rowHandle, fieldName) == null)
                    return string.Empty;
                else
                    return gridViewBanded.GetRowCellValue(rowHandle, fieldName).ToString();
            }
            return string.Empty;
        }
        /// <summary>
        /// GetFieldValueStr
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetFieldValueStr(GridView gv, int rowHandle, string fieldName)
        {
            if (gv != null)
            {
                if (gv.GetRowCellValue(rowHandle, fieldName) == null)
                    return string.Empty;
                else
                    return gv.GetRowCellValue(rowHandle, fieldName).ToString();
            }
            return string.Empty;
        }
        #endregion

        #region GetFieldValueObj
        /// <summary>
        /// GetFieldValueObj
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object GetFieldValueObj(BandedGridView gridViewBanded, int rowHandle, string fieldName)
        {
            return gridViewBanded.GetRowCellValue(rowHandle, fieldName);
        }
        /// <summary>
        /// GetFieldValueObj
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object GetFieldValueObj(GridView gv, int rowHandle, string fieldName)
        {
            return gv.GetRowCellValue(rowHandle, fieldName);
        }

        #endregion

    }
}


