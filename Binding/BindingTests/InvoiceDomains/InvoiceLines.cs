using System.Collections.Generic;

namespace Binding.Tests.InvoiceDomains
{
    public class InvoiceLines
    {
        public List<InvoiceItem> InvoiceList { get; private set; }
        public InvoiceLines(List<InvoiceItem> invoiceList)
        {
            this.InvoiceList = invoiceList;
        }

        public void AddItem(InvoiceItem invoiceItem)
        {
            this.InvoiceList.Add(invoiceItem);
        }

        public void RemoveItem(InvoiceItem invoiceItem)
        {
            this.InvoiceList.RemoveAt(this.InvoiceList.IndexOf(invoiceItem));
        }

        public void GetItem(InvoiceItem invoiceItem)
        {
            this.InvoiceList.IndexOf(invoiceItem);
        }


        public List<InvoiceItem> GetInvoceList()
        {
            return this.InvoiceList;
        }


    }
}
