namespace GildedRoseKata.Config;

public class ItemConfigQualityOffsetThresholds
{
    public ItemConfigQualityOffsetThresholds(int? lowerBound, int? upperBound, int offset) 
    { 
        LowerBound = lowerBound;
        Offset = offset;
        UpperBound = upperBound;
    }

    public int? LowerBound { get; }

    public int Offset { get; }

    public int? UpperBound { get; }
}
