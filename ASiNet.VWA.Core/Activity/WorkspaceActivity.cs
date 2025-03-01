using ASiNet.VWA.Core.Entities;
using ASiNet.VWA.Core.Interfaces;
using ASiNet.VWA.Core.logging;
using ASiNet.VWA.Core.Workspace;

namespace ASiNet.VWA.Core.Activity;
public class WorkspaceActivity(IWorkspaceContext context)
{

    public static event Action<WorkspaceActivity>? Initialized;

    public static WorkspaceActivity? Current { get; private set; }


    public static WorkspaceActivity Initialize(IWorkspaceContext context)
    {
        if (Current is not null)
        {
            Logger.Error($"WorkspaceActivity::Initialize Error.\nInitialized.");
            return Current;
        }
        Logger.Info($"WorkspaceActivity::Initialize ok.\nInitialized.");
        var wa = new WorkspaceActivity(context);
        Current = wa;
        Initialized?.Invoke(wa);
        return wa;
    }

    public IWorkspaceContext CurrentContext { get; } = context;

    private Dictionary<Guid, Registered> _registeredItems = [];

    public Guid Register<Tv, Tvm>(ObjectInfo info) where Tv : WorkspaceObject where Tvm : IWorkspaceObjectViewModel, new()
    {
        var viewType = typeof(Tv);
        var viewModelType = typeof(Tvm);
        var id = Guid.NewGuid();
        var registered = new Registered(id, info, viewType, viewModelType);
        info.SetId(registered);

        _registeredItems.Add(id, registered);
        Logger.Info($"WorkspaceActivity::Register ok.\nId = [{id:D}]\nVT: {viewType.Name}\nVMT: {viewModelType.Name}");
        return id;
    }


    public IEnumerable<ObjectInfo> EnumerateRegisteredObjects()
    {
        return _registeredItems.Values.Select(x => x.Info);
    }

    public bool Contains(Guid id) =>
        _registeredItems.ContainsKey(id);

    public ObjectInfo? GetInfo(Guid id)
    {
        if (GetRegisteredById(id) is Registered value)
            return value.Info;
        Logger.Error($"WorkspaceActivity::GetInfo registered not found.\nId = [{id:D}]");
        return null;
    }

    public WorkspaceObject? CreateWorkspaceObject(Guid id)
    {
        if (GetRegisteredById(id) is Registered value)
            return CreateWorkspaceObject(value);
        Logger.Error($"WorkspaceActivity::CreateWorkspaceObject registered not found.\nId = [{id:D}]");
        return null;
    }

    public IWorkspaceObjectViewModel? CreateWorkspaceObjectViewModel(Guid id, in CreatePosition position)
    {
        if (GetRegisteredById(id) is Registered value)
        {
            var viewModel = CreateWorkspaceObjectViewModel(value, position);
            return viewModel;
        }
        Logger.Error($"WorkspaceActivity::CreateWorkspaceObjectViewModel registered not found.\nId = [{id:D}]");
        return null;
    }

    public WorkspaceObject? CreateWorkspaceObject(IWorkspaceObjectViewModel viewModel)
    {
        if (GetRegisteredById(viewModel.RegisteredId) is Registered value)
        {
            var view = CreateWorkspaceObject(value);
            if (view is not null)
                view.DataContext = viewModel;
            return view;
        }
        Logger.Error($"WorkspaceActivity::CreateWorkspaceObject registered not found.\nId = [{viewModel.RegisteredId:D}]");
        return null;
    }

    public WorkspaceObject? CreateWorkspaceObjectAndBindViewModel(Guid id, in CreatePosition position)
    {
        if (GetRegisteredById(id) is Registered value)
        {
            var view = CreateWorkspaceObject(value);
            var viewModel = CreateWorkspaceObjectViewModel(value, position);
            if(view is not null)
                view.DataContext = viewModel;
            return view;
        }
        Logger.Error($"WorkspaceActivity::CreateWorkspaceObjectViewModel registered not found.\nId = [{id:D}]");
        return null;
    }


    internal WorkspaceObject? CreateWorkspaceObject(Registered value)
    {
        var instance = Activator.CreateInstance(value.ViewType) as WorkspaceObject;
        if (instance is null)
        {
            Logger.Error($"WorkspaceActivity::CreateWorkspaceObject create instance error.\nId = [{value.Id:D}]");
            return null;
        }
        return instance;
    }

    internal IWorkspaceObjectViewModel? CreateWorkspaceObjectViewModel(Registered value, in CreatePosition position)
    {
        var instance = Activator.CreateInstance(value.ViewModelType) as IWorkspaceObjectViewModel;
        if (instance is null)
        {
            Logger.Error($"WorkspaceActivity::CreateWorkspaceObjectViewModel create instance error.\nId = [{value.Id:D}]");
            return null;
        }
        instance.RegisteredId = value.Id;
        instance.Position = position.PositionType switch
        {
            PositionType.Custom => position.Position,
            PositionType.MousePositionRelativeToArea => CurrentContext.GetMousePositionRelativeToArea(),
            PositionType.LastActiveMousePositionRelativeToArea => CurrentContext.Transformer.LastActiveMousePosition,
            _ => position.Position,
        };
        return instance;
    }

    internal Registered? GetRegisteredById(Guid id)
    {
        if (_registeredItems.TryGetValue(id, out var value))
            return value;

        Logger.Error($"WorkspaceActivity::GetRegisteredById registered not found.\nId = [{id:D}]");
        return null;
    }
}
