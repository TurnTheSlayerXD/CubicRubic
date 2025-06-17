using Raylib_cs;
using System.Numerics;
public class Cubic
{
    public Vector3 pos;
    public Edge[] edges;
    public int count;
    public Edge LeftSide
    {
        get => edges.Where(c => c.dir.X == -1).Single();
        set => edges[Array.FindIndex(edges, c => c.dir.X == -1)] = value;
    }
    public Edge RightSide
    {
        get => edges.Where(c => c.dir.X == 1).Single();
        set => edges[Array.FindIndex(edges, c => c.dir.X == 1)] = value;

    }
    public Edge UpperSide
    {
        get => edges.Where(c => c.dir.Y == 1).Single();
        set => edges[Array.FindIndex(edges, c => c.dir.Y == 1)] = value;

    }
    public Edge DownSide
    {
        get => edges.Where(c => c.dir.Y == -1).Single();
        set => edges[Array.FindIndex(edges, c => c.dir.Y == -1)] = value;

    }
    public Edge FrontSide
    {
        get => edges.Where(c => c.dir.Z == 1).Single();
        set => edges[Array.FindIndex(edges, c => c.dir.Z == 1)] = value;

    }
    public Edge BackSide
    {
        get => edges.Where(c => c.dir.Z == -1).Single();
        set => edges[Array.FindIndex(edges, c => c.dir.Z == -1)] = value;
    }
    public Cubic()
    {
        edges = [new Edge { dir = new Vector3(-1, 0, 0), },
                 new Edge { dir = new Vector3(1, 0, 0), },
                 new Edge { dir = new Vector3(0, -1, 0), },
                 new Edge { dir = new Vector3(0, 1, 0), },
                 new Edge { dir = new Vector3(0, 0, -1), },
                 new Edge { dir = new Vector3(0, 0, 1), } ];
    }
}

