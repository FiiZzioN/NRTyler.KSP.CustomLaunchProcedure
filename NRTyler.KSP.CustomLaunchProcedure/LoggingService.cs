// ***********************************************************************
// Assembly         : NRTyler.KSP.CustomLaunchProcedure
//
// Author           : Nicholas Tyler
// Created          : 09-21-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 09-22-2017
//
// License          : MIT License
// ***********************************************************************

using System.IO;

namespace NRTyler.KSP.CustomLaunchProcedure
{
    /// <summary>
    /// Handles everything pertaining to logging custom messages or application actions to a log file.
    /// </summary>
    public static class LoggingService
    {
        /// <summary>
        /// Gets the name of the log file.
        /// </summary>
        public static string LogFileName { get; } = "KSP.CustomLaunchProcedure.Log.txt";

        /// <summary>
        /// Gets the log file's path in its' entirety.
        /// </summary>
        public static string LogFilePath { get; } = $"{AppSetup.CurrentDirectory}/{LogFileName}";

        /// <summary>
        /// Gets the log file stream.
        /// </summary>
        private static FileStream LogFileStream { get; } = File.Open(LogFilePath, FileMode.OpenOrCreate, FileAccess.Write);

        /// <summary>
        /// Gets the <see cref="StreamWriter"/> for the log file.
        /// </summary>
        public static StreamWriter LogWriter { get; } = new StreamWriter(LogFileStream);

        /// <summary>
        /// Writes the message to the log file.
        /// </summary>
        /// <param name="message">The message to write to the log file.</param>
        public static void WriteToLog(string message)
        {
            LogWriter.WriteLine(message);
        }
    }
}