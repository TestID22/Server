using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace HttpServer
{
    class Response
    {
        private Byte[] data = null;
        private string status;
        string mime;

        Response(string status, string mime, Byte[] data)
        {
            this.data = data;
            this.status = status;
        }

        public static Response From(Request request)
        {
            if (request == null)
            {
                return MakeNullRequest();
            }
            else if(request.type.ToUpper() == "GET")
            {
                return Index();
            }
            return null;

        }

        //переделать под индекс
        private static Response Index()
        {
            string html = " ";
            using (StreamReader sr = new StreamReader("index.html"))
            {
                html = sr.ReadToEnd();
                sr.Close();
            }

            byte[] localData = Encoding.ASCII.GetBytes(html);
            Response index = new Response("200", "text / html", localData);
            return index;
        }

        private static Response MakeNullRequest()
        {
            return new Response("404", "text/html", new byte[0]);
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine(String.Format($"Response:\r\n{Server.VERSION} {status}\r\n"+
                $"Server:{Server.SERVERNAME}\r\n" +
                $"Content-type: {mime}\r\n" +
                $"Constent-length: {data.Length}\r\n"));

            stream.Write(data, 0, data.Length);
        }
    }
}
