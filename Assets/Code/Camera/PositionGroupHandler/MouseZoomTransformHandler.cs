using UnityEngine;

namespace CrazyPawns.CameraController
{
    public class MouseZoomTransformHandler : PositionGroupHandler
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _zoomSpeed = 1;
        
        private Plane _zoomPlane = new(Vector3.up, Vector3.zero);
        private Vector3 _zoomOffset;

        
        public override void Process(ref Vector3 value, Transform component)
        {
            HandleZoom(component.position + value);
            value += _zoomOffset;
            _zoomOffset = Vector3.zero;
        }
        
        private void HandleZoom(Vector3 position)
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                ZoomTowardsMouse(scroll, position);
            }
        }
        
        private void ZoomTowardsMouse(float scrollDelta, Vector3 position)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (_zoomPlane.Raycast(ray, out var distance))
            {
                var worldPoint = ray.GetPoint(distance);
                var directionToPoint = (worldPoint - position).normalized;
                var zoomAmount = position.y - (scrollDelta * _zoomSpeed);
                var currentDistance = Vector3.Distance(position, worldPoint);
                var targetDistance = currentDistance * (zoomAmount / transform.position.y);
                
                _zoomOffset = directionToPoint * (currentDistance - targetDistance);
            }
        }
    }
}