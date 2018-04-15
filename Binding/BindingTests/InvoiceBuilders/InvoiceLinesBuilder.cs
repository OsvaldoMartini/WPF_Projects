using System.Collections.Generic;
using Binding.Tests.Builders;
using Binding.Tests.InvoiceDomains;

namespace Binding.Tests.InvoiceBuilders
{
    public class InvoiceLinesBuilder
    {
        private List<InvoiceItem> _InvoiceList  = new List<InvoiceItem>
        {
            new InvoiceItemBuilder { itemNumber=1,itemName="IPad",itemValue=20.00d},
            new InvoiceItemBuilder { itemNumber=2,itemName="Stand for IPad",itemValue=15.00d},
            new InvoiceItemBuilder { itemNumber=3,itemName="LapTop Asus",itemValue=505.00d}
        };

        public InvoiceLines Build()
        {
            return new InvoiceLines(this._InvoiceList);
        }

        public InvoiceLinesBuilder WithInvoiceLines(List<InvoiceItem> invoiceList)
        {
            this._InvoiceList = invoiceList;
            return this;
        }


        public static implicit operator InvoiceLines(InvoiceLinesBuilder instance)
        {
            return instance.Build();
        }


    }
}
