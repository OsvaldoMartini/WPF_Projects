using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using WPF_Europa_MVVM.Foundation;
using WPF_Europa_MVVM.Model;

namespace WPF_Europa_MVVM.DB_Helper
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

        public static List<UserModel> MockUserModel()
        {
            List<UserModel> users = new List<UserModel>()
            {
                new UserModel
                {
                    Guid = Guid.NewGuid(),
                    UserId = 1,
                    UserName = "jbloggs",
                    Forename = "Joe",
                    Surname = "Bloggs",
                    StartDate = new DateTime(2014, 3, 1),
                    _Role = new RoleModel {id = 1, RoleName = "Software Developer"},
                    Depto = new DeptoModel {id = 1, DeptoName = "Software Developer"},
                    Leaver = false
                },
                new UserModel
                {
                    Guid = Guid.NewGuid(),
                    UserId = 2,
                    UserName = "jsmith",
                    Forename = "John",
                    Surname = "Smith",
                    StartDate = new DateTime(2005, 11, 13),
                    _Role = new RoleModel {id = 2, RoleName = "Development Manager"},
                    Depto = new DeptoModel {id = 1, DeptoName = "Software Developer"},
                    Leaver = false
                },
                new UserModel
                {
                    Guid = Guid.NewGuid(),
                    UserId = 3,
                    UserName = "dvogle",
                    Forename = "Dan",
                    Surname = "Vogle",
                    StartDate = new DateTime(2009, 05, 17),
                    _Role = new RoleModel {id = 1, RoleName = "Software Developer"},
                    Depto = new DeptoModel {id = 1, DeptoName = "Software Developer"},
                    Leaver = true,
                    LeavingDate = new DateTime(2011, 9, 23)
                },
                new UserModel
                {
                    Guid = Guid.NewGuid(),
                    UserId = 4,
                    UserName = "jdoe",
                    Forename = "Jane",
                    Surname = "Doe",
                    StartDate = new DateTime(2001, 10, 11),
                    _Role = new RoleModel {id = 3, RoleName = "IT Director"},
                    Depto = new DeptoModel {id = 2, DeptoName = "Group"},
                    Leaver = false
                }
            };

            return users;

        }

        public static List<RoleModel> MockRoles()
        {
            List<RoleModel> roles = new List<RoleModel>()
            {
                new RoleModel {id = 1, RoleName = "Software Developer"},
                new RoleModel {id = 2, RoleName = "Development Manager"},
                new RoleModel {id = 3, RoleName = "IT Director"},
            };

            return roles;

        }

        public static List<DeptoModel> MockDepartment()
        {
            List<DeptoModel> dptos = new List<DeptoModel>()
            {

                new DeptoModel {id = 1, DeptoName = "Software Developer"},
                new DeptoModel {id = 2, DeptoName = "Group"},
            };

            return dptos;

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

        public static bool AddByXML(UserModel p, string xmlFilename)
        {

            XDocument xdoc = XDocument.Load(xmlFilename);

            if (p.UserId <= 0)
                p.UserId = xdoc.Root.Elements().Count() + 1;
            p.Guid = Guid.NewGuid();

            GenericPropertyFinder<UserModel> objGenericPropertyFinder = new GenericPropertyFinder<UserModel>();
            objGenericPropertyFinder.PrintTModelPropertyAndValue(p);
            var lstDic = objGenericPropertyFinder.ReturTModelPropertyAndValue(p);

            XElement ele = new XElement("UserModel", null);

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
            xdoc.Element("ArrayOfUserModel")
                .Elements("UserModel")
                .Where(x => (string) x.Element("UserId") == p.ToString())
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


        public static bool UpdateByXML(UserModel p, string xmlFilename)
        {

            GenericPropertyFinder<UserModel> objGenericPropertyFinder = new GenericPropertyFinder<UserModel>();
            objGenericPropertyFinder.PrintTModelPropertyAndValue(p);
            var lstDic = objGenericPropertyFinder.ReturTModelPropertyAndValue(p);

            XDocument xdoc = XDocument.Load(xmlFilename);
            var elements = xdoc.Element("ArrayOfUserModel").Elements();

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

                        if (item.Name == "_Role")
                        {
                            GenericPropertyFinder<RoleModel> objRole= new GenericPropertyFinder<RoleModel>();
                            
                            var pItems = objRole.ReturTModelPropertyAndValue(p._Role);
                            
                            foreach (var subItem in item.Elements())
                            {
                                var valueOf = (from x in pItems
                                    where x.Key.Contains(subItem.Name.ToString())
                                    select x.Value).FirstOrDefault();
                                
                                subItem.SetValue(valueOf);
                            }
                        }else if (item.Name == "Depto")
                        {
                            GenericPropertyFinder<DeptoModel> objDepto= new GenericPropertyFinder<DeptoModel>();
                            
                            var pItems = objDepto.ReturTModelPropertyAndValue(p.Depto);

                            foreach (var subItem in item.Elements())
                            {
                                var valueOf = (from x in pItems
                                    where x.Key.Contains(subItem.Name.ToString())
                                    select x.Value).FirstOrDefault();

                                subItem.SetValue(valueOf);
                            }
                        }
                        else
                        {
                            var valueOf = (from x in lstDic
                                where x.Key.Contains(item.Name.ToString())
                                select x.Value).FirstOrDefault();

                            if (valueOf != null)
                                item.SetValue(valueOf);

                        }
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
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(paramList.GetType());

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
                returnObject = (T) serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                //ExceptionLogger.WriteExceptionToConsole(ex, DateTime.Now);
            }

            return returnObject;
        }

    }

}
