namespace Enigma.Core
{
    using System;
    using System.Text;

    public class Enigma
    {
        public Rotor LeftRotor { get; set; }

        public Rotor MiddleRotor { get; set; }

        public Rotor RightRotor { get; set; }

        public Reflector Reflector { get; set; }

        public Plugboard Plugboard { get; set; }

        public Enigma(string[] rotors, int[] rotorPositions, int[] ringSettings, string reflector, string plugboardConnections)
        {
            LeftRotor = Rotor.Create(rotors[0], rotorPositions[0], ringSettings[0]);
            MiddleRotor = Rotor.Create(rotors[1], rotorPositions[1], ringSettings[1]);
            RightRotor = Rotor.Create(rotors[2], rotorPositions[2], ringSettings[2]);
            Reflector = Reflector.Create(reflector);
            Plugboard = new Plugboard(plugboardConnections);
        }

        public Enigma(EnigmaKey key) : this(key.Rotors, key.Positions, key.Rings, "B", key.Plugboard)
        {
        }

        public void Rotate()
        {
            //  If middle rotor notch - double-stepping
            if (MiddleRotor.IsAtNotch())
            {
                MiddleRotor.TurnOver();
                LeftRotor.TurnOver();
            }

            //  If left-rotor notch
            if (RightRotor.IsAtNotch())
            {
                MiddleRotor.TurnOver();
            }

            //  Increment right-most rotor
            RightRotor.TurnOver();
        }

        public int Encrypt(int c)
        {
            Rotate();

            //  Plugboard in
            c = Plugboard.Forward(c);

            //  Right to left
            c = RightRotor.Forward(c);
            c = MiddleRotor.Forward(c);
            c = LeftRotor.Forward(c);

            //  Reflector
            c = Reflector.Forward(c);

            //  Left to right
            c = LeftRotor.Backward(c);
            c = MiddleRotor.Backward(c);
            c = RightRotor.Backward(c);

            //  Plugboard out
            c = Plugboard.Forward(c);

            return c;
        }

        public string Encrypt(string input)
        {
            input = input.ToUpper();

            Console.WriteLine(input);

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
