using System.Windows;

namespace ASiNet.VWA.Controls;
public partial class VirtualWorkspace
{

    public Point Position { get; set; }
    public double MaxZoom { get; set; } = 2;
    public double MinZoom { get; set; } = 0.15;
    public double Scale { get; set; } = 1;
    public double MaxXOffset { get; set; }
    public double MaxYOffset { get; set; }
    public double MinXOffset { get; set; }
    public double MinYOffset { get; set; }
}
