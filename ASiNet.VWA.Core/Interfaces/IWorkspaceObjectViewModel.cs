using System.Windows;

namespace ASiNet.VWA.Core.Interfaces;
public interface IWorkspaceObjectViewModel
{

    public Type ObjectType { get; }

    public double Width { get; set; }

    public double Height { get; set; }

    public Point Position { get; set; }

    public bool IsPinned { get; set; }

    public int ZIndex { get; set; }
}
