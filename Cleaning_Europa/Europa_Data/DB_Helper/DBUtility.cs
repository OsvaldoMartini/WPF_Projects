using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Europa_Data.Model;

namespace Europa_Data.DB_Helper
{
    public class DBUtility
    {
        public const string FilePath = "DBFiles";

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
                    StartDate = new DateTime(2014, 3, 1),//.ToString("yyyy-MM-dd HH:mm:ss"),
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
                    StartDate = new DateTime(2005, 11, 13),//.ToString("yyyy-MM-dd HH:mm:ss"),
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
                    StartDate = new DateTime(2009, 05, 17),//.ToString("yyyy-MM-dd HH:mm:ss"),
                    _Role = new RoleModel {id = 1, RoleName = "Software Developer"},
                    Depto = new DeptoModel {id = 1, DeptoName = "Software Developer"},
                    Leaver = true,
                    LeavingDate = new DateTime(2011, 9, 23),//.ToString("yyyy-MM-dd HH:mm:ss")
                },
                new UserModel
                {
                    Guid = Guid.NewGuid(),
                    UserId = 4,
                    UserName = "jdoe",
                    Forename = "Jane",
                    Surname = "Doe",
                    StartDate = new DateTime(2001, 10, 11),//.ToString("yyyy-MM-dd HH:mm:ss"),
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

        //Type of CSV must be Implemented
        private static string _typeOfFile= ConfigurationSettings.AppSettings["typeOfFile"];

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
            if (!File.Exists(filePath) && (_typeOfFile.Equals("XML")))
            {

                XDocument doc = new XDocument();
                DBUtility.SerializeParams<T>(doc, list);
                doc.Root.Name = "ArrayOf" + fileName;

                doc.Save(filePath + ".xml");

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
                .Where(x => (string)x.Element("UserId") == p.ToString())
                .Remove();
            xdoc.Save(xmlFilename);
            return true;
        }


        public static bool UpdateByXML(UserModel p, string xmlFilename)
        {

            GenericPropertyFinder<UserModel> objGenericPropertyFinder = new GenericPropertyFinder<UserModel>();
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
                            GenericPropertyFinder<RoleModel> objRole = new GenericPropertyFinder<RoleModel>();

                            var pItems = objRole.ReturTModelPropertyAndValue(p._Role);

                            foreach (var subItem in item.Elements())
                            {
                                var valueOf = (from x in pItems
                                               where x.Key.Contains(subItem.Name.ToString())
                                               select x.Value).FirstOrDefault();

                                subItem.SetValue(valueOf);
                            }
                        }
                        else if (item.Name == "Depto")
                        {
                            GenericPropertyFinder<DeptoModel> objDepto = new GenericPropertyFinder<DeptoModel>();

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
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                //ExceptionLogger.WriteExceptionToConsole(ex, string.Now);
            }

            return returnObject;
        }

    }

}
