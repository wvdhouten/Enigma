namespace Enigma.Core.Config;

public class RotorConfig
{
    public string Encoding { get; set; } = string.Empty;

    public int Position { get; set; }

    public int RingSetting { get; set; }

    public int[] NotchPositions { get; set; } = [];
}
