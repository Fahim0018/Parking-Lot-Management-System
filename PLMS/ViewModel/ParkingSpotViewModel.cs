using PLMS.Model;
using System.Collections.Generic;
using System.ComponentModel;
using PLMS.Enum;

namespace PLMS.ViewModel
{
    class ParkingSpotViewModel
    {

        private ParkingSpot parkingSpot;

        public ParkingSpotViewModel()
        {
            parkingSpot = new ParkingSpot();
        }

        public List<ParkingSpot> GetAvailabeParkingSpotDetails()
        { 
            return parkingSpot.GetAvailableParkingSpots();
        }

        public bool CheckForVacantSpace()
        {
            return parkingSpot.CheckForEmptySpots();
        }

    }
}
