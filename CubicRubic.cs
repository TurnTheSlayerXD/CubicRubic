using Raylib_cs;
using System.Diagnostics;
using System.Numerics;


public class CubicRubic
{
    Queue<Rotation> rotationStack;
    // Rotation currentRotation;
    public readonly Cubic[] cubics;

    public readonly CubicRubicConfig config;

    public Cubic[] LeftSide
    {
        get => cubics.Where(c => FuncTool.AreClose(c.pos.X, -config.edgeLength)).ToArray();
    }
    public Cubic[] RightSide
    {
        get => cubics.Where(c => FuncTool.AreClose(c.pos.X, config.edgeLength)).ToArray();
    }
    public Cubic[] UpperSide
    {
        get => cubics.Where(c => FuncTool.AreClose(c.pos.Y, config.edgeLength)).ToArray();
    }

    public Cubic[] DownSide
    {
        get => cubics.Where(c => FuncTool.AreClose(c.pos.Y, -config.edgeLength)).ToArray();
    }

    public Cubic[] FrontSide
    {
        get => cubics.Where(c => FuncTool.AreClose(c.pos.Z, config.edgeLength)).ToArray();
    }

    public Cubic[] BackSide
    {
        get => cubics.Where(c => FuncTool.AreClose(c.pos.Z, -config.edgeLength)).ToArray();
    }

    public CubicRubic(CubicRubicConfig config)
    {
        rotationStack = new Queue<Rotation>();
        this.config = config;
        cubics = new Cubic[27];
        int count = 0;
        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                for (int k = -1; k <= 1; ++k)
                {
                    cubics[count++] = new Cubic
                    {
                        pos = new Vector3(i * config.edgeLength, j * config.edgeLength, k * config.edgeLength),
                    };
                }
            }
        }

        foreach (var cubic in LeftSide)
        {
            cubic.LeftSide.color = Color.Red;
        }
        foreach (var cubic in RightSide)
        {
            cubic.RightSide.color = Color.Orange;
        }
        foreach (var cubic in UpperSide)
        {
            cubic.UpperSide.color = Color.Blue;
        }
        foreach (var cubic in DownSide)
        {
            cubic.DownSide.color = Color.Green;
        }
        foreach (var cubic in FrontSide)
        {
            cubic.FrontSide.color = Color.RayWhite;
        }
        foreach (var cubic in BackSide)
        {
            cubic.BackSide.color = Color.Yellow;
        }
    }

    private void ToAlignedPosition()
    {
        var edges = cubics.SelectMany(c => c.edges);
        foreach (var edge in edges)
        {
            edge.dir.X = FuncTool.ClampToOneZeroMinusOne(edge.dir.X);
            edge.dir.Y = FuncTool.ClampToOneZeroMinusOne(edge.dir.Y);
            edge.dir.Z = FuncTool.ClampToOneZeroMinusOne(edge.dir.Z);
        }
        foreach (var cubic in cubics)
        {
            cubic.pos.X = config.edgeLength * FuncTool.ClampToOneZeroMinusOne(cubic.pos.X / config.edgeLength);
            cubic.pos.Y = config.edgeLength * FuncTool.ClampToOneZeroMinusOne(cubic.pos.Y / config.edgeLength);
            cubic.pos.Z = config.edgeLength * FuncTool.ClampToOneZeroMinusOne(cubic.pos.Z / config.edgeLength);
        }
    }

    public (Vector3[], Color)[] GetDrawable()
    {
        if (rotationStack.Count > 0)
        {
            var curRotation = rotationStack.Peek();
            var cubicsToRotate = curRotation.GetSideToRotate(this);
            var axis = curRotation.GetRotationAxis();
            foreach (var cubic in cubicsToRotate)
            {
                cubic.pos = Raymath.Vector3RotateByAxisAngle(cubic.pos, axis, config.rotationAngleSpeed);
                foreach (var edge in cubic.edges)
                {
                    edge.dir = Raymath.Vector3RotateByAxisAngle(edge.dir, axis, config.rotationAngleSpeed);
                }
            }
            curRotation.currentAngle += config.rotationAngleSpeed;
            if (curRotation.currentAngle > config.maxRotationAngle || FuncTool.AreClose(curRotation.currentAngle, config.maxRotationAngle))
            {
                rotationStack.Dequeue();
                ToAlignedPosition();
            }
        }

        var edges = new (Vector3[], Color)[27 * 6];
        var count = 0;
        foreach (var cubic in cubics)
        {
            foreach (var edge in cubic.edges)
            {
                var edgeCenter = cubic.pos + edge.dir * config.edgeLength / 2;
                var v1 = edge.dir;
                var (v2, v3) = FuncTool.GetTwoPerpendicular(v1, cubic.edges);
                var halfEdge = config.edgeLength / 2;
                edges[count++] = (FuncTool.GetTriangleStrip([edgeCenter + v2 * halfEdge + v3 * halfEdge,
                                                             edgeCenter - v2 * halfEdge + v3 * halfEdge,
                                                             edgeCenter + v2 * halfEdge - v3 * halfEdge,
                                                             edgeCenter - v2 * halfEdge - v3 * halfEdge], edge.dir), edge.color);
            }
        }
        Debug.Assert(count == 27 * 6);
        return edges;
    }
    public void AddRotation(Rotation rot)
    {
        rotationStack.Enqueue(rot);
    }


}