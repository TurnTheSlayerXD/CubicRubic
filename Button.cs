using Raylib_cs;
public class Button
{

    public const float defaultWidth = 200;
    public const float defaultHeight = 50;

    Action callback;
    Rectangle rect = new Rectangle { X = 0, Y = 0, Width = defaultWidth, Height = defaultHeight };
    readonly string text;
    readonly Color highlightColor = Color.Pink;
    readonly Color defaultColor = Color.Black;
    readonly float lineWidth = 5;
    public static readonly int fontSize = 20;

    bool hasOutline;
    public Button(float X, float Y, string text, Action callback, bool hasOutline = true)
    {
        rect = rect with { X = X, Y = Y };
        this.callback = callback;
        this.text = text;
        this.callback = callback;
        this.hasOutline = hasOutline;
    }

    public void Draw()
    {
        var mousePos = Raylib.GetMousePosition();
        if (Raylib.CheckCollisionPointRec(mousePos, rect))
        {
            if (hasOutline)
            {
                Raylib.DrawRectangleLinesEx(rect, lineWidth, highlightColor);
            }
            Raylib.DrawText(text, (int)rect.X + 10, (int)(rect.Y + rect.Height / 2), fontSize, highlightColor);
        }
        else
        {
            if (hasOutline)
            {
                Raylib.DrawRectangleLinesEx(rect, lineWidth, defaultColor);
            }
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
