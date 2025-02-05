using System.Data;
using System.Diagnostics;

namespace Cube;

public class Matcher
{
    public CubicRubic Rubic { get; }
    public List<(string, Action<CubicRubic> )> Rotations { get; }

    public Matcher(CubicRubic rubic, List<(string rule, Action<CubicRubic> action)> rotations)
    {
        Rubic = rubic;
        Rotations = rotations;
    }

    public void Process()
    {
        for (var (rule, action) = Rotations.Find(x => Matches(x.Item1));
             rule != string.Empty;
             (rule, action) = Rotations.Find(x => Matches(x.Item1)))
        {
            action(Rubic);
        }
    }


    private void CheckRules(string target)
    {
        Debug.Assert(target.Length == 120);
        var threeSpaces = Enumerable.Repeat(" ", 3).ToString()
                          ?? throw new InvalidOperationException();
        var threeSpacesWithEndl = threeSpaces + '\n';

        for (int i = 0; i < 60; i += 10)
        {
            var substr = new string(target.Skip(i).Take(10).ToArray());
            Debug.Assert(substr.StartsWith(threeSpaces));
            Debug.Assert(substr.EndsWith(threeSpacesWithEndl));
        }

        var subst = new string(target.Skip(110).Take(10).ToArray());
        Debug.Assert(subst.StartsWith(threeSpaces));
        Debug.Assert(subst.EndsWith(threeSpacesWithEndl));
    }

    private bool Matches(string target)
    {
        CheckRules(target);
        var current = Rubic.ToString();
        for (int i = 0; i < current.Length; ++i)
        {
            if (target[i] != 'A' && target[i] != current[i])
            {
                return false;
            }
        }

        return true;
    }
}