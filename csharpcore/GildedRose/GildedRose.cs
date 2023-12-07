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
            ApplySellinOffset(item);
            int offset = GetPostSellinQualityOffset(item);
            item.Quality += offset;
        }
    }

    private void ApplyPreSellinQualityOffset(Item item)
    {
        if (item.Name != Constants.AgedBrie && item.Name != Constants.BackstagePasses)
        {
            if (item.Quality > 0)
            {
                if (item.Name != Constants.Sulfuras)
                {
                    item.Quality = item.Quality - 1;
                }
            }
        }
        else
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;

                if (item.Name == Constants.BackstagePasses)
                {
                    if (item.SellIn < 11)
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }

                    if (item.SellIn < 6)
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }
                }
            }
        }
    }

    private void ApplySellinOffset(Item item)
    {
        if (item.Name != Constants.Sulfuras)
        {
            item.SellIn = item.SellIn - 1;
        }
    }
        
    private int GetPostSellinQualityOffset(Item item)
    {
        if (item.SellIn < 0)
        {
            if (item.Name != Constants.AgedBrie)
            {
                if (item.Name != Constants.BackstagePasses)
                {
                    if (item.Quality > 0)
                    {
                        if (item.Name != Constants.Sulfuras)
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    return -1 * item.Quality;
                }
            }
            else
            {
                if (item.Quality < 50)
                {
                    return 1;
                }
            }
        }

        return 0;
    }
}