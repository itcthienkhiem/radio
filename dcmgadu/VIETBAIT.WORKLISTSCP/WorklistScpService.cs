using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using VietBaIT.CommonLibrary;
using VIETBAIT.CONFIG;
using VietBaIT.DICOM;


namespace VIETBAIT.WORKLISTSCP
{
    public partial class WorklistScpService : ServiceBase
    {
        #region Attributies               

        #endregion
        public WorklistScpService()
        {
            InitializeComponent();
            
            try
            {
                Process pc = Process.GetCurrentProcess();
            
                Directory.SetCurrentDirectory(pc.MainModule.FileName.Substring(0, pc.MainModule.FileName.LastIndexOf(@"\")));
                
                DataSet dsConfigXML = new DataSet();

                string sPathXML = "Config.XML";
                if (System.IO.File.Exists(sPathXML))
                {
                    dsConfigXML.ReadXml(sPathXML);
                    if (dsConfigXML.Tables[0].Rows.Count > 0)
                    {
                        globalVariables.ServerName = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["SERVERADDRESS"], ".");
                        globalVariables.sUName = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["USERNAME"], "sa");
                        globalVariables.sPwd = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["PASSWORD"], "sa");
                        globalVariables.sDbName = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["DATABASE_ID"], "RISLINK_DB");
                        globalVariables.sWLAETitle = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["WL_AETITLE"], "VIETBAIT");
                        globalVariables.iWLPort = Utility.Int32Dbnull(dsConfigXML.Tables[0].Rows[0]["WL_PORT"], 104);
                    }
                    //else
                    //{
                    //    Utility.ShowMsg("Không tìm thấy File Config.XML , Bạn xem lại", "Thông báo", MessageBoxIcon.Error);

                    //}
                }
                WorklistScp.StartListening(globalVariables.sWLAETitle, globalVariables.iWLPort);
            }
                
            catch (Exception)
            {
                
                throw;
            }
           
            
        }

        protected override void OnStart(string[] args)
        {
            
            Thread.Sleep(1000);

            //get database connection parameters
            string connectionstring = "";
            connectionstring = connectionstring + "data source=" + globalVariables.ServerName + ";";
            connectionstring = connectionstring + "initial catalog=" + globalVariables.sDbName + ";";
            connectionstring = connectionstring + "user id=" + globalVariables.sUName + ";";
            connectionstring = connectionstring + "password=" + globalVariables.sPwd + ";";

            //khởi tạo subsonic với provider riêng.
            Utility.InitSubSonic(connectionstring, "ORM");

            WorklistScp.StartListening(globalVariables.sWLAETitle, globalVariables.iWLPort);
        }

        protected override void OnStop()
        {
            WorklistScp.StopListening(globalVariables.iWLPort);
        }
    }
}
