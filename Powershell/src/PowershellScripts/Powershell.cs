using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PowershellScripts
{
    public static class Powershell
    {
        public static object Enivronment { get; private set; }

        public static Task StartAsync()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"powershell.exe";
            startInfo.WorkingDirectory = @"D:\Development\Personal\Github\Samples\Powershell\src\PowershellScripts\";
            startInfo.Arguments = @" & '.\Test.ps1'";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();

                Console.WriteLine($"Script Output : {Environment.NewLine + output}");
                Console.WriteLine($"Script Errors : {Environment.NewLine + errors}");
            }

            return Task.CompletedTask;
        }
    }
}
