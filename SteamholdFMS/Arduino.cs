using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SteamholdFMS
{
    class Arduino
    {
        static SerialPort port = new SerialPort("COM12", 9600);

        public static void Initialize()
        {
            //port.Open();
        }
        
        public static void Send(char[] package)
        {
            //port.Write(package, 0, 13);
        }

        public static void Close()
        {
            //port.Close();
        }
    }
}
