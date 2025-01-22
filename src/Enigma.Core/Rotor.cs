namespace Enigma.Core;

using System;
using Enigma.Core.Config;
using Enigma.Core.Extensions;
using Enigma.Core.Utils;

public class Rotor
{
    private readonly int[] _forwardWiring;
    private readonly int[] _backwardWiring;

    internal int Position { get; private set; }

    internal int RingSetting { get; }

    internal int[] NotchPositions { get; }

    private int Positions => _forwardWiring.Length;

    private int Shift => Position - RingSetting;

    private Rotor(string encoding, int rotorPosition, int ringSetting, int[] notchPositions, CharacterSet? characterSet = null)
    {
        characterSet ??= CharacterSet.Default;

        _forwardWiring = Wiring.Decode(encoding, characterSet.Characters);
        _backwardWiring = Wiring.Invert(_forwardWiring);
        Position = rotorPosition;
        RingSetting = ringSetting;
        NotchPositions = notchPositions;
    }

    public static Rotor CreateKnown(NamedRotorConfig config) => CreateKnown(config.Name, config.Position, config.RingSetting);

    public static Rotor CreateKnown(string name, int position, int ringSetting) => name switch
    {
        "I" => new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", position, ringSetting, [17]),
        "II" => new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE", position, ringSetting, [5]),
        "III" => new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO", position, ringSetting, [22]),
        "IV" => new Rotor("ESOVPZJAYQUIRHXLNFTGKDCMWB", position, ringSetting, [10]),
        "V" => new Rotor("VZBRGITYUPSDNHLXAWMJQOFECK", position, ringSetting, [26]),
        "VI" => new Rotor("JPGVOUMFYQBENHZRDKASXLICTW", position, ringSetting, [13, 26]),
        "VII" => new Rotor("NZJHGRCXMYSWBOUFAIVLPEKQDT", position, ringSetting, [13, 26]),
        "VIII" => new Rotor("FKQHTLXOCBJSPDZRAMEWNIUYGV", position, ringSetting, [13, 26]),
        _ => throw new Exception("Rotor does not exist")
    };

    public static Rotor Create(RotorConfig config, CharacterSet? characterSet)
    {
        return new Rotor(config.Encoding, config.Position, config.RingSetting, config.NotchPositions, characterSet);
    }

    public static Rotor CreateCustom(string encoding, int position, int ringSetting, int[] notchPositions, CharacterSet? characterSet)
    {
        return new Rotor(encoding, position, ringSetting, notchPositions, characterSet);
    }

    public static Rotor CreateRandom(out RotorConfig config, CharacterSet? characterSet, Random? random = null)
    {
        random ??= new();
        characterSet ??= CharacterSet.Default;
        config = new RotorConfig
        {
            Encoding = characterSet.Characters.Shuffle(random),
            Position = random.Next(0, characterSet.Characters.Length - 1),
            RingSetting = random.Next(0, characterSet.Characters.Length - 1),
            NotchPositions = [random.Next(0, characterSet.Characters.Length - 1)]
        };

        return new Rotor(config.Encoding, config.Position, config.RingSetting, config.NotchPositions, characterSet);
    }

    public static Rotor CreateRandom(out string encoding, out int position, out int ringSetting, out int[] notchPositions, CharacterSet? characterSet, Random? random = null)
    {
        random ??= new();
        characterSet ??= CharacterSet.Default;
        encoding = characterSet.Characters.Shuffle(random);
        position = random.Next(0, characterSet.Characters.Length - 1);
        ringSetting = random.Next(0, characterSet.Characters.Length - 1);
        notchPositions = [random.Next(0, characterSet.Characters.Length - 1)];

        return new Rotor(encoding, position, ringSetting, notchPositions, characterSet);
    }

    internal int Forward(int character) => Encipher(character, _forwardWiring);

    internal int Backward(int character) => Encipher(character, _backwardWiring);

    private int Encipher(int character, int[] mapping) => (mapping[(character + Shift + Positions) % Positions] - Shift + Positions) % Positions;

    public bool IsAtNotch() => NotchPositions.Contains(Position);

    public void Rotate() => Position = Position % Positions + 1;
}
