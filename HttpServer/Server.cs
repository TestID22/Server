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
        public const string VERSION = "HTTP/1.0";
        public const string SERVERNAME = "septemberServer";


        public Server(int port)
        {
            this.port = port;
            ipLocal = GetMyLocalIp();
            listener = new TcpListener(IPAddress.Parse(ipLocal), port);
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
                //client.Close();

            }

        }


        private void HandleClient(TcpClient client)
        {

            using (StreamReader reader = new StreamReader(client.GetStream()))
            {
                string data = "";
                while (reader.Peek()!=-1) //тут фикс Пик заканчивает цикл при получении -1 
                {
                    data += reader.ReadLine() + "\n";
                }
                Console.WriteLine($"REQ:\r\n{data}");

                //TODO: Обработка запроса, создание ответа.
                Request request = Request.GetRequest(data);
                Response response = Response.From(request);
                //тут баг
                try
                {
                    response.Post(client.GetStream());
                }catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

            }

        }
        
        //TODO: функция возвращает локальный ИП 
        private string GetMyLocalIp()
        {
            IPHostEntry name = Dns.GetHostEntry(Dns.GetHostName());
            string ip = name.AddressList[^1].ToString();
            return ip;
        }
    }
}
