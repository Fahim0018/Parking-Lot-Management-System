using PLMS.Model;
using System.ComponentModel;

namespace PLMS.ViewModel
{
    class ParkingLotViewModel
    {

        private ParkingLot _newParkingLot;

        public ParkingLotViewModel()
        {
            _newParkingLot = new ParkingLot();
        }

        public void CreateParkingLotDB()
        {  
            _newParkingLot.CreateParkigLot();
        }
    }
}
