using System;
using PLMS.ViewModel;
using PLMS.Utils;

namespace PLMS.View
{
    class ParkingLotView
    {
        ParkingLotViewModel parkingLot = new ParkingLotViewModel();      

        public ParkingLotView()
        {
            

            parkingLot.CreateParkingLotDB();
            
            int Flag = 0;
            while (Flag == 0)
            {
                Console.WriteLine("\n---Select User Type--- \n\n1.) Admin\n2.) User\n3.) Exit");
                int userInput = Utility.IntegerInputCheck("YourChoice: ");
                switch (userInput)
                {
                    case 1:
                        AdminLoginView adminView = new AdminLoginView();
                        bool result = adminView.LoginView();
                        if (result)
                        {
                            AddParkingSpaceView addParkingSpaceView = new AddParkingSpaceView();
                            addParkingSpaceView.AddParkingSpace();
                        }
                        else
                        {
                            Utility.StyleMessage("Incorrect Credentials", ConsoleColor.Red, ConsoleColor.White);
                        }
                        break;
                    case 2:
                        UserView userView = new UserView();
                        userView.UserOptionsView();
                        break;
                    case 3:
                        Flag = 1;
                        break;
                    default:
                        Flag = 1;
                        break;
                }
            }


            
        }
    }
}
