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
    public partial class frm_LargeMsgBox : Form
    {
        public frm_LargeMsgBox(string title,string content,string OK,string Cancel)
        {
            InitializeComponent();
            lblTitle.Text = title.Trim() == "" ? "THÔNG BÁO" : title;
            lblMsg.Text = content;
            cmdOK.Text = OK;
            cmdCancel.Text = Cancel;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {

        }
    }
}
