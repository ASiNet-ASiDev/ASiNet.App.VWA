using System.Collections.ObjectModel;
using ASiNet.VWA.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.VWA.ViewModel;
public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        WorkspaceObjects.Add(new ConsolePageVM() { Position = new(-131072, -131072) });
    }

    public ObservableCollection<IWorkspaceObjectViewModel> WorkspaceObjects { get; } = [];
}
