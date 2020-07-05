
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
            string usecom = "COM4";
            try
            {
                using (ControlAPI eleksTubeAPI = new ControlAPI(usecom))
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
                Console.WriteLine(usecom + " is Using");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
