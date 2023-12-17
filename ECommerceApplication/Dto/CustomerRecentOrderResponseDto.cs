using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Dto
{
    public class CustomerRecentOrderResponseDto
    {
        public CustomerDto? Customer { get; set; }

        public OrderDto? Order { get; set; }
        //public IEnumerable<OrderItemDto>? OrderItems { get; set; }
    }
}
