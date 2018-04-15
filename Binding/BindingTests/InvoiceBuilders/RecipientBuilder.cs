using System;
using Binding.Tests.Builders;
using Binding.Tests.InvoiceDomains;

namespace Binding.Tests.InvoiceBuilders
{
    class RecipientBuilder
    {
        private int id = 1;
        private string FirstName = "Osvaldo";
        private string LastName = "Martini";
        public Recipient Build()
        {
            return new Recipient(this.id, this.FirstName, this.LastName);
        }
        public RecipientBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public RecipientBuilder WithFirstName(string firstname)
        {
            this.FirstName = firstname;
            return this;
        }

        public RecipientBuilder WithLastName(string lastname)
        {
            this.LastName = lastname;
            return this;
        }

        public static implicit operator Recipient(RecipientBuilder instance)
        {
            return instance.Build();
        }
    }
}
