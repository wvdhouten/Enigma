using System.Text;
using Enigma.Core.Config;

namespace Enigma.Core;
public class EnigmaM3
{
    public CharacterSet CharacterSet { get; }

    public Rotor LeftRotor { get; }

    public Rotor MiddleRotor { get; }

    public Rotor RightRotor { get; }

    public Reflector Reflector { get; }

    public Plugboard Plugboard { get; }

    public EnigmaM3(EnigmaM3Config config)
    {
        CharacterSet = CharacterSet.Default;
        Plugboard = Plugboard.Create(config.Plugboard, CharacterSet);
        LeftRotor = Rotor.CreateKnown(config.LeftRotor);
        MiddleRotor = Rotor.CreateKnown(config.MiddleRotor);
        RightRotor = Rotor.CreateKnown(config.RightRotor);
        Reflector = Reflector.CreateKnown(config.Reflector);
    }

    public EnigmaM3(EnigmaMachineConfig config)
    {
        CharacterSet = config.CharacterSet;
        Plugboard = Plugboard.Create(config.Plugboard, CharacterSet);
        LeftRotor = Rotor.Create(config.LeftRotor, CharacterSet);
        MiddleRotor = Rotor.Create(config.MiddleRotor, CharacterSet);
        RightRotor = Rotor.Create(config.RightRotor, CharacterSet);
        Reflector = Reflector.Create(config.Reflector, CharacterSet);
    }

    public EnigmaM3(CharacterSet characterSet, out EnigmaMachineConfig config)
    {
        CharacterSet = characterSet;
        Plugboard = Plugboard.CreateRandom(out string connections, Convert.ToInt32(Math.Floor(CharacterSet.Characters.Length / 4d)), CharacterSet);
        LeftRotor = Rotor.CreateRandom(out RotorConfig leftRotorConfig, CharacterSet);
        MiddleRotor = Rotor.CreateRandom(out RotorConfig middleRotorConfig, CharacterSet);
        RightRotor = Rotor.CreateRandom(out RotorConfig rightRotorConfig, CharacterSet);
        Reflector = Reflector.CreateRandom(out string encoding, CharacterSet);

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

    public string Encrypt(string input, bool skipUnknownCharacters = true, char? fallbackCharacter = null)
    {
        input = input.ToUpper();

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

            if (output.Length % 6 == 5)
                output.Append(' ');

            output.Append(Encrypt(index));
        }
        return output.ToString();
    }

    private char Encrypt(int index)
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
