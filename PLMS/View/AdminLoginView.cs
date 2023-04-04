using PLMS.ViewModel;
using PLMS.Utils;

namespace PLMS.View
{
    class AdminLoginView
    {
        private AdminViewModel adminViewModel;

        public AdminLoginView()
        {
            adminViewModel = new AdminViewModel();
        }

        static bool IsloggedIn = false;

       
        public bool LoginView()
        {
            
            string username = Utility.StringInputCheck("\nEnter UserName : ");
            
            string password = Utility.StringInputCheck("Enter Password: ");

            IsloggedIn = adminViewModel.AdminLogin(username, password);

            return IsloggedIn;
        }
    }
}
