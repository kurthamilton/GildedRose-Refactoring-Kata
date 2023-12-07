using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata.Config;

public class ItemConfig
{
    public ItemConfig(int max = 50, int min = 0, int defaultOffset = -1, int sellInOffset = -1, 
        IEnumerable<QualityThresholds> thresholds = null)
    {
        DefaultOffset = defaultOffset;
        Max = max;
        Min = min;
        SellInOffset = sellInOffset;
        Thresholds = thresholds?.ToArray() ?? Array.Empty<QualityThresholds>();
    }        

    /// <summary>
    /// The per-day offset for item expiry date
    /// </summary>
    public int SellInOffset { get; }

    /// <summary>
    /// The default per-day offset for item quality
    /// </summary>
    private int DefaultOffset { get; }

    /// <summary>
    /// The max value for item quality
    /// </summary>
    private int Max { get; }

    /// <summary>
    /// The min value for item quality
    /// </summary>
    private int Min { get; }

    /// <summary>
    /// A collection of thresholds for variable quality offsets
    /// </summary>
    private IReadOnlyCollection<QualityThresholds> Thresholds { get; }

    public int GetQuality(Item item)
    {
        var quality = GetBoundedValue(item.Quality);

        var threshold = ThresholdFor(item.SellIn);
        if (threshold == null)
        {
            return GetBoundedValue(quality + DefaultOffset);
        }

        return GetBoundedValue(threshold.AbsoluteAmount != null
            ? threshold.AbsoluteAmount.Value
            : quality + threshold.Offset);
    }

    private int GetBoundedValue(int value)
    {
        return value > Max
            ? Max
            : value < Min
            ? Min
            : value;
    }

    private QualityThresholds ThresholdFor(int sellIn)
    {
        return Thresholds
            .FirstOrDefault(x => x.IsFor(sellIn));
    }
}
