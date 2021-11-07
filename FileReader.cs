using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
namespace FNPLPreInteview
{
    public class FileReader
    {
        public readonly string fileExtension = ".txt";

        public readonly string[] acceptedEncodings = { Encoding.UTF8.EncodingName };

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
         * <remarks>This was adapted from 
         * https://stackoverflow.com/a/19283954/8814328 due to the fact that 
         * System.IO.StreamReader.CurrentEncoding() is unreliable</remarks>
         * <param name="filePath"/>
         * <returns>System.Text.Encoding</returns>
         */
        protected static Encoding readEncoding(string filePath)
        {
            byte[] bom = new byte[4];
            using (FileStream file =
                new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            foreach(KeyValuePair<Encoding, byte[]>encoding in encodingMap)
            {
                if (
                    Enumerable.Count(encoding.Value.Intersect(bom)) == encoding.Value.Length
                ) {
                    return encoding.Key;
                }
            }

            return Encoding.ASCII;
        }

        public string FileContents { get => fileContents; }

        public string FilePath { get => filePath; }
    }
}
