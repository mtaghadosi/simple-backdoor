using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SimpleTcpServer
{
    class Program
    {
        public static int recv, Result;
        static void Main(string[] args)
        {
            char[] determine = { ',' };
            byte[] data = new byte[1024];
            string Received;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newSock.Bind(ipep);
            newSock.Listen(10);
            Console.WriteLine("Waiting for a client...");

            Socket client = newSock.Accept();

            
            //Must_send = Console.ReadLine();

            //client.Send(Encoding.ASCII.GetBytes(Must_send));

            //Console.WriteLine("\n\nYour data has been sent, Waiting for results...\n\n\n\n");

            //recv = client.Receive(data);
            //string result_recieved = Encoding.ASCII.GetString(data, 0, recv);
            //Console.WriteLine(result_recieved);



            while (true)
            {
                Console.WriteLine("\n\nAn Incomming Connection Detected, Waiting for requests...");
                recv = client.Receive(data);
                Received = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine("\n\nIncoming Request Detected, Calculating...");
                if (recv == 0)
                {
                    Console.WriteLine("\n\nProblem in Receiving Data ...Exiting!");
                    break;
                }
                
                Console.WriteLine("\n\nReceived Data is:   " + Received);
                Thread.Sleep(300);
                if (Received == "exit" || Received=="e")
                {
                    data = Encoding.ASCII.GetBytes("exit");
                    client.Send(data, SocketFlags.None);
                    break;
                }
                Calculate_Data(determine, Received);
                data = Encoding.ASCII.GetBytes(Result.ToString());
                client.Send(data, SocketFlags.None);
                Console.WriteLine("\n\nData Sent...!");
            }
            client.Close();
            newSock.Close();
        }

        private static void Calculate_Data(char[] determine, string Received)
        {
            string[] Splited = Received.Split(determine);

            if (Splited.Length != 3)
            {
                //payam khata be karbar ersal shavad.
            }

            if (Splited[2] == "+")
            {
                Result = int.Parse(Splited[0]) + int.Parse(Splited[1]);
            }
            if (Splited[2] == "-")
            {
                Result = int.Parse(Splited[0]) - int.Parse(Splited[1]);
            }
            if (Splited[2] == "*")
            {
                Result = int.Parse(Splited[0]) * int.Parse(Splited[1]);
            }
        }
    }
}
