using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Helper
{
    /// <summary>
    ///     Helper class for shell commands.
    /// </summary>
    public static class ShellHelper
    {
        /// <summary>
        ///     Escape an argument for inclusion within a shell command.
        /// </summary>
        /// <param name="argument">The argument to escape.</param>
        /// <param name="quote"></param>
        /// <returns>An escapd string.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string Escape(string argument, bool quote = false)
        {
            if (quote)
            {
                return "\"" + argument
                    .Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace(";", "\\;")
                       + "\"";
            }
            return argument
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace(";", "\\;");
        }

        /// <summary>
        ///     Execute a shell command.
        /// </summary>
        /// <param name="filename">The path to the executable.</param>
        /// <param name="arguments">The arguments to add.</param>
        /// <param name="timeout">The timeout for the command.</param>
        /// <returns>The output of the shell command.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="AbandonedMutexException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="SystemException"></exception>
        /// <exception cref="Exception"></exception>
        public static string ExecuteShellCommand(string filename, string arguments, int timeout = 9000)
        {
            string result;
            using (var process = new Process())
            {
                process.StartInfo.FileName = filename;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                //set it to english
                if (!process.StartInfo.EnvironmentVariables.ContainsKey("LC_ALL"))
                {
                    process.StartInfo.EnvironmentVariables.Add("LC_ALL", "C");
                }
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                var output = new StringBuilder();
                var error = new StringBuilder();

                using (var outputWaitHandle = new AutoResetEvent(false))
                using (var errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            output.AppendLine(e.Data);
                        }
                    };
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            errorWaitHandle.Set();
                        }
                        else
                        {
                            error.AppendLine(e.Data);
                        }
                    };
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    if (process.WaitForExit(timeout) &&
                        outputWaitHandle.WaitOne(timeout) &&
                        errorWaitHandle.WaitOne(timeout))
                    {
                        if (process.ExitCode == 0)
                        {
                            result = output.ToString();
                        }
                        else
                        {
                            throw new Exception(error.ToString());
                        }
                    }
                    else
                    {
                        // Timed out.
                        throw new Exception("Timed out");
                    }
                }
            }
            return result;
        }
    }
}