using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ADO2
{
    internal class Program
    {
        static SqlConnection conn = null;
        static void Main(string[] args)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString);
            try
            {
                conn.Open();
                Console.WriteLine("Connection Opened");
                string pr = "Select * from Products";
                SqlCommand cmd = new SqlCommand(pr, conn);
                
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(
                        $"Id: {reader["Id"]}, Name: {reader["Name"]}, Count: {reader["Count"]}, Cost: {reader["Cost"]}"
                    );
                }

                reader.Close();

                string ca = "SELECT [Name] FROM Categories";

                SqlCommand cmdCategory = new SqlCommand(ca, conn);

                SqlDataReader readerc = cmdCategory.ExecuteReader();

                while (readerc.Read())
                {
                    Console.WriteLine($"Category: {readerc["Name"]}");
                }

                readerc.Close();

                string su = "SELECT [Name] FROM Suppliers";
                SqlCommand cmdSupplier = new SqlCommand(su, conn);
                SqlDataReader readersu = cmdSupplier.ExecuteReader();

                while (readersu.Read())
                {
                    Console.WriteLine($"Name: {readersu["Name"]}");
                }
                readersu.Close();

                string minc = "Select min([Count]) from Products";
                SqlCommand cmdMinc = new SqlCommand(minc, conn);
                object result = cmdMinc.ExecuteScalar();
                Console.WriteLine($"Minimum count: {result}");

                string minco = "Select min([Cost]) from Products";
                SqlCommand cmdMinco = new SqlCommand(minco, conn);
                object resultco = cmdMinco.ExecuteScalar();
                Console.WriteLine($"Minimum cost: {resultco}");

                string maxco = "Select max([Cost]) from Products";
                SqlCommand cmdMaxco = new SqlCommand(maxco, conn);
                object resultMaxco = cmdMaxco.ExecuteScalar();
                Console.WriteLine($"Max cost: {resultMaxco}");

                string pco = "select * from Products as p Join Categories as c on p.CategoryId = c.Id where c.Name = @p1";
                SqlCommand cmdPco = new SqlCommand(pco, conn);
                cmdPco.Parameters.Add("@p1", System.Data.SqlDbType.NVarChar).Value = "Electronics";
                SqlDataReader readerPco = cmdPco.ExecuteReader();

                while (readerPco.Read()) {
                    Console.WriteLine(
                    $"Id: {readerPco["Id"]}, Name: {readerPco["Name"]}, Count: {readerPco["Count"]}, Cost: {readerPco["Cost"]}");
                }

                readerPco.Close();
                
                string psu = "SELECT * FROM Products AS p JOIN Suppliers AS s on p.SupplierId = s.Id Where s.Name = @p2";
                SqlCommand cmdPsu = new SqlCommand(psu, conn);
                cmdPsu.Parameters.Add("@p2", System.Data.SqlDbType.NVarChar).Value = "Adidas Group";
                SqlDataReader readerPsu = cmdPsu.ExecuteReader();

                while (readerPsu.Read()) {
                    Console.WriteLine(
                        $"Id: {readerPsu["Id"]}, Name: {readerPsu["Name"]}, Count: {readerPsu["Count"]}, Cost: {readerPsu["Cost"]}");
                }

                readerPsu.Close();

                string minDi = "SELECT MIN(DeliveryDate) FROM Products";
                SqlCommand cmdDi = new SqlCommand(minDi, conn);
                object resultDi = cmdDi.ExecuteScalar();
                Console.WriteLine($"longst DeliveryDate: {resultDi}");

                string AvgCo = "SELECT c.Name, AVG(p.Count) AS ProductCount " +
              "FROM Products AS p " +
              "JOIN Categories AS c ON c.Id = p.CategoryId " +
              "GROUP BY c.Id, c.Name";

                SqlCommand cmdAvgCo = new SqlCommand(AvgCo, conn);
                SqlDataReader readercmdAvgCo = cmdAvgCo.ExecuteReader();

                while (readercmdAvgCo.Read())
                {
                    Console.WriteLine($"{readercmdAvgCo["Name"]}: {readercmdAvgCo["ProductCount"]}");
                }

                readercmdAvgCo.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn?.Close();
            }


        }
    }
}

/*
 CREATE DATABASE Storage 
go

Use Storage
go

CREATE Table Categories
(
Id int not null identity(1,1) primary key,
[Name] nvarchar(100) check([Name] <> '') unique
)

CREATE Table Suppliers
(
Id int not null identity(1,1) primary key,
[Name] nvarchar(100) check([Name] <> '') unique
)

CREATE Table [Product]
(
Id int not null identity(1,1) primary key,
[Name] nvarchar(100) check([Name] <> ''),
DeliveryDate Date, CHECK(DeliveryDate <= GETDATE()),
[Count] decimal not null check([Count] >= 0),
CategoryId int not null,
foreign key (CategoryId) references Categories(Id),
SupplierId int not null,
foreign key (SupplierId) references Suppliers(Id),
)
 */