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

            bool sideEffectFeatureToggle = true;
            int preSellInOffset = ApplyPreSellInQualityOffset(item, sideEffectFeatureToggle);
            if (!sideEffectFeatureToggle)
            {
                item.Quality += preSellInOffset;
            }

            int sellInOffset = GetSellInOffset(item);
            item.SellIn += sellInOffset;

            int postSellInOffset = GetPostSellInQualityOffset(item);
            item.Quality += postSellInOffset;
        }
    }

    private int ApplyPreSellInQualityOffset(Item item, bool sideAffectFeatureToggle)
    {
        if (item.Name == Constants.AgedBrie || item.Name == Constants.BackstagePasses)
        {
            if (item.Quality >= 50)
            {
                return 0;
            }

            int offset = 1;
            int quality = item.Quality + offset;

            if (sideAffectFeatureToggle)
            {
                item.Quality = quality;
            }            

            if (item.Name != Constants.BackstagePasses)
            {
                return offset;
            }

            if (item.SellIn < 11 && quality < 50)
            {
                quality += 1;
                offset += 1;

                if (sideAffectFeatureToggle)
                {
                    item.Quality = quality;
                }                
            }

            if (item.SellIn < 6 && quality < 50)
            {
                offset += 1;
                quality += 1;

                if (sideAffectFeatureToggle)
                {
                    item.Quality = quality;
                }                
            }

            return offset;
        }

        if (item.Quality <= 0)
        {
            return 0;
        }

        if (item.Name == Constants.Sulfuras)
        {
            return 0;
        }

        if (sideAffectFeatureToggle)
        {
            item.Quality = item.Quality - 1;
        }

        return -1;
    }

    private int GetSellInOffset(Item item)
    {
        return item.Name == Constants.Sulfuras ? 0 : -1;        
    }
        
    private int GetPostSellInQualityOffset(Item item)
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