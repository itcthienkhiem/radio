using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using SubSonic;
using VIETBAIT.CONFIG;
using VIETBAIT.DICOMSERVER.SERVERMANAGER.Properties;

namespace VIETBAIT.DICOMSERVER.SERVERMANAGER
{
    public partial class FrmMainConfig : Office2007RibbonForm
    {
        #region Attributies

        private readonly Config _mainConfig = new Config();
        private Config _prnConfig;
        private Config _risConfig;
        string rislinkConfigFilename =string.Empty;

        #endregion

        #region Contructor

        public FrmMainConfig()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Nạp các cấu hình vào biến toàn cục
        /// </summary>
        private void LoadParameters()
        {
            try
            {
                //Load parameters for Tab Dicom Store Config
                txtServerAETitle.Text = _mainConfig.GetValueFromKey("serveraetitle");
                txtServerPort.Text = _mainConfig.GetValueFromKey("serverport");
                txtImagePath.Text = _mainConfig.GetValueFromKey("imagepath");
                txtMaKieuDv.Text = _mainConfig.GetValueFromKey("makieudv");
                if (_mainConfig.GetValueFromKey("runningmode") == "0") rbtfulloption.Checked = true;
                if (_mainConfig.GetValueFromKey("runningmode") == "1") rbtRisFeedbackOnly.Checked = true;

                //Load Parameters for TAB Dicom Worklist Config
                string rislinkPath = _mainConfig.GetValueFromKey("rislinkpath");
                txtRislinkPath.Text = rislinkPath;
                if (Directory.Exists(rislinkPath))
                {
                    rislinkConfigFilename = txtRislinkPath.Text + Path.DirectorySeparatorChar + "App.config";
                    LoadRislinkConfig(rislinkConfigFilename);
                }


                //Load Parameter for Tab Logging
                txtLogIP.Text = _mainConfig.GetValueFromKey("logip");
                txtLogPort.Text = _mainConfig.GetValueFromKey("logport");

                //Load Parameter for tab Service
                chkVietbaService.Checked = _mainConfig.GetValueFromKey("loadallservice") != "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Lưu cấu hình vào file để phục vụ cho services
        /// </summary>
        private void SaveParameters()
        {
            if (_mainConfig != null) _mainConfig.SaveConfig();
        }

        private void LoadRislinkConfig(string rislinkConfigFilename)
        {
            _risConfig = new Config(rislinkConfigFilename);
            txtWLIPAddress.Text = _risConfig.GetValueFromKey("wlipaddress");
            txtWLDelayTime.Text = _risConfig.GetValueFromKey("wldelaytime");
            txtWLServerAETitle.Text = _risConfig.GetValueFromKey("wlserveraetitle");
            txtWLPort.Text = _risConfig.GetValueFromKey("wlserverport");

            txtRisServerName.Text = _risConfig.GetValueFromKey("risservername");
            txtRisDataBase.Text = _risConfig.GetValueFromKey("risdatabase");
            txtRisUserName.Text = _risConfig.GetValueFromKey("risusername");
            txtRisPassword.Text = _risConfig.GetValueFromKey("rispassword");
        }

        private void SaveRisLinkConfig()
        {
            _risConfig = new Config(rislinkConfigFilename);
            _risConfig.SetValueForKey("wlipaddress", txtWLIPAddress.Text);
            _risConfig.SetValueForKey("wldelaytime", txtWLDelayTime.Text);
            _risConfig.SetValueForKey("wlserveraetitle", txtWLServerAETitle.Text);
            _risConfig.SetValueForKey("wlserverport", txtWLPort.Text);
            _risConfig.SetValueForKey("risservername", txtRisServerName.Text);
            _risConfig.SetValueForKey("risdatabase", txtRisDataBase.Text);
            _risConfig.SetValueForKey("risusername", txtRisUserName.Text);
            _risConfig.SetValueForKey("rispassword", txtRisPassword.Text);

            _risConfig.SaveConfig();
        }

        private void LoadPrinterConfig(string prnConfigFilename)
        {
            _prnConfig = new Config(prnConfigFilename);
            txtPrinterIP.Text = _prnConfig.GetValueFromKey("prnpaddress");
            numTimeout.Value = Convert.ToDecimal(_prnConfig.GetValueFromKey("prndelaytime"));
            txtPrinterAETitle.Text = _prnConfig.GetValueFromKey("prnaetitle");
            txtPrinterPort.Text = _prnConfig.GetValueFromKey("prnport");
        }

        private void SavePrinterConfig()
        {
            _prnConfig.SetValueForKey("prnpaddress", txtPrinterIP.Text);
            _prnConfig.SetValueForKey("prndelaytime", numTimeout.Value.ToString());
            _prnConfig.SetValueForKey("prnaetitle", txtPrinterAETitle.Text);
            _prnConfig.SetValueForKey("prnport", txtPrinterPort.Text);


            _prnConfig.SaveConfig();
        }

        /// <summary>
        /// Kiểm tra thông tin kết nối CSDL
        /// </summary>
        /// <param name="pserverName"></param>
        /// <param name="pdataBase"></param>
        /// <param name="puserName"></param>
        /// <param name="ppasword"></param>
        /// <returns></returns>
        private static bool CheckConnection(string pserverName, String pdataBase, string puserName, string ppasword)
        {
            bool result;
            try
            {
                var connectionString = new StringBuilder();
                connectionString.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}", "Server=", pserverName,
                                              "; DataBase=",
                                              pdataBase, "; UID=", puserName, ";Password=", ppasword, ";");
                var scn = new SqlConnection(connectionString.ToString());

                scn.Open();
                scn.Close();
                result = true;
            }

            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(ex.ToString());
                //throw;
            }

            return result;
        }

        private static void InitSubSonic(string sqlConnstr, string providerName)
        {
            try
            {
                DataService.Providers = new DataProviderCollection();
                var myProvider = new CustomSqlProvider(sqlConnstr);
                if (DataService.Providers[providerName] == null)
                {
                    DataService.Providers.Add(myProvider);
                    DataService.Provider = myProvider;
                }
                else
                {
                    DataService.Provider.DefaultConnectionString = sqlConnstr;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Trả về IP hiện tại của máy sử dụng
        /// </summary>
        /// <returns></returns>
        private static List<string> GetCurrentIpAddress()
        {
            var strHostName = Dns.GetHostName();
            var ipEntry = Dns.GetHostEntry(strHostName);
            var addr = ipEntry.AddressList;
            var listIp =
                addr.Where(address => address.AddressFamily == AddressFamily.InterNetwork).Select(
                    address => address.ToString()).ToList();
            return listIp;
        }


        /// <summary>
        /// Load Service to Listbox
        /// </summary>
        /// <param name="allservice"> allService =1 to Load All Service else load Vietba Service Only</param>
        private void LoadServiceToListbox(int allservice)
        {
            lbxServices.Items.Clear();
            foreach (object obj in groupPanel20.Controls)
            {
                if (obj.GetType() == typeof (TextBoxX))
                {
                    var t = (TextBoxX) obj;
                    t.Text = "";
                }
            }
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                if (allservice == 1)
                {
                    var li = new ListItem(service.DisplayName, service.ServiceName);
                    lbxServices.Items.Add(li);
                    continue;
                }
                else
                {
                    if (service.ServiceName.StartsWith("VietBa"))
                    {
                        var li = new ListItem(service.DisplayName, service.ServiceName);
                        lbxServices.Items.Add(li);
                    }
                }
                //Console .WriteLine(service.);
            }
            if (lbxServices.Items.Count > 0) lbxServices.SelectedIndex = 0;
        }

        /// <summary>
        /// Get Service Path from ServiceName
        /// </summary>
        /// <param name="pServiceName">Name of Service (Not Display Name)</param>
        /// <returns>Service Executive command</returns>
        private static ManagementObject GetServiceFromName(string pServiceName)
        {
            //using (var searcher = new
            //    ManagementObjectSearcher("SELECT PathName from Win32_Service where Name = " + "\"" + pServiceName + "\""))
            //    ret = searcher.Get().Cast<ManagementObject>().FirstOrDefault();

            var class1 = new ManagementClass("Win32_Service");

            return
                class1.GetInstances().Cast<ManagementObject>().FirstOrDefault(
                    ob => ob.GetPropertyValue("Name").ToString().Trim() == pServiceName);
        }

        /// <summary>
        /// This routine updates the start mode of the provided service.
        /// Add Reference to System.Management .net Assembly
        /// </summary>
        /// <param name="serviceName">Name of the service to be updated</param>
        /// <param name="serviceStart"></param>
        /// <param name="errorMsg">If applicable, error message assoicated with exception</param>
        /// <returns>Success or failure.  False is returned if service is not found.</returns>
        private static bool SetServiceStartupMode(string serviceName, ServiceStartMode serviceStart, out string errorMsg)
        {
            uint success = 1;
            errorMsg = string.Empty;
            string startMode = serviceStart.ToString();

            string filter =
                String.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", serviceName);

            var query = new ManagementObjectSearcher(filter);

            try
            {
                ManagementObjectCollection services = query.Get();

                foreach (ManagementObject service in services)
                {
                    ManagementBaseObject inParams = service.GetMethodParameters("ChangeStartMode");
                    inParams["startmode"] = startMode;

                    ManagementBaseObject outParams =
                        service.InvokeMethod("ChangeStartMode", inParams, null);
                    if (outParams != null) success = Convert.ToUInt16(outParams.Properties["ReturnValue"].Value);
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                throw;
            }
            return (success == 0);
        }

        #endregion

        #region Form Events Handles

        private void BtnExitClick(object sender, EventArgs e)
        {
            Dispose();
        }

        private void FrmConfigLoad(object sender, EventArgs e)
        {
            try
            {                

                Directory.SetCurrentDirectory(Application.StartupPath);

                LoadParameters();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        //private void BtnCheckConnectionClick(object sender, EventArgs e)
        //{
        //    lblChecking.Text = @"Checking .....";

        //    bool ok = CheckConnection(txtServerName.Text.Trim(), txtDataBase.Text.Trim(), txtUsername.Text.Trim(),
        //                              txtPassword.Text.Trim());
        //    if (ok)
        //    {
        //        const string strKetnoithanhcong = "Kết nối thành công";
        //        lblChecking.Text = strKetnoithanhcong;
        //        imageChecking.Image = Resources.agt_action_success;
        //        btnApplyConnection.Enabled = true;
        //    }
        //    else
        //    {
        //        const string strKetnoikhongthanhcong = "Kết nối không thành công";
        //        lblChecking.Text = strKetnoikhongthanhcong;
        //        imageChecking.Image = Resources.agt_action_fail;
        //        btnApplyConnection.Enabled = false;
        //    }
        //}

        //private void BtnApplyConnectionClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        _mainConfig.SetValueForKey("servername", txtServerName.Text);
        //        _mainConfig.SetValueForKey("database", txtDataBase.Text);
        //        _mainConfig.SetValueForKey("username", txtUsername.Text);
        //        _mainConfig.SetValueForKey("password", txtPassword.Text);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxEx.Show(ex.ToString());
        //    }
        //}

        private void BtnApplyStoreConfigClick(object sender, EventArgs e)
        {
            try
            {
                _mainConfig.SetValueForKey("serveraetitle", txtServerAETitle.Text);
                _mainConfig.SetValueForKey("serverport", txtServerPort.Text);
                _mainConfig.SetValueForKey("imagepath", txtImagePath.Text.Trim());
                _mainConfig.SetValueForKey("makieudv", txtMaKieuDv.Text.Trim());
                if (rbtfulloption.Checked) _mainConfig.SetValueForKey("runningmode", "0");
                if (rbtRisFeedbackOnly.Checked) _mainConfig.SetValueForKey("runningmode", "1");
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.ToString());
                throw;
            }
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            SaveParameters();
        }

        private void TabDevicesClick(object sender, EventArgs e)
        {
            //if (!_tabDeviceSelected)
            //{
            //    _tabDeviceSelected = true;
            //    LoadDevices();
            //}
        }

        private void BtnOpenConfigFileClick(object sender, EventArgs e)
        {
            var of = new FolderBrowserDialog();
            string rislinkpath = _mainConfig.GetValueFromKey("rislinkpath");

            of.SelectedPath = rislinkpath == "" ? Application.StartupPath : rislinkpath;

            if (of.ShowDialog() == DialogResult.OK)
            {
                txtRislinkPath.Text = of.SelectedPath;
                _mainConfig.SetValueForKey("rislinkpath", of.SelectedPath);
                _mainConfig.SaveConfig();
                rislinkConfigFilename = of.SelectedPath + Path.DirectorySeparatorChar + "App.config";
                LoadRislinkConfig(rislinkConfigFilename);
            }
        }

        private void BtnDicomWlConfigApplyClick(object sender, EventArgs e)
        {
            SaveRisLinkConfig();
        }

        private void BtnGetCurrentIpAddressClick(object sender, EventArgs e)
        {
            txtWLIPAddress.Text = GetCurrentIpAddress()[0];
        }

        private void BtnGetLogIpClick(object sender, EventArgs e)
        {
            //txtLogIP.Text = GetCurrentIpAddress()[0];
        }

        private void BtnLoggingApplyClick(object sender, EventArgs e)
        {
            try
            {
                _mainConfig.SetValueForKey("logip", txtLogIP.Text);
                _mainConfig.SetValueForKey("logport", txtLogPort.Text);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void TabServicesControllerClick(object sender, EventArgs e)
        {

            try
            {
                LoadServiceToListbox(Convert.ToInt32(_mainConfig.GetValueFromKey("loadallservice")));

            }
            catch (Exception)
            {
                
                throw;
            }           


            
        }

        private void ChkVietbaServiceCheckedChanged(object sender, EventArgs e)
        {
            if (chkVietbaService.Checked)
            {
                _mainConfig.SetValueForKey("loadallservice", "0");
                LoadServiceToListbox(0);
            }
            else
            {
                _mainConfig.SetValueForKey("loadallservice", "1");
                LoadServiceToListbox(1);
            }
        }

        private void LbxServicesSelectedIndexChanged(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            var li = (ListItem) lbxServices.SelectedItem;
            ManagementObject mo = GetServiceFromName(li.Value);
            if (mo == null) return;
            try
            {
                txtServiceName.Text = li.Value;
                txtServicePath.Text = mo["PathName"].ToString();
                txtServiceState.Text = mo.GetPropertyValue("State").ToString();
                txtServiceStartupMode.Text = mo["StartMode"].ToString();
                if (txtServiceStartupMode.Text == @"Auto") rbtServiceAutomatic.Checked = true;
                if (txtServiceStartupMode.Text == @"Manual") rbtServiceManual.Checked = true;
                if (txtServiceStartupMode.Text == @"Disabled") rbtServiceDisable.Checked = true;
                txtServiceType.Text = mo["ServiceType"].ToString();
                txtServiceDescription.Text = mo["Description"] == null ? "" : mo["Description"].ToString();
            }
            catch (Exception)
            {
            }


            myService = new ServiceController(li.Value);
            if (myService.Status == ServiceControllerStatus.Running)
            {
                btnStartService.HotTrackingStyle = eHotTrackingStyle.Color;
                btnStartService.Enabled = false;
                btnStopService.HotTrackingStyle = eHotTrackingStyle.Default;
                btnStopService.Enabled = true;
            }
            else
            {
                btnStartService.HotTrackingStyle = eHotTrackingStyle.Default;
                btnStartService.Enabled = true;
                btnStopService.HotTrackingStyle = eHotTrackingStyle.Color;
                btnStopService.Enabled = false;
            }
            UseWaitCursor = false;
        }

        private void btnStartService_Click(object sender, EventArgs e)
        {
            myService.Start();
            myService.WaitForStatus(ServiceControllerStatus.Running);
            MessageBox.Show(@"Start Service Success");
            LoadServiceToListbox(Convert.ToInt32(_mainConfig.GetValueFromKey("loadallservice")));
        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            myService.Stop();
            myService.WaitForStatus(ServiceControllerStatus.Stopped);
            MessageBox.Show(@"Stop Service Success");
            LoadServiceToListbox(Convert.ToInt32(_mainConfig.GetValueFromKey("loadallservice")));
        }

        private void rbtServiceAutomatic_CheckedChanged(object sender, EventArgs e)
        {
            string err;
            bool ret = SetServiceStartupMode(txtServiceName.Text, ServiceStartMode.Automatic, out err);
        }

        private void rbtServiceManual_CheckedChanged(object sender, EventArgs e)
        {
            string err;
            bool ret = SetServiceStartupMode(txtServiceName.Text, ServiceStartMode.Manual, out err);
        }

        private void rbtServiceDisable_CheckedChanged(object sender, EventArgs e)
        {
            string err;
            bool ret = SetServiceStartupMode(txtServiceName.Text, ServiceStartMode.Disabled, out err);
        }

        private void btnDicomPrinterApply_Click(object sender, EventArgs e)
        {
            SavePrinterConfig();
        }

        #endregion

        private void btnImagePath_Click(object sender, EventArgs e)
        {
            var of = new FolderBrowserDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                txtImagePath.Text = of.SelectedPath;
            }
        }
    }
}