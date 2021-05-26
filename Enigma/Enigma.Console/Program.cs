namespace Enigma.Console
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            //FitnessFunction ioc = new IoCFitness();
            //FitnessFunction bigrams = new BigramFitness();
            //FitnessFunction quadgrams = new QuadramFitness();

            //var startTime = DateTime.Now;

            var ciphertext = "oivaa vrrvk khrut cjojv edtwu gdnxl vvemf a";
            var enigma = new Core.Enigma(new[] { "II", "V", "III" }, new[] { 7, 4, 19 }, new[] { 12, 2, 20 }, "B", "AF TV KO BL RW");
            var decipheredText = enigma.Encrypt(ciphertext);

            Console.WriteLine(decipheredText);
            Console.ReadLine();

            //// Begin by finding the best combination of rotors and start positions (returns top n)
            //var rotorConfigurations = EnigmaAnalysis.FindRotorConfiguration(ciphertext, EnigmaAnalysis.AvailableRotors.FIVE, "B", "", 10, ioc);

            //Console.WriteLine("Top 10 rotor configurations:");
            //foreach (var key in rotorConfigurations)
            //{
            //    Console.WriteLine($"{key.Rotors[0]} {key.Rotors[1]} {key.Rotors[2]} / {key.Positions[0]} {key.Positions[1]} {key.Positions[2]} / {key.Score}");
            //}
            //Console.WriteLine($"Current decryption: {new string(new Core.Enigma(rotorConfigurations[0]).Encrypt(ciphertext))}");

            //// Next find the best ring settings for the best configuration (index 0)
            //var rotorAndRingConfiguration = EnigmaAnalysis.FindRingSettings(rotorConfigurations[0], ciphertext, ioc); //bigrams

            //Console.WriteLine($"Best ring settings: {rotorAndRingConfiguration.Rings[0]} {rotorAndRingConfiguration.Rings[1]} {rotorAndRingConfiguration.Rings[2]}");
            //Console.WriteLine($"Current decryption: {new string(new Core.Enigma(rotorAndRingConfiguration).Encrypt(ciphertext))}");

            //// Finally, perform hill climbing to find plugs one at a time
            //var optimalKeyWithPlugs = EnigmaAnalysis.FindPlugs(rotorAndRingConfiguration, 5, ciphertext, ioc); //quadgrams
            //Console.WriteLine($"Best plugboard: {optimalKeyWithPlugs.Plugboard}");
            //Console.WriteLine($"Final decryption: {new string(new Core.Enigma(optimalKeyWithPlugs).Encrypt(ciphertext))}");

            //var endTime = DateTime.Now;

            //Console.WriteLine($"Total execution time: {(endTime-startTime).TotalSeconds}s");
            //Console.ReadKey();
        }
    }
}
