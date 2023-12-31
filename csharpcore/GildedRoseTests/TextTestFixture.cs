﻿using System;
using System.Collections.Generic;
using GildedRoseKata;
using GildedRoseKata.Config;

namespace GildedRoseTests;

public static class TextTestFixture
{
    public static void Main(string[] args)
    {
        Console.WriteLine("OMGHAI!");

        var items = new List<Item>{
            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new Item {Name = Constants.AgedBrie, SellIn = 2, Quality = 0},
            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new Item {Name = Constants.Sulfuras, SellIn = 0, Quality = 80},
            new Item {Name = Constants.Sulfuras, SellIn = -1, Quality = 80},
            new Item
            {
                Name = Constants.BackstagePasses,
                SellIn = 15,
                Quality = 20
            },
            new Item
            {
                Name = Constants.BackstagePasses,
                SellIn = 10,
                Quality = 49
            },
            new Item
            {
                Name = Constants.BackstagePasses,
                SellIn = 5,
                Quality = 49
            },
            // this conjured item does not work properly yet
            new Item {Name = Constants.Conjured, SellIn = 3, Quality = 6}
        };

        var app = new GildedRose(items, new ItemConfigFactory());

        int days = 2;
        if (args.Length > 0)
        {
            days = int.Parse(args[0]) + 1;
        }

        for (var i = 0; i < days; i++)
        {
            Console.WriteLine("-------- day " + i + " --------");
            Console.WriteLine("name, sellIn, quality");
            for (var j = 0; j < items.Count; j++)
            {
                Console.WriteLine(items[j].Name + ", " + items[j].SellIn + ", " + items[j].Quality);
            }
            Console.WriteLine("");
            app.UpdateQuality();
        }
    }
}