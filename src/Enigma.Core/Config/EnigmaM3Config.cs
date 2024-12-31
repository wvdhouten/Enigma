namespace Enigma.Core.Config;

public class EnigmaM3Config
{
    public NamedRotorConfig LeftRotor { get; set; } = new NamedRotorConfig();

    public NamedRotorConfig MiddleRotor { get; set; } = new NamedRotorConfig();

    public NamedRotorConfig RightRotor { get; set; } = new NamedRotorConfig();

    public string Reflector { get; set; } = string.Empty;

    public string Plugboard { get; set; } = string.Empty;
}
