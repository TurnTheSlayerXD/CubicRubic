
using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public static class FuncTool
{
    public static bool AreClose(float a, float b)
    {
        return float.Abs(a - b) < 1e-5;
    }

    public static (Vector3, Vector3) GetTwoPerpendicular(Vector3 v, Edge[] edges)
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

    public static int[] SortPointsClockwise((Vector4 vec, int ind)[] points)
    {
        var lowestLeft = points.Aggregate((acc, e) => e.vec.Y < acc.vec.Y ? e : e.vec.Y == acc.vec.Y && e.vec.X < acc.vec.X ? e : acc);
        var ind = Array.IndexOf(points, lowestLeft);
        var b = lowestLeft;
        b.vec.X += 0.5f;
        var comp = b.vec - lowestLeft.vec;
        (points[0], points[ind]) = (points[ind], points[0]);
        return points.Skip(1).OrderByDescending((v) =>
        {
            var buf = v.vec - lowestLeft.vec;
            return Raymath.Vector2Angle(new Vector2(buf.X, buf.Y), new Vector2(comp.X, comp.Y));
        }).Prepend(points[0]).Select(e => e.ind).ToArray();
    }
    public static Vector3[] GetTriangleStrip(Vector3[] points, Vector3 norm)
    {
        var v1 = norm;
        var v2 = Raymath.Vector3Normalize(Raymath.Vector3Perpendicular(v1));
        var v3 = Raymath.Vector3Normalize(Raymath.Vector3CrossProduct(v1, v2));
        var planeMat = Matrix4x4.Transpose(new Matrix4x4(v2.X, v2.Y, v2.Z, 0,
                                     v3.X, v3.Y, v3.Z, 0,
                                     v1.X, v1.Y, v1.Z, 0,
                                     0, 0, 0, 1));
        var projected = points.Select(p => Vector4.Transform(p, planeMat)).ToArray();
        Debug.Assert(projected.All(p => p.Z == projected[0].Z));
        var res = SortPointsClockwise(projected.Select((e, i) => (e, i)).ToArray());
        Vector3[] toReturn = [points[res[0]], points[res[1]], points[res[3]], points[res[2]]];
        return toReturn;
    }
}

