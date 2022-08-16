
using System;
using System.Data;
using MySql.Data.MySqlClient;

public class Test
{
   public static string getTable(){
      string shops = "shops";
      string vendor = "vendor";
      string parts = "parts";
      string prices = "prices";
      int i;
      while(true){
         Console.Write("Chose table to work with:\n1) shops\n2) vendor\n3) parts\n4) prices\n0)To Leave\n->");
         i = Int32.Parse(Console.ReadLine());
         if(i == 1){
            return shops;
         }
         if(i == 2){
            return vendor;
         }
         if(i == 3){
            return parts;
         }
         if(i == 4){
            return prices;
         }
         if(i == 0){
            return "break";
         }
      }
   }
   public static void showTb(IDbConnection dbcon, string table){
      IDbCommand dbcmd = dbcon.CreateCommand();
      string sql = "select * from " + table + "";
      dbcmd.CommandText = sql;
      IDataReader reader = dbcmd.ExecuteReader();

      // считываем названия столбцов
      for (int i = 0; i < reader.FieldCount; i++)
      Console.Write($"{reader.GetName(i)}\t".PadLeft(25));
      Console.WriteLine();

      // считываем значения
      while (reader.Read())
      {
      for (int i = 0; i < reader.FieldCount; i++)
      Console.Write($"{reader[i]}\t".PadLeft(25));
      Console.WriteLine();
      }

      reader.Close();
      reader = null;
      Console.WriteLine("\n");
   }
   public static void writeInTb(IDbConnection dbcon, string table) {
      if(table == "shops"){
         writeInShops(dbcon, table);
         return;
      }
      if(table == "vendor"){
         writeInVendor(dbcon, table);
         return;
      }
      if(table == "parts"){
         writeInParts(dbcon, table);
         return;
      }
      if(table == "prices"){
         writeInPrices(dbcon, table);
         return;
      }
   }
   public static void writeInShops(IDbConnection dbcon, string table){
      Console.WriteLine("Enter shop primary key: ");
      int shop_id = Int32.Parse(Console.ReadLine());
      Console.WriteLine("Enter shop name: ");
      string name = Console.ReadLine();

      IDbCommand dbcmd = dbcon.CreateCommand();
      string sql = "INSERT INTO shops (shop_id, name) VALUES ('" + shop_id + "', '" + name + "')";
      dbcmd.CommandText = sql;
      dbcmd.ExecuteNonQuery();
      Console.WriteLine("Try to write in Table \"" + table + "\": [" + shop_id + "], [" + name + "]\n");
      dbcmd.Dispose();
      dbcmd = null;
   }
   public static void writeInVendor(IDbConnection dbcon, string table){
      Console.WriteLine("Enter vendor primary key: ");
      int vendor_id = Int32.Parse(Console.ReadLine());
      Console.WriteLine("Enter vendor part name: ");
      string vendor_part_name = Console.ReadLine();
      Console.WriteLine("Enter vendor part id full: ");
      string vendor_part_id_full = Console.ReadLine();

      IDbCommand dbcmd = dbcon.CreateCommand();
      string sql = "INSERT INTO vendor (vendor_id, vendor_part_name, vendor_part_id_full) VALUES ('" + vendor_id + "', '" + vendor_part_name + "', '" + vendor_part_id_full + "')";
      dbcmd.CommandText = sql;
      dbcmd.ExecuteNonQuery();
      Console.WriteLine("Try to write in Table \"" + table + "\": [" + vendor_id + "], [" + vendor_part_name + "], [" + vendor_part_id_full + "]\n");
      dbcmd.Dispose();
      dbcmd = null;
   }
   public static void writeInParts(IDbConnection dbcon, string table){
      Console.WriteLine("Enter part id primary key: ");
      int part_id = Int32.Parse(Console.ReadLine());
      Console.WriteLine("Enter vendor part id: ");
      int vendor_part_id = Int32.Parse(Console.ReadLine());

      IDbCommand dbcmd = dbcon.CreateCommand();
      string sql = "INSERT INTO parts (part_id , vendor_part_id) VALUES ('" + part_id + "', '" + vendor_part_id + "')";
      dbcmd.CommandText = sql;
      dbcmd.ExecuteNonQuery();
      Console.WriteLine("Try to write in Table \"" + table + "\": [" + part_id + "], [" + vendor_part_id + "]\n");
      dbcmd.Dispose();
      dbcmd = null;
   }
      public static void writeInPrices(IDbConnection dbcon, string table){
      Console.WriteLine("Enter part id  foreing key: ");
      int part_id_p = Int32.Parse(Console.ReadLine());
      Console.WriteLine("Enter shop id foreing key: ");
      int shop_id_p = Int32.Parse(Console.ReadLine());
      Console.WriteLine("Enter price: ");
      int price = Int32.Parse(Console.ReadLine());
      Console.WriteLine("Enter amount: ");
      int amount = Int32.Parse(Console.ReadLine());

      IDbCommand dbcmd = dbcon.CreateCommand();
      string sql = "INSERT INTO prices (part_id_p , shop_id_p, price, amount) VALUES ('"+part_id_p+"','"+shop_id_p+"','"+price+"','"+amount+"')";
      dbcmd.CommandText = sql;
      dbcmd.ExecuteNonQuery();
      Console.WriteLine("Try to write in Table \"" + table + "\": [" + part_id_p + "], [" + shop_id_p + "], [" + price + "], [" + amount + "]\n");
      dbcmd.Dispose();
      dbcmd = null;
   }
   public static void deleteInTb(IDbConnection dbcon){
      Console.WriteLine("Delete from Table\n Enter [part_id_p] to delete it: ");
      string delId = Console.ReadLine();
      IDbCommand dbcmd = dbcon.CreateCommand();
      string sql = "delete from prices where part_id_p = '" + delId + "';";
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
         Console.WriteLine("\n1) Show Tables\n2) Insert Data\n3) Delete data from table \"vendor\"\n0) Leave\nEnter mode: ");
         i = Int32.Parse(Console.ReadLine());
         if(i == 1){
            string table = getTable();
            if(table == "break"){
               continue;
            }
            showTb(dbcon, table);
         }
         if(i == 2){
            string table = getTable();
            if(table == "break"){
               continue;
            }
            writeInTb(dbcon, table);
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
