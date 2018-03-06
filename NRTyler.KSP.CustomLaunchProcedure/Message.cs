// ***********************************************************************
// Assembly         : NRTyler.KSP.CustomLaunchProcedure
//
// Author           : Nicholas Tyler
// Created          : 09-21-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 03-05-2018
//
// License          : MIT License
// ***********************************************************************

using System;

namespace NRTyler.KSP.CustomLaunchProcedure
{
    /// <summary>
    /// Contains methods that help inform the user about what's currently happening via the console. <see cref="Message"/> 
    /// also has methods that deal with logging messages and actions to the <see cref="LoggingService"/>'s log file.
    /// </summary>
    public static class Message
    {
        /// <summary>
        /// Writes the specified message to the <see cref="Console" /> and the
        /// <see cref="LoggingService" />'s log file. Provides formating for the message.
        /// </summary>
        /// <param name="message">The message to send to the <see cref="Console" /> and the <see cref="LoggingService" />'s log file.</param>
        /// <returns>A formated string containing the date it was written, the time, and the message itself.</returns>
        public static string Write(string message)
        {
            return LogMessage(FormatMessage(message));
        }

        /// <summary>
        /// Formats the message for better readability in both the <see cref="Console"/> and the <see cref="LoggingService"/>'s log file. 
        /// </summary>
        /// <param name="message">The message to format.</param>
        /// <returns>The formated message.</returns>
        private static string FormatMessage(string message)
        {
            // Instantiates 'value' expecting the users message to be null, empty, or whitespace. Instead of 
            // applying the users message, we just stick a default "Message is empty." to take up the space instead.
            var value = $"| {CurrentDate()} | {CurrentTime()} | Message was empty.";

            // If the user message isn't null, empty, or whitespace, then it's actually included in the final formated message.
            if (!String.IsNullOrWhiteSpace(message))
            {
                value = $"| {CurrentDate()} | {CurrentTime()} | {message}";               
            }

            return value;
        }

        /// <summary>
        /// Provides no formatting, use the <see cref="Message.Write" /> method instead! Logs the message to both
        /// the <see cref="Console" /> and the <see cref="LoggingService" />'s log file with no formatting.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A formated string containing the date it was written, the time, and the message itself.</returns>
        public static string LogMessage(string message)
        {
            // To console:
            Console.WriteLine(message);

            // To log file:
            LoggingService.WriteToLog(message);

            return message;
        }

        /// <summary>
        /// Get the current time in a formatted string.
        /// </summary>
        /// <returns>The current time in a formatted string.</returns>
        private static string CurrentTime()
        {
            return DateTime.Now.ToLongTimeString();
        }

        /// <summary>
        /// Get the current date in a formatted string.
        /// </summary>
        /// <returns>The current date in a formatted string.</returns>
        private static string CurrentDate()
        {
            return DateTime.Now.ToShortDateString();
        }


        /// <summary>
        /// Formats the exception notification text, error message, and logging text.
        /// </summary>
        /// <param name="exception">The exception you've caught.</param>
        /// <param name="additionalInfo">The additional information that should be included in the error message.</param>
        /// <remarks>Yay for formatting! -_- ...</remarks>
        public static void FormatException(Exception exception, string additionalInfo = null)
        {
            // Grabbing the "nameof" the exception like this so when the actual exception 
            // comes through, it will get the correct name.
            var exceptionName = exception.GetType().Name;

            if (String.IsNullOrWhiteSpace(additionalInfo))
            {
                Message.Write($"A {exceptionName} has occurred! Exception details are listed below.");
            }

            Message.Write($"A {exceptionName} has occurred! {additionalInfo} Exception details are listed below.");
            Message.Write("Begin exception log.");
            Console.WriteLine();
            Message.Write("End exception log.");
            Console.WriteLine();            
        }
    }
}