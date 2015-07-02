using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace VietBaIT.DROC
{
    public partial class frm_UDM : Form
    {
        System.Threading.Thread t;
        System.Threading.Thread tUpLoad;
        public string fileDownload = "";
        public bool bCancel = true;
        public bool bDownloading = false;
        public bool bUploading = false;
        public bool bSuccess = false;
        public bool AbortThread = false;
        System.Net.WebClient WC = new WebClient();
        public frm_UDM()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(frm_UDM_KeyDown);
        }

        void frm_UDM_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdclose.PerformClick();
        }
        
        private void DownloadIMG()
        {
            

            try
            {
                DetectProxy();
                if (!Directory.Exists(txtDir.Text.Trim())) Directory.CreateDirectory(txtDir.Text.Trim());
                string FileName = Utils.GetRealtimeFileName(txtURL.Text.Substring(txtURL.Text.LastIndexOf(".") + 1));
                if (txtDir.Text.Trim().EndsWith(@"\") || txtDir.Text.Trim().EndsWith(@"/"))
                {
                }
                else txtDir.Text = txtDir.Text.Trim() + @"\";
                fileDownload = txtDir.Text.Trim() + FileName;
                WC.DownloadFile(txtURL.Text, txtDir.Text.Trim() + FileName);
                bSuccess = true;
                bDownloading = false;
            }
            catch
            {
                bDownloading = false;
                bSuccess = false;
            }
        }
        private void DetectProxy()
        {
            if (optAutoDetect.Checked)
            {
                WC.Proxy = WebProxy.GetDefaultProxy();
                WC.Proxy.Credentials = CredentialCache.DefaultCredentials;
            }
            else
            {
                if (!bCheckData()) return;
                WebProxy _Wp = new WebProxy(txtProxyServer.Text.Trim(),Convert.ToInt32( txtPort.Text.Trim()));
                _Wp.BypassProxyOnLocal = true;
                if (chkAuthentication.Checked)
                {
                    _Wp.Credentials = new NetworkCredential(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    _Wp.UseDefaultCredentials = true;
                    
                }
                WC.Proxy = _Wp;
            }
        }
        private WebProxy GetWebProxy()
        {
            WebProxy wp = null;
            if (optAutoDetect.Checked)
            {
                wp = WebProxy.GetDefaultProxy();
                wp.Credentials = CredentialCache.DefaultCredentials;
            }
            else
            {
                if (!bCheckData()) return null;
                wp = new WebProxy(txtProxyServer.Text.Trim(), Convert.ToInt32(txtPort.Text.Trim()));
                wp.BypassProxyOnLocal = true;
                if (chkAuthentication.Checked)
                {
                    wp.Credentials = new NetworkCredential(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    wp.UseDefaultCredentials = true;

                }

            }
            return wp;
        }
        private bool bCheckData()
        {
            if (txtProxyServer.Text.Trim() == "")
            {
                Utils.ShowMsg("Bạn phải nhập địa chỉ máy chủ Proxy!");
                tabControl1.SelectedTab = tabPage3;
                txtProxyServer.Focus();
                return false;
            }
            if (txtPort.Text.Trim() == "")
            {
                Utils.ShowMsg("Bạn phải nhập cổng truy cập!");
                tabControl1.SelectedTab = tabPage3;
                txtPort.Focus();
                return false;
            }
            if (!mdlStatic._IsNumeric(txtPort.Text.Trim()))
            {
                Utils.ShowMsg("Cổng truy cập phải là số!");
                tabControl1.SelectedTab = tabPage3;
                txtPort.Focus();
                return false;
            }
            if (chkAuthentication.Checked)
            {
                if (txtUsername.Text.Trim() == "")
                {
                    Utils.ShowMsg("Bạn phải nhập tên truy cập!");
                    tabControl1.SelectedTab = tabPage3;
                    txtUsername.Focus();
                    return false;
                }
            }
            return true;
        }
        private void cmdLoad_Click(object sender, EventArgs e)
        {
            try
            {
                lblCapacity.Visible = true;
                cmdOpen.Visible = false;
                lblResult.Visible = false;
                txtDir.Enabled = false;
                txtURL.Enabled = false;
                cmdDir.Enabled = false;
                cmdLoad.Enabled = false;
                lblStatus.Visible = true;
                picLoading.Visible = true;
                cmdCancel.Visible = true;
                bDownloading=true;
                this.Refresh();
                t = new System.Threading.Thread(new System.Threading.ThreadStart(DownloadIMG));
                t.Start();
                Application.DoEvents();
                do
                {

                    if (System.IO.File.Exists(fileDownload))
                    {
                        lblCapacity.Text = GetCapacityOfFile(fileDownload);
                    }
                    else
                    {
                        lblCapacity.Text = "0 (KB)";
                    }
                    Application.DoEvents();
                }
                while (bDownloading);
                bDownloading = false;
                Application.DoEvents();
                //-------------------------
                lblCapacity.Visible = false;
                txtDir.Enabled = true;
                txtURL.Enabled = true;
                cmdDir.Enabled = true;
                cmdLoad.Enabled = true;
                lblStatus.Visible = false;
                picLoading.Visible = false;
                cmdCancel.Visible = false;
                if (bSuccess)
                {
                    cmdOpen.Visible = true;
                    lblResult.Visible = true;
                    bSuccess = false;
                    lblResult.Text = "Đã download file vào thư mục bạn chọn!";
                }
                else
                {
                    cmdOpen.Visible = false;
                    lblResult.Visible = true;
                    lblResult.Text = "Chưa download được file!";
                }

            }
            catch(Exception ex)
            {
                lblResult.Visible = true;
                lblResult.Text = ex.Message;
                cmdOpen.Visible = false;
                lblCapacity.Visible = false;
                txtDir.Enabled = true;
                cmdCancel.Visible = false;
                txtURL.Enabled = true;
                cmdDir.Enabled = true;
                cmdLoad.Enabled = true;
                lblStatus.Visible = false;
                picLoading.Visible = false;
            }
        }

        private void frm_UDM_Load(object sender, EventArgs e)
        {
            cmdCancel.Visible = false;
            cmdOpen.Visible = false;
            lblResult.Visible = false;
            lblCapacity.Visible = false;
            lblStatus.Visible = false;
            picLoading.Visible = false;
            string reval = "Http://10.40.0.29:1080/BNP/UploadFile.aspx";
            txtUploadURL.Text = hrk.RegConfiguration.sDbnull(hrk.RegConfiguration.GetSettings("hrk", "DICOM", "UPLOADADDRESS"), reval);
        }
        private string GetCapacityOfFile(string filePath)
        {
            System.IO.FileInfo infor = new FileInfo(filePath);
            try
            {
                return (infor.Length / 1024).ToString() + " (KB)";
            }
            catch
            {
                return "";
            }
        }
        private void cmdOpen_Click(object sender, EventArgs e)
        {
            
            if (tUpLoad != null && tUpLoad.ThreadState == System.Threading.ThreadState.Running)
            {
                if (MessageBox.Show("Đang có tiến trình khác chạy, bạn có muốn hủy cả tiến trình này hay không?", "RISlink", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AbortThread = true;
                    Close();
                    bCancel = false;
                }
            }
            else
            {
                Close();
                bCancel = false;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn hủy việc download file hay không?", "RISlink", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (t==null && t.ThreadState != System.Threading.ThreadState.Running) return;
                    t.Abort();
                    t = null;
                    lblCapacity.Visible = false;
                    txtDir.Enabled = true;
                    txtURL.Enabled = true;
                    cmdDir.Enabled = true;
                    cmdLoad.Enabled = true;
                    lblStatus.Visible = false;
                    picLoading.Visible = false;
                    cmdCancel.Visible = false;
                    cmdOpen.Visible = false;
                    lblResult.Visible = false;
                    cmdLoad.Focus();
                }
                catch
                {
                    lblCapacity.Visible = false;
                    txtDir.Enabled = true;
                    txtURL.Enabled = true;
                    cmdDir.Enabled = true;
                    cmdLoad.Enabled = true;
                    lblStatus.Visible = false;
                    picLoading.Visible = false;
                    cmdCancel.Visible = false;
                    cmdOpen.Visible = false;
                    lblResult.Visible = false;
                    cmdLoad.Focus();
                    
                    t.Abort();
                    t = null;
                }
            }
        }

        private void cmdclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void optAutoDetect_CheckedChanged(object sender, EventArgs e)
        {
            grbProxy.Enabled = optUseConfig.Checked;
        }

        private void optUseConfig_CheckedChanged(object sender, EventArgs e)
        {
            grbProxy.Enabled = optUseConfig.Checked;
            if (optUseConfig.Checked) txtProxyServer.Focus();
        }

        private void chkAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            txtUsername.Enabled = chkAuthentication.Checked;
            txtPassword.Enabled = chkAuthentication.Checked;
            if (chkAuthentication.Checked) txtUsername.Focus();
        }

        private void cmdDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();
            if (fld.ShowDialog()== DialogResult.OK)
            {
                txtDir.Text = fld.SelectedPath;
                txtURL.Focus();
            }
        }

        private void cmdFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fld = new OpenFileDialog();
            if (fld.ShowDialog() == DialogResult.OK)
            {
                txtFileUpload.Text = fld.FileName;
                txtUploadURL.Focus();
            }
        }
        private void UploadIMG()
        {
            try
            {
                DetectProxy();
                if (!System.IO.File.Exists(txtFileUpload.Text.Trim()))
                {
                    lblUploadResult.Text = "File file bạn chọn để Upload không tồn tại. Mời bạn chọn lại";
                    cmdFile.Focus();
                    bUploading = false;
                    bSuccess = false;
                    return;
                }
                string URLUpLoad = txtUploadURL.Text;
                string FileUpLoad = txtFileUpload.Text;
                bSuccess=MyUploader(FileUpLoad, URLUpLoad);
                //WC.UploadFile(URLUpLoad, FileUpLoad);
                bUploading = false;
            }
            catch(Exception ex)
            {
                lblUploadResult.Text = ex.Message;
                bUploading = false;
                bSuccess = false;
            }
        }

        public bool MyUploader(string strFileToUpload, string strUrl)
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                string strFileFormName = "file";
                Uri oUri = new Uri(strUrl);
                string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
                // The trailing boundary string
                byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");
                // The post message header
                StringBuilder sb = new StringBuilder();
                sb.Append("--");
                sb.Append(strBoundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data; name=\"");
                sb.Append(strFileFormName);
                sb.Append("\"; filename=\"");
                sb.Append(Path.GetFileName(strFileToUpload));
                sb.Append("\"");
                sb.Append("\r\n");
                sb.Append("Content-Type: ");
                sb.Append("application/octet-stream");
                sb.Append("\r\n");
                sb.Append("\r\n");
                string strPostHeader = sb.ToString();
                byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);

                // The WebRequest
                HttpWebRequest oWebrequest = (HttpWebRequest)WebRequest.Create(oUri);
                oWebrequest.ContentType = "multipart/form-data; boundary=" + strBoundary;
                oWebrequest.Proxy = GetWebProxy();
                oWebrequest.PreAuthenticate = true;
                oWebrequest.Method = "HEAD";
                oWebrequest.Credentials = CredentialCache.DefaultCredentials;
                WebResponse oWResponse = oWebrequest.GetResponse();
                oWResponse.Close();
                //-----------------------------
                oWebrequest = (HttpWebRequest)WebRequest.Create(oUri);
                oWebrequest.ContentType = "multipart/form-data; boundary=" + strBoundary;
                oWebrequest.Proxy = GetWebProxy();
                oWebrequest.Credentials = CredentialCache.DefaultCredentials;
                oWebrequest.Method = "POST";
                oWebrequest.KeepAlive = false;
                oWebrequest.Timeout = System.Threading.Timeout.Infinite;
                oWebrequest.ProtocolVersion = HttpVersion.Version11;

                // This is important, otherwise the whole file will be read to memory anyway...
                oWebrequest.AllowWriteStreamBuffering = false;

                // Get a FileStream and set the final properties of the WebRequest
                FileStream oFileStream = new FileStream(strFileToUpload, FileMode.Open, FileAccess.Read);
                long FileLen = Convert.ToInt32(oFileStream.Length / 1024);
                long length = postHeaderBytes.Length + oFileStream.Length + boundaryBytes.Length;
                oWebrequest.ContentLength = length;
                Stream oRequestStream = oWebrequest.GetRequestStream();

                // Write the post header
                oRequestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                // Stream the file contents in small pieces (4096 bytes, max).
                byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)oFileStream.Length))];
                int bytesRead = 0;
                int TotalKb = 0;
                int Total = 0;
                while ((bytesRead = oFileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    Total += bytesRead;
                    TotalKb = Total / 1024;
                    lblUploadCapacity.Text = (Total / 1024).ToString() + " (KB)";
                    decimal cPercent = 0;
                    cPercent = (100 * TotalKb / FileLen);
                    lblPercent.Text = cPercent.ToString() + " %";
                    oRequestStream.Write(buffer, 0, bytesRead);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    GC.Collect();
                }

                oFileStream.Close();

                // Add the trailing boundary
                oRequestStream.Write(boundaryBytes, 0, boundaryBytes.Length);

                Stream s = oWResponse.GetResponseStream();
                StreamReader sr = new StreamReader(s);
                String sReturnString = sr.ReadToEnd();
                // Clean up
                oFileStream.Close();
                oRequestStream.Close();
                s.Close();
                sr.Close();
                bSuccess = true;
                bUploading = false;
                return true;
            }

            catch (Exception ex)
            {
                lblUploadResult.Text = ex.Message;
                bUploading = false;
                bSuccess = false;
                return false;

            }

        }

        private void cmdUpload_Click(object sender, EventArgs e)
        {
            try
            {
                hrk.RegConfiguration.SaveSettings("hrk", "DICOM", "UPLOADADDRESS", txtUploadURL.Text);
                lblUploadResult.Visible = false;
                txtFileUpload.Enabled = false;
                txtUploadURL.Enabled = false;
                cmdFile.Enabled = false;
                cmdUpload.Enabled = false;
                lblUploadStatus.Visible = true;
                picUpLoad.Visible = true;
                cmdCancelUpload.Visible = true;
                lblPercent.Visible = true;
                lblUploadCapacity.Visible = true;
                bUploading = true;
                this.Refresh();
                tUpLoad = new System.Threading.Thread(new System.Threading.ThreadStart(UploadIMG));
                tUpLoad.Start();
                Application.DoEvents();
                do
                {
                    Application.DoEvents();
                }
                while (bUploading);
                bUploading = false;
                Application.DoEvents();
                //-------------------------
                lblPercent.Visible = false;
                lblUploadResult.Visible = true;
                txtFileUpload.Enabled = true;
                txtUploadURL.Enabled = true;
                cmdFile.Enabled = true;
                lblUploadCapacity.Visible = false;
                cmdUpload.Enabled = true;
                lblUploadStatus.Visible = false;
                picUpLoad.Visible = false;
                cmdCancelUpload.Visible = false;
                if (bSuccess)
                {
                    lblUploadResult.Visible = true;
                    bSuccess = false;
                    lblUploadResult.Text = "Đã Upload file đến địa chỉ Web bạn chọn!";
                }
                else
                {
                    lblUploadResult.Visible = true;
                    //lblUploadResult.Text = "Không thể Upload file đến địa chỉ Web bạn chọn!";
                }

            }
            catch (Exception ex)
            {
                lblPercent.Visible = false;
                lblUploadResult.Visible = true;
                lblUploadResult.Text = ex.Message;
                txtFileUpload.Enabled = true;
                txtUploadURL.Enabled = true;
                lblUploadCapacity.Visible = false;
                cmdFile.Enabled = true;
                cmdUpload.Enabled = true;
                lblUploadStatus.Visible = false;
                picUpLoad.Visible = false;
                cmdCancelUpload.Visible = false;
            }
        }

        private void lblUploadCapacity_Click(object sender, EventArgs e)
        {

        }

        private void cmdCancelUpload_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn hủy việc Upload file hay không?", "RISlink", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (tUpLoad == null && tUpLoad.ThreadState != System.Threading.ThreadState.Running) return;
                    tUpLoad.Abort();
                    tUpLoad = null;
                    lblUploadResult.Visible = true;
                    txtFileUpload.Enabled = true;
                    txtUploadURL.Enabled = true;
                    cmdFile.Enabled = true;
                    lblUploadCapacity.Visible = false;
                    cmdUpload.Enabled = true;
                    lblUploadStatus.Visible = false;
                    picUpLoad.Visible = false;
                    cmdCancelUpload.Visible = false;
                    lblPercent.Visible = false;
                    cmdUpload.Focus();
                }
                catch
                {
                    lblPercent.Visible = false;
                    lblUploadResult.Visible = true;
                    txtFileUpload.Enabled = true;
                    txtUploadURL.Enabled = true;
                    cmdFile.Enabled = true;
                    lblUploadCapacity.Visible = false;
                    cmdUpload.Enabled = true;
                    lblUploadStatus.Visible = false;
                    picUpLoad.Visible = false;
                    cmdCancelUpload.Visible = false;
                    cmdUpload.Focus();

                    tUpLoad.Abort();
                    tUpLoad = null;
                }
            }
        }
    }
}
