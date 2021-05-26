namespace Enigma.Core
{
    using System;

    public class Rotor
    {
        public string Name { get; private set; }
        protected int[] ForwardWiring { get; set; }
        protected int[] BackwardWiring { get; set; }

        protected int RotorPosition { get; set; }
        protected int NotchPosition { get; set; }
        protected int RingSetting { get; set; }

        private readonly bool _hasDoubleNotch;

        private Rotor(string name, string encoding, int rotorPosition, int notchPosition, int ringSetting, bool hasDoubleNotch = false)
        {
            Name = name;
            ForwardWiring = DecodeWiring(encoding);
            BackwardWiring = InverseWiring(ForwardWiring);
            RotorPosition = rotorPosition;
            NotchPosition = notchPosition;
            RingSetting = ringSetting;
            _hasDoubleNotch = hasDoubleNotch;
        }

        public static Rotor Create(string name, int position, int ringSetting)
        {
            return name switch
            {
                "I" => new Rotor("I", "EKMFLGDQVZNTOWYHXUSPAIBRCJ", position, 17, ringSetting),
                "II" => new Rotor("II", "AJDKSIRUXBLHWTMCQGZNPYFVOE", position, 5, ringSetting),
                "III" => new Rotor("III", "BDFHJLCPRTXVZNYEIWGAKMUSQO", position, 22, ringSetting),
                "IV" => new Rotor("IV", "ESOVPZJAYQUIRHXLNFTGKDCMWB", position, 10, ringSetting),
                "V" => new Rotor("V", "VZBRGITYUPSDNHLXAWMJQOFECK", position, 0, ringSetting),
                "VI" => new Rotor("VI", "JPGVOUMFYQBENHZRDKASXLICTW", position, -1, ringSetting, true),
                "VII" => new Rotor("VII", "NZJHGRCXMYSWBOUFAIVLPEKQDT", position, -1, ringSetting, true),
                "VIII" => new Rotor("VIII", "FKQHTLXOCBJSPDZRAMEWNIUYGV", position, -1, ringSetting, true),
                _ => throw new Exception("Rotor does not exist")
            };
        }

        protected static int[] DecodeWiring(string encoding)
        {
            var charWiring = encoding.ToCharArray();
            var wiring = new int[charWiring.Length];
            for (var i = 0; i < charWiring.Length; i++)
            {
                wiring[i] = charWiring[i] - 65;
            }
            return wiring;
        }

        protected static int[] InverseWiring(int[] wiring)
        {
            var inverse = new int[wiring.Length];
            for (var i = 0; i < wiring.Length; i++)
            {
                var forward = wiring[i];
                inverse[forward] = i;
            }
            return inverse;
        }

        protected static int Encipher(int k, int pos, int ring, int[] mapping)
        {
            var shift = pos - ring;
            return (mapping[(k + shift + 26) % 26] - shift + 26) % 26;
        }

        public int Forward(int character)
        {
            return Encipher(character, RotorPosition, RingSetting, ForwardWiring);
        }

        public int Backward(int character)
        {
            return Encipher(character, RotorPosition, RingSetting, BackwardWiring);
        }

        public bool IsAtNotch()
        {
            return _hasDoubleNotch
                ? RotorPosition == 13 || RotorPosition == 0
                : NotchPosition == RotorPosition;

        }

        public void TurnOver()
        {
            RotorPosition = (RotorPosition + 1) % 26;
        }
    }

}
