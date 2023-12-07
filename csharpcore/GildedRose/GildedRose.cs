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

            ApplyPreSellinQualityOffset(item);
            int sellinOffset = GetSellinOffset(item);
            item.SellIn += sellinOffset;

            int postSellinOffset = GetPostSellinQualityOffset(item);
            item.Quality += postSellinOffset;
        }
    }

    private void ApplyPreSellinQualityOffset(Item item)
    {
        if (item.Name == Constants.AgedBrie || item.Name == Constants.BackstagePasses)
        {
            if (item.Quality >= 50)
            {
                return;
            }

            item.Quality = item.Quality + 1;

            if (item.Name != Constants.BackstagePasses)
            {
                return;
            }

            if (item.SellIn < 11 && item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }

            if (item.SellIn < 6 && item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }

            return;
        }

        if (item.Quality <= 0)
        {
            return;
        }

        if (item.Name == Constants.Sulfuras)
        {
            return;
        }

        item.Quality = item.Quality - 1;
    }

    private int GetSellinOffset(Item item)
    {
        return item.Name == Constants.Sulfuras ? 0 : -1;        
    }
        
    private int GetPostSellinQualityOffset(Item item)
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
            return item.Quality < 50 ? 1 : 0;
        }

        if (item.Name == Constants.BackstagePasses)
        {
            return -1 * item.Quality;
        }

        return item.Quality > 0 ? -1 : 0;
    }
}