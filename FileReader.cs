using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
namespace FNPLPreInteview
{
    public class FileReader
    {
        public static readonly string fileExtension = ".txt";

        public static readonly string[] acceptedEncodings = { Encoding.UTF8.EncodingName };

        protected string filePath;

        protected string fileContents = null;

        protected static Dictionary<Encoding, byte[]> encodingMap = new Dictionary<Encoding, byte[]>() {
            { Encoding.UTF7, new byte[]{ 0x2b, 0x2f, 0x76 } },
            { Encoding.UTF8, new byte[]{ 0xef, 0xbb, 0xbf } },
            { Encoding.UTF32, new byte[]{ 0xff, 0xfe, 0, 0 } },
            { Encoding.Unicode, new byte[]{ 0xff, 0xfe } },
            { Encoding.BigEndianUnicode, new byte[]{ 0xfe, 0xff }}
        };


        /**
         * <param name="fileName">Name of file, without extension</param>
         * <param name="basePath">Path to fileName, from project root</param>
         * <exception cref="InvalidOperationException">Throws exception when file
         * parameters do not meet requirements</exception>
         * <exception cref="FileNotFoundException">Incorrect file name</exception>
         * <exception cref="DirectoryNotFoundException">Incorrect basePath 
         * parameter</exception>
         * <exception cref="InvalidDataException">Illegal chars found in 
         * input</exception>
         */
        public FileReader(string fileName, string basePath)
        {
            string extended = String.Concat(fileName, fileExtension);
            filePath = Path.Join(basePath, extended);

            if (!verifyEncoding()) {
                throw new InvalidOperationException(
                    String.Format("Encoding of file at {0} is invalid", filePath)
                );
            }

            fileContents = File.ReadAllText(filePath);
            char[] illegalChars = fileContents.ToCharArray()
                .Where(checkCharacter).ToArray();

            if (fileContents.Length == 0)
            {
                throw new InvalidDataException($"No content found in filepath ${filePath}");
            }
            else if (illegalChars.Length > 0)
            {
                throw new InvalidDataException($"Illegal chars '${new string(illegalChars)}' " +
                    $"found in input file. See README.md for input file specifications");
            }
        }


        /**
         * <returns>bool</returns>
         */
        protected bool verifyEncoding()
        {
            string encodingName = readEncoding(filePath).EncodingName;

            return Array.IndexOf(acceptedEncodings, encodingName) != -1;
        }

        /**
         * Verifies that no illegal characters are present
         * <param name="filePath">The location of the file to inspect</param>
         */
        protected static Encoding readEncoding(string filePath)
        {
            byte[] bom = new byte[4];
            using (FileStream file =
                new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            foreach (KeyValuePair<Encoding, byte[]> encoding in encodingMap)
            {
                if (
                    Enumerable.Count(encoding.Value.Intersect(bom)) == encoding.Value.Length
                ) {
                    return encoding.Key;
                }
            }

            return Encoding.ASCII;
        }

        /**
         * Test that a single character meets conditions
         */
        private static bool checkCharacter(char character)
        {
            return !char.IsSymbol(character)
                    && !char.IsPunctuation(character)
                    && (character < 97 || character > 122);
        }

        public override string ToString()
        {
            return fileContents;
        }

        public string FilePath { get => filePath; }
    }
}
