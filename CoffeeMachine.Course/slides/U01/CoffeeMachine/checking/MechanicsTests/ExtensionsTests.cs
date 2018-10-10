using System;
using CoffeeMachine.Mechanics;
using NUnit.Framework;

namespace CoffeeMachine.MechanicsTests
{
    public class ExtensionsTests
    {
        [TestCase("a", "a")]
        [TestCase(" a ", "a")]
        [TestCase(" a b", "a b")]
        [TestCase(" a  b", "a b")]
        [TestCase("  a   b ", "a b")]
        [TestCase("", "")]
        [TestCase(null, null)]
        [TestCase("string", "string")]
        [TestCase("A  big    black     bug bit a big         black bear       ",
            "A big black bug bit a big black bear")]
        public void TrimWhitespaces_ShouldProduceExpectedString(string actual, string expected)
        {
            Assert.AreEqual(expected, actual.TrimWhitespaces());
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("                        ")]
        public void TrimWhitespaces_ShouldProduceEmptyString_OnWhitespacesString(string actual)
        {
            Assert.AreEqual(string.Empty, actual.TrimWhitespaces());
        }


        [Test]
        public void SerializeNoMoreThan_TopIsMoreThanLegnthOfCollectionTest()
        {
            var initial = new[]
            {
                "1",
            };
            var actual = initial.SerializeNoMoreThan(5);
            Assert.AreEqual("1.", actual);
        }

        [TestCase(3, "1, 2, 3 и еще 4.")]
        [TestCase(1, "1 и еще 6.")]
        [TestCase(6, "1, 2, 3, 4, 5, 6 и еще 1.")]
        public void SerializeNoMoreThan_ShouldSerializeCorrectly(int top, string expected)
        {
            var initial = new[]
            {
                "1", "2", "3", "4", "5", "6", "7"
            };
            var actual = initial.SerializeNoMoreThan(top);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(3, "каких-то штук", "1, 2, 3 и еще 4 каких-то штук.")]
        [TestCase(1, "каких-то штук", "1 и еще 6 каких-то штук.")]
        [TestCase(6, "", "1, 2, 3, 4, 5, 6 и еще 1.")]
        [TestCase(6, "каких-то штук.", "1, 2, 3, 4, 5, 6 и еще 1 каких-то штук.")]
        public void SerializeNoMoreThan_ShouldSerializeCorrectly_WithEnding(int top, string ending, string expected)
        {
            var initial = new[]
            {
                "1", "2", "3", "4", "5", "6", "7"
            };
            var actual = initial.SerializeNoMoreThan(top, ending);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void SerializeNoMoreThan_ShouldThrow(int top)
        {
            var initial = new[]
            {
                "1"
            };
            Assert.Throws<ArgumentOutOfRangeException>(() => initial.SerializeNoMoreThan(top));
        }
    }
}