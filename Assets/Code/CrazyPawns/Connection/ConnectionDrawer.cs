using UnityEngine;
using CrazyPawns.Utils;

namespace CrazyPawns.Pawns
{
    public class ConnectionDrawer
    {
        private readonly LineRenderer _line;
        
        public ConnectionDrawer(Transform parentTransform)
        {
            _line = LineRendererUtils.CreateSimpleLine("Temp Line", 0.07f, parentTransform);
            _line.enabled = false;
        }

        public void SetVisibility(bool isVisible)
        {
            _line.enabled = isVisible;
        }

        public void SetWidth(float width)
        {
            _line.startWidth = width;
            _line.endWidth = width;
        }

        public void SetPoints(Vector3 startPoint, Vector3 endPoint)
        {
            _line.SetPosition(0, startPoint);
            _line.SetPosition(1, endPoint);
        }
    }
}