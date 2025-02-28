using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ASiNet.VWA.Core.Workspace;

namespace ASiNet.VWA.Controls;
public partial class WorkspaceObject : UserControl, IMovementElement, IScaledElement, IResizedElement
{
    public WorkspaceObject(WorkspaceAreaController areaController)
    {
        AreaController = areaController;
        this.RenderTransform = RootMatrix = new MatrixTransform();
    }

    public WorkspaceAreaController AreaController { get; set; }

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
}
