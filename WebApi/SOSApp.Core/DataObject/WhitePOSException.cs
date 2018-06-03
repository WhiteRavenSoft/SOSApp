using WhiteRaven.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WhiteRaven.Core.DataObject
{
    public class WhiteRavenException : Exception
    {
        public string PageName
        {
            get; set;
        }

        public string IPAddress
        {
            get; set;
        }

        public string ModuleName
        {
            get; set;
        }

        public LogSeverityEnum LogSeverity
        {
            get; set;
        }

        /// <summary>
        /// Initializes a new instance of the Exception class.
        /// </summary>
        public WhiteRavenException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        /// <param name="page">Page of error.</param>
        /// <param name="ipAddress">Usuario IP Address.</param>
        /// <param name="severity">Error Severity.</param>
        public WhiteRavenException(string message, Exception innerException, string page, string ipAddress, LogSeverityEnum severity)
            : base(message, innerException)
        {
        }

        public WhiteRavenException(Exception exception, string page, string ipAddress, LogSeverityEnum severity)
            : base(exception.Message, exception.InnerException)
        {
            PageName = page;
            IPAddress = ipAddress;
            LogSeverity = severity;
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public WhiteRavenException(string message, string page, string ipAddress, LogSeverityEnum severity, string module = null)
            : base(message)
        {
            PageName = page;
            IPAddress = ipAddress;
            LogSeverity = severity;

            if (module != null)
                ModuleName = module;
            else
                ModuleName = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
		/// <param name="messageFormat">The exception message format.</param>
		/// <param name="args">The exception message arguments.</param>
        public WhiteRavenException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data aWhiteRavenut the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information aWhiteRavenut the source or destination.</param>
        protected WhiteRavenException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public WhiteRavenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
