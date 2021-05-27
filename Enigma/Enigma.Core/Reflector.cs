namespace Enigma.Core
{
    using System;

    public class Reflector
    {
        public int[] Wiring { get; private set; }

        private Reflector(string encoding)
        {
            Wiring = Utils.Wiring.Decode(encoding);
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

        public int Resolve(int c)
        {
            return Wiring[c];
        }
    }
}
