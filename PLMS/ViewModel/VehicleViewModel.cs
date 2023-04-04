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
        


        public (bool,int,int) VehicleEntry(Vehicle Vehicle)
        {         
            if (!CheckForVehicle(Vehicle.VehicleLicenceNumber))
            {
                          
                (VehicleCategory parkingSpotCategory,int spot ,int floor) = vehicle.VehicleEntryInParkingLot(Vehicle);

                if (parkingSpotCategory == VehicleCategory.Bike)
                {
                    parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Bike, floor, UpdateOperation.Decrease, 1);
                }
                if (parkingSpotCategory == VehicleCategory.Car)
                {
                    parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Car, floor, UpdateOperation.Decrease, 1);
                }
                if (parkingSpotCategory == VehicleCategory.Bus)
                {
                    parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Bus, floor, UpdateOperation.Decrease, 1);
                }
                if (parkingSpotCategory == VehicleCategory.None)
                {
                    parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Bike, floor, UpdateOperation.Decrease, 0);
                }
                return (false,spot,floor);
            }
            else
            {
                return (true,0,0);
            }

        }


        public decimal VehicleExit(string vehicleLicenceNumber)
        {
            ( int Floor, int SpotID,VehicleCategory ParkingSpotCategory) = vehicle.VehicleExitFromParkingLot(vehicleLicenceNumber);           
            if (ParkingSpotCategory == VehicleCategory.Bike)
            {
                parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Bike, Floor, UpdateOperation.Increase, 1);
            }
            else if (ParkingSpotCategory == VehicleCategory.Car)
            {
                parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Car, Floor, UpdateOperation.Increase, 1);
            }
            else if (ParkingSpotCategory == VehicleCategory.Bus)
            {
                parkingSpaceVM.UpdateParkingSpace(VehicleCategory.Bus, Floor, UpdateOperation.Increase, 1);
            }
           
            return parkingFee.CalculateParkingFee(vehicleLicenceNumber,SpotID,Floor);
        }



        public bool CheckForVehicle(string VehicleNumber)
        {
            return vehicle.CheckParkedVehicle(VehicleNumber);
        }

    }
}
