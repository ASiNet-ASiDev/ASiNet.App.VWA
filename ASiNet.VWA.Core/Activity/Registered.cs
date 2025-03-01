namespace ASiNet.VWA.Core.Activity;
internal class Registered(Guid id, ObjectInfo info, Type v, Type vm)
{
    public Guid Id { get; } = id;

    public ObjectInfo Info { get; } = info;

    public Type ViewType { get; } = v;

    public Type ViewModelType { get; } = vm;
}
