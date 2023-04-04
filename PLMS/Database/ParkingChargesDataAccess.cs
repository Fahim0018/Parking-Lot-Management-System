using System.Data.SQLite;
using PLMS.Enum;

namespace PLMS.Database
{
    class ParkingChargesDataAccess

    {
        DataBaseConnection database = new DataBaseConnection();

        object locker = new object();

        SQLiteCommand command;


        public (int,int) GetFeeStructureDetails(VehicleCategory VehicleType)
        {
            lock (locker)
            {
                database.OpenConnection();
                using (command = database.MySqliteConnection.CreateCommand())
                {
                    
                    command.CommandText = QueryConstants.GetFeeStructureDetails;
                    command.Parameters.AddWithValue("$vehicleType", VehicleType);
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {

                        int charge1 = reader.GetInt32(0);
                        int charge2 =reader.GetInt32(1);
                        reader.Close();
                        database.CloseConnection();
                        return (charge1,charge2 );
                            
                        
                    }
                    else
                    {
                        reader.Close();
                        database.CloseConnection();
                        return (0, 0);
                    }
                }
            }
        }
           
        public void UpadateParkingFeesInDB(decimal parkingfee, string vehicleNumber)
        {
            database.OpenConnection();
            lock (locker)
            {
                
                using (command = database.MySqliteConnection.CreateCommand())
                {
                   
                    command.CommandText = QueryConstants.UpdateParkingFee;
                    command.Parameters.AddWithValue("$parkingFee", parkingfee);
                    command.Parameters.AddWithValue("$vehicleNumber", vehicleNumber);
                    command.ExecuteNonQuery();
                    
                }
              
            }
            database.CloseConnection();
        }

    }

    
}
