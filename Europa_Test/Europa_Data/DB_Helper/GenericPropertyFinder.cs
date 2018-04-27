using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Europa_Data.Model;

namespace Europa_Data.DB_Helper
{
    public class GenericPropertyFinder<TModel> where TModel : class
    {

        public Dictionary<string, string> ReturTModelPropertyAndValue(object tmodelObj)
        {
            //Getting Type of Generic Class Model
            Type tModelType = tmodelObj.GetType();

            //We will be defining a PropertyInfo Object which contains details about the class property 
            PropertyInfo[] arrayPropertyInfos = tModelType.GetProperties();

            Dictionary<string, string> myDict = new Dictionary<string, string>();
            //Now we will loop in all properties one by one to get value
            foreach (PropertyInfo property in arrayPropertyInfos)
            {
                if (property.Name == "_Role"|| property.Name == "Depto")
                {
                    var childProp = tModelType.GetProperty(property.Name);
                    //var childValues = (RoleModel)childProp.GetValue(tmodelObj);
                    var childValues = childProp.GetValue(tmodelObj);
                    Type subType = childValues.GetType();
                    PropertyInfo[] subProps = subType.GetProperties();
                    
                    foreach (PropertyInfo subproperty in subProps)
                    {
                        myDict.Add(property.Name+"."+subproperty.Name,
                            subproperty.GetValue(childValues) != null ? subproperty.GetValue(childValues).ToString() : null);
                    }
                    //var childObj = (RoleModel)childProp.GetValue(tmodelObj);
                    //Debug.WriteLine(childObj.id);
                    //Debug.WriteLine(childObj.RoleName);

                    //var parent = new Parent();
                    //parent.Child.age = "100";
                    //var childValue = childProp.ConvertToChildObject<Parent, Child>(parent);
                    //var childValueNull = childProp.ConvertToChildObject<Parent, Child>(null);

                  //  Type subObj = property.GetValue(tmodelObj).GetType();
                  //  Type obj = typeof(RoleModel).GetProperty(property.Name);
                  //  //RoleModel objRole = (RoleModel)obj.GetValue(property);
                  // foreach (PropertyInfo subproperty in obj)
                  //{
                  //  myDict.Add(objRole.GetType().Name,
                  //          property.GetValue(tmodelObj) != null ? objRole.RoleName.ToString() : null);

                  //  }
                    //PropertyInfo[] subObjPropertyInfos = subObj.GetProperties();
                    //foreach (PropertyInfo subproperty in subObjPropertyInfos)
                    //{
                    //    myDict.Add(subproperty.Name,
                    //        property.GetValue(tmodelObj) != null ? subproperty.GetValue(tmodelObj).ToString() : null);

                    //}
                    //ReturTModelPropertyAndValue(subObj.GetType());
                }
                else
                    myDict.Add(property.Name,
                        property.GetValue(tmodelObj) != null ? property.GetValue(tmodelObj).ToString(): null);
            }

            return myDict;
        }

        public static TPropertyType ConvertToChildObject<TInstanceType, TPropertyType>(PropertyInfo propertyInfo, TInstanceType instance)
            where TInstanceType : class, new()
        {
            if (instance == null)
                instance = Activator.CreateInstance<TInstanceType>();

            //var p = (Child)propertyInfo;
            return (TPropertyType)propertyInfo.GetValue(instance);

        }

     
    }
    public class Child
    {
        public string name = "S";
        public string age = "44";
    }

    public class Parent
    {
        public Parent()
        {
            Child = new Child();
        }

        public Child Child { get; set; }
    }

}
