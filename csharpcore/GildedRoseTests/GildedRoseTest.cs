using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

// Requirements: https://github.com/emilybache/GildedRose-Refactoring-Kata/blob/main/GildedRoseRequirements.txt
public static class GildedRoseTest
{
    [Test]
    public static void Foo()
    {
        var items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual("foo", items[0].Name);
    }

    [TestCase(5, 10, ExpectedResult = 9)]
    [TestCase(4, 10, ExpectedResult = 9)]
    [TestCase(3, 10, ExpectedResult = 9)]
    [TestCase(2, 10, ExpectedResult = 9)]
    [TestCase(1, 10, ExpectedResult = 9)]
    public static int UpdateQuality_BeforeSellByDate_QualityDegradesByOnePerDay(int sellIn, int quality)
    {
        var item = new Item { Name = "foo", SellIn = sellIn, Quality = quality };
        var app = new GildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }
}