using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using ASiNet.VWA.Core.Interfaces;
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

    public void RemoveElement(WorkspaceObject workspaceObject)
    {
        Area.Children.Remove(workspaceObject);
    }

    public void AddElement(Point pos, IWorkspaceObjectViewModel workspaceObject)
    {
        var inst = (WorkspaceObject)Activator.CreateInstance(workspaceObject.ObjectType, [WorkspaceAreaController])!;
        inst.DataContext = workspaceObject;
        AddElement(new(pos.X, pos.Y), inst);
    }

    public void AddElement(Point pos, WorkspaceObject workspaceObject)
    {
        _objects.Add(workspaceObject);
        Area.Children.Add(workspaceObject);
        workspaceObject.MoveElement(new(workspaceObject.Position.X, workspaceObject.Position.Y), Scale);
    }
}
