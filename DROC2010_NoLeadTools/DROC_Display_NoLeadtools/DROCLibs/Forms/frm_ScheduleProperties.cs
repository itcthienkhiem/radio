using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.Controls;
using VietBaIT.DROC.Objects.ObjectController;
using VietBaIT.CommonLibrary;

namespace VietBaIT
{
    public partial class frm_ScheduleProperties : Form
    {
        ScheduledControl _ScheduledControl = null;
        int Device_ID = -1;
        public frm_ScheduleProperties(ScheduledControl _ScheduledControl,int Device_ID)
        {
            InitializeComponent();
            this.Device_ID = Device_ID;
            this._ScheduledControl = _ScheduledControl;
            this.KeyDown += new KeyEventHandler(frm_ScheduleProperties_KeyDown);
            this.Load += new EventHandler(frm_ScheduleProperties_Load);
        }

        void frm_ScheduleProperties_Load(object sender, EventArgs e)
        {
            ShowProperties();
        }
        void ShowProperties()
        {
            if (_ScheduledControl == null) return;
            lblName.Text = _ScheduledControl.DETAIL_ID.ToString() + ": " + _ScheduledControl.A_Code + " / " + _ScheduledControl.P_Code;
            lblHardwareParams.Text = "kVp = " + _ScheduledControl.kVp.ToString() + " mAs = " + _ScheduledControl.mAs;
            lblImgPath.Text = _ScheduledControl.DcmfileName;
           LoadIEConfig();
        }
        void LoadIEConfig()
        {
            try
            {
                int WW = -1;
                int WC = -1;
                DataSet ds = new ModalityController().GetIECONFIG(Device_ID, _ScheduledControl.A_Code, _ScheduledControl.P_Code, ref WW, ref WC);
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    WW = 0;
                    WC = 0;
                    //Thử load cấu hình đang được áp dụng cho một thiết bị bất kỳ trong hệ thống
                    ds = new ModalityController().GetIECONFIG(_ScheduledControl.A_Code, _ScheduledControl.P_Code);
                    if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        lblCIP.Text = "NONE";
                       
                        return;
                    }
                }
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    string s = "";
                    s = Utility.Int32Dbnull(dr["APPLY_INVERT_FIRST"], 0).ToString();
                    s+=Utility.Int32Dbnull(dr["AUTO_MIN_MAX_BIT"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["APPLY_HEC"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["HEC_STT"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["MED_STT"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["ID"], 0).ToString();
                    lblCIP.Text = Utility.sDbnull(dr["IE_NAME"], "NONE");
                    s += Utility.Int32Dbnull(dr["WL_STT"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["WL_CENTER"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["WL_WIDTH"], 0).ToString();
                    s += Utility.sDbnull(dr["WL_WOB"], 0) == "1" ? true : false;
                    s += Utility.Int32Dbnull(dr["APPLY_WL"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["LOW"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["HIGH"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["WL_LOW"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["WL_HIGH"], 0).ToString();
                   // ApplyWL = APPLY_WL == 1 ? true : false;
                    s += Utility.Int32Dbnull(dr["GAMMA_VALUE"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["MED_VALUE"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["CONTRAST_VALUE"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["BRIGHTNESS_VALUE"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["MSE_VALUE"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["LOW"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["HIGH"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["WL_LOW"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["WL_HIGH"], 0).ToString();

                    s += Utility.Int32Dbnull(dr["MSE_EC"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["MSE_EL"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["MSE_LC"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["MSE_LL"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["MSE_TYPE"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["MSE_ORDER"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["CONTRAST_ORDER"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["BRIGHTNESS_ORDER"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["GAMMA_ORDER"], 0).ToString();
                    s += Utility.sDbnull(dr["WOB"], 0) == "1" ? true : false;

                    s += Utility.sDbnull(dr["APPLY_MOTIONBLUR"], 0) == "1" ? true : false;
                    s += Utility.Int32Dbnull(dr["MB_DIMENSION"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["MB_ANGLE"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["MB_STT"], 0).ToString();


                    s += Utility.sDbnull(dr["APPLY_ANTIALIAS"], 0) == "1" ? true : false;
                    s += Utility.Int32Dbnull(dr["ANTIALIAS_THRESHOLD"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["ANTIALIAS_DIMENSION"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["ANTIALIAS_FILTER"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["ANTIALIAS_STT"], 0).ToString();


                    s += Utility.sDbnull(dr["MSE_APPLY_ELGE_EHANCEMENT"], 0) == "1" ? true : false;
                    s += Utility.sDbnull(dr["MSE_APPLY_LATITUDE_REDUCTION"], 0) == "1" ? true : false;

                    s += Utility.Int32Dbnull(dr["START_WIDTH"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["START_CENTER"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["END_WIDTH"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["END_CENTER"], 0).ToString();

                    s += Utility.Int32Dbnull(dr["INVERT_AFTER"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["INVERT_STT"], 0).ToString();
                    s += Utility.Int32Dbnull(dr["APPLY_INVERT"], 0).ToString();;
                    s += Utility.sDbnull(dr["APPLY_GAMMA"], 0) == "1" ? true : false;
                    s += Utility.sDbnull(dr["APPLY_CONTRAST"], 0) == "1" ? true : false;
                    s += Utility.sDbnull(dr["APPLY_MED"], 0) == "1" ? true : false;
                    s += Utility.sDbnull(dr["APPLY_BRIGHTNESS"], 0) == "1" ? true : false;
                    s += Utility.sDbnull(dr["APPLY_MSE"], 0) == "1" ? true : false;
                    lblCIPVal.Text=s;
                }
            }
            catch
            {
            }
        }
        void frm_ScheduleProperties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void cmdClose2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
