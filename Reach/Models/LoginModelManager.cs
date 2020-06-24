using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Reach.Models
{
    public class LoginModelManager
    {
        public Customer RegisterCustomer(Customer c)
        {
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("registerCustomer", cn))
                {
                    dynamic Password = null;
                    dynamic AltPhone = null;
                    dynamic AltEmail = null;
                    if (c.CustomerType == "Guest")
                    {
                        Password = DBNull.Value;
                    }
                    else
                    {
                        Password = c.Password;
                    }
                    if (String.IsNullOrWhiteSpace(c.AltPhone))
                    {
                        AltPhone = DBNull.Value;
                    }
                    else
                    {
                        AltPhone = c.AltPhone;
                    }
                    if (String.IsNullOrWhiteSpace(c.AltEmail))
                    {
                        AltEmail = DBNull.Value;
                    }
                    else
                    {
                        AltEmail = c.AltEmail;
                    }
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_type_name", c.CustomerType);
                    cmd.Parameters.AddWithValue("@password", Password);
                    cmd.Parameters.AddWithValue("@customer_fname", c.Fname);
                    cmd.Parameters.AddWithValue("@customer_lname", c.Lname);
                    cmd.Parameters.AddWithValue("@primary_phone", c.Phone);
                    cmd.Parameters.AddWithValue("@secondary_phone", AltPhone);
                    cmd.Parameters.AddWithValue("@primary_email", c.Email);
                    cmd.Parameters.AddWithValue("@secondary_email", AltEmail);
                    cmd.Parameters.AddWithValue("@address", c.Address);
                    cmd.Parameters.AddWithValue("@city", c.City);
                    cmd.Parameters.AddWithValue("@state", c.State);
                    cmd.Parameters.AddWithValue("@pincode", c.Pincode);
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            c.Customer_ID = Convert.ToInt32(dr["customer_id"]);
                            c.ShippingAddressId = Convert.ToInt32(dr["shipping_address"]);
                        }
                    }
                    catch (SqlException ex)
                    {

                        throw;
                    }
                    if (cn.State == System.Data.ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
            }
            return c;
        }

        public int VerifyLogin(string Password, string EmailID)
        {
            int Cid = 0 ;
            //int status = 0;
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("verifyLogin", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@password", Password);
                    cmd.Parameters.AddWithValue("@login_email", EmailID);
                    try
                    {
                        cn.Open();
                        Cid=Convert.ToInt32(cmd.ExecuteScalar());

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
            return Cid;
        }

        public bool VerifyRegisteredEmail(string EmailID)
        {
            var status = false;
            //int status = 0;
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("verifyRegisteredEmail", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@login_email", EmailID);
                    try
                    {
                        cn.Open();
                        status = Convert.ToBoolean(cmd.ExecuteScalar());

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
            return status;
        }

        public Customer GetCustomerById(Customer c)
        {

            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("getCustomerById", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_id", c.Customer_ID);

                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            c.CustomerType = Convert.ToString(dr["customer_type_name"]);
                            c.Fname = Convert.ToString(dr["customer_fname"]);
                            c.Lname = Convert.ToString(dr["customer_lname"]);
                            c.Phone = Convert.ToString(dr["primary_phone"]);
                            c.AltPhone = Convert.ToString(dr["secondary_phone"]);
                            c.Email = Convert.ToString(dr["primary_email"]);
                            c.AltEmail = Convert.ToString(dr["secondary_email"]);
                            c.Address = Convert.ToString(dr["address"]);
                            c.City = Convert.ToString(dr["city"]);
                            c.State = Convert.ToString(dr["state"]);
                            c.Pincode = Convert.ToString(dr["pincode"]);
                            c.ShippingAddressId = Convert.ToInt32(dr["shipping_address"]);
                        }
                    }
                    catch (SqlException ex)
                    {

                        
                    }
                    finally
                    {
                        if (cn.State==System.Data.ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }


                }
            }
            return c;
        }
    }


}