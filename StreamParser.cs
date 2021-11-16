using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace FNPLPreInteview
{
    public class StreamParser
    {
        protected Dictionary<string, Regex> methodMap = new Dictionary<string, Regex>()
        {
            { "letter", new Regex(@"(([a-z])\2*)") },
            { "punctuation", new Regex(@"(([!\\\#%&'\(\)\*,-\.\/:;\?@\[\]_\{}¡§«¶·»¿])\2*)") },
            { "symbol", new Regex(@"(([$+<=>^`|~¢£¤¥¦¨©¬®¯°±´¸×÷])\2*)") }
        };

        protected string stream;

        protected Dictionary<string, Dictionary<char, int>> frequencies =
            new Dictionary<string, Dictionary<char, int>>();

        public StreamParser(FileReader fileReader)
        {
            char[] chars = fileReader.ToString().ToCharArray();
            Array.Sort(chars);
            stream = new string(chars);

            classify();
        }

        protected void classify()
        {
             foreach (KeyValuePair<string, Regex> pair in methodMap)
            {
                if (!frequencies.ContainsKey(pair.Key))
                {
                    frequencies.Add(pair.Key, new Dictionary<char, int>());
                }
                MatchCollection matches = pair.Value.Matches(stream);
                foreach(Match match in matches.OrderBy(x => x.Length))
                {
                    frequencies[pair.Key].Add(match.Value[0], match.Value.Length);
                }
            }
        }

        public override string ToString()
        {
            return stream;
        }

        public Dictionary<char, int> Letters
        {
            get => frequencies["letter"];
        }

        public Dictionary<char, int> Symbols
        {
            get => frequencies["symbol"];
        }

        public Dictionary<char, int> Punctuation
        {
            get => frequencies["punctuation"];
        }

        public char[]? getLeastRecurring(string typeKey)
        {
            if (!frequencies.ContainsKey(typeKey))
            {
                return null;
            }
            int minFreq = frequencies[typeKey].Values.First();
            IEnumerable<KeyValuePair<char, int>> minPairs = frequencies[typeKey]
                .Where(x => x.Value == minFreq);

            return minPairs.Select(x => x.Key).ToArray();
        }

        public char[]? getMostRecurring(string typeKey)
        {
            if (!frequencies.ContainsKey(typeKey))
            {
                return null;
            }
            int maxFreq = frequencies[typeKey].Values.Last();
            IEnumerable<KeyValuePair<char, int>> maxPairs = frequencies[typeKey]
                .Where(x => x.Value == maxFreq);

            return maxPairs.Select(x => x.Key).ToArray();
        }

        public char[]? getNonRecurring(string typeKey)
        {
            if (!frequencies.ContainsKey(typeKey))
            {
                return null;
            }
            IEnumerable<KeyValuePair<char, int>> nonRecurring =
                frequencies[typeKey].Where(x => x.Value == 1);

            return nonRecurring.Select(x => x.Key).ToArray();
        }
    }
}
