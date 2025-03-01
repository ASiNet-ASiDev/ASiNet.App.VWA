using System.Windows;
using ASiNet.VWA.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.VWA.ViewModel;
public partial class WorkspaceWindowVM : WorkspaceObjectVM, IWorkspaceWindowViewModel
{
    [ObservableProperty]
    public partial bool IsMinimize { get; set; }
}
