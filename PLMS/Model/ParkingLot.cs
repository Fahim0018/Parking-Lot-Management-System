using PLMS.Database;
namespace PLMS.Model
{
    class ParkingLot
    {
        private string _ParkingLotName;
        public string ParkingLotName
        {
            get
            {
                return _ParkingLotName;
            }
            set
            {
                _ParkingLotName = value;
            }
        }

        private int _NumberOfFloors;
        public int NumberOfFloors
        {
            get
            {
                return _NumberOfFloors;
            }
            set
            {
                _NumberOfFloors = value;
            }
        }

        private ParkingSpace _ParkingSpace;
        public ParkingSpace ParkingSpace
        {
            get
            {
                return _ParkingSpace;
            }
            set
            {
                _ParkingSpace = value;
            }
        }

        private int _ParkingLotID;
        public int ParkingLotID
        {
            get
            {
                return _ParkingLotID;
            }
            set
            {
                _ParkingLotID = value;
            }
        }


        private DataBaseConnection dataBase;

        private ParkingLotDatabase _ParkingLotDB;

        public void CreateParkigLot()
        {
           dataBase  = new DataBaseConnection();
            _ParkingLotDB = new ParkingLotDatabase();
        }

       
    }
}
