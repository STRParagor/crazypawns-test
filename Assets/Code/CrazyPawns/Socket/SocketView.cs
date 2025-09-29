using UnityEngine;

namespace CrazyPawns.Pawns
{
    public class SocketView : MonoBehaviour, ISocketView
    {
        public Vector3 Position => _socketTransform.position;

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Transform _socketTransform;
        
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