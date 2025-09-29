using UnityEngine;
using CrazyPawns.Utils;

namespace CrazyPawns.CameraController
{
    public class BoundsClampTransformHandler : PositionGroupHandler
    {
        public Bounds CurrentBounds => _currentBounds;
        
        [SerializeField] private Bounds _currentBounds = new Bounds(Vector3.up, new Vector3(10, 5, 10));

        public void SetBoundsSize(Vector2 size)
        {
            var boundSize = _currentBounds.size;
            _currentBounds.size = new Vector3(size.x, boundSize.y, size.y);
        }
        
        public override void Process(ref Vector3 value, Transform component)
        {
            var position = component.position;
            var minBounds = _currentBounds.min;
            var maxBounds = _currentBounds.max;
            value.x = Mathf.Clamp(position.x + value.x, minBounds.x, maxBounds.x) -  position.x;
            value.y = Mathf.Clamp(position.y + value.y, minBounds.y, maxBounds.y) -  position.y;
            value.z = Mathf.Clamp(position.z + value.z, minBounds.z, maxBounds.z) -  position.z;
        }

        private void OnDrawGizmosSelected()
        {
            GizmosUtils.DrawBounds(_currentBounds);
        }
    }
}