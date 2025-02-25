using System.Windows;

namespace ASiNet.VWA.Core.Workspace;
public interface IMovementElement
{

    public double MaxXOffset { get; set; }

    public double MaxYOffset { get; set; }

    public double MinXOffset { get; set; }

    public double MinYOffset { get; set; }

    public Point Position { get; set; }

    public void MoveElement(Vector offset, double scale);
}
