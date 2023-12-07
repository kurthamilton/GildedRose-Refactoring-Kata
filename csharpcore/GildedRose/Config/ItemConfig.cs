using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata.Config;

public class ItemConfig
{
    public ItemConfig(int max, int min, int defaultOffset, int sellInOffset, 
        IEnumerable<QualityThresholds> thresholds)
    {
        DefaultOffset = defaultOffset;
        Max = max;
        Min = min;
        SellInOffset = sellInOffset;
        Thresholds = thresholds?.ToArray() ?? Array.Empty<QualityThresholds>();
    }

    public int DefaultOffset { get; }

    public int Max { get; }

    public int Min { get; }

    public int SellInOffset { get; }

    private IReadOnlyCollection<QualityThresholds> Thresholds { get; }

    public QualityThresholds ThresholdFor(int sellIn)
    {
        return Thresholds
            .FirstOrDefault(x => x.IsFor(sellIn));
    }
}
