using System.Windows;

namespace ASiNet.VWA.Core.Entities;

public enum PositionType : byte
{
    Undefined,
    Custom,
    MousePositionRelativeToArea,
    LastActiveMousePositionRelativeToArea
}

public struct CreatePosition
{
    public CreatePosition(Point point, PositionType positionType)
    {
        PositionType = positionType;
        Position = point;
    }

    public CreatePosition(Point point)
    {
        PositionType = PositionType.Custom;
        Position = point;
    }

    public CreatePosition(PositionType type)
    {
        PositionType = type;
    }

    public PositionType PositionType { get; set; }

    public Point Position { get; set; }
}
