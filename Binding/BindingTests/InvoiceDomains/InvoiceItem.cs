namespace Binding.Tests.InvoiceDomains
{
    public class InvoiceItem
    {
        public int itemNumber { get; private set; }
        public string itemName { get; private set; }
        public double itemValue { get; private set; }

        public InvoiceItem(int itemNumber, string itemName, double itemValue)
        {
            this.itemNumber = itemNumber;
            this.itemName = itemName;
            this.itemValue = itemValue;
        }

        public override string ToString()
        {
            return string.Format("Item Number: {0} Item Name: {1} Item Value: {2:#.##}", this.itemNumber, this.itemName, itemValue);
        }
    }
}
