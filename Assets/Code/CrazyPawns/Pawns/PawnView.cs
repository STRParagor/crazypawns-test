using UnityEngine;

namespace CrazyPawns.Pawns
{
    public class PawnView : MonoBehaviour, IPawnView
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private Material _initialMaterial;
        
        private void Start()
        {
            _initialMaterial = _meshRenderer.sharedMaterial;
        }

        public void ChangeMaterial(Material material)
        {
            _meshRenderer.sharedMaterial = material;
        }

        public void ResetMaterial()
        {
            ChangeMaterial(_initialMaterial);
        }
    }
}


