using System.Runtime.CompilerServices;

namespace Enigma.Core.Extensions;
internal static class ArrayExtensions
{
    private static readonly Random random = new();

    public static string Shuffle(this string source) => new(source.ToCharArray().Shuffle());

    public static TEntity[] Shuffle<TEntity>(this TEntity[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int j = random.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }

        return array;
    }

    public static string TakeRandom(this string source, out char value)
    {
        int randomIndex = random.Next(0, source.Length);
        value = source[randomIndex];
        return source.Remove(randomIndex, 1);
    }

    public static bool TryIndexOf(this string source, char value, out int index)
    {
        index = source.IndexOf(value);
        return index >= 0;
    }

    public static int IndexOrCrash(this string source, char value)
    {
        if (source.TryIndexOf(value, out var index))
            return index;

        throw new ArgumentOutOfRangeException(nameof(value), $"Character '{value}' was not found in the source string.");
    }
}
