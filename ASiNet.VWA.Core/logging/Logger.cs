using ASiNet.VWA.Core.Entities;
using ASiNet.VWA.Core.Enums;

namespace ASiNet.VWA.Core.logging;
public static class Logger
{
    public static LogLevel LogLevel { get; set; } = LogLevel.Information;

    public static event Action<Log>? RegisteredLog;

    public static IEnumerable<Log> Buffer => _buffer;

    public static int BufferSize
    { 
        get => field;
        set
        {
            if (BufferSize <= 0)
            {
                Error("Logging buffer maximum size must be greater than zero!");
                return;
            }
            field = value;
        }
        
    } = 128;


    private static object _locker = new();

    private static List<Log> _buffer = [];

    public static void Error(string message)
    {
        if (LogLevel > LogLevel.Error)
            return;
        lock (_locker)
        {
            var log = new Log(LogType.Error, DateTime.Now, message);
            RegisteredLog?.Invoke(log);
            UpdateBuffer(log);
        }
    }

    public static void Warning(string message)
    {
        if (LogLevel > LogLevel.Warning)
            return;
        lock (_locker)
        {
            var log = new Log(Enums.LogType.Warning, DateTime.Now, message);
            RegisteredLog?.Invoke(log);
            UpdateBuffer(log);
        }
    }

    public static void Info(string message)
    {
        if (LogLevel > LogLevel.Information)
            return;
        lock (_locker)
        {
            var log = new Log(Enums.LogType.Information, DateTime.Now, message);
            RegisteredLog?.Invoke(log);
            UpdateBuffer(log);
        }
    }

    public static void ErrorAsync(string message)
    {
        if (LogLevel > LogLevel.Error)
            return;
        Task.Run(() => Error(message));
    }

    public static void WarningAsync(string message)
    {
        if (LogLevel > LogLevel.Warning)
            return;
        Task.Run(() => Warning(message));
    }

    public static void InfoAsync(string message)
    {
        if (LogLevel > LogLevel.Information)
            return;
        Task.Run(() => Info(message));
    }

    private static void UpdateBuffer(Log log)
    {
        _buffer.Add(log);
        if (_buffer.Count > 100)
            _buffer.RemoveAt(0);
    }
}
