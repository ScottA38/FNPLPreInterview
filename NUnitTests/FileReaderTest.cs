
using NUnit.Framework;
using System;
using System.IO;
using FNPLPreInteview;
namespace NUnitTests
{
    [TestFixture]
    public class FileReaderTest
    {
        protected FileReader sampleFileReader;

        protected readonly string validBasePath = "files";

        protected readonly string validFileName = "test";

        [SetUp]
        public void init()
        {
            sampleFileReader = new FileReader(validFileName, validBasePath);
        }

        [Test]
        public void constructorAcceptsValidFilenameAndBasePath()
        {
            string basePath = validBasePath;
            Assert.DoesNotThrow(
                () => { new FileReader(validFileName, basePath); }
            );
        }

        [Test]
        public void constructorDoesNotAcceptInvalidFileName()
        {
            Assert.Throws<FileNotFoundException>(
                () => { new FileReader("invalid", validBasePath); }
            );
        }

        [Test]
        public void constructorDoesNotAcceptInvalidBasePath()
        {
            Assert.Throws<DirectoryNotFoundException>(
                () => { new FileReader(validFileName, "invalid"); }
            );
        }

        [Test]
        public void providesFileContentsForValidPath()
        {
            Assert.IsInstanceOf<string>(sampleFileReader.ToString());
        }

        [Test]
        public void constrcutorAllowsCorrectEncoding()
        {
            string utf8File = validFileName;
            string basePath = validBasePath;

            try {
                new FileReader(utf8File, basePath);
            } catch (FileLoadException) {
                Assert.Fail();
            };

            // Would expect a FileLoadException if encoding were incorrect
            Assert.Pass();
        }
            
        [Test]
        public void constructorDoesNotAcceptInvalidEncoding()
        {
            string asciiFile = "incorrect_encoding";
            string basePath = validBasePath;

            Assert.Throws<InvalidOperationException>(
                () => { new FileReader(asciiFile, basePath); }
            );
        }

        [Test]
        public void constructorDoesNotAcceptInvalidCharacters()
        {
            string illegalFile = "invalid_chars";
            string basePath = validBasePath;

            Assert.Throws<InvalidDataException>(
                () => { new FileReader(illegalFile, basePath); }
            );
        }
    }
}
