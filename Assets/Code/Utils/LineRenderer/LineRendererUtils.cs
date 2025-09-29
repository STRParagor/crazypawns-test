using UnityEngine;

namespace CrazyPawns.Utils
{
    public static class LineRendererUtils
    {
        public static LineRenderer CreateSimpleLine(string name, float width, Transform parent = null)
        {
            var line = new GameObject(name);
            line.transform.SetParent(parent);

            var lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            lineRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;

            return lineRenderer;
        }
    }
}