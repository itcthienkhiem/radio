using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SubSonic;

namespace VIETBAIT.DICOMSERVER.SERVERMANAGER
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Process pc = Process.GetCurrentProcess();
            Directory.SetCurrentDirectory(pc.MainModule.FileName.Substring(0, pc.MainModule.FileName.LastIndexOf(@"\", StringComparison.Ordinal)));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmConfig() );
            Application.Run(new FrmMainConfig());
        }
    }

    internal class CustomSqlProvider : SqlDataProvider
    {
        public CustomSqlProvider(string connectionString)
        {
            DefaultConnectionString = connectionString;
        }

        public override string Name
        {
            get { return "ORM"; }
        }
    }
}