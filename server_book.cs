/*
 * C# program to accept a book title from clients and sends back    
 * its price using XML
 */
 
//SERVER SIDE PROGRAM
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Configuration;

namespace ServerSocket
{
    class Program
    {
        static TcpListener listener;
        const int LIMIT = 10; 
        public static void Query()
        {
            while (true)
            {
                Socket soc = listener.AcceptSocket();
                Console.WriteLine("Connected: {0}", soc.RemoteEndPoint);
                try
                {	 
					Stream s = new NetworkStream(soc);
                    StreamReader sr = new StreamReader(s);
                    StreamWriter sw = new StreamWriter(s);
                    sw.AutoFlush = true; // enable automatic flushing
                    sw.WriteLine("{0} books available", ConfigurationManager.AppSettings.Count);
                    while (true)
                    {
                        string bookTitle = sr.ReadLine();
                        if (bookTitle == "" || bookTitle == null) 
							break;
                        string price = ConfigurationManager.AppSettings[bookTitle];
                        if (price == null) 
							price = "Sorry, no such book!";
                        sw.WriteLine(price);
                    }
                    s.Close();
                }
                catch (Exception e)
                {
 
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Disconnected: {0}", soc.RemoteEndPoint);
                soc.Close();
            }
        }
        static void Main(string[] args)
        {
			IPAddress ipAd = IPAddress.Parse("127.0.0.1"); 
            listener = new TcpListener(ipAd, 2057);
            listener.Start();
 
            Console.WriteLine("Server started, listening to port 2057");
            for (int i = 0; i < LIMIT; i++)
            {
                //Thread t = new Thread(new ThreadStart(Query));
				//The above statement can be simplified to next:
				Thread t = new Thread(Query);
                t.Start();
				//Console.WriteLine("Server thread {0} started....", ++i);
				Console.WriteLine("Server thread {0} started....", i+1);
            }
        }
    }
}