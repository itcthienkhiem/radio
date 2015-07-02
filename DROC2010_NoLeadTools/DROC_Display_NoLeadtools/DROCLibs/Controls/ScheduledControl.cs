using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace VietBaIT.Controls
{
    public partial class ScheduledControl : UserControl
    {
        public bool isPressed = false;
        public string A_Code = "";
        public string B_Code = "";
        public string fileName = "";
        public string DcmfileName = "";
        
        public string SeriesInstanceUID = "";
        public string StudyInstanceUID = "";
        public string SOPInstanceUID = "";
        public string Acqdate = "";
        public string P_Code = "";
        public string A_Vn_Name = "";
        public string A_En_Name = "";
        public string P_Vn_Name = "";
        public string P_En_Name = "";
        public string B_Vn_Name = "";
        public string B_En_Name = "";
        public int PrintCount = 0;
        public decimal kVp = 0M;
        public int mA = 0;
        public decimal mAs = 0;

        public int REG_ID ;
        public int DETAIL_ID = -1;
        public int A_STT;
        public int P_STT;
        public int Status = 0;//0=No Image,1=Has Image
        public delegate void OnClick(ScheduledControl obj);
        public event OnClick _OnClick;

        public delegate void OnNewScheduleClick(ScheduledControl obj);
        public event OnNewScheduleClick _OnNewScheduleClick;

        public delegate void OnDelScheduleClick(ScheduledControl obj);
        public event OnDelScheduleClick _OnDelScheduleClick;

        public delegate void OnRejectScheduleClick(ScheduledControl obj);
        public event OnRejectScheduleClick _OnRejectScheduleClick;

        public delegate void OnNewScheduleDoubleClick(ScheduledControl obj);
        public event OnNewScheduleDoubleClick _OnNewScheduleDoubleClick;

        public delegate void OnDelScheduleDoubleClick(ScheduledControl obj);
        public event OnDelScheduleDoubleClick _OnDelScheduleDoubleClick;

        public delegate void OnRejectScheduleDoubleClick(ScheduledControl obj);
        public event OnRejectScheduleDoubleClick _OnRejectScheduleDoubleClick;

        public delegate void OnKeyDown(ScheduledControl obj, KeyEventArgs e);
        public event OnKeyDown _OnKeyDown;
        public Button _AnatomyObject
        {
            get { return AnatomyObject; }
        }

        public ScheduledControl(string fileName, int REG_ID, int DETAIL_ID, string StudyInstanceUID, string SeriesInstanceUID, string SOPInstanceUID, string A_Code, string P_Code, string B_Code, string A_Vn_Name, string A_En_Name, string P_Vn_Name, string P_En_Name, string B_Vn_Name, string B_En_Name, decimal kVp, int mA, decimal mAs, int A_STT, int P_STT, int PrintCount, int Status)
        {
            InitializeComponent();
            this.kVp = kVp;
            this.StudyInstanceUID = StudyInstanceUID;
            this.SeriesInstanceUID = SeriesInstanceUID;
            this.SOPInstanceUID = SOPInstanceUID;
            this.mA = mA;
            this.mAs = mAs;
            bool DisplayAction = File.Exists(Application.StartupPath + @"\displayAct.cfg");
            lblNew.Visible = DisplayAction;
            lblReject.Visible = DisplayAction;
            lblDelete.Visible = DisplayAction;
            this.fileName = fileName;
            DcmfileName = this.fileName + ".DCM";
            this.REG_ID = REG_ID;
            this.DETAIL_ID = DETAIL_ID;
            this.A_Code = A_Code;
            this.B_Code = B_Code;
            this.P_Code = P_Code;
            this.PrintCount = PrintCount;
            this.A_Vn_Name = A_Vn_Name;
            this.A_En_Name = A_En_Name;
            this.P_Vn_Name = P_Vn_Name;
            this.P_En_Name = P_En_Name;
            this.B_Vn_Name = B_Vn_Name;
            this.B_En_Name = B_En_Name;

            this.A_STT = A_STT;
            this.P_STT = P_STT;
            this.Status = Status;
            AnatomyObject.Text = A_Vn_Name + "/" + P_Vn_Name;
            lblNew.MouseHover += new EventHandler(lblNew_MouseHover);
            lblNew.MouseLeave += new EventHandler(lblNew_MouseLeave);
            lblDelete.MouseHover += new EventHandler(lblNew_MouseHover);
            lblDelete.MouseLeave += new EventHandler(lblNew_MouseLeave);
            lblReject.MouseHover += new EventHandler(lblNew_MouseHover);
            lblReject.MouseLeave += new EventHandler(lblNew_MouseLeave);

            lblNew.DoubleClick += new EventHandler(lblNew_DoubleClick);
            lblReject.DoubleClick += new EventHandler(lblReject_DoubleClick);
            lblDelete.DoubleClick += new EventHandler(lblDelete_DoubleClick);

            lblNew.Click += new EventHandler(lblNew_Click);
            lblReject.Click += new EventHandler(lblReject_Click);
            lblDelete.Click += new EventHandler(lblDelete_Click);

            AnatomyObject.Click += new EventHandler(AnatomyObject_Click);
            AnatomyObject.KeyDown += new KeyEventHandler(AnatomyObject_KeyDown);
            this.KeyDown += new KeyEventHandler(ScheduledControl_KeyDown);
            //InvisibleButton();
            ResetStatus(Status);
        }
        
        public void UpdateFileName(string fileName)
        {
            this.fileName = fileName.ToUpper().Replace(".DCM", "").Replace(".RAW", "").Replace(".PNG", "");
            DcmfileName = this.fileName + ".DCM";
        }
        public ScheduledControl(string fileName, int REG_ID, int DETAIL_ID, string StudyInstanceUID, string SeriesInstanceUID, string SOPInstanceUID, string A_Code, string P_Code, string B_Code, string A_Vn_Name, string A_En_Name, string P_Vn_Name, string P_En_Name, string B_Vn_Name, string B_En_Name, decimal kVp, int mA, decimal mAs, int A_STT, int P_STT, int PrintCount, int Status, bool DisplayAction)
        {
            InitializeComponent();
            this.kVp = kVp;
            this.StudyInstanceUID = StudyInstanceUID;
            this.SeriesInstanceUID = SeriesInstanceUID;
            this.SOPInstanceUID = SOPInstanceUID;
            this.mA = mA;
            this.mAs = mAs;
            this.fileName = fileName;
            DcmfileName = this.fileName + ".DCM";
            lblNew.Visible = DisplayAction;
            lblReject.Visible = DisplayAction;
            lblDelete.Visible = DisplayAction;
            DcmfileName = fileName + ".DCM";
            this.REG_ID = REG_ID;
            this.DETAIL_ID = DETAIL_ID;
            this.A_Code = A_Code;
            this.B_Code = B_Code;
            this.P_Code = P_Code;
            this.PrintCount = PrintCount;
            this.A_Vn_Name = A_Vn_Name;
            this.A_En_Name = A_En_Name;
            this.P_Vn_Name = P_Vn_Name;
            this.P_En_Name = P_En_Name;
            this.B_Vn_Name = B_Vn_Name;
            this.B_En_Name = B_En_Name;

            this.A_STT = A_STT;
            this.P_STT = P_STT;
            this.Status = Status;
            AnatomyObject.Text = A_Vn_Name + "/" + P_Vn_Name;
            lblNew.MouseHover += new EventHandler(lblNew_MouseHover);
            lblNew.MouseLeave += new EventHandler(lblNew_MouseLeave);
            lblDelete.MouseHover += new EventHandler(lblNew_MouseHover);
            lblDelete.MouseLeave += new EventHandler(lblNew_MouseLeave);
            lblReject.MouseHover += new EventHandler(lblNew_MouseHover);
            lblReject.MouseLeave += new EventHandler(lblNew_MouseLeave);

            lblNew.DoubleClick += new EventHandler(lblNew_DoubleClick);
            lblReject.DoubleClick += new EventHandler(lblReject_DoubleClick);
            lblDelete.DoubleClick += new EventHandler(lblDelete_DoubleClick);

            lblNew.Click += new EventHandler(lblNew_Click);
            lblReject.Click += new EventHandler(lblReject_Click);
            lblDelete.Click += new EventHandler(lblDelete_Click);

            AnatomyObject.Click += new EventHandler(AnatomyObject_Click);
            AnatomyObject.KeyDown += new KeyEventHandler(AnatomyObject_KeyDown);
            this.KeyDown += new KeyEventHandler(ScheduledControl_KeyDown);
            //InvisibleButton();
            ResetStatus(Status);
        }
       public void UpdateFolder(string newFolder)
        {
            try
            {
                string tempFileName = this.fileName.ToUpper().Replace(".DCM","").Replace(".RAW","").Replace(".PNG","")+".DCM";
                this.fileName = newFolder + @"\" + Path.GetFileName(tempFileName).Replace(".DCM", "").Replace(".RAW", "").Replace(".PNG", "");
                DcmfileName = this.fileName + ".DCM";
            }
            catch
            {
            }
        }
        void lblDelete_Click(object sender, EventArgs e)
        {
            _OnDelScheduleClick(this);
        }

        void lblReject_Click(object sender, EventArgs e)
        {
            _OnRejectScheduleClick(this); 
        }

        void lblNew_Click(object sender, EventArgs e)
        {
            _OnNewScheduleClick(this); 
        }
        public void UpdateNewImage()
        {
            try
            {
                Image ThumbnailImg = GetThumbnailImg();
                if (ThumbnailImg != null)
                {
                    AnatomyObject.BackgroundImage = ThumbnailImg;
                    AnatomyObject.BackgroundImageLayout = ImageLayout.Zoom;
                    //AnatomyObject.Text = "";
                }
                else
                {
                    AnatomyObject.Text = "IMAGE NOT FOUND";
                    //AnatomyObject.Image = DROCLibs.Properties.Resources.CORRECT;
                }
            }
            catch
            {
            }
        }
        void lblNew_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                Label objlbl = sender as Label;
                objlbl.BorderStyle = BorderStyle.None;
            }
            catch
            {
            }
        }

        void lblNew_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Label objlbl = sender as Label;
                objlbl.BorderStyle = BorderStyle.FixedSingle;
            }
            catch
            {
            } 
        }

        void lblDelete_DoubleClick(object sender, EventArgs e)
        {
            _OnDelScheduleDoubleClick(this);
        }

        void lblReject_DoubleClick(object sender, EventArgs e)
        {
            _OnRejectScheduleDoubleClick(this);
        }

        void lblNew_DoubleClick(object sender, EventArgs e)
        {
            _OnNewScheduleDoubleClick(this);
        }

        void ScheduledControl_KeyDown(object sender, KeyEventArgs e)
        {
            _OnKeyDown(this, e); 
        }

        void AnatomyObject_KeyDown(object sender, KeyEventArgs e)
        {
           // _OnKeyDown(this, e); 
        }
        public void UpdateHardwareParams( decimal kVp ,int mA ,decimal mAs)
        {
            this.kVp = kVp;
            this.mA = mA;
            this.mAs = mAs;
        }
        public void ResetStatus(int Status)
        {
            lblNew.BackColor = AnatomyObject.BackColor;
            lblReject.BackColor = AnatomyObject.BackColor;
            lblDelete.BackColor = AnatomyObject.BackColor;
            this.Status = Status;
            AnatomyObject.ImageAlign = ContentAlignment.TopLeft;
            AnatomyObject.Text = A_Vn_Name + "/" + P_Vn_Name;
            AnatomyObject.Image = null;
            AnatomyObject.BackgroundImage = null;
            lblPrintCount.Visible = false;
            switch (Status)
            {
                case 0:
                    lblDelete.Enabled = true;
                    lblReject.Enabled = false;
                    //if (!isPressed) AnatomyObject.ForeColor = Color.Crimson;
                    if (!isPressed)
                        AnatomyObject.Image = DROCLibs.Properties.Resources.FILE_NEW;
                    else
                        AnatomyObject.Image = DROCLibs.Properties.Resources.TIME;
                    break;
                case 1:
                    lblDelete.Enabled = false;
                    lblReject.Enabled = true;
                    Image ThumbnailImg = GetThumbnailImg();
                    if (ThumbnailImg != null)
                    {
                        AnatomyObject.BackgroundImage = null;
                        AnatomyObject.BackgroundImage = ThumbnailImg;
                        AnatomyObject.BackgroundImageLayout = ImageLayout.Zoom;
                        //toolTip1.SetToolTip(AnatomyObject, AnatomyObject.Text);
                        AnatomyObject.Text = "";
                       // AnatomyObject.Image = null;
                    }
                    else
                    {
                        AnatomyObject.Image = DROCLibs.Properties.Resources.CORRECT;
                    }
                    if (PrintCount > 0)
                    {
                        lblPrintCount.Visible = true;
                        AnatomyObject.ImageAlign = ContentAlignment.BottomLeft;
                        AnatomyObject.Image = DROCLibs.Properties.Resources.PRINTER;
                        lblPrintCount.Text = PrintCount.ToString();
                    }
                    else
                    {
                        lblPrintCount.Text = "";
                        AnatomyObject.Image = null;
                    }
                    break;
                default:
                    AnatomyObject.Image = DROCLibs.Properties.Resources.FILE_NEW;
                    break;
            }
        }
        Image GetThumbnailImg()
        {
            try
            {
                string _ThumbnailFile = fileName.ToUpper().Replace("_THUMBNAIL","").Replace(".PNG", "") + "_THUMBNAIL.PNG";
                if (File.Exists(_ThumbnailFile))
                {
                    return Image.FromFile(_ThumbnailFile);
                    
                }
               
                return null;
            }
            catch
            {
                return null;
            }
        }
        public void ResetPrintCount()
        {
            try
            {
                if (PrintCount > 0)
                {
                    lblPrintCount.Visible = true;
                    AnatomyObject.ImageAlign = ContentAlignment.BottomLeft;
                    AnatomyObject.Image = DROCLibs.Properties.Resources.PRINTER;
                    lblPrintCount.Text = PrintCount.ToString();
                }
                else
                {
                    lblPrintCount.Text = "";
                    AnatomyObject.Image = null;
                }
            }
            catch
            {
            }
        }
       
        public void Reset()
        {
            isPressed = false;
            AnatomyObject.BackColor = Color.WhiteSmoke;
            AnatomyObject.ForeColor = Color.Black;
            ResetStatus(this.Status);

        }
        public ScheduledControl Copy()
        {
            ScheduledControl newObj = new ScheduledControl(this.fileName, this.REG_ID, this.DETAIL_ID, this.StudyInstanceUID, this.SeriesInstanceUID, this.SOPInstanceUID, this.A_Code, this.P_Code, this.B_Code, this.A_Vn_Name, this.A_En_Name, this.P_Vn_Name, this.P_En_Name, this.B_Vn_Name, this.B_En_Name, this.kVp, this.mA, this.mAs, this.A_STT, this.P_STT, this.PrintCount, this.Status);
            return newObj;
        }
        void AnatomyObject_Click(object sender, EventArgs e)
        {
            _OnClick(this);
           
        }
    }
}
