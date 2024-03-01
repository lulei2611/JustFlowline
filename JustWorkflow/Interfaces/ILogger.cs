using System;

namespace JustFlowline.Interfaces
{
    public interface ILogger
    {
        //
        // 摘要:
        //     Writes a log entry.
        //
        // 参数:
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   eventId:
        //     Id of the event.
        //
        //   state:
        //     The entry to be written. Can be also an object.
        //
        //   exception:
        //     The exception related to this entry.
        //
        //   formatter:
        //     Function to create a string message of the state and exception.
        void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);

        //
        // 摘要:
        //     Checks if the given logLevel is enabled.
        //
        // 参数:
        //   logLevel:
        //     level to be checked.
        //
        // 返回结果:
        //     true if enabled.
        bool IsEnabled(LogLevel logLevel);

        //
        // 摘要:
        //     Begins a logical operation scope.
        //
        // 参数:
        //   state:
        //     The identifier for the scope.
        //
        // 返回结果:
        //     An IDisposable that ends the logical operation scope on dispose.
        IDisposable BeginScope<TState>(TState state);
    }

    public readonly struct EventId
    {
        public int Id { get; }

        public string Name { get; }

        public static implicit operator EventId(int i)
        {
            return new EventId(i);
        }

        public static bool operator ==(EventId left, EventId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EventId left, EventId right)
        {
            return !left.Equals(right);
        }

        public EventId(int id, string name = null)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name ?? Id.ToString();
        }

        public bool Equals(EventId other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is EventId other)
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    //
    // 摘要:
    //     Defines logging severity levels.
    public enum LogLevel
    {
        //
        // 摘要:
        //     Logs that contain the most detailed messages. These messages may contain sensitive
        //     application data. These messages are disabled by default and should never be
        //     enabled in a production environment.
        Trace,
        //
        // 摘要:
        //     Logs that are used for interactive investigation during development. These logs
        //     should primarily contain information useful for debugging and have no long-term
        //     value.
        Debug,
        //
        // 摘要:
        //     Logs that track the general flow of the application. These logs should have long-term
        //     value.
        Information,
        //
        // 摘要:
        //     Logs that highlight an abnormal or unexpected event in the application flow,
        //     but do not otherwise cause the application execution to stop.
        Warning,
        //
        // 摘要:
        //     Logs that highlight when the current flow of execution is stopped due to a failure.
        //     These should indicate a failure in the current activity, not an application-wide
        //     failure.
        Error,
        //
        // 摘要:
        //     Logs that describe an unrecoverable application or system crash, or a catastrophic
        //     failure that requires immediate attention.
        Critical,
        //
        // 摘要:
        //     Not used for writing log messages. Specifies that a logging category should not
        //     write any messages.
        None
    }
}
