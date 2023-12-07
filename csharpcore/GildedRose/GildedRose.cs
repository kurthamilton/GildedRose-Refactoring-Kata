using System.Collections.Generic;
using GildedRoseKata.Config;

namespace GildedRoseKata;

public class GildedRose
{
    // DO NOT TOUCH!!!
    private readonly IList<Item> _items;
    private readonly IItemConfigFactory _itemConfigFactory;

    public GildedRose(IList<Item> items, IItemConfigFactory itemConfigFactory)
    {
        _itemConfigFactory = itemConfigFactory;
        _items = items;
    }

    public void UpdateQuality()
    {
        foreach (var item in _items)
        {
            var config = _itemConfigFactory.Get(item);

            EnsureWithinBounds(item, config);

            var qualityOffset = GetQualityOffset(item, config);
            item.Quality += qualityOffset;

            item.SellIn += config.SellInOffset;            

            EnsureWithinBounds(item, config);
        }
    }

    private void EnsureWithinBounds(Item item, ItemConfig config)
    {
        if (item.Quality < config.Min)
        {
            item.Quality = config.Min;
        }

        if (item.Quality > config.Max)
        {
            item.Quality = config.Max;
        }
    }

    private int GetQualityOffset(Item item, ItemConfig config)
    {
        var threshold = config.ThresholdFor(item.SellIn);
        if (threshold == null)
        {
            return config.DefaultOffset;
        }

        return threshold.AbsoluteAmount != null
            ? threshold.AbsoluteAmount.Value - item.Quality
            : threshold.Offset;
    }
}