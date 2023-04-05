using PLMS.Database;
using System;
using PLMS.Enum;
namespace PLMS.Model

{
    class Vehicle
    {
        private int _VehicleID;
        public int VehicleID
        {
            get
            {
                return _VehicleID;
            }
            set
            {
                _VehicleID = value;
            }
        }

        private string _VehicleLicenceNumber;
        public string VehicleLicenceNumber
        {
            get
            {
                return _VehicleLicenceNumber;
            }
            set
            {
                _VehicleLicenceNumber = value;
            }
        }

        private VehicleCategory _VehicleCategory;
        public VehicleCategory VehicleCategory
        {
            get
            {
                return _VehicleCategory;
            }
            set
            {
                _VehicleCategory = value;
            }
        }


        public bool _IsParked;
        public bool IsParked
        {
            get
            {
                return _IsParked;
            }
            set
            {
                _IsParked = value;
            }
        }

        private DateTime _VehicleInDateTime;
        public DateTime VehicleInDateTime
        {
            get
            {
                return _VehicleInDateTime;
            }
            set
            {
                _VehicleInDateTime = value;
            }
        }

        private DateTime _VehicleOutDateTime;
        public DateTime VehicleOutDateTime
        {
            get
            {
                return _VehicleOutDateTime;
            }
            set
            {
                _VehicleOutDateTime = value;
            }
        }

        private decimal _VehicleParkingDuration;
        public decimal VehicleParkingDuration
        {
            get
            {
                return _VehicleParkingDuration;
            }
            set
            {
                _VehicleParkingDuration = value;
            }
        }

        private decimal _VehicleParkingFee;
        public decimal VehicleParkingFee
        {
            get
            {
                return _VehicleParkingFee;
            }
            set
            {
                _VehicleParkingFee = value;
            }
        }

        private VehicleDataAccess vehicleData;

        private ParkedVehicleDataAccess parkedVehicleData;

        private ParkingSpotsDataAccess parkingSpotsData;

        public Vehicle()
        {
            vehicleData = new VehicleDataAccess();
            parkedVehicleData = new ParkedVehicleDataAccess();
            parkingSpotsData = new ParkingSpotsDataAccess();
        }



        public ParkingSpot VehicleEntryInParkingLot(Vehicle vehicle)
        {
            
            vehicleData.InsertVehicleInDB(vehicle);
             
            ParkingSpot parkingSpot = parkingSpotsData.AllotParkingSpotFromDB(vehicle);

            if (parkingSpot.Floor == 0)
            {
                vehicleData.DeleteVehicleFromTable(vehicle.VehicleLicenceNumber);               
                return (parkingSpot);
            }
            return (parkingSpot);

        }



        public ParkingSpot VehicleExitFromParkingLot(string vehicleLicenceNumber)
        {
            
            vehicleData.UpdateParkingDetails(vehicleLicenceNumber);          
            ParkingSpot parkingSpot = parkedVehicleData.GetParkedVehicleDetails(vehicleLicenceNumber);
            parkingSpotsData.UpdateParkingSpotInDB(parkingSpot,false);
            parkedVehicleData.RemoveVehicle(vehicleLicenceNumber);
            
            return parkingSpot;
        }



        public bool CheckParkedVehicle(string VehicleLicenceNumber)
        {
            if (parkedVehicleData.CheckForVehicleInDB(VehicleLicenceNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
