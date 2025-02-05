// #define debug


using System.Collections;
using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

namespace Cube;

public class CubicRubic : IEnumerable<Cubic>, ICloneable
{
    private List<Cubic> Cubes { get; set; }
    public List<Cubic> Up => Cubes.FindAll(x => Math.Abs(x.Position[1] - 1) < Drawing.Epsilon).ToList();
    public List<Cubic> Down => Cubes.FindAll(x => Math.Abs(x.Position[1] - (-1)) < Drawing.Epsilon).ToList();
    public List<Cubic> Left => Cubes.FindAll(x => Math.Abs(x.Position[0] - (-1)) < Drawing.Epsilon).ToList();
    public List<Cubic> Right => Cubes.FindAll(x => Math.Abs(x.Position[0] - 1) < Drawing.Epsilon).ToList();
    public List<Cubic> Front => Cubes.FindAll(x => Math.Abs(x.Position[2] - 1) < Drawing.Epsilon).ToList();
    public List<Cubic> Back => Cubes.FindAll(x => Math.Abs(x.Position[2] - (-1)) < Drawing.Epsilon).ToList();


    public CubicRubic()
    {
        Cubes = new List<Cubic>();
        for (int x = -1; x <= 1; ++x)
        for (int y = -1; y <= 1; ++y)
        for (int z = -1; z <= 1; ++z)
            Cubes.Add(new Cubic(new Vector3(x, y, z)));

        foreach (var c in Front)
        {
            c.Front.Color = Color.White;
        }

        foreach (var c in Back)
        {
            c.Back.Color = Color.Yellow;
        }

        foreach (var c in Up)
        {
            c.Up.Color = Color.Orange;
        }

        foreach (var c in Down)
        {
            c.Down.Color = Color.Red;
        }

        foreach (var c in Left)
        {
            c.Left.Color = Color.Blue;
        }

        foreach (var c in Right)
        {
            c.Right.Color = Color.Green;
        }
    }

    public Cubic Get(float x, float y, float z)
    {
        return Cubes.Find(c =>
            Math.Abs(c.Position.X - x) < Drawing.Epsilon &&
            Math.Abs(c.Position.Y - y) < Drawing.Epsilon &&
            Math.Abs(c.Position.Z - z) < Drawing.Epsilon) ?? throw new InvalidOperationException();
    }


    public static string ColorToString(Color color)
    {
        var str = color.ToString();
        if (str == Color.Red.ToString()) return "R";
        if (str == Color.Blue.ToString()) return "B";
        if (str == Color.Green.ToString()) return "G";
        if (str == Color.Orange.ToString()) return "O";
        if (str == Color.Yellow.ToString()) return "Y";
        if (str == Color.White.ToString()) return "W";
        throw new ArgumentOutOfRangeException(nameof(color), color, null);
    }

    public IEnumerator<Cubic> GetEnumerator()
    {
        foreach (var cube in Cubes)
        {
            yield return cube;
        }
    }

    public override string ToString()
    {
        var f = (int x, int y, int z, Func<Cubic, Edge> dir)
            => ColorToString(dir(Get(x, y, z)).Color);

        var dir = (Cubic c) => c.Back;
        var back_down = $"{f(-1, -1, -1, dir)}{f(0, -1, -1, dir)}{f(1, -1, -1, dir)}";
        var back_middle = $"{f(-1, 0, -1, dir)}{f(-1, 0, -1, dir)}{f(1, 0, -1, dir)}";
        var back_up = $"{f(-1, 1, -1, dir)}{f(-1, 1, -1, dir)}{f(1, 1, -1, dir)}";


        dir = c => c.Up;
        var up_down = $"{f(-1, 1, -1, dir)}{f(0, 1, -1, dir)}{f(1, 1, -1, dir)}";
        var up_middle = $"{f(-1, 1, 0, dir)}{f(-1, 1, 0, dir)}{f(1, 1, 0, dir)}";
        var up_up = $"{f(-1, 1, 1, dir)}{f(-1, 1, 1, dir)}{f(1, 1, 1, dir)}";

        dir = c => c.Left;
        var left_down = $"{f(-1, 1, -1, dir)}{f(-1, 1, 0, dir)}{f(-1, 1, 1, dir)}";
        var left_middle = $"{f(-1, 0, -1, dir)}{f(-1, 0, 0, dir)}{f(-1, 0, 1, dir)}";
        var left_up = $"{f(-1, -1, -1, dir)}{f(-1, -1, 0, dir)}{f(-1, -1, 1, dir)}";

        dir = c => c.Right;
        var right_down = $"{f(1, 1, 1, dir)}{f(1, 1, 0, dir)}{f(1, 1, -1, dir)}";
        var right_middle = $"{f(1, 0, 1, dir)}{f(1, 0, 0, dir)}{f(1, 0, -1, dir)}";
        var right_up = $"{f(1, -1, 1, dir)}{f(1, -1, 0, dir)}{f(1, -1, -1, dir)}";

        dir = c => c.Front;
        var front_down = $"{f(-1, 1, 1, dir)}{f(0, 1, 1, dir)}{f(1, 1, 1, dir)}";
        var front_middle = $"{f(-1, 0, 1, dir)}{f(0, 0, 1, dir)}{f(1, 0, 1, dir)}";
        var front_up = $"{f(-1, -1, 1, dir)}{f(0, -1, 1, dir)}{f(1, -1, 1, dir)}";


        dir = c => c.Down;
        var down_down = $"{f(-1, -1, 1, dir)}{f(0, -1, 1, dir)}{f(1, -1, 1, dir)}";
        var down_middle = $"{f(-1, -1, 0, dir)}{f(0, -1, 0, dir)}{f(1, -1, 0, dir)}";
        var down_up = $"{f(-1, -1, -1, dir)}{f(0, -1, -1, dir)}{f(1, -1, -1, dir)}";


        var spc = new String(' ', 3);

        return $"{spc}{back_down}{spc}\n" +
               $"{spc}{back_middle}{spc}\n" +
               $"{spc}{back_up}{spc}\n" +
               $"{spc}{up_down}{spc}\n" +
               $"{spc}{up_middle}{spc}\n" +
               $"{spc}{up_up}{spc}\n" +
               $"{left_down}{front_down}{right_down}\n" +
               $"{left_middle}{front_middle}{right_middle}\n" +
               $"{left_up}{front_up}{right_up}\n" +
               $"{spc}{down_down}{spc}\n" +
               $"{spc}{down_middle}{spc}\n" +
               $"{spc}{down_up}{spc}\n";
    }

