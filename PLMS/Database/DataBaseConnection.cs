
using System;
using System.Data.SQLite;
using System.IO;
namespace PLMS.Database
{
    class DataBaseConnection
    {
        public SQLiteConnection MySqliteConnection;

        public DataBaseConnection()
        {
            MySqliteConnection = new SQLiteConnection("Data Source = ParkingLot.sqlite3");
            if (!File.Exists("./ParkingLot.sqlite3"))
            {

                SQLiteConnection.CreateFile("ParkingLot.sqlite3");
                Console.WriteLine("DataBase File Created");
            }
        }
       
        public void OpenConnection()
        {
            if (MySqliteConnection.State != System.Data.ConnectionState.Open)
            {
                MySqliteConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (MySqliteConnection.State != System.Data.ConnectionState.Closed)
            {
                MySqliteConnection.Close();
            }
        }
    }
}

