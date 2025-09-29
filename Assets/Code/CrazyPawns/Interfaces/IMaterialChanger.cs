using UnityEngine;

namespace CrazyPawns.Pawns
{
    public interface IMaterialChanger
    {
        void ChangeMaterial(Material material);
        void ResetMaterial();
    }
}