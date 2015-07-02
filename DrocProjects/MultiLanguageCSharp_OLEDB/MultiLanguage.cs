using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;
using VB6 = Microsoft.VisualBasic.Strings;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Data.OleDb;
using System.ComponentModel;
namespace CSHARP
{
    public class MultiLanguage
    {
        private static DataGridView CurrDtGridView;
        private static StatusStrip CurrStatusBar;
        private static ToolStrip CurrToolBar;
        //private static Janus.Windows.UI.Tab.UITab CurrUITab;
       static ToolTip TTT = new ToolTip();
        private static TabControl CurrTabControl;
        private static string gv_sAnnouce = "Thông báo";
        /// <summary>
        /// Lấy về chuỗi giá trị dựa trên giá trị ngôn ngữ truyền vào
        /// </summary>
        /// <param name="Language">Mã ngôn ngữ truyền vào thường là biến gv_sLanguageDisplay</param>
        /// <param name="VnText">Giá trị tiếng Việt(Language=VN)</param>
        /// <param name="EnText">Giá trị tiếng Anh(Language=EN)</param>
        /// <returns>Nếu gv_sLanguageDisplay=EN thì trả về giá trị VnText. Ngược lại trả về giá trị EnText</returns>
        /// <remarks></remarks>
        public static string GetText(string Language, string VnText, string EnText)
        {
            if (Language.ToUpper() == "VN")
            {
                return VnText;
            }
            else
            {
                return EnText;
            }
        }
        private static BindingFlags flag = BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic;
        /// <summary>
        /// SetLanguageJanus()
        /// </summary>
        /// <param name="Language"></param>
        /// <param name="_control"></param>
        /// <param name="DT1"></param>
        //private static void SetLanguageJanus(string Language, Control _control, DataTable DT1)
        //{
        //    try
        //    {
        //        if (DT1 == null || DT1.Columns.Count <= 0 || DT1.Rows.Count <= 0) return;
        //        DataRow[] dr = null;
        //        Type oClass = _control.GetType();
        //        FieldInfo[] fInfos = oClass.GetFields(flag);
        //        foreach (FieldInfo fInfo in fInfos)
        //        {
        //            try
        //            {
        //                object obj = fInfo.GetValue(_control);
        //                //((Janus.Windows.ExplorerBar.JNSN)((Janus.Windows.ExplorerBar.ExplorerBar)((System.Windows.Forms.Panel)((Janus.Windows.UI.Dock.UIPanelInnerContainer)(() (()obj).Components[2]).Panels[0].Controls[0]).Controls[0]).Controls[0]).Controls[0])
        //                if (obj.GetType().Equals(typeof(System.ComponentModel.Container)))
        //                    foreach (IComponent IC in ((System.ComponentModel.Container)obj).Components)
        //                    {
        //                        if (IC.GetType().Equals(typeof(Janus.Windows.UI.Dock.UIPanelManager)))
        //                        {
        //                            foreach (Janus.Windows.UI.Dock.UIPanel p in ((Janus.Windows.UI.Dock.UIPanelManager)IC).Panels)
        //                            {
        //                                if (p.GetType().Equals(typeof(Janus.Windows.UI.Dock.UIPanel)))
        //                                {

        //                                    dr = DT1.Select("sFormName='" + _control.Name + "' AND sControlName='" + p.Name + "'");
        //                                    if (dr.GetLength(0) > 0)
        //                                    {
        //                                        if (Language.ToUpper() == "VN")
        //                                        {
        //                                            p.Text = sDBnull(dr[0]["sVn"], "");
        //                                        }
        //                                        else
        //                                        {
        //                                            p.Text = sDBnull(dr[0]["sEn"], "");
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                    }

