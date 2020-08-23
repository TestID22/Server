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
        int port ;
        TcpListener listener;
        string ipLocal;
        bool isRunning = false;
        
        public Server(int port)
        {
            this.port = port;
            listener = new TcpListener(IPAddress.Any, port);
        }
        //Поток принимает в себя делегат
        public void start()
        {
            Thread thread = new Thread(new ThreadStart(run));
            thread.Start();
        }
        
        private void run()
        {
            listener.Start();

            isRunning = true;
            string localIP = GetMyLocalIp();
            Console.WriteLine($"Сервак стартанул на: {localIP}");

            while (isRunning)
            {
                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Client подключился");
                HandleClient(client);
                client.Close();

                Http
            }

        }


        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());
            string data = "";
            while(reader.Peek() != -1) //тут фикс Пик заканчивает цикл при получении -1 
            {
                data += reader.ReadLine() + "\n";
            }
            Console.WriteLine($"REQ:\r\n{data}");
        }

        //TODO: функция возвращает локальный ИП 
        private string GetMyLocalIp()
        {
            IPHostEntry name = Dns.GetHostEntry(Dns.GetHostName());
            string ip = name.AddressList[name.AddressList.Length - 1].ToString();
            return ip;
        }
    }
}
