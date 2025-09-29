using System;
using System.Collections.Generic;
using UnityEngine;
using CrazyPawns.Configs;

namespace CrazyPawns.Pawns
{
    public class PawnManager : MonoBehaviour
    {
        public event Action<IPawn> PawnRemoved = delegate {  };
        public HashSet<IPawn> ActivePawns => _activePawns;
        
        [SerializeField] private PawnSpawner _spawner;
        [SerializeField] private PawnDragInputHandler _dragHandler;

        private HashSet<IPawn> _activePawns;
        private CrazyPawnSettings _settings;
        
        public void Initialize(CrazyPawnSettings settings)
        {
            _settings = settings;
            
            Clear();

            var pawnCount = settings.InitialPawnCount;
            var initialRadius = settings.InitialZoneRadius;

            _activePawns = _spawner.RandomSpawnInCircle(Vector3.zero, initialRadius, pawnCount, transform);
        }
        
        private void OnEnable()
        {
            _dragHandler.EnterInBounds += OnPawnInsideBoard;
            _dragHandler.ExitFromBounds += OnPawnOutsideBoard;
            _dragHandler.EndDrag += OnPawnDeleted;
        }
        
        private void OnDisable()
        {
            _dragHandler.EnterInBounds -= OnPawnInsideBoard;
            _dragHandler.ExitFromBounds -= OnPawnOutsideBoard;
            _dragHandler.EndDrag -= OnPawnDeleted;
        }

        private void OnPawnInsideBoard(IPawn pawn)
        {
            pawn.ResetMaterial();
        }

        private void OnPawnOutsideBoard(IPawn pawn)
        {
            pawn.ChangeMaterial(_settings.DeleteMaterial);
        }

        private void OnPawnDeleted(IPawn pawn, bool inBounds)
        {
            if (inBounds) return;
            
            if (_activePawns.Remove(pawn))
            {
                PawnRemoved(pawn);
                pawn.Destroy();
            }
        }
        
        public void Clear()
        {
            if (_activePawns is {Count: > 0})
            {
                foreach (var pawn in _activePawns)
                {
                    pawn.Destroy();
                }
                
                _activePawns.Clear();
            }

            _activePawns ??= new HashSet<IPawn>();
        }
    }
}