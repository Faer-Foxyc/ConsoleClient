using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleClient
{
    internal class Program
    {
        const int port = 8888;
        const string address = "Adress local server";

        static void Main(string[] args)
        {
            //Console.Write("Введите свое имя: ");
            string userName = Environment.UserName;
            string OsVersion = Environment.OSVersion.ToString();
            string UserDomainName = Environment.UserDomainName.ToString();
            string MachineName = Environment.MachineName.ToString();
            
            TcpClient tcpClient = null;
            try
            {
                tcpClient = new TcpClient(address, port);
                NetworkStream networkStream = tcpClient.GetStream();

                for (int i = 0; i < 1; i++)
                {


                    Console.WriteLine(userName + ": ");
                    // Ввод сообщения
                    //string message = Console.ReadLine();
                    string message = String.Format("{0}: Версия Ос: {1}, Доменное имя: {2}", userName, OsVersion, UserDomainName);
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

    }
}
