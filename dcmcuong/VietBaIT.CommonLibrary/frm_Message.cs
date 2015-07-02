using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
namespace VietBaIT.CommonLibrary
{
    public partial class frm_Message : Form
    {
        private string sTitle = "Thông báo";
        private string sNoiDung = "";
       // private string sNoiDung = "";
        public bool b_Cancel = false;
        private int sIndex = 0;
        public frm_Message(string vTitle, string vNoiDung,System.Windows.Forms.MessageBoxIcon Icon)
        {
            InitializeComponent();
            lblTitle.ImageList = imageList1;
            this.sTitle = vTitle;
            this.sNoiDung = vNoiDung;
            switch (Icon)
            {
                case  MessageBoxIcon.Warning:
                sIndex = 0;
                    break;
                case MessageBoxIcon.Error:
                    sIndex = 1;
                    break;
                case MessageBoxIcon.Information:
                    sIndex = 2;
                    break;
                default:
                    sIndex = 3;
                    break;
               
                   
            }
            
            lblTitle.ImageIndex = sIndex;
            lblTitle.Text = sTitle;
            lblNoiDung.Text = sNoiDung;

        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
