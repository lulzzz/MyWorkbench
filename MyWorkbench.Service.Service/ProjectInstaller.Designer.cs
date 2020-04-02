namespace MyWorkbench.Service {
    partial class ProjectInstaller {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.intelliServeProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.intelliServeInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // intelliServeProcessInstaller
            // 
            this.intelliServeProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.intelliServeProcessInstaller.Password = null;
            this.intelliServeProcessInstaller.Username = null;
            // 
            // intelliServeInstaller
            // 
            this.intelliServeInstaller.Description = "MyWorkbench cloud application service";
            this.intelliServeInstaller.DisplayName = "MyWorkbenchService";
            this.intelliServeInstaller.ServiceName = "MyWorkbenchService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.intelliServeProcessInstaller,
            this.intelliServeInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller intelliServeProcessInstaller;
        private System.ServiceProcess.ServiceInstaller intelliServeInstaller;
    }
}