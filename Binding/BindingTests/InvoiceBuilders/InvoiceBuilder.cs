using System.Dynamic;
using Binding.Tests.InvoiceBuilders;
using Binding.Tests.InvoiceDomains;

namespace Binding.Tests.Builders
{
    public class InvoiceBuilder
    {
        private Recipient recipient = null;
        private InvoiceLines lines = null;
        PoundsShillingsPence discount = PoundsShillingsPence.ZERO;

        public Invoice Build()
        {
            return new Invoice(recipient, lines, discount);
        }
        public InvoiceBuilder WithRecipient(Recipient recipient)
        {
            this.recipient = recipient;
            return this;
        }

        public InvoiceBuilder WithDiscount(PoundsShillingsPence discount)
        {
            this.discount = discount;
            return this;
        }

        public InvoiceBuilder WithRecipient()
        {
            this.recipient = new RecipientBuilder().Build();
            return this;
        }
        public InvoiceBuilder WithInvoiceLines()
        {
            this.lines = new InvoiceLinesBuilder().Build();
            return this;
        }

        public static implicit operator Invoice(InvoiceBuilder instance)
        {
            return instance.Build();
        }

    }
}
