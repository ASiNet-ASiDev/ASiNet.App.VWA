using ASiNet.VWA.Core.Workspace;

namespace ASiNet.App.VWA.View.Pages;
public partial class ConsolePage : ASiNet.VWA.Core.WorkspaceWindow
{
    public ConsolePage()
    {
        InitializeComponent();
    }

    public ConsolePage(IAreaController areaController) : base(areaController)
    {

        InitializeComponent();
    }
}
