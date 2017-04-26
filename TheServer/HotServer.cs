using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;
namespace TheServer
{
    class HotServer
    {
        string msg;
        private bool KeepRunning
        {
            get
            {             
                if (msg == "quit")
                    return false;
                return true;
            }
        }

        public HotServer(string pipename)
        {
            var server = new NamedPipeServer<string>(pipename);
            server.Start();
            server.ClientConnected += OnClientConnected;
            server.ClientDisconnected += OnClientDisconnected;
            server.ClientMessage += OnClientMessage;
            server.Error += OnError;
            while (KeepRunning)
            {
                msg = Console.ReadLine();
                Console.WriteLine("message: {0}", msg);

                server.PushMessage(msg);
            }
            server.Stop();
        }

        private void OnClientConnected(NamedPipeConnection<string, string> connection)
        {
            Console.WriteLine("Client {0} is now connected!", connection.Id);
            connection.PushMessage("Welcome!");
        }

        private void OnClientDisconnected(NamedPipeConnection<string, string> connection)
        {
            Console.WriteLine("Client {0} disconnected", connection.Id);
        }

        private void OnClientMessage(NamedPipeConnection<string, string> connection, string message)
        {
            Console.WriteLine("Client {0} says: {1}", connection.Id, message);
        }

        private void OnError(Exception exception)
        {
            Console.Error.WriteLine("ERROR: {0}", exception);
        }
    }
}
