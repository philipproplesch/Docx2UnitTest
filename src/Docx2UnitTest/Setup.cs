using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace devplex.Tools
{
    /// <summary>
    /// Setup for the Docx to UnitTest code generator.
    /// </summary>
    [RunInstaller(true)]
    public class Setup 
        : Installer
    {
        #region Install(IDictionary stateSaver)
        /// <summary>
        /// When overridden in a derived class, performs the installation.
        /// </summary>
        /// <param name="stateSaver">An 
        /// <see cref="T:System.Collections.IDictionary"/> used to save 
        /// information needed to perform a commit, rollback, or uninstall operation.</param>
        /// <exception cref="T:System.ArgumentException">The 
        /// <paramref name="stateSaver"/> parameter is null. </exception>
        /// <exception cref="T:System.Exception">An exception occurred in the 
        /// <see cref="E:System.Configuration.Install.Installer.BeforeInstall"/> 
        /// event handler of one of the installers in the collection.-or- An exception occurred in 
        /// the 
        /// <see cref="E:System.Configuration.Install.Installer.AfterInstall"/> 
        /// event handler of one of the installers in the collection. </exception>
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            RegisterAssembly("/codebase");
        } 
        #endregion

        #region Uninstall(IDictionary savedState)
        /// <summary>
        /// When overridden in a derived class, removes an installation.
        /// </summary>
        /// <param name="savedState">An 
        /// <see cref="T:System.Collections.IDictionary"/> that contains the state of 
        /// the computer after the installation was complete.</param>
        /// <exception cref="T:System.ArgumentException">The saved-state 
        /// <see cref="T:System.Collections.IDictionary"/> might have been 
        /// corrupted. </exception>
        /// <exception cref="T:System.Configuration.Install.InstallException">
        /// An exception occurred while uninstalling. This exception is ignored and the uninstall 
        /// continues. However, the application might not be fully uninstalled after the 
        /// uninstallation completes. </exception>
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            RegisterAssembly("/u");
        } 
        #endregion

        #region RegisterAssembly(string parameter)
        /// <summary>
        /// Registers the assembly.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void RegisterAssembly(string parameter)
        {
            var regasmPath =
                RuntimeEnvironment.GetRuntimeDirectory() + @"regasm.exe";
            var assemblyPath = GetType().Assembly.Location;

            var regasm =
                new ProcessStartInfo(
                    regasmPath,
                    string.Format("{0} \"{1}\"", parameter, assemblyPath))
                    {
                        WindowStyle = ProcessWindowStyle.Hidden
                    };

            Process.Start(regasm);
        } 
        #endregion
    }
}