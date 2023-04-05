using System;
using System.Text.RegularExpressions;

namespace PLMS.Utils

{
    public static class Utility
    {
        static string regexEmail = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

        static string regexPhone = "^[6-9][0-9]{9}$";

        static string regexVehicleNumber = @"[A-Za-z]+(\s([A-Za-z0-9]+\s)+)[A-Za-z0-9]+";

        public static  bool validinput = true;

        public static bool _verifyEmail(string email) 
        {

            return Regex.IsMatch(email, regexEmail, RegexOptions.IgnoreCase);
        }


        public static bool _verifyPhone(long Phone)
        {

            return Regex.IsMatch(Phone.ToString(), regexPhone, RegexOptions.IgnoreCase);
        }

        public static void StyleMessage(string message, ConsoleColor color, ConsoleColor bgColor)
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static bool ValidateVEhicleNumber(string VehicleNumber)
        {
            Regex pattern = new Regex(regexVehicleNumber);
            Match match = pattern.Match(VehicleNumber);
            return match.Success;
            

        }
        public static int IntegerInputCheck(string message)
        {
            int input;
            while (true)
            {
                try
                {
                    Console.Write(message);
                    input = int.Parse(Console.ReadLine());

                    break;
                }
                catch (Exception exception)
                {
                    StyleMessage(exception.Message, ConsoleColor.Red, ConsoleColor.White);
                }
            }
            return input;
        }
        public static long LongInputCheck(string message)
        {
            long input;
            while (true)
            {
                try
                {
                    Console.Write(message);
                    input = long.Parse(Console.ReadLine());

                    break;
                }
                catch (Exception e)
                {
                    StyleMessage(e.Message, ConsoleColor.Red, ConsoleColor.White);
                }
            }
            return input;
        }

        public static DateTime DateInputCheck(string message)
        {
            DateTime date;
            while (true)
            {
                try
                {

                    Console.Write(message);
                    date = DateTime.Parse(Console.ReadLine());
                    int res = DateTime.Compare(date, DateTime.Now);
                    if (res > 0)
                    {
                        break;
                    }
                    else
                    {
                        Utility.StyleMessage("Don't Enter Past Date!!", ConsoleColor.Black, ConsoleColor.Red);
                    }
                }
                catch (Exception e)
                {

                    StyleMessage(e.Message, ConsoleColor.Red, ConsoleColor.White);
                }
            }

            return date;
        }

        public static bool DateCheck(DateTime date)
        {


            date = DateTime.Parse(Console.ReadLine());
            int res = DateTime.Compare(date, DateTime.Now);
            if (res < 0)
            {
                return true;
            }
            else
            {
                return false;

            }




        }

        public static DateTime TimeInputCheck(string message)
        {
            DateTime time;
            while (true)
            {
                try
                {

                    Console.Write(message);
                    time = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {

                    StyleMessage(e.Message, ConsoleColor.Red, ConsoleColor.White);
                }
            }

            return time;
        }


        public static string StringInputCheck(string message)
        {
            string input;
            while (true)
            {
                try
                {
                    Console.Write(message);
                    input = Console.ReadLine();
                    break;
                }
                catch (Exception e)
                {
                    StyleMessage(e.Message, ConsoleColor.Red, ConsoleColor.White);
                }
            }
            return input;
        }


        public static bool ValidatePassword(String password)
        {
            // Verify password length
            if (password.Length < 8 || password.Length > 16)
            {

                StyleMessage("Password must be in range of 8-16 characters.", ConsoleColor.Red, ConsoleColor.White);
                return false;
            }
            Regex regex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]+");

            if (regex.Match(password).Value == "")
            {

                StyleMessage("Password must contains alphanumeric characters ,atleast 1 uppercase letter and atleast 1 special symbol.", ConsoleColor.Red, ConsoleColor.White);
                return false;
            }

            return true;
        }

    }
}

