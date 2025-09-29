using System;
using UnityEngine;

namespace CrazyPawns.Pawns
{
    public class PawnDragInputHandler : MonoBehaviour, IDragInputHandler<IPawn>
    {
        public event Action<IPawn, bool> EndDrag = delegate {  };
        public event Action<IPawn> EnterInBounds = delegate {  };
        public event Action<IPawn> ExitFromBounds = delegate {  };
        
        [SerializeField] private Camera _camera;
        
        private Plane _dragPlane;
        private bool _isDragging;
        private IDragHandle<IPawn> _currentHandle;
        private IBoundary _dragPlaceBounds;

        private Vector3 _startPawnPosition;
        private Vector3 _startDragPoint;
        private Vector3 _currentDragPoint;

        private Bounds _dragBounds;

        private bool _isOutsideBoard;

        public void SetBounds(IBoundary boundary)
        {
            _dragPlaceBounds = boundary;
        }
        
        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0) && _isDragging == false)
            {
                OnBeginDrag();
            }

            if (Input.GetMouseButton(0) && _isDragging)
            {
                OnDrag();
            }

            if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                OnEndDrag();
            }
        }

        private void OnBeginDrag()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, 999f) == false ||
                hitInfo.collider.TryGetComponent(out _currentHandle) == false)
            {
                return;
            }

            _dragPlane.SetNormalAndPosition(Vector3.up, hitInfo.point);
            _dragPlane.Raycast(ray, out var distance);

            _startDragPoint = ray.GetPoint(distance);
            _startPawnPosition = _currentHandle.PivotPosition;

            _dragBounds = _dragPlaceBounds.Bounds;
            _dragBounds.size = new Vector3(_dragBounds.size.x, 5f, _dragBounds.size.z);
            _isDragging = true;
            _isOutsideBoard = false;
        }

        private void OnDrag()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            _dragPlane.Raycast(ray, out var distance);
            _currentDragPoint = ray.GetPoint(distance);

            var deltaPosition = _currentDragPoint - _startDragPoint;
            _currentHandle.SetPivotPosition(_startPawnPosition + deltaPosition);

            if (_isOutsideBoard && _dragBounds.Contains(_currentHandle.PivotPosition))
            {
                _isOutsideBoard = false;
                EnterInBounds(_currentHandle.Owner);
            }
            else if (_isOutsideBoard == false && _dragBounds.Contains(_currentHandle.PivotPosition) == false)
            {
                _isOutsideBoard = true;
                ExitFromBounds(_currentHandle.Owner);
            }
        }

        private void OnEndDrag()
        {
            EndDrag(_currentHandle.Owner, !_isOutsideBoard);

            _isDragging = false;
            _currentHandle = null;
        }
    }
}