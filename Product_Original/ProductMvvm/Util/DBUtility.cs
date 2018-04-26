using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Hosting;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ProductMvvm.Foundation;
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
                    Guid = Guid.NewGuid(),
                    ProductId = 1,
                    CategoryName = "Category Descents",
                    Description = "Bla Bla Bla",
                    ModelName = "ModelName",
                    ModelNumber = "ABL1235",
                    UnitCost = Convert.ToDecimal("105.00")

                },
                new ProductModel
                {
                    Guid = Guid.NewGuid(),
                    ProductId = 2,
                    CategoryName = "Category Vehicles",
                    Description = "Tom Tom Tom",
                    ModelName = "Toyota",
                    ModelNumber = "FFhTR65",
                    UnitCost = Convert.ToDecimal("3105.55")

                }
            };

            return products;

        }


        public static List<T> CreateFile<T>(List<T> list, string fileName = "")
        {
            string directory =
                Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    FilePath);
            Directory.CreateDirectory(directory); // no need to check if it exists

            if (fileName == string.Empty)
            {
                Type objtype = typeof(T);
                fileName = objtype.Name;
            }

            string filePath = Path.Combine(directory, fileName);
            if (!File.Exists(filePath))
            {
                //Just Create File Blank
                //FileStream f = File.Create(filePath);
                //f.Close();

                XDocument doc = new XDocument();
                DBUtility.SerializeParams<T>(doc, list);
                doc.Root.Name = "ArrayOf" + fileName;
                // doc.Root.Namespace = 
                doc.Save(filePath + ".xml");

                ////Creates Interelly XML File From Object
                //DataContractSerializer serializer = new DataContractSerializer(typeof(List<T>));
                //using (FileStream writer = new FileStream(filePath, FileMode.Create))
                //{
                //    serializer.WriteObject(writer, list);
                //}

            }

            return list;

        }

        public static bool AddByXML(ProductModel p, string xmlFilename)
        {

            XDocument xdoc = XDocument.Load(xmlFilename);

            if (p.ProductId <= 0)
                p.ProductId = xdoc.Root.Elements().Count()+1;
            p.Guid = Guid.NewGuid();

            GenericPropertyFinder<ProductModel> objGenericPropertyFinder = new GenericPropertyFinder<ProductModel>();
            objGenericPropertyFinder.PrintTModelPropertyAndValue(p);
            var lstDic = objGenericPropertyFinder.ReturTModelPropertyAndValue(p);

            XElement ele = new XElement("ProductModel", null);

            foreach (var item in lstDic)
            {
                var valueOf = (from x in lstDic
                    where x.Key.Contains(item.Key)
                    select x.Value).FirstOrDefault();

                ele.SetElementValue(item.Key, valueOf);
                //item.SetAttributeValue(item.Name, valueOf);
            }

            xdoc.Root.Add(ele);



            xdoc.Save(xmlFilename);

            return true;

        }

        public static bool DeleteByXML(int p, string xmlFilename)
        {
            XDocument xdoc = XDocument.Load(xmlFilename);
            xdoc.Element("ArrayOfProductModel")
                .Elements("ProductModel")
                .Where(x => (string)x.Element("ProductId") == p.ToString())
                .Remove();
            xdoc.Save(xmlFilename);

            //var settings = new XmlWriterSettings
            //{
            //    OmitXmlDeclaration = true
            //};
            //using (var stream = File.Create(xmlFilename))
            //{
            //    using (var writer = XmlWriter.Create(stream, settings))
            //    {
            //        xdoc.Save(writer);
            //    }
            //}
            
            return true;
        }


        public static bool UpdateByXML(ProductModel p, string xmlFilename)
        {

            GenericPropertyFinder<ProductModel> objGenericPropertyFinder = new GenericPropertyFinder<ProductModel>();
            objGenericPropertyFinder.PrintTModelPropertyAndValue(p);
            var lstDic = objGenericPropertyFinder.ReturTModelPropertyAndValue(p);

            XDocument xdoc = XDocument.Load(xmlFilename);
            var elements = xdoc.Element("ArrayOfProductModel").Elements();

            var valueProdID = (from x in lstDic
                               where x.Key.Contains("Guid")
                               select x.Value).FirstOrDefault();

            foreach (var child in elements)
            {
                var elmId = child.Element("Guid");
                if (elmId.Value == valueProdID)
                {
                    foreach (var item in child.Elements())
                    {
                        var valueOf = (from x in lstDic
                                       where x.Key.Contains(item.Name.ToString())
                                       select x.Value).FirstOrDefault();
                        if (valueOf != null)
                            item.SetValue(valueOf);
                    }
                }
                else
                {
                    continue;
                }


            }

            xdoc.Save(xmlFilename);

            return true;

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

                result = (List<T>)serializer.Deserialize(reader);
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
