using System;
using System.IO;
using NDesk.Options;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;

namespace FNPLPreInteview
{
    class Program
    {
        static readonly Dictionary<string, string> formats =
            new Dictionary<string, string>()
        {
            { "non-repeating", "getNonRecurring" },
            { "least-repeating", "getLeastRecurring" },
            { "most-repeating", "getMostRecurring" }
        };

        static int Main(string[] args)
        {
            string inputFile = default(string);
            string basePath = "files";
            string format = default(string);
            FileReader fileReader;
            List<string> charClasses = new List<string>();
            string method;
            bool help = false;

            OptionSet options = new OptionSet()
            {
                {  "i|input=", "name of the input file (Do not suffix with .txt, only .txt is accepted, UTF-8 encoding only)", v => inputFile = v },
                {  "p|base-path", "an optional base path to the file name specified", v => basePath = v },
                {  "f|format=", $"the frequency restraint that you would like " +
                $"to apply. Choose from: {String.Join(", ", formats.Keys)}", v => format = v },
                { "L|include-letter", "Get the specified format option for lowercase ascii letters", v => charClasses.Add("letter")},
                { "P|include-punctuation", "Get the specified format option for symbol-type characters", v => charClasses.Add("symbol")},
                { "S|include-symbol", "Get the specified format option for punctuation-type characters", v => charClasses.Add("punctuation")},
                { "h|help|?", "Display this help", v =>  help = true }
            };
            List<string> extra = options.Parse(args);

            if (help)
            {
                options.WriteOptionDescriptions(Console.Out);

                return 0;
            }

            if (inputFile == default(string))
            {
                return 1;
            }
            if (charClasses.Count == 0)
            {
                return 4;
            } else if (format == default(string) || !formats.ContainsKey(format))
            {
                return 3;
            }

            method = formats[format];

            try
            {
                fileReader = new FileReader(inputFile, basePath);
            } catch (FileNotFoundException e)
            {
#if DEBUG
                Debug.WriteLine(e.Message);
#endif

                return 1;
            } catch (DirectoryNotFoundException e)
            {
#if DEBUG
                Debug.WriteLine(e.Message);
#endif

                return 1;
            } catch (InvalidOperationException e)
            {
#if DEBUG
                Debug.WriteLine(e.Message);
#endif

                return 2;
            } catch (InvalidDataException e)
            {
#if DEBUG
                Debug.WriteLine(e.Message);
#endif

                return 2;
            }

            StreamParser streamParser = new StreamParser(fileReader);
            Type streamParserType = streamParser.GetType();
            MethodInfo toInvoke = streamParserType.GetMethod(method);
            Console.WriteLine($"File: {fileReader.FilePath}");
            foreach (string charClass in charClasses)
            {
                object? result = toInvoke.Invoke(streamParser, new object[] { charClass });
                if (result.GetType().Name == "Char[]")
                {
                    char[] output = (char[])result;
                    string formattedMethod = CultureInfo.CurrentCulture
                        .TextInfo.ToTitleCase(format).Replace("-", " ");
                    Console.WriteLine($"{formattedMethod} {charClass}{(output.Length > 1 ? "s" : "")}:");
                    Console.WriteLine(String.Join(", ", output));
                }
            }

            return 0;
        }

    }
}
