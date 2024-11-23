using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Telegram_bots.Exceptions
{
    /// <summary>
    /// An exception that is raised when an incorrect request was made
    /// </summary>
    public class IncorrectRequestException : Exception
    {
        /// <summary>
        /// Causes an exception
        /// </summary>
        public IncorrectRequestException() : base("Incorrect request") { }
        /// <summary>
        /// Causes an exception
        /// </summary>
        /// <param name="message">Error message</param>
        public IncorrectRequestException(string? message) : base(message) { }
        /// <summary>
        /// Causes an exception
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="inner">Parental error</param>
        public IncorrectRequestException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Checks the value of "ok" in the JsonDocument and if it is "false" it throws an exception
        /// </summary>
        /// <param name="json">The JsonDocument that needs to be checked</param>
        public static void ThrowIfNotOk(JsonDocument json)
        {
            bool isOk = json.RootElement.GetProperty("ok").GetBoolean();
            if (!isOk)
            {
                string? description = json.RootElement.GetProperty("description").GetString();
                int errorCode = json.RootElement.GetProperty("error_code").GetInt32();
                if (description != null)
                {
                    Throw(description, errorCode);
                }
                else
                {
                    Throw(null, errorCode);
                }
            }
        }

        [DoesNotReturn]
        internal static void Throw(string? message, int errorCode) =>
            throw new IncorrectRequestException("[" + errorCode + "] " + message);
    }

}
