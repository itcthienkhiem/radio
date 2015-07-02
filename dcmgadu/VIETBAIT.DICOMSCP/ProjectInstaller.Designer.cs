namespace VIETBAIT.DICOMSCP
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DicomStoreServiceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.DicomStoreServiceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // DicomStoreServiceProcessInstaller1
            // 
            this.DicomStoreServiceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.DicomStoreServiceProcessInstaller1.Password = null;
            this.DicomStoreServiceProcessInstaller1.Username = null;
            this.DicomStoreServiceProcessInstaller1.BeforeInstall += new System.Configuration.Install.InstallEventHandler(this.DicomStoreServiceProcessInstaller1_BeforeInstall);
            // 
            // DicomStoreServiceInstaller1
            // 
            this.DicomStoreServiceInstaller1.ServiceName = "VietBa Dicom Store";
            this.DicomStoreServiceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.DicomStoreServiceProcessInstaller1,
            this.DicomStoreServiceInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller DicomStoreServiceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller DicomStoreServiceInstaller1;
    }
}