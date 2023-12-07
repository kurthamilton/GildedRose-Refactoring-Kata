namespace GildedRoseKata;

public class ItemConfigFactory : IItemConfigFactory
{
    public ItemConfig Get(Item item)
    {
        return item.Name switch
        {
            Constants.AgedBrie =>        new ItemConfig(max: 50, min: 0,  defaultOffset: 1,  sellInOffset: -1),
            Constants.BackstagePasses => new ItemConfig(max: 50, min: 0,  defaultOffset: 1,  sellInOffset: -1),
            // ignore conjured for now
            // Constants.Conjured =>        new ItemConfig(max: 50, min: 0,  defaultOffset: -2),
            Constants.Sulfuras =>        new ItemConfig(max: 80, min: 80, defaultOffset: 0,  sellInOffset: 0),
            _ =>                         new ItemConfig(max: 50, min: 0,  defaultOffset: -1, sellInOffset: -1)
        };
    }
}
