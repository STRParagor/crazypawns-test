using UnityEngine;
using CrazyPawns.CameraController;
using CrazyPawns.Checkerboard;
using CrazyPawns.Configs;
using CrazyPawns.Pawns;

namespace CrazyPawns.Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CrazyPawnSettings _settings;
        [SerializeField] private PawnManager _pawnManager;
        [SerializeField] private BoundsClampTransformHandler _cameraBounds;
        [SerializeField] private PawnDragInputHandler _pawnDragInputHandler;

        [Header("Board Settings")]
        [SerializeField] private float _boardCellSize = 1.5f;
        [SerializeField] private Material _boardWhiteCellMaterial;
        [SerializeField] private Material _boardBlackCellMaterial;

        private void Start()
        {
            var args = new CheckerboardGeneratorArgs()
            {
                BoardSize = _settings.CheckerboardSize,
                CellSize = _boardCellSize,
                WhiteCellMaterial = _boardWhiteCellMaterial,
                BlackCellMaterial = _boardBlackCellMaterial,
            };
            
            var checkerboard = CheckerboardGenerator.Generate(args);
            
            checkerboard.SetWhiteCellColor(_settings.WhiteCellColor);
            checkerboard.SetBlackCellColor(_settings.BlackCellColor);

            var cameraBounds = checkerboard.Bounds.size;
            var cameraSize = new Vector2(cameraBounds.x + 10, cameraBounds.z + 15);

            _pawnManager.Initialize(_settings);
            _pawnDragInputHandler.SetBounds(checkerboard);
            _cameraBounds.SetBoundsSize(cameraSize);
        }
    }
}