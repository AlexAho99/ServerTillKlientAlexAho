using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ServerTillKlientAlexAho
{
    class Program
    {
        static TcpListener tcpListener;

        public static void Main()
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress);
            IPAddress myIp = IPAddress.Parse("127.0.0.1");
            tcpListener = new TcpListener(myIp, 8001);
            tcpListener.Start();

            while (true)
            {
                try
                {
                    Console.WriteLine("Väntar på anslutning...");
                    Socket socket = tcpListener.AcceptSocket();
                    Console.WriteLine("Anslutning accepterad från " + socket.RemoteEndPoint);

                    Byte[] bMessage = new Byte[256];
                    int messageSize = socket.Receive(bMessage);
                    Console.WriteLine("Meddelandet mottogs...");

                    string message = "";
                    for (int i = 0; 1 < messageSize; i++)
                        message += Convert.ToChar(bMessage[i]);
                    Console.WriteLine("Meddelande: " + message);

                    Byte[] bSend = System.Text.Encoding.ASCII.GetBytes("Tack!");
                    socket.Send(bSend);
                    Console.WriteLine("Svar skickat");

                    socket.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

        static void CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            tcpListener.Stop();
            Console.WriteLine("Servern stängdes av!");
        }
    }
}
