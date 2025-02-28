using System.Windows;
using ASiNet.VWA.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.VWA.ViewModel;
public partial class WorkspaceWindowVM : WorkspaceObjectVM, IWorkspaceWindowViewModel
{
    public WorkspaceWindowVM(Type objectType) : base(objectType)
    {
    }

    [ObservableProperty]
    public partial bool IsMinimize { get; set; }
}
