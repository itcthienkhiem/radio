using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
using DROCLibs.Entities;
using VietBaIT.DROC.Objects;
using VietBaIT.DROC.Objects.ObjectController;
using VietBaIT.DROC.Objects.ObjectInfor;
using System.Collections;

using VietBaIT.Controls;
namespace VietBaIT.DROC
{
    public partial class frm_ChooseAnatomy : BaseForm
    {
        public DataTable AnatomyDataSource = new DataTable();
       
        public frm_ChooseAnatomy()
        {
            InitializeComponent();
           
            Application.DoEvents();
            GetData();
            this.WindowState = FormWindowState.Maximized;
           
        }

        void _KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                e.Handled = VietBaIT.CommonLibrary.Utility.NumbersOnly(e.KeyChar, txt.Text);
            }
            catch
            {
            }
        }

        public void GetSelectedAnatomyName()
        {
            try
            {
                string s = "";
                foreach (Control ctr in pnlAnatomyList.Controls)
                {
                    AnatomyControl _Anatomy =ctr as AnatomyControl;
                    if (_Anatomy.isPressed)
                        s += _Anatomy.Vn_Name + ";";
                }
                if(s.Length>0)lblAnatomyList.Text = s.Substring(0, s.Length - 1);
            }
            catch
            {
            }
        }
        public FlowLayoutPanel _pnlAnatomy
        {
            get { return pnlAnatomyList; }
        }

        public void ResetPreviousSelectedObject(string CODE)
        {
            try
            {
                foreach (Control ctr in pnlAnatomyList.Controls)
                {
                    AnatomyControl _Anatomy = ctr as AnatomyControl;
                    if (_Anatomy.isPressed && _Anatomy.Code != CODE)
                        _Anatomy.Reset();
                }
            }
            catch
            {
            }
        }
        private void GetData()
        {
            try
            {
                AnatomyDataSource = new DoctorController().GetAnatomy().Tables[0];
                AnatomyControl _Anatomy = null;
                foreach (DataRow dr in AnatomyDataSource.Rows)
                {
                    _Anatomy = new AnatomyControl(this, dr["Code"].ToString(), Utility.sDbnull(dr["VN_ANATOMY_NAME"].ToString(), "NO NAME"), Utility.sDbnull(dr["EN_ANATOMY_NAME"].ToString(), "NO NAME"), Utility.Int32Dbnull(dr["STT"].ToString(), 0));
                    _Anatomy._OnClick += new AnatomyControl.OnClick(_Anatomy__OnClick);
                    pnlAnatomyList.Controls.Add(_Anatomy);
                }
                cmdOK.Enabled = AnatomyDataSource.Rows.Count > 0;
            }
            catch
            {
            }
        }

        void _Anatomy__OnClick(AnatomyControl obj)
        {
            try
            {
                ResetPreviousSelectedObject(obj.Code);
                obj.isPressed = !obj.isPressed;
                if (!obj.isPressed)
                {
                    obj._AnatomyObject.BackColor = Color.WhiteSmoke;
                    obj._AnatomyObject.ForeColor = Color.Black;
                }
                else
                {
                    obj._AnatomyObject.BackColor = Color.SteelBlue;
                    obj._AnatomyObject.ForeColor = Color.White;
                }
                cmdOK.Enabled = HasSelectedAnatomy();
            }
            catch
            {
            }
        }

     

        private bool HasSelectedAnatomy()
        {

            try
            {
                foreach (Control ctr in pnlAnatomyList.Controls)
                {
                    AnatomyControl _Anatomy = ctr as AnatomyControl;
                    if (_Anatomy.isPressed)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        private void InitView()
        {
           
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
