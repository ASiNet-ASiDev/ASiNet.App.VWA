using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASiNet.VWA.Controls;
public partial class WorkspaceWindow : WorkspaceObject
{
    public WorkspaceWindow() : base(null!)
    {
        InitializeComponent();
    }

    public WorkspaceWindow(WorkspaceAreaController workspaceAreaController) : base(workspaceAreaController)
    {
        InitializeComponent();
    }

    private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        AreaController.StartMove(this);
    }

    private void Header_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        AreaController.EndMove();
    }
}
