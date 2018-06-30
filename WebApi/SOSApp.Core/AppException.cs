using SOSApp.Core.Enum;
using System;
using System.Runtime.Serialization;

namespace SOSApp.Core
{
    public class AppException : Exception
    {
        public string Code { get; set; }
        public string IPAddress { get; set; }
        public string Module { get; set; }
        public LogSeverityEnum LogSeverity { get; set; }
        public string InnerExceptionDetail { get; set; }
        public string FullDetail { get; set; }

        /// <summary>
        /// Initializes a new instance of the Exception class.
        /// </summary>
        public AppException() : base()
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
        public AppException(string message, Exception innerException, string page, string ipAddress, LogSeverityEnum severity)
            : base(message, innerException)
        {
        }

        public AppException(Exception exception, string code, string ipAddress, LogSeverityEnum severity)
            : base(exception.Message, exception.InnerException)
        {
            Code = code;
            IPAddress = ipAddress;
            LogSeverity = severity;
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AppException(string message, string code, string ipAddress, LogSeverityEnum severity, string module = null)
            : base(message)
        {
            Code = code;
            IPAddress = ipAddress;
            LogSeverity = severity;

            if (module != null)
                Module = module;
            else
                Module = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
		/// <param name="messageFormat">The exception message format.</param>
		/// <param name="args">The exception message arguments.</param>
        public AppException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected AppException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public AppException(string message, Exception innerException)
            : base(message, innerException)
        {
            if (innerException != null)
                InnerExceptionDetail = innerException.ToString();

            FullDetail = string.Format("Message: {0} StackTrace: {1} Inner Exception: {2}", this.Message, this.StackTrace, this.InnerExceptionDetail);
        }
    }
}
