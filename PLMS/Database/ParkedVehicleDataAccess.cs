using System.Data.SQLite;
using PLMS.Model;
using PLMS.Enum;
namespace PLMS.Database

{
    class ParkedVehicleDataAccess
    {
        DataBaseConnection database = new DataBaseConnection();
        SQLiteCommand command;
        object locker = new object();

        public void ParkVehicle(Vehicle vehicle,ParkingSpot parkingspot)
        {
            database.OpenConnection();
            lock (locker)
            {
             
                using (command = database.MySqliteConnection.CreateCommand())
                {
                    
                    command.CommandText = QueryConstants.InsertParkedVehicle;

                    command.Parameters.AddWithValue("$vehicletype", vehicle.VehicleCategory);
                    command.Parameters.AddWithValue("$vehiclenumber", vehicle.VehicleLicenceNumber);
                    command.Parameters.AddWithValue("$parkingspot", parkingspot.ParkingSpotID);
                    command.Parameters.AddWithValue("$Floor", parkingspot.Floor);

                    command.ExecuteNonQuery();
                    
                }
                database.CloseConnection();
            }
        }

        public void RemoveVehicle(string vehicleNumber)
        {
            database.OpenConnection();
            lock (locker)
            {
                using (command = database.MySqliteConnection.CreateCommand())
                {
                    command.CommandText = QueryConstants.RemoveVehicleFromParkedVehicle;
                    command.Parameters.AddWithValue("$vehicleNumber", vehicleNumber);
                    command.ExecuteNonQuery();
                    database.CloseConnection();
                }
            }

        }

        public bool CheckForVehicleInDB(string VehicleNumber)
        {
            database.OpenConnection();
            lock (locker)
            {
               
                using (command = database.MySqliteConnection.CreateCommand())
                {
                    
                    command.CommandText = QueryConstants.CheckForVehicleInDB;
                    command.Parameters.AddWithValue("$vehicleNumber", VehicleNumber);
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {

                        reader.Close();
                        database.CloseConnection();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        database.CloseConnection();
                        return false;
                    }
                }
            }
        }

        public ( int, int) GetParkedVehicleDetails(string VehicleNumber)
        {
            database.OpenConnection();
            lock (locker)
            {
               
                using (command = database.MySqliteConnection.CreateCommand())
                {
                    
                    command.CommandText = QueryConstants.GetParkedVehicleDetails;
                    command.Parameters.AddWithValue("$vehicleNumber", VehicleNumber);
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int floor = reader.GetInt32(3);
                        int spotid = reader.GetInt32(2);
                        reader.Close();
                        database.CloseConnection();
                        return ( floor, spotid);
                       
                    }
                    else
                    {
                        reader.Close();
                        database.CloseConnection();
                        return ( 0, 0);
                    }
                }
            }
        }
    }
}
