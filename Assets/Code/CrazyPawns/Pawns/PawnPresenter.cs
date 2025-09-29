using UnityEngine;

namespace CrazyPawns.Pawns
{
    public class PawnPresenter : MonoBehaviour, IPawn
    {
        public IPawnView PawnView => _view;
        public ISocket[] Sockets => _sockets;
        
        [SerializeField] private PawnView _view;
        [SerializeField] private SocketPresenter[] _sockets;

        private void Awake()
        {
            foreach (var socketPresenter in _sockets)
            {
                socketPresenter.Initialize(this);
            }
        }

        public void ChangeMaterial(Material material)
        {
            _view.ChangeMaterial(material);

            foreach (var socket in _sockets)
            {
                socket.ChangeMaterial(material);
            }
        }

        public void ResetMaterial()
        {
            _view.ResetMaterial();
            
            foreach (var socket in _sockets)
            {
                socket.ResetMaterial();
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}