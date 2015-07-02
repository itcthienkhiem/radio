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
    public partial class OScheduledControl : UserControl
    {
        public bool isPressed = true;
        public string A_Code = "";
        public string B_Code = "";
        public string fileName = "";
        public string DcmfileName = "";
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
        public delegate void OnClick(OScheduledControl obj);
        public delegate void OnDoubleClick(OScheduledControl obj);
        public event OnDoubleClick _OnDoubleClick;
        public event OnClick _OnClick;
        public Button _AnatomyObject
        {
            get { return AnatomyObject; }
        }
        public Label _IconObject
        {
            get { return lblIcon; }
        }
        public Label _Title
        {
            get { return lblTitle; }
        }
        public OScheduledControl()
        {
            InitializeComponent();
          
        }
        
        void _IconObject_DoubleClick(object sender, EventArgs e)
        {
            _OnDoubleClick(this);
        }

        void lblTitle_DoubleClick(object sender, EventArgs e)
        {
            _OnDoubleClick(this);
        }
        public OScheduledControl(string fileName, int REG_ID, int DETAIL_ID, string A_Code, string P_Code, string B_Code, string A_Vn_Name, string A_En_Name, string P_Vn_Name, string P_En_Name, string B_Vn_Name, string B_En_Name, decimal kVp, int mA, decimal mAs, int A_STT, int P_STT, int PrintCount, int Status)
        {
            InitializeComponent();
            lblTitle.DoubleClick += new EventHandler(lblTitle_DoubleClick);
            lblIcon.DoubleClick += new EventHandler(_IconObject_DoubleClick);
            this.kVp = kVp;
            this.mA = mA;
            this.mAs = mAs;
            this.fileName = fileName;
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
            lblIcon.BackColor = Color.WhiteSmoke;
            this.A_STT = A_STT;
            this.P_STT = P_STT;
            this.Status = Status;
            AnatomyObject.Text = A_Vn_Name + "/" + P_Vn_Name;
            lblTitle.Text = A_Vn_Name + "/" + P_Vn_Name;
            //AnatomyObject.Click += new EventHandler(AnatomyObject_Click);
            ResetStatus(Status);
        }
        public void ResetStatus(int Status)
        {

            this.Status = Status;
            AnatomyObject.ImageAlign = ContentAlignment.TopLeft;
            AnatomyObject.Image = null;
            AnatomyObject.BackgroundImage = null;
            switch (Status)
            {
                case 0:
                    AnatomyObject.Text = "NO IMAGE";
                    //if (!isPressed) AnatomyObject.ForeColor = Color.Crimson;
                    if (!isPressed)
                        AnatomyObject.Image =  DROCLibs.Properties.Resources.FILE_NEW;
                    else
                        AnatomyObject.Image =  DROCLibs.Properties.Resources.TIME;
                    break;
                case 1:
                    Image ThumbnailImg = GetThumbnailImg();
                    if (ThumbnailImg != null)
                    {
                        AnatomyObject.BackgroundImage = ThumbnailImg;
                        AnatomyObject.BackgroundImageLayout = ImageLayout.Zoom;
                        toolTip1.SetToolTip(AnatomyObject, AnatomyObject.Text);
                        AnatomyObject.Text = "";
                       // AnatomyObject.Image = null;
                    }
                    else
                    {
                        AnatomyObject.Text = "IMAGE NOT FOUND";
                        AnatomyObject.Image =  DROCLibs.Properties.Resources.CORRECT;
                    }
                    
                    break;
                default:
                    AnatomyObject.Image =  DROCLibs.Properties.Resources.FILE_NEW;
                    break;
            }
        }
        Image GetThumbnailImg()
        {
            try
            {
                string _ThumbnailFile = fileName.ToUpper().Replace("_THUMBNAIL.PNG", "") + "_THUMBNAIL.PNG";
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
        public void Reset()
        {
            isPressed = false;
            AnatomyObject.BackColor = Color.WhiteSmoke;
            AnatomyObject.ForeColor = Color.Black;
            ResetStatus(this.Status);

        }
        public OScheduledControl Copy()
        {
            OScheduledControl newObj = new OScheduledControl(this.fileName, this.REG_ID, this.DETAIL_ID, this.A_Code, this.P_Code, this.B_Code, this.A_Vn_Name, this.A_En_Name, this.P_Vn_Name, this.P_En_Name, this.B_Vn_Name, this.B_En_Name,this.kVp,this.mA,this.mAs, this.A_STT, this.P_STT,this.PrintCount, this.Status);
            return newObj;
        }
        void AnatomyObject_Click(object sender, EventArgs e)
        {
            _OnClick(this);

        }

        private void lblIcon_Click(object sender, EventArgs e)
        {
            
        }

        private void AnatomyObject_Click_1(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblIcon.ImageIndex == 1)
                {
                    isPressed = true;
                    lblIcon.ImageIndex = 0;
                }
                else
                {
                    isPressed = false;
                    lblIcon.ImageIndex = 1;
                }
            }
            catch
            {
            }
        }

        private void AnatomyObject_Click_2(object sender, EventArgs e)
        {
            try
            {
                if (lblIcon.ImageIndex == 1)
                {
                    isPressed = true;
                    lblIcon.ImageIndex = 0;
                }
                else
                {
                    isPressed = false;
                    lblIcon.ImageIndex = 1;
                }
            }
            catch
            {
            }
        }

        private void lblIcon_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (lblIcon.ImageIndex == 1)
                {
                    isPressed = true;
                    lblIcon.ImageIndex = 0;
                }
                else
                {
                    isPressed = false;
                    lblIcon.ImageIndex = 1;
                }
            }
            catch
            {
            }
        }
    }
}
