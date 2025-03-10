﻿using System.Collections.ObjectModel;
using ASiNet.VWA.Core;
using ASiNet.VWA.Core.Entities;
using ASiNet.VWA.Core.logging;

namespace ASiNet.App.VWA.ViewModel;
public partial class ConsolePageVM : WorkspaceWindowVM
{
    public ConsolePageVM()
    {
        Logger.RegisteredLog += OnRegisteredLog;
        Logger.Buffer.ForEach(x => Logs.Add(new(x)));
    }

    public ObservableCollection<LogVM> Logs { get; } = [];

    private void OnRegisteredLog(Log log)
    {
        Logs.Add(new(log));
    }


}
