using System.Diagnostics;
using Cube;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using Raylib_cs;
using Color = Raylib_cs.Color;

class Program
{
    static void NicePrint(string schema)
    {
        foreach (char c in schema)
        {
            var color = c switch
            {
                'R' => ConsoleColor.DarkRed,
                'G' => ConsoleColor.Green,
                'B' => ConsoleColor.DarkBlue,
                'W' => ConsoleColor.White,
                'Y' => ConsoleColor.Yellow,
                'O' => ConsoleColor.DarkMagenta,
                '\n' => ConsoleColor.Black,
                '\0' => ConsoleColor.Black,
                ' ' => ConsoleColor.Black,
            };

            if (c == '\n')
            {
                Console.WriteLine();
            }
            else
            {
                Console.BackgroundColor = color;
                Console.Write(' ');
                Console.ResetColor();
                Console.Write(' ');
            }
        }

        Console.ResetColor();
    }

    enum GameState
    {
        Animation,
        Nothing
    }

    static void Main(string[] args)
    {
        CubicRubic rubic = new CubicRubic();

        Raylib.InitWindow(800, 600, "Raylib-cs 3d Cube");
        Raylib.SetTargetFPS(60);

        var camera = new Camera3D();
        camera.Position = new Vector3(8.0f, 8.0f, 8.0f);
        camera.Target = new Vector3(0.0f, 0.0f, 0.0f);

        camera.Up = new Vector3(0.0f, 1.0f, 0.0f);
        camera.FovY = 45.0f;

        camera.Projection = CameraProjection.Perspective;


        var state = GameState.Nothing;
        IEnumerator<CubicRubic>? iter = null;
        Drawing.Button button = null;
        while (!Raylib.WindowShouldClose()) // Главный цикл
        {
            Drawing.CameraRotationButtonPressed(ref camera);

            switch (state)
            {
                case GameState.Nothing:
                    button = Drawing.RotationButtonPressed();
                    if (button != null)
                    {
                        button.Color = Color.Blue;
                        state = GameState.Animation;
                        iter = button.Rotation(rubic, null).GetEnumerator();
                    }

                    break;
                case GameState.Animation:
                    Debug.Assert(iter != null);
                    if (!iter.MoveNext())
                    {
                        if (button != null) button.Color = Color.DarkGray;
                        iter.Dispose();
                        iter = null;
                        state = GameState.Nothing;
                    }
                    else
                    {
                        rubic = iter.Current;
                    }

                    break;
            }

            Debug.Assert(rubic != null);
            Drawing.DrawFromInstance(rubic.CreateDrawable(), ref camera);
        }

        if (iter != null)
        {
            iter.Dispose();
        }

        Raylib.CloseWindow(); // Закрытие окна
    }
}