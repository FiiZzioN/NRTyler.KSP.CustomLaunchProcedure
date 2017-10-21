// ***********************************************************************
// Assembly         : NRTyler.KSP.CustomLaunchProcedure
//
// Author           : Nicholas Tyler
// Created          : 09-20-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 09-21-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.IO;

namespace NRTyler.KSP.CustomLaunchProcedure
{
    /// <summary>
    /// Holds the methods that enable you to copy all the files and subdirectories in one location, and place them in another location.
    /// </summary>
    public class FileCopier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileCopier"/> class.
        /// </summary>
        public FileCopier() : this(new AppSetup())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCopier"/> class.
        /// </summary>
        /// <param name="appSetup">The application setup.</param>
        public FileCopier(AppSetup appSetup)
        {
            AppSetup = appSetup;
        }

        /// <summary>
        /// Gets or sets the application setup.
        /// </summary>
        public AppSetup AppSetup { get; set; }



        public void CopierLog(string beginningPath, string destinationPath)
        {
            var formatMessage = $"{beginningPath} --> {destinationPath}";
            Message.Write(formatMessage);
        }

        public void DirectoryCreatedLog(string path)
        {
            Message.Write($"Directory Created: {path}");
        }

        /// <summary>
        /// Takes all files and subdirectories in a given location and copies them to a new 
        /// location. The files being copied will be removed after the coping has taken place.
        /// </summary>
        /// <param name="beginningPath">The beginning path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="overwriteFiles">If set to true, any files that already exist in the destination will be overwritten.</param>
        /// <exception cref="DirectoryNotFoundException">Get's thrown if the beginning or destination path can't be found.</exception>
        public void CopyFilesAndSubdirectories(string beginningPath, string destinationPath, bool overwriteFiles)
        {
            // Make sure the directories we're working with actually exist.
            if (!Directory.Exists(beginningPath))
                throw new DirectoryNotFoundException($"'{beginningPath}' doesn't exist or can't be found!");

            // Be aware this is checking for the AppSetup destination directory, not this method's argument 'destinationPath'.
            if (!Directory.Exists(AppSetup.DestinationDirectory))
                throw new DirectoryNotFoundException($"'{AppSetup.DestinationDirectory}' doesn't exist or can't be found!");

            var beginningDirectory = new DirectoryInfo(beginningPath);

            // Get directories and files that will be worked with below.
            var baseDirectories = beginningDirectory.GetDirectories();
            var baseFiles       = beginningDirectory.GetFiles();

            // Makes sure the destination directory exists.
            if (!Directory.Exists(destinationPath))
            {
                var createDirectory = Directory.CreateDirectory(destinationPath);
                DirectoryCreatedLog(createDirectory.FullName);
            }

            // Copy files to the destination.
            foreach (var file in baseFiles)
            {
                var path = Path.Combine(destinationPath, file.Name);

                // Log copy process.
                CopierLog(beginningPath, path);
                file.CopyTo(path, overwriteFiles);
            }

            // Copy subdirectories plus their content to the destination. Accomplished via recursion.
            foreach (var directory in baseDirectories)
            {
                var path = Path.Combine(destinationPath, directory.Name);

                // Log copy process.
                CopierLog(beginningPath, path);
                CopyFilesAndSubdirectories(directory.FullName, path, overwriteFiles);
            }
        }

        /// <summary>
        /// Takes all files and subdirectories in a given location and copies them to a new 
        /// location. The files being copied will be removed after the coping has taken place.
        /// </summary>
        /// <param name="appSetup">The application setup holding the beginning and destination paths.</param>
        /// <param name="overwriteFiles">If set to true, any files that already exist in the destination will be overwritten.</param>
        /// <exception cref="DirectoryNotFoundException">Get's thrown if the beginning path can't be found.</exception>
        public void CopyFilesAndSubdirectories(AppSetup appSetup, bool overwriteFiles)
        {
            CopyFilesAndSubdirectories(appSetup.BeginningDirectory, appSetup.DestinationDirectory, overwriteFiles);
        }

    }
}