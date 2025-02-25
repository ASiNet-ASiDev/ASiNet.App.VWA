﻿using System.Collections.ObjectModel;
using System.Windows;

namespace ASiNet.VWA.Core.Interfaces;
public interface IVirtualWorkspaceViewModel
{
    public ObservableCollection<IVirtualWindowViewModel> VirtualWindows { get; }

    public double Scale { get; set; }

    public Point Position { get; set; }
}
