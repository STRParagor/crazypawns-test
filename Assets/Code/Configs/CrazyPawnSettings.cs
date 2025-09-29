using UnityEngine;

namespace CrazyPawns.Configs
{
    [CreateAssetMenu(menuName = "CrazyPawn/Settings", fileName = "CrazyPawnSettings")]
    public class CrazyPawnSettings : ScriptableObject
    {
        [field: SerializeField] public float InitialZoneRadius { get; private set; } = 10f;
        [field: SerializeField] public int InitialPawnCount { get; private set; } = 7;

        [field: SerializeField] public Material DeleteMaterial { get; private set; }
        [field: SerializeField] public Material ActiveConnectorMaterial { get; private set; }

        [field: SerializeField] public int CheckerboardSize { get; private set; } = 18;
        [field: SerializeField] public Color BlackCellColor { get; private set; } = Color.yellow;
        [field: SerializeField] public Color WhiteCellColor { get; private set; } = Color.green;
    }
}