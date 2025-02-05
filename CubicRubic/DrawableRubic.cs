using System.Numerics;

namespace Cube;

public class DrawableRubic
{
    public List<(Raylib_cs.Color, Vector3[])> Edges { get; init; }

    public DrawableRubic(List<(Raylib_cs.Color, Vector3[])> edges)
    {
        Edges = edges;
    }

    public static Vector3[] GetOrderedVerticles(Vector3[] verts)
    {
        var cloned = (Vector3[])verts.Clone();
        var atLestTwo = (Vector3 a, Vector3 b) =>
        {
            int count = 0;
            for (int i = 0; i < 3; ++i)
            {
                if (Math.Abs(a[i] - b[i]) < Drawing.Epsilon)
                {
                    ++count;
                }
            }

            return count == 2;
        };

        for (int i = 0; i < 2; ++i)
        {
            if (!atLestTwo(cloned[i], cloned[i + 1]))
            {
                (cloned[i + 1], cloned[i + 2]) = (cloned[i + 2], cloned[i + 1]);
            }
        }
        return cloned;
    }
}