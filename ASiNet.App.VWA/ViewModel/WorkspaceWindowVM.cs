﻿using System.Windows;
using ASiNet.VWA.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.VWA.ViewModel;
public partial class WorkspaceWindowVM : WorkspaceObjectVM, IVirtualWindowViewModel
{
    public WorkspaceWindowVM(Type objectType) : base(objectType)
    {
    }

    [ObservableProperty]
    public partial UIElement? Content { get; set; }

    [ObservableProperty]
    public partial bool IsMinimize { get; set; }
}
