using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace Products_Tests
{
    [TestFixture]
    public class Test_Exceptions_Caught
    {

        [Test]
        public void XMLElement_Search_Test()
        {

            XDocument root = XDocument.Parse(@"<?xml version='1.0' encoding='utf-8'?>
                <ArrayOfProductModel xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                <ProductModel>
                <ID>4a2f066d-d0c5-4c71-bd3b-74e6938cf37b</ID>
                <ProductId>1</ProductId>
                <ModelNumber>ABL1235</ModelNumber>
                <ModelName>ModelName</ModelName>
                <UnitCost>10500</UnitCost>
                <Description>Bla Bla Bla</Description>
                <CategoryName>Category Descents</CategoryName>
                </ProductModel>
                <ProductModel>
                <ID>2eae414d-d7cd-43a2-b67e-e00b6b525dd0</ID>
                <ProductId>2</ProductId>
                <ModelNumber>FFhTR65</ModelNumber>
                <ModelName>Toyota</ModelName>
                <UnitCost>310555</UnitCost>
                <Description>Tom Tom Tom</Description>
                <CategoryName>Category Vehicles</CategoryName>
                </ProductModel>
                </ArrayOfProductModel>");
            //string grandChild3 = (string)(root.Descendants("ProductModel").Select(el => el.Value=="a2f066d-d0c5-4c71-bd3b-74e6938cf37b"));

            Dictionary<string, string> lstDic = new Dictionary<string, string>();
            lstDic.Add("ID", "2eae414d-d7cd-43a2-b67e-e00b6b525dd0");
            lstDic.Add("ProductId", "20");
            lstDic.Add("ModelNumber", "Osvaldo Martini");

            var valueProdID = (from x in lstDic
                where x.Key.Contains("ID")
                select x.Value).FirstOrDefault();

            foreach (var child in root.Root.Element("ProductModel").Elements())
                Debug.WriteLine(string.Format("{0}:{1}", child.Name, child.Value));

            //Assert.AreEqual("", string.Format("{0}:{1}", child.Name, child.Value));

            var elements = root.Element("ArrayOfProductModel").Elements();
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
        public void Test_Name_Change()
        {
            //Mock<IHelperMock> mockBilling = new Mock<UserDisplayModel>();
            //Mock<ICustomer> mockCustomer = new Mock<ICustomer>();
            ////Arrange
            //UserDisplayModel userModel = new UserDisplayModel{new UserVM{UserName = "martini"}};

            ////Action
            //userVm.UserName = "aa";

        }


    }
}
