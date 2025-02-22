namespace Enigma.Core;

using Enigma.Core.Extensions;
using Enigma.Core.Utils;

public class CharacterSet
{
    public static CharacterSet Default { get; } = new CharacterSet(Constants.BaseCharacterSet);

    public string Characters { get; }

    internal char this[int index] => Characters[index];

    public CharacterSet(string characters)
    {
        Characters = characters;
    }

    internal int IndexOf(char c, char? fallback = null)
    {
        if (Characters.TryIndexOf(c, out var index))
            return index;

        if (fallback is not null)
            return Characters.IndexOf(fallback.Value);

        return -1;
    }
}
