namespace GildedRoseKata;

public class ItemConfig
{
    public ItemConfig(int max, int min, int defaultOffset, int sellInOffset)
    {
        DefaultOffset = defaultOffset;
        Max = max;
        Min = min;
        SellInOffset = sellInOffset;
    }

    public int DefaultOffset { get; }

    public int Max { get; }

    public int Min { get; }

    public int SellInOffset { get; }
}
