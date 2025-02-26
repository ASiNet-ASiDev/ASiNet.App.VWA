using System.Windows;
using System.Windows.Controls;
using ASiNet.VWA.Core.Workspace;

namespace ASiNet.VWA.Controls;
public partial class VirtualWorkspace : UserControl, IScaledElement, IMovementElement
{
    public VirtualWorkspace()
    {
        MaxZoom = 2;
        MinZoom = 0.15;
        Scale = 1;
        InitializeComponent();
        WorkspaceAreaController = new(Root, Area);
        Root.SizeChanged += OnSizeChanged;
        WorkspaceAreaController.StartScale(this, Area);
    }

    public WorkspaceAreaController WorkspaceAreaController;

    public void MoveElement(Vector offset, double scale)
    {
        var matrix = AreaMatrix.Matrix;
        offset.Negate();
        matrix.Translate(offset.X, offset.Y);
        AreaMatrix.Matrix = matrix;
        Position = new(matrix.OffsetX, matrix.OffsetY);
    }

    public void ScaleElement(Point position, double scale)
    {
        var matrix = AreaMatrix.Matrix;
        matrix.ScaleAtPrepend(scale, scale, position.X, position.Y);
        AreaMatrix.Matrix = matrix;
        Scale = matrix.M11;
    }
}
