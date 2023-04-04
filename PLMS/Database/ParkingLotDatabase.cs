using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLMS.Enum;

namespace PLMS.Database
{
    class ParkingLotDatabase
    {
        DataBaseConnection database = new DataBaseConnection();
        SQLiteCommand command;
        object locker = new object();

        public ParkingLotDatabase()
        {
            database.OpenConnection();
            lock (locker)
            {
                
                using (command = database.MySqliteConnection.CreateCommand())
                {

                    command.CommandText = "CREATE TABLE IF NOT EXISTS ADMIN" +
                               "(Name VARCHAR  UNIQUE,ID INTEGER,Password VARCHAR,Email VARCHAR UNIQUE," +
                               "CONSTRAINT ADMIN_PK PRIMARY KEY(ID))";
                    command.ExecuteNonQuery();


                    command.CommandText = "Create Table If Not EXISTS ParkingSpace" +
                        "(Floor integer not null unique,BikeSpace integer,CarSpace integer,BusSpace integer," +
                        "Constraint ParkingSpace_pk Primary Key(Floor))";
                    command.ExecuteNonQuery();

                    command.CommandText = "Create Table If Not EXISTS VehicleDetails" +
                            "(VehicleType Varchar not null,VehicleNumber Varchar Not Null,VehicleInDateTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,VehicleOutDateTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP,TotalTimeDuration Real,IsParked Integer,ParkingFee Real)";
                    command.ExecuteNonQuery();

                    command.CommandText = "Create Table If Not EXISTS ParkedVehicle" +
                        "(VehicleType Varchar not null,VehicleNumber Varchar Not Null Unique,ParkingSpot Integer,Floor Itneger," +
                        "Constraint ParkedVehicle_pk Primary Key(VehicleNumber));";
                    command.ExecuteNonQuery();

                    command.CommandText = "Create Table If not EXISTS ParkingCharges(VehicleType Text,ChargeForFirstHour Integer not null,ChargeAfterFirstHour Integer not null)"; 
                                          
                    command.ExecuteNonQuery();

                    command.CommandText = "Create Table If Not EXISTS ParkingSpots(Floor Integer,ParkingSpotNumber Integer,ParkingSpotCategory Text,IsOccupied integer)";
                    command.ExecuteNonQuery();

                    command.CommandText = QueryConstants.InsertInFeeStructure;
                    command.Parameters.AddWithValue("$vehicleType",VehicleCategory.Bike );
                    command.Parameters.AddWithValue("$forfirsthour", 60);
                    command.Parameters.AddWithValue("$afterfirsthour", 80);
                    command.ExecuteNonQuery();

                    command.CommandText = QueryConstants.InsertInFeeStructure;
                    command.Parameters.AddWithValue("$vehicleType", VehicleCategory.Car);
                    command.Parameters.AddWithValue("$forfirsthour", 80);
                    command.Parameters.AddWithValue("$afterfirsthour", 100);
                    command.ExecuteNonQuery();

                    command.CommandText = QueryConstants.InsertInFeeStructure;
                    command.Parameters.AddWithValue("$vehicleType", VehicleCategory.Bus);
                    command.Parameters.AddWithValue("$forfirsthour", 100);
                    command.Parameters.AddWithValue("$afterfirsthour", 120);
                    command.ExecuteNonQuery();

                }
                database.CloseConnection();
            }
        }
    }
}
