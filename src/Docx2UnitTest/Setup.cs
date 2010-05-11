using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Docx2UnitTest
{
    [RunInstaller(true)]
    public class Setup : Installer
    {
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            RegisterAssembly("/codebase");
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            RegisterAssembly("/u");
        }

        private void RegisterAssembly(string parameter)
        {
            string regasmPath = RuntimeEnvironment.GetRuntimeDirectory() + @"regasm.exe";
            string assemblyPath = GetType().Assembly.Location;

            var regasm = new ProcessStartInfo(regasmPath, string.Format("{0} \"{1}\"", parameter, assemblyPath));
            regasm.WindowStyle = ProcessWindowStyle.Hidden;

            Process.Start(regasm);
        }
    }
}