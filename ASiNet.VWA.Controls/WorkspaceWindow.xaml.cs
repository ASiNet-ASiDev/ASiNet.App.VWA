using System.Windows;
using System.Windows.Input;
using ASiNet.VWA.Core;
using ASiNet.VWA.Core.Interfaces;

namespace ASiNet.VWA.Controls;
public partial class WorkspaceWindow : WorkspaceObject
{
    public WorkspaceWindow() : base(null!)
    {
        MinimumHeight = 100;
        MaximumHeight = 1024;
        MinimumWidth = 100;
        MaximumWidth = 1280;
        InitializeComponent();
    }

    public WorkspaceWindow(WorkspaceAreaController workspaceAreaController) : base(workspaceAreaController)
    {
        MinimumHeight = 100;
        MaximumHeight = 1024;
        MinimumWidth = 100;
        MaximumWidth = 1280;
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
            case nameof(IsMinimize):
                MinimizeUpdate();
                break;
            case nameof(DataContext):
                e.NewValue.ContainsPropertyTo(nameof(IVirtualWindowViewModel.IsMinimize), (o, n) => CreateBinding(this, o, IsMinimizeProperty, n));
                break;
        }

        base.OnPropertyChanged(e);
    }


    private void MinimizeUpdate()
    {
        if (IsMinimize)
        {
            ResizeHandler.Visibility = Visibility.Collapsed;
            Height = 20;

        }
        else
        {
            Height = ContentHeight;
            ResizeHandler.Visibility = Visibility.Visible;
        }
    }

    private void ResizeHandler_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (IsPinned)
            return;
        
        AreaController.StartResize(this);
    }

    private void ResizeHandler_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (IsPinned)
            return;
        AreaController.EndResize();
    }

    private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
    {
        ClosingCommand?.Execute(null);
        AreaController.RemoveElement(this);
        ClosedCommand?.Execute(null);
    }
}
