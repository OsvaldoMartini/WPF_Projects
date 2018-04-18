using System;
using System.IO;
using NUnit.Framework;
using ProductMvvm.Model;
using ProductMvvm.Util;

namespace ProductMvvmTests
{
    [TestFixture]
    public class Test_Exceptions_Caught
    {



        [Test]
        public void Witch_Exception_Will_Fire_PathNotExist()
        {
            //Arrange
            string pathFile = null; 
            try
            {
                //open file
                pathFile = String.Format("{0}{1}\\ProductModel.xml", AppDomain.CurrentDomain.BaseDirectory, DBUtility.FilePath);
                bool existexpected  = File.Exists(pathFile);
                Assert.IsFalse(existexpected);
            }
            catch (FileNotFoundException)
            {
                Assert.Pass("FileNotFoundException");
            }
            catch (IOException)
            {
                Assert.Pass("IOException");
            }
        }



        [Test]
        public void Witch_Exception_Will_Fire_PathExist()
        {
            //Arrange
            string pathFile = null;
            try
            {
                //open file
                pathFile = String.Format("{0}{1}\\ProductModel.xml", AppDomain.CurrentDomain.BaseDirectory, DBUtility.FilePath);
                bool existexpected = File.Exists(pathFile);
                if (!File.Exists(pathFile))
                    DBUtility.CreateFile<ProductModel>(DBUtility.MockProductModel(), pathFile);

                Assert.IsTrue(existexpected);
            }
            catch (FileNotFoundException)
            {
                Assert.Pass("FileNotFoundException");
            }
            catch (IOException)
            {
                Assert.Pass("IOException");
            }
        }
    }
}
