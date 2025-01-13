using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting to server...");
            TcpClient client = new TcpClient("127.0.0.1", 8080); // Cambia "127.0.0.1" si tu servidor está en otra IP.
            Console.WriteLine("Connected to server!");

            NetworkStream stream = client.GetStream();
            Thread readThread = new Thread(() => ReadMessages(stream));
            readThread.Start();

            while (true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }

        private static void ReadMessages(NetworkStream stream)
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Message from server: {message}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Disconnected from server: {e.Message}");
            }
        }
    }
}
