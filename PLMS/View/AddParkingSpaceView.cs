using PLMS.ViewModel;
using System;
using PLMS.Utils;

namespace PLMS.View
{
    class AddParkingSpaceView
    {

        private ParkingSpaceViewModel parkingSpaceVM;

        public AddParkingSpaceView()
        {
            parkingSpaceVM = new ParkingSpaceViewModel();
        }
        public void AddParkingSpace()
        {
          
            int FloorNumber = Utility.IntegerInputCheck("\nEnter The Number Of Floors: ");
           
            int TotalSpaceAvailable = Utility.IntegerInputCheck("Enter Number Of Spots: ");

            for (int i = 1; i <= FloorNumber; i++)
            {

                int flag = 1;
                while (flag!=0)
                {
                    Console.WriteLine("\nFloor" + i);

                    int SpaceForBike = Utility.IntegerInputCheck("No. Of spots For Bike: ");

                    int SpaceForCar = Utility.IntegerInputCheck("No. Of spots For Car: ");

                    int SpaceForBus = Utility.IntegerInputCheck("No. Of spots For Bus: ");

                    if (SpaceForBike + SpaceForCar + SpaceForBus == TotalSpaceAvailable)
                    {
                        
                        parkingSpaceVM.parkingSpace.SpaceForBike = SpaceForBike;
                        parkingSpaceVM.parkingSpace.SpaceForBus = SpaceForBus;
                        parkingSpaceVM.parkingSpace.SpaceForCar = SpaceForCar;
                        parkingSpaceVM.CreateParkingSpace(parkingSpaceVM.parkingSpace);
                        flag = 0;
                    }
                    else
                    {
                       
                        Utility.StyleMessage("Enter Correct Number of Spots!!!", ConsoleColor.Red, ConsoleColor.White);

                    }
                }
                
            }
        }

        
    }
}
