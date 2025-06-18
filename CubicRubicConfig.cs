using Raylib_cs;
using System.Numerics;

public class CubicRubicConfig
{

    public float edgeLength = 1;
    public float rotationAngleSpeed = 3e-2f;
    public float maxRotationAngle = float.Pi / 2;
    public Color outlineColor = Color.Black;
    public float outlineWidth = 0.05f;
    public Vector2 cameraRotationAngle = new Vector2(5e-3f, 5e-3f);
    public (float min, float max) cameraDistanseLimit = (10, 40);

    public float buttonsLeftColumnX = 300;
    public float buttonsRightColumnX = 800;

}