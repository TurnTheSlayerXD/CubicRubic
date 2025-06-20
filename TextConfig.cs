
using System.Diagnostics;
using System.Numerics;
using System.Text;
using Raylib_cs;

public static class TextConfigCubicRubic
{


    static IEnumerable<(Vector3 pos, Color color)> Project(CubicRubic rubic, Func<CubicRubic, Cubic[]> sideCbk, Func<(Vector3 pos, Edge edge), bool> dirCbk)
    {
        return sideCbk(rubic).SelectMany(c => c.edges, (cubic, edge) => (cubic.pos, edge))
                            .Where(dirCbk).Select(obj => (obj.pos, obj.edge.color));
    }

    public static string GetTextConfig(CubicRubic rubic)
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

        string str = $@"



                        ";

        return "";
    }

}