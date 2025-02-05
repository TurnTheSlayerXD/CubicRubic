using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

namespace Cube;

using Raylib_cs;

public class Rotator
{
    private const float RotationStep = 0.03f;

    public static readonly Matrix<float> Singular = Matrix.Build.DenseOfArray(
        new float[,]
        {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 }
        });

    public static readonly Matrix<float> AroundX = Matrix.Build.DenseOfArray(
        new float[,]
        {
            { 1, 0, 0 },
            { 0, 0, -1 },
            { 0, 1, 0 }
        });

    public static readonly Matrix<float> AroundY = Matrix.Build.DenseOfArray(
        new float[,]
        {
            { 0, 0, 1 },
            { 0, 1, 0 },
            { -1, 0, 0 }
        });

    public static readonly Matrix<float> AroundZ = Matrix.Build.DenseOfArray(
        new float[,]
        {
            { 0, -1, 0 },
            { 1, 0, 0 },
            { 0, 0, 1 }
        });

    public static Func<Vector3, Vector3> Rotator_X(Matrix<float> rotation, float fi)
    {
        var mat = Matrix.Build.DenseOfArray(
            new[,]
            {
                { 1, 0, 0 },
                { 0, float.Cos(fi), -float.Sin(fi) },
                { 0, float.Sin(fi), float.Cos(fi) }
            });
        return vec =>
        {
            var x = rotation * mat * rotation.Inverse() *
                    CreateVector.Dense([vec.X, vec.Y, vec.Z]);
            return new Vector3(x[0], x[1], x[2]);
        };
    }

    public static Func<Vector3, Vector3> Rotator_Y(Matrix<float> rotation, float fi)
    {
        var mat = Matrix.Build.DenseOfArray(
            new[,]
            {
                { float.Cos(fi), 0, float.Sin(fi) },
                { 0, 1, 0 },
                { -float.Sin(fi), 0, float.Cos(fi) }
            });
        return vec =>
        {
            var x = rotation * mat * rotation.Inverse() *
                    CreateVector.Dense([vec.X, vec.Y, vec.Z]);
            return new Vector3(x[0], x[1], x[2]);
        };
    }

    public static Func<Vector3, Vector3> Rotator_Z(Matrix<float> rotation, float fi)
    {
        var mat = Matrix.Build.DenseOfArray(
            new[,]
            {
                { float.Cos(fi), -float.Sin(fi), 0 },
                { float.Sin(fi), float.Cos(fi), 0 },
                { 0, 0, 1 }
            });
        var invrotation = rotation.Inverse();
        return vec =>
        {
            var x = rotation * mat * invrotation *
                    CreateVector.Dense([vec.X, vec.Y, vec.Z]);
            return new Vector3(x[0], x[1], x[2]);
        };
    }


    private static void Operation(List<Cubic> side, Func<Vector3, Vector3> rotator)
    {
        foreach (var cube in side)
        {
            cube.Position = rotator(cube.Position);
            foreach (var edge in cube)
            {
                edge.Direction = rotator(edge.Direction);
            }
        }
    }

    public static List<Cubic> GetSideWithPerspective(CubicRubic rubic, List<Cubic> side, Matrix<float> rot)
        => side
            .Select(x => CreateVector.Dense([x.Position[0], x.Position[1], x.Position[2]]) * rot)
            .Select(x => rubic.Get(x[0], x[1], x[2]))
            .ToList();

    public static IEnumerable<CubicRubic> DoR(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Right;

        List<Cubic> side = rubic.Right;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Right, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Right, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_X(perspectiverotation, -Single.Pi / 2));

        var rotator = Rotator_X(perspectiverotation, -RotationStep);
        for (float fi = 0; fi > -Single.Pi / 2; fi -= RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }

    public static IEnumerable<CubicRubic> DoRC(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Right;

        List<Cubic> side = rubic.Right;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Right, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Right, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_X(perspectiverotation, Single.Pi / 2));

        var rotator = Rotator_X(perspectiverotation, RotationStep);
        for (float fi = 0; fi < Single.Pi / 2; fi += RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }

    public static IEnumerable<CubicRubic> DoL(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Left;

        List<Cubic> side = rubic.Left;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Left, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Left, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_X(perspectiverotation, Single.Pi / 2));

        var rotator = Rotator_X(perspectiverotation, RotationStep);
        for (float fi = 0; fi < Single.Pi / 2; fi += RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }

    public static IEnumerable<CubicRubic> DoLC(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Left;

        List<Cubic> side = rubic.Left;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Left, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Left, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_X(perspectiverotation, -Single.Pi / 2));

        var rotator = Rotator_X(perspectiverotation, -RotationStep);
        for (float fi = 0; fi > -Single.Pi / 2; fi -= RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }

    public static IEnumerable<CubicRubic> DoU(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Up;

        List<Cubic> side = rubic.Up;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Up, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Up, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_Y(perspectiverotation, -Single.Pi / 2));

        var rotator = Rotator_Y(perspectiverotation, -RotationStep);
        for (float fi = 0; fi > -Single.Pi / 2; fi -= RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }

    public static IEnumerable<CubicRubic> DoUC(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Up;

        List<Cubic> side = rubic.Up;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Up, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Up, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_Y(perspectiverotation, Single.Pi / 2));

        var rotator = Rotator_Y(perspectiverotation, RotationStep);
        for (float fi = 0; fi < Single.Pi / 2; fi += RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }

    public static IEnumerable<CubicRubic> DoD(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Down;

        List<Cubic> side = rubic.Down;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Down, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Down, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_Y(perspectiverotation, Single.Pi / 2));

        var rotator = Rotator_Y(perspectiverotation, RotationStep);
        for (float fi = 0; fi < Single.Pi / 2; fi += RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }

    public static IEnumerable<CubicRubic> DoDC(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Down;

        List<Cubic> side = rubic.Down;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Down, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Down, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_Y(perspectiverotation, -Single.Pi / 2));

        var rotator = Rotator_Y(perspectiverotation, -RotationStep);
        for (float fi = 0; fi > -Single.Pi / 2; fi -= RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }
    public static IEnumerable<CubicRubic> DoF(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Front;

        List<Cubic> side = rubic.Front;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Front, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Front, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_Z(perspectiverotation, -Single.Pi / 2));

        var rotator = Rotator_Z(perspectiverotation, -RotationStep);
        for (float fi = 0; fi > -Single.Pi / 2; fi -= RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }

    public static IEnumerable<CubicRubic> DoFC(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Front;

        List<Cubic> side = rubic.Front;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Front, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Front, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_Z(perspectiverotation, Single.Pi / 2));

        var rotator = Rotator_Z(perspectiverotation, RotationStep);
        for (float fi = 0; fi < Single.Pi / 2; fi += RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }
    
    public static IEnumerable<CubicRubic> DoB(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Back;

        List<Cubic> side = rubic.Back;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Back, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Back, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_Z(perspectiverotation, Single.Pi / 2));

        var rotator = Rotator_Z(perspectiverotation, RotationStep);
        for (float fi = 0; fi < Single.Pi / 2; fi += RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }
    
    public static IEnumerable<CubicRubic> DoBC(CubicRubic rubic, Matrix<float>? perspectiverotation = null)
    {
        var final = (CubicRubic)rubic.Clone();
        List<Cubic> finalside = final.Back;

        List<Cubic> side = rubic.Back;
        if (perspectiverotation != null)
        {
            side = GetSideWithPerspective(rubic, rubic.Back, perspectiverotation);
            finalside = GetSideWithPerspective(final, final.Back, perspectiverotation);
        }
        else
        {
            perspectiverotation = Singular;
        }

        Operation(finalside, Rotator_Z(perspectiverotation, -Single.Pi / 2));

        var rotator = Rotator_Z(perspectiverotation, -RotationStep);
        for (float fi = 0; fi > -Single.Pi / 2; fi -= RotationStep)
        {
            Operation(side, rotator);
            yield return rubic;
        }

        yield return final;
    }
}