using System.Diagnostics;
using System.Numerics;
using System.Text;
using Raylib_cs;

public class TextConfigCubicRubic(CubicRubic rubic)
{
    private readonly CubicRubic rubic = rubic;

    static string ProjectColor(Color col)
    {
        if (col.Equals(Color.Red))
        {
            return "R";
        }
        if (col.Equals(Color.Orange))
        {
            return "O";
        }
        if (col.Equals(Color.Yellow))
        {
            return "Y";
        }
        if (col.Equals(Color.Green))
        {
            return "G";
        }
        if (col.Equals(Color.Blue))
        {
            return "B";
        }
        if (col.Equals(Color.RayWhite))
        {
            return "W";
        }
        throw new UnreachableException("NO SUCH COLOR");
    }

    static IEnumerable<(Vector3 pos, Color color)> Project(CubicRubic rubic, Func<CubicRubic, Cubic[]> sideCbk, Func<(Vector3 pos, Edge edge), bool> dirCbk)
    {
        return sideCbk(rubic).SelectMany(c => c.edges, (cubic, edge) => (cubic.pos, edge))
                            .Where(dirCbk).Select(obj => (obj.pos, obj.edge.color));
    }
    public string at(IEnumerable<(Vector3 pos, Color color)> side, Func<(Vector3 pos, Color color), bool> cbk)
    {
        return side.Where(cbk).Select(obj => ProjectColor(obj.color)).Aggregate((s, c) => s + c);
    }

    public string GetTextConfig()
    {
        var builder = new StringBuilder();
        var front = Project(rubic, rubic => rubic.FrontSide, static obj => obj.edge.dir.Z == 1);
        var back = Project(rubic, rubic => rubic.BackSide, static obj => obj.edge.dir.Z == -1);
        var left = Project(rubic, rubic => rubic.LeftSide, static obj => obj.edge.dir.X == -1);
        var right = Project(rubic, rubic => rubic.RightSide, static obj => obj.edge.dir.X == 1);
        var upper = Project(rubic, rubic => rubic.UpperSide, static obj => obj.edge.dir.Y == 1);
        var down = Project(rubic, rubic => rubic.DownSide, static obj => obj.edge.dir.Y == -1);

        Debug.Assert(front.Count() == 9);
        Debug.Assert(back.Count() == 9);
        Debug.Assert(left.Count() == 9);
        Debug.Assert(right.Count() == 9);
        Debug.Assert(upper.Count() == 9);
        Debug.Assert(down.Count() == 9);

        string str =
        $@"
           {at(front, (obj) => obj.pos.Y == 1)}{at(right, (obj) => obj.pos.Y == 1)}
           {at(front, (obj) => obj.pos.Y == 0)}{at(right, (obj) => obj.pos.Y == 0)}
           {at(front, (obj) => obj.pos.Y == -1)}{at(right, (obj) => obj.pos.Y == -1)}
           ";

        return "";
    }

}