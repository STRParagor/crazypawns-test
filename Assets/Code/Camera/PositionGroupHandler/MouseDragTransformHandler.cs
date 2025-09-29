using UnityEngine;

namespace CrazyPawns.CameraController
{
    public class MouseDragTransformHandler : PositionGroupHandler
    {
        [SerializeField] private Camera _camera;

        private Plane _dragPlane = new Plane(Vector3.up, Vector3.zero);
        
        private Vector3 _startDragPosition;
        private Vector3 _currentDragPosition;
        private Vector3 _targetOffset;
        
        private bool _isDragging;

        public override void Process(ref Vector3 value, Transform component)
        {
            HandleDrag();
            value += _targetOffset;
        }
        
        private void HandleDrag()
        {
            if (Input.GetMouseButtonDown(1))
            {
                OnBeginDrag();
            }
            
            if (Input.GetMouseButton(1) && _isDragging)
            {
                OnDrag();
            }
            
            if (Input.GetMouseButtonUp(1))
            {
                _isDragging = false;
                _targetOffset = Vector3.zero;
            }
        }
        
        private void OnBeginDrag()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (_dragPlane.Raycast(ray, out var distance))
            {
                _startDragPosition = ray.GetPoint(distance);
                _isDragging = true;
            }
        }
    
        private void OnDrag()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (_dragPlane.Raycast(ray, out var distance))
            {
                _currentDragPosition = ray.GetPoint(distance);
                _targetOffset = _startDragPosition - _currentDragPosition;
            }
        }
    }
}