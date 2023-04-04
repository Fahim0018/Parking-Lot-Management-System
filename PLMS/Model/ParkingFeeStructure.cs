using PLMS.Database;
using PLMS.Enum;

namespace PLMS.Model
{
    class ParkingFeeStructure
    {
        private int _ChargesForFirstHour;
        public int ChargesForFirstHour
        {
            get
            {
                return _ChargesForFirstHour;
            }
            set
            {
                _ChargesForFirstHour = value;
            }
        }

        private int _ChargesAfterFirstHour;
        public int ChargesAfterFirstHour
        {
            get
            {
                return _ChargesAfterFirstHour;
            }
            set
            {
                _ChargesAfterFirstHour = value;
            }
        }

        private ParkingChargesDataAccess parkingChargesData;

        private VehicleDataAccess vehicleData;

        private ParkingSpotsDataAccess parkingSpotData;

        public ParkingFeeStructure()
        {
            parkingChargesData = new ParkingChargesDataAccess();
            vehicleData = new VehicleDataAccess();
            parkingSpotData = new ParkingSpotsDataAccess();
        }

        public decimal CalculateParkingFee(string vehicleNumber, int SpotID,int Floor)
        {
            (ChargesForFirstHour, ChargesAfterFirstHour) = parkingChargesData.GetFeeStructureDetails(parkingSpotData.GetParkingSpotCategory(SpotID, Floor));
            

            decimal timeduartion = vehicleData.GetParkingTimeDuration(vehicleNumber);

            decimal parkingFee;

            if (timeduartion > 1)
            {
                parkingFee = ChargesForFirstHour + (timeduartion - 1) * ChargesAfterFirstHour;
            }
            else
            {
                parkingFee = ChargesForFirstHour * timeduartion;              
            }

            parkingChargesData.UpadateParkingFeesInDB(parkingFee, vehicleNumber);
            return parkingFee;
           
        }
    }
}

