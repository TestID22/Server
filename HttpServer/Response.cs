using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace HttpServer
{
    class Response
    {
        Response()
        {

        }

        public static Response From(Request request)
        {
            return null;
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);

            Console.WriteLine(String.Format(""));
        }
    }
}
