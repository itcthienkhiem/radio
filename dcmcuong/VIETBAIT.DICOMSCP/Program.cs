using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace VIETBAIT.DICOMSCP
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()

        {
            
            //checker checker;
            //checker = new checker();
            //checker.ApplicationName = "Vietba Dicom Storage";
            
            //checker.CheckLicense();
            //if (Debugger.IsAttached)
            //{
            //    var s = new DicomStoreService();
            //}
            //else
            //{
            //    Thread.Sleep(10000);
            //    var servicesToRun = new ServiceBase[] { new DicomStoreService() };
            //    ServiceBase.Run(servicesToRun);
            //}


            //Thread.Sleep(10000);
            var servicesToRun = new ServiceBase[] { new DicomStoreService() };
            ServiceBase.Run(servicesToRun);
        }
    }
}