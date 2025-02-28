using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ASiNet.VWA.Core.Interfaces;
using ASiNet.VWA.Core.logging;
using ASiNet.VWA.Core.Workspace;

namespace ASiNet.VWA.Core;
public class WorkspaceObject : UserControl, IMovementElement, IScaledElement, IResizedElement
{
    public WorkspaceObject(IAreaController areaController)
    {
        AreaController = areaController;
        this.RenderTransform = RootMatrix = new MatrixTransform();
    }

    public readonly static DependencyProperty PositionProperty = DependencyProperty.Register(nameof(Position), typeof(Point), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty MaxZoomProperty = DependencyProperty.Register(nameof(MaxZoom), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty MinZoomProperty = DependencyProperty.Register(nameof(MinZoom), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty ScaleProperty = DependencyProperty.Register(nameof(Scale), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty MaxXOffsetProperty = DependencyProperty.Register(nameof(MaxXOffset), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty MinXOffsetProperty = DependencyProperty.Register(nameof(MinXOffset), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty MaxYOffsetProperty = DependencyProperty.Register(nameof(MaxYOffset), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty MinYOffsetProperty = DependencyProperty.Register(nameof(MinYOffset), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty MaximumWidthProperty = DependencyProperty.Register(nameof(MaximumWidth), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty MaximumHeightProperty = DependencyProperty.Register(nameof(MaximumHeight), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty MinimumWidthProperty = DependencyProperty.Register(nameof(MinimumWidth), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty MinimumHeightProperty = DependencyProperty.Register(nameof(MinimumHeight), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty IsPinnedProperty = DependencyProperty.Register(nameof(IsPinned), typeof(bool), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty ContentHeightProperty = DependencyProperty.Register(nameof(ContentHeight), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty ContentWidthProperty = DependencyProperty.Register(nameof(ContentWidth), typeof(double), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty ClosingCommandProperty = DependencyProperty.Register(nameof(ClosingCommand), typeof(ICommand), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty ClosedCommandProperty = DependencyProperty.Register(nameof(ClosedCommand), typeof(ICommand), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty OpeningCommandProperty = DependencyProperty.Register(nameof(OpeningCommand), typeof(ICommand), typeof(WorkspaceObject), new PropertyMetadata(null));
    public readonly static DependencyProperty OpenedCommandProperty = DependencyProperty.Register(nameof(OpenedCommand), typeof(ICommand), typeof(WorkspaceObject), new PropertyMetadata(null));

    public ICommand? ClosingCommand { get => (ICommand?)GetValue(ClosingCommandProperty); set => SetValue(ClosingCommandProperty, value); }
    public ICommand? ClosedCommand { get => (ICommand?)GetValue(ClosedCommandProperty); set => SetValue(ClosedCommandProperty, value); }
    public ICommand? OpeningCommand { get => (ICommand?)GetValue(OpeningCommandProperty); set => SetValue(OpeningCommandProperty, value); }
    public ICommand? OpenedCommand { get => (ICommand?)GetValue(OpenedCommandProperty); set => SetValue(OpenedCommandProperty, value); }
    public double ContentHeight { get => (double)GetValue(ContentHeightProperty); set => SetValue(ContentHeightProperty, value); }
    public double ContentWidth { get => (double)GetValue(ContentWidthProperty); set => SetValue(ContentWidthProperty, value); }
    public bool IsPinned { get => (bool)GetValue(IsPinnedProperty); set => SetValue(IsPinnedProperty, value); }
    public Point Position { get => (Point)GetValue(PositionProperty); set => SetValue(PositionProperty, value); }
    public double MaxZoom { get => (double)GetValue(MaxZoomProperty); set => SetValue(MaxZoomProperty, value); }
    public double MinZoom { get => (double)GetValue(MinZoomProperty); set => SetValue(MinZoomProperty, value); }
    public double Scale { get => (double)GetValue(ScaleProperty); set => SetValue(ScaleProperty, value); }
    public double MaxXOffset { get => (double)GetValue(MaxXOffsetProperty); set => SetValue(MaxXOffsetProperty, value); }
    public double MaxYOffset { get => (double)GetValue(MaxYOffsetProperty); set => SetValue(MaxYOffsetProperty, value); }
    public double MinXOffset { get => (double)GetValue(MinXOffsetProperty); set => SetValue(MinXOffsetProperty, value); }
    public double MinYOffset { get => (double)GetValue(MinYOffsetProperty); set => SetValue(MinYOffsetProperty, value); }
    public double MaximumWidth { get => (double)GetValue(MaximumWidthProperty); set => SetValue(MaximumWidthProperty, value); }
    public double MaximumHeight { get => (double)GetValue(MaximumHeightProperty); set => SetValue(MaximumHeightProperty, value); }
    public double MinimumWidth { get => (double)GetValue(MinimumWidthProperty); set => SetValue(MinimumWidthProperty, value); }
    public double MinimumHeight { get => (double)GetValue(MinimumHeightProperty); set => SetValue(MinimumHeightProperty, value); }


    public IAreaController AreaController { get; set; }

    public MatrixTransform RootMatrix { get; set; }

    public virtual void MoveElement(Vector offset, double scale)
    {
        var matrix = RootMatrix.Matrix;
        offset.Negate();
        matrix.Translate(offset.X, offset.Y);
        RootMatrix.Matrix = matrix;
        Position = new(matrix.OffsetX, matrix.OffsetY);
    }

    public virtual void ResizeElement(Vector offset, double scale)
    {
        var newOffset = offset;
        var oldPos = AreaController.TransformToRoot(this);
        Width -= newOffset.X;
        Height -= newOffset.Y;
        AreaController.UpdateAreaLayout();
        var newPos = AreaController.TransformToRoot(this);
        var pos = oldPos - newPos;
        ContentHeight = Height;
        ContentWidth = Width;
        MoveElement(pos, 1);
    }

    public virtual void ScaleElement(Point position, double scale)
    {
        throw new NotImplementedException();
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        switch (e.Property.Name)
        {
            case nameof(DataContext):
                e.NewValue
                    .ContainsPropertyTo(nameof(IWorkspaceObjectViewModel.Position), (o, n) => CreateBinding(this, o, PositionProperty, n), x => Logger.Warning($"[BINDING_SKIPPED]: {x}"))
                    .ContainsPropertyTo(nameof(IWorkspaceObjectViewModel.Height), (o, n) => CreateBinding(this, o, ContentHeightProperty, n), x => Logger.Warning($"[BINDING_SKIPPED]: {x}"))
                    .ContainsPropertyTo(nameof(IWorkspaceObjectViewModel.Width), (o, n) => CreateBinding(this, o, ContentWidthProperty, n), x => Logger.Warning($"[BINDING_SKIPPED]: {x}"))
                    .ContainsPropertyTo(nameof(IWorkspaceObjectViewModel.IsPinned), (o, n) => CreateBinding(this, o, IsPinnedProperty, n), x => Logger.Warning($"[BINDING_SKIPPED]: {x}"))
                    .ContainsPropertyTo(nameof(WorkspaceObject.ClosingCommand), (o, n) => CreateBinding(this, o, ClosingCommandProperty, n, BindingMode.OneWay), x => Logger.Warning($"[BINDING_SKIPPED]: {x}"))
                    .ContainsPropertyTo(nameof(WorkspaceObject.ClosedCommand), (o, n) => CreateBinding(this, o, ClosedCommandProperty, n, BindingMode.OneWay), x => Logger.Warning($"[BINDING_SKIPPED]: {x}"))
                    .ContainsPropertyTo(nameof(WorkspaceObject.OpeningCommand), (o, n) => CreateBinding(this, o, OpeningCommandProperty, n, BindingMode.OneWay), x => Logger.Warning($"[BINDING_SKIPPED]: {x}"))
                    .ContainsPropertyTo(nameof(WorkspaceObject.OpenedCommand), (o, n) => CreateBinding(this, o, OpenedCommandProperty, n, BindingMode.OneWay), x => Logger.Warning($"[BINDING_SKIPPED]: {x}"));
                break;
        }

        base.OnPropertyChanged(e);
    }


    protected static void CreateBinding(DependencyObject target, object src, DependencyProperty property, string path, BindingMode mode = BindingMode.TwoWay)
    {
        var bind = new Binding(path)
        {
            Source = src,
            Mode = mode,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        };
        BindingOperations.SetBinding(target, property, bind);
    }
}
