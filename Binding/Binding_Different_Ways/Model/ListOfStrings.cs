using System;
using System.Collections.Generic;

namespace Binding.Different.Ways.Model
{
    public class ListOfStrings : List<String>
    {
        public ListOfStrings()
        {
            this.Add("Vodka");
            this.Add("Whiskey");
            this.Add("Run   ");
            this.Add("Wine");
            this.Add("Pepsi");
            this.Add("Fanta");
            this.Add("Coca-Cola");
            this.Add("Water");

        }


        public List<String> GetListOfDrinks()
        {
            return this;
        }
    }
}
