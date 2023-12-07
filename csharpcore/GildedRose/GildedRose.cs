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

            item.SellIn += config.SellInOffset;

            int qualityOffset = GetQualityOffset(item, config);
            item.Quality += qualityOffset;            

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
        if (item.Name == Constants.BackstagePasses)
        {
            if (item.SellIn < 0)
            {
                return -1 * item.Quality;
            }

            int offset = config.DefaultOffset;

            if (item.SellIn < 10)
            {
                offset += config.DefaultOffset;             
            }

            if (item.SellIn < 5)
            {
                offset += config.DefaultOffset;      
            }

            return offset;
        }

        return item.SellIn < 0 
            ? 2 * config.DefaultOffset 
            : config.DefaultOffset;
    }
}