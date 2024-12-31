
using Enigma.Core.Extensions;

namespace Enigma.Core;
public partial class Plugboard
{
    private static readonly char[] pairDelimiters = [' ', '.', ',', ';', '-', '|'];

    internal int[] Wiring { get; }

    private Plugboard(string connections, CharacterSet characterSet)
    {
        Wiring = Utils.Wiring.Decode(characterSet.Characters, characterSet.Characters);

        DecodeConnections(connections, characterSet.Characters);
    }

    internal static Plugboard Create(string connections, CharacterSet characterSet) => new(connections, characterSet);

    internal static Plugboard CreateRandom(out string connections, int connectionCount, CharacterSet characterSet)
    {
        characterSet ??= CharacterSet.Default;
        var characters = characterSet.Characters;

        List<string> pairs = [];
        for (int i = 0; i < connectionCount; i++) {
            characters = characters.TakeRandom(out var left);
            characters = characters.TakeRandom(out var right);

            pairs.Add(string.Empty + left + right);
        }

        connections = string.Join(' ', pairs);

        return new Plugboard(connections, characterSet);
    }

    internal int Resolve(int c) => Wiring[c];

    private void DecodeConnections(string connections, string characters)
    {
        if (string.IsNullOrEmpty(connections))
            return;

        var pairs = connections.Split(pairDelimiters);
        var pluggedCharacters = new HashSet<int>();

        foreach (var pair in pairs)
            SwapPair(pluggedCharacters, pair, characters);
    }

    private void SwapPair(HashSet<int> pluggedCharacters, string pair, string characters)
    {
        if (pair.Length != 2)
            throw new ArgumentException($"Invalid pair '{pair}' detected.", nameof(pair));

        var c1 = characters.IndexOf(pair[0]);
        var c2 = characters.IndexOf(pair[1]);

        if (!pluggedCharacters.Add(c1) || !pluggedCharacters.Add(c2))
            throw new ArgumentException($"Duplicate connection '{c1}-{c2}' detected.", nameof(pair));

        Wiring[c1] = c2;
        Wiring[c2] = c1;
    }
}
