namespace Enigma.Core;

using System.Text;
using Enigma.Core.Config;

public class EnigmaMachine
{
    public CharacterSet CharacterSet { get; }

    public Rotor LeftRotor { get; }

    public Rotor MiddleRotor { get; }

    public Rotor RightRotor { get; }

    public Reflector Reflector { get; }

    public Plugboard Plugboard { get; }

    public int? BlockSize { get; set; } = 5;

    public EnigmaMachine(EnigmaM3Config config)
    {
        CharacterSet = CharacterSet.Default;
        Plugboard = Plugboard.Create(config.Plugboard, CharacterSet);
        LeftRotor = Rotor.CreateKnown(config.LeftRotor);
        MiddleRotor = Rotor.CreateKnown(config.MiddleRotor);
        RightRotor = Rotor.CreateKnown(config.RightRotor);
        Reflector = Reflector.CreateKnown(config.Reflector);
    }

    public EnigmaMachine(EnigmaMachineConfig config)
    {
        CharacterSet = config.CharacterSet;
        Plugboard = Plugboard.Create(config.Plugboard, CharacterSet);
        LeftRotor = Rotor.Create(config.LeftRotor, CharacterSet);
        MiddleRotor = Rotor.Create(config.MiddleRotor, CharacterSet);
        RightRotor = Rotor.Create(config.RightRotor, CharacterSet);
        Reflector = Reflector.Create(config.Reflector, CharacterSet);
    }

    public EnigmaMachine(CharacterSet characterSet, out EnigmaMachineConfig config, int? seed = null)
    {
        Random random = seed.HasValue ? new(seed.Value) : new();

        CharacterSet = characterSet;
        Plugboard = Plugboard.CreateRandom(out string connections, Convert.ToInt32(Math.Floor(CharacterSet.Characters.Length / 4d)), CharacterSet, random);
        LeftRotor = Rotor.CreateRandom(out RotorConfig leftRotorConfig, CharacterSet, random);
        MiddleRotor = Rotor.CreateRandom(out RotorConfig middleRotorConfig, CharacterSet, random);
        RightRotor = Rotor.CreateRandom(out RotorConfig rightRotorConfig, CharacterSet, random);
        Reflector = Reflector.CreateRandom(out string encoding, CharacterSet, random);

        config = new EnigmaMachineConfig
        {
            CharacterSet = CharacterSet,
            Plugboard = connections,
            LeftRotor = leftRotorConfig,
            MiddleRotor = middleRotorConfig,
            RightRotor = rightRotorConfig,
            Reflector = encoding
        };
    }

    public void Rotate()
    {
        if (MiddleRotor.IsAtNotch())
        {
            // Middle rotor double-steps.
            MiddleRotor.Rotate();
            LeftRotor.Rotate();
        }

        if (RightRotor.IsAtNotch())
            MiddleRotor.Rotate();
        
        RightRotor.Rotate();
    }

    public string Encipher(string input, bool skipUnknownCharacters = true, char? fallbackCharacter = null)
    {
        var output = new StringBuilder();
        foreach (var c in input)
        {
            var index = CharacterSet.IndexOf(c, fallbackCharacter);
            if (index < 0)
            { 
                if (skipUnknownCharacters)
                    continue;

                throw new IndexOutOfRangeException($"Character '{c}' not found in character set.");
            }

            if (BlockSize is not null && output.Length % (BlockSize + 1) == BlockSize)
                output.Append(' ');

            output.Append(Encipher(index));
        }
        return output.ToString();
    }

    private char Encipher(int index)
    {
        Rotate();

        index = Plugboard.Resolve(index);

        index = RightRotor.Forward(index);
        index = MiddleRotor.Forward(index);
        index = LeftRotor.Forward(index);

        index = Reflector.Resolve(index);

        index = LeftRotor.Backward(index);
        index = MiddleRotor.Backward(index);
        index = RightRotor.Backward(index);

        index = Plugboard.Resolve(index);

        return CharacterSet[index];
    }
}
