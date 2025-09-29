using UnityEngine;

namespace CrazyPawns.Pawns
{
    public interface IDragHandle<out T>
    {
        Vector3 PivotPosition { get; }
        T Owner { get; }
        void SetPivotPosition(Vector3 position);
    }
}