namespace Enigma.Core
{
    using System;
    using Enigma.Core.Utils;

    public class Rotor
    {
        private readonly int[] _forwardWiring;
        private readonly int[] _backwardWiring;

        private readonly int _notchPosition;
        private readonly bool _hasDoubleNotch;

        public string Name { get; private set; }
        public int RotorPosition { get; private set; }
        public int RingSetting { get; private set; }

        private Rotor(string name, string encoding, int rotorPosition, int ringSetting, int? notchPosition = null)
        {
            Name = name;
            _forwardWiring = Wiring.Decode(encoding);
            _backwardWiring = Wiring.Inverse(_forwardWiring);
            RotorPosition = rotorPosition;
            RingSetting = ringSetting;
            _notchPosition = notchPosition ?? -1;
            _hasDoubleNotch = notchPosition == null;
        }

        public static Rotor Create(RotorConfig settings)
        {
            return Create(settings.Name, settings.Position, settings.RingSetting);
        }

        public static Rotor Create(string name, int position, int ringSetting)
        {
            return name switch
            {
                "I" => new Rotor("I", "EKMFLGDQVZNTOWYHXUSPAIBRCJ", position, ringSetting, 17),
                "II" => new Rotor("II", "AJDKSIRUXBLHWTMCQGZNPYFVOE", position, ringSetting, 5),
                "III" => new Rotor("III", "BDFHJLCPRTXVZNYEIWGAKMUSQO", position, ringSetting, 22),
                "IV" => new Rotor("IV", "ESOVPZJAYQUIRHXLNFTGKDCMWB", position, ringSetting, 10),
                "V" => new Rotor("V", "VZBRGITYUPSDNHLXAWMJQOFECK", position, ringSetting, 26),
                "VI" => new Rotor("VI", "JPGVOUMFYQBENHZRDKASXLICTW", position, ringSetting, null),
                "VII" => new Rotor("VII", "NZJHGRCXMYSWBOUFAIVLPEKQDT", position, ringSetting, null),
                "VIII" => new Rotor("VIII", "FKQHTLXOCBJSPDZRAMEWNIUYGV", position, ringSetting, null),
                _ => throw new Exception("Rotor does not exist")
            };
        }


        public int Forward(int character)
        {
            return Encipher(character, RotorPosition, RingSetting, _forwardWiring);
        }

        public int Backward(int character)
        {
            return Encipher(character, RotorPosition, RingSetting, _backwardWiring);
        }

        private static int Encipher(int character, int position, int ringSetting, int[] mapping)
        {
            var shift = position - ringSetting;
            return (mapping[(character + shift + 26) % 26] - shift + 26) % 26;
        }

        public bool IsAtNotch()
        {
            return _hasDoubleNotch
                ? RotorPosition == 13 || RotorPosition == 26
                : _notchPosition == RotorPosition;

        }

        public void Rotate()
        {
            RotorPosition = RotorPosition % 26 + 1;
        }
    }

}
