using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ASiNet.VWA.Core.Interfaces;
using ASiNet.VWA.Core.Workspace;

namespace ASiNet.VWA.Core;
public partial class WorkspaceWindow : WorkspaceObject
{
    public WorkspaceWindow() : base(null!)
    {
        MinimumHeight = 100;
        MaximumHeight = 1024;
        MinimumWidth = 100;
        MaximumWidth = 1280;

        Width = 300;
        Height = 200;
    }

    public WorkspaceWindow(IAreaController areaController) : base(areaController)
    {
        MinimumHeight = 100;
        MaximumHeight = 1024;
        MinimumWidth = 100;
        MaximumWidth = 1280;

        Width = 300;
        Height = 200;
    }

    public readonly static DependencyProperty IsMinimizeProperty = DependencyProperty.Register(nameof(IsMinimize), typeof(bool), typeof(WorkspaceWindow), new PropertyMetadata(null));
    public bool IsMinimize { get => (bool)GetValue(IsMinimizeProperty); set => SetValue(IsMinimizeProperty, value); }

    private Rectangle ResizeHandler = null!;
    private Grid Root = null!;


    protected override void OnContentChanged(object oldContent, object newContent)
    {
        base.OnContentChanged(oldContent, newContent);
        if (newContent == Root)
            return;

        if (newContent is UIElement newElement)
        {
            Content = GenerateWindow();
            Root.Children.Add(newElement);
            Grid.SetRow(newElement, 1);
            Panel.SetZIndex(newElement, 1);
        }
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


    private Grid GenerateWindow()
    {
        var root = GenerateRootGrid();
        var header = GenerateHeader();
        var headerContextMenu = GenerateHeaderContextMenu();
        var resizeTrigger = GenerateResizeTrigger();

        root.Children.Add(header);
        Grid.SetRow(header, 0);
        header.ContextMenu = headerContextMenu;
        root.Children.Add(resizeTrigger);
        Grid.SetRow(resizeTrigger, 1);
        Panel.SetZIndex(resizeTrigger, 100);
        return root;
    }

    private Grid GenerateRootGrid()
    {
        Grid grid = new()
        {
            Name = "Root",
            Background = new SolidColorBrush(Colors.White)
        };
        RowDefinition rowDefinition = new()
        {
            Height = new(20, GridUnitType.Pixel)
        };
        grid.RowDefinitions.Add(rowDefinition);
        RowDefinition rowDefinition2 = new()
        {
            Height = new(1, GridUnitType.Star)
        };
        grid.RowDefinitions.Add(rowDefinition2);
        Root = grid;
        return grid;
    }

    private Grid GenerateHeader()
    {
        Grid grid = new()
        {
            Name = "Header",
            Background = new SolidColorBrush(Colors.White)
        };
        ColumnDefinition columnDefinition = new();
        grid.ColumnDefinitions.Add(columnDefinition);
        ColumnDefinition columnDefinition2 = new()
        {
            Width = new(20, GridUnitType.Pixel)
        };
        grid.ColumnDefinitions.Add(columnDefinition2);
        ColumnDefinition columnDefinition3 = new()
        {
            Width = new(20, GridUnitType.Pixel)
        };
        grid.ColumnDefinitions.Add(columnDefinition3);
        ColumnDefinition columnDefinition4 = new()
        {
            Width = new(20, GridUnitType.Pixel)
        };
        grid.ColumnDefinitions.Add(columnDefinition4);

        var pinBtn = new Button()
        {
            Content = ">"
        };
        var minimizeBtn = new Button()
        {
            Content = "__"
        };
        var closeBtn = new Button()
        {
            Content = "X"
        };

        grid.Children.Add(pinBtn);
        Grid.SetColumn(pinBtn, 1);
        grid.Children.Add(minimizeBtn);
        Grid.SetColumn(minimizeBtn, 2);
        grid.Children.Add(closeBtn);
        Grid.SetColumn(closeBtn, 3);

        grid.MouseLeftButtonDown += Header_MouseLeftButtonDown;
        grid.MouseLeftButtonUp += Header_MouseLeftButtonUp;

        pinBtn.Click += Pin_Click;
        minimizeBtn.Click += Minimize_Click;
        closeBtn.Click += Close_Click;
        return grid;
    }

    private ContextMenu GenerateHeaderContextMenu()
    {
        ContextMenu contextMenu = new();
        var pin = new MenuItem() { Header = "Pin" };
        contextMenu.Items.Add(pin);
        var minimize = new MenuItem() { Header = "Minimize" };
        contextMenu.Items.Add(minimize);
        contextMenu.Items.Add(new Separator());
        var close = new MenuItem() { Header = "Close" };
        contextMenu.Items.Add(close);

        pin.Click += PinMenuItem_Click;
        minimize.Click += MinimizeMenuItem_Click;
        close.Click += CloseMenuItem_Click;
        return contextMenu;
    }

    private Rectangle GenerateResizeTrigger()
    {
        Rectangle rectangle = new()
        {
            Height = 5,
            Width = 5,
            Cursor = Cursors.SizeNWSE,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Bottom,
            Fill = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0))
        };
        ResizeHandler = rectangle;

        rectangle.MouseLeftButtonDown += ResizeHandler_MouseLeftButtonDown;
        rectangle.MouseLeftButtonUp += ResizeHandler_MouseLeftButtonUp;
        return rectangle;
    }
}