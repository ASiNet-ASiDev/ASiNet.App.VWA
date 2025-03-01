using System.Collections.ObjectModel;
using System.Windows;
using ASiNet.App.VWA.View.Pages;
using ASiNet.VWA.Core.Activity;
using ASiNet.VWA.Core.Entities;
using ASiNet.VWA.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.VWA.ViewModel;
public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        WorkspaceActivity.Initialized += OnActivityInitialized;
    }

    private void OnActivityInitialized(WorkspaceActivity activity)
    {
        activity.Register<ConsolePage, ConsolePageVM>(new("VWA Console", "Application console", "ASiDev"));
        WorkspaceActivity.Initialized -= OnActivityInitialized;
    }

    public ObservableCollection<IWorkspaceObjectViewModel> WorkspaceObjects { get; } = [];


    [RelayCommand]
    private void AddObject()
    {
        var info = WorkspaceActivity.Current!.EnumerateRegisteredObjects().First();
        WorkspaceObjects.Add(WorkspaceActivity.Current!.CreateWorkspaceObjectViewModel(info.Id, new(PositionType.LastActiveMousePositionRelativeToArea))!);
    }
}
