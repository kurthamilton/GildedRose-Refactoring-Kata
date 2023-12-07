using System.Collections.Generic;

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

            int preSellInOffset = ApplyPreSellInQualityOffset(item, config);
            item.Quality += preSellInOffset;

            int sellInOffset = GetSellInOffset(item);
            item.SellIn += sellInOffset;

            int postSellInOffset = GetPostSellInQualityOffset(item, config);
            item.Quality += postSellInOffset;

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

    private int ApplyPreSellInQualityOffset(Item item, ItemConfig config)
    {        
        if (item.Name == Constants.BackstagePasses)
        {
            int offset = config.DefaultOffset;

            if (item.SellIn < 11)
            {
                offset += config.DefaultOffset;             
            }

            if (item.SellIn < 6)
            {
                offset += config.DefaultOffset;      
            }

            return offset;
        }

        return config.DefaultOffset;
    }

    private int GetSellInOffset(Item item)
    {
        return item.Name == Constants.Sulfuras ? 0 : -1;        
    }
        
    private int GetPostSellInQualityOffset(Item item, ItemConfig config)
    {
        if (item.SellIn >= 0)
        {
            return 0;
        }

        if (item.Name == Constants.BackstagePasses)
        {
            return -1 * item.Quality;
        }

        return config.DefaultOffset;
    }
}