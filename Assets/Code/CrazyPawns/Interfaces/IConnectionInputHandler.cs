using System;
using UnityEngine;

namespace CrazyPawns.Pawns
{
    public interface IConnectionInputHandler
    {
        event Action<ISocket, Vector3> BeginConnection;
        event Action<ISocket, Vector3> DragConnection;
        event Action<ISocket, ISocket> SuccessConnection;
        event Action<ISocket> FailedConnection;
    }
}