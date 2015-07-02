using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VietBaIT.RISLink.DataAccessLayer;
using VietBaIT.CommonLibrary;
namespace VietBaIT.CommonLibrary
{
    public partial class frm_TIEU_DE : Form
    {
     
        public frm_TIEU_DE()
        {
            InitializeComponent();
            this.KeyDown+=new KeyEventHandler(frm_TIEU_DE_KeyDown);
            txtTieuDe.Enabled = true;
            txtTieuDe.Focus();
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_TIEU_DE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdQuit.PerformClick();
            if (e.KeyCode == Keys.A && e.Control) cmdOK.PerformClick();
            //if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }
        private void cmdQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện chấp nhập thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdOK_Click(object sender, EventArgs e)
        {
            SqlQuery sqlQuery = new SubSonic.Select().From(SysTrinhky.Schema)
                .Where(SysTrinhky.Columns.ReportName).IsEqualTo(txtBaoCao.Text);
               
            if(sqlQuery.GetRecordCount()>0)
            {
                new Update(SysTrinhky.Schema)
                    .Set(SysTrinhky.Columns.TitleReport).EqualTo(txtTieuDe.Text)
                    .Where(SysTrinhky.Columns.ReportName).IsEqualTo(txtBaoCao.Text).Execute();
            }
            else
            {
                SysTrinhky.Insert(txtBaoCao.Text, globalVariables.UserName, "Arial", 12, "Chữ đậm", "", txtTieuDe.Text);
            }
            this.Close();
        }

        private void frm_TIEU_DE_Load(object sender, EventArgs e)
        {
           
        }
    }
}
