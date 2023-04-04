using PLMS.Database;
using PLMS.Enum;
namespace PLMS.Model
{
    class ParkingSpace
    {
        private int _TotalSpaceAvailable;
        public int TotalSpaceAvailable
        {
            get
            {
                return _TotalSpaceAvailable;
            }
            set
            {
                _TotalSpaceAvailable = value;
            }
        }

        private int _SpaceForBike;
        public int SpaceForBike
        {
            get
            {
                return _SpaceForBike;
            }
            set
            {
                _SpaceForBike = value;
            }
        }

        private int _SpaceForCar;
        public int SpaceForCar
        {
            get
            {
                return _SpaceForCar;
            }
            set
            {
                _SpaceForCar = value;
            }
        }

        private int _SpaceForBus;
        public int SpaceForBus
        {
            get
            {
                return _SpaceForBus;
            }
            set
            {
                _SpaceForBus = value;
            }
        }

        private int _FloorNumber;
        public int FloorNumber
        {
            get
            {
                return _FloorNumber;
            }
            set
            {
                _FloorNumber = value;
            }
        }

        private ParkingSpaceDataAccess parkingSpaceData;
        private ParkingSpotsDataAccess parkingSpots;

        public ParkingSpace()
        {
            parkingSpaceData = new ParkingSpaceDataAccess();
            parkingSpots = new ParkingSpotsDataAccess();
        }

        public void AddParkingSpace(ParkingSpace parkingSpace)
        {
            parkingSpaceData.InserNewFloorInDB(parkingSpace);
            int CurrentFloor = parkingSpaceData.GetCurrentFLoor();
            if (CurrentFloor != 0)
            {
                parkingSpots.PopulateParkingSpots(CurrentFloor, parkingSpaceData);
            }
        }
        
        public void UpdateParkingSpace(VehicleCategory vehicleCategory, int floor, UpdateOperation updateType, int space)
        {
            parkingSpaceData.UpdateParkingSpaceInDB(vehicleCategory, floor, updateType, space);
        }
    }
}
