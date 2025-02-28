using System.Windows;

namespace ASiNet.VWA.Core.Interfaces;
public interface IWorkspaceWindowViewModel : IWorkspaceObjectViewModel
{
    public UIElement? Content { get; set; }

    public bool IsMinimize { get; set; }
}
