using System.Windows;
using System.Windows.Input;
using ASiNet.VWA.Core;
using ASiNet.VWA.Core.Interfaces;

namespace ASiNet.VWA.Controls;
public partial class WorkspaceWindow : WorkspaceObject
{
    public WorkspaceWindow() : base(null!)
    {
        InitializeComponent();
    }

    public WorkspaceWindow(WorkspaceAreaController workspaceAreaController) : base(workspaceAreaController)
    {
        InitializeComponent();
    }

    private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (IsPinned)
            return;
        AreaController.StartMove(this);
    }

    private void Header_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (IsPinned)
            return;
        AreaController.EndMove();
    }

    private void PinMenuItem_Click(object sender, RoutedEventArgs e)
    {
        IsPinned = !IsPinned;
    }

    private void MinimizeMenuItem_Click(object sender, RoutedEventArgs e)
    {
        IsMinimize = !IsMinimize;
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        switch (e.Property.Name)
        {
            case nameof(DataContext):
                e.NewValue.ContainsPropertyTo(nameof(IVirtualWindowViewModel.IsMinimize), (o, n) => CreateBinding(this, o, IsMinimizeProperty, n));
                break;
        }

        base.OnPropertyChanged(e);
    }
}
