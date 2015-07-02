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
    public partial class frm_ChooseProjection : BaseForm
    {
        public DataTable ProjectionDataSource = new DataTable();
        public int SelectedObjectCount = 0;
        public frm_ChooseProjection()
        {
            InitializeComponent();
           
            Application.DoEvents();
            GetData();
            this.WindowState = FormWindowState.Maximized;
        }


        public void GetSelectedProjectionName()
        {
            try
            {
                SelectedObjectCount = 0;
                string s = "";
                foreach (Control ctr in pnlProjectionList.Controls)
                {
                    ProjectionControl _Projection = ctr as ProjectionControl;
                    if (_Projection.isPressed)
                    {
                        SelectedObjectCount++;
                        s += _Projection.Vn_Name + ";";
                    }
                }
                if (s.Length > 0) lblAnatomyList.Text = s.Substring(0, s.Length - 1);
            }
            catch
            {
            }
        }
        public FlowLayoutPanel _pnlProjection
        {
            get { return pnlProjectionList ; }
        }
        private void GetData()
        {
            try
            {
                ProjectionDataSource = new DoctorController().GetProjection().Tables[0];
                ProjectionControl _Projection = null;
                foreach (DataRow dr in ProjectionDataSource.Rows)
                {
                    _Projection = new ProjectionControl(this, dr["Code"].ToString(), Utility.sDbnull(dr["VN_Projection_NAME"].ToString(), "NO NAME"), Utility.sDbnull(dr["EN_Projection_NAME"].ToString(), "NO NAME"), Utility.Int32Dbnull(dr["STT"].ToString(), 0),false);
                    _Projection._OnClick += new ProjectionControl.OnClick(_Projection__OnClick);
                    pnlProjectionList.Controls.Add(_Projection);
                }
                cmdOK.Enabled = ProjectionDataSource.Rows.Count > 0;
            }
            catch
            {
            }
        }

        void _Projection__OnClick(ProjectionControl obj)
        {
            obj.isPressed = !obj.isPressed;
            if (!obj.isPressed)
            {
                obj._ProjectionObject.BackColor = Color.WhiteSmoke;
                obj._ProjectionObject.ForeColor = Color.Black;
            }
            else
            {
                obj._ProjectionObject.BackColor = Color.SteelBlue;
                obj._ProjectionObject.ForeColor = Color.White;
            }
           GetSelectedProjectionName();
           cmdOK.Enabled = HasSelectedProjection();
        }

        private bool HasSelectedProjection()
        {

            try
            {
                foreach (Control ctr in pnlProjectionList.Controls)
                {
                    ProjectionControl _Projection = ctr as ProjectionControl;
                    if (_Projection.isPressed)
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

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
