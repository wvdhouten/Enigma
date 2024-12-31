namespace Enigma.Console;

using System;
using Enigma.Core;
using Enigma.Core.Config;

public class Program
{
    public static void Main(string[] args)
    {
        var ciphertext = "IFRGRGBLXJJUMQKFFGMDRGXRHBRGWGFVXAQICWNEOMNAHPGMEGBILRBAEHYFNWSDSNIQPSHSWDVMLQLDTWSVDANGIQZFQUEHGSVDCDHDEFDMFCMIFPCFJPFKZHLGRXMWWHZLSLSOVWVGMZFBTPHCTZFWPOWNHGHFWHVNSRYAVUIBXXNSMGIYYUKDPWICGOZRHMVKSJTHWFMJNFOMRGPQVECDSNWOAIEVSBUPJAOJBYKUPCZNCHYGSWVKCADNIXBSPDGFFDYMHOVYXIORUUGNDFKZGLNQKSFIHPEFU";

        var config = new EnigmaM3Config
        {
            LeftRotor = { Name = "II", Position = 17, RingSetting = 11 },
            MiddleRotor = { Name = "IV", Position = 1, RingSetting = 7 },
            RightRotor = { Name = "III", Position = 17, RingSetting = 8 },
            Reflector = "B",
            Plugboard = "SH XW EP AZ KC UJ RO"
        };

        var enigma = new EnigmaM3(config);
        var decipheredText = enigma.Encrypt(ciphertext);

        Console.WriteLine(decipheredText);
        Console.ReadLine();
    }
}
