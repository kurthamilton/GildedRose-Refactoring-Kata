namespace GildedRoseKata.Config;

public class QualityThresholds
{
    public QualityThresholds(int? lowerBound, int? upperBound, int offset) 
    { 
        LowerBound = lowerBound;
        Offset = offset;
        UpperBound = upperBound;
    }    

    public int Offset { get; }

    private int? LowerBound { get; }

    private int? UpperBound { get; }

    public bool IsFor(int value)
    {
        return 
            (LowerBound == null || value >= LowerBound) &&
            (UpperBound == null || value <= UpperBound);
    }
}
