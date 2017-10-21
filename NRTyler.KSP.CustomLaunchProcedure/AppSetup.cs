// ************************************************************************
// Assembly         : NRTyler.KSP.CustomLaunchProcedure
// 
// Author           : Nicholas Tyler
// Created          : 09-20-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 09-20-2017
// 
// License          : MIT License
// ***********************************************************************

using System;

namespace NRTyler.KSP.CustomLaunchProcedure
{
    /// <summary>
    /// Holds information directory info that must to be present for this application to run properly.
    /// </summary>
    public class AppSetup
    {
        public AppSetup() : this(null, null)
        {
            
        }

        public AppSetup(string directoryBeingCopied, string directoryBeingReplaced)
        {
            BeginningDirectory   = directoryBeingCopied;
            DestinationDirectory = directoryBeingReplaced;
        }

        /// <summary>
        /// The current directory that the program is located in.
        /// </summary>
        //public static readonly string CurrentDirectory = @"C:\SteamSwapOut\common\Kerbal Space Program v1.2.2";
        public static readonly string CurrentDirectory = Environment.CurrentDirectory;

        /// <summary>
        /// The path to the KSP GameData directory.
        /// </summary>
        public static readonly string GameData = $"{CurrentDirectory}/GameData";

        /// <summary>
        /// The path to the BackupFiles directory. This is where all backup files and directories are stored.
        /// </summary>
        public static readonly string BackupFiles = $"{CurrentDirectory}/BackupFiles";

        /// <summary>
        /// Gets or sets the path to the directory being copied.
        /// </summary>
        public string BeginningDirectory { get; set; }

        /// <summary>
        /// Gets or sets the path to the directory being replaced.
        /// </summary>
        public string DestinationDirectory { get; set; }
    }
}