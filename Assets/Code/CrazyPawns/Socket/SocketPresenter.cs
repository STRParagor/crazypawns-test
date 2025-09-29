using UnityEngine;

namespace CrazyPawns.Pawns
{
    public class SocketPresenter : MonoBehaviour, ISocket
    {
        public ISocketView SocketView => _view;
        public IPawn Owner { get; private set; }
        public bool HasConnection { get; private set; }

        [SerializeField] private SocketView _view;

        public void Initialize(IPawn owner)
        {
            Owner = owner;
        }

        public void SetConnectionState(bool isConnected) => HasConnection = isConnected;
        
        public void ChangeMaterial(Material material) => _view.ChangeMaterial(material);
        
        public void ResetMaterial() => _view.ResetMaterial();
    }
}

