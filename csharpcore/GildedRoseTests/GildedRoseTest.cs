using System.Collections.Generic;
using GildedRoseKata;
using GildedRoseKata.Config;
using NUnit.Framework;

namespace GildedRoseTests;

// Requirements: https://github.com/emilybache/GildedRose-Refactoring-Kata/blob/main/GildedRoseRequirements.txt
/* 
 * TODO: 
 * - 1. Refactor code to satisfy passing tests
 * - 2. Fix ignored Conjured tests
 * - 3. Change tests to meet stricter requirements:
 *      - Ensure quality can never exceed 50 - even if starting above 50
 *      - Ensure Sulfuras quality is always 80
 * - 4. Fix failing tests
*/
public static class GildedRoseTest
{
    [Test]
    public static void Foo()
    {
        var items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        var app = CreateGildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual("foo", items[0].Name);
    }

    /** GENERAL ITEM TESTS **/
    [TestCase(5, 10, ExpectedResult = 9)]
    [TestCase(4, 10, ExpectedResult = 9)]
    [TestCase(3, 10, ExpectedResult = 9)]
    [TestCase(2, 10, ExpectedResult = 9)]
    [TestCase(1, 10, ExpectedResult = 9)]
    public static int UpdateQuality_BeforeSellByDate_QualityDegradesByOnePerDay(int sellIn, int quality)
    {
        var item = new Item { Name = "foo", SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(0, 10, ExpectedResult = 8)]
    [TestCase(-1, 10, ExpectedResult = 8)]
    [TestCase(-2, 10, ExpectedResult = 8)]
    [TestCase(-3, 10, ExpectedResult = 8)]
    [TestCase(-4, 10, ExpectedResult = 8)]
    public static int UpdateQuality_OnOrAfterSellByDate_QualityDegradesByTwoPerDay(int sellIn, int quality)
    {
        var item = new Item { Name = "foo", SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(1, ExpectedResult = 0)]
    [TestCase(0, ExpectedResult = 0)]
    [TestCase(-1, ExpectedResult = 0)]    
    public static int UpdateQuality_QualityZero_QualityNeverNegative(int sellIn)
    {
        var item = new Item { Name = "foo", SellIn = sellIn, Quality = 0 };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(1, 100, ExpectedResult = 49)]
    [TestCase(0, 100, ExpectedResult = 48)]
    [TestCase(-1, 100, ExpectedResult = 48)]
    public static int UpdateQuality_QualityStartsAboveFifty_QualityDecreases(int sellIn, int quality)
    {
        var item = new Item { Name = "foo", SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }


    /** AGED BRIE TESTS **/
    [TestCase(5, 10, ExpectedResult = 11)]
    [TestCase(4, 10, ExpectedResult = 11)]
    [TestCase(3, 10, ExpectedResult = 11)]
    [TestCase(2, 10, ExpectedResult = 11)]
    [TestCase(1, 10, ExpectedResult = 11)]
    public static int UpdateQuality_AgedBrie_BeforeSellByDate_IncreasesInQualityByOne(int sellIn, int quality)
    {
        var item = new Item { Name = Constants.AgedBrie, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(0, 10, ExpectedResult = 12)]
    [TestCase(-1, 10, ExpectedResult = 12)]
    [TestCase(-2, 10, ExpectedResult = 12)]
    [TestCase(-3, 10, ExpectedResult = 12)]
    [TestCase(-4, 10, ExpectedResult = 12)]
    public static int UpdateQuality_AgedBrie_AfterSellByDate_IncreasesInQualityByTwo(int sellIn, int quality)
    {
        var item = new Item { Name = Constants.AgedBrie, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(1, 50, ExpectedResult = 50)]
    [TestCase(0, 50, ExpectedResult = 50)]
    [TestCase(-1, 50, ExpectedResult = 50)]
    public static int UpdateQuality_AgedBrie_QualityNeverExceedsFifty(int sellIn, int quality)
    {
        var item = new Item { Name = Constants.AgedBrie, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(1, 100, ExpectedResult = 50)]
    [TestCase(0, 100, ExpectedResult = 50)]
    [TestCase(-1, 100, ExpectedResult = 50)]
    public static int UpdateQuality_AgedBrie_QualityStartsAboveFifty_QualityDoesNotExceedFifty(int sellIn, int quality)
    {
        var item = new Item { Name = Constants.AgedBrie, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }    
    
    /** SULFURAS TESTS **/
    [TestCase(5, 10, ExpectedResult = 80)]
    [TestCase(4, 10, ExpectedResult = 80)]
    [TestCase(3, 10, ExpectedResult = 80)]
    [TestCase(2, 10, ExpectedResult = 80)]
    [TestCase(1, 10, ExpectedResult = 80)]
    [TestCase(0, 10, ExpectedResult = 80)]
    [TestCase(-1, 10, ExpectedResult = 80)]
    [TestCase(-2, 10, ExpectedResult = 80)]
    [TestCase(-3, 10, ExpectedResult = 80)]
    [TestCase(-4, 10, ExpectedResult = 80)]
    public static int UpdateQuality_Sulfuras_QualityEqualsEighty(int sellIn, int quality)
    {
        var item = new Item { Name = Constants.Sulfuras, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    /** BACKSTAGE PASSES TESTS **/
    [TestCase(5, 10, ExpectedResult = 13)]
    [TestCase(4, 10, ExpectedResult = 13)]
    [TestCase(3, 10, ExpectedResult = 13)]
    [TestCase(2, 10, ExpectedResult = 13)]
    [TestCase(1, 10, ExpectedResult = 13)]
    public static int UpdateQuality_BackstagePasses_FiveDaysOrLessBeforeSellByDate_IncreasesInQualityByThree(
        int sellIn, int quality)
    {
        var item = new Item { Name = Constants.BackstagePasses, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(10, 10, ExpectedResult = 12)]
    [TestCase(9, 10, ExpectedResult = 12)]
    [TestCase(8, 10, ExpectedResult = 12)]
    [TestCase(7, 10, ExpectedResult = 12)]
    [TestCase(6, 10, ExpectedResult = 12)]
    public static int UpdateQuality_BackstagePasses_TenDaysOrLessBeforeSellByDate_IncreasesInQualityByTwo(
        int sellIn, int quality)
    {
        var item = new Item { Name = Constants.BackstagePasses, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(11, 10, ExpectedResult = 11)]
    [TestCase(12, 10, ExpectedResult = 11)]
    [TestCase(13, 10, ExpectedResult = 11)]
    [TestCase(14, 10, ExpectedResult = 11)]
    [TestCase(15, 10, ExpectedResult = 11)]
    public static int UpdateQuality_BackstagePasses_MoreThanTenDaysBeforeSellByDate_IncreasesInQualityByOne(
        int sellIn, int quality)
    {
        var item = new Item { Name = Constants.BackstagePasses, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(0, 10, ExpectedResult = 0)]
    [TestCase(-1, 10, ExpectedResult = 0)]
    [TestCase(-2, 10, ExpectedResult = 0)]
    [TestCase(-3, 10, ExpectedResult = 0)]
    [TestCase(-4, 10, ExpectedResult = 0)]
    public static int UpdateQuality_BackstagePasses_AfterSellByDate_DropsToZero(int sellIn, int quality)
    {
        var item = new Item { Name = Constants.BackstagePasses, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(3, 50, ExpectedResult = 50)]
    [TestCase(2, 50, ExpectedResult = 50)]
    [TestCase(1, 50, ExpectedResult = 50)]
    public static int UpdateQuality_BackstagePasses_QualityNeverExceedsFifty(int sellIn, int quality)
    {
        var item = new Item { Name = Constants.BackstagePasses, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(3, 100, ExpectedResult = 50)]
    [TestCase(2, 100, ExpectedResult = 50)]
    [TestCase(1, 100, ExpectedResult = 50)]
    public static int UpdateQuality_BackstagePasses_QualityStartsAboveFifty_QualityDoesNotExceedFifty(int sellIn, int quality)
    {        
        var item = new Item { Name = Constants.BackstagePasses, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    /** CONJURED TESTS **/
    [Ignore("Currently not implemented")]
    [TestCase(5, 10, ExpectedResult = 8)]
    [TestCase(4, 10, ExpectedResult = 8)]
    [TestCase(3, 10, ExpectedResult = 8)]
    [TestCase(2, 10, ExpectedResult = 8)]
    [TestCase(1, 10, ExpectedResult = 8)]
    public static int UpdateQuality_Conjured_BeforeSellByDate_QualityDegradesByTwoPerDay(int sellIn, int quality)
    {
        var item = new Item { Name = Constants.Conjured, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [Ignore("Currently not implemented")]
    [TestCase(0, 10, ExpectedResult = 6)]
    [TestCase(-1, 10, ExpectedResult = 6)]
    [TestCase(-2, 10, ExpectedResult = 6)]
    [TestCase(-3, 10, ExpectedResult = 6)]
    [TestCase(-4, 10, ExpectedResult = 6)]
    public static int UpdateQuality_Conjured_OnOrAfterSellByDate_QualityDegradesByFourPerDay(int sellIn, int quality)
    {
        var item = new Item { Name = Constants.Conjured, SellIn = sellIn, Quality = quality };
        var app = CreateGildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    /** HELPERS **/
    private static IItemConfigFactory CreateConfigFactory()
    {
        return new ItemConfigFactory();
    }

    private static GildedRose CreateGildedRose(IList<Item> items)
    {
        var configFactory = CreateConfigFactory();
        return new GildedRose(items, configFactory);
    }
}