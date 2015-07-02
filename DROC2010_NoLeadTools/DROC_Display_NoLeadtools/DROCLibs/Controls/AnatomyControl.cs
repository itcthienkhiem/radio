using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.DROC;

namespace VietBaIT.Controls
{
    public partial class AnatomyControl : UserControl
    {
        public bool isPressed = false;
        public string Code = "";
       
        public string Vn_Name = "";
        public string En_Name = "";
        public int STT;
        frm_ChooseAnatomy Parent;
        public delegate void OnClick(AnatomyControl obj);
        public event OnClick _OnClick;
        public delegate void OnDoubleClick(AnatomyControl obj);
        public event OnDoubleClick _OnDoubleClick;
        public Button _AnatomyObject
        {
            get { return AnatomyObject; }
        }
        public AnatomyControl(frm_ChooseAnatomy Parent, string Code, string Vn_Name, string En_Name, int STT)
        {
            InitializeComponent();
            this.STT = STT;
            this.Parent = Parent;
            this.Code = Code;
            this.Vn_Name = Vn_Name;
            this.En_Name = En_Name;
            AnatomyObject.Text = Vn_Name;
            AnatomyObject.Click += new EventHandler(AnatomyObject_Click);
        }
        public AnatomyControl(string Code, string Vn_Name, string En_Name, int STT)
        {
            InitializeComponent();
            this.STT = STT;
            this.Code = Code;
            this.Vn_Name = Vn_Name;
            this.En_Name = En_Name;
            AnatomyObject.Text = Vn_Name;
            AnatomyObject.Click += new EventHandler(AnatomyObject_Click);
            AnatomyObject.DoubleClick += new EventHandler(AnatomyObject_DoubleClick);
        }

        void AnatomyObject_DoubleClick(object sender, EventArgs e)
        {
            _OnDoubleClick(this);
        }
        public void Reset()
        {
            isPressed = false;
            AnatomyObject.BackColor = Color.WhiteSmoke;
            AnatomyObject.ForeColor = Color.Black;

        }
        public AnatomyControl Copy()
        {
            AnatomyControl newObj = new AnatomyControl(this.Code, this.Vn_Name, this.En_Name,this.STT);
            return newObj;
        }
        void AnatomyObject_Click(object sender, EventArgs e)
        {
            _OnClick(this);
            //if (Parent!=null) Parent.ResetPreviousSelectedObject(Code);
            //isPressed = !isPressed;
            //if (!isPressed)
            //{
            //    AnatomyObject.BackColor = Color.WhiteSmoke;
            //    AnatomyObject.ForeColor = Color.Black;
            //}
            //else
            //{
            //    AnatomyObject.BackColor = Color.SteelBlue;
            //    AnatomyObject.ForeColor = Color.White;
            //}
            
        }
    }
}
