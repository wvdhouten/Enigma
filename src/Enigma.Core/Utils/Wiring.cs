namespace Enigma.Core.Utils;

using Enigma.Core.Extensions;
using System.Linq;

internal static class Wiring
{
    internal static int[] Decode(string encoding, string characterSet)
    {
        if (!new HashSet<char>(characterSet.ToCharArray()).SetEquals(encoding))
            throw new ArgumentException("Encoding does not conform to character set.", nameof(encoding));

        return encoding.Select(characterSet.IndexOrCrash).ToArray();
    }

    internal static int[] Invert(int[] wiring)
    {
        var inverse = new int[wiring.Length];

        for (var i = 0; i < wiring.Length; i++)
            inverse[wiring[i]] = i;

        return inverse;
    }
}
