using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Reach.Models
{
    public class ProductModelManager
    {
        //public ProductModel GetProduct()
        //{
 
        //    ProductModel pm = new ProductModel();
        //    pm.ProductID = "1";
        //    pm.ProductName = "Nike Shoe1";
        //    pm.DescriptionShort = "Very Good Shoes";
        //    pm.DescriptionLong = "it is from the manufacturers of one among the best shoes.just take it ";
        //    pm.Rate = 1000.55;
        //    pm.Images = new string[] { "https://image.cnbcfm.com/api/v1/image/105680013-1547583426762nike1.jpg?v=1547583682&w=678&h=381", "https://images-na.ssl-images-amazon.com/images/I/71iTHVTtD%2BL._AC_UL1500_.jpg" };
        //    pm.RemainingQuantity = 12;
        //    return pm;
        //}

        //public ProductModel GetProductByKey(string Category, string Key)
        //{
        //    ProductModel pm = new ProductModel();
        //    List<string> categories = FetchCategory();
        //    if (categories.Contains(Category))
        //    {
        //        if (Category==categories[0]| Category == categories[2])
        //        {
        //            if (Key.Contains("nike") || Key.Contains( "1"))
        //            {
        //                pm.ProductID = "1";
        //                pm.ProductName = "Nike Shoe1";
        //                pm.DescriptionShort = "Very Good Shoes";
        //                pm.DescriptionLong = "it is from the manufacturers of one among the best shoes.just take it ";
        //                pm.Rate = 1000.55;
        //                pm.Images = new string[] { "https://image.cnbcfm.com/api/v1/image/105680013-1547583426762nike1.jpg?v=1547583682&w=678&h=381", "https://images-na.ssl-images-amazon.com/images/I/71iTHVTtD%2BL._AC_UL1500_.jpg" };
        //                pm.RemainingQuantity = 12;

        //            }
        //            else if (Key.Contains("adidas") || Key.Contains("2"))
        //            {
        //                pm.ProductID = "2";
        //                pm.ProductName = "Adidas Shoe1";
        //                pm.DescriptionShort = "All Day I Dream About Sports";
        //                pm.DescriptionLong = "it is from the manufacturers of one among the best shoes. Stop dreaming about it ";
        //                pm.Rate = 14000.55;
        //                pm.Images = new string[] { "https://n1.sdlcdn.com/imgs/i/z/e/Adidas-Gray-Basketball-Shoes-SDL861211348-1-fc9c8.jpg", "https://www-konga-com-res.cloudinary.com/w_auto,f_auto,fl_lossy,dpr_auto,q_auto/media/catalog/product/M/A/158276_1570261338.jpg" };
        //                pm.RemainingQuantity = 0;
        //            }
        //            else if (Key.Contains("puma") || Key.Contains("3"))
        //            {
        //                pm.ProductID = "3";
        //                pm.ProductName = "Puma Shoe1";
        //                pm.DescriptionShort = "Run the Streets";
        //                pm.DescriptionLong = "it is from the manufacturers of one among the best shoes. Shop all the latest gear from the world of Fashion, Sport, and everywhere in between. ";
        //                pm.Rate = 999.99;
        //                pm.Images = new string[] { "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/puma-hybrid-runner-fusefit-mens-1196-tested-1549051028.jpg?crop=0.844xw:0.765xh;0.0204xw,0.0842xh&resize=480:*", "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/puma-hybrid-runner-fusefit-womens-1220-1549051048.jpg" };
        //                pm.RemainingQuantity = 6;
        //            }
        //        }
        //    }
        //    return pm;
        //}

        public List<ProductModel> GetProductByKeyword(string Keyword, string Category)
        {
            List<ProductModel> lstPm = new List<ProductModel>();
            ProductModel pm = null;
            if (String.IsNullOrEmpty(Category))
            {
                Category = "All";
            }
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS;initial catalog=reach;integrated security=sspi"))
            {
                using (SqlCommand cmd = new SqlCommand("getProductByKeyword", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@keyword", Keyword.Trim());
                    cmd.Parameters.AddWithValue("@category",Category);
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            pm = new ProductModel
                            {
                                ProductID = Convert.ToString(dr["product_id"]),
                                ProductName = Convert.ToString(dr["product_name"]),
                                BrandName = Convert.ToString(dr["brand_name"]),
                                CategoryName = Convert.ToString(dr["category_name"]),
                                DescriptionShort = Convert.ToString(dr["product_short_decription"]),
                                DescriptionLong = Convert.ToString(dr["product_description"]),
                                RemainingQuantity = Convert.ToInt32(dr["remaining_quantity"]),
                                Rate = Convert.ToDouble(dr["selling_price"])
                            };
                            pm.Images = Convert.ToString(dr["product_images"]);
                            lstPm.Add(pm);
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                    finally
                    {
                        if (cn.State == System.Data.ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }  
                }
            }
            return lstPm;
        }

        public ProductModel GetProductByProductID(string PId)
        {
            ProductModel pm = null;
            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS;initial catalog=reach;integrated security=sspi"))
            {
                using (SqlCommand cmd = new SqlCommand("getProductByProductID", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@product_id", Convert.ToInt32(PId));
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            pm = new ProductModel
                            {
                                ProductID = Convert.ToString(dr["product_id"]),
                                ProductName = Convert.ToString(dr["product_name"]),
                                BrandName = Convert.ToString(dr["brand_name"]),
                                CategoryName = Convert.ToString(dr["category_name"]),
                                DescriptionShort = Convert.ToString(dr["product_short_decription"]),
                                DescriptionLong = Convert.ToString(dr["product_description"]),
                                RemainingQuantity = Convert.ToInt32(dr["remaining_quantity"]),
                                Rate = Convert.ToDouble(dr["selling_price"])
                            };
                            pm.Images = Convert.ToString(dr["product_images"]);

                        }
                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                    finally
                    {
                        if (cn.State == System.Data.ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return pm;
        }

        public List<string> FetchCategory()
        {
            List<string> categories = new List<string>();

            using (SqlConnection cn = new SqlConnection("data source=ADMIN-PC\\SQLEXPRESS;initial catalog=reach;integrated security=sspi"))
            {
                using (SqlCommand cmd = new SqlCommand("fetchCategory", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                           categories.Add(Convert.ToString(dr["category_name"]));
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                    finally
                    {
                        if (cn.State == System.Data.ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return categories;
        }
    }
    
}

