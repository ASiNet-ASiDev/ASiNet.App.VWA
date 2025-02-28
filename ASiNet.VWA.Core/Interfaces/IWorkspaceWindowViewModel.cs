using System.Windows;

namespace ASiNet.VWA.Core.Interfaces;
public interface IWorkspaceWindowViewModel : IWorkspaceObjectViewModel
{
    public bool IsMinimize { get; set; }
}