        //                                    foreach (Control c1 in ((Janus.Windows.UI.Dock.UIPanel)p).Controls)
        //                                    {
        //                                        if (c1.GetType().Equals(typeof(Janus.Windows.UI.Dock.UIPanelInnerContainer)))
        //                                        {
        //                                            foreach (Control c2 in ((Janus.Windows.UI.Dock.UIPanelInnerContainer)c1).Controls)
        //                                            {
        //                                                if (c2.GetType().Equals(typeof(System.Windows.Forms.Panel)))
        //                                                {
        //                                                    foreach (Control c3 in ((System.Windows.Forms.Panel)c2).Controls)
        //                                                    {
        //                                                        if (c3.GetType().Equals(typeof(Janus.Windows.ExplorerBar.ExplorerBar)))
        //                                                        {
        //                                                            //dr = DT1.Select("sFormName='" + _control.Name + "' AND sControlName='" + ((Janus.Windows.ExplorerBar.ExplorerBar)c3).Name + "'");
        //                                                            //if (dr.GetLength(0) > 0)
        //                                                            //{
        //                                                            //    if (Language.ToUpper() == "VN")
        //                                                            //    {
        //                                                            //        p.Text = sDBnull(dr[0]["sVn"], "");
        //                                                            //    }
        //                                                            //    else
        //                                                            //    {
        //                                                            //        p.Text = sDBnull(dr[0]["sEn"], "");
        //                                                            //    }
        //                                                            //}
        //                                                            //else
        //                                                            //{
        //                                                            //}

