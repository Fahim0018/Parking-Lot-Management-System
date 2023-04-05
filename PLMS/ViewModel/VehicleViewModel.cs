using PLMS.Model;
using System;
using System.ComponentModel;
using PLMS.Enum;

namespace PLMS.ViewModel
{
    class VehicleViewModel
    {

        private ParkingFeeStructure parkingFee;

        private ParkingSpaceViewModel parkingSpaceVM;

        public Vehicle vehicle;

        public VehicleViewModel()
        {
            parkingFee = new ParkingFeeStructure();
            parkingSpaceVM = new ParkingSpaceViewModel();
            vehicle = new Vehicle();
        }
        


        public (bool,ParkingSpot) VehicleEntry(Vehicle Vehicle)
        {      
            
            if (!CheckForVehicle(Vehicle.VehicleLicenceNumber))
            {
                          
                ParkingSpot parkingSpot = vehicle.VehicleEntryInParkingLot(Vehicle);

                if (parkingSpot.SpotCategory == VehicleCategory.Bike)
                {
                    parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Bike, parkingSpot.Floor, UpdateParkingSpace.Decrease, 1);
                }
                if (parkingSpot.SpotCategory == VehicleCategory.Car)
                {
                    parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Car, parkingSpot.Floor, UpdateParkingSpace.Decrease, 1);
                }
                if (parkingSpot.SpotCategory == VehicleCategory.Bus)
                {
                    parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Bus, parkingSpot.Floor, UpdateParkingSpace.Decrease, 1);
                }
                if (parkingSpot.SpotCategory == VehicleCategory.None)
                {
                    parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Bike, parkingSpot.Floor, UpdateParkingSpace.Decrease, 0);
                }
                return (false, parkingSpot);
            }
            else
            {
                return (true,null);
            }

        }


        public decimal VehicleExit(string vehicleLicenceNumber)
        {
            ParkingSpot parkingSpot = vehicle.VehicleExitFromParkingLot(vehicleLicenceNumber);           
            if (parkingSpot.SpotCategory == VehicleCategory.Bike)
            {
                parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Bike, parkingSpot.Floor, UpdateParkingSpace.Increase, 1);
            }
            else if (parkingSpot.SpotCategory == VehicleCategory.Car)
            {
                parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Car, parkingSpot.Floor, UpdateParkingSpace.Increase, 1);
            }
            else if (parkingSpot.SpotCategory == VehicleCategory.Bus)
            {
                parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Bus, parkingSpot.Floor, UpdateParkingSpace.Increase, 1);
            }
           
            return parkingFee.CalculateParkingFee(vehicleLicenceNumber,parkingSpot.ParkingSpotID, parkingSpot.Floor);
        }



        public bool CheckForVehicle(string VehicleNumber)
        {
            return vehicle.CheckParkedVehicle(VehicleNumber);
        }

    }
}
