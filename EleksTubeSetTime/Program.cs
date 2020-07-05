
using EleksTubeAPI;
using System;
using System.Drawing;
using System.Threading;

namespace EleksTubeSetTime
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    using (ControlAPI eleksTubeAPI = new ControlAPI(args[0]))
                    {
                        eleksTubeAPI.colorMode = ColorMode.Flow;
                        eleksTubeAPI.timeMode = TimeMode.XXIVh;
                        eleksTubeAPI.AllColor = Color.Orange;
                        eleksTubeAPI.SendMode();
                        eleksTubeAPI.SendNow();
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(args[0] + " is Using");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                Console.WriteLine("using: EleksTubeSetTime <SerialPortName>");
                Console.WriteLine("SerialPortName");
                Console.WriteLine("--------");
                ControlAPI.PortList();
                Console.WriteLine("--------");
                Console.WriteLine("Exit to enter key...");
                Console.ReadLine();
            }

        }
    }
}
