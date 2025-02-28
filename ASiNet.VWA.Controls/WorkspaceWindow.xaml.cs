using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ASiNet.VWA.Core;
using ASiNet.VWA.Core.Interfaces;
using ASiNet.VWA.Core.Workspace;

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

    public WorkspaceWindow(IAreaController workspaceAreaController) : base(workspaceAreaController)
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
                e.NewValue.ContainsPropertyTo(nameof(IWorkspaceWindowViewModel.IsMinimize), (o, n) => CreateBinding(this, o, IsMinimizeProperty, n));
                e.NewValue.ContainsPropertyTo(nameof(IWorkspaceWindowViewModel.Content), (o, n) => CreateBinding(this, o, ContentPageProperty, n));
                break;
            case nameof(ContentPage):
                if(e.OldValue is UIElement element)
                    Root.Children.Remove(element);
                if(e.NewValue is UIElement newElement)
                {
                    Root.Children.Add(newElement);
                    Panel.SetZIndex(newElement, 1);
                    Grid.SetRow(newElement, 1);
                }
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

    private void Pin_Click(object sender, RoutedEventArgs e)
    {
        IsPinned = !IsPinned;
    }

    private void Minimize_Click(object sender, RoutedEventArgs e)
    {
        IsMinimize = !IsMinimize;
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        ClosingCommand?.Execute(null);
        AreaController.RemoveElement(this);
        ClosedCommand?.Execute(null);
    }
}
