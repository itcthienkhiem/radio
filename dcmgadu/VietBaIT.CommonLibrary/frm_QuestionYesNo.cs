using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VietBaIT.CommonLibrary;
namespace VietBaIT.CommonLibrary
{
    public partial class frm_QuestionYesNo : Form
    {
        private string sTitle = "Thông báo";
        private string sNoiDung = "";
        public bool b_Cancel = false;
        public frm_QuestionYesNo(string vTitle,string vNoiDung)
        {
            InitializeComponent();
            //lblTitle.ImageList = imageList1;
            this.sTitle = vTitle;
            this.sNoiDung = vNoiDung;
            lblTitle.Text = sTitle;
            lblNoiDung.Text = sNoiDung;
        }
        
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hiện việc load thông tin của forrm hiện tại lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_QuestionYesNo_Load(object sender, EventArgs e)
        {
            lblTitle.Text = sTitle;
            lblNoiDung.Text = sNoiDung;
            cmdAccept.Focus();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            b_Cancel = false;
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc chấp nhâp thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            b_Cancel = true;
            this.Close();
        }
    }
}
