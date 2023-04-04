using PLMS.Database;
using System.Collections.Generic;
using PLMS.Enum;
namespace PLMS.Model

{
    class ParkingSpot
    {
        private int _ParkingSpotID;
        public int ParkingSpotID
        {
            get
            {
                return _ParkingSpotID;
            }
            set
            {
                _ParkingSpotID = value;
            }
        }

        private int _Floor;
        public int Floor
        {
            get
            {
                return _Floor;
            }
            set
            {
                _Floor = value;
            }
        }

        private bool _IsOccupied;
        public bool IsOccupied
        {
            get
            {
                return _IsOccupied;
            }
            set
            {
                _IsOccupied = value;
            }
        }

        private int _ParkedVehicleNumber;
        public int ParkedVehicleNumber
        {
            get
            {
                return _ParkedVehicleNumber;
            }
            set
            {
                _ParkedVehicleNumber = value;
            }
        }

        private VehicleCategory _SpotCategory;
        public VehicleCategory SpotCategory
        {
            get
            {
                return _SpotCategory;
            }
            set
            {
                _SpotCategory = value;
            }
        }

        private ParkingSpotsDataAccess parkingSpots;

        public ParkingSpot()
        {
            parkingSpots = new ParkingSpotsDataAccess();
        }



        public List<ParkingSpot> GetAvailableParkingSpots()
        {
            List<ParkingSpot> parkingSpotList= parkingSpots.GetParkingSpotsDetails();
            return parkingSpotList;
        }



        public bool CheckForEmptySpots()
        {
           return parkingSpots.CheckForEmptySpotsInDB();
        }
    }
}
