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

            int preSellInOffset = ApplyPreSellInQualityOffset(item, config);
            item.Quality += preSellInOffset;

            int sellInOffset = GetSellInOffset(item);
            item.SellIn += sellInOffset;

            int postSellInOffset = GetPostSellInQualityOffset(item, config);
            item.Quality += postSellInOffset;
        }
    }

    private int ApplyPreSellInQualityOffset(Item item, ItemConfig config)
    {        
        if (item.Name == Constants.AgedBrie || item.Name == Constants.BackstagePasses)
        {
            if (item.Quality >= config.Max)
            {
                return 0;
            }

            int offset = config.DefaultOffset;
            
            if (item.Name != Constants.BackstagePasses)
            {
                return offset;
            }

            if (item.SellIn < 11 && item.Quality + offset < config.Max)
            {
                offset += config.DefaultOffset;             
            }

            if (item.SellIn < 6 && item.Quality + offset < config.Max)
            {
                offset += config.DefaultOffset;      
            }

            return offset;
        }

        if (item.Quality <= config.Min)
        {
            return 0;
        }

        if (item.Name == Constants.Sulfuras)
        {
            return 0;
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

        if (item.Name == Constants.Sulfuras)
        {
            return 0;
        }

        if (item.Name == Constants.AgedBrie)
        {
            return item.Quality < config.Max ? config.DefaultOffset : 0;
        }

        if (item.Name == Constants.BackstagePasses)
        {
            return -1 * item.Quality;
        }

        return item.Quality > 0 ? config.DefaultOffset : 0;
    }
}