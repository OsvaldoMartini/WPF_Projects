﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Europa_Data.DB_Helper;
using Europa_Data.Model;
using NUnit.Framework;

namespace Europa_Tests
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
                pathFile = String.Format("{0}{1}\\UserModel.xml", AppDomain.CurrentDomain.BaseDirectory,
                    DBUtility.FilePath);
                bool existexpected = File.Exists(pathFile);
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
                pathFile = String.Format("{0}{1}\\UserModel.xml", AppDomain.CurrentDomain.BaseDirectory,
                    DBUtility.FilePath);
                bool existexpected = File.Exists(pathFile);
                if (!File.Exists(pathFile))
                    DBUtility.CreateFile<UserModel>(DBUtility.MockUserModel(), pathFile);

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

        [Test]
        public void XMLElement_Search_Test()
        {

            XDocument root = XDocument.Parse(@"<?xml version='1.0' encoding='utf-8'?>
                <ArrayOfUserModel xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                <UserModel>
                <ID>4a2f066d-d0c5-4c71-bd3b-74e6938cf37b</ID>
                <UserId>1</UserId>
                <ModelNumber>ABL1235</ModelNumber>
                <ModelName>ModelName</ModelName>
                <UnitCost>10500</UnitCost>
                <Description>Bla Bla Bla</Description>
                <CategoryName>Category Descents</CategoryName>
                </UserModel>
                <UserModel>
                <ID>2eae414d-d7cd-43a2-b67e-e00b6b525dd0</ID>
                <UserId>2</UserId>
                <ModelNumber>FFhTR65</ModelNumber>
                <ModelName>Toyota</ModelName>
                <UnitCost>310555</UnitCost>
                <Description>Tom Tom Tom</Description>
                <CategoryName>Category Vehicles</CategoryName>
                </UserModel>
                </ArrayOfUserModel>");
            //string grandChild3 = (string)(root.Descendants("UserModel").Select(el => el.Value=="a2f066d-d0c5-4c71-bd3b-74e6938cf37b"));

            Dictionary<string, string> lstDic = new Dictionary<string, string>();
            lstDic.Add("ID", "2eae414d-d7cd-43a2-b67e-e00b6b525dd0");
            lstDic.Add("UserId", "20");
            lstDic.Add("ModelNumber", "Osvaldo Martini");

            var valueProdID = (from x in lstDic
                where x.Key.Contains("ID")
                select x.Value).FirstOrDefault();

            foreach (var child in root.Root.Element("UserModel").Elements())
                Debug.WriteLine(string.Format("{0}:{1}", child.Name, child.Value));

            //Assert.AreEqual("", string.Format("{0}:{1}", child.Name, child.Value));

            var elements = root.Element("ArrayOfUserModel").Elements();
            foreach (var child in elements)
            {
                var elmId = child.Element("ID");
                if (elmId.Value == valueProdID)
                {

                    foreach (var item in child.Elements())
                    {
                        var valueOf = (from x in lstDic
                            where x.Key.Contains(item.Name.ToString())
                            select x.Value).FirstOrDefault();

                        //item.SetElementValue(item.Name, valueOf);
                        //item.SetAttributeValue(item.Name, valueOf);
                        //if (!string.IsNullOrEmpty(valueOf))
                        if (valueOf != null)
                        {
                            item.SetValue(valueOf);
                            Debug.WriteLine(string.Format("{0} : {1}", item.Name, item.Value));
                        }
                    }
                }
                //Debug.WriteLine(elemProduc);  
            }
        }

        [Test]
        public void Test_Some_DI()
        {
            //Mock<IHelperMock> mockBilling = new Mock<UserDisplayModel>();
          
            ////Arrange
            //UserDisplayModel userModel = new UserDisplayModel{new UserVM{UserName = "martini"}};

            ////Action
            //userVm.UserName = "aa";

        }


    }
}
