using System.Numerics;
using Raylib_cs;

namespace Cube;

public class Edge : ICloneable
{
    public Vector3 Direction { get; set; }
    public Color Color { get; set; }

    public Edge(Vector3 direction)
    {
        Direction = direction;
        Color = Color.Black;
    }

    public object Clone()
    {
        return new Edge(Direction) { Color = Color };
    }
}