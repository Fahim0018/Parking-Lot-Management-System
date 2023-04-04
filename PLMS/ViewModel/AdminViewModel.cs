using PLMS.Model;
using System.ComponentModel;

namespace PLMS.ViewModel
{
    class AdminViewModel
    {

        private Admin admin;

        public  AdminViewModel()
        {
            admin = new Admin();

            admin.Name = "fahim";
            admin.ID = 1;
            admin.Password = "fahim@123";
            admin.Email = "fahim@gmail.com";

        }

        public bool AdminLogin(string username,string password)
        {
            return admin.AdminLogin(username, password);
        }

    }
}