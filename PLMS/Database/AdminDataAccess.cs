using PLMS.Model;
using System.Data.SQLite;

namespace PLMS.Database
{
    class AdminDataAccess 
    {

        object locker = new object();

        DataBaseConnection database = new DataBaseConnection();

        SQLiteCommand command;



        public void InsertAdminInDB(Admin admin)
        {

            database.OpenConnection();
            using (command = database.MySqliteConnection.CreateCommand())
            {

                command.CommandText = QueryConstants.InsertAdmin;
                command.Parameters.AddWithValue("$name", admin.Name);
                command.Parameters.AddWithValue("$id", admin.ID);
                command.Parameters.AddWithValue("$password", admin.Password);
                command.Parameters.AddWithValue("$email", admin.Email);
                command.ExecuteNonQuery();

            }
            database.CloseConnection();

        }

        public bool Login(string username, string password)
        {
            database.OpenConnection();
            lock (locker)
            {
                using (command = database.MySqliteConnection.CreateCommand())
                {

                    command.CommandText = QueryConstants.getAdminDetails;

                    SQLiteDataReader reader = command.ExecuteReader();
                    int flag = 0;
                    while (reader.Read())
                    {
                        if (reader["Name"].Equals(username) && reader["Password"].Equals(password))
                        {

                            flag = 1;
                        }

                    }
                    reader.Close();
                    if (flag == 1)
                    {

                        return true;
                    }
                    else
                    {

                        return false;
                    }
                }

            }

        }


    }
}
