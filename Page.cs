
using System;
using System.Data;
using MySql.Data.MySqlClient;

public class Test
{
   public static void showTb(IDbConnection dbcon){
      IDbCommand dbcmd = dbcon.CreateCommand();
      string sql = "select * from employee";
      dbcmd.CommandText = sql;
      IDataReader reader = dbcmd.ExecuteReader();
      while(reader.Read()) {
         string firstName = (string) reader["firstname"];
         string lastName = (string) reader["lastname"];
         string age_int = reader["age_int"].ToString();
         string your_number_float = reader["your_number_float"].ToString();
         string your_number_bigint = reader["your_number_bigint"].ToString();
         Console.WriteLine("Primary key: " + firstName +
                           "; lastname: " + lastName +
                           "; age_int: " + age_int +
                           "; your number float: " + your_number_float +
                           "; your number bigint: " + your_number_bigint);
      }
      reader.Close();
      reader = null;
      Console.WriteLine("\n");
   }
   public static void writeInTb(IDbConnection dbcon){
      Console.WriteLine("Enter primary key: ");
      string firstname = Console.ReadLine();
      Console.WriteLine("Enter Lastname: ");
      string lastname = Console.ReadLine();
      Console.WriteLine("Enter Age: ");
      int age_int = Int32.Parse(Console.ReadLine());
      Console.WriteLine("Enter your_number_float: ");
      float your_number_float = (float)Convert.ToDouble(Console.ReadLine());
      Console.WriteLine("Enter your_number_bigint: ");
      long your_number_bigint = (long)Convert.ToDouble(Console.ReadLine());

      IDbCommand dbcmd = dbcon.CreateCommand();
      string sql = "INSERT INTO employee (firstname , lastname, age_int, your_number_float, your_number_bigint) VALUES ('" + firstname + "', '"
                                                                                                                       + lastname + "', '"
                                                                                                                       + age_int + "', '"
                                                                                                                       + your_number_float + "', '"
                                                                                                                       + your_number_bigint + "')";
      dbcmd.CommandText = sql;
      dbcmd.ExecuteNonQuery();
      Console.WriteLine("Try to write in Table [" + firstname + "], [" + lastname + "], [" + age_int + "], [" + your_number_float + "], [" + your_number_bigint + "]\n");
      dbcmd.Dispose();
      dbcmd = null;
   }
   public static void deleteInTb(IDbConnection dbcon){
      Console.WriteLine("Delete from Table\n Enter [firstname] to delete it: ");
      string delId = Console.ReadLine();
      IDbCommand dbcmd = dbcon.CreateCommand();
      string sql = "delete from employee where firstname = '" + delId + "';";
      dbcmd.CommandText = sql;
      dbcmd.ExecuteNonQuery();
      Console.WriteLine("Try to delete ["+ delId +"] from Table\n");
      dbcmd.Dispose();
      dbcmd = null;
   }
   public static void Main(string[] args)
   {
      int i = 0;
      string connectionString =
                              "Server=172.18.0.2;" +
                              "Database=newuser_db;" +
                              "User ID=newuser;" +
                              "Password=123;" +
                              "Pooling=false";
      IDbConnection dbcon;
      dbcon = new MySqlConnection(connectionString);
      dbcon.Open();
      IDbCommand dbcmd = dbcon.CreateCommand();
      // requires a table to be created named employee
      // with columns firstname and lastname
      // such as,
      //        CREATE TABLE employee (
      //           firstname char(32),
      //           lastname char(32));
      //           age_int int;
      //           your_number int;
      string sql = "select * from employee";
      dbcmd.CommandText = sql;
      Console.WriteLine("\nWelcome to MariaDB interface");
      while(true)
      {
         Console.WriteLine("\n1) Show Table\n2) Insert Data\n3) Delte data\n0) Leave\nEnter mode: ");
         i = Int32.Parse(Console.ReadLine());
         if(i == 1){
            showTb(dbcon);
         }
         if(i == 2){
            writeInTb(dbcon);
         }
         if(i == 3){
            deleteInTb(dbcon);
         }
         if(i == 0){
         break;
         }
      }
      // clean up
      dbcmd.Dispose();
      dbcmd = null;
      dbcon.Close();
      dbcon = null;
   }
}
