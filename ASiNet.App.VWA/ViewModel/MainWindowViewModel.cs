using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.VWA.Controls;
using ASiNet.VWA.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.VWA.ViewModel;
public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        WorkspaceObjects.Add(new WorkspaceWindowVM(typeof(WorkspaceWindow)));

        WorkspaceObjects.Add(new WorkspaceWindowVM(typeof(WorkspaceWindow)));
    }

    public ObservableCollection<IWorkspaceObjectViewModel> WorkspaceObjects { get; } = [];
}
