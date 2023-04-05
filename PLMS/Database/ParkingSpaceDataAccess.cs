using PLMS.Model;
using System.Data.SQLite;
using PLMS.Enum;

namespace PLMS.Database
{
    class ParkingSpaceDataAccess
    {
        DataBaseConnection database = new DataBaseConnection();
        SQLiteCommand command;

        object locker = new object();
      

        public void InserNewFloorInDB(ParkingSpace _parkingSpace)
        {
            database.OpenConnection();
            
                
                using (command = database.MySqliteConnection.CreateCommand())
                {
                    
                    command.CommandText = QueryConstants.InsertNewFloor;                
                    command.Parameters.AddWithValue("$bikespace", _parkingSpace.SpaceForBike);
                    command.Parameters.AddWithValue("$carspace", _parkingSpace.SpaceForCar);
                    command.Parameters.AddWithValue("$busspace", _parkingSpace.SpaceForBus);

                    command.ExecuteNonQuery();
                    
                }
                database.CloseConnection();

            
        }

        public ParkingSpace GetParkingSpaceDetailsFromDB(int Floor)
        {
            
                database.OpenConnection();


                using (command = database.MySqliteConnection.CreateCommand())
                {

                    command.CommandText = QueryConstants.GetParkingSpaceDetails;
                    command.Parameters.AddWithValue("$floor", Floor);
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                         ParkingSpace parkingSpace = new ParkingSpace
                         {
                            FloorNumber = reader.GetInt32(0),
                            SpaceForBike = reader.GetInt32(1),
                            SpaceForCar = reader.GetInt32(2),
                            SpaceForBus = reader.GetInt32(3)
                         };

                        reader.Close();
                        database.CloseConnection();
                        
                        return parkingSpace;
                       

                    }
                    else
                    {
                        reader.Close();
                        database.CloseConnection();

                        return null;
                    }

                }
            
            
        
        }

        public void UpdateParkingSpaceInDB(VehicleCategory spacetype,int floor,UpdateParkingSpace updateType,int space)
        {
            database.OpenConnection();
            lock (locker)
            {
                if (updateType== UpdateParkingSpace.Decrease)
                {
                   
                    using (command = database.MySqliteConnection.CreateCommand())
                    {

                        if (spacetype == VehicleCategory.Bike)
                        {
                            command.CommandText = QueryConstants.DecreaseBikeParkingSpace;
                            command.Parameters.AddWithValue("$space", space);
                            command.Parameters.AddWithValue("$floor", floor);
                            command.ExecuteNonQuery();
                        }
                        else if(spacetype== VehicleCategory.Car)
                        {
                            command.CommandText = QueryConstants.DecreaseCarParkingSpace;
                            command.Parameters.AddWithValue("$space", space);
                            command.Parameters.AddWithValue("$floor", floor);
                            command.ExecuteNonQuery();

                        }
                        else if(spacetype == VehicleCategory.Bus)
                        {
                            command.CommandText = QueryConstants.DecreaseBusParkingSpace;
                            command.Parameters.AddWithValue("$space", space);
                            command.Parameters.AddWithValue("$floor", floor);
                            command.ExecuteNonQuery();
                        }
                        database.CloseConnection();
                    }
                }
                else if (updateType == UpdateParkingSpace.Increase)
                {
                    
                    using (command = database.MySqliteConnection.CreateCommand())
                    {
                        if (spacetype == VehicleCategory.Bike)
                        {
                            command.CommandText = QueryConstants.IncreaseBikeParkingSpace;  
                            command.Parameters.AddWithValue("$space", space);
                            command.Parameters.AddWithValue("$floor", floor);
                            command.ExecuteNonQuery();
                        }
                        else if (spacetype == VehicleCategory.Car)
                        {
                            command.CommandText = QueryConstants.IncreaseCarParkingSpace;
                            command.Parameters.AddWithValue("$space", space);
                            command.Parameters.AddWithValue("$floor", floor);
                            command.ExecuteNonQuery();

                        }
                        else if (spacetype == VehicleCategory.Bus)
                        {
                            command.CommandText = QueryConstants.IncreaseBusParkingSpace;
                            command.Parameters.AddWithValue("$space", space);
                            command.Parameters.AddWithValue("$floor", floor);
                            command.ExecuteNonQuery();
                        }
                        database.CloseConnection();
                    }

                }
            }
        }

        public int GetCurrentFLoor()
        {
            database.OpenConnection();
            lock (locker)
            {
                using(command = database.MySqliteConnection.CreateCommand())
                {
                    command.CommandText = QueryConstants.GetCurrentFloor;
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();                    
                        int floor = reader.GetInt32(0);
                        reader.Close();
                        database.CloseConnection();
                        return floor;
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
    }

}
