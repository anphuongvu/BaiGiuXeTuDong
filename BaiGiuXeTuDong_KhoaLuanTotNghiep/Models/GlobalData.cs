using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Web;

namespace BaiGiuXeTuDong_KhoaLuanTotNghiep.Models
{
    public class GlobalData
    {
        private static string IP_Address { get; set; }
        private static SerialPort serialPort { get; set; }

        public static string initIP()
        {
            if (IP_Address == null || IP_Address == "")
            {
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress ip in localIPs)
                {
                    if (!ip.IsIPv6LinkLocal)
                    {
                        IP_Address = ip.ToString();
                        break;
                    }

                }
            }
            return IP_Address;
        }

        public static SerialPort initSerialPort()
        {
            if (serialPort == null)
            {
                serialPort = new SerialPort();
                serialPort.BaudRate = 9600;
                serialPort.PortName = "COM5";
                serialPort.Open();
            }

            return serialPort;
        }
    }
}