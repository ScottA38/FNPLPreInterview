
using NUnit.Framework;
using System;
using System.IO;
using FNPLPreInteview;

namespace Tests
{
    [TestFixture()]
    public class FileReaderTest
    {
        protected FileReader sampleFileReader;

        protected FileReader errorFileReader;

        protected readonly string validBasePath = "files";

        protected readonly string validFileName = "test";

        [SetUp()]
        public void init()
        {
            sampleFileReader = new FileReader(validFileName, validBasePath);
            try
            {
                errorFileReader = new FileReader("invalid", "invalid");
            } catch { }
        }

        [Test()]
        public void constructorAcceptsValidFilenameAndBasePath()
        {
            Assert.DoesNotThrow(
                () => { new FileReader(validFileName, validBasePath); }
            );
        }

        [Test()]
        public void constructorDoesNotAcceptInvalidFileName()
        {
            Assert.Throws<FileLoadException>(
                () => { new FileReader("invalid", validBasePath); }
            );
        }

        [Test()]
        public void constructorDoesNotAcceptInvalidBasePath()
        {
            Assert.Throws<FileLoadException>(
                () => { new FileReader(validFileName, "invalid"); }
            );
        }

        [Test()]
        public void providesFileContentsForValidPath()
        {
            Assert.IsInstanceOf<string>(sampleFileReader.FileContents);
        }

        [Test()]
        public void providesNullFileContentsForInvalidPath()
        {
            Assert.IsNull(errorFileReader.FileContents);
        }
    }
}
