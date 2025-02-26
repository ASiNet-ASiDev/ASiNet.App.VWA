using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ASiNet.VWA.Core;
using ASiNet.VWA.Core.Interfaces;

namespace ASiNet.VWA.Controls;
public partial class VirtualWorkspace
{

    private void Area_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        WorkspaceAreaController.Scale(e.Delta >= 0 ? 1.1 : 0.9);
    }

    private void Area_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        WorkspaceAreaController.StartMove(this, false);
    }

    private void Area_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        WorkspaceAreaController.EndMove();
        WorkspaceAreaController.EndResize();
    }

    private void Area_MouseLeave(object sender, MouseEventArgs e)
    {
        WorkspaceAreaController.EndMove();
        WorkspaceAreaController.EndResize();
    }

    private void Root_MouseMove(object sender, MouseEventArgs e)
    {
        WorkspaceAreaController.Move(Scale);
        WorkspaceAreaController.Resize(Scale);
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        var l = (Root.ActualWidth - Area.ActualWidth) / 2;
        var h = (Root.ActualHeight - Area.ActualHeight) / 2;
        var newV = new Vector(l, h);
        newV.Negate();
        MoveElement(newV, 1);
        Root.SizeChanged -= OnSizeChanged;
    }


    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        switch (e.Property.Name)
        {
            case nameof(Scale) :
                // TODO: SetScale
                break;
            case nameof(Position):
                // TODO: SetPosition
                break;
            case nameof(WorkspaceObjects):
                if(e.OldValue is ObservableCollection<IWorkspaceObjectViewModel> oldVWVM)
                {
                    oldVWVM.CollectionChanged -= OnWorkspaceObjectsChanged;
                }
                if(e.NewValue is ObservableCollection<IWorkspaceObjectViewModel> newVWVM)
                {
                    newVWVM.CollectionChanged += OnWorkspaceObjectsChanged;
                    ResetWorkspaceObjects(newVWVM);
                }
                break;

        }
        base.OnPropertyChanged(e);
    }

    private void ResetWorkspaceObjects(ObservableCollection<IWorkspaceObjectViewModel> contents)
    {
        Area.Children.Clear();
        contents.ForEach(x =>
        {
            var inst = (WorkspaceObject)Activator.CreateInstance(x.ObjectType, [WorkspaceAreaController])!;
            Area.Children.Add(inst);
            inst.DataContext = x;
            var newPos = new Vector(Area.Width / 2, Area.Height / 2);
            newPos.Negate();
            inst.MoveElement(newPos, Scale);
        });
    }


    private void OnWorkspaceObjectsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // TODO: Implement....
        throw new NotImplementedException();
    }
}