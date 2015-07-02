using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using Leadtools.MedicalViewer;
using System.Windows.Forms;
//Leadtools Library
using Leadtools;
namespace VietBaIT.DROC
{
    public partial class frm_BrightnessController : Form
    {
        int _DcmViewerCellIndex;
        DROC_Ribbon CallForm;
        public MedicalViewer _AcqMedicalVwr;
        public frm_BrightnessController(DROC_Ribbon _CallForm, MedicalViewer _AcqMedicalVwr, int _CellIndex)
        {
            InitializeComponent();
            this.Load += new EventHandler(frm_BrightnessController_Load);
            this.KeyDown += new KeyEventHandler(frm_BrightnessController_KeyDown);
            TrackbarBright.ValueChanged += new EventHandler(TrackbarBright_ValueChanged);
            TrackbarContrast.ValueChanged += new EventHandler(TrackbarContrast_ValueChanged);
            this.CallForm = _CallForm;
            this._AcqMedicalVwr = _AcqMedicalVwr;
            this._DcmViewerCellIndex = _CellIndex;
        }
        public frm_BrightnessController()
        {
            InitializeComponent();
           
        }
        public int _Brightness
        {
            get { return TrackbarBright.Value; }
        }
        public int _Contrast
        {
            get { return TrackbarContrast.Value; }
        }
        void TrackbarContrast_ValueChanged(object sender, EventArgs e)
        {
            Contrast();
        }

        void TrackbarBright_ValueChanged(object sender, EventArgs e)
        {
            Bright();
        }

        void frm_BrightnessController_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        void frm_BrightnessController_Load(object sender, EventArgs e)
        {
            try
            {
                TrackbarBright.Value = ((MedicalViewerMultiCell)_AcqMedicalVwr.Cells[_DcmViewerCellIndex]).Image.PaintIntensity;
                TrackbarContrast.Value = ((MedicalViewerMultiCell)_AcqMedicalVwr.Cells[_DcmViewerCellIndex]).Image.PaintContrast;
            }
            catch (Exception ex)
            {
            }
        }
        private void Contrast()
        {
            try
            {
                int index1;
                index1 = 1;
                while (index1 <= ((MedicalViewerMultiCell)_AcqMedicalVwr.Cells[_DcmViewerCellIndex]).Image.PageCount)
                {
                    ((MedicalViewerMultiCell)_AcqMedicalVwr.Cells[_DcmViewerCellIndex]).Image.Page = index1;
                    ((MedicalViewerMultiCell)_AcqMedicalVwr.Cells[_DcmViewerCellIndex]).Image.PaintContrast = TrackbarContrast.Value;
                    ((MedicalViewerMultiCell)_AcqMedicalVwr.Cells[_DcmViewerCellIndex]).Invalidate();
                    index1 += 1;
                }
            }
            catch (Exception ex)
            {
               
            }

        }
        private void Bright()
        {
            try
            {
              
                int index1;
                index1 = 1;
                while (index1 <= ((MedicalViewerMultiCell)_AcqMedicalVwr.Cells[_DcmViewerCellIndex]).Image.PageCount)
                {
                    ((MedicalViewerMultiCell)_AcqMedicalVwr.Cells[_DcmViewerCellIndex]).Image.Page = index1;
                    ((MedicalViewerMultiCell)_AcqMedicalVwr.Cells[_DcmViewerCellIndex]).Image.PaintIntensity = TrackbarBright.Value;
                    ((MedicalViewerMultiCell)_AcqMedicalVwr.Cells[_DcmViewerCellIndex]).Invalidate();
                    index1 += 1;
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TrackbarBright_Scroll(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}