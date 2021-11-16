using System;
using System.Text.RegularExpressions;
namespace FNPLPreInteview
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            FileReader fileReader = new FileReader("test", "files");
            StreamParser streamParser = new StreamParser(fileReader);
            Console.WriteLine(streamParser.getLeastRecurring("symbol"));
            Console.WriteLine(streamParser.getMostRecurring("letter"));
            Console.WriteLine(streamParser.getNonRecurring("punctuation"));
        }
    }
}
