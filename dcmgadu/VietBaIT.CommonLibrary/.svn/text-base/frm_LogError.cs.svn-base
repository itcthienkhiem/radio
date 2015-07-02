using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.RISLink.DataAccessLayer;
using SubSonic;
using VietBaIT.CommonLibrary;
namespace VietBaIT.CommonLibrary
{
    public partial class frm_LogError : Form
    {
        private DataTable m_dtErrorLog=new DataTable();
        public frm_LogError()
        {
            InitializeComponent();
            dtToDate.Value = dtFromDate.Value = BusinessHelper.GetSysDateTime();
            cboKieu.SelectedIndex = 0;
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin của log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoa_Click(object sender, EventArgs e)
        {
            if(grdLogError.GetCheckedRows().GetLength(0)<=0)
            {
                Utility.ShowMsg("Bạn phải chọn lại thông tin ","Thông báo",MessageBoxIcon.Warning);
                return;
            }
            System.Collections.ArrayList arrayList=new ArrayList();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdLogError.GetCheckedRows())
            {
                arrayList.Add(Utility.Int32Dbnull(gridExRow.Cells[RISLink.DataAccessLayer.ErrorLog.Columns.ErrorId].Value, -1));
                gridExRow.Delete();
                grdLogError.UpdateData();
                grdLogError.Refresh();
            }
            new Delete().From(RISLink.DataAccessLayer.ErrorLog.Columns.ErrorId).Where(
                RISLink.DataAccessLayer.ErrorLog.Columns.ErrorId).In(arrayList).Execute();
            m_dtErrorLog.AcceptChanges();
        }

        private void frm_LogError_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hêện tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SqlQuery sqlQuery = new Select().From(RISLink.DataAccessLayer.ErrorLog.Schema)
                .Where(RISLink.DataAccessLayer.ErrorLog.Columns.ErrorCode).IsEqualTo(cboKieu.SelectedValue)
                .And(RISLink.DataAccessLayer.ErrorLog.Columns.ErrorTime).IsBetweenAnd(dtFromDate.Value.Date,
                                                                                      dtToDate.Value.Date);
            m_dtErrorLog = sqlQuery.ExecuteDataSet().Tables[0];
            grdLogError.DataSource = m_dtErrorLog;

        }
    }
}
