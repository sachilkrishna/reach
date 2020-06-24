using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Reach.Models
{
    public class CustomerModelManager
    {
        public List<AddressModel> GetAvailableAddresses(int Cid)
        {

            AddressModel a = null;  
            List<AddressModel> lstC = new List<AddressModel>();

            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("fetchAvailableAddress", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_id", Cid);

                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            a = new AddressModel();
                            a.AddressId = Convert.ToInt32(dr["address_id"]);
                            a.Fname = Convert.ToString(dr["fname"]);
                            a.Lname = Convert.ToString(dr["lname"]);
                            a.Address = Convert.ToString(dr["address"]);
                            a.City = Convert.ToString(dr["city"]);
                            a.State = Convert.ToString(dr["state"]);
                            a.Pincode = Convert.ToString(dr["pincode"]);
                            lstC.Add(a);
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
            return lstC;
        }

        public void UpdateShippingAddress(Customer c)
        {
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("updateShippingAddress", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@shipping_address_id", c.ShippingAddressId);
                    cmd.Parameters.AddWithValue("@customer_id", c.Customer_ID);

                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
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
        }

        public void AddNewAddress(int Cid, AddressModel a)
        {

            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("addAddress", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_id", Cid);
                    cmd.Parameters.AddWithValue("@customer_fname", a.Fname);
                    cmd.Parameters.AddWithValue("@customer_lname", a.Lname);
                    cmd.Parameters.AddWithValue("@address", a.Address);
                    cmd.Parameters.AddWithValue("@city", a.City);
                    cmd.Parameters.AddWithValue("@state", a.State);
                    cmd.Parameters.AddWithValue("@pincode", a.Pincode);

                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
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
        }
        public void RemoveAddressById(int Aid)
        {

            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("removeAddress", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@address_id", Aid);


                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
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
        }
        public AddressModel FetchAddressById(int Aid)
        {

            AddressModel a = new AddressModel();
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("fetchAddressById", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@address_id", Aid);

                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            a.AddressId = Convert.ToInt32(dr["address_id"]);
                            a.Fname = Convert.ToString(dr["fname"]);
                            a.Lname = Convert.ToString(dr["lname"]);
                            a.Address = Convert.ToString(dr["address"]);
                            a.City = Convert.ToString(dr["city"]);
                            a.State = Convert.ToString(dr["state"]);
                            a.Pincode = Convert.ToString(dr["pincode"]);
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
            return a;
        }

        public void AddPidToWishList(int Cid,int Pid)
        {
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd= new SqlCommand("addToWishList", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_id", Cid );
                    cmd.Parameters.AddWithValue("@product_id", Pid);
                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();

                    }
                    catch (SqlException ex)
                    {

                        throw;
                    }
                    if (cn.State==System.Data.ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
            }
        }

        public void ShowWishList(int Cid)
        {
            List<string> LstPid = new List<string>();
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS; initial catalog=reach;integrated security=sspi;"))
            {
                using (SqlCommand cmd = new SqlCommand("showWishList", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_id", Cid);

                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            LstPid.Add("product_id");
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
        }
    }

}