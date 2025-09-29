using System;

namespace CrazyPawns.Pawns
{
    public interface IDragInputHandler<out T> : IBoundaryLimiter
    {
        event Action<T, bool> EndDrag;
        event Action<T> EnterInBounds;
        event Action<T> ExitFromBounds;
    }
}