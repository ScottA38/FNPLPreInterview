using NUnit.Framework;
using System;
using System.Linq;
using FNPLPreInteview;
namespace NUnitTests
{
    [TestFixture]
    public class StreamParserTest
    {
        protected StreamParser streamParser;

        protected readonly char[] validSymbols = {
            '$','+','<','=','>','^','`','|','~','¢','£','¤','¥','¦','¨','©','¬',
            '®','¯','°','±','´','¸','×','÷'
        };

        protected readonly char[] validPunctuation = {
            '!','\\','#','%','&','\'','(',')','*',',','-','.','/',':',';','?',
            '@','[','\\',']','_','{','}','¡','§','«','¶','·','»','¿'
        };

        protected readonly char[] validLetters =
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q',
            'r','s','t','u','v','w','x','y','z'
        };

        [SetUp]
        public void init()
        {
            FileReader fileReader = new FileReader("test", "files");
            streamParser = new StreamParser(fileReader);
        }

        [Test]
        public void constructorAcceptsCorrectParameters()
        {
            FileReader fileReader = new FileReader("test", "files");
            Assert.DoesNotThrow(
                () => { new StreamParser(fileReader); }
            );
        }

        [Test]
        public void correctlySortsStream()
        {
            string expected = @"!!$$$$$'')),,::::???@@aaaaaaaaaaacccccddddddeeeeeeeeeeeeeeeeeeeeeeefffgggghhhhhhhiiiiiiiiiiiiiiiiiiiiijkkkkllllmmmmmmmmmmnnnnnnnnnnnnnoooooooooooooooooppppprrrrrrrrrrsssssssssttttttttttttttttttuuuuuuuuuvvvwwwxxyyyy¢£¤¥";

            Assert.AreEqual(expected, streamParser.ToString());
        }

        [Test]
        public void canFindNonRepeatingChar()
        {
            FileReader fileReader = new FileReader("non-repeat", "files");
            StreamParser newStream = new StreamParser(fileReader);
            char[] nonRepeat = newStream.getNonRecurring("punctuation");

            Assert.Contains('!', nonRepeat);
            foreach (char character in nonRepeat)
            {
                Assert.Contains(character, validPunctuation);
            }
        }

        [Test]
        public void canFindMostRepeatingLetter()
        {
            Assert.AreEqual(new char[] { 'e' }, streamParser.getMostRecurring("letter"));
        }

        [Test]
        public void canFindLeastRepeatingLetter()
        {
            Assert.AreEqual(new char[] { 'j' }, streamParser.getLeastRecurring("letter"));
        }

        [Test]
        public void canFindNonRepeatingSymbol()
        {
            char[] symbolsExpected = new char[] { '¢', '£', '¤', '¥' };
            int symbolsIntersectionCount = streamParser
                .getNonRecurring("symbol").Count();

            Assert.AreEqual(symbolsExpected.Count(), symbolsIntersectionCount);
        }

        [Test]
        public void canFindMostRepeatingSymbol()
        {
            Assert.AreEqual(new char[] { '$' }, streamParser.getMostRecurring("symbol"));
        }

        [Test]
        public void canFindLeastRepeatingSymbol()
        {
            char[] symbolsExpected = new char[] { '¢', '£', '¤', '¥' };
            int symbolsIntersectionCount = streamParser
                .getLeastRecurring("symbol").Count();

            Assert.AreEqual(symbolsExpected.Count(), symbolsIntersectionCount);
        }

        [Test]
        public void canFindNonRepeatingPunctuation()
        {
            Assert.IsEmpty(streamParser.getNonRecurring("punctuation"));
        }

        [Test]
        public void canFindMostRepeatingPunctuation()
        {
            Assert.AreEqual(new char[] { ':' }, streamParser.getMostRecurring("punctuation"));
        }

        [Test]
        public void canFindLeastRepeatingPunctuation()
        {
            char[] expected = new char[] { '\'', '@', ')', ',', '!' };
            int intersectionCount = streamParser
                .getLeastRecurring("punctuation").Intersect(expected)
                .Count();

            Assert.AreEqual(expected.Count(), intersectionCount);
        }
    }
}