        //                                                            foreach (Janus.Windows.ExplorerBar.ExplorerBarGroup gr in ((Janus.Windows.ExplorerBar.ExplorerBar)c3).Groups)
        //                                                            {
        //                                                                dr = DT1.Select("sFormName='" + _control.Name + "' AND sControlName='" + gr.Key + "'");
        //                                                                if (dr.GetLength(0) > 0)
        //                                                                {
        //                                                                    if (Language.ToUpper() == "VN")
        //                                                                    {
        //                                                                        gr.Text = sDBnull(dr[0]["sVn"], "");
        //                                                                    }
        //                                                                    else
        //                                                                    {
        //                                                                        gr.Text = sDBnull(dr[0]["sEn"], "");
        //                                                                    }
        //                                                                }
        //                                                                else
        //                                                                {
        //                                                                }
        //                                                                //Duyệt qua các Items
        //                                                                for (int i = 0; i <= gr.Items.Count - 1; i++)
        //                                                                {
        //                                                                    dr = DT1.Select("sFormName='" + _control.Name + "' AND sControlName='" + gr.Items[i].Key + "'");
        //                                                                    if (dr.GetLength(0) > 0)
        //                                                                    {
        //                                                                        if (Language.ToUpper() == "VN")
        //                                                                        {
        //                                                                            gr.Items[i].Text = sDBnull(dr[0]["sVn"], "");
        //                                                                        }
        //                                                                        else
        //                                                                        {
        //                                                                            gr.Items[i].Text = sDBnull(dr[0]["sEn"], "");
        //                                                                        }
        //                                                                    }
        //                                                                    else
        //                                                                    {
        //                                                                    }
        //                                                                }
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //            }
        //            catch
        //            {
        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);


        //    }

        //}

        /// <summary>
        /// Thiết lập ngôn ngữ hiển thị cho một Form chức năng 01/01/2900
        /// </summary>
        /// <param name="Language">Mã ngôn ngữ truyền vào thường là biến gv_sLanguageDisplay</param>
        /// <param name="ctrMain">Tên Control(Form) sẽ bị tác động. Giá trị thường là 'me' trong VB.NET hoặc 'this' trong C#</param>
        /// <param name="sDLLName">Tên DLL chứa Form chức năng(Ví dụ: Invoice.Dll)</param>
        /// <param name="gv_oSqlCnn">Biến kết nối tới CSDL. Thường sử dụng luôn biến gv_oSqlCnn</param>
        /// <remarks></remarks>
        public static void SetLanguage(string Language, Control ctrMain, string sDLLName, System.Data.OleDb.OleDbConnection gv_oSqlCnn)
        {

            OleDbDataAdapter da1 = new OleDbDataAdapter("SELECT * FROM Sys_MULTILANGUAGE WHERE sFormName='" + ctrMain.Name + "' AND sDLLName='" + sDLLName + "'", gv_oSqlCnn);
            DataTable DT1 = new DataTable();
            string sVnText = null;
            string sEnText = null;
            sVnText = "";
            sEnText = "";
            try
            {
                da1.Fill(DT1);
                if (DT1.Rows.Count > 0)
                {
                    //SetLanguageJanus(Language, ctrMain, DT1);
                    foreach (DataRow dr1 in DT1.Rows)
                    {
                        if (!DBNull.Value.Equals(dr1["sVnText"]) & string.IsNullOrEmpty(sVnText))
                        {
                            sVnText = sDBnull(dr1["sVnText"], "");
                        }
                        if (!DBNull.Value.Equals(dr1["sEnText"]))
                        {
                            sEnText = sDBnull(dr1["sEnText"], "");
                        }
                        if (!string.IsNullOrEmpty(sVnText) & !string.IsNullOrEmpty(sEnText))
                        {
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    if (Language.ToUpper() == "VN")
                    {
                        ctrMain.Text = sVnText.Replace("&BranchName","VIETNAM");
                    }
                    else
                    {
                        ctrMain.Text = sEnText.Replace("&BranchName", "VIETNAM");
                    }
                    UserControl UC = new UserControl();
                    foreach (Control ctr in ctrMain.Controls)
                    {
                       // if (ctr is Janus.Windows.UI.Tab.UITab || ctr is Janus.Windows.UI.Tab.UITabPage || ctr is TabControl || ctr is TabPage || ctr is StatusStrip || ctr is UserControl || ctr is ToolStrip || ctr is Label || ctr is Button || ctr is GroupBox || ctr is CheckBox || ctr is RadioButton ||  ctr is Panel || ctr is ComboBox || ctr is TextBox)
                            if (ctr is TabControl || ctr is TabPage || ctr is StatusStrip || ctr is UserControl || ctr is ToolStrip || ctr is Label || ctr is Button || ctr is GroupBox || ctr is CheckBox || ctr is RadioButton || ctr is Panel || ctr is ComboBox || ctr is TextBox)
                        {
                            string CtrlName = ctr.Name;
                            if (ctr is Label)
                            {
                                if (ctr.Parent != null && ctr.Parent.GetType().BaseType.FullName == UC.GetType().FullName)
                                {


                                }
                                else
                                {
                                    CtrlName = ctr.Parent.Name + "." + ctr.Name;

                                }
                            }


                            DataRow[] dr = null;
                            dr = DT1.Select("sFormName='" + ctrMain.Name + "' AND sControlName='" + CtrlName + "'");
                            if (dr.GetLength(0) > 0)
                            {
                                if (Language.ToUpper() == "VN")
                                {
                                    if (ctr is ComboBox)
                                    {
                                        ComboBox cbo = null;
                                        string[] splt = sDBnull(dr[0]["sVn"].ToString(), "").Split(',');
                                        cbo = (ComboBox)ctr;
                                        cbo.Items.Clear();
                                        for (int i = 0; i <= splt.GetLength(0) - 1; i++)
                                        {
                                            cbo.Items.Add(splt[i]);
                                        }
                                    }
                                    else
                                    {
                                        if (sDBnull(dr[0]["TTT"], "").ToString() == "1")
                                        {
                                            TTT.SetToolTip(ctr, sDBnull(dr[0]["sVn"], ""));
                                        }
                                        else
                                        {
                                            if (sDBnull(dr[0]["LCHK"], "").ToString() == "1")
                                                ctr.Text = "        " + sDBnull(dr[0]["sVn"], "");
                                            else
                                                ctr.Text = sDBnull(dr[0]["sVn"], "");
                                        }
                                    }
                                }
                                else
                                {
                                    if (ctr is ComboBox)
                                    {
                                        ComboBox cbo = null;
                                        string[] splt = sDBnull(dr[0]["sEn"], "").Split(',');
                                        cbo = (ComboBox)ctr;
                                        cbo.Items.Clear();
                                        for (int i = 0; i <= splt.GetLength(0) - 1; i++)
                                        {
                                            cbo.Items.Add(splt[i]);
                                        }
                                    }
                                    else
                                    {
                                        if (sDBnull(dr[0]["TTT"],"").ToString() == "1")
                                        {
                                            TTT.SetToolTip(ctr, sDBnull(dr[0]["sEn"], ""));
                                        }
                                        else
                                            if (sDBnull(dr[0]["LCHK"], "").ToString() == "1")
                                                ctr.Text = "        " + sDBnull(dr[0]["sEn"], "");
                                            else
                                                ctr.Text = sDBnull(dr[0]["sEn"], "");
                                    }
                                }
                            }
                            else
                            {
                            }
                            LoopControl(ctrMain, DT1, ctr, Language);
                        }
                        //else if (ctr is Janus.Windows.UI.Tab.UITab)
                        //{
                        //    //StatusStrip or ToolStrip
                        //    objGetCurrentUITab((Form)ctrMain, ctr.Name);
                        //    // CType(ctr, DataGridView)
                        //    for (int i = 0; i <= CurrUITab.TabPages.Count - 1; i++)
                        //    {
                        //        DataRow[] dr = null;
                        //        dr = DT1.Select("sFormName='" + ctrMain.Name + "' AND sControlName='" + CurrUITab.TabPages[i].Name + "'");
                        //        if (dr.GetLength(0) > 0)
                        //        {
                        //            if (Language.ToUpper() == "VN")
                        //            {
                        //                CurrUITab.TabPages[i].Text = sDBnull(dr[0]["sVn"], "");
                        //            }
                        //            else
                        //            {
                        //                CurrUITab.TabPages[i].Text = sDBnull(dr[0]["sEn"], "");
                        //            }
                        //        }
                        //        else
                        //        {
                        //        }
                        //    }
                        //    LoopControl(ctrMain, DT1, ctr, Language);
                        //}
                        else if (ctr is TabControl)
                        {
                            //TabControl
                            objGetCurrentTabControl((Form)ctrMain, ctr.Name);
                            // CType(ctr, DataGridView)
                            for (int i = 0; i <= CurrTabControl.TabPages.Count - 1; i++)
                            {
                                DataRow[] dr = null;
                                dr = DT1.Select("sFormName='" + ctrMain.Name + "' AND sControlName='" + CurrTabControl.TabPages[i].Name + "'");
                                if (dr.GetLength(0) > 0)
                                {
                                    if (Language.ToUpper() == "VN")
                                    {
                                        if (sDBnull(dr[0]["TTT"], "").ToString() == "1")
                                        {
                                            TTT.SetToolTip(CurrTabControl.TabPages[i], sDBnull(dr[0]["sVn"], ""));
                                        }
                                        else
                                        CurrTabControl.TabPages[i].Text = sDBnull(dr[0]["sVn"], "");
                                    }
                                    else
                                    {
                                        if (sDBnull(dr[0]["TTT"], "").ToString() == "1")
                                        {
                                            TTT.SetToolTip(CurrTabControl.TabPages[i], sDBnull(dr[0]["sEn"], ""));
                                        }
                                        else
                                        CurrTabControl.TabPages[i].Text = sDBnull(dr[0]["sEn"], "");
                                    }
                                }
                                else
                                {
                                }
                            }
                            LoopControl(ctrMain, DT1, ctr, Language);
                        }
                        else if (ctr is StatusStrip && ctr.GetType().FullName != "System.Windows.Forms.ToolStrip")
                        {
                            //StatusStrip or ToolStrip
                            objGetCurrentStatusBar((Form)ctrMain, ctr.Name);
                            // CType(ctr, DataGridView)
                            for (int i = 0; i <= CurrStatusBar.Items.Count - 1; i++)
                            {
                                DataRow[] dr = null;
                                dr = DT1.Select("sFormName='" + ctrMain.Name + "' AND sControlName='" + CurrStatusBar.Items[i].Name + "'");
                                if (dr.GetLength(0) > 0)
                                {
                                    if (Language.ToUpper() == "VN")
                                    {
                                        
                                        CurrStatusBar.Items[i].Text = sDBnull(dr[0]["sVn"], "");
                                    }
                                    else
                                    {
                                        
                                        CurrStatusBar.Items[i].Text = sDBnull(dr[0]["sEn"], "");
                                    }
                                }
                                else
                                {
                                }
                            }
                            LoopControl(ctrMain, DT1, ctr, Language);
                        }
                        else if (ctr is ToolStrip && ctr.GetType().FullName == "System.Windows.Forms.ToolStrip")
                        {
                            //StatusStrip or ToolStrip
                            objGetCurrentToolbar((Form)ctrMain, ctr.Name);
                            // CType(ctr, DataGridView)
                            for (int i = 0; i <= CurrToolBar.Items.Count - 1; i++)
                            {
                                DataRow[] dr = null;
                                dr = DT1.Select("sFormName='" + ctrMain.Name + "' AND sControlName='" + CurrToolBar.Items[i].Name + "'");
                                if (dr.GetLength(0) > 0)
                                {
                                    if (Language.ToUpper() == "VN")
                                    {
                                        
                                        CurrToolBar.Items[i].ToolTipText = sDBnull(dr[0]["sVn"], "");
                                    }
                                    else
                                    {
                                       
                                        CurrToolBar.Items[i].ToolTipText = sDBnull(dr[0]["sEn"], "");
                                    }
                                }
                                else
                                {
                                }
                            }
                            LoopControl(ctrMain, DT1, ctr, Language);
                        }
                        else if (ctr is DataGridView)
                        {
                            objGetCurrentDataGridView((Form)ctrMain, ctr.Name);
                            // CType(ctr, DataGridView)
                            foreach (DataGridViewColumn col in CurrDtGridView.Columns)
                            {
                                DataRow[] dr = null;
                                dr = DT1.Select("sFormName='" + ctrMain.Name + "' AND sControlName='" + col.Name + "'");
                                if (dr.GetLength(0) > 0)
                                {
                                    if (Language.ToUpper() == "VN")
                                    {
                                        col.HeaderText = sDBnull(dr[0]["sVn"], "");
                                    }
                                    else
                                    {
                                        col.HeaderText = sDBnull(dr[0]["sEn"], "");
                                    }
                                }
                                else
                                {
                                }
                            }
                            LoopControl(ctrMain, DT1, ctr, Language);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrMsg(ex.Message);
            }
        }
        //private static void objGetCurrentUITab(Control objMain, string pv_sName)
        //{
        //    foreach (Control ctr in objMain.Controls)
        //    {
        //        if (ctr.Name == pv_sName)
        //        {
        //            CurrUITab = (Janus.Windows.UI.Tab.UITab)ctr;
        //        }
        //        else
        //        {
        //            objGetCurrentUITab(ctr, pv_sName);
        //        }
        //    }
        //}
        private static void LoopControl(Control mainForm, DataTable dt1, Control objCtr, string pv_sLang)
        {
            UserControl UC = new UserControl();
            foreach (Control ctr in objCtr.Controls)
            {
                //if (ctr is Janus.Windows.UI.Tab.UITab || ctr is Janus.Windows.UI.Tab.UITabPage || ctr is TabControl || ctr is TabPage || ctr is StatusStrip || ctr is UserControl || ctr is ToolStrip || ctr is Label || ctr is Button || ctr is GroupBox || ctr is CheckBox || ctr is RadioButton ||  ctr is Panel || ctr is ComboBox || ctr is TextBox)
                    if ( ctr is TabControl || ctr is TabPage || ctr is StatusStrip || ctr is UserControl || ctr is ToolStrip || ctr is Label || ctr is Button || ctr is GroupBox || ctr is CheckBox || ctr is RadioButton || ctr is Panel || ctr is ComboBox || ctr is TextBox)
                {

                    DataRow[] dr = null;
                    string CtrlName = ctr.Name;
                    if (CtrlName.Contains("Panel5"))
                    {
                        int i = 0;
                    }
                    if (ctr is Label)
                    {
                        if (ctr.Parent != null && ctr.Parent.GetType().BaseType.FullName == UC.GetType().FullName)
                        {
                           

                        }
                        else
                        {
                            CtrlName = ctr.Parent.Name + "." + ctr.Name;

                        }
                    }

                    dr = dt1.Select("sFormName='" + mainForm.Name + "' AND sControlName='" + CtrlName + "'");
                    if (dr.GetLength(0) > 0)
                    {
                        if (pv_sLang.ToUpper() == "VN")
                        {
                            if (ctr is ComboBox)
                            {
                                ComboBox cbo = null;
                                string[] splt = sDBnull(dr[0]["sVn"], "").Split(',');
                                cbo = (ComboBox)ctr;
                                cbo.Items.Clear();
                                for (int i = 0; i <= splt.GetLength(0) - 1; i++)
                                {
                                    cbo.Items.Add(splt[i]);
                                }
                            }
                            else
                            {
                                if (sDBnull(dr[0]["TTT"], "").ToString() == "1")
                                {
                                    TTT.SetToolTip(ctr, sDBnull(dr[0]["sVn"], ""));
                                }
                                else
                                    if (sDBnull(dr[0]["LCHK"], "").ToString() == "1")
                                        ctr.Text = "        " + sDBnull(dr[0]["sVn"], "");
                                    else
                                        ctr.Text = sDBnull(dr[0]["sVn"], "");
                            }
                        }
                        else
                        {
                            if (ctr is ComboBox)
                            {
                                ComboBox cbo = null;
                                string[] splt = sDBnull(dr[0]["sEn"], "").Split(',');
                                cbo = (ComboBox)ctr;
                                cbo.Items.Clear();
                                for (int i = 0; i <= splt.GetLength(0) - 1; i++)
                                {
                                    cbo.Items.Add(splt[i]);
                                }
                            }
                            else
                            {
                                if (sDBnull(dr[0]["TTT"], "").ToString() == "1")
                                {
                                    TTT.SetToolTip(ctr, sDBnull(dr[0]["sEn"], ""));
                                }
                                else
                                    if (sDBnull(dr[0]["LCHK"], "").ToString() == "1")
                                        ctr.Text = "        " + sDBnull(dr[0]["sEn"], "");
                                    else
                                        ctr.Text = sDBnull(dr[0]["sEn"], "");
                            }
                        }
                    }
                    else
                    {
                    }
                    LoopControl(mainForm, dt1, ctr, pv_sLang);
                }
                //else if (ctr is Janus.Windows.UI.Tab.UITab)
                //{
                //    //StatusStrip or ToolStrip
                //    objGetCurrentUITab((Form)mainForm, ctr.Name);
                //    // CType(ctr, DataGridView)
                //    for (int i = 0; i <= CurrUITab.TabPages.Count - 1; i++)
                //    {
                //        DataRow[] dr = null;
                //        dr = dt1.Select("sFormName='" + mainForm.Name + "' AND sControlName='" + CurrUITab.TabPages[i].Name + "'");
                //        if (dr.GetLength(0) > 0)
                //        {
                //            if (pv_sLang.ToUpper() == "VN")
                //            {
                //                CurrUITab.TabPages[i].Text = sDBnull(dr[0]["sVn"], "");
                //            }
                //            else
                //            {
                //                CurrUITab.TabPages[i].Text = sDBnull(dr[0]["sEn"], "");
                //            }
                //        }
                //        else
                //        {
                //        }
                //    }
                //    LoopControl(mainForm, dt1, ctr, pv_sLang);
                //}
                else if (ctr is TabControl)
                {
                    //TabControl
                    objGetCurrentTabControl((Form)mainForm, ctr.Name);
                    // CType(ctr, DataGridView)
                    for (int i = 0; i <= CurrTabControl.TabPages.Count - 1; i++)
                    {
                        DataRow[] dr = null;
                        dr = dt1.Select("sFormName='" + mainForm.Name + "' AND sControlName='" + CurrTabControl.TabPages[i].Name + "'");
                        if (dr.GetLength(0) > 0)
                        {
                            if (pv_sLang.ToUpper() == "VN")
                            {
                                if (sDBnull(dr[0]["TTT"], "").ToString() == "1")
                                {
                                    TTT.SetToolTip(CurrTabControl.TabPages[i], sDBnull(dr[0]["sVn"], ""));
                                }
                                else
                                CurrTabControl.TabPages[i].Text = sDBnull(dr[0]["sVn"], "");
                            }
                            else
                            {
                                if (sDBnull(dr[0]["TTT"], "").ToString() == "1")
                                {
                                    TTT.SetToolTip(CurrTabControl.TabPages[i], sDBnull(dr[0]["sEn"], ""));
                                }
                                else
                                CurrTabControl.TabPages[i].Text = sDBnull(dr[0]["sEn"], "");
                            }
                        }
                        else
                        {
                        }
                    }
                    LoopControl(mainForm, dt1, ctr, pv_sLang);
                }
                else if (ctr is StatusStrip)
                {
                }
                else if (ctr is ToolStrip)
                {
                }
                else if (ctr is DataGridView)
                {
                    objGetCurrentDataGridView((Form)mainForm, ctr.Name);
                    // CType(ctr, DataGridView)
                    foreach (DataGridViewColumn col in CurrDtGridView.Columns)
                    {
                        DataRow[] dr = null;
                        dr = dt1.Select("sFormName='" + mainForm.Name + "' AND sControlName='" + col.Name + "'");
                        if (dr.GetLength(0) > 0)
                        {
                            if (pv_sLang.ToUpper() == "VN")
                            {
                                col.HeaderText = sDBnull(dr[0]["sVn"], "");
                            }
                            else
                            {
                                col.HeaderText = sDBnull(dr[0]["sEn"], "");
                            }
                        }
                        else
                        {
                        }
                    }
                    LoopControl(mainForm, dt1, ctr, pv_sLang);
                }
            }
        }
        private static void objGetCurrentTabControl(Control objMain, string pv_sName)
        {
            foreach (Control ctr in objMain.Controls)
            {
                if (ctr.Name == pv_sName)
                {
                    CurrTabControl = (TabControl)ctr;
                }
                else
                {
                    objGetCurrentTabControl(ctr, pv_sName);
                }
            }
        }
        private static void objGetCurrentDataGridView(Control objMain, string pv_sName)
        {
            foreach (Control ctr in objMain.Controls)
            {
                if (ctr.Name == pv_sName)
                {
                    CurrDtGridView = (DataGridView)ctr;
                }
                else
                {
                    objGetCurrentDataGridView(ctr, pv_sName);
                }
            }
        }
        private static void objGetCurrentStatusBar(Control objMain, string pv_sName)
        {
            foreach (Control ctr in objMain.Controls)
            {
                if (ctr.Name == pv_sName)
                {
                    CurrStatusBar = (StatusStrip)ctr;
                }
                else
                {
                    objGetCurrentStatusBar(ctr, pv_sName);
                }
            }
        }
        private static void objGetCurrentToolbar(Control objMain, string pv_sName)
        {
            foreach (Control ctr in objMain.Controls)
            {
                if (ctr.Name == pv_sName)
                {
                    CurrToolBar = (ToolStrip)ctr;
                }
                else
                {
                    objGetCurrentToolbar(ctr, pv_sName);
                }
            }
        }
        private static string sDBnull(object pv_obj, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("")]  // ERROR: Optional parameters aren't supported in C#
string Reval)
        {
            if (DBNull.Value.Equals(pv_obj) | (pv_obj == null))
            {
                return Reval;
            }
            else
            {
                return pv_obj.ToString();
            }
        }
        private static bool IsDBnullOrNothing(object pv_obj)
        {
            if (DBNull.Value.Equals(pv_obj) | (pv_obj == null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static int intDBnull(object pv_obj, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(-1)]  // ERROR: Optional parameters aren't supported in C#
int reval)
        {
            if (DBNull.Value.Equals(pv_obj) | (pv_obj == null))
            {
                return reval;
            }
            else
            {
                return (int)pv_obj;
            }
        }
        private static double fDBnull(object pv_obj, double Reval)
        {
            if (DBNull.Value.Equals(pv_obj) || (pv_obj == null) || string.IsNullOrEmpty(sDBnull(pv_obj, "")))
            {
                return Reval;
            }
            else
            {
                return (double)pv_obj;
            }
        }
        private static void ShowMsg(string VnMsg, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("VN")]  // ERROR: Optional parameters aren't supported in C#
string sLanguage, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("")]  // ERROR: Optional parameters aren't supported in C#
string EnMsg)
        {
            if (sLanguage.ToUpper() == "VN")
            {
                MessageBox.Show(VnMsg, gv_sAnnouce, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(EnMsg, gv_sAnnouce, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private static void ShowErrMsg(string pv_sError)
        {
            MessageBox.Show("Lỗi: " + pv_sError, gv_sAnnouce, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
