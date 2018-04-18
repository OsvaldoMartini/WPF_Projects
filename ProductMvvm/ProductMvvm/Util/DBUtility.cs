using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Hosting;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ProductMvvm.Model;

namespace ProductMvvm.Util
{
    public class DBUtility
    {
        public const string FilePath = "DBFiles";
        public static List<CustomerModel> MockCustomerModel()
        {

            List<CustomerModel> customers = new List<CustomerModel>();
            customers.Add(new CustomerModel
            {
                CustomerId = 1,
                CustomerName = "Tejas Trivedi",
                PhoneNumber = "23244556",
                Email = "tejas@gmail.com"
            });
            customers.Add(new CustomerModel
            {
                CustomerId = 2,
                CustomerName = "Jignesh Trivedi",
                PhoneNumber = "4545453322",
                Email = "jignesh@gmail.com"
            });
            customers.Add(new CustomerModel
            {
                CustomerId = 3,
                CustomerName = "Rakesh Trivedi",
                PhoneNumber = "333222111",
                Email = "rakesh@gmail.com"
            });
            customers.Add(new CustomerModel
            {
                CustomerId = 4,
                CustomerName = "Keyur Joshi",
                PhoneNumber = "999888822",
                Email = "keyur@gmail.com"
            });
            customers.Add(new CustomerModel
            {
                CustomerId = 5,
                CustomerName = "Sachin shah",
                PhoneNumber = "38888232",
                Email = "sachin@gmail.com"
            });
            customers.Add(new CustomerModel
            {
                CustomerId = 6,
                CustomerName = "Mandar Bhatt",
                PhoneNumber = "343412212",
                Email = "mandar@gmail.com"
            });
            return customers;
        }

        public static List<ProductModel> MockProductModel()
        {

            List<ProductModel> products = new List<ProductModel>()
            {
                new ProductModel
                {
                    CategoryName = "Category Descents",
                    Description = "Bla Bla Bla",
                    ModelName = "ModelName",
                    ModelNumber = "ABL1235",
                    UnitCost = Convert.ToDecimal("105.00")

                },
                new ProductModel
                {
                    CategoryName = "Category Vehicles",
                    Description = "Tom Tom Tom",
                    ModelName = "Toyota",
                    ModelNumber = "FFhTR65",
                    UnitCost = Convert.ToDecimal("3105.55")

                }
            };

            return products;

        }


        public static List<T> CreateFile<T>(List<T> list, string fileName) 
        {
            string directory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), FilePath);
            Directory.CreateDirectory(directory); // no need to check if it exists

            string filePath = Path.Combine(directory, fileName);
            if (!File.Exists(filePath))
            {
                //Just Create File Blank
                //FileStream f = File.Create(filePath);
                //f.Close();

                XDocument doc = new XDocument();
                DBUtility.SerializeParams<T>(doc, list);
                doc.Root.Name = fileName;
                doc.Save(filePath +".xml");

                ////Creates Interelly XML File From Object
                //DataContractSerializer serializer = new DataContractSerializer(typeof(List<T>));
                //using (FileStream writer = new FileStream(filePath, FileMode.Create))
                //{
                //    serializer.WriteObject(writer, list);
                //}

            }
            
            return list;
            
         }

        public static bool UpdateByXML(ProductModel p, string xmlFilename)
        {

            XDocument xdoc = XDocument.Load(xmlFilename);
            
            IEnumerable<XElement> elemProduc =  
                from el in xdoc.Descendants("ProductModel")  
                where (string)el.Attribute("ProductId") == p.ProductId.ToString() 
                select el;  
            var items = from item in xdoc.Descendants("ProductModel")  
                where item.Attribute("ID").Value == "2"  
                select item;  
            // Customers is a List<Customer>
            //XElement customersElement = new XElement("ProductModel",
            //    p.Select(c => new XElement("customer",
            //        new XAttribute("name", c.Name),
            //        new XAttribute("lastSeen", c.LastOrder)
            //new XElement("address",
            //    new XAttribute("town", c.Town),
            //    new XAttribute("firstline", c.Address1),
            //    // etc
            //));


            foreach (XElement item in items)  
            {  
                //item.SetAttributeValue(nameof(p.CategoryName),p.CategoryName);  
                //item.SetAttributeValue(nameof(p.CategoryName),p.CategoryName);  
                //item.SetAttributeValue(nameof(p.CategoryName),p.CategoryName);  
                //item.SetAttributeValue(nameof(p.CategoryName),p.CategoryName);  
                //item.SetAttributeValue(nameof(p.CategoryName),p.CategoryName);  
            }  
            
            
            xdoc.Save(xmlFilename);

            return true;

            //var query = from q in xdoc.Elements()
            //            select new ProductModel
            //            {
            //                ProductId = p.ProductId,
            //                ModelNumber = q.ModelNumber,
            //                ModelName = q.ModelName,
            //                UnitCost = (decimal)q.UnitCost,
            //                Description = q.Description,
            //                CategoryName = q.LinqCategory.CategoryName
            //            };



        }

        public static Object ObjectToXML(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception exp)
            {
                //Handle Exception Code
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }
            return obj;
        }

        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }


        public static List<T> DeserializeParamsListOf<T>(string xmlFilename)
        {
            List<T> result;
            XDocument xdoc = XDocument.Load(xmlFilename);
            try
            {

                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(List<T>));

                System.Xml.XmlReader reader = xdoc.CreateReader();

                result = (List<T>) serializer.Deserialize(reader);
                reader.Close();
            }
            finally
            {

            
            }
            return result;

        }


        public static void SerializeParams<T>(XDocument doc, List<T> paramList)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(paramList.GetType());

            System.Xml.XmlWriter writer = doc.CreateWriter();

            serializer.Serialize(writer, paramList);

            writer.Close();
        }


        public static T DeserializeXMLFileOf<T>(string XmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);

            try
            {
                StreamReader xmlStream = new StreamReader(XmlFilename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                //ExceptionLogger.WriteExceptionToConsole(ex, DateTime.Now);
            }
            return returnObject;
        }



    }
}
