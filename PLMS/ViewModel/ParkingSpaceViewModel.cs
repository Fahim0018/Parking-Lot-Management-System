using PLMS.Model;
using System.ComponentModel;
using PLMS.Enum;

namespace PLMS.ViewModel
{
    class ParkingSpaceViewModel
    {       

        public ParkingSpace parkingSpace;

        public ParkingSpaceViewModel()
        {
            parkingSpace = new ParkingSpace();
        }
        
        public void CreateParkingSpace(ParkingSpace Parkingspace)
        {
          
            parkingSpace.AddParkingSpace(Parkingspace);
        }

        public void UpdateParkingSpace(VehicleCategory vehicleCategory,int floor,UpdateParkingSpace updateType,int space)
        {
            parkingSpace.UpdateParkingSpace(vehicleCategory, floor, updateType, space);
           
        }
    }
}
