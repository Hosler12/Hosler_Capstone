using FinchAPI;
using System;
using System.IO;
using System.Collections.Generic;


// Application:     Finch Control
// Author:          Hosler, Robert
// Description:     An application that allows the user to 
//          control the finch robot
// Date Created:    04/26/2020 16:15
// Date Revised:    04/26/2020 21:34
// Note: Some of the code was created by Velis, John

namespace Finch_Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            Welcome();
            Console.Clear();
            bool quitApplication = false;
            Finch finchRobot = new Finch();
            Connect(finchRobot);

            //variables

            int motors = 100;
            int faster = 0;
            int slower = 0;
            int quiet = 0;
            int red = 10;
            int rStop = 0;
            int blue = 10;
            int bStop = 0;
            int green = 10;
            int gStop = 0;
            int sound = 0;

            do
            {
                Console.Clear();
                //Get info and display it
                finchRobot.noteOn(sound);
                finchRobot.setLED(red, green, blue);
                if (red > 9)
                {
                    red = red - 1;
                }
                if (green > 9)
                {
                    green = green - 1;
                }
                if (blue > 9)
                {
                    blue = blue - 1;
                }
                int left = finchRobot.getLeftLightSensor();
                int right = finchRobot.getRightLightSensor();
                int both = (right + left) / 2;
                double temperature = finchRobot.getTemperature();
                double temperican = temperature * 1.8 + 32;
                Console.WriteLine($"Light is currently reading at {both}");
                Console.WriteLine($"Temperature in Farenheight is {temperican}, and {temperature} in celcius");
                Console.WriteLine();

                // Notifications

                Console.WriteLine("Notifications");
                int notifications = 0;
                if (faster > 0)
                {
                    Console.WriteLine("Can't go Faster");
                    faster--;
                    notifications++;
                }
                if (slower > 0)
                {
                    Console.WriteLine("Can't go slower");
                    slower--;
                    notifications++;
                }
                if (quiet > 0)
                {
                    Console.WriteLine("Can't be quieter");
                    quiet--;
                    notifications++;
                }
                if (rStop > 0)
                {
                    Console.WriteLine("Can't be more red");
                    rStop--;
                    notifications++;
                }
                if (bStop > 0)
                {
                    Console.WriteLine("Can't be more blue");
                    bStop--;
                    notifications++;
                }
                if (gStop > 0)
                {
                    Console.WriteLine("Can't be more green");
                    gStop--;
                    notifications++;
                }
                if (notifications == 0)
                {
                    Console.WriteLine("No new notifications");
                }

                // Instructions
                Console.WriteLine();
                Console.WriteLine("Due to C# limitations, only one command can be accepted at any given time. You don't need to hold keys down.");
                Console.WriteLine("Holding keys down will cause application screen to flicker. You have been warned.");
                Console.WriteLine();
                Console.WriteLine("Press Z to stop moving");
                Console.WriteLine("Use W to move Forward, S to go backward");
                Console.WriteLine("Use A to move Left, D to move right");
                Console.WriteLine("Use E to move Faster, Q to move slower");
                Console.WriteLine("Y will make the finch be louder, T will make it quieter");
                Console.WriteLine("The colors of the robot will change gradually by pressing F, G, and H.");
                Console.WriteLine("F will change the amount of red, G changes green, and H alters blue.");
                Console.WriteLine("These values will gradually lower");
                Console.WriteLine();
                Console.WriteLine("Use P to Quit");

                ConsoleKeyInfo command = Console.ReadKey();
                string input = command.KeyChar.ToString();
                if (input == "w")
                {
                    finchRobot.setMotors(motors, motors);
                }
                else if (input == "s")
                {
                    finchRobot.setMotors(-motors, -motors);
                }
                else if (input == "a")
                {
                    finchRobot.setMotors(-motors, motors);
                }
                else if (input == "d")
                {
                    finchRobot.setMotors(motors, -motors);
                }
                else if (input == "z")
                {
                    finchRobot.setMotors(0, 0);
                }
                else if (input == "e")
                {
                    if (motors < 181)
                    {
                        motors = motors + 10;
                    }
                    else
                    {
                        faster = 10;
                    }
                }
                else if (input == "q")
                {
                    if (motors > 19)
                    {
                        motors = motors - 10;
                    }
                    else
                    {
                        slower = 10;
                    }
                }
                else if (input == "y")
                {
                    sound = sound + 10;
                }
                else if (input == "t")
                {
                    if (sound > 1)
                    {
                        sound = sound - 10;
                    }
                    else
                    {
                        quiet = 10;
                    }
                }
                else if (input == "f")
                {
                    if (red < 249)
                    {
                        red = red + 20;
                    }
                    else
                    {
                        rStop = 10;
                    }
                }
                else if (input == "g")
                {
                    if (green < 249)
                    {
                        green = green + 20;
                    }
                    else
                    {
                        gStop = 10;
                    }
                }
                else if (input == "h")
                {
                    if (blue < 249)
                    {
                        blue = blue + 20;
                    }
                    else
                    {
                        bStop = 10;
                    }
                }
                else if (input == "p")
                {
                    quitApplication = true;
                }
                
                input = "";
            }
            while (!quitApplication);

            Disconnect(finchRobot);
        }

        /// <summary>
        /// Disconnect and close
        /// </summary>
        /// <param name="finchRobot"></param>
        static void Disconnect(Finch finchRobot)
        {
            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnected.");
            Console.WriteLine();
            Console.WriteLine("Thank you for using my application!");
            Continue();
        }
       
        /// <summary>
        /// Connect to finch robot
        /// </summary>
        /// <param name="finchRobot"></param>
        /// <returns></returns>
        static bool Connect(Finch finchRobot)
        {
            bool robotConnected = false; 
            string Connecting = " ";
            do
            {
                Console.Clear();
                Console.WriteLine("Please make sure the finch is plugged in.");
                Console.WriteLine("If you are having issues, please reopen the program with the finch plugged in.");
                Console.WriteLine("Press c to connect your finch robot.");
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.KeyChar == 'c')
                {
                    Connecting = "Go";
                }
            }
            while (Connecting != "Go");

            Console.WriteLine("\tConnecting to Finch robot. The light and sound should be active for 2 seconds");
            robotConnected = finchRobot.connect();
            do
            {
                if (robotConnected == true)
                {
                    finchRobot.setLED(10, 10, 10);
                    finchRobot.noteOn(50);
                    finchRobot.wait(2000);
                    finchRobot.setLED(0, 0, 0);
                    finchRobot.noteOff();
                    Connecting = "Clear";
                    Console.Clear();
                }
                else
                {
                    finchRobot.disConnect();
                    Connect(finchRobot);
                }
            } while (Connecting != "Clear");
            return robotConnected;
        }
       
        /// <summary>
        /// Setup the Console & welcome user.
        /// </summary>
        static void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("\tFinch Robot Control");
            Console.WriteLine();
            Console.WriteLine("Please make sure your Finch robot is plugged in.");
            Continue();
        }
       
        /// <summary>
        /// Continue prompt
        /// </summary>
        static void Continue()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }
    }
}