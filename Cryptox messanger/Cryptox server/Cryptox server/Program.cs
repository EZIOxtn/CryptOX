using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Cryptox_server
{
    class Program
    {
        private static List<Socket> clients = new List<Socket>();
        private static Socket serverSocket;

        static void Main(string[] args)
        {
            int port = GetPortNumberFromUser();
            StartServer(port);
        }
        private static int GetPortNumberFromUser()
        {
            int port;
            while (true)
            {
                Console.Write("port: ");
                if (int.TryParse(Console.ReadLine(), out port))
                    break;
                Console.WriteLine("Invalid port ");
            }
            return port;
        }
        private static void HandleClient(object clientObject)
        {
            Socket clientSocket = (Socket)clientObject;
            IPEndPoint clientEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
            string clientAddress = clientEndPoint.Address.ToString() + ":" + clientEndPoint.Port.ToString();

            try
            {
                while (true)
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead = clientSocket.Receive(buffer);
                    if (bytesRead == 0)
                        break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received from {clientAddress}: {message}");
                    Broadcast(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
            }
            finally
            {
                clientSocket.Close();
                clients.Remove(clientSocket);
            }
        }

        private static void Broadcast(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            foreach (Socket client in clients)
            {
                try
                {
                    client.Send(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error broadcasting message: {e}");
                }
            }
        }

        private static void StartServer(int port)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, 5050));
            serverSocket.Listen(port);
            Console.WriteLine("Server is running and listening on port 5050");

            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                clients.Add(clientSocket);
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(clientSocket);
            }
        }
    }
}
