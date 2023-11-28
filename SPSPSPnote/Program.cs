using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace SPSPSPnote
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string[] filesInHomeDir = Directory.GetFiles(homeDirectory);
            foreach (string filePath in filesInHomeDir)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "notepad.exe",
                        WorkingDirectory = homeDirectory,
                        UseShellExecute = true
                    };
                    Process childProcess = Process.Start(startInfo);

                    if (IsChildProcessOfCurrent(childProcess))
                    {
                        Console.WriteLine($"{childProcess.ProcessName} its child process.");
                    }
                    else
                    {
                        Console.WriteLine($"{childProcess.ProcessName} not child process.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            Console.ReadLine();
        }

        static bool IsChildProcessOfCurrent(Process process)
        {
            Process currentProcess = Process.GetCurrentProcess();
            while (process != null)
            {
                if (process.Id == currentProcess.Id)
                {
                    return true;
                }
                try
                {
                    process = Process.GetProcessById(process.Parent().Id);
                }
                catch (ArgumentException)
                {
                    process = null;
                }
            }
            return false;
        }
    }
}