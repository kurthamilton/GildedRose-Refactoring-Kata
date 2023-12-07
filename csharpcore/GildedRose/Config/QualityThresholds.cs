namespace GildedRoseKata.Config;

public class QualityThresholds
{
    public QualityThresholds(int? lower, int? upper, int offset = 0, int? absolute = null) 
    { 
        AbsoluteAmount = absolute;
        LowerBound = lower;
        Offset = offset;
        UpperBound = upper;
    }    

    public int? AbsoluteAmount { get; set; }

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
