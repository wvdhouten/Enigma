namespace Enigma.Core
{
    using System;
    using Core.Utils;

    public class Reflector
    {
        private readonly int[] _wiring;

        private Reflector(string encoding)
        {
            _wiring = Wiring.Decode(encoding);
        }

        public static Reflector Create(string name)
        {
            return name switch
            {
                "B" => new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT"),
                "C" => new Reflector("FVPJIAOYEDRZXWGCTKUQSBNMHL"),
                _ => throw new Exception("Reflector does not exist")
            };
        }

        public int Forward(int c)
        {
            return _wiring[c];
        }
    }
}
