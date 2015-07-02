using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.DROC.Objects.ObjectController;

namespace VietBaIT
{
    public partial class frm_ImgAlgorithsmList : Form
    {
        public frm_ImgAlgorithsmList()
        {
            InitializeComponent();
        }

        private void frm_ImgAlgorithsmList_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            try
            {
                DataTable dtIEData = new ModalityController().GetIEData("IS_ENABLE=1 OR IS_ENABLE IS NULL order by IE_NAME").Tables[0];
                if (dtIEData != null)
                {
                    dtIEData.DefaultView.Sort = "IE_Name";
                    lstIEList.DataSource = dtIEData.DefaultView;
                    lstIEList.DisplayMember = "IE_NAME";
                    lstIEList.ValueMember = "ID";

                }
            }
            catch
            {
            }
            finally
            {
                if (lstIEList.Items.Count > 0) lstIEList.SelectedIndex = 0;
            }
        }
        public int CurrentIE_ID = -1;
        public string ImgconfigName = "";
        private void lstIEList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                if (lstIEList.Items.Count <= 0 || lstIEList.SelectedItem == null || lstIEList.SelectedValue == null || lstIEList.SelectedIndex < 0)
                {
                    CurrentIE_ID = -1;
                    ImgconfigName = "NONE";
                    return;
                }
                CurrentIE_ID = Convert.ToInt32(((DataRowView)lstIEList.SelectedItem)["ID"].ToString());
                ImgconfigName = ((DataRowView)lstIEList.SelectedItem)["IE_nAME"].ToString();
            }
            catch
            {
            }
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
