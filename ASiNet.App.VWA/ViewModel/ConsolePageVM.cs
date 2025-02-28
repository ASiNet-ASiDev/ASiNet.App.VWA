using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.VWA.Core.Entities;
using ASiNet.VWA.Core.logging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.VWA.ViewModel;
public partial class ConsolePageVM : ObservableObject
{
    public ConsolePageVM()
    {
        Logger.RegisteredLog += OnRegisteredLog;
    }

    public ObservableCollection<LogVM> Logs { get; } = [];

    private void OnRegisteredLog(Log log)
    {
        Logs.Add(new(log));
    }


}
