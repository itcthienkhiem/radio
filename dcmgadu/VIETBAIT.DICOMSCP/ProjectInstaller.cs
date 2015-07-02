using System.ComponentModel;
using System.Configuration.Install;

namespace VIETBAIT.DICOMSCP
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void DicomStoreServiceProcessInstaller1_BeforeInstall(object sender, InstallEventArgs e)
        {

        }
    }
}