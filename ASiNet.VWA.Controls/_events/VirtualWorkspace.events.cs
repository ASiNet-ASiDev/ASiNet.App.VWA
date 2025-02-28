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
        AreaController.Scale(e.Delta >= 0 ? 1.1 : 0.9);
    }

    private void Area_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        AreaController.StartMove(this, false);
    }

    private void Area_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        AreaController.EndMove();
        AreaController.EndResize();
    }

    private void Area_MouseLeave(object sender, MouseEventArgs e)
    {
        AreaController.EndMove();
        AreaController.EndResize();
    }

    private void Root_MouseMove(object sender, MouseEventArgs e)
    {
        if(!AreaController.IsResized)
            AreaController.Move(Scale);
        else
            AreaController.Resize(Scale);
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
            AddElement(x.Position, x);
        });
    }

    private void OnWorkspaceObjectsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                foreach (var item in e.NewItems!)
                {
                    if(item is IWorkspaceObjectViewModel vm)
                        AddElement(vm.Position, vm);
                }
                break;
            case NotifyCollectionChangedAction.Remove:
                foreach (var item in e.OldItems!)
                {
                    var res = _objects.FirstOrDefault(x => x.DataContext == item);
                    if(res is null)
                        continue;
                    RemoveElement(res);
                }
                break;
            case NotifyCollectionChangedAction.Replace:
                throw new NotImplementedException("Replace not supported");
            case NotifyCollectionChangedAction.Move:
                throw new NotImplementedException("Move not supported");
            case NotifyCollectionChangedAction.Reset:
                throw new NotImplementedException("Reset not supported");
        }
    }
}