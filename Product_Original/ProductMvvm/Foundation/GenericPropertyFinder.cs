using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductMvvm.Foundation
{
    public class GenericPropertyFinder<TModel> where TModel : class
    {
      

        public void PrintTModelPropertyAndValue(TModel tmodelObj)
        {
            //Getting Type of Generic Class Model
            Type tModelType = tmodelObj.GetType();

            //We will be defining a PropertyInfo Object which contains details about the class property 
            PropertyInfo[] arrayPropertyInfos = tModelType.GetProperties();

            //Now we will loop in all properties one by one to get value
            foreach (PropertyInfo property in arrayPropertyInfos)
            {

                Debug.WriteLine("Name of Property is\t:\t" + property.Name);
                Debug.WriteLine("Value of Property is\t:\t" + property.GetValue(tmodelObj).ToString());
                Debug.WriteLine(Environment.NewLine);
            }
        }

        public Dictionary<string,string> ReturTModelPropertyAndValue(TModel tmodelObj)
        {
            //Getting Type of Generic Class Model
            Type tModelType = tmodelObj.GetType();

            //We will be defining a PropertyInfo Object which contains details about the class property 
            PropertyInfo[] arrayPropertyInfos = tModelType.GetProperties();

            Dictionary<string, string> myDict = new Dictionary<string, string>();
            //Now we will loop in all properties one by one to get value
            foreach (PropertyInfo property in arrayPropertyInfos)
                myDict.Add(property.Name, property.GetValue(tmodelObj).ToString());

            return myDict;
        }

        //public System.Collections.IDictionaryEnumerator returnList<T>(T list)
        //{
        //    IEnumerator<T> ide = list.GetEnumerator();
            
        //    return ide as System.Collections.IDictionaryEnumerator;
        //}

        public interface IDictionaryEnumerator : IEnumerator
        {
            DictionaryEntry Entry { get; }
            Object Key { get; }
            Object Value { get; }
        }
    }
}