    public object Clone()
    {
        return new CubicRubic { Cubes = Cubes.Select(x => (Cubic)x.Clone()).ToList() };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static bool IsCollinear(Vector3 x, Vector3 y)
        => Math.Abs(Math.Abs(Vector3.Dot(x, y)) - Raymath.Vector3Length(x) * Raymath.Vector3Length(y))
           < Drawing.Epsilon;

    public static bool IsOrthogonal(Vector3 x, Vector3 y)
        => Math.Abs(Vector3.Dot(x, y)) < Drawing.Epsilon;

    public DrawableRubic CreateDrawable()
    {
        var vertices = new List<(Color, Vector3[])>();


        foreach (var cube in Cubes)
        {
            List<Vector3> list = [cube.First().Direction];

            foreach (var dir in cube.Skip(1).Select(x => x.Direction))
            {
                if (list.All(v => IsOrthogonal(v, dir)))
                {
                    list.Add(dir);
                }
            }

            Debug.Assert(list.Count == 3);
            var (x, y, z) = (list[0], list[1], list[2]);
            foreach (var edge in cube)
            {
                var point = edge.Direction * 0.5f + cube.Position;
                Vector3 f = new Vector3();
                Vector3 s = new Vector3();
                if (IsCollinear(x, edge.Direction))
                {
                    f = y;
                    s = z;
                }
                else if (IsCollinear(y, edge.Direction))
                {
                    f = x;
                    s = z;
                }
                else if (IsCollinear(z, edge.Direction))
                {
                    f = x;
                    s = y;
                }

                Debug.Assert(f != Vector3.Zero && s != Vector3.Zero);
                vertices.Add((edge.Color, SortedVerticles([
                    point + 0.5f * s - 0.5f * f,
                    point + 0.5f * s + 0.5f * f,
                    point - 0.5f * s - 0.5f * f,
                    point - 0.5f * s + 0.5f * f,
                ], edge.Direction)));
            }
        }

        return new DrawableRubic(vertices);
    }

    Vector3 GetCenter(Vector3[] vertices) =>
        vertices.Aggregate((s, v) => s + v) / vertices.Length;

    Vector3[] SortedVerticles(Vector3[] points, Vector3 ortho)
    {
        Vector3 center = new Vector3(
            points.Average(p => p.X),
            points.Average(p => p.Y),
            points.Average(p => p.Z)
        );

        Vector3 normal = Vector3.Normalize(-ortho);

        Vector3 tangent = Vector3.Normalize(points[0] - center);

        Vector3 binormal = Vector3.Normalize(Vector3.Cross(normal, tangent));

        // 5. Вычисляем углы относительно центра
        var angles = points.Select(p =>
        {
            float localX = Vector3.Dot(p - center, tangent);
            float localY = Vector3.Dot(p - center, binormal);
            float angle = MathF.Atan2(localY, localX);
            return (angle, p);
        }).OrderByDescending(a => a.angle).ToList();

        // 6. Сортируем по убыванию угла (по часовой стрелке)
        Vector3[] sortedPoints = angles.Select(a => a.p).ToArray();

        // 7. Переставляем в "змейку" для GL_TRIANGLE_STRIP
        int[] stripOrder = { 0, 1, 3, 2 }; // GL_TRIANGLE_STRIP порядок
        Vector3[] finalPoints = { sortedPoints[stripOrder[0]], sortedPoints[stripOrder[1]], sortedPoints[stripOrder[2]], sortedPoints[stripOrder[3]] };

        return finalPoints;
    }
}