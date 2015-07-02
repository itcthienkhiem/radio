using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using VIETBAIT.CONFIG;

namespace VIETBAIT.DICOMSCP
{
    public partial class DicomStoreService : ServiceBase
    {
        #region Attributies

        private string _aeTitle;
        private int _port;
        private string _storeLocation;

        #endregion

        public DicomStoreService()
        {
            InitializeComponent();
            var pc = Process.GetCurrentProcess();
            Directory.SetCurrentDirectory(pc.MainModule.FileName.Substring(0, pc.MainModule.FileName.LastIndexOf(@"\", System.StringComparison.Ordinal)));
            //if (Debugger.IsAttached)StartService();
        }

        protected override void OnStart(string[] args)
        {
            StartService();
        }

        private void StartService()
        {
            //System.Threading.Thread.Sleep(5000);
            var config = new Config();
            //Get Log parameters
            string logIp = config.GetValueFromKey("logip");
            string logPort = config.GetValueFromKey("logport");
            VBLogger.InitLogger(logIp, logPort);

            VBLogger.LogInfo("Getting Parameters");
            
            DicomScpHelper.GetRisLinkConnection();

            VBLogger.LogInfo("Getting Parameters success");

            _aeTitle = DicomScpHelper.GetAeTitle();
            _port = DicomScpHelper.GetPort();
            _storeLocation = DicomScpHelper.GetstoreLocation();
            VietBaIT.CommonLibrary.globalVariables.MaKieuDV = DicomScpHelper.GetMaKieuDv();
            StorageScp.StorageLocation = _storeLocation;
            StorageScp.StartListening(_aeTitle, _port);
        }

        protected override void OnStop()
        {
            StorageScp.StopListening(_port);
        }
    }
}