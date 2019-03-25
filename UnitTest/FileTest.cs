using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class FileTest
    {
        [TestMethod]
        public void FileRead()
        {
            StreamReader sr = new StreamReader("C:\\RDMS\\Geo DSS-Demand\\Test Driven Development\\FamilyTree\\FamilyTree\\InputFile.txt");
            String line = sr.ReadLine();

            Assert.IsNotNull(line);
        }

        [TestMethod]
        public void ReadEmptyFile()
        {
            StreamReader sr = new StreamReader("C:\\Users\\stdiwaka\\Documents\\TestFile.txt");
            String line = sr.ReadLine();

            Assert.IsNull(line);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void FileNotCompatible()
        {
            StreamReader sr = new StreamReader("C:\\Users\\stdiwaka\\Documents\\IMG_7266.JPG");
            String line = sr.ReadLine();

            
            StringAssert.Contains(line, "ADD_CHILD");
        }
    }
}
