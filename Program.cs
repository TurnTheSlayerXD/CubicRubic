using Raylib_cs;
using System.Collections;
using System.Numerics;


public enum State
{
    Rotation,
    Stable
}

public class Button
{

    Action callback;
    Rectangle rect;
    readonly string text;
    readonly Color highlightColor = Color.Pink;
    readonly Color defaultColor = Color.Black;
    readonly float width = 10;
    readonly int fontSize = 15;

    public Button(Rectangle rect, string text, Action callback)
    {
        this.rect = rect;
        this.callback = callback;
        this.text = text;
        this.callback = callback;
    }

    public void Draw()
    {
        var mousePos = Raylib.GetMousePosition();
        if (Raylib.CheckCollisionPointRec(mousePos, rect))
        {
            Raylib.DrawRectangleLinesEx(rect, width, highlightColor);
            Raylib.DrawText(text, (int)rect.X + 10, (int)(rect.Y + rect.Height / 2), fontSize, highlightColor);
        }
        else
        {
            Raylib.DrawRectangleLinesEx(rect, width, defaultColor);
            Raylib.DrawText(text, (int)rect.X + 10, (int)(rect.Y + rect.Height / 2), fontSize, defaultColor);
        }
    }

    public void ActIfMouseOnButton()
    {
        var mousePos = Raylib.GetMousePosition();
        if (Raylib.CheckCollisionPointRec(mousePos, rect))
        {
            callback();
        }
    }

}

public class Program
{

    public static string PrettyView(Matrix4x4 mat)
    {
        return $"<{mat.M11}, {mat.M12}, {mat.M13}, {mat.M14}\n\r" +
                $" {mat.M21}, {mat.M22}, {mat.M23}, {mat.M24}\n\r" +
                $" {mat.M31}, {mat.M32}, {mat.M33}, {mat.M34}\n\r" +
                $" {mat.M41}, {mat.M42}, {mat.M43}, {mat.M44}>\n\r";

    }
    public unsafe static void Main(string[] args)
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.Fatal);

        var screen = new Vector2(1024, 768);
        Raylib.InitWindow((int)screen.X, (int)screen.Y, "Hello, world!");
        Raylib.SetTargetFPS(60);
        var defaultCamera = new Camera3D
        {
            Position = new Vector3(0, 0, 20),
            Target = new Vector3(0, 0, 0),
            Up = new Vector3(0, 1, 0),
            FovY = 100f,
            Projection = CameraProjection.Perspective
        };
        var camera = defaultCamera;
        Button[] buttons = [
            new Button(new Rectangle{X = screen.X - 300, Y = 0, Width = 200, Height = 100},
                       "To default disposition",
                       () => {camera = defaultCamera;}),
        ];

        var rubic = new CubicRubic(new CubicRubicConfig(2));

        var rotationAngle = new Vector2(5e-3f, 5e-3f);

        (float min, float max) cameraDistanseLimit = (10, 40);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Gray);
            Raylib.BeginMode3D(camera);

            var ps = rubic.GetDrawable();
            foreach (var (points, color) in ps)
            {
                Raylib.DrawTriangleStrip3D(points, 4, color);
            }

            Raylib.EndMode3D();
            var mouseScroll = Raylib.GetMouseWheelMove();
            if (float.Abs(mouseScroll) > 0)
            {
                var cameraCopy = camera;
                Raylib.CameraMoveToTarget(&cameraCopy, -mouseScroll);
                var dist = Raymath.Vector3Distance(cameraCopy.Position, cameraCopy.Target);
                if (cameraDistanseLimit.min < dist && dist < cameraDistanseLimit.max)
                {
                    camera = cameraCopy;
                }
            }

            foreach (var button in buttons)
            {
                button.Draw();
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                foreach (var button in buttons)
                {
                    button.ActIfMouseOnButton();
                }
            }
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                var delta = Raylib.GetMouseDelta();
                var cameraCopy = camera;
                if (float.Abs(delta.X) > 0)
                {
                    Raylib.CameraYaw(&cameraCopy, -rotationAngle.X * delta.X, true);
                }
                if (float.Abs(delta.Y) > 0)
                {
                    Raylib.CameraPitch(&cameraCopy, -rotationAngle.X * delta.Y, true, true, true);
                }
                camera = cameraCopy;
            }
            Raylib.EndDrawing();

        }

        Raylib.CloseWindow();
    }
}
