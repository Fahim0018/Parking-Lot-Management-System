using PLMS.Model;
using System.Collections.Generic;
using System.Data.SQLite;
using PLMS.Enum;
namespace PLMS.Database
{
    class ParkingSpotsDataAccess
    {
        DataBaseConnection database = new DataBaseConnection();

        object locker = new object();

        SQLiteCommand command;

        public void PopulateParkingSpots(int Floor,ParkingSpaceDataAccess parkingSpaceData)
        {
                ParkingSpace parkingSpace = parkingSpaceData.GetParkingSpaceDetailsFromDB(Floor);
                database.OpenConnection();
           
                
                using (command = database.MySqliteConnection.CreateCommand())
                {
   
                    int spotnumber = 1;
                    while (spotnumber<= parkingSpace.SpaceForBike)
                    {
                        command.CommandText = QueryConstants.InsertParkingSpotDetails;
                        command.Parameters.AddWithValue("$floor", parkingSpace.FloorNumber);
                        command.Parameters.AddWithValue("$parkingspotnumber", spotnumber);
                        command.Parameters.AddWithValue("$parkingspotcategory", VehicleCategory.Bike);
                        command.Parameters.AddWithValue("$isoccupied", 0);
                        command.ExecuteNonQuery();
                        spotnumber += 1;

                    }
                    while (spotnumber <= (parkingSpace.SpaceForBike + parkingSpace.SpaceForCar))
                    {
                        command.CommandText = QueryConstants.InsertParkingSpotDetails;
                        command.Parameters.AddWithValue("$floor", parkingSpace.FloorNumber);
                        command.Parameters.AddWithValue("$parkingspotnumber", spotnumber);
                        command.Parameters.AddWithValue("$parkingspotcategory", VehicleCategory.Car);
                        command.Parameters.AddWithValue("$isoccupied", 0);
                        command.ExecuteNonQuery();
                        spotnumber += 1;
                    }
                    while (spotnumber <= (parkingSpace.SpaceForBike+ parkingSpace.SpaceForCar+parkingSpace.SpaceForBus))
                    {
                        command.CommandText = QueryConstants.InsertParkingSpotDetails;
                        command.Parameters.AddWithValue("$floor", parkingSpace.FloorNumber);
                        command.Parameters.AddWithValue("$parkingspotnumber", spotnumber);
                        command.Parameters.AddWithValue("$parkingspotcategory", VehicleCategory.Bus);
                        command.Parameters.AddWithValue("$isoccupied", 0);
                        command.ExecuteNonQuery();
                        spotnumber += 1;
                    }
                    


                
                database.CloseConnection();
            }
        }

