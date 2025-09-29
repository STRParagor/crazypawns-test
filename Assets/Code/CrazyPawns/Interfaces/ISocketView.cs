using UnityEngine;

namespace CrazyPawns.Pawns
{
    public interface ISocketView : IMaterialChanger
    {
        Vector3 Position { get; }
    }
}