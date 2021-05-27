namespace Enigma.Core
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Plugboard
    {
        public int[] Wiring { get; private set; }

        private Plugboard(string connections)
        {
            Wiring = DecodePlugboard(connections);
        }

        public static Plugboard Create(string connections)
        {
            return new Plugboard(connections.ToUpper());
        }

        public int Resolve(int c)
        {
            return Wiring[c];
        }

        private static int[] DefaultPlugboard()
        {
            var mapping = new int[26];
            for (var i = 0; i < 26; i++)
            {
                mapping[i] = i;
            }

            return mapping;
        }

        private static int[] DecodePlugboard(string plugboard)
        {
            if (string.IsNullOrEmpty(plugboard))
            {
                return DefaultPlugboard();
            }

            var pairings = Regex.Split(plugboard, "[^A-Z]");
            var pluggedCharacters = new HashSet<int>();
            var mapping = DefaultPlugboard();

            // Validate and create mapping
            foreach (var pair in pairings)
            {
                if (pair.Length != 2)
                {
                    return DefaultPlugboard();
                }

                var c1 = pair[0] - 65;
                var c2 = pair[1] - 65;
                if (pluggedCharacters.Contains(c1) || pluggedCharacters.Contains(c2))
                {
                    return DefaultPlugboard();
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
