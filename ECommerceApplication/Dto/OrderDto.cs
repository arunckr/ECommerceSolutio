using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Dto
{
    public class OrderDto
    {
        public OrderDto()
        {
            OrderItem = new List<OrderItemDto>();
        }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string? DeliveryAddress { get; set; }
        public List<OrderItemDto>? OrderItem { get; set; }
        public DateTime DeliveryExpected { get; set; }
    }
}
