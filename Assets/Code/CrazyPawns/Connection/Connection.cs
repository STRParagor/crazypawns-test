using UnityEngine;

namespace CrazyPawns.Pawns
{
    public class Connection : IConnection
    {
        public ISocket FirstSocket { get; }
        public ISocket SecondSocket { get; }
        private LineRenderer _connectionLine { get; }
        
        public Connection(ISocket firstSocket, ISocket secondSocket, LineRenderer connectionLine)
        {
            FirstSocket = firstSocket;
            SecondSocket = secondSocket;
            _connectionLine = connectionLine;

            FirstSocket.SetConnectionState(true);
            SecondSocket.SetConnectionState(true);
        }

        public void UpdateConnection()
        {
            _connectionLine.SetPosition(0, FirstSocket.SocketView.Position);
            _connectionLine.SetPosition(1, SecondSocket.SocketView.Position);
        }

        public void Destroy()
        {
            FirstSocket.SetConnectionState(false);
            SecondSocket.SetConnectionState(false);
            Object.Destroy(_connectionLine.gameObject);
        }
    }
}