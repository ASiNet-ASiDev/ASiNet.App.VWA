using System.Windows;

namespace ASiNet.VWA.Controls;
public partial class WorkspaceObject
{
    public double MaxXOffset { get; set; }

    public double MaxYOffset { get; set; }

    public double MinXOffset { get; set; }

    public double MinYOffset { get; set; }

    public double MaxZoom { get; set; }

    public double MinZoom { get; set; }

    public double Scale { get; set; }

    public double MaximumWidth { get; set; }

    public double MaximumHeight { get; set; }

    public double MinimumWidth { get; set; }

    public double MinimumMinHeight { get; set; }

    public Point Position { get; set; }
}
