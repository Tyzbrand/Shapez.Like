using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileType", menuName = "Tile/RessourceTile")]
public class TileType : Tile
{
    public RessourceBehaviour.RessourceType tileType;
}
