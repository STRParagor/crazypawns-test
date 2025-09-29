using CrazyPawns.Pawns;
using UnityEngine;

namespace CrazyPawns.Checkerboard
{
    public class Checkerboard : MonoBehaviour, IBoundary
    {
        public Bounds Bounds { get; private set; }
        
        private MeshRenderer _meshRenderer;

        private MaterialPropertyBlock _colorPropertyBlock;
        private static readonly int ColorID = Shader.PropertyToID("_Color");

        public void Initialize(MeshRenderer meshRenderer)
        {
            _colorPropertyBlock ??= new MaterialPropertyBlock();
            _meshRenderer = meshRenderer;
            
            Bounds = _meshRenderer.bounds;
        }

        public void SetWhiteCellColor(Color color)
        {
            _colorPropertyBlock.SetColor(ColorID, color);
            _meshRenderer.SetPropertyBlock(_colorPropertyBlock, 0);
        }
        
        public void SetBlackCellColor(Color color)
        {
            _colorPropertyBlock.SetColor(ColorID, color);
            _meshRenderer.SetPropertyBlock(_colorPropertyBlock, 1);
        }
    }
}