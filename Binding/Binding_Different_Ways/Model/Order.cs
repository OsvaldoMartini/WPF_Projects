using System.Collections.Generic;

namespace Binding.Different.Ways.Model
{
    public class Order
    {
        public int orderID { get; set; }
        public Customer customer { get; set; }


        public List<OrderItem> items { get; set; }
    }
}
