using System.Windows;

namespace ASiNet.VWA.Core.Workspace;
public interface IResizedElement
{

    public double MaximumWidth { get; set; }
    
    public double MaximumHeight { get; set; }

    public double MinimumWidth { get; set; }

    public double MinimumMinHeight { get; set; }

    public void ResizeElement(Vector offset, double scale);
}
