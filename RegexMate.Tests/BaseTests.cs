using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexMate.Tests
{
    [TestClass]
    public class BaseTests
    {
        [TestMethod]
        public void MatchAlphaNumeric()
        {
            var regex = new RM().AlphaNumeric.Build();
            Assert.AreEqual(regex.ToString(), "[A-Za-z0-9]");
            Assert.IsTrue(regex.IsMatch("y"));
            Assert.IsTrue(regex.IsMatch("9"));
            Assert.IsFalse(regex.IsMatch("\t"));
        }

        [TestMethod]
        public void MatchSingleDigit()
        {
            var regex = new RM().Digit.Build();
            Assert.AreEqual(regex.ToString(), @"\d");
            Assert.IsTrue(regex.IsMatch("5"));
            Assert.IsFalse(regex.IsMatch("a"));
        }

        [TestMethod]
        public void MatchSingleNonDigit()
        {
            var regex = new RM().NonDigit.Build();
            Assert.AreEqual(regex.ToString(), @"\D");
            Assert.IsTrue(regex.IsMatch("u"));
            Assert.IsFalse(regex.IsMatch("2"));
        }

        [TestMethod]
        public void MatchSingleWord()
        {
            var regex = new RM().Word.Build();
            Assert.AreEqual(regex.ToString(), @"\w");
            Assert.IsTrue(regex.IsMatch("5"));
            Assert.IsTrue(regex.IsMatch("a"));
            Assert.IsFalse(regex.IsMatch(" "));
        }

        [TestMethod]
        public void MatchSingleNonWord()
        {
            var regex = new RM().NonWord.Build();
            Assert.AreEqual(regex.ToString(), @"\W");
            Assert.IsTrue(regex.IsMatch(" "));
            Assert.IsTrue(regex.IsMatch("\t"));
            Assert.IsFalse(regex.IsMatch("a"));
        }

        [TestMethod]
        public void MatchBoundary()
        {
            var regex = new RM().Boundary.Word.Boundary.Build();
            Assert.AreEqual(regex.ToString(), @"\b\w\b");
            Assert.IsTrue(regex.IsMatch(" a "));
            Assert.IsFalse(regex.IsMatch("abc"));
        }

        [TestMethod]
        public void MatchNonBoundary()
        {
            var regex = new RM().NonBoundary.Word.NonBoundary.Build();
            Assert.AreEqual(regex.ToString(), @"\B\w\B");
            Assert.IsTrue(regex.IsMatch("abc"));
            Assert.IsFalse(regex.IsMatch(" a "));
        }

        [TestMethod]
        public void OnlyCharactersStringRegex()
        {
            var regex = new RM().BeginLine
                            .AlphaNumeric.OneOrMore
                            .EndLine
                            .Build();

            Assert.AreEqual(regex.ToString(), "^[A-Za-z0-9]+$");
            Assert.IsTrue(regex.IsMatch("abc89079813asldkf"));
            Assert.IsFalse(regex.IsMatch("\n\r"));
        }

        [TestMethod]
        public void MatchExactly5Characters()
        {
            var regex = new RM().BeginLine
                .Word.Exactly(5)
                .EndLine
                .Build();
            Assert.AreEqual(regex.ToString(), @"^\w{5}$");
            Assert.IsTrue(regex.IsMatch("abcde"));
            Assert.IsFalse(regex.IsMatch("1234"));
        }

        [TestMethod]
        public void MatchAtLeast6Numbers()
        {
            var regex = new RM().BeginLine
                .Digit.AtLeast(6)
                .EndLine
                .Build();
            Assert.AreEqual(regex.ToString(), @"^\d{6,}$");
            Assert.IsTrue(regex.IsMatch("1234567"));
            Assert.IsFalse(regex.IsMatch("1234"));
        }

        [TestMethod]
        public void MatchAtMost3AlphaNumeric()
        {
            var regex = new RM().BeginLine
                .AlphaNumeric.AtMost(3)
                .EndLine
                .Build();
            Assert.AreEqual(regex.ToString(), @"^[A-Za-z0-9]{1,3}$");
            Assert.IsTrue(regex.IsMatch("1a"));
            Assert.IsFalse(regex.IsMatch("sd5fa6sd1"));
        }

    }
}
