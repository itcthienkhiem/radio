using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VietBaIT
{
    public partial class frm_LargeMsgBoxOK : Form
    {
        public frm_LargeMsgBoxOK(string title,string content,string OK,string Cancel)
        {
            InitializeComponent();
            lblTitle.Text = title.Trim() == "" ? "THÔNG BÁO" : title;
            lblMsg.Text = content;
            cmdOK.Text = OK;
           
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {

        }

        private void lnkShowAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new VietBaIT.DROC.AboutBox1().ShowDialog();
        }
    }
}
