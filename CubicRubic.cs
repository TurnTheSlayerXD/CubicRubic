
using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata;

class CubicRubicConfig
{

    public readonly float edgeLength;
    public readonly float rotationSpeed;
    CubicRubicConfig(float edgeLength = 1, float rotationSpeed = 0.1f)
    {
        this.edgeLength = edgeLength;
        this.rotationSpeed = rotationSpeed;
    }

}



class CubicRubic
{
    Rotation currentRotation;
    public readonly Cubic[] cubics;

    public readonly CubicRubicConfig config;

    public Cubic[] LeftSide
    {
        get => cubics.Where(c => c.pos.X == -1).ToArray();
    }
    public Cubic[] RightSide
    {
        get => cubics.Where(c => c.pos.X == 1).ToArray();
    }
    public Cubic[] UpperSide
    {
        get => cubics.Where(c => c.pos.Y == 1).ToArray();
    }

    public Cubic[] DownSide
    {
        get => cubics.Where(c => c.pos.Y == -1).ToArray();
    }

    public Cubic[] FrontSide
    {
        get => cubics.Where(c => c.pos.Z == 1).ToArray();
    }

    public Cubic[] BackSide
    {
        get => cubics.Where(c => c.pos.Z == -1).ToArray();
    }


    public CubicRubic(CubicRubicConfig config)
    {
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
            cubic.FrontSide.color = Color.White;
        }
        foreach (var cubic in BackSide)
        {
            cubic.BackSide.color = Color.Yellow;
        }
    }

    public unsafe (Vector3*, Color)[] getDrawable()
    {

        foreach (var cubic in cubics)
        {
            foreach (var edge in cubic.edges)
            {


            }
        }


    }


}