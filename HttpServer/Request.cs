﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HttpServer
{
    class Request
    {
        string host;
        string url;
        string directory;

        private Request(string host, string url, string directory)
        {
            this.host = host;
            this.url = url;
            this.directory = directory;

        }


        public static Request GetRequest(string clientData)
        {
            if (String.IsNullOrEmpty(clientData))
            {
                return null;
            }
            string[] data = clientData.Split("\n");
            Console.WriteLine($"Hots {data[1]}\n, URL = {data[0].Split(" ")[1]}\n, Type :{data[0]}\n");
            return new Request(data[3], data[1], data[0]);
        }
    }
}