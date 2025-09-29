using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawns.Pawns
{
    public class PawnSpawner : MonoBehaviour
    {
        [SerializeField] private PawnPresenter _pawnPrefab;

        public HashSet<IPawn> RandomSpawnInCircle(Vector3 position, float radius, int count, Transform parent = null)
        {
            count = Mathf.Max(1, count);
            var collection = new HashSet<IPawn>(count);
            

            for (var i = 0; i < count; i++)
            {
                var randomCircle = Random.insideUnitCircle * radius;
                var spawnPoint = position + new Vector3(randomCircle.x, 0, randomCircle.y);
                collection.Add(Spawn(spawnPoint, parent));
            }

            return collection;
        }
        
        private IPawn Spawn(Vector3 position, Transform parent)
        {
            return Instantiate(_pawnPrefab, position, Quaternion.identity, parent);
        }
    } 
}


