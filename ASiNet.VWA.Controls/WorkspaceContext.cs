using System.Windows;
using System.Windows.Input;
using ASiNet.VWA.Core;
using ASiNet.VWA.Core.Workspace;

namespace ASiNet.VWA.Controls;
public class WorkspaceContext(VirtualWorkspace root, UIElement area) : IWorkspaceContext
{
    public IWorkspaceTransformer Transformer { get; } = new WorkspaceTransformer(root, area);
    public Point LastActiveMousePosition { get; set; }

    public Point GetMousePositionRelativeToArea()
    {
        var pos = Mouse.GetPosition(area);

        return pos;
    }

    public void RemoveElement(WorkspaceObject workspaceObject)
    {
        root.RemoveElement(workspaceObject);
    }



    public Point TransformToRoot(UIElement element)
    {
        return area.TransformToVisual(element).Transform(new(0, 0));
    }

    public void UpdateAreaLayout()
    {
        area.UpdateLayout();
    }
}
