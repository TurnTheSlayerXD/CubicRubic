using System.Collections;
using System.Numerics;
using Raylib_cs;

interface Rotation
{

    CubicRubic rubic;
    public Vector3 getAxis();
    public Cubic[] getSide();

    public static bool AreClose(float a, float b)
    {
        return float.Abs(a - b) < 1e-5;
    }


    public (Vector3, Vector3) getTwoPerpendicular(Vector3 v, Edge[] edges)
    {
        var v2 = new Vector3();
        var v3 = new Vector3();
        foreach (var other in edges)
        {
            if (AreClose(Vector3.Dot(other.dir, v), 0))
            {
                v2 = other.dir;
            }
        }
        foreach (var other in edges)
        {
            if (AreClose(Vector3.Dot(other.dir, v), 0) && AreClose(Vector3.Dot(other.dir, v2), 0))
            {
                v3 = other.dir;
            }
        }
        return (v2, v3);
    }

    public unsafe (Vector3[], Color)[] getState()
    {
        var side = getSide();
        var edges = new (Vector3[], Color)[27 * 6];

        int count = 0;

        foreach (var cubic in side)
        {
            foreach (var edge in cubic.edges)
            {
                var edgeCenter = cubic.pos + edge.dir * rubic.config.edgeLength / 2;
                var v1 = edge.dir;
                var (v2, v3) = getTwoPerpendicular(v1, cubic.edges);
                var halfEdge = rubic.config.edgeLength / 2;
                edges[count++] = ([ edgeCenter + v2 * halfEdge + v3 * halfEdge,
                                    edgeCenter - v2 * halfEdge + v3 * halfEdge,
                                    edgeCenter + v2 * halfEdge - v3 * halfEdge,
                                    edgeCenter - v2 * halfEdge - v3 * halfEdge,], edge.color);
            }
        }
    }
}
