using System.Windows.Media;
using ASiNet.VWA.Core.Entities;
using ASiNet.VWA.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.VWA.ViewModel;
public partial class LogVM : ObservableObject
{
    public LogVM(Log log)
    {
        Log = log;
        Time = log.Time;
        Message = log.Message;
        Type = log.Type;
        var color = Type switch
        {
            LogType.Information => Color.FromRgb(85, 85, 187),
            LogType.Warning => Color.FromRgb(187, 187, 85),
            LogType.Error => Color.FromRgb(187, 85, 85),
            _ => Color.FromRgb(85, 85, 187),
        };
        Foreground = new SolidColorBrush(color);
    }

    public Log Log { get; }
    [ObservableProperty]
    public partial DateTime Time { get; set; }
    [ObservableProperty]
    public partial string Message { get; set; } 
    [ObservableProperty]
    public partial LogType Type { get; set; }
    [ObservableProperty]
    public partial Brush Foreground { get; set; }
}
