namespace Enigma.Core
{
    public class EnigmaKey
    {
        public string[] Rotors { get; set; }
        public int[] Positions { get; set; }
        public int[] Rings { get; set; }
        public string Reflector { get; set; }
        public string Plugboard { get; set; }

        public EnigmaKey(string[] rotors, int[] positions, int[] rings, string reflector, string plugboard)
        {
            Rotors = rotors ?? new string[] { "I", "II", "III" };
            Positions = positions ?? new int[] { 0, 0, 0 };
            Rings = rings ?? new int[] { 0, 0, 0 };
            Reflector = reflector;
            Plugboard = plugboard ?? "";
        }

        public EnigmaKey(EnigmaKey key) : this(key.Rotors, key.Positions, key.Rings, key.Reflector, key.Plugboard)
        {
        }
    }
}