
using NUnit.Framework;
using System;
using System.IO;
using System.Runtime;
using FNPLPreInteview;
namespace Tests
{
    [TestFixture()]
    public class FileReaderTest
    {
        protected FileReader sampleFileReader;

        protected FileReader errorFileReader;

        protected readonly string validBasePath = "NUnitTests/files";

        protected readonly string validFileName = "test";

        public string projectRoot;

        [SetUp]
        public void init()
        {
            projectRoot = AppContext.BaseDirectory;
            sampleFileReader = new FileReader(validFileName, validBasePath);
            try
            {
                errorFileReader = new FileReader("invalid", "invalid");
            } catch { }
        }

        [Test]
        public void constructorAcceptsValidFilenameAndBasePath()
        {
            string basePath = projectRoot + validBasePath;
            Assert.DoesNotThrow(
                () => { new FileReader(this.validFileName, basePath); }
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
            Assert.IsInstanceOf<string>(sampleFileReader.FileContents);
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
    }
}
