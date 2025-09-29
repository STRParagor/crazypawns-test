using System.Collections.Generic;
using CrazyPawns.Utils;
using UnityEngine;

namespace CrazyPawns.Pawns
{
    public class ConnectionHandler
    {
        private readonly HashSet<IConnection> _connetions;
        private readonly Dictionary<IPawn, List<IConnection>> _pawnConnections;
        private readonly List<IConnection> _toRemoveConnections;
        
        private readonly float _connectionWidth;
        private readonly Transform _linesParent;
        
        public ConnectionHandler(float connectionWidth, Transform linesParent)
        {
            _connectionWidth = connectionWidth;
            _linesParent = linesParent;
            _pawnConnections = new Dictionary<IPawn, List<IConnection>>();
            _connetions = new HashSet<IConnection>();
            
            _toRemoveConnections = new List<IConnection>(4);
        }
        
        public void Update()
        {
            foreach (var connection in _connetions)
            {
                connection.UpdateConnection();
            }
        }
        
        public void GetAvailableSockets(HashSet<IPawn> activePawns, ISocket targetSocket, ref HashSet<ISocket> availableSockets)
        {
            availableSockets.Clear();
            
            foreach (var pawn in activePawns)
            {
                if (pawn == targetSocket.Owner) continue;
                
                if (_pawnConnections.TryGetValue(targetSocket.Owner, out var connections))
                {
                    var hasConnection = false;
                    foreach (var connect in connections)
                    {
                        if (connect.FirstSocket.Owner == pawn ||
                            connect.SecondSocket.Owner == pawn)
                        {
                            hasConnection = true;
                            break;
                        }
                    }
                    
                    if (hasConnection) continue;
                }
                
                foreach (var socket in pawn.Sockets)
                {
                    if (socket.HasConnection) continue;
                    
                    availableSockets.Add(socket);
                }
            }
        }
        
        public void TryAddConnection(HashSet<ISocket> availableSockets, ISocket sourceSocket, ISocket targetSocket)
        {
            if (availableSockets.Contains(targetSocket))
            {
                var connectionLine = LineRendererUtils.CreateSimpleLine("Connection", _connectionWidth, _linesParent);
                var connection = new Connection(sourceSocket, targetSocket, connectionLine);

                AddConnection(sourceSocket.Owner, connection);
                AddConnection(targetSocket.Owner, connection);
            }
        }

        public void RemoveAllConnectionForPawn(IPawn pawn)
        {
            if (_pawnConnections.TryGetValue(pawn, out var connections))
            {
                foreach (var connection in connections)
                {
                    _toRemoveConnections.Add(connection);
                }
                
                foreach (var removeConnection in _toRemoveConnections)
                {
                    var firstPawn = removeConnection.FirstSocket.Owner;
                    var secondPawn = removeConnection.SecondSocket.Owner;
                    
                    if (_pawnConnections.TryGetValue(firstPawn, out var firstConnections))
                    {
                        firstConnections.Remove(removeConnection);
                    }
                    
                    if (_pawnConnections.TryGetValue(secondPawn, out var secondConnections))
                    {
                        secondConnections.Remove(removeConnection);
                    }
                    
                    _connetions.Remove(removeConnection);
                    _pawnConnections.Remove(pawn);
                    removeConnection.Destroy();
                }
                
                _toRemoveConnections.Clear();
            }
        }

        private void AddConnection(IPawn pawn, IConnection connection)
        {
            if (_pawnConnections.TryAdd(pawn, null))
            {
                _pawnConnections[pawn] = new List<IConnection>(4);
            }

            _pawnConnections[pawn].Add(connection);
            _connetions.Add(connection);
        }
    }
}