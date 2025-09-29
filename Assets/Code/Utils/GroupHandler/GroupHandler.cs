using UnityEngine;

namespace CrazyPawns.Utils
{
    public abstract class GroupHandler<TValue> : MonoBehaviour
    {
        public abstract void Process(ref TValue value);
    }

    public abstract class GroupHandler<TValue, TComponent> : MonoBehaviour
    {
        public abstract void Process(ref TValue value, TComponent component);
    }
}