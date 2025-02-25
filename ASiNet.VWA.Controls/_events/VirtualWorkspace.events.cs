using System.Windows;
using System.Windows.Input;

namespace ASiNet.VWA.Controls;
public partial class VirtualWorkspace
{

    private void Area_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        WorkspaceAreaController.Scale(e.Delta >= 0 ? 1.1 : 0.9);
    }

    private void Area_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        WorkspaceAreaController.StartMove(this, false);
    }

    private void Area_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        WorkspaceAreaController.EndMove();
        WorkspaceAreaController.EndResize();
    }

    private void Area_MouseLeave(object sender, MouseEventArgs e)
    {
        WorkspaceAreaController.EndMove();
        WorkspaceAreaController.EndResize();
    }

    private void Root_MouseMove(object sender, MouseEventArgs e)
    {
        WorkspaceAreaController.Move(Scale);
        WorkspaceAreaController.Resize(Scale);
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        var l = (Root.ActualWidth - Area.ActualWidth) / 2;
        var h = (Root.ActualHeight - Area.ActualHeight) / 2;
        var newV = new Vector(l, h);
        newV.Negate();
        MoveElement(newV, 1);
        Root.SizeChanged -= OnSizeChanged;
    }
}