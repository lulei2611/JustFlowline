using JustFlowline.Interfaces;
using System;

namespace JustFlowline.Extensions
{
    public static class LoggerExtensions
    {
        private static readonly Func<object, Exception, string> _messageFormatter = MessageFormatter;

        //
        // 摘要:
        //     Formats and writes a debug log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogDebug(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, eventId, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a debug log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogDebug(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, eventId, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a debug log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogDebug(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a debug log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogDebug(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a trace log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogTrace(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, eventId, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a trace log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogTrace(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, eventId, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a trace log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogTrace(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a trace log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogTrace(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, message, args);
        }

        //
        // 摘要:
        //     Formats and writes an informational log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogInformation(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, eventId, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes an informational log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogInformation(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, eventId, message, args);
        }

        //
        // 摘要:
        //     Formats and writes an informational log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogInformation(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes an informational log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogInformation(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a warning log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogWarning(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, eventId, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a warning log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogWarning(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, eventId, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a warning log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogWarning(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a warning log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogWarning(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, message, args);
        }

        //
        // 摘要:
        //     Formats and writes an error log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogError(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, eventId, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes an error log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogError(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, eventId, message, args);
        }

        //
        // 摘要:
        //     Formats and writes an error log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogError(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes an error log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogError(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a critical log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogCritical(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, eventId, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a critical log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogCritical(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, eventId, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a critical log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogCritical(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a critical log message.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogCritical(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a log message at the specified log level.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void Log(this ILogger logger, LogLevel logLevel, string message, params object[] args)
        {
            logger.Log(logLevel, 0, null, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a log message at the specified log level.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, string message, params object[] args)
        {
            logger.Log(logLevel, eventId, null, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a log message at the specified log level.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void Log(this ILogger logger, LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            logger.Log(logLevel, 0, exception, message, args);
        }

        //
        // 摘要:
        //     Formats and writes a log message at the specified log level.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            logger.Log(logLevel, eventId, new FormattedLogValues(message, args), exception, _messageFormatter);
        }

        //
        // 摘要:
        //     Formats the message and creates a scope.
        //
        // 参数:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to create the scope in.
        //
        //   messageFormat:
        //     Format string of the log message in message template format. Example:
        //
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        //
        // 返回结果:
        //     A disposable scope object. Can be null.
        public static IDisposable BeginScope(this ILogger logger, string messageFormat, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            return logger.BeginScope(new FormattedLogValues(messageFormat, args));
        }

        private static string MessageFormatter(object state, Exception error)
        {
            return state.ToString();
        }
    }

    internal class FormattedLogValues
    {
        private readonly string _message;
        private readonly object[] _args;

        public FormattedLogValues(string message, object[] args)
        {
            _message = message;
            _args = args;
        }
    }
}
