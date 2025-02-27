namespace Enigma.Core;

using Enigma.Core.Extensions;

public class Reflector
{
    internal int[] Wiring { get; private set; }

    private Reflector(string encoding, CharacterSet? characterSet)
    {
        characterSet ??= CharacterSet.Default;
        Wiring = Utils.Wiring.Decode(encoding, (characterSet ?? CharacterSet.Default).Characters);
    }

    public static Reflector CreateKnown(string name) => name switch
    {
        "B" => new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT", CharacterSet.Default),
        "C" => new Reflector("FVPJIAOYEDRZXWGCTKUQSBNMHL", CharacterSet.Default),
        _ => throw new ArgumentOutOfRangeException(nameof(name), $"Reflector with name '{name}' does not exist.")
    };

    public static Reflector Create(string encoding, CharacterSet? characterSet)
    {
        return new Reflector(encoding, characterSet);
    }

    public static Reflector CreateRandom(out string encoding, CharacterSet? characterSet = null, Random? random = null)
    {
        random ??= new();
        characterSet ??= CharacterSet.Default;
        encoding = characterSet.Characters.SwapPairs(random);

        return new Reflector(encoding, characterSet);
    }

    internal int Resolve(int c)
    {
        return Wiring[c];
    }
}
