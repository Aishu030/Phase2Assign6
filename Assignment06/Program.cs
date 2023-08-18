using System;
using System.Data.SqlClient;
using System.Data;

namespace Assignment06
{
    internal class Program
    {
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader reader;
        static string conStr = "server=LAPTOP-EA5C4MP1;database=Assignment6Db; trusted_connection = true;";

        static void View()
        {
            cmd = new SqlCommand("select * from Products", con);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine($" Product ID: {reader["ProductId"]}\n Name: {reader["ProductName"]}\n Price: {reader["Price"]}\n Quantity: {reader["Quantity"]}\n MFDate: {reader["MFDate"]}\n ExpDate: {reader["ExpDate"]}\n");
                Console.WriteLine("-------------------------------");
            }

            reader.Close();
        }
        static void Add()
        {
            cmd = new SqlCommand("insert into Products (ProductId,ProductName, Price, Quantity, MfDate, ExpDate) values (@pid,@pn, @pp, @Qty, @mfd, @exp)", con);
            Console.WriteLine("Enter Product Id: ");
            cmd.Parameters.AddWithValue("@pid", int.Parse(Console.ReadLine()));
            Console.Write("Enter Product Name: ");
            cmd.Parameters.AddWithValue("@pn", Console.ReadLine());
            Console.Write("Enter Price: ");
            cmd.Parameters.AddWithValue("@pp", decimal.Parse(Console.ReadLine()));
            Console.Write("Enter Quantity: ");
            cmd.Parameters.AddWithValue("@Qty", int.Parse(Console.ReadLine()));
            Console.Write("Enter Manufacturing Date (yyyy-mm-dd): ");
            cmd.Parameters.AddWithValue("@mfd", DateTime.Parse(Console.ReadLine()));
            Console.Write("Enter Expiry Date (yyyy-mm-dd): ");
            cmd.Parameters.AddWithValue("@exp", DateTime.Parse(Console.ReadLine()));
            int nor = cmd.ExecuteNonQuery();
            if (nor >=1)
            {
                Console.WriteLine("Product inserted!!!");
            }
        }
        static void Update()
        {
            cmd = new SqlCommand("update Products set Quantity = @NewQty where ProductId = @PId", con);
            Console.Write("Enter Product ID to update quantity: ");
            cmd.Parameters.AddWithValue("@PId", int.Parse(Console.ReadLine()));
            Console.Write("Enter New Quantity: ");
            cmd.Parameters.AddWithValue("NewQty", int.Parse(Console.ReadLine()));
            int nor = cmd.ExecuteNonQuery();
            if (nor >=1)
            {
                Console.WriteLine("Product quantity updated successfully!!!!");
            }
            else
            {
                Console.WriteLine("Product quantity update failed");
            }
        }

        static void Remove()
        {
            cmd = new SqlCommand("delete from Products where ProductId = @PId", con);
            Console.Write("Enter Product ID to remove: ");
            cmd.Parameters.AddWithValue("@PId", int.Parse(Console.ReadLine()));
            int nor = cmd.ExecuteNonQuery();
            if (nor > 0)
            {
                Console.WriteLine("Product Deleted!!!");
            }
            else
            {
                Console.WriteLine("Product removal failed");
            }
        }

        static void Main(string[] args)
        {
            try
            {
                con = new SqlConnection(conStr);
                con.Open();
                while (true)
                {
                    Console.WriteLine("Select the operation you want to perform\n");
                    Console.WriteLine("1. View Products\n2. Add New Product\n3. Update Product Quantity\n4. Remove Product\n5. Exit");
                    int op = int.Parse(Console.ReadLine());

                    switch (op)
                    {
                        case 1:
                            View();
                            break;
                        case 2:
                            Add();
                            break;
                        case 3:
                            Update();
                            break;
                        case 4:
                            Remove();
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("Invalid operation choice!!!");
                            break;
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Error!!!" + ex.Message); }
            finally
            {
                con.Close();
                Console.ReadKey();

            }
        }
    }
}
