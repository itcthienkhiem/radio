using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace VietBaIT.DcmProcessing
{
    public partial class frm_AddnewConfig : Form
    {
        public frm_AddnewConfig()
        {
            InitializeComponent();
        }
        public string _Name
        {
            get { return txtName.Text.Trim(); }
            set { txtName.Text = value; }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
