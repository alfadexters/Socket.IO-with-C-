using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace server
{
    internal class Program
    {
        private static List<TcpClient> clients = new List<TcpClient>();
        private static object lockObj = new object();

        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8080);
            server.Start();
            Console.WriteLine("Server is running on port 8080...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                lock (lockObj)
                {
                    clients.Add(client);
                }
                Console.WriteLine("New client connected!");

                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        private static void HandleClient(object clientObj)
        {
            TcpClient client = (TcpClient)clientObj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received: {message}");
                    BroadcastMessage(message, client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Client disconnected: {e.Message}");
            }
            finally
            {
                lock (lockObj)
                {
                    clients.Remove(client);
                }
                client.Close();
            }
        }

        private static void BroadcastMessage(string message, TcpClient sender)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);

            lock (lockObj)
            {
                foreach (var client in clients)
                {
                    if (client != sender)
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(data, 0, data.Length);
                    }
                }
            }
        }
    }
}

