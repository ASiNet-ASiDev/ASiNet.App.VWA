using System.Windows;

namespace ASiNet.VWA.Core.Workspace;
public interface IWorkspaceContext
{
    public IWorkspaceTransformer Transformer { get; }

    public Point GetMousePositionRelativeToArea();

    public void RemoveElement(WorkspaceObject workspaceObject);

    public Point TransformToRoot(UIElement element);

    public void UpdateAreaLayout();
}
