using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using Raylib_cs;
using Raylib_cs;

namespace Cube;

public static class Drawing
{
    public const float Epsilon = 0.0001f;
    private const float XAngle = 0.02f;
    private const float YAngle = 0.02f;

    public class Button
    {
        public Color Color = Color.DarkGray;
        public string Name { get; init; }
        public Rectangle Area { get; init; }

        public Func<CubicRubic, Matrix<float>?, IEnumerable<CubicRubic>> Rotation { get; init; }
    }

    private static readonly List<Button> Buttons =
    [
        new() { Name = "R", Area = new Rectangle(300, 10, 50, 50), Rotation = Rotator.DoR },
        new() { Name = "RC", Area = new Rectangle(400, 10, 50, 50), Rotation = Rotator.DoRC },
        new() { Name = "L", Area = new Rectangle(500, 10, 50, 50), Rotation = Rotator.DoL },
        new() { Name = "LC", Area = new Rectangle(600, 10, 50, 50), Rotation = Rotator.DoLC },
        new() { Name = "F", Area = new Rectangle(400, 70, 50, 50), Rotation = Rotator.DoF },
        new() { Name = "FC", Area = new Rectangle(500, 70, 50, 50), Rotation = Rotator.DoF },
        new() { Name = "B", Area = new Rectangle(600, 70, 50, 50), Rotation = Rotator.DoB },
        new() { Name = "BC", Area = new Rectangle(700, 70, 50, 50), Rotation = Rotator.DoBC },
        new() { Name = "U", Area = new Rectangle(700, 10, 50, 50), Rotation = Rotator.DoU },
        new() { Name = "UC", Area = new Rectangle(300, 70, 50, 50), Rotation = Rotator.DoUC },
        new() { Name = "D", Area = new Rectangle(300, 130, 50, 50), Rotation = Rotator.DoD },
        new() { Name = "DC", Area = new Rectangle(400, 130, 50, 50), Rotation = Rotator.DoDC },
    ];

   
    public static void DrawFromInstance(DrawableRubic rubic, ref Camera3D camera)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.DarkGray);
        Raylib.BeginMode3D(camera); // Включаем 3D-режим

        Raylib.DrawGrid(10, 1.0f);

        foreach (var (color, vertices) in rubic.Edges)
        {
            Raylib.DrawTriangleStrip3D(vertices, 4, color);
            var ordered = DrawableRubic.GetOrderedVerticles(vertices);
            Raylib.DrawLine3D(ordered[0], ordered[1], Color.Black);
            Raylib.DrawLine3D(ordered[1], ordered[2], Color.Black);
            Raylib.DrawLine3D(ordered[2], ordered[3], Color.Black);
            Raylib.DrawLine3D(ordered[0], ordered[3], Color.Black);
        }

        Raylib.EndMode3D();

        Raylib.DrawText("Raylib-cs 3D Cube", 10, 10, 20, Color.White);

        foreach (var button in Buttons)
        {
            Raylib.DrawRectangleLines((int)button.Area.X, (int)button.Area.Y, (int)button.Area.Width,
                (int)button.Area.Height, Color.Black);
            Raylib.DrawRectangle((int)button.Area.X + 1, (int)button.Area.Y + 1, (int)button.Area.Width - 2,
                (int)button.Area.Height - 2, button.Color);
            Raylib.DrawText(button.Name, (int)button.Area.X + 10, (int)button.Area.Y + 10,
                20, Color.White);
        }


        Raylib.EndDrawing();
    }

    public static Button? RotationButtonPressed()
    {
        var pos = Raylib.GetMousePosition();
        var button = Buttons.Find(b => Raylib.CheckCollisionPointRec(pos, b.Area));
        if (Raylib.IsMouseButtonPressed(MouseButton.Left) && button != null)
        {
            return button;
        }

        return null;
    }


    public static void CameraRotationButtonPressed(ref Camera3D camera)
    {
        if (Raylib.IsKeyDown(KeyboardKey.Left))
        {
            camera.Position = Rotator.Rotator_Y(Rotator.Singular, -YAngle)(camera.Position);
        }

        if (Raylib.IsKeyDown(KeyboardKey.Right))
        {
            camera.Position = Rotator.Rotator_Y(Rotator.Singular, YAngle)(camera.Position);
        }

        if (Raylib.IsKeyDown(KeyboardKey.Up))
        {
            camera.Position = Rotator.Rotator_X(Rotator.Singular, -XAngle)(camera.Position);
        }

        if (Raylib.IsKeyDown(KeyboardKey.Down))
        {
            camera.Position = Rotator.Rotator_X(Rotator.Singular, XAngle)(camera.Position);
        }
    }
}