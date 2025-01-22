namespace Enigma.Console;

using System;
using Enigma.Core;
using Enigma.Core.Config;

public class Program
{
    public static void Main()
    {
        var input = " jJAWUMhnSqVqCQp KasxrmgCZHinILIDZgwZmfxHXqT";

        var characterSet = new CharacterSet("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ .");
        var enigma = new EnigmaMachine(new EnigmaMachineConfig
        {
            CharacterSet = characterSet,
            Plugboard = "KhgCkpODjRorEuxqGbVJyidQWH",
            LeftRotor = new RotorConfig { Encoding = "Y.OZdwpIakLobKnDzCxShHjUcifgNtBurq RFevlWmJsPGTQMVXAyE", NotchPositions = [1], RingSetting = 49, Position = 48 },
            MiddleRotor = new RotorConfig { Encoding = "KQtyUXavbRfPrLBAZwHTElFJxqSeCkmDuiIsMon GcNO.jhVzWgYdp", NotchPositions = [44], RingSetting = 2, Position = 0 },
            RightRotor = new RotorConfig { Encoding = "KixJIRLdVcCuBnErWQmhbpkAPjqXNzyYfvgsMalD.UF TwoeOZGHtS", NotchPositions = [27], RingSetting = 18, Position = 14 },
            Reflector = "bacHIXYvLoBTtRjsqMpmUhSQGEDkNAzPydeW.irCVFxnwluOJfg ZK"
        }) { BlockSize = null };

        var output = enigma.Encipher(input);

        Console.WriteLine(output);
        Console.ReadLine();
    }
}
