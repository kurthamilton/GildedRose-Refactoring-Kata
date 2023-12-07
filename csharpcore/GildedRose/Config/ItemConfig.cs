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
    /// The max value for item quality
    /// </summary>
    public int Max { get; }

    /// <summary>
    /// The min value for item quality
    /// </summary>
    public int Min { get; }

    /// <summary>
    /// The per-day offset for item expiry date
    /// </summary>
    public int SellInOffset { get; }

    /// <summary>
    /// The default per-day offset for item quality
    /// </summary>
    private int DefaultOffset { get; }

    /// <summary>
    /// A collection of thresholds for variable quality offsets
    /// </summary>
    private IReadOnlyCollection<QualityThresholds> Thresholds { get; }

    public int GetQuality(Item item)
    {
        var threshold = ThresholdFor(item.SellIn);
        if (threshold == null)
        {
            return item.Quality + DefaultOffset;
        }

        return threshold.AbsoluteAmount != null
            ? threshold.AbsoluteAmount.Value
            : item.Quality + threshold.Offset;
    }

    private QualityThresholds ThresholdFor(int sellIn)
    {
        return Thresholds
            .FirstOrDefault(x => x.IsFor(sellIn));
    }
}
