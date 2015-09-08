using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Trial.Scheduler.Client.Service;
using Trial.Scheduler.Contracts.Dto;

namespace Trial.Scheduler.Client
{
    public class CommandHandler : IRegistratorCallback
    {
        public ExecuteCommandResponse Execute(ExecuteCommandRequest request)
        {
            var sb = new StringBuilder();
            string command = string.Format("/C {0} {1}", request.CommandText, request.CommandParameters);
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = command,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true })
            {
                Console.WriteLine();
                Console.WriteLine(new string('-', 20));
                Console.WriteLine("Start command: {0} {1}", request.CommandText, request.CommandParameters);

                var thread = new Thread(Read);
                process.Start();
                thread.Start(new { sb, process });

                const int timeout = 30000;
                process.WaitForExit(timeout);
                thread.Join(timeout);

                Console.WriteLine(sb.ToString());
                Console.WriteLine(" Completed.");
            }

            return new ExecuteCommandResponse { CommandOutput = sb.ToString() };
        }

        private static void Read(dynamic parameter)
        {
            StringBuilder sb = parameter.sb;
            Process process = parameter.process;
            var buffer = new char[Console.BufferWidth];
            int read = 1;
            while (read > 0)
            {
                read = process.StandardOutput.Read(buffer, 0, buffer.Length);
                string data = read > 0 ? new string(buffer, 0, read) : null;
                sb.AppendLine(data);
            }
        }
    }
}