using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ASiNet.VWA.Core.Workspace;
public interface IWorkspaceTransformer
{
    public bool IsResized { get; }

    public Point LastActiveMousePosition { get; set; }

    public bool StartMove(IMovementElement element, bool isConsiderScale = true);

    public bool StartScale(IScaledElement element, IInputElement? relative = null);

    public bool StartResize(IResizedElement element);

    public void EndMove();

    public void EndScale();

    public void EndResize();

    public void Move(double scale);

    public void Scale(double scale);

    public void Resize(double scale);
}
