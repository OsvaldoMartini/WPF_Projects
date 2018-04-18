using System;
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
    }
}
