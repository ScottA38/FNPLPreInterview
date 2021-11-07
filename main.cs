using System;

namespace FNPLPreInteview
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FileReader fileReader = new FileReader("test", "files");
            Console.WriteLine(fileReader.FileContents);
        }
    }
}
