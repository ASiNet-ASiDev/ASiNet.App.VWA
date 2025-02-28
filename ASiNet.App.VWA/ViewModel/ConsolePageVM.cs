using System.Collections.ObjectModel;
using ASiNet.VWA.Core.Entities;
using ASiNet.VWA.Core.logging;

namespace ASiNet.App.VWA.ViewModel;
public partial class ConsolePageVM : WorkspaceWindowVM
{
    public ConsolePageVM() : base(typeof(View.Pages.ConsolePage))
    {
        Logger.RegisteredLog += OnRegisteredLog;
    }

    public ObservableCollection<LogVM> Logs { get; } = [];

    private void OnRegisteredLog(Log log)
    {
        Logs.Add(new(log));
    }


}
