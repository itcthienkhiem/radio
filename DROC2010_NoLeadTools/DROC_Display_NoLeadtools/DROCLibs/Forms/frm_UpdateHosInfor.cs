using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace VietBaIT.DROC
{
    public partial class frm_UpdateHosInfor : Form
    {
        
        private string filePath = Application.StartupPath + @"\Company.dat";
        DROC_Ribbon _mainForm;
        public frm_UpdateHosInfor(DROC_Ribbon _mainForm)
        {
            InitializeComponent();
            this._mainForm = _mainForm;
            LoadConfig();
        }
        public frm_UpdateHosInfor()
        {
            InitializeComponent();
          
        }
        public string _HosName
        {
            get { return txtHosName.Text.Trim(); }
        }
        public string _DepartName
        {
            get { return txtDepartmentName.Text.Trim(); }
        }
        /// <summary>
        /// Load một số thông tin đã được cấu hình và lưu lại vào biến dùng chung toàn hệ thống
        /// </summary>
        void LoadConfig()
        {
            #region Load đơn vị làm việc-Chủ yếu phục vụ để burn vào ảnh khi in ra máy in Film
            try
            {

                if (!File.Exists(filePath))
                {
                }
                else
                {
                    using (StreamReader _reader = new StreamReader(filePath))
                    {
                        string obj = _reader.ReadLine();
                        if (obj != null) _mainForm._HosName = obj.ToString();
                        obj = _reader.ReadLine();
                        if (obj != null) _mainForm._DepartName = obj.ToString();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                txtDepartmentName.Text = _mainForm._DepartName;
                txtHosName.Text = _mainForm._HosName;
            }
            #endregion
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            SaveInfor();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// Lưu thông tin đơn vị phục vụ in lên phim vào file Company.txt
        /// </summary>
        void SaveInfor()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(txtHosName.Text.Trim());
                    writer.WriteLine(txtDepartmentName.Text.Trim());
                    writer.Flush();
                    writer.Close();
                }
            }
            catch
            {
            }

        }
        private void frm_UpdateHosInfor_Load(object sender, EventArgs e)
        {

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
