using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using VietBaIT.CommonLibrary;
namespace VietBaIT.DROC
{
    public partial class frm_MapingExcelColumns : Form
    {
        DataSet dsMapping = new DataSet();
        public frm_MapingExcelColumns()
        {
            InitializeComponent();
        }
        DataSet CreateExcelMapping()
        {
            DataSet ds = new DataSet();
            DataTable dtSheetName = new DataTable("SheetName");
            dtSheetName.Columns.Add(new DataColumn("SName",typeof(string)));
            DataRow dr = dtSheetName.NewRow();
            dr["SName"] = "DS_BENHNHAN";
            dtSheetName.Rows.Add(dr);
            DataTable dt = new DataTable("Mapping");
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("STT",typeof(Int32)),
            new DataColumn("Standard_ColName",typeof(string)),
            new DataColumn("Excel_ColName",typeof(string))});
            //Add defaultRows
            AddRow(dt, 1, "Mã Bệnh nhân", "MA_BN");
            AddRow(dt, 2, "Tên Bệnh nhân", "TEN_BN");
            AddRow(dt, 3, "Ngày sinh", "NGAY_SINH");
            AddRow(dt, 4, "Giới tính", "GIOI_TINH");
            AddRow(dt, 5, "Địa chỉ", "DIA_CHI");
            AddRow(dt, 6, "Đơn vị khám bệnh", "DVI_KHAMBENH");
            AddRow(dt, 6, "Ngày chụp", "NGAY_CHUP");
            AddRow(dt, 7, "Đường dẫn file ảnh", "TEN_FILE");
            ds.Tables.Add(dtSheetName);
            ds.Tables.Add(dt);
            return ds;
        }
        void AddRow(DataTable dt, int STT, string Standard_ColName, string Excel_ColName)
        {
            DataRow dr = dt.NewRow();
            dr["STT"] = STT;
            dr["Standard_ColName"] = Standard_ColName;
            dr["Excel_ColName"] = Excel_ColName;
            dt.Rows.Add(dr);
        }
        void LoadMapping()
        {
           
            string fileName = Application.StartupPath + @"\ExcelMapping.xml";
            try
            {
                if (!File.Exists(fileName))
                {
                    dsMapping = CreateExcelMapping();
                }
                else
                {
                    dsMapping.ReadXml(fileName);
                    if (dsMapping != null && dsMapping.Tables.Count > 0 && dsMapping.Tables["MAPPING"].Columns.Contains("STT") && dsMapping.Tables["MAPPING"].Columns.Contains("Standard_ColName") && dsMapping.Tables["MAPPING"].Columns.Contains("Excel_ColName"))
                    {
                        string temp;
                        temp = "";
                    }
                    else
                        dsMapping = CreateExcelMapping();
                }
                //Binding into DataGrid
                dsMapping.Tables["MAPPING"].DefaultView.Sort = "STT ASC";
                grdMapping.AutoGenerateColumns = false;
                grdMapping.DataSource = dsMapping.Tables["MAPPING"].DefaultView;
                txtSheetName.Text=dsMapping.Tables["SheetName"].Rows[0]["SName"].ToString();
                txtSheetName.Focus();
            }
            catch
            {
            }
        }
        private void frm_MapingExcelColumns_Load(object sender, EventArgs e)
        {
            LoadMapping();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblSheetName_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void SaveMapping2XML()
        {
            DataSet ds = new DataSet();
            string fileName = Application.StartupPath + @"\ExcelMapping.xml";
            if (txtSheetName.Text.Trim() == "")
            {
                Utility.ShowMsg("Bạn phải nhập tên trang trong file Excel chứa danh sách BN");
                return;
            }
            if (dsMapping.Tables["Mapping"].Select("trim(Excel_ColName)=''").GetLength(0)>0)
            {
                Utility.ShowMsg("Cột ánh xạ trong excel không được để trống");
                return;
            }
            try
            {
                dsMapping.Tables["SheetName"].Rows[0]["SName"] = txtSheetName.Text.Trim();
                dsMapping.WriteXml(fileName, XmlWriteMode.WriteSchema);
                Utility.ShowMsg("Đã cập nhật ánh xạ file Excel thành công!");
                this.Close();
            }
            catch
            {
            }
        }
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            SaveMapping2XML();  
        }
    }
}
