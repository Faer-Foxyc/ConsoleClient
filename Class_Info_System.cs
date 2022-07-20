using System;
using System.Management;

namespace ConsoleClient
{
    internal class Class_Info_System
    {
        public static string Bios_Info(string Version_Bios)
        {
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_BIOS");
            foreach (ManagementObject queryObj in managementObjectSearcher.Get())
            {
                 Version_Bios = "Version: " + queryObj["Version"];
            }
            return Version_Bios;
        }
    }
}
