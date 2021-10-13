using System;
using System.Linq;
using System.Globalization;

namespace PragueParkingVersionVG
{

    class Program
    {
        public static void Main(string[] args)
        {
            int origWidth, width;
            int origHeight, height;
            width = 180;
            height = 80;
            Console.SetWindowSize(width, height);
            origWidth = Console.WindowWidth;
            origHeight = Console.WindowHeight;
            string time = DateTime.Now.ToString("HH:mm");
            Console.WriteLine(DateTime.Now.ToString("HH:mm"));
            string[] parking = new string[101];
            Array.Fill(parking, "LEDIGT");
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu(parking, time);
            }
            Console.ReadKey(true);
        }
        public static bool MainMenu(string[] parking, string time)
        {

            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Clear();
                Console.WriteLine(string.Format("{0:HH:mm}", DateTime.Now));
                Console.WriteLine("Hi! And Welcome to Prague parking assistance!");
                Console.WriteLine("How can i be of service?");
                Console.WriteLine("1) Park a vehicle");
                Console.WriteLine("2) Relocate vehicles");
                Console.WriteLine("3) Search for a vehicle by registration number");
                Console.WriteLine("Q) Quit");
                Console.WriteLine("5) Print the array");
                Console.WriteLine("Make a selection:");

                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                        {
                            ParkVehicle(parking, time);
                            return true;
                        }
                    case "2":
                        {
                            RelocateVehicle(parking, time);
                            return true;
                        }
                    case "3":
                        {
                            SearchVehicle(parking);
                            return true;
                        }
                    case "q":
                        {
                            Console.WriteLine("Thanks for using Prague parking assistance");
                            Console.WriteLine("Have a nice day!");
                            Environment.Exit(0);
                            return false;
                        }
                    case "5":
                        {
                            PrintArray(parking);
                            return true;
                        }
                    default:
                        {
                            Console.WriteLine("Error: Invalid Input");
                            return true;
                        }
                }
            }
        }

        public static void SearchVehicle(string[] parking)
        {
            Console.WriteLine("Car or MC");
            switch (Console.ReadLine().ToUpper())
            {
                case "CAR":
                    {
                        Console.WriteLine("Type the registration number: ");
                        string hit = "CAR" + "_" + Console.ReadLine().ToUpper() + "%";
                        for (int i = 1; i < parking.Length - 1; i++)
                        {
                            if (hit == parking[i])
                            {
                                Console.WriteLine("{0} is located at {1}", hit, i);
                            }
                        }
                        Console.ReadKey();
                        return;
                    }
                case "MC":
                    {
                        Console.WriteLine("Type the registration number: ");
                        string hit = "MC" + "_" + Console.ReadLine().ToUpper();
                        for (int i = 100; i > 1; i--)
                        {
                            if (hit == parking[i])
                            {
                                continue;
                            }
                            if (parking[i].Contains('#'))
                            {
                                Console.WriteLine("{0} is located at {1}", hit, i);
                                break;
                            }

                        }
                        Console.ReadKey();
                        return;
                    }

                default:
                    {
                        Console.WriteLine("Invalid Input!");
                        return;
                    }


            }

        }

        public static void RelocateVehicle(string[] parking, string time)
        {

            Console.WriteLine("Car or MC");
            switch (Console.ReadLine().ToUpper())
            {
                case "CAR":
                    {
                        Console.WriteLine("Type in the registration number: ");
                        string hit = "CAR" + "_" + Console.ReadLine().ToUpper();
                        for (int i = 1; i < parking.Length; i++)
                        {

                            if (parking[i].Contains(hit.Substring(0, hit.Length)))
                            {
                                hit = hit + "(" + time + ")" + '%';
                                Console.WriteLine("{0} is located at {1}", hit, i);
                                Console.WriteLine("Do you wish to relocate? (y/n)");
                                string answer = Console.ReadLine().ToUpper();
                                string yes = "Y";
                                string no = "N";
                                if (answer == yes)
                                {
                                    Console.WriteLine("Enter a parkingspot: (1-100)");
                                    string relocate = Console.ReadLine();
                                    int index = int.Parse(relocate);
                                    if (!parking[index].Contains("LEDIGT"))
                                    {
                                        Console.WriteLine("Spot taken, Press any key to continue...");
                                        Console.ReadKey();
                                        break;
                                    }
                                    var buffer = parking[i];
                                    parking[i] = parking[index];
                                    parking[index] = buffer;
                                    Console.WriteLine("Car: {0}, Successfully moved to spot : {1}", hit, index);
                                    Console.ReadKey();
                                }
                                while (answer == no)
                                {
                                    break;
                                }
                                break;
                            }

                        }
                        break;
                    }
                case "MC":
                    {
                        string hit;
                        Console.WriteLine("Type the registration number: ");
                        hit = "MC" + "_" + Console.ReadLine().ToUpper();
                        int indexTest = hit.IndexOf('%', 0);
                        for (int i = 100; i < parking.Length; i--)
                        {
                            //TESTER

                            //int indexerare = parking[i].IndexOf('(', 0);
                            //int indexOne = indexerare + 1;
                            //int indexerareTwo = parking[i].IndexOf(')', 0);
                            //var timer = parking[i].Substring(parking[i].IndexOf(')', indexOne -1));
                            //if (!parking[i].Contains(hit + '#'))
                            //{
                            //    if (!parking[i].Contains(hit + '%'))
                            //    {
                            //    Console.WriteLine("Invalid Input! (Error). Press any key to continue...");
                            //    break;
                            //    }

                            //}

                            if (parking[i].Contains(hit + '(' + time + ')' + '#'))
                            {
                                hit = hit + '(' + time + ')' + '#';
                            }
                            else if (parking[i].Contains(hit + '(' + time + ')' + '%'))
                            {
                                hit = hit + '(' + time + ')' + '%';
                            }
                            if (!parking[i].Contains(hit))
                            {
                                Console.WriteLine("Invalid Input! (Error). Press any key to continue...");
                                break;
                            }
                            if (parking[i].Contains(hit))
                            {
                                Console.WriteLine("{0} is located at {1}", hit, i);
                                Console.WriteLine("Do you wish to relocate? (y/n)");
                                string answer = Console.ReadLine().ToUpper();
                                string yes = "Y";
                                string no = "N";
                                if (answer == yes)
                                {
                                    Console.WriteLine("Enter a Parkingspot: (1-100)");
                                    string relocate = Console.ReadLine();
                                    int index = int.Parse(relocate);
                                    if (parking[index].Contains('%'))
                                    {
                                        Console.WriteLine("Spot taken, Press any key to continue...");
                                        Console.ReadKey();
                                        break;
                                    }
                                    if (hit.Contains('%'))
                                    {
                                        var IndexRemover = hit.IndexOf('%', 0);
                                        parking[i] = parking[i].Remove(IndexRemover);
                                        parking[i] = parking[i] + '#';
                                    }
                                    else if (hit.Contains('#'))
                                    {
                                        parking[i] = parking[i].Remove(0, hit.Length);
                                        //var IndexRemover = parking[i].IndexOf(')', 0);
                                        //parking[i] = parking[i].Remove(IndexRemover);
                                        parking[i] = parking[i].Replace('%', '#');
                                    }
                                    if (parking[index].Contains("LEDIGT"))
                                    {
                                        parking[index] = String.Empty;
                                        hit = hit.Replace('%', '#');
                                        parking[index] = hit;
                                    }
                                    else if (parking[index].Contains('#'))
                                    {
                                        Console.WriteLine("Vill du parkera din MC bredvid {0}? (y/n)", parking[index]);
                                        answer = Console.ReadLine().ToUpper();

                                        if (answer == yes)
                                        {
                                            hit = hit.Replace('#', '%');
                                            parking[index] += string.Join('#', hit);
                                            parking[i] = "LEDIGT";
                                        }
                                        while (answer == no)
                                        {
                                            Console.WriteLine("Too bad. Press any key to continue...");
                                            Console.ReadKey();
                                            break;
                                        }

                                    }
                                    Console.WriteLine("MC: {0}, Successfully moved to spot : {1}", hit, index);
                                    Console.ReadKey();
                                }
                                break;
                            }


                        }
                        break;


                    }


            }
            MainMenu(parking, time);
        }
        public static void ParkVehicle(string[] parking, string time)
        {
            string car = "car".ToUpper();
            string mc = "mc".ToUpper();
            Console.WriteLine("What type of vehicle do you want to park?");
            switch (Console.ReadLine().ToLower())
            {
                case "car":
                    {
                        ParkingCar(car, parking, time);
                        DoneParking(parking, time);
                        break;
                    }
                case "mc":
                    {
                        ParkingMC(mc, parking, time);
                        DoneParking(parking, time);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public static void ParkingCar(string car, string[] parking, string time)
        {

            string regNumberCar;
            Console.WriteLine("Skriv in ditt regnr:");
            regNumberCar = Console.ReadLine().ToUpper();
            while (!regNumberCar.Contains("CAR_"))
            {
                if (regNumberCar.Length <= 10)
                {
                    regNumberCar = "CAR" + '_' + regNumberCar;
                }
                else if (regNumberCar.Length > 10)
                {
                    Console.WriteLine("Too many Chars");
                    break;
                }

                for (int i = 1; i < parking.Length; i++)
                {
                    if (parking[i].Contains('%'))
                    {
                        continue;
                    }
                    if (parking[i].Contains("LEDIGT"))
                    {
                        string timeStamp = DateTime.Now.ToString("HH:mm");
                        parking[i] = regNumberCar + "(" + timeStamp + ")" + '%';
                        break;
                    }
                    break;
                }
            }
        }

        public static void ParkingMC(string mc, string[] parking, string time)
        {
            string regNumberMc;
            Console.WriteLine("Skriv in ditt regnr: ");
            regNumberMc = Console.ReadLine().ToUpper();
            while (!regNumberMc.Contains("MC_"))
            {
                if (regNumberMc.Length <= 10)
                {
                    regNumberMc = "MC" + '_' + regNumberMc;
                }
                else if (regNumberMc.Length > 10)
                {
                    Console.WriteLine("Too many Chars");
                    break;
                }

                for (int i = 100; i > 1; i--)
                {

                    string timeStamp = DateTime.Now.ToString("HH:mm");
                    if (parking[i].Contains('%'))
                    {
                        continue;
                    }
                    if (parking[i].Contains('#'))
                    {
                        parking[i] += regNumberMc + "(" + timeStamp + ")" + '%';
                        break;
                    }
                    if (parking[i].Contains("LEDIGT"))
                    {
                        parking[i] = regNumberMc + "(" + timeStamp + ")" + '#';
                        break;
                    }

                }
            }
        }

        public static void DoneParking(string[] parking, string time)
        {
            Console.WriteLine("Har du Parkerat klart?(y/n)");
            switch (Console.ReadLine().ToLower())
            {
                case "y":
                    {
                        break;
                    }
                default:
                    {
                        ParkVehicle(parking, time);
                        break;
                    }
            }

            //string answer = Console.ReadLine().ToLower();
            //string yes1 = "y";
            //string no1 = "n";
            //if (answer == yes1)
            //{

            //    //MainMenu(parking,time);
            //}
            //while (answer == no1)
            //{
            //    ParkVehicle(parking,time);
            //    break;
            //}
        }

        public static void PrintArray(string[] parking)
        {
            Console.Clear();
            int y = 0;
            for (int j = 1; j < 21; j++)
            {

                Console.SetCursorPosition(0, y);
                if (parking[j].Contains("LEDIGT"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}:", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" {0}", parking[j]);
                    Console.ResetColor();

                }
                if (!parking[j].Contains("LEDIGT"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}:", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(" {0}", parking[j]);
                    Console.ResetColor();
                }
                y++;
            }
            int q = 0;
            for (int j = 21; j < 41; j++)
            {
                Console.SetCursorPosition(40, q);
                if (parking[j].Contains("LEDIGT"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}:", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" {0}", parking[j]);
                    Console.ResetColor();
                }
                if (!parking[j].Contains("LEDIGT"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}:", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(" {0}", parking[j]);
                    Console.ResetColor();
                }
                q++;
            }
            int d = 0;
            for (int j = 41; j < 61; j++)
            {
                Console.SetCursorPosition(80, d);
                if (parking[j].Contains("LEDIGT"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}:", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" {0}", parking[j]);
                    Console.ResetColor();
                }
                if (!parking[j].Contains("LEDIGT"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}:", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(" {0}", parking[j]);
                    Console.ResetColor();
                }
                d++;
            }
            int z = 0;
            for (int j = 61; j < 81; j++)
            {
                Console.SetCursorPosition(120, z);
                if (parking[j].Contains("LEDIGT"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}:", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" {0}", parking[j]);
                    Console.ResetColor();
                }
                if (!parking[j].Contains("LEDIGT"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}:", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(" {0}", parking[j]);
                    Console.ResetColor();
                }
                z++;
            }
            int w = 0;
            for (int j = 81; j < 101; j++)
            {
                Console.SetCursorPosition(160, w);
                if (parking[j].Contains("LEDIGT"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}:", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" {0}", parking[j]);
                    Console.ResetColor();
                }
                if (!parking[j].Contains("LEDIGT"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}:", j);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(" {0}", parking[j]);
                    Console.ResetColor();
                }
                w++;




            }


            Console.SetCursorPosition(0, 26);
            Console.ReadKey();
            //Console.ForegroundColor = ConsoleColor.DarkYellow;
            /*Console.WriteLine("Press any Key to Continue...");*/

        }
        //public static void PrintArray(string[] parking)
        //{
        //    Console.Clear();
        //    for (int j = 1; j < parking.Length; j++)
        //    {

        //        if (parking[j].Contains("LEDIGT"))
        //        {
        //            Console.ForegroundColor = ConsoleColor.DarkYellow;
        //            Console.Write("Plats: {0}", j);
        //            Console.ResetColor();
        //            Console.ForegroundColor = ConsoleColor.Green;
        //            Console.WriteLine(" {0}", parking[j]);
        //            Console.ResetColor();
        //        }
        //        if (!parking[j].Contains("LEDIGT"))
        //        {
        //            Console.ForegroundColor = ConsoleColor.DarkYellow;
        //            Console.Write("Plats: {0}", j);
        //            Console.ResetColor();
        //            Console.ForegroundColor = ConsoleColor.DarkRed;
        //            Console.WriteLine(" {0}", parking[j]);
        //            Console.ResetColor();
        //        }
        //    }
        //    Console.ForegroundColor = ConsoleColor.DarkYellow;
        //    Console.WriteLine("Press any Key to Continue..."); Console.ReadKey();
        //}
    }
}