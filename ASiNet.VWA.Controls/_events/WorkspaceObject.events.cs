using System.Windows;
using System.Windows.Data;
using ASiNet.VWA.Core;
using ASiNet.VWA.Core.Interfaces;

namespace ASiNet.VWA.Controls;
public partial class WorkspaceObject
{


    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        switch (e.Property.Name)
        {
            case nameof(DataContext):
                e.NewValue
                    .ContainsPropertyTo(nameof(IWorkspaceObjectViewModel.Position), (o, n) => CreateBinding(this, o, PositionProperty, n))
                    .ContainsPropertyTo(nameof(IWorkspaceObjectViewModel.Height), (o, n) => CreateBinding(this, o, ContentHeightProperty, n))
                    .ContainsPropertyTo(nameof(IWorkspaceObjectViewModel.Width), (o, n) => CreateBinding(this, o, ContentWidthProperty, n))
                    .ContainsPropertyTo(nameof(IWorkspaceObjectViewModel.IsPinned), (o, n) => CreateBinding(this, o, IsPinnedProperty, n));
                break;
            case nameof(Height):
                ContentHeight = (double)e.NewValue;
                break;
            case nameof(Width):
                ContentWidth = (double)e.NewValue;
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
