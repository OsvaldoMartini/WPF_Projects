using System;

namespace Binding.Tests.InvoiceDomains
{
    public class Invoice
    {

        public double invoiceTotal { get;private set; }
        public Recipient recipient { get; private set; }
        public InvoiceLines lines { get; private set; }
        public PoundsShillingsPence discount{ get; private set; }
        public Invoice(Recipient recipient, InvoiceLines lines, PoundsShillingsPence discount)
        {
            this.recipient = recipient;
            this.lines = lines;
            this.discount = discount;
        }

        public double getTotalPayment(double paymentTotal)
        {
            double sum = 0;

            if (discount == PoundsShillingsPence.ZERO)
            { 
                foreach (InvoiceItem item in lines.GetInvoceList())
                {
                    sum += item.itemValue;

                }
            } else
            {
                foreach (InvoiceItem item in lines.GetInvoceList())
                {
                    sum += item.itemValue;
                }

                sum = GetPercentageDiscount(sum, (double)discount);

            }
            return sum;
        }

        public double GetPercentageDiscount(double value, double percentage)
        {
            double percent = (value * percentage) / 100.0;

            return value - percent;
        }

        public double GetPercentFromTwoNumber(double valid, double total)
        {
            // We want to have 92.9% from these two numbers.
            //int valid = 92;
            //int total = 99;

            // First multiply top by 100 then divide.
            double percent = (double) (valid * 100) / total; // <-- Use cast

            // This is the percent number.
            //return percent; //92.9292929292929
            //return Math.Floor(percent); //92
            //return Math.Ceiling(percent); //93
            return Math.Round(percent, 1); //92.9
        }


    }
}
