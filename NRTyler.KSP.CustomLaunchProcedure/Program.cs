// ***********************************************************************
// Assembly         : NRTyler.KSP.CustomLaunchProcedure
//
// Author           : Nicholas Tyler
// Created          : 09-20-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 09-22-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.Diagnostics;
using System.IO;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.CodeLibrary.Utilities;

namespace NRTyler.KSP.CustomLaunchProcedure
{
    public class Program
    {
        private static void Main()
        {
            using (LoggingService.LogWriter)
            {
                //Message.LogService = new LoggingService();

                Message.Write("KSP Custom Launch Procedure is Starting!");

                var appSetup = SetupApplication();

                CopyFiles(appSetup, true);

                LaunchGame();
            }
        }

        private static void LaunchGame()
        {
            var gameName = "KSP_x64";

            var process = new Process
            {
                StartInfo =
                {
                    FileName = $"{AppSetup.CurrentDirectory}/{gameName}.exe",
                    CreateNoWindow = true
                }
            };

            Message.Write("Process Starting!");
            process.Start();

            process.PriorityClass = ProcessPriorityClass.High;
            Message.Write("Process' priority class set to 'High'.");

            process.WaitForExit(1000);
            Message.Write("KSP Custom Launch Procedure has completed!");

            Message.Write($"This program's log file can be found at: {LoggingService.LogFilePath}");
            ConsoleEx.ClosingMessage();
        }

        /// <summary>
        /// Copies the files and subdirectories to the desired location.
        /// </summary>
        /// <param name="appSetup">The application setup.</param>
        /// <param name="overwriteFiles">If set to true, any files that already exist in the destination will be overwritten.</param>
        private static void CopyFiles(AppSetup appSetup, bool overwriteFiles)
        {
            var fileCopier = new FileCopier(appSetup);

            try
            {
                Message.Write("Attempting to copy files from backup.");

                fileCopier.CopyFilesAndSubdirectories(appSetup, overwriteFiles);

                Message.Write("Copying Complete!");
            }
            catch (DirectoryNotFoundException exception)
            {
                Message.FormatException(exception, "The files and subdirectories have not been copied.");
            }
        }

        /// <summary>
        /// Sets various fields to the desired values before the application runs various methods.
        /// </summary>
        /// <returns>AppSetup.</returns>
        private static AppSetup SetupApplication()
        {
            Message.Write("Setting up application...");

            var appSetup = new AppSetup
            {
                BeginningDirectory   = $"{AppSetup.BackupFiles}/MechJeb2",
                DestinationDirectory = $"{AppSetup.GameData}/MechJeb2"
            };

            Message.Write("Application setup complete.");

            return appSetup;
        }
    }
}
