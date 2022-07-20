using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Management;

namespace ConsoleClient
{
    internal class Program : Class_Info_System
    {
        const int port = 8888;
        const string address = "192.168.0.140";

        static void Main(string[] args)
        {
            //Console.Write("Введите свое имя: ");
            string userName = Environment.UserName;
            string OsVersion = Environment.OSVersion.ToString();
            string UserDomainName = Environment.UserDomainName.ToString();
            string MachineName = Environment.MachineName.ToString();
            string ProcessorCount = Environment.ProcessorCount.ToString();
            string Pc_Ram = "";
            string Version_bios = "";
            
            TcpClient tcpClient = null;
            try
            {
                tcpClient = new TcpClient(address, port);
                NetworkStream networkStream = tcpClient.GetStream();

                for (int i = 0; i < 1; i++)
                {


                    Console.WriteLine(userName + ": Передаю данные на сервер...");
                    // Ввод сообщения
                    //string message = Console.ReadLine();
                    string message = String.Format("{0}: Версия Ос: {1}, Доменное имя: {2}, Имя компьютера: {3}, Количество процессоров: {4}, Оперативная память: {5}," +
                        " Версия биос: {6}"
                        , userName, OsVersion, UserDomainName, MachineName, ProcessorCount, Ram(Pc_Ram), Bios_Info(Version_bios));
                    // Преобразуем сообщение в массив байтов
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    // Отправка сообщения
                    networkStream.Write(data, 0, data.Length);

                }
                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                tcpClient.Close();
            }
        }

        static string Ram(string Memory)
        {
            ManagementObjectSearcher Ram_Monitor = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");
            foreach  (ManagementObject Obj_Ram in Ram_Monitor.Get())
            {
                ulong totalRam = Convert.ToUInt64(Obj_Ram["TotalVisibleMemorySize"]);
                ulong busyRam = totalRam - Convert.ToUInt64(Obj_Ram["FreePhysicalMemory"]);
                Memory = Convert.ToString((busyRam * 100) / totalRam);
            }
            return Memory;
        }

    }
}
