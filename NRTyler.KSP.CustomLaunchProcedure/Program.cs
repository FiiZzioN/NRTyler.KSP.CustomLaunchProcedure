// ***********************************************************************
// Assembly         : NRTyler.KSP.CustomLaunchProcedure
//
// Author           : Nicholas Tyler
// Created          : 09-20-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 03-09-2018
//
// License          : MIT License
// ***********************************************************************

using System;
using System.Diagnostics;
using System.IO;

namespace NRTyler.KSP.CustomLaunchProcedure
{
    public class Program
    {
        private static void Main()
        {
            OpeningSequence();

            using (LoggingService.LogWriter)
            {
                // Get our dynamic variables so they can be used in the application.
                var applicationSettings = LoadApplicationSettings();
                var appSetup            = SetupApplication(applicationSettings);

                CopyFiles(appSetup, true);

                LaunchGame(applicationSettings);
            }

            ClosingSequence(30);
        }

        /// <summary>
        /// The steps this application goes through to ensure everything is setup to run properly.
        /// </summary>
        private static void OpeningSequence()
        {
            Console.Title = "KSP Custom Launch Procedure";
            Message.Write("KSP Custom Launch Procedure is Starting!");

            LoggingService.DeleteLogFile();
        }

        /// <summary>
        /// Loads this application's settings that hold dynamic variables.
        /// </summary>
        private static ApplicationSettings LoadApplicationSettings()
        {
            Message.Write("Retrieving saved settings!");

            var repo = new SettingsRepo();

            return repo.Retrieve(ApplicationSettings.FileName);
        }

        /// <summary>
        /// Goes through the process of starting the game.
        /// </summary>
        /// <param name="applicationSettings">This application's settings.</param>
        private static void LaunchGame(ApplicationSettings applicationSettings)
        {
            var gameName = applicationSettings.GameExecutableName;

            var process = new Process
            {
                StartInfo =
                {
                    FileName       = $"{ApplicationDirectoryInfo.CurrentDirectory}/{gameName}.exe",
                    CreateNoWindow = true
                }
            };

            Message.Write("Process Starting!");
            process.Start();

            process.PriorityClass = ProcessPriorityClass.High;
            Message.Write($"Process' priority class set to '{process.PriorityClass.ToString()}'.");

            process.WaitForExit(1000);
            Message.Write("KSP Custom Launch Procedure has completed!");

            Message.Write($"This program's log file can be found at: {LoggingService.LogFilePath}");
        }


        /// <summary>
        /// Copies the files and subdirectories to the desired location.
        /// </summary>
        /// <param name="directoryInfo">This application's directory info.</param>
        /// <param name="overwriteFiles">If set to true, any files that already exist in the destination will be overwritten.</param>
        private static void CopyFiles(ApplicationDirectoryInfo directoryInfo, bool overwriteFiles)
        {
            var fileCopier = new FileCopier(directoryInfo);

            try
            {
                Message.Write("Attempting to copy files from backup.");

                fileCopier.CopyFilesAndSubdirectories(directoryInfo, overwriteFiles);

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
        private static ApplicationDirectoryInfo SetupApplication(ApplicationSettings applicationSettings)
        {
            Message.Write("Setting up application...");

            var appSetup = new ApplicationDirectoryInfo
            {
                BeginningDirectory = $"{ApplicationDirectoryInfo.CurrentDirectory}/{applicationSettings.BackupFilesFolderName}/{applicationSettings.ReplacableFilesFolderName}",
                DestinationDirectory = $"{ApplicationDirectoryInfo.GameData}/{applicationSettings.ReplacableFilesFolderName}"
            };

            Message.Write("Application setup complete.");

            return appSetup;
        }

        /// <summary>
        /// Contains the closing countdown logic that's executed before the <see cref="Console" /> closes.
        /// </summary>
        /// <param name="waitFor">How many seconds the closing sequence should last for.</param>
        private static void ClosingSequence(int waitFor)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            while (true)
            {
                // Trap the loop here until the desired amount of time has elapsed. 
                // Once the time has been reached, continue down the chain.
                if (stopwatch.Elapsed.Seconds < waitFor)
                {
                    continue;
                }

                stopwatch.Stop();
                break;
            }

            Process.GetCurrentProcess().Close();
        }
    }
}
