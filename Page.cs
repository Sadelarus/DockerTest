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
         string FirstName = (string) reader["firstname"];
         string LastName = (string) reader["lastname"];
         Console.WriteLine("Primary key: " + FirstName + "; lastname: " + LastName);
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
      IDbCommand dbcmd = dbcon.CreateCommand();
      string sql = "INSERT INTO employee (firstname , lastname) VALUES ('" + firstname + "', '" + lastname + "')";
      dbcmd.CommandText = sql;
      dbcmd.ExecuteNonQuery();
      Console.WriteLine("Try to write in Table [" + firstname + "] and [" + lastname + "]\n");
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
      //           firstname varchar(32),
      //           lastname varchar(32));
      string sql = "select * from employee";
      dbcmd.CommandText = sql;
      Console.WriteLine("\nWelcome to MariaDB interface");
      while(true)
      {
         Console.WriteLine("\n1) Show Table\n 2) Insert Data\n 3) Delte data\n 0) Leave\n Enter mode: ");
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
