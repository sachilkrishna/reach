using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Reach.Models
{
    public class OrderModelManager
    {
        public int AddOrder(Order o,  List<ProductModel> lpm)
        {
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("addOrder", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_id", o.CustomerId);
                    cmd.Parameters.AddWithValue("@payment_status_name", o.PaymentStatus);
                    cmd.Parameters.AddWithValue("@payment_mode_name", o.PaymentMode);
                    cmd.Parameters.AddWithValue("@payment_reference", o.PaymentReference);
                    cmd.Parameters.AddWithValue("@payment_amount", o.PaymentAmount);
                    cmd.Parameters.AddWithValue("@order_date", o.OrderDate);
                    cmd.Parameters.AddWithValue("@shipped_date", o.ShippedDate);
                    cmd.Parameters.AddWithValue("@expected_delivery_date", o.ExpectedDeliveryDate);
                    try
                    {
                        cn.Open();
                        o.OrderId = Convert.ToInt32(cmd.ExecuteScalar());

                    }
                    catch (SqlException ex)
                    {

                    }

                    foreach (var pm in lpm)
                    {
                        AddOrderItem(o.OrderId,pm,cn);
                    }

                    if (cn.State == System.Data.ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
            }

            return o.OrderId;
        }

        public void AddOrderItem(int OrderId, ProductModel pm, SqlConnection cn)
        {
            using (SqlCommand cmd2 = new SqlCommand("addOrderItem", cn))
            {
                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@order_id", OrderId);
                cmd2.Parameters.AddWithValue("@product_id", pm.ProductID );
                cmd2.Parameters.AddWithValue("@quantity", pm.RequiredQuantity);
                cmd2.Parameters.AddWithValue("@list_price", pm.Amount);
                cmd2.Parameters.AddWithValue("@discount", 0 );
                cmd2.Parameters.AddWithValue("@order_status_name", "Order Placed" );
                try
                {
                    cmd2.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
            }
        }

        public List<Order> GetAllOrders()
        {
            Order o = null;
            List<Order> lstO = new List<Order>();
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("getAllOrders", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            o = new Order {
                                OrderId = Convert.ToInt32(dr["order_id"]),
                                CustomerId = Convert.ToInt32(dr["customer_id"]),
                                CustomerName = Convert.ToString(dr["customer_name"]),
                                PaymentStatus = Convert.ToString(dr["payment_status_name"]),
                                PaymentMode = Convert.ToString(dr["payment_mode_name"]),
                                PaymentReference = Convert.ToString(dr["payment_reference"]),
                                PaymentAmount = Convert.ToDouble(dr["payment_amount"]),
                                OrderDate = Convert.ToDateTime(dr["order_date"]),
                                ShippedDate = Convert.ToDateTime(dr["shipped_date"]),
                                ExpectedDeliveryDate = Convert.ToDateTime(dr["expected_delivery_date"])
                            };
                            lstO.Add(o);
                        }

                    }
                    catch (SqlException ex)
                    {

                    }
                    if (cn.State == System.Data.ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
            }

            return lstO;
        }

        public List<OrderItem> GetOrderItems()
        {
            OrderItem oi = null;
            List<OrderItem> lstOI = new List<OrderItem>();
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("getAllOrderItems", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            oi = new OrderItem
                            {
                                OrderId = Convert.ToInt32(dr["order_id"]),
                                OrderItemId = Convert.ToInt32(dr["order_item_id"]),
                                ProductId = Convert.ToInt32(dr["product_id"]),
                                ProductName = Convert.ToString(dr["product_name"]),
                                Quantity = Convert.ToInt32(dr["quantity"]),
                                ListPrice = Convert.ToDouble(dr["list_price"]),
                                Discount = Convert.ToDouble(dr["discount"]),
                                OrderStatusId = Convert.ToInt32(dr["order_status_id"]),
                                OrderStatus = Convert.ToString(dr["order_status_name"]),
                            };
                            lstOI.Add(oi);
                        }

                    }
                    catch (SqlException ex)
                    {

                    }
                    if (cn.State == System.Data.ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
            }

            return lstOI;
        }

        public List<OrderItem> GetOrderItems(int? OrderId)
        {
            OrderItem oi = null;
            List<OrderItem> lstOI = new List<OrderItem>();
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("getOrderItemsById", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@order_id", OrderId);

                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            oi = new OrderItem
                            {
                                OrderId = Convert.ToInt32(dr["order_id"]),
                                OrderItemId = Convert.ToInt32(dr["order_item_id"]),
                                ProductId = Convert.ToInt32(dr["product_id"]),
                                ProductName = Convert.ToString(dr["product_name"]),
                                Quantity = Convert.ToInt32(dr["quantity"]),
                                ListPrice = Convert.ToDouble(dr["list_price"]),
                                Discount = Convert.ToDouble(dr["discount"]),
                                OrderStatusId = Convert.ToInt32(dr["order_status_id"]),
                                OrderStatus = Convert.ToString(dr["order_status_name"]),
                            };
                            lstOI.Add(oi);
                        }

                    }
                    catch (SqlException ex)
                    {

                    }
                    if (cn.State == System.Data.ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
            }

            return lstOI;
        }
        public List<Order> GetOrdersByCid( Customer c)
        {
            Order o = null;
            List<Order> lstO = new List<Order>();
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("getOrdersByCid", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_id", c.Customer_ID);
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            o = new Order
                            {
                                OrderId = Convert.ToInt32(dr["order_id"]),
                                CustomerId = Convert.ToInt32(dr["customer_id"]),
                                CustomerName = Convert.ToString(dr["customer_name"]),
                                PaymentStatus = Convert.ToString(dr["payment_status_name"]),
                                PaymentMode = Convert.ToString(dr["payment_mode_name"]),
                                PaymentReference = Convert.ToString(dr["payment_reference"]),
                                PaymentAmount = Convert.ToDouble(dr["payment_amount"]),
                                OrderDate = Convert.ToDateTime(dr["order_date"]),
                                ShippedDate = Convert.ToDateTime(dr["shipped_date"]),
                                ExpectedDeliveryDate = Convert.ToDateTime(dr["expected_delivery_date"])
                            };
                            lstO.Add(o);
                        }

                    }
                    catch (SqlException ex)
                    {

                    }
                    if (cn.State == System.Data.ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
            }

            return lstO;
        }
    }
}