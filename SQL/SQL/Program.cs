using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SQL
{
    class Program
    {
        private static SqlConnection sqlConnection = null;

        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);

            string connectionString = @"Data Source=LAPTOP-C1RT6KKV;Initial Catalog=usersdb;Integrated Security=True";

            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
            {
                Console.WriteLine("Open!");
            }

            //string sqlComand = "INSERT INTO Product (id, Price, Number, Information) VALUES ('3002','500','10',N'Cable')";
            string sqlComand = "SELECT * FROM Product";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlComand, connection);
                int number = command.ExecuteNonQuery();
                //Console.WriteLine("Добавлено объектов: {0}", number);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3));

                    while (reader.Read()) // построчно считываем данные
                    {
                        object id = reader.GetValue(0);
                        object price = reader.GetValue(1);
                        object num = reader.GetValue(2);
                        object information = reader.GetValue(3);

                        Console.WriteLine("{0} \t{1} \t{2} \t{3}", id, price, num, information);
                    }
                }

                reader.Close();
            }
            Console.Read();
        }
    }
}
