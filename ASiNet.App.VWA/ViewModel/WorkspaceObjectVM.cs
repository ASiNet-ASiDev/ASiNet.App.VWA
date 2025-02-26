using System.Windows;
using ASiNet.VWA.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.VWA.ViewModel;
public partial class WorkspaceObjectVM : ObservableObject, IWorkspaceObjectViewModel
{
    public WorkspaceObjectVM(Type objectType)
    {
        ObjectType = objectType;
    }

    public Type ObjectType { get; }

    [ObservableProperty]
    public partial double Width { get; set; }
    [ObservableProperty]
    public partial double Height { get; set; }
    [ObservableProperty]
    public partial Point Position { get; set; }
    [ObservableProperty]
    public partial bool IsPinned { get; set; }
    [ObservableProperty]
    public partial int ZIndex { get; set; }
}
