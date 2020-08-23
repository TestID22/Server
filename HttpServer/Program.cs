using System;
using System.Text;

namespace HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(8080);
            server.start();

        }
    }
}
