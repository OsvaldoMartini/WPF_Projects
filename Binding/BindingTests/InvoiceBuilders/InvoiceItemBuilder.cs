using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Binding.Different.Ways.Model;
using Binding.Tests.Builders;
using Binding.Tests.InvoiceDomains;

namespace Binding.Tests.InvoiceBuilders
{

    public class InvoiceItemBuilder
    {
        public int itemNumber = 1;
        public string itemName = "IPad";
        public double itemValue = 15.00d;
            
        public InvoiceItem Build()
        {
            return new InvoiceItem(itemNumber, itemName, itemValue);
        }

        public InvoiceItemBuilder WithItemNumber(int itemNumber)    
        {
            this.itemNumber = itemNumber;
            return this;
        }

        public InvoiceItemBuilder WithLastName(string itemName)
        {
            this.itemName = itemName;
            return this;
        }

        public InvoiceItemBuilder WithBirthDate(double itemValue)
        {
            this.itemValue = itemValue;
            return this;
        }

        public static implicit operator InvoiceItem(InvoiceItemBuilder instance)
        {
            return instance.Build();

        }
    }
}
