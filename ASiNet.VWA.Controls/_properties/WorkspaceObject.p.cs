using System.Windows;

namespace ASiNet.VWA.Controls;
public partial class WorkspaceObject
{
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
}
