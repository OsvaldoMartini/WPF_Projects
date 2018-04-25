using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Europa_Data.DB_Helper
{
    public class GenericPropertyFinder<TModel> where TModel : class
    {
 
        public Dictionary<string,string> ReturTModelPropertyAndValue(TModel tmodelObj)
        {
            //Getting Type of Generic Class Model
            Type tModelType = tmodelObj.GetType();

            //We will be defining a PropertyInfo Object which contains details about the class property 
            PropertyInfo[] arrayPropertyInfos = tModelType.GetProperties();

            Dictionary<string, string> myDict = new Dictionary<string, string>();
            //Now we will loop in all properties one by one to get value
            foreach (PropertyInfo property in arrayPropertyInfos)
            {
                myDict.Add(property.Name,
                    property.GetValue(tmodelObj) != null ? property.GetValue(tmodelObj).ToString(): null);
            }

            return myDict;
        }
    }
}
