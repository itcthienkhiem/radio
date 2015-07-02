using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
using VietBaIT.DROC;

namespace VietBaIT.Controls
{
    public partial class ProjectionControl : UserControl
    {
        
        public bool isPressed = false;
        public string Code = "";
        public string RAD_Code = "";
        public string Vn_Name = "";
        public string En_Name = "";
        public int STT;
        public bool IsUsed4Emerency = false;
        frm_ChooseProjection Parent;
        public delegate void OnClick(ProjectionControl obj);
        public event OnClick _OnClick;
        public Button _ProjectionObject
        {
            get { return ProjectionObject; }
        }
        public ProjectionControl(frm_ChooseProjection Parent, string Code, string Vn_Name, string En_Name, int STT,bool IsUsed4Emerency)
        {
            InitializeComponent();
            this.IsUsed4Emerency = IsUsed4Emerency;
            if (IsUsed4Emerency) ProjectionObject.Image = DROCLibs.Properties.Resources.HELP;
            this.STT = STT;
            this.Parent = Parent;
            this.Code = Code;
            this.Vn_Name = Vn_Name;
            this.En_Name = En_Name;
            ProjectionObject.Text = Vn_Name;
            ProjectionObject.Click += new EventHandler(ProjectionObject_Click);
        }
        public void MakeAsEmerency(bool IsUsed4Emerency)
        {
            try
            {
                this.IsUsed4Emerency = IsUsed4Emerency;
                if (IsUsed4Emerency) ProjectionObject.Image = DROCLibs.Properties.Resources.HELP;
                else ProjectionObject.Image = null;
            }
            catch
            {
            }
        }
        public ProjectionControl(string Code, string Vn_Name, string En_Name, int STT, bool IsUsed4Emerency)
        {
            InitializeComponent();
            this.IsUsed4Emerency = IsUsed4Emerency;
            if (IsUsed4Emerency) ProjectionObject.Image = DROCLibs.Properties.Resources.HELP;
            this.STT = STT;
            this.Code = Code;
            this.Vn_Name = Vn_Name;
            this.En_Name = En_Name;
            ProjectionObject.Text = Vn_Name;
            ProjectionObject.Click += new EventHandler(ProjectionObject_Click);
        }
        public void Reset()
        {
            isPressed = false;
            ProjectionObject.BackColor = Color.WhiteSmoke;
            ProjectionObject.ForeColor = Color.Black;

        }
        public ProjectionControl Copy()
        {
            ProjectionControl newObj = new ProjectionControl(this.Code, this.Vn_Name, this.En_Name, this.STT,this.IsUsed4Emerency);
            return newObj;
        }
        void ProjectionObject_Click(object sender, EventArgs e)
        {
            _OnClick(this);
            //isPressed = !isPressed;
            //if (!isPressed)
            //{
            //    ProjectionObject.BackColor = Color.WhiteSmoke;
            //    ProjectionObject.ForeColor = Color.Black;
            //}
            //else
            //{
            //    ProjectionObject.BackColor = Color.SteelBlue;
            //    ProjectionObject.ForeColor = Color.White;
            //}
            //if (Parent!=null) Parent.GetSelectedProjectionName();
            
        }
    }
}
