namespace Enigma.Core.Config
{
    public class EnigmaM3Config
    {
        public RotorConfig LeftRotor { get; set; } = new RotorConfig();

        public RotorConfig MiddleRotor { get; set; } = new RotorConfig();

        public RotorConfig RightRotor { get; set; } = new RotorConfig();

        public string Reflector { get; set; }

        public string Plugboard { get; set; }
    }
}
