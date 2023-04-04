using PLMS.Database;
namespace PLMS.Model
{

    class Admin 
    {
        private string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }

        private int _ID;
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }


        private string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }

        private AdminDataAccess adminData;


        public Admin()
        {
            adminData = new AdminDataAccess();
        }
        
        public void AddAdmin(Admin admin)
        {          
            adminData.InsertAdminInDB(admin);
        }

        public bool AdminLogin(string UserName,string PassWord)
        {
           
            return adminData.Login(UserName, PassWord);
        }
    }
}

