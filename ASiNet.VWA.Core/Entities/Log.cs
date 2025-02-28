using ASiNet.VWA.Core.Enums;

namespace ASiNet.VWA.Core.Entities;
public class Log(LogType type, DateTime time, string message)
{

    public LogType Type { get; } = type;

    public DateTime Time { get; } = time;

    public string Message { get; } = message;
}
