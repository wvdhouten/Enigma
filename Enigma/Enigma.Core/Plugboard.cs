namespace Enigma.Core
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Plugboard
    {

        private readonly int[] _wiring;

        public Plugboard(string connections)
        {
            _wiring = DecodePlugboard(connections);
        }

        public int Forward(int c)
        {
            return _wiring[c];
        }

        private static int[] IdentityPlugboard()
        {
            var mapping = new int[26];
            for (var i = 0; i < 26; i++)
            {
                mapping[i] = i;
            }

            return mapping;
        }

        public static HashSet<int> GetUnpluggedCharacters(string plugboard)
        {
            var unpluggedCharacters = new HashSet<int>();
            for (var i = 0; i < 26; i++)
            {
                unpluggedCharacters.Add(i);
            }

            if (plugboard.Equals(""))
            {
                return unpluggedCharacters;
            }

            var pairings = Regex.Split(plugboard, "[^a-zA-Z]");

            // Validate and create mapping
            foreach (var pair in pairings)
            {
                var c1 = pair[0] - 65;
                var c2 = pair[1] - 65;
                unpluggedCharacters.Remove(c1);
                unpluggedCharacters.Remove(c2);
            }

            return unpluggedCharacters;
        }

        public static int[] DecodePlugboard(string plugboard)
        {
            if (plugboard == null || plugboard.Equals(""))
            {
                return IdentityPlugboard();
            }

            var pairings = Regex.Split(plugboard, "[^a-zA-Z]");
            var pluggedCharacters = new HashSet<int>();
            var mapping = IdentityPlugboard();

            // Validate and create mapping
            foreach (var pair in pairings)
            {
                if (pair.Length != 2)
                {
                    return IdentityPlugboard();
                }

                var c1 = pair[0] - 65;
                var c2 = pair[1] - 65;
                if (pluggedCharacters.Contains(c1) || pluggedCharacters.Contains(c2))
                {
                    return IdentityPlugboard();
                }

                pluggedCharacters.Add(c1);
                pluggedCharacters.Add(c2);
                mapping[c1] = c2;
                mapping[c2] = c1;
            }

            return mapping;
        }
    }
}
