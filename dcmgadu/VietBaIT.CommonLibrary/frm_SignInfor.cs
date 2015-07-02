using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
using SubSonic;
using VietBaIT.RISLink.DataAccessLayer;
using VietBaIT.CommonLibrary;
namespace  VietBaIT.CommonLibrary
{
    public partial class frm_SignInfor : Form
{
	public bool mv_bChapNhan = false;
	public string mv_sFontName;
	public string mv_sFontStyle;
	public string mv_sFontSize;
    public string mv_sTieuDe = "";
	private void cmdQuit_Click(object sender, System.EventArgs e)
	{
		this.Close();
	}

	private void cmdOK_Click(object sender, System.EventArgs e)
	{
		//thuc hien lay gia tri

		mv_bChapNhan = true;
		this.Close();
	}
	private void GetFontList()
	{
		try {
			foreach (FontFamily s in FontFamily.Families) {
				cboFontName.Items.Add(s.Name);
			}
			for (int i = 6; i <= 72; i++) {
				cboFontSize.Items.Add(i.ToString());
			}
		}
		catch (Exception ex) {

		}
	}
	private void SetFontInfor(string pv_sFontName, string pv_sFontStyple, string pv_sFontSize)
	{
		bool sv_bFound = false;
		try {
            for (int i = 0; i <= cboFontName.Items.Count - 1; i++)
            {
                if (cboFontName.Items[i].ToString().Trim().ToUpper().Equals(pv_sFontName.Trim().ToUpper()))
                {
                    cboFontName.SelectedIndex = i;
                    sv_bFound = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
		    //cboFontName.SelectedValue = pv_sFontName;
            //if (pv_sFontName != null) sv_bFound = true;
			if (!sv_bFound) cboFontName.Text = "Arial"; 
			sv_bFound = false;
            cboFontStyle.SelectedValue = pv_sFontStyple;
            if(pv_sFontStyple!=null) sv_bFound = true;
			if (!sv_bFound) cboFontStyle.SelectedIndex = 2; 
			sv_bFound = false;
			for (int i = 0; i <= cboFontSize.Items.Count - 1; i++) {
				if (cboFontSize.Items[i].ToString().Trim().ToUpper().Equals(pv_sFontSize.Trim().ToUpper())) {
					cboFontSize.SelectedIndex = i;
					sv_bFound = true;
					break; // TODO: might not be correct. Was : Exit For
				}
			}
			if (!sv_bFound) cboFontSize.Text = "8"; 
			sv_bFound = false;
		}
		catch (Exception ex) {

		}
	}

	private void frm_SignInfor_Load(object sender, System.EventArgs e)
	{
      
		try {
			//SetLanguage(gv_sLanguageDisplay, this, "GOLFMAN", gv_oSqlCnn);
		    //sysColor.BackColor = globalVariables.SystemColor;
		   // PricTure.BackColor = globalVariables.SystemColor;
			GetFontList();
			SetFontInfor(mv_sFontName, mv_sFontStyle, mv_sFontSize);
			txtBaoCao.Enabled = false;
			txtBaoCao.BackColor = Color.WhiteSmoke;
           
		}

		catch (Exception ex) {
			//SetLanguage(gv_sLanguageDisplay, this, "GOLFMAN", gv_oSqlCnn);
		}

	}
	public frm_SignInfor()
	{
        InitializeComponent();
        //Utility.loadIconToForm(this);
        InitializeEvents();
		
	}
    private void InitializeEvents()
    { 
        this.Load+=new EventHandler(frm_SignInfor_Load);
        cmdOK.Click+=new EventHandler(cmdOK_Click);
        cmdQuit.Click+=new EventHandler(cmdQuit_Click);
    }

    private void cmdOK_Click_1(object sender, EventArgs e)
    {

    }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    private void frm_SignInfor_KeyDown(object sender, KeyEventArgs e)
    {
        if(e.KeyCode==Keys.Escape)cmdQuit.PerformClick();
        if(e.KeyCode==Keys.A&&e.Control)cmdOK.PerformClick();
        //if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
    }

    private void frm_SignInfor_Load_1(object sender, EventArgs e)
    {

    }

    private void cmdOK_Click_2(object sender, EventArgs e)
    {

    }
}

}
