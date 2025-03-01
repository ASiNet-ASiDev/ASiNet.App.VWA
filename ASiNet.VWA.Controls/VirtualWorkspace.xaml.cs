using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ASiNet.VWA.Core;
using ASiNet.VWA.Core.Activity;
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
        WorkspaceContext = new WorkspaceContext(Root, Area);
        WorkspaceActivity.Initialize(WorkspaceContext);
        Root.SizeChanged += OnSizeChanged;
        WorkspaceContext.Transformer.StartScale(this, Area);
    }

    public IWorkspaceContext WorkspaceContext;

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

    public void AddElement(IWorkspaceObjectViewModel workspaceObject)
    {
        var instance = WorkspaceActivity.Current!.CreateWorkspaceObject(workspaceObject)!;
        AddElement(workspaceObject.Position, instance);
    }

    public void AddElement(Point pos, WorkspaceObject workspaceObject)
    {
        _objects.Add(workspaceObject);
        workspaceObject.OpeningCommand?.Execute(null);
        Area.Children.Add(workspaceObject);
        workspaceObject.MoveElement(new(-pos.X, -pos.Y), Scale);
        workspaceObject.OpenedCommand?.Execute(null);
    }
}
