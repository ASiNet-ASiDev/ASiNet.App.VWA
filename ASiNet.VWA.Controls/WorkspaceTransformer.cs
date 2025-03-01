using System.Windows;
using System.Windows.Input;
using ASiNet.VWA.Core.Workspace;

namespace ASiNet.VWA.Controls;
public class WorkspaceTransformer(VirtualWorkspace root, UIElement area) : IWorkspaceTransformer
{
    public bool IsResized => _resizedElement is not null;

    public Point RelativeToRootPosition => Mouse.GetPosition(root);
    public Point RelativeToAreaPosition => Mouse.GetPosition(area);

    public Point LastActiveMousePosition { get; set; } = GetRelativePosition(area);

    private IMovementElement? _movementElement;
    private IResizedElement? _resizedElement;
    private IScaledElement? _scaledElement;

    private bool _isConsiderScale;

    private IInputElement? _scaleRelativeElement;

    private Point _oldMovementPosition;
    private Point _oldResizedPosition;


    public bool StartMove(IMovementElement element, bool isConsiderScale = true)
    {
        if (_movementElement is not null)
            return false;
        _isConsiderScale = isConsiderScale;
        _oldMovementPosition = RelativeToRootPosition;
        _movementElement = element;
        LastActiveMousePosition = GetRelativePosition(area);
        return true;
    }

    public bool StartScale(IScaledElement element, IInputElement? relative = null)
    {
        if (_scaledElement is not null)
            return false;
        _scaledElement = element;
        _scaleRelativeElement = relative is null ? root : relative;
        LastActiveMousePosition = GetRelativePosition(area);
        return true;
    }

    public bool StartResize(IResizedElement element)
    {
        if (_resizedElement is not null)
            return false;

        _oldResizedPosition = RelativeToRootPosition;
        _resizedElement = element;
        LastActiveMousePosition = GetRelativePosition(area);
        return true;
    }

    public void EndMove()
    {
        _movementElement = null;
        LastActiveMousePosition = GetRelativePosition(area);
    }

    public void EndScale()
    {
        _scaledElement = null;
        _scaleRelativeElement = null;
        LastActiveMousePosition = GetRelativePosition(area);
    }

    public void EndResize()
    {
        _resizedElement = null;
        LastActiveMousePosition = GetRelativePosition(area);
    }

    public void Move(double scale)
    {
        if (_movementElement is null)
            return;
        var position = RelativeToRootPosition;
        var offset = _isConsiderScale ?
            (_oldMovementPosition - position) / scale :
            _oldMovementPosition - position;
        _oldMovementPosition = position;
        _movementElement.MoveElement(offset, scale);
    }

    public void Scale(double scale)
    {
        if (_scaledElement is null)
            return;
        var position = GetRelativePosition(_scaleRelativeElement!);
        if (scale > 1 && _scaledElement!.IsMaximumZoom)
            return;
        if (scale < 1 && _scaledElement!.IsMinimumZoom)
            return;
        _scaledElement.ScaleElement(position, scale);
    }

    public void Resize(double scale)
    {
        if (_resizedElement is null)
            return;
        var position = RelativeToRootPosition;
        var offset = (_oldResizedPosition - position) / scale;
        if ((offset.X > 0 && _resizedElement.ContentWidth <= _resizedElement.MinimumWidth) ||
            (offset.X < 0) && _resizedElement.ContentWidth >= _resizedElement.MaximumWidth)
            offset.X = 0;
        if ((offset.Y > 0 && _resizedElement.ContentHeight <= _resizedElement.MinimumHeight) ||
            (offset.Y < 0) && _resizedElement.ContentHeight >= _resizedElement.MaximumHeight)
            offset.Y = 0;


        _oldResizedPosition = position;
        _resizedElement.ResizeElement(offset, scale);

    }


    private static Point GetRelativePosition(IInputElement element) => Mouse.GetPosition(element);
}
