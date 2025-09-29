using System;
using UnityEngine;

namespace CrazyPawns.Pawns
{
    public class ConnectionInputHandler : MonoBehaviour, IConnectionInputHandler
    {
        public event Action<ISocket, Vector3> BeginConnection = delegate{  };
        public event Action<ISocket, Vector3> DragConnection = delegate{  };
        public event Action<ISocket, ISocket> SuccessConnection = delegate{  };
        public event Action<ISocket> FailedConnection = delegate{  };
        
        [SerializeField] private Camera _camera;
        [SerializeField] private float _dragThreshold = 5f;

        private Vector3 _startMousePosition;
        private bool _isDragged;

        private Plane _planeConnection;

        private ISocket _currentSocket;
        private bool _isDragConnection;
        
        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0) && _isDragConnection == false)
            {
                _startMousePosition = Input.mousePosition;
                
                if (TryGetSocketByRaycast(out _currentSocket) && _currentSocket.HasConnection)
                {
                    _currentSocket = null;
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (_isDragConnection == false && _isDragged == false && _currentSocket != null)
                {
                    var sqrDistance = (_startMousePosition - Input.mousePosition).sqrMagnitude;
                    if (sqrDistance > _dragThreshold * _dragThreshold)
                    {
                        _isDragged = true;
                        OnMouseDragBegin();
                    }
                }
            }
            
            if (_isDragConnection)
            {
                OnMouseDrag();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_currentSocket == null) return;
                
                if (_isDragged)
                {
                    OnMouseDragEnd();
                }
                else
                {
                    OnMouseClick();
                }
            }
        }

        private void OnMouseClick()
        {
            if (_isDragConnection == false)
            {
                if (TryGetSocketByRaycast(out _currentSocket) && _currentSocket.HasConnection == false)
                {
                    OnBeginConnection();
                }
            }
            else
            {
                OnEndConnection();
            }
        }


        private void OnMouseDragBegin()
        {
            if (_currentSocket != null && _currentSocket.HasConnection == false)
            {
                OnBeginConnection();
            }
        }
        
        private void OnMouseDrag()
        {
            if (_currentSocket == null) return;
            
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (_planeConnection.Raycast(ray, out var distance))
            {
                var point = ray.GetPoint(distance);
                DragConnection(_currentSocket, point);
            }
        }

        private void OnMouseDragEnd()
        {
            OnEndConnection();
        }

        private void OnBeginConnection()
        {
            _planeConnection.SetNormalAndPosition(Vector3.up, _currentSocket.SocketView.Position);
                    
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
                    
            if (_planeConnection.Raycast(ray, out var distance))
            {
                var point = ray.GetPoint(distance);
                        
                _isDragConnection = true;
                        
                BeginConnection(_currentSocket, point);
            }
        }

        private void OnEndConnection()
        {
            if (TryGetSocketByRaycast(out var socket))
            {
                SuccessConnection(_currentSocket, socket);
            }
            else
            {
                FailedConnection(_currentSocket);
            }

            _isDragged = false;
            _isDragConnection = false;
            _currentSocket = null;
        }

        private bool TryGetSocketByRaycast(out ISocket socket)
        {
            socket = null;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out var hitInfo, 999f) == false)
            {
                return false;
            }
            
            socket = hitInfo.collider.GetComponent<ISocket>();

            return socket != null;
        }
    }
}