using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using ASiNet.VWA.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.VWA.ViewModel;
public partial class WorkspaceObjectVM : ObservableObject, IWorkspaceObjectViewModel
{

    public Guid RegisteredId { get; set; }


    [ObservableProperty]
    public partial double Width { get; set; } = 300;
    [ObservableProperty]
    public partial double Height { get; set; } = 200;
    [ObservableProperty]
    public partial Point Position { get; set; }
    [ObservableProperty]
    public partial bool IsPinned { get; set; }
    [ObservableProperty]
    public partial int ZIndex { get; set; }

    [RelayCommand]
    private void Closing()
    {

    }

    [RelayCommand]
    private void Closed()
    {

    }


#if DEBUG
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        Debug.WriteLine($"{e.PropertyName}");
        base.OnPropertyChanged(e);
    }
#endif
}
