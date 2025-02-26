using System.Windows;
using System.Windows.Data;
using ASiNet.VWA.Core.Interfaces;

namespace ASiNet.VWA.Controls;
public partial class WorkspaceObject
{


    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        switch (e.Property.Name)
        {
            case nameof(DataContext):
                if (e.NewValue is null)
                    return;
                if(e.NewValue is IWorkspaceObjectViewModel)
                {
                    CreateBinding(this, DataContext, PositionProperty, nameof(IWorkspaceObjectViewModel.Position));
                    CreateBinding(this, DataContext, HeightProperty, nameof(IWorkspaceObjectViewModel.Height));
                    CreateBinding(this, DataContext, WidthProperty, nameof(IWorkspaceObjectViewModel.Width));
                    //CreateBinding(this, DataContext, , nameof(IWorkspaceObjectViewModel.ZIndex));
                    CreateBinding(this, DataContext, IsPinnedProperty, nameof(IWorkspaceObjectViewModel.IsPinned));
                }
                break;
        }

        base.OnPropertyChanged(e);
    }


    protected static void CreateBinding(DependencyObject target, object src, DependencyProperty property, string path, BindingMode mode = BindingMode.TwoWay)
    {
        var bind = new Binding(path)
        {
            Source = src,
            Mode = mode,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        };
        BindingOperations.SetBinding(target, property, bind);
    }
}
