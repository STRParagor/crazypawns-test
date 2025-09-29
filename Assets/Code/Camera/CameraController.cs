using UnityEngine;

namespace CrazyPawns.CameraController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private PositionGroupHandler[] _positionGroupHandlers;

        private void Update()
        {
            var position = Vector3.zero;
        
            foreach (var positionGroupHandler in _positionGroupHandlers)
            {
                positionGroupHandler.Process(ref position, transform);
            }

            transform.position += position;
        }
    }
}
