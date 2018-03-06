// ************************************************************************
// Assembly         : NRTyler.KSP.CustomLaunchProcedure
// 
// Author           : Nicholas Tyler
// Created          : 03-05-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 03-05-2018
// 
// License          : MIT License
// ***********************************************************************

using System;
using System.Runtime.Serialization;

namespace NRTyler.KSP.CustomLaunchProcedure
{
    [Serializable]
    [DataContract(Name = "ApplicationSettings")]
    public class ApplicationSettings
    {
        public ApplicationSettings()
        {

        }

        /// <summary>
        /// The name that this application's setting file will go by.
        /// </summary>
        public const string FileName = "CustomLaunchProcedureSettings";

        /// <summary>
        /// Gets or sets the name of the folder holding the backup files..
        /// </summary>
        [DataMember(Order = 1)]
        public string BackupFilesFolderName { get; set; }

        /// <summary>
        /// Gets or sets the name of the folders whose content is going to be replaced..
        /// </summary>
        [DataMember(Order = 1)]
        public string ReplacableFilesFolderName { get; set; }

        /// <summary>
        /// Gets or sets the name of the game's executable.
        /// </summary>
        [DataMember(Order = 1)]
        public string GameExecutableName { get; set; }
    }
}