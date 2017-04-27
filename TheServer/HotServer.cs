using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;
using Theft;

namespace Finger
{
    class HotServer
    {
        SVRMSG msg = new SVRMSG("Welcome!");
        List<Grasp> grasps = new List<Grasp>();

        public HotServer(string pipename)
        {
            var server = new NamedPipeServer<SVRMSG>(pipename);
            server.Start();
            server.ClientConnected += OnClientConnected;
            server.ClientDisconnected += OnClientDisconnected;
            server.ClientMessage += OnClientMessage;
            server.Error += OnError;
            while (!ShouldClose())
            {
                string newmsg = Console.ReadLine();

                Console.WriteLine("message: {0}", msg);

                msg = new SVRMSG(newmsg);

                server.PushMessage(msg);
            }
            server.Stop();
        }

        private void OnClientConnected(NamedPipeConnection<SVRMSG, SVRMSG> connection)
        {
            Console.WriteLine("Client {0} is now connected!", connection.Id);
            connection.PushMessage(new SVRMSG("Welcome!"));
        }

        private void OnClientDisconnected(NamedPipeConnection<SVRMSG, SVRMSG> connection)
        {
            Console.WriteLine("Client {0} disconnected", connection.Id);
        }

        private void OnClientMessage(NamedPipeConnection<SVRMSG, SVRMSG> connection, SVRMSG message)
        {
            Console.WriteLine("Client {0} says: {1}", connection.Id, message);
        }

        private void OnError(Exception exception)
        {
            Console.Error.WriteLine("ERROR: {0}", exception);
        }

        private bool ShouldClose()
        {
            if(msg.text == "quit")
                return true;
            return false;
        }
    }
}
