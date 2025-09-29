using UnityEngine;

namespace CrazyPawns.Pawns
{
    public class PawnDragHandle : MonoBehaviour, IDragHandle<IPawn>
    {
        public Vector3 PivotPosition => _rootTransform.position;
        public IPawn Owner => _owner;

        [SerializeField] private Transform _rootTransform;
        [SerializeField] private PawnPresenter _owner;

        public void SetPivotPosition(Vector3 position)
        {
            _rootTransform.position = position;
        }
    }
}