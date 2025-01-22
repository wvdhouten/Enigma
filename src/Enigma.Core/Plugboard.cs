using Enigma.Core.Extensions;

namespace Enigma.Core;
public partial class Plugboard
{
    internal int[] Wiring { get; }

    private Plugboard(string connections, CharacterSet characterSet)
    {
        Wiring = Utils.Wiring.Decode(characterSet.Characters, characterSet.Characters);

        DecodeConnections(connections, characterSet.Characters);
    }

    internal static Plugboard Create(string connections, CharacterSet characterSet) => new(connections, characterSet);

    internal static Plugboard CreateRandom(out string connections, int connectionCount, CharacterSet? characterSet, Random? random = null)
    {
        random ??= new();
        characterSet ??= CharacterSet.Default;
        var characters = characterSet.Characters;

        List<string> pairs = [];
        for (int i = 0; i < connectionCount; i++) {
            characters = characters.TakeRandom(out var left, random);
            characters = characters.TakeRandom(out var right, random);

            pairs.Add(string.Empty + left + right);
        }

        connections = string.Join("", pairs);

        return new Plugboard(connections, characterSet);
    }

    internal int Resolve(int c) => Wiring[c];

    private void DecodeConnections(string connections, string characters)
    {
        if (string.IsNullOrEmpty(connections))
            return;

        var pairs = GetPairs(connections);
        var pluggedCharacters = new HashSet<int>();

        foreach (var pair in pairs)
            SwapPair(pluggedCharacters, pair, characters);
    }

    private static IEnumerable<string> GetPairs(string connections)
    {
        if (connections.Length % 2 > 0)
            throw new ArgumentException("Connections must be maid in pairs.", nameof(connections));

        if (connections.Distinct().Count() < connections.Length)
            throw new ArgumentException("Connections contain duplicated characters.", nameof(connections));

        for (var i = 0; i < connections.Length; i += 2)
            yield return connections.Substring(i, Math.Min(2, connections.Length - i));
    }

    private void SwapPair(HashSet<int> pluggedCharacters, string pair, string characters)
    {
        if (pair.Length != 2)
            throw new ArgumentException($"Invalid pair '{pair}' detected.", nameof(pair));

        var c1 = characters.IndexOrCrash(pair[0]);
        var c2 = characters.IndexOrCrash(pair[1]);

        if (!pluggedCharacters.Add(c1) || !pluggedCharacters.Add(c2))
            throw new ArgumentException($"Duplicate connection '{c1}-{c2}' detected.", nameof(pair));

        Wiring[c1] = c2;
        Wiring[c2] = c1;
    }
}
