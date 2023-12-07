namespace GildedRoseKata.Config;

public class QualityThresholds
{
    public QualityThresholds(int? lower = null, int? upper = null, int offset = 0, int? absolute = null) 
    { 
        AbsoluteAmount = absolute;
        LowerBound = lower;
        Offset = offset;
        UpperBound = upper;
    }    

    /// <summary>
    /// The absolute amount to set item quality for this threshold
    /// </summary>
    public int? AbsoluteAmount { get; set; }

    /// <summary>
    /// The offset to apply to item quality for this threshold
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// The lower bound of this threshold, null means unbounded
    /// </summary>
    private int? LowerBound { get; }

    /// <summary>
    /// The upper bound of this threshold, null means unbounded
    /// </summary>
    private int? UpperBound { get; }

    /// <summary>
    /// Whether or not the given value falls within the threshold range
    /// </summary>
    public bool IsFor(int value)
    {
        return 
            (LowerBound == null || value >= LowerBound) &&
            (UpperBound == null || value <= UpperBound);
    }
}
