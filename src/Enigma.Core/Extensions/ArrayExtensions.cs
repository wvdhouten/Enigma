namespace Enigma.Core.Extensions;
internal static class ArrayExtensions
{
    public static string Shuffle(this string source, Random? random = null)
    {
        return new(source.ToCharArray().Shuffle(random));
    }

    public static TEntity[] Shuffle<TEntity>(this TEntity[] array, Random? random = null)
    {
        random ??= new();

        for (int i = 0; i < array.Length; i++)
        {
            int j = random.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }

        return array;
    }

    public static string SwapPairs(this string source, Random? random = null)
    {
        random ??= new();

        var array = new char[source.Length];
        var remaining = source.ToList();
        while (remaining.Count > 1)
        {
            var a = remaining[random.Next(0, remaining.Count)];
            var b = remaining[random.Next(0, remaining.Count)];
            array[source.IndexOf(a)] = b;
            array[source.IndexOf(b)] = a;
            remaining.Remove(a);
            remaining.Remove(b);
        }
        if (remaining.Count == 1)
        {
            array[source.IndexOf(remaining[0])] = remaining[0];
        }

        return new(array);
    }

    public static string TakeRandom(this string source, out char value, Random? random = null)
    {
        random ??= new();

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
