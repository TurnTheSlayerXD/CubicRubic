using System.Collections;
using System.Numerics;

namespace Cube;

public class Cubic : IEnumerable<Edge>, ICloneable
{
    public Edge Up => Edges.Find(x => x.Direction[1] == 1);
    public Edge Down => Edges.Find(x => x.Direction[1] == -1);
    public Edge Left => Edges.Find(x => x.Direction[0] == -1);
    public Edge Right => Edges.Find(x => x.Direction[0] == 1);
    public Edge Front => Edges.Find(x => x.Direction[2] == 1);
    public Edge Back => Edges.Find(x => x.Direction[2] == -1);

    private List<Edge> Edges { get; set; }

    public Vector3 Position { get; set; }

    public Cubic(Vector3 position)
    {
        Edges =
            new List<Edge>
            {
                new(new Vector3([0, 1, 0])),
                new(new Vector3([0, -1, 0])),
                new(new Vector3([1, 0, 0])),
                new(new Vector3([-1, 0, 0])),
                new(new Vector3([0, 0, 1])),
                new(new Vector3([0, 0, -1])),
            };
        Position = position;
    }


    public IEnumerator<Edge> GetEnumerator()
    {
        foreach (var edge in Edges)
        {
            yield return edge;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public object Clone()
    {
        return new Cubic(Position) { Edges = Edges.Select(x => (Edge)x.Clone()).ToList() };
    }
}