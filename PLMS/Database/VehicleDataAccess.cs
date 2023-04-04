using PLMS.Model;
using System.Data.SQLite;
using System;

namespace PLMS.Database
{
    class VehicleDataAccess 
    {


        DataBaseConnection database = new DataBaseConnection();
        SQLiteCommand command;

        object locker = new object();

        public void InsertVehicleInDB(Vehicle vehicle)
        {
            database.OpenConnection();
            lock (locker)
            {

                using (command = database.MySqliteConnection.CreateCommand())
                {

                    command.CommandText = QueryConstants.InsertVehicle;
                    command.Parameters.AddWithValue("$vehicletype", vehicle.VehicleCategory);
                    command.Parameters.AddWithValue("$vehiclenumber", vehicle.VehicleLicenceNumber);
                    command.Parameters.AddWithValue("$vehicleindatetime", vehicle.VehicleInDateTime);
                    command.Parameters.AddWithValue("$vehicleoutdatetime", null);
                    command.Parameters.AddWithValue("$totaltimeduration", null);
                    if (vehicle.IsParked == true)
                    {
                        command.Parameters.AddWithValue("$isParked", 1);
                    }
                    else if (vehicle.IsParked == false)
                    {
                        command.Parameters.AddWithValue("$isParked", 0);
                    }

                    command.Parameters.AddWithValue("$parkingFee", null);


                    command.ExecuteNonQuery();

                }
                database.CloseConnection();
            }
        }

        public void UpdateParkingDetails(string vehicleNumber)
        {
            database.OpenConnection();
            lock (locker)
            {

                using (command = database.MySqliteConnection.CreateCommand())
                {


                    command.CommandText = QueryConstants.UpdateVehicleDetails;
                    command.Parameters.AddWithValue("$vehicleNumber", vehicleNumber);
                    command.Parameters.AddWithValue("$vehicleoutdatetime", DateTime.Now);


                    command.ExecuteNonQuery();


                    command.CommandText = QueryConstants.CalculateTimeDuration;
                    command.Parameters.AddWithValue("$vehicleNumber", vehicleNumber);

                    SQLiteDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        float timeduratiion = reader.GetFloat(1);

                        reader.Close();
                        command.CommandText = QueryConstants.UpdateTimeDuration;
                        command.Parameters.AddWithValue("$totaltimeduration", timeduratiion);
                        command.Parameters.AddWithValue("$vehicleNumber", vehicleNumber);
                        command.Parameters.AddWithValue("$isParked", 0);
                        command.ExecuteNonQuery();
                    }


                }
                database.CloseConnection();
            }


        }

        public decimal GetParkingTimeDuration(string vehicleNumber)
        {
            database.OpenConnection();
            lock (locker)
            {

                using (command = database.MySqliteConnection.CreateCommand())
                {

                    command.CommandText = QueryConstants.GetParkingDuration;
                    command.Parameters.AddWithValue("$vehicleNumber", vehicleNumber);
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        decimal timeduration = reader.GetDecimal(0);

                        reader.Close();
                        database.CloseConnection();
                        return timeduration;

                    }
                    else
                    {
                        reader.Close();
                        database.CloseConnection();
                        return 0;
                    }
                }
            }




        }

        public void DeleteVehicleFromTable(string VehicleNumber)
        {
            database.OpenConnection();
            lock (locker)
            {
                using (command = database.MySqliteConnection.CreateCommand())
                {
                    command.CommandText = QueryConstants.DeleteVehicleFromVehicleDetails;
                    command.Parameters.AddWithValue("$vehicleNumber", VehicleNumber);
                    command.ExecuteReader();
                }
            }
            database.CloseConnection();
        }



    }
}
