using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HttpServer
{
    class Server
    {
        int port = default;
        TcpListener listener;
        bool isRunning = false;
        public Server(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }
        //Поток принимает в себя делегат
        public void start()
        {
            Thread thread = new Thread(new ThreadStart(run));
            thread.Start();
            isRunning = true;
        }
        
        private void run()
        {
            listener.Start();
            string server = GetMyLocalIp();
            Console.WriteLine($"Сервак стартанул на:{server}");
            while (isRunning)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client подключился");
                HandleClient(client);
                listener.Stop();
            }

        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());
            string data = default;
            while (reader.Peek() != 1)
            {
                data += reader.ReadLine() + "\n";

            }
            Console.WriteLine($"REQ:{data}");

        }
        //TODO: функция возвращает локальный ИП 
        private string GetMyLocalIp()
        {
            return Dns.GetHostName().ToString();
        }
    }
}
