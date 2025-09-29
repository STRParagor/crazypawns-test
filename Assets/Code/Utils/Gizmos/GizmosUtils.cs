using UnityEngine;

namespace CrazyPawns.Utils
{
    public static class GizmosUtils
    {
        private static Color _defaultWireColor = Color.green;
        private static Color _defaultSolidColor = new Color(0f, 1f, 0f, 0.25f);
        
        public static void DrawSolidBounds(Bounds bounds, Color? color = null)
        {
            var oldColor = Gizmos.color;

            var originalColor = Gizmos.color;
            Gizmos.color = color ?? _defaultSolidColor;
            Gizmos.DrawCube(bounds.center, -bounds.size);

            Gizmos.color = oldColor;
        }
        
        public static void DrawWireBounds(Bounds bounds, Color? color = null)
        {
            var oldColor = Gizmos.color;
            Gizmos.color = color ?? _defaultWireColor;

            var center = bounds.center;
            var size = bounds.size;

            var topFrontLeft = center + new Vector3(-size.x / 2, size.y / 2, -size.z / 2);
            var topFrontRight = center + new Vector3(size.x / 2, size.y / 2, -size.z / 2);
            var topBackLeft = center + new Vector3(-size.x / 2, size.y / 2, size.z / 2);
            var topBackRight = center + new Vector3(size.x / 2, size.y / 2, size.z / 2);

            var bottomFrontLeft = center + new Vector3(-size.x / 2, -size.y / 2, -size.z / 2);
            var bottomFrontRight = center + new Vector3(size.x / 2, -size.y / 2, -size.z / 2);
            var bottomBackLeft = center + new Vector3(-size.x / 2, -size.y / 2, size.z / 2);
            var bottomBackRight = center + new Vector3(size.x / 2, -size.y / 2, size.z / 2);

            Gizmos.DrawLine(topFrontLeft, topFrontRight);
            Gizmos.DrawLine(topFrontRight, topBackRight);
            Gizmos.DrawLine(topBackRight, topBackLeft);
            Gizmos.DrawLine(topBackLeft, topFrontLeft);

            Gizmos.DrawLine(bottomFrontLeft, bottomFrontRight);
            Gizmos.DrawLine(bottomFrontRight, bottomBackRight);
            Gizmos.DrawLine(bottomBackRight, bottomBackLeft);
            Gizmos.DrawLine(bottomBackLeft, bottomFrontLeft);

            Gizmos.DrawLine(topFrontLeft, bottomFrontLeft);
            Gizmos.DrawLine(topFrontRight, bottomFrontRight);
            Gizmos.DrawLine(topBackLeft, bottomBackLeft);
            Gizmos.DrawLine(topBackRight, bottomBackRight);

            Gizmos.color = oldColor;
        }
        
        public static void DrawBounds(Bounds bounds, Color? solidColor = null, Color? wireColor = null)
        {
            DrawSolidBounds(bounds, solidColor);
            DrawWireBounds(bounds, wireColor);
        }
    }
}