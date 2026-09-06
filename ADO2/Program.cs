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

                Console.WriteLine("-------------Homework-------------");

                //-------------Homework-------------
                //string instp = "INSERT INTO Products (Name, Count, Cost, CategoryId,SupplierId) VALUES ('Whatches', 10, 99.99, 1,1);";
                //SqlCommand cmdinstp = new SqlCommand(instp, conn);
                //int RowsAffacted = cmdinstp.ExecuteNonQuery();

                //Console.WriteLine(RowsAffacted);

                //string instc = "INSERT INTO Categories (Name) VALUES ('Toys');";
                //SqlCommand cmdinstc = new SqlCommand(instc, conn);
                //int RowsAffacted2 = cmdinstc.ExecuteNonQuery();

                //Console.WriteLine(RowsAffacted2);

                //string insts = "INSERT INTO Suppliers (Name) VALUES ('Pop Markt');";
                //SqlCommand cmdinsts = new SqlCommand(insts, conn);
                //int RowsAffacted3 = cmdinsts.ExecuteNonQuery();

                //Console.WriteLine(RowsAffacted3);

                string uppr = "UPDATE Products SET Name = 'Glasses' WHERE Id = @p3";
                SqlCommand cmduppr = new SqlCommand(uppr, conn);
                cmduppr.Parameters.Add("@p3", System.Data.SqlDbType.Int).Value = 8;
                int RowsAffacteduppr = cmduppr.ExecuteNonQuery();

                Console.WriteLine(RowsAffacteduppr);

                string upsu = "UPDATE Suppliers SET Name = 'Apple' WHERE Id = @p4";
                SqlCommand cmdupsu = new SqlCommand(upsu, conn);
                cmdupsu.Parameters.Add("@p4", System.Data.SqlDbType.Int).Value = 7;
                int RowsAffactedupsu = cmdupsu.ExecuteNonQuery();

                Console.WriteLine(RowsAffactedupsu);

                string upca = "UPDATE Categories SET Name = 'Phones' WHERE Id = @p5";
                SqlCommand cmdca = new SqlCommand(upca, conn);
                cmdca.Parameters.Add("@p5", System.Data.SqlDbType.Int).Value = 9;
                int RowsAffactedca = cmdca.ExecuteNonQuery();

                Console.WriteLine(RowsAffactedca);

                string delpr = "DELETE FROM Products WHERE Id = @p6";
                SqlCommand cmddelpr = new SqlCommand(delpr, conn);
                cmddelpr.Parameters.Add("@p6", System.Data.SqlDbType.Int).Value = 8;

                int RowsAffacteddelpr = cmddelpr.ExecuteNonQuery();

                Console.WriteLine(RowsAffacteddelpr);

                string delsu = "DELETE FROM Suppliers WHERE Id = @p7";
                SqlCommand cmddelsu= new SqlCommand(delsu, conn);
                cmddelsu.Parameters.Add("@p7", System.Data.SqlDbType.Int).Value = 7;

                int RowsAffacteddelsu = cmddelsu.ExecuteNonQuery();

                Console.WriteLine(RowsAffacteddelsu);

                string delsca = "DELETE FROM Categories WHERE Id = @p8";
                SqlCommand cmddelsca = new SqlCommand(delsca, conn);
                cmddelsca.Parameters.Add("@p8", System.Data.SqlDbType.Int).Value = 9;

                int RowsAffacteddelsca = cmddelsca.ExecuteNonQuery();

                Console.WriteLine(RowsAffacteddelsca);

                string maxSu = "SELECT TOP 1 s.Name, SUM(p.Count) AS ProductCount " +
              "FROM Products AS p " +
              "JOIN Suppliers AS s ON p.SupplierId = s.Id " +
              "GROUP BY s.Id, s.Name " +
              "ORDER BY SUM(p.Count) DESC";

                SqlCommand cmdMaxSu = new SqlCommand(maxSu, conn);
                SqlDataReader readerMaxSu = cmdMaxSu.ExecuteReader();

                while (readerMaxSu.Read())
                {
                    Console.WriteLine(
                        $"Supplier: {readerMaxSu["Name"]}, Count: {readerMaxSu["ProductCount"]}");
                }

                readerMaxSu.Close();

                string minSu = "SELECT TOP 1 s.Name, SUM(p.Count) AS ProductCount " +
               "FROM Products AS p " +
               "JOIN Suppliers AS s ON p.SupplierId = s.Id " +
               "GROUP BY s.Id, s.Name " +
               "ORDER BY SUM(p.Count) ASC";

                SqlCommand cmdMinSu = new SqlCommand(minSu, conn);
                SqlDataReader readerMinSu = cmdMinSu.ExecuteReader();

                while (readerMinSu.Read())
                {
                    Console.WriteLine(
                        $"Supplier: {readerMinSu["Name"]}, Count: {readerMinSu["ProductCount"]}");
                }

                readerMinSu.Close();


                string maxCa = "SELECT TOP 1 c.Name, SUM(p.Count) AS ProductCount " +
              "FROM Products AS p " +
              "JOIN Categories AS c ON p.CategoryId = c.Id " +
              "GROUP BY c.Id, c.Name " +
              "ORDER BY SUM(p.Count) DESC";

                SqlCommand cmdMaxCa = new SqlCommand(maxCa, conn);
                SqlDataReader readerMaxCa = cmdMaxCa.ExecuteReader();

                while (readerMaxCa.Read())
                {
                    Console.WriteLine(
                        $"Category: {readerMaxCa["Name"]}, Count: {readerMaxCa["ProductCount"]}");
                }

                readerMaxCa.Close();

                string minCa = "SELECT TOP 1 c.Name, SUM(p.Count) AS ProductCount " +
              "FROM Products AS p " +
              "JOIN Categories AS c ON p.CategoryId = c.Id " +
              "GROUP BY c.Id, c.Name " +
              "ORDER BY SUM(p.Count) ASC";

                SqlCommand cmdMinCa = new SqlCommand(minCa, conn);
                SqlDataReader readerMinCa = cmdMinCa.ExecuteReader();

                while (readerMinCa.Read())
                {
                    Console.WriteLine(
                        $"Category: {readerMinCa["Name"]}, Count: {readerMinCa["ProductCount"]}");
                }

                readerMinCa.Close();

                Console.Write("Enter number of days: ");
                int days = Convert.ToInt32(Console.ReadLine());

                string oldPr = "SELECT * FROM Products " +
                               "WHERE DeliveryDate <= DATEADD(DAY, -@days, GETDATE())";

                SqlCommand cmdOldPr = new SqlCommand(oldPr, conn);
                cmdOldPr.Parameters.Add("@days", System.Data.SqlDbType.Int).Value = days;

                SqlDataReader readerOldPr = cmdOldPr.ExecuteReader();

                while (readerOldPr.Read())
                {
                    Console.WriteLine(
                        $"Id: {readerOldPr["Id"]}, " +
                        $"Name: {readerOldPr["Name"]}, " +
                        $"Count: {readerOldPr["Count"]}, " +
                        $"Cost: {readerOldPr["Cost"]}, " +
                        $"DeliveryDate: {readerOldPr["DeliveryDate"]}");
                }

                readerOldPr.Close();


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