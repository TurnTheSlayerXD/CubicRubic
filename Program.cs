using Raylib_cs;
using System.Numerics;

public class CameraRef(Camera3D camera)
{
    public Camera3D obj = camera;
}
public class Program
{

    public unsafe static void DoLoop(CubicRubic rubic, CubicRubicConfig config, CameraRef camera, List<Button> buttons)
    {
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Gray);

            Raylib.BeginMode3D(camera.obj);
            var ps = rubic.GetDrawable();
            foreach (var (points, color) in ps)
            {
                Raylib.DrawTriangleStrip3D(points, 4, color);
                Raylib.DrawCylinderEx(points[0], points[1], config.outlineWidth, config.outlineWidth, 0, config.outlineColor);
                Raylib.DrawCylinderEx(points[1], points[3], config.outlineWidth, config.outlineWidth, 0, config.outlineColor);
                Raylib.DrawCylinderEx(points[0], points[2], config.outlineWidth, config.outlineWidth, 0, config.outlineColor);
                Raylib.DrawCylinderEx(points[2], points[3], config.outlineWidth, config.outlineWidth, 0, config.outlineColor);
            }
            Raylib.EndMode3D();

            var mouseScroll = Raylib.GetMouseWheelMove();
            if (float.Abs(mouseScroll) > 0)
            {
                var cameraCopy = camera.obj;
                Raylib.CameraMoveToTarget(&cameraCopy, -mouseScroll);
                var dist = Raymath.Vector3Distance(cameraCopy.Position, cameraCopy.Target);
                if (config.cameraDistanseLimit.min < dist && dist < config.cameraDistanseLimit.max)
                {
                    camera.obj = cameraCopy;
                }
            }

            Raylib.DrawText("Clockwise actions", (int)config.buttonsLeftColumnX, 10, Button.fontSize, Color.Black);
            Raylib.DrawText("CounterClockwise actions", (int)config.buttonsRightColumnX, 10, Button.fontSize, Color.Black);

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
                var cameraCopy = camera.obj;
                if (float.Abs(delta.X) > 0)
                {
                    Raylib.CameraYaw(&cameraCopy, -config.cameraRotationAngle.X * delta.X, true);
                }
                if (float.Abs(delta.Y) > 0)
                {
                    Raylib.CameraPitch(&cameraCopy, -config.cameraRotationAngle.X * delta.Y, true, true, true);
                }
                camera.obj = cameraCopy;
            }
            Raylib.EndDrawing();

        }

        Raylib.CloseWindow();
    }

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
            FovY = 100,
            Projection = CameraProjection.Perspective
        };
        var camera = new CameraRef(defaultCamera);
        var config = new CubicRubicConfig { edgeLength = 3, buttonsLeftColumnX = 100, buttonsRightColumnX = screen.X - 300 };
        var rubic = new CubicRubic(config);

        float Y = -Button.defaultHeight + 20;
        float NextY()
        {
            Y += Button.defaultHeight + 30;
            return Y;
        }

        List<Button> buttons = new List<Button>();
        buttons.AddRange([
            new Button(screen.X / 2 - Button.defaultWidth / 2, 10,
                       "To default disposition",
                       () => camera.obj = defaultCamera,
                       false),

            new Button(config.buttonsRightColumnX, NextY(),
                       "Left Rotation",
                       () => rubic.AddRotation(new LeftRotation { clockwise = false })),
            new Button(config.buttonsRightColumnX, NextY(),
                       "Right Rotation",
                       () => rubic.AddRotation(new RightRotation{ clockwise = false })),
            new Button(config.buttonsRightColumnX, NextY(),
                       "Upper Rotation",
                       () => rubic.AddRotation(new UpperRotation{ clockwise = false })),
            new Button(config.buttonsRightColumnX, NextY(),
                       "Down Rotation",
                       () => rubic.AddRotation(new DownRotation{ clockwise = false })),
            new Button(config.buttonsRightColumnX, NextY(),
                       "Front Rotation",
                       () => rubic.AddRotation(new FrontRotation{ clockwise = false })),
            new Button(config.buttonsRightColumnX, NextY(),
                       "Back Rotation",
                       () => rubic.AddRotation(new BackRotation{ clockwise = false })),
        ]);

        Y = -Button.defaultHeight + 20;
        buttons.AddRange([
            new Button(config.buttonsLeftColumnX, NextY(),
                       "Left Rotation",
                        () => rubic.AddRotation(new LeftRotation { clockwise = true })),
            new Button(config.buttonsLeftColumnX, NextY(),
                        "Right Rotation",
                        () => rubic.AddRotation(new RightRotation{ clockwise = true })),
            new Button(config.buttonsLeftColumnX, NextY(),
                        "Upper Rotation",
                        () => rubic.AddRotation(new UpperRotation{ clockwise = true })),
            new Button(config.buttonsLeftColumnX, NextY(),
                        "Down Rotation",
                        () => rubic.AddRotation(new DownRotation{ clockwise = true })),
            new Button(config.buttonsLeftColumnX, NextY(),
                        "Front Rotation",
                        () => rubic.AddRotation(new FrontRotation{ clockwise = true })),
            new Button(config.buttonsLeftColumnX, NextY(),
                        "Back Rotation",
                        () => rubic.AddRotation(new BackRotation{ clockwise = true })),]);

        DoLoop(rubic, config, camera, buttons);
    }
}
