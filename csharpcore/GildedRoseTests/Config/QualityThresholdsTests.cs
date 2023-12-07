using GildedRoseKata.Config;
using NUnit.Framework;

namespace GildedRoseTests.Config;

public static class QualityThresholdsTests
{
    [TestCase(null, null, 1, ExpectedResult = true)]
    [TestCase(1, null, 0, ExpectedResult = false)]
    [TestCase(1, null, 1, ExpectedResult = true)]
    [TestCase(1, null, 2, ExpectedResult = true)]
    [TestCase(null, 1, 0, ExpectedResult = true)]
    [TestCase(null, 1, 1, ExpectedResult = true)]
    [TestCase(null, 1, 2, ExpectedResult = false)]
    [TestCase(1, 3, 0, ExpectedResult = false)]
    [TestCase(1, 3, 1, ExpectedResult = true)]
    [TestCase(1, 3, 2, ExpectedResult = true)]
    [TestCase(1, 3, 3, ExpectedResult = true)]
    [TestCase(1, 3, 4, ExpectedResult = false)]
    public static bool IsFor(int? lowerBound, int? upperBound, int value)
    {
        var thresholds = new QualityThresholds(lowerBound, upperBound, 1);
        return thresholds.IsFor(value);
    }
}
