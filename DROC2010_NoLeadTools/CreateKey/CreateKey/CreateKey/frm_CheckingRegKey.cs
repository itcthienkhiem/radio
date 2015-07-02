using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VietBaIT.DcmProcessing
{
    public partial class frm_CheckingRegKey : Form
    {
        public frm_CheckingRegKey()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 10;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
        }
        private IAsyncResult _ar;
        private void Startup()
        {
            EndInvoke(_ar);
            Application.DoEvents();
            try
            {
                do 
                {
                    Application.DoEvents();
                    //System.Threading.Thread.Sleep(10);
                    progressBar1.Value += 1;
                    Application.DoEvents();
                }
                while (Utils.bHasFound);
                Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }
        private delegate void StartupCallback();
        private void frm_CheckingRegKey_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            _ar = BeginInvoke(new StartupCallback(Startup));
        }
    }
}
