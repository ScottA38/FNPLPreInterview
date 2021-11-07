using System;
namespace FNPLPreInteview
{
    public class StreamParser
    {
        protected string stream;

        public StreamParser(FileReader fileReader)
        {
            stream = fileReader.FileContents;
        }
    }
}
