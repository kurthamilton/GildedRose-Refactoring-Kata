﻿using System.Collections.Generic;
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

    [TestCase(0, 10, ExpectedResult = 8)]
    [TestCase(-1, 10, ExpectedResult = 8)]
    [TestCase(-2, 10, ExpectedResult = 8)]
    [TestCase(-3, 10, ExpectedResult = 8)]
    [TestCase(-4, 10, ExpectedResult = 8)]
    public static int UpdateQuality_OnOrAfterSellByDate_QualityDegradesByTwoPerDay(int sellIn, int quality)
    {
        var item = new Item { Name = "foo", SellIn = sellIn, Quality = quality };
        var app = new GildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(1, ExpectedResult = 0)]
    [TestCase(0, ExpectedResult = 0)]
    [TestCase(-1, ExpectedResult = 0)]    
    public static int UpdateQuality_QualityZero_QualityNeverNegative(int sellIn)
    {
        var item = new Item { Name = "foo", SellIn = sellIn, Quality = 0 };
        var app = new GildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(5, 10, ExpectedResult = 11)]
    [TestCase(4, 10, ExpectedResult = 11)]
    [TestCase(3, 10, ExpectedResult = 11)]
    [TestCase(2, 10, ExpectedResult = 11)]
    [TestCase(1, 10, ExpectedResult = 11)]
    public static int UpdateQuality_AgedBrie_BeforeSellByDate_IncreasesInQualityByOne(int sellIn, int quality)
    {
        var item = new Item { Name = "Aged Brie", SellIn = sellIn, Quality = quality };
        var app = new GildedRose(new List<Item> { item });
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
        var item = new Item { Name = "Aged Brie", SellIn = sellIn, Quality = quality };
        var app = new GildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }

    [TestCase(1, 50, ExpectedResult = 50)]
    [TestCase(0, 50, ExpectedResult = 50)]
    [TestCase(-1, 50, ExpectedResult = 50)]
    public static int UpdateQuality_AgedBrie_QualityNeverExceedsFifty(int sellIn, int quality)
    {
        var item = new Item { Name = "Aged Brie", SellIn = sellIn, Quality = quality };
        var app = new GildedRose(new List<Item> { item });
        app.UpdateQuality();
        return item.Quality;
    }
}