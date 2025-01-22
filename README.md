<p id="top" align="center">
    <a href="https://github.com/wvdhouten/enigma/blob/main/LICENSE">
        <img alt="Github License" src="https://img.shields.io/github/license/wvdhouten/enigma?style=for-the-badge&color=skyblue" />
    </a>
    <a href="https://github.com/wvdhouten/enigma/stargazers">
        <img alt="Github License" src="https://img.shields.io/github/stars/wvdhouten/enigma?style=for-the-badge&color=gold" />
    </a>
    <a href="https://github.com/wvdhouten/enigma/issues">
        <img alt="GitHub issues" src="https://img.shields.io/github/issues/wvdhouten/enigma?style=for-the-badge&color=plum" />
    </a>
    <a href="https://github.com/wvdhouten/enigma/network">
        <img alt="GitHub forks" src="https://img.shields.io/github/forks/wvdhouten/enigma?style=for-the-badge&color=lightgreen">
    </a>
</p>

<p align="center">
    <!-- LOGO GOES HERE -->
</p>

<p align="center" style="font-size: 3em;">
    <b>Enigma</b>
</p>

## About

The Enigma machine is a cipher device developed and used in the early- to mid-20th century to protect commercial, diplomatic, and military communication. It was employed extensively by Nazi Germany during World War II, in all branches of the German military. The Enigma machine was considered so secure that it was used to encipher the most top-secret messages. ([Wikipedia](https://en.wikipedia.org/wiki/Enigma_machine))

The project offers 3 different ways to configure the Enigma Machine. The default implementation is the M3-Model with the 3 additional rotors. The second implementation allows full customization of character sets, reflectors, rotors and plugboards. The last implementation allows for generating a random configuration specifying only the character set. The randomizer can be seeded, and the generated configuration will be returned.

## Getting Started

<!-- TODO -->

The solution is implemented as a class library that can be integrated in other .NET Projects. (It currently does not have a NuGet package, so it needs to be compiled and added manually.)

### Default M3-Model configuration

- The available reflectors are `B` and `C`.
- The available rotors are the roman numerals from `I` to `VIII`.
- The plugboard can be any set of combinations between 2 unique letters.

```csharp
var input = "IFCOV OKDLI UDBKF QQFLS CPDPV RKZMP FWFBB ZOEEK VSFMG KUNE";

var config = new EnigmaM3Config
{
    Reflector = "B",
    LeftRotor = { Name = "II", Position = 17, RingSetting = 11 },
    MiddleRotor = { Name = "IV", Position = 1, RingSetting = 7 },
    RightRotor = { Name = "III", Position = 17, RingSetting = 8 },
    Plugboard = "SHXWEPAZKCUJRO"
};

var enigma = new EnigmaMachine(config);
var output = enigma.Encrypt(input);
```

### Custom configuration

In order to encipher or decipher a text using a custom configuration the initial values must be defined using the `EnigmaMachineConfig`. The `EnigmaMachineConfig` requires a character set defined as a string.

The sample code below will result in the input being deciphered to: `InordertoencipherordecipheratextusingacustomconfigurationtheinitialvaluesmustbedefinedusingtheEnigmaMachineConfig.`

```csharp
var input = " jJAWUMhnSqVqCQp KasxrmgCZHinILIDZgwZmfxHXqT";

var characterSet = new CharacterSet("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ .");
var enigma = new EnigmaMachine(new EnigmaMachineConfig
{
    CharacterSet = characterSet,
    Reflector = "bacHIXYvLoBTtRjsqMpmUhSQGEDkNAzPydeW.irCVFxnwluOJfg ZK",
    Plugboard = "KhgCkpODjRorEuxqGbVJyidQWH",
    LeftRotor = new RotorConfig { Encoding = "Y.OZdwpIakLobKnDzCxShHjUcifgNtBurq RFevlWmJsPGTQMVXAyE", NotchPositions = [1], RingSetting = 49, Position = 48 },
    MiddleRotor = new RotorConfig { Encoding = "KQtyUXavbRfPrLBAZwHTElFJxqSeCkmDuiIsMon GcNO.jhVzWgYdp", NotchPositions = [44], RingSetting = 2, Position = 0 },
    RightRotor = new RotorConfig { Encoding = "KixJIRLdVcCuBnErWQmhbpkAPjqXNzyYfvgsMalD.UF TwoeOZGHtS", NotchPositions = [27], RingSetting = 18, Position = 14 }
}) { BlockSize = null };

var output = enigma.Encipher(input);
```

### Random configuration

In order to encipher or decipher a text using a random configuration you initiate the Enigma Machine with a character set and (optionally) a seed for the randomizer.

The config that will be generated based on the character set and the randomizer will be returned in an `out` parameter.

```csharp
var input = "uo4uat8ek76ga9q43rq4mb6si49kioyvnx";

var characterSet = new CharacterSet("abcdefghijklmnopqrstuvwxyz0123456789");
var enigma = new EnigmaMachine(characterSet, out var config, 1337) { BlockSize = null };

var output = enigma.Encipher(input);
```

## Table of Contents

<ul>
    <li><a href="#about">About</a></li>
    <li><a href="#getting-started">Getting started</li>
    <li><a href="#samples">Demo</a></li>
    <li><a href="#roadmap">Project Roadmap</a></li>
    <li><a href="#documentation">Documentation</a></li>
    <li><a href="#contributors">Contributors</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
    <li><a href="#feedback">Feedback</a></li>
    <li><a href="#license">License</a></li>
</ul>

<p align="right"><a href="#top">back to top ⬆️</a></p>

## Roadmap

- [x] Update README.

<p align="right"><a href="#top">back to top ⬆️</a></p>

## Documentation

<!-- TODO -->

## Acknowledgments

- [Make a Readme](https://www.makeareadme.com/)
- [Shields](https://shields.io/)

<p align="right"><a href="#top">back to top ⬆️</a></p>

## Feedback

If you encounter any problems or have any suggestions, please feel free to open an [Issue](https://github.com/wvdhouten/enigma/issues). Please specify in the issue whether it is a bug or a feature request.

## Contributing

<!-- TODO -->

## Contributors

<a href="https://github.com/wvdhouten/enigma/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=wvdhouten/enigma" />
</a>

## License
- [MIT License](./LICENSE)

<p align="right"><a href="#top">back to top ⬆️</a></p>