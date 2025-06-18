using System.Collections;
using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public abstract class Rotation
{
    public float currentAngle = 0;
    public bool clockwise = false;
    public abstract Vector3 GetRotationAxis();
    public abstract Cubic[] GetSideToRotate(CubicRubic rubic);
}
public class LeftRotation : Rotation
{
    public override Cubic[] GetSideToRotate(CubicRubic rubic)
    {
        return rubic.LeftSide;
    }

    public override Vector3 GetRotationAxis()
    {
        return clockwise ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
    }
}
public class RightRotation : Rotation
{
    public override Cubic[] GetSideToRotate(CubicRubic rubic)
    {
        return rubic.RightSide;

    }
    public override Vector3 GetRotationAxis()
    {
        return clockwise ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0);
    }
}
public class UpperRotation : Rotation
{
    public override Cubic[] GetSideToRotate(CubicRubic rubic)
    {
        return rubic.UpperSide;


    }
    public override Vector3 GetRotationAxis()
    {
        return clockwise ? new Vector3(0, -1, 0) : new Vector3(0, 1, 0);
    }
}
public class DownRotation : Rotation
{
    public override Cubic[] GetSideToRotate(CubicRubic rubic)
    {
        return rubic.DownSide;

    }
    public override Vector3 GetRotationAxis()
    {
        return clockwise ? new Vector3(0, 1, 0) : new Vector3(0, -1, 0);
    }
}
public class FrontRotation : Rotation
{
    public override Cubic[] GetSideToRotate(CubicRubic rubic)
    {
        return rubic.FrontSide;

    }
    public override Vector3 GetRotationAxis()
    {
        return clockwise ? new Vector3(0, 0, -1) : new Vector3(0, 0, 1);
    }
}
public class BackRotation : Rotation
{
    public override Cubic[] GetSideToRotate(CubicRubic rubic)
    {
        return rubic.BackSide;
    }
    public override Vector3 GetRotationAxis()
    {
        return clockwise ? new Vector3(0, 0, 1) : new Vector3(0, 0, -1);
    }
}
