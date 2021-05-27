namespace Enigma.Console
{
    using System;
    using Enigma.Core;
    using Enigma.Core.Config;

    public class Program
    {
        public static void Main(string[] args)
        {
            var ciphertext = "jgbfa xqodz fxkkw nfval bfgoy dyhlg y";

            var config = new EnigmaM3Config
            {
                LeftRotor = { Name = "VI", Position = 1, RingSetting = 1 },
                MiddleRotor = { Name = "I", Position = 17, RingSetting = 1 },
                RightRotor = { Name = "III", Position = 12, RingSetting = 1 },
                Reflector = "B",
                Plugboard = "bq cr di ej kw mt os px uz gh"
            };

            var enigma = new EnigmaM3(config);
            var decipheredText = enigma.Encrypt(ciphertext);

            Console.WriteLine(decipheredText);
            Console.ReadLine();
        }
    }
}
