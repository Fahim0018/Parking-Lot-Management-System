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



        public (VehicleCategory,int,int) VehicleEntryInParkingLot(Vehicle vehicle)
        {
            
            vehicleData.InsertVehicleInDB(vehicle);
            (int spot, int floor) = parkingSpotsData.AllotParkingSpotFromDB(vehicle);
            VehicleCategory ParkingSpotCategory = parkingSpotsData.GetParkingSpotCategory(spot, floor);
            if (floor == 0)
            {
                vehicleData.DeleteVehicleFromTable(vehicle.VehicleLicenceNumber);
                
                return (VehicleCategory.None,spot, floor);
            }
            return (ParkingSpotCategory, spot, floor);

        }



        public (int,int,VehicleCategory) VehicleExitFromParkingLot(string vehicleLicenceNumber)
        {
            vehicleData.UpdateParkingDetails(vehicleLicenceNumber);
            ( int Floor, int SpotID) = parkedVehicleData.GetParkedVehicleDetails(vehicleLicenceNumber);
            parkingSpotsData.UpdateParkingSpotInDB(Floor, SpotID, false);
            parkedVehicleData.RemoveVehicle(vehicleLicenceNumber);
            VehicleCategory ParkingSpotCategory = parkingSpotsData.GetParkingSpotCategory(SpotID, Floor);
            return ( Floor, SpotID, ParkingSpotCategory);
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
