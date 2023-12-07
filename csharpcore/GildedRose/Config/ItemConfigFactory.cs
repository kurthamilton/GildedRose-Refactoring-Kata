namespace GildedRoseKata.Config;

public class ItemConfigFactory : IItemConfigFactory
{
    public ItemConfig Get(Item item)
    {
        return item.Name switch
        {
            Constants.AgedBrie =>new ItemConfig(max: 50, min: 0, defaultOffset: 1, sellInOffset: -1, 
                thresholds: new[] 
                { 
                    new QualityThresholds(lower: null, upper: 0, offset : 2) 
                }),
            Constants.BackstagePasses => new ItemConfig(max: 50, min: 0, defaultOffset: 1, sellInOffset: -1, 
                thresholds: new[] 
                { 
                    new QualityThresholds(lower: null, upper: 0, absolute: 0),
                    new QualityThresholds(lower: 1, upper: 5, offset: 3),
                    new QualityThresholds(lower: 6, upper: 10, offset: 2)
                }),
            Constants.Conjured => new ItemConfig(max: 50, min: 0, defaultOffset: -2, sellInOffset: -1, thresholds: new[]
            {
                new QualityThresholds(lower: null, upper: 0, offset: -4)
            }),
            Constants.Sulfuras => new ItemConfig(max: 80, min: 80, defaultOffset: 0,  sellInOffset: 0, thresholds: null),
            _ => new ItemConfig(max: 50, min: 0, defaultOffset: -1, sellInOffset: -1, thresholds: new[]
            {
                new QualityThresholds(lower: null, upper : 0, offset: -2)
            })
        };
    }
}
