using Binding.Tests.InvoiceBuilders;
using Binding.Tests.InvoiceDomains;

namespace Binding.Tests.Builders
{
    public class InvoiceBuilder
    {
        Recipient recipient = new RecipientBuilder().Build();
        InvoiceLines lines = new InvoiceLinesBuilder().Build();
        PoundsShillingsPence discount = PoundsShillingsPence.ZERO;

        public Invoice Build()
        {
            return new Invoice(recipient, lines, discount);
        }
        public InvoiceBuilder withRecipient(Recipient recipient)
        {
            this.recipient = recipient;
            return this;
        }

        public InvoiceBuilder withInvoiceLines(InvoiceLines lines)
        {
            this.lines = lines;
            return this;
        }

        public InvoiceBuilder withDiscount(PoundsShillingsPence discount)
        {
            this.discount = discount;
            return this;
        }

        public static implicit operator Invoice(InvoiceBuilder instance)
        {
            return instance.Build();
        }

    }
}
