using ECommerceApplication.Dto;
using ECommerceApplication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderQueries _customerOrderQueries;
        private readonly ILogger<CustomerOrderController> _logger;

        public CustomerOrderController(ICustomerOrderQueries customerOrderQueries, ILogger<CustomerOrderController> logger)
        {
            _customerOrderQueries = customerOrderQueries ?? throw new ArgumentNullException(nameof(customerOrderQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpPost("GetCustomerRecentOrders")]
        public IActionResult GetCustomerRecentOrders(CustomerRecentOrderDto input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }

                var result = _customerOrderQueries.GetCustomerRecentOrders(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetCustomerRecentOrders");
                return BadRequest("Request Cannot be processed.");
            }
        }
    }
}
