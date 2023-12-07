namespace GildedRoseKata.Config;

public interface IItemConfigFactory
{
    ItemConfig Get(Item item);
}
