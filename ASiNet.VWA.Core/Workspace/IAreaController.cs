using System.Windows;

namespace ASiNet.VWA.Core.Workspace;
public interface IAreaController
{
    public bool IsResized { get; }

    public void RemoveElement(WorkspaceObject workspaceObject);

    public bool StartMove(IMovementElement element, bool isConsiderScale = true);

    public bool StartScale(IScaledElement element, IInputElement? relative = null);

    public bool StartResize(IResizedElement element);

    public void EndMove();

    public void EndScale();

    public void EndResize();

    public void Move(double scale);

    public void Scale(double scale);

    public void Resize(double scale);

    public Point TransformToRoot(UIElement element);

    public void UpdateAreaLayout();
}
