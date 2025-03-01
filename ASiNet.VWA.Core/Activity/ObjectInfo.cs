namespace ASiNet.VWA.Core.Activity;
public class ObjectInfo(string title, string? description = null, string? author = null, string? iconPath = null)
{
    public Guid Id { get; private set; }

    public string Title { get; } = title;

    public string? Description { get; } = description;

    public string? Author { get; } = author;

    public string? IconPath { get; } = iconPath;

    internal void SetId(Registered registered)
    {
        Id = registered.Id;
    }
}
