namespace GildedRoseKata;

public class ItemConfig
{
    public ItemConfig(int max, int min, int defaultOffset)
    {
        DefaultOffset = defaultOffset;
        Max = max;
        Min = min;
    }

    public int DefaultOffset { get; }

    public int Max { get; }

    public int Min { get; }
}
