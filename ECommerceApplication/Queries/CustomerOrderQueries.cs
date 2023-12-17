using Dapper;
using ECommerceApplication.Dto;
using ECommerceApplication.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Queries
{
    public class CustomerOrderQueries : ICustomerOrderQueries
    {
        private const string ConnectionStringName = "DbConnection";
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerOrderQueries> _logger; 

        public CustomerOrderQueries(IConfiguration configuration, ILogger<CustomerOrderQueries> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public CustomerRecentOrderResponseDto GetCustomerRecentOrders(CustomerRecentOrderDto input)
        {

            CustomerRecentOrderResponseDto response = new();

            try
            {
                string connectionString = _configuration.GetConnectionString(ConnectionStringName);
                DataTable dt;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand sqlComm = new SqlCommand("RecentOrderDetails", conn))
                    {
                        sqlComm.Parameters.AddWithValue("@User", input.Email);
                        sqlComm.Parameters.AddWithValue("@CustomerId", input.CustomerId);
                        sqlComm.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                           
                        }
                        if (dt.Rows.Count > 0)
                        {
                            int LastOrderNumber = (int)dt.Rows[0]["OrderNumber"];

                            response.Customer = new CustomerDto()
                            {
                                FirstName = dt.Rows[0]["FirstName"].ToString(),
                                LastName = dt.Rows[0]["LastName"].ToString()
                            };

                            response.Order = new OrderDto()
                            {
                                DeliveryAddress = dt.Rows[0]["DeliveryAddress"].ToString(),
                                DeliveryExpected = (DateTime)dt.Rows[0]["DeliveryExpected"],
                                OrderDate = (DateTime)dt.Rows[0]["OrderDate"],
                                OrderNumber = (int)dt.Rows[0]["OrderNumber"],
                                OrderItem = GetRecentOrderItems(LastOrderNumber)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
                throw;
            }

            return response;
        }

        private List<OrderItemDto> GetRecentOrderItems(int lastOrderId)
        {
            DataTable dt;
            List<OrderItemDto> ordersItems = new();

            string connectionString = _configuration.GetConnectionString(ConnectionStringName);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand sqlComm = new SqlCommand("spRecentOrderItemDetails", conn))
                {
                    sqlComm.Parameters.AddWithValue("@LastOrderId", lastOrderId);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                    {
                        dt = new DataTable();
                        da.Fill(dt);
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    OrderItemDto orderItemModel = new()
                    {
                        Price = row.Field<decimal>("PriceEach"),
                        ProductName = row.Field<string>("Product"),
                        Quantity = row.Field<int>("Quantity")
                    };
                    ordersItems.Add(orderItemModel);
                }
            }
            return ordersItems;
        }


     
       
    }
}