        public ParkingSpot AllotParkingSpotFromDB(Vehicle vehicle)
        {
            database.OpenConnection();
            lock (locker)
            {
                
                using (command = database.MySqliteConnection.CreateCommand()) {
                    
                    ParkingSpot parkingSpot = new ParkingSpot();
                    command.CommandText = QueryConstants.AllotParkingSpot;
                    if (vehicle.VehicleCategory == VehicleCategory.Bike  )
                    {
                        command.Parameters.AddWithValue("$parkingspotcategory", VehicleCategory.Bike);
                    }
                    if (vehicle.VehicleCategory == VehicleCategory.Car)
                    {
                        command.Parameters.AddWithValue("$parkingspotcategory", VehicleCategory.Car);
                    }
                    if (vehicle.VehicleCategory == VehicleCategory.Bus)
                    {
                        command.Parameters.AddWithValue("$parkingspotcategory", VehicleCategory.Bus);
                    }
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetInt32(3).Equals(0))
                        {
                            
                            parkingSpot.ParkingSpotID = reader.GetInt32(1);
                            parkingSpot.Floor = reader.GetInt32(0);
                            if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(0))
                            {
                                parkingSpot.SpotCategory = VehicleCategory.Bike;
                            }
                            else if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(1))
                            {
                                parkingSpot.SpotCategory = VehicleCategory.Car;
                            }

                            else if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(2))
                            {
                                parkingSpot.SpotCategory = VehicleCategory.Bus;

                            }
                            ParkedVehicleDataAccess parkedVehicleTable = new ParkedVehicleDataAccess();
                            reader.Close();
                            database.CloseConnection();
                            parkedVehicleTable.ParkVehicle(vehicle, parkingSpot);
                            UpdateParkingSpotInDB(parkingSpot, true);
                            break;
                        }

                    }
                    if(parkingSpot.ParkingSpotID==0 && parkingSpot.Floor == 0 && vehicle.VehicleCategory==VehicleCategory.Bike)
                    {
                        database.OpenConnection();
                        lock (locker)
                        {
                            using(command = database.MySqliteConnection.CreateCommand())
                            {
                                command.CommandText = QueryConstants.AllotDifferentParkingSpotForBike;
                                
                                command.Parameters.AddWithValue("$parkingspotcategory", VehicleCategory.Bike);
                                
                                reader = command.ExecuteReader();
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(3).Equals(0))
                                    {

                                        parkingSpot.ParkingSpotID = reader.GetInt32(1);
                                        parkingSpot.Floor = reader.GetInt32(0);
                                        if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(0))
                                        {
                                            parkingSpot.SpotCategory = VehicleCategory.Bike;
                                        }
                                        else if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(1))
                                        {
                                            parkingSpot.SpotCategory = VehicleCategory.Car;
                                        }

                                        else if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(2))
                                        {
                                            parkingSpot.SpotCategory = VehicleCategory.Bus;

                                        }
                                        ParkedVehicleDataAccess parkedVehicleTable = new ParkedVehicleDataAccess();
                                        reader.Close();
                                        database.CloseConnection();
                                        parkedVehicleTable.ParkVehicle(vehicle, parkingSpot);
                                        UpdateParkingSpotInDB(parkingSpot, true);
                                        break;
                                    }

                                }


                            }
                        }
                    }
                    if (parkingSpot.ParkingSpotID == 0 && parkingSpot.Floor == 0 && vehicle.VehicleCategory == VehicleCategory.Car)
                    {
                        database.OpenConnection();
                        lock (locker)
                        {
                            using (command = database.MySqliteConnection.CreateCommand())
                            {
                                command.CommandText = QueryConstants.AllotDifferentParkingSpotForCar;

                                command.Parameters.AddWithValue("$parkingspotcategory", VehicleCategory.Bus);

                                reader = command.ExecuteReader();
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(3).Equals(0))
                                    {

                                        parkingSpot.ParkingSpotID = reader.GetInt32(1);
                                        parkingSpot.Floor = reader.GetInt32(0);
                                        if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(0))
                                        {
                                            parkingSpot.SpotCategory = VehicleCategory.Bike;
                                        }
                                        else if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(1))
                                        {
                                            parkingSpot.SpotCategory = VehicleCategory.Car;
                                        }

                                        else if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(2))
                                        {
                                            parkingSpot.SpotCategory = VehicleCategory.Bus;

                                        }
                                        ParkedVehicleDataAccess parkedVehicleTable = new ParkedVehicleDataAccess();
                                        reader.Close();
                                        database.CloseConnection();
                                        parkedVehicleTable.ParkVehicle(vehicle, parkingSpot);
                                        UpdateParkingSpotInDB(parkingSpot, true);
                                        break;
                                    }

                                }


                            }
                        }
                    }

                    return parkingSpot;
                }
                

            }
        }

        public void UpdateParkingSpotInDB(ParkingSpot parkingSpot,bool IsOccupied)
        {
            database.OpenConnection();
            lock (locker)
            {
                
                using (command = database.MySqliteConnection.CreateCommand())
                {
                   
                    command.CommandText = QueryConstants.UpdateParkingSpot;
                    command.Parameters.AddWithValue("$floor", parkingSpot.Floor);
                    if (IsOccupied == true)
                    {
                        command.Parameters.AddWithValue("$isOccupied", 1);
                    }
                    else if(IsOccupied==false)
                    {
                        command.Parameters.AddWithValue("$isOccupied", 0);
                    }
                    command.Parameters.AddWithValue("$parkingSpotNumber", parkingSpot.ParkingSpotID);
                    command.ExecuteNonQuery();
                    
                }
                database.CloseConnection();
            }
        }

        public List<ParkingSpot> GetParkingSpotsDetails()
        {
            database.OpenConnection();
            lock (locker)
            {
                
                using (command = database.MySqliteConnection.CreateCommand())
                {
                    
                    
                    List<ParkingSpot> parkingSpotList = new List<ParkingSpot>();
                    command.CommandText = QueryConstants.GetParkingSpotDetails;
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetInt32(3).Equals(0))
                        {
                            ParkingSpot parkingSpot = new ParkingSpot();
                            parkingSpot.Floor = reader.GetInt32(0);
                            parkingSpot.ParkingSpotID = reader.GetInt32(1);

                            if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(0)) {
                                parkingSpot.SpotCategory = VehicleCategory.Bike;
                            }
                            else if(int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(1))
                            {
                                parkingSpot.SpotCategory = VehicleCategory.Car;
                            }

                            else if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(2))
                            {
                                parkingSpot.SpotCategory = VehicleCategory.Bus;

                            }
                           
                            parkingSpot.IsOccupied= false;
                            parkingSpotList.Add(parkingSpot);
                        }
                        
                    }
                    reader.Close();
                    database.CloseConnection();
                    return parkingSpotList;
                }
            }
        }

        public VehicleCategory GetParkingSpotCategory(int SpotID, int Floor )
        {
            database.OpenConnection();
            lock (locker)
            {
                using(command = database.MySqliteConnection.CreateCommand())
                {
                    command.CommandText = QueryConstants.GetParkingSpotCategoryFromDB;
                    command.Parameters.AddWithValue("$floor", Floor);
                    command.Parameters.AddWithValue("$parkingSpotNumber", SpotID);
                    SQLiteDataReader reader = command.ExecuteReader();
                    VehicleCategory VehicleType;
                    if (reader.Read())
                    {
                        if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(0))
                        {
                            VehicleType = VehicleCategory.Bike;
                        }
                        else if (int.Parse(reader["ParkingSpotCategory"].ToString()).Equals(1))
                        {
                            VehicleType = VehicleCategory.Car;
                        }

                        else
                        {
                            VehicleType = VehicleCategory.Bus;

                        }
                        reader.Close();
                        database.CloseConnection();
                        return VehicleType;
                    }
                    else
                    {
                        reader.Close();
                        database.CloseConnection();
                        return VehicleCategory.None;
                    }
                   
                   
                }
            }

        }

        public bool CheckForEmptySpotsInDB()
        {
            database.OpenConnection();
            lock (locker)
            {
                using(command = database.MySqliteConnection.CreateCommand())
                {
                    command.CommandText = QueryConstants.CheckForEmptySpots;
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
    }
}
