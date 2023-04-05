
using PLMS.ViewModel;
using System;
using System.Collections.Generic;
using PLMS.Utils;
using PLMS.Enum;
using PLMS.Model;

namespace PLMS.View
{
    class UserView
    {
        private VehicleViewModel vehicleVM;

        private ParkingSpotViewModel parkingspotVM;

        public UserView()
        {
            vehicleVM = new VehicleViewModel();
            parkingspotVM = new ParkingSpotViewModel();
        }
        

        public void UserOptionsView()
        {
            int flag = 0;
            while (flag == 0)
            {
                Console.WriteLine("\n\n1.) Vehicle Entry\n2.) Vehicle Exit\n3.) View Available Spots\n4.) Exit");
                int userinput1 = Utility.IntegerInputCheck("Enter Your Choice: ");
                switch (userinput1)
                {
                    case 1:
                        
                        int flag1 = 0;
                        while (flag1 == 0)
                        {
                         
                            if (parkingspotVM.CheckForVacantSpace())
                            {

                                while (true)
                                {                                 
                                    vehicleVM.vehicle.VehicleLicenceNumber = Utility.StringInputCheck("\nEnter Vehicle Licence Number(XX 88 XY 8888): ").ToUpper();
                                    if (Utility.ValidateVEhicleNumber(vehicleVM.vehicle.VehicleLicenceNumber))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Utility.StyleMessage("\nEnter Valid Vehicle Number!!", ConsoleColor.Red, ConsoleColor.White);
                                    }
                                }
                                Console.WriteLine("\n----Select vehicle Type----\n1.)Bike\n2.)Car\n3.)Bus");
                                int flag2 = 0;
                                while (flag2 == 0)
                                {
                                    int userinput2 = Utility.IntegerInputCheck("Your Choice: ");
                                    if (userinput2 == 1)
                                    {
                                        vehicleVM.vehicle.VehicleCategory = VehicleCategory.Bike;
                                        flag2 = 1;
                                    }
                                    else if (userinput2 == 2)
                                    {
                                        vehicleVM.vehicle.VehicleCategory = VehicleCategory.Car;
                                        flag2 = 1;
                                    }
                                    else if (userinput2 == 3)
                                    {
                                        vehicleVM.vehicle.VehicleCategory = VehicleCategory.Bus;
                                        flag2 = 1;
                                    }
                                    else
                                    {
                                        Utility.StyleMessage("\nEnter Correct Option!!", ConsoleColor.Red, ConsoleColor.White);
                                    }
                                }

                                vehicleVM.vehicle.VehicleInDateTime = DateTime.Now;
                                vehicleVM.vehicle.IsParked = true;
                                (bool result, ParkingSpot parkingSpot) = vehicleVM.VehicleEntry(vehicleVM.vehicle);
                                if (!result)
                                {
                                    if (parkingSpot.Floor != 0)
                                    {
                                        Utility.StyleMessage($"\nYour Parking Spot is Floor {parkingSpot.Floor} Spot {parkingSpot.ParkingSpotID}", ConsoleColor.DarkGreen, ConsoleColor.White);           
                                    }
                                    else
                                    {
                                        Utility.StyleMessage("\nNo Space Available!!", ConsoleColor.Red, ConsoleColor.White);                            
                                    }
                                    flag1 = 1;
                                }
                                else
                                {
                                    Utility.StyleMessage("Vehicle Already Parked!!", ConsoleColor.Red, ConsoleColor.White);
                                }
                            }
                            else
                            {
                                Utility.StyleMessage("\nNo Space Available!!", ConsoleColor.Red, ConsoleColor.White);
                                flag1 = 1;

                            }
                        }
                        break;


                    case 2:
                        int flag3 = 0;
                        while (flag3 == 0)
                        {
                            string vehicleNumber;
                            while (true)
                            {
                                vehicleNumber = Utility.StringInputCheck("\nEnter Vehicle Licence Number(XX 88 XY 8888): ").ToUpper();
                                if (Utility.ValidateVEhicleNumber(vehicleNumber))
                                {
                                    break;
                                }
                                else
                                {
                                    Utility.StyleMessage("\nEnter Valid Vehicle Number!!", ConsoleColor.Red, ConsoleColor.White);
                                }
                            }
                           
                            if (vehicleVM.CheckForVehicle(vehicleNumber))
                            {
                                decimal parkingFee = vehicleVM.VehicleExit(vehicleNumber);
                                Utility.StyleMessage($"\nParking Fee is {parkingFee}", ConsoleColor.Black, ConsoleColor.Green);
                                flag3 = 1;
                            }
                            else
                            {
                                Utility.StyleMessage("Enter Correct Vehicle Number!!", ConsoleColor.Red, ConsoleColor.White);
                            }
                        }

                        break;


                    case 3:
                        if (parkingspotVM.GetAvailabeParkingSpotDetails().Count != 0)
                        {
                            Console.WriteLine("\n\n---------------------------------------");                      
                            Console.WriteLine("|Floor    |Spot Number   |Category    |");
                            Console.WriteLine("---------------------------------------");

                        
                            foreach (ParkingSpot parkingSpot in parkingspotVM.GetAvailabeParkingSpotDetails())
                            {
                                if (parkingSpot.SpotCategory == VehicleCategory.Bike)
                                {
                                    Console.WriteLine($"|{parkingSpot.Floor}        |{parkingSpot.ParkingSpotID}             |{parkingSpot.SpotCategory}        |");
                                }
                                else
                                {
                                    Console.WriteLine($"|{parkingSpot.Floor}        |{parkingSpot.ParkingSpotID}             |{parkingSpot.SpotCategory}         |");
                                }
                            }
                            Console.WriteLine("---------------------------------------\n");
                        }
                        else
                        {
                            Utility.StyleMessage("\nNo Space Available!!", ConsoleColor.Red, ConsoleColor.White);
                        }
                       
                        
                        break;


                    case 4:
                        flag = 1;
                        break;


                    default:
                        flag = 1;
                        break;


                }
            }

        }


    }
}
