using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace EleksTubeAPI
{

    /// <summary>
    /// https://forum.eleksmaker.com/topic/1941/elekstube-api-control-protocol
    /// maybe support v6.2
    /// </summary>
    public class ControlAPI : IDisposable
    {
        public static void PortList()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                Console.WriteLine(port);
            }
        }
        SerialPort serial;
        Boolean SendWait = false;

        Mode mode { set; get; }
        public ColorMode colorMode { set; get; }
        public TimeMode timeMode { set; get; }
        public Color[] cColor { set; get; }
        public Color AllColor
        {
            set
            {
                for (int i = 0; i < cColor.Length; i++)
                {
                    cColor[i] = value;
                }
            }
        }
        public ControlAPI(String PortName)
        {
            serial = new SerialPort(PortName);
            serial.BaudRate = 115200;
            serial.DataBits = 8;
            serial.Parity = Parity.None;
            serial.StopBits = StopBits.One;
            serial.Handshake = Handshake.None;
            serial.DataReceived += Serial_DataReceived;
            serial.Open();

            cColor = new Color[6];
            AllColor = Color.White;

            mode = Mode.Non;
            colorMode = ColorMode.Monochrome;
            timeMode = TimeMode.XXIVh;

        }

        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string str = ((SerialPort)sender).ReadLine();
            Console.WriteLine(str);
            if (str.IndexOf("Success") >= 0)
            {
                SendWait = false;
            }
        }

        public void Dispose()
        {
            serial.Close();
        }

        public void SendNow()
        {
            int[] tTime = new int[6];
            DateTime now = System.DateTime.Now;
            tTime[0] = now.Hour / 10;
            tTime[1] = now.Hour % 10;
            tTime[2] = now.Minute / 10;
            tTime[3] = now.Minute % 10;
            tTime[4] = now.Second / 10;
            tTime[5] = now.Second % 10;

            StringBuilder sendparam = new StringBuilder();
            sendparam.Append("*");

            for (int i = 0; i < cColor.Length; i++)
            {
                sendparam.Append(String.Format("{0}{1:X2}{2:X2}{3:X2}", tTime[i], cColor[i].R, cColor[i].G, cColor[i].B));
            }
            Console.WriteLine(sendparam.ToString());
            SendWait = true;
            serial.Write(sendparam.ToString());
            while (SendWait) Thread.Sleep(1);
        }

        public void SendMode()
        {
            StringBuilder sendparam = new StringBuilder();
            sendparam.Append("$");
            sendparam.Append(String.Format("{0}{1}{2}", (int)mode, (int)colorMode, (int)timeMode));
            Console.WriteLine(sendparam.ToString());
            SendWait = true;
            serial.Write(sendparam.ToString());
            while (SendWait) Thread.Sleep(1);
        }
#if DEBUG
        public void TestSend(byte[] test)
        {
            serial.Write(test, 0, 1);
        }
#endif
    }
}
