using System.Collections.Generic;
using CrazyPawns.Configs;
using UnityEngine;

namespace CrazyPawns.Pawns
{
    public class ConnectionManager : MonoBehaviour
    {
        [SerializeField] private CrazyPawnSettings _settings;
        [SerializeField] private PawnManager _pawnManager;
        [SerializeField] private ConnectionInputHandler _connectionInputHandler;
        [SerializeField] private float _connectionLineWidht = 0.07f;

        private ConnectionHandler _connectionHandler;
        private ConnectionDrawer _connectionDrawer;
        private HashSet<ISocket> _availableSockets;
        
        private void Awake()
        {
            _availableSockets = new HashSet<ISocket>(100);
            _connectionHandler = new ConnectionHandler(_connectionLineWidht, transform);
            
            _connectionDrawer = new ConnectionDrawer(transform);
            _connectionDrawer.SetVisibility(false);
            _connectionDrawer.SetWidth(_connectionLineWidht);
        }

        private void OnEnable()
        {
            _pawnManager.PawnRemoved += OnPawnRemoved;
            _connectionInputHandler.BeginConnection += OnBeginConnection;
            _connectionInputHandler.DragConnection += OnDragConnection;
            _connectionInputHandler.SuccessConnection += OnSuccessConnection;
            _connectionInputHandler.FailedConnection += OnFailedConnection;
        }
        
        private void OnDisable()
        {
            _pawnManager.PawnRemoved -= OnPawnRemoved;
            _connectionInputHandler.BeginConnection -= OnBeginConnection;
            _connectionInputHandler.DragConnection -= OnDragConnection;
            _connectionInputHandler.SuccessConnection -= OnSuccessConnection;
            _connectionInputHandler.FailedConnection -= OnFailedConnection;
        }

        private void OnBeginConnection(ISocket sourceSocket, Vector3 endPosition)
        {
            var activePawns = _pawnManager.ActivePawns;
            
            _connectionHandler.GetAvailableSockets(activePawns, sourceSocket, ref _availableSockets);
            
            foreach (var socket in _availableSockets)
            {
                socket.ChangeMaterial(_settings.ActiveConnectorMaterial);
            }

            _connectionDrawer.SetPoints(sourceSocket.SocketView.Position, endPosition);
            _connectionDrawer.SetVisibility(true);
        }

        private void OnDragConnection(ISocket sourceSocket, Vector3 endPosition)
        {
            _connectionDrawer.SetPoints(sourceSocket.SocketView.Position, endPosition);
        }
        
        private void OnSuccessConnection(ISocket sourceSocket, ISocket targetSocket)
        {
            _connectionDrawer.SetVisibility(false);

            _connectionHandler.TryAddConnection(_availableSockets, sourceSocket, targetSocket);
            
            ResetAvailableSockets();
        }

        private void ResetAvailableSockets()
        {
            foreach (var availableSocket in _availableSockets)
            {
                availableSocket.ResetMaterial();
            }
            
            _availableSockets.Clear();
        }
        
        private void OnFailedConnection(ISocket sourceSocket)
        {
            _connectionDrawer.SetVisibility(false);
            ResetAvailableSockets();
        }
        
        private void OnPawnRemoved(IPawn pawn)
        {
            _connectionHandler.RemoveAllConnectionForPawn(pawn);
        }

        private void LateUpdate()
        {
            _connectionHandler.Update();
        }
    }
}