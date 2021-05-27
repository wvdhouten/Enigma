namespace Enigma.Core
{
    using System.Text;
    using Enigma.Core.Config;

    public class EnigmaM3
    {
        public Rotor LeftRotor { get; private set; }

        public Rotor MiddleRotor { get; private set; }

        public Rotor RightRotor { get; private set; }

        public Reflector Reflector { get; private set; }

        public Plugboard Plugboard { get; private set; }

        public EnigmaM3(string[] rotors, int[] rotorPositions, int[] ringSettings, string reflector, string plugboardConnections)
        {
            LeftRotor = Rotor.Create(rotors[0], rotorPositions[0], ringSettings[0]);
            MiddleRotor = Rotor.Create(rotors[1], rotorPositions[1], ringSettings[1]);
            RightRotor = Rotor.Create(rotors[2], rotorPositions[2], ringSettings[2]);
            Reflector = Reflector.Create(reflector);
            Plugboard = Plugboard.Create(plugboardConnections);
        }

        public EnigmaM3(EnigmaM3Config config)
        {
            LeftRotor = Rotor.Create(config.LeftRotor);
            MiddleRotor = Rotor.Create(config.MiddleRotor);
            RightRotor = Rotor.Create(config.RightRotor);
            Reflector = Reflector.Create(config.Reflector);
            Plugboard = Plugboard.Create(config.Plugboard);
        }

        public void Rotate()
        {
            if (MiddleRotor.IsAtNotch())
            {
                // Middle rotor double-steps.
                MiddleRotor.Rotate();
                LeftRotor.Rotate();
            }

            if (RightRotor.IsAtNotch())
            {
                MiddleRotor.Rotate();
            }
            RightRotor.Rotate();
        }

        public int Encrypt(int c)
        {
            Rotate();

            c = Plugboard.Resolve(c);

            c = RightRotor.Forward(c);
            c = MiddleRotor.Forward(c);
            c = LeftRotor.Forward(c);

            c = Reflector.Resolve(c);

            c = LeftRotor.Backward(c);
            c = MiddleRotor.Backward(c);
            c = RightRotor.Backward(c);

            c = Plugboard.Resolve(c);

            return c;
        }

        public string Encrypt(string input)
        {
            input = input.ToUpper();

            var output = new StringBuilder();
            foreach (var c in input)
            {
                var i = c - 65;
                if (i < 0 || i > 25)
                {
                    continue;
                }

                if (output.Length % 6 == 5)
                {
                    output.Append(' ');
                }

                output.Append((char)(Encrypt(i) + 65));
            }
            return output.ToString();
        }
    }
}
