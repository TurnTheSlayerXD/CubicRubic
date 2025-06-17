using System.Collections;
using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

abstract class Rotation
{
    CubicRubic rubic;

    public Rotation(CubicRubic rubic)
    {
        this.rubic = rubic;
    }


    abstract public Vector3 GetAxis();
    abstract public Cubic[] GetSide();




}
