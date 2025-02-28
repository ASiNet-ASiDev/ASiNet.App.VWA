using System.Windows;

namespace ASiNet.VWA.Controls;
public partial class WorkspaceWindow
{
    public readonly static DependencyProperty IsMinimizeProperty = DependencyProperty.Register(nameof(IsMinimize), typeof(bool), typeof(WorkspaceWindow), new PropertyMetadata(null));
    public readonly static DependencyProperty ContentPageProperty = DependencyProperty.Register(nameof(ContentPage), typeof(UIElement), typeof(WorkspaceWindow), new PropertyMetadata(null));
    public bool IsMinimize { get => (bool)GetValue(IsMinimizeProperty); set => SetValue(IsMinimizeProperty, value); }

    public UIElement? ContentPage { get => (UIElement?)GetValue(ContentPageProperty); set => SetValue(ContentPageProperty, value); }
}
