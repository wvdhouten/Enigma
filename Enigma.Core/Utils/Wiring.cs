namespace Enigma.Core.Utils
{
    using System.Linq;

    internal class Wiring
    {
        internal static int[] Decode(string encoding)
        {
            return encoding.Select(c => c - 65).ToArray();
        }

        internal static int[] Inverse(int[] wiring)
        {
            var inverse = new int[wiring.Length];
            for (var i = 0; i < wiring.Length; i++)
            {
                var forward = wiring[i];
                inverse[forward] = i;
            }
            return inverse;
        }
    }
}
