using NUnit.Framework;
using System;
using System.IO;
using FNPLPreInteview;
namespace NUnitTests
{
    [TestFixture]
    public class StreamParserTest
    {
        protected StreamParser streamParser;

        protected readonly string[] validSymbols = {
            "$","+","<","=",">","^","`","|","~","¢","£","¤","¥","¦","¨","©","¬",
            "®","¯","°","±","´","¸","×","÷"
        };

        protected readonly string[] validPunctuation = {
            "!","\"","#","%","&","\"","(",")","*",",","-",".","/",":",";","?",
            "@","[","\\","]","_","{","}","¡","§","«","¶","·","»","¿"
        };

        protected readonly string[] validLetters =
        {
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q",
            "R","S","T","U","V","W","X","Y","Z","a","b","c","d","e","f","g","h",
            "i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y",
            "z","ª","µ","º","À","Á","Â","Ã","Ä","Å","Æ","Ç","È","É","Ê","Ë","Ì",
            "Í","Î","Ï","Ð","Ñ","Ò","Ó","Ô","Õ","Ö","Ø","Ù","Ú","Û","Ü","Ý","Þ",
            "ß","à","á","â","ã","ä","å","æ","ç","è","é","ê","ë","ì","í","î","ï",
            "ð","ñ","ò","ó","ô","õ","ö","ø","ù","ú","û","ü","ý","þ"
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
                () => { FileReader fileReader = new FileReader("test", "files"); }
            );
        }

        [Test]
        public void recognisesCorrectSymbols()
        {
            
        }

        [Test]
        public void recognisesCorrectLetters()
        {

        }

        [Test]
        public void recognisesCorrectPunctuation()
        {

        }
    }
}
