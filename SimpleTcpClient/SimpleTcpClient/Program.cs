using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SimpleTcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            int recv;
            string Phrase;
            
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                Console.WriteLine("Successfuly Connected to server...");
                Thread.Sleep(300);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to Connect to Server!");
                Console.ReadLine();
                return;
            }



            while (true)
            {
                Console.WriteLine("Please enter your request:(type 'e' for Quit)");
                Phrase = Console.ReadLine().ToLower();
                server.Send(Encoding.ASCII.GetBytes(Phrase));
                Console.WriteLine("Data has been sent, waiting for results:");
                Thread.Sleep(200);
                recv = server.Receive(data);
                if (Encoding.ASCII.GetString(data, 0, recv) == "exit" || Encoding.ASCII.GetString(data, 0, recv)=="e")
                {
                    break;
                }
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                
            }
            server.Close();
        }

        
    }
}
