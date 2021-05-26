namespace Enigma.Core
{
    using System;

    public class Reflector
    {
        protected int[] ForwardWiring { get; set; }

        public Reflector(string encoding)
        {
            ForwardWiring = DecodeWiring(encoding);
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

        public int Forward(int c)
        {
            return ForwardWiring[c];
        }

    }
}
