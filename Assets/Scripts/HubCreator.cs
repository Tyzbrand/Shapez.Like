using UnityEngine;
using UnityEngine.Tilemaps;

public class HubCreator : MonoBehaviour
{
    private BuildingManager buildingManager;
    private PlayerVariables player;
    public Tilemap tilemap;

    void Awake()
    {
        player = ReferenceHolder.instance.playervariable;
        player.tilemap1 = tilemap;
    }


    void Start()
    {
        buildingManager = ReferenceHolder.instance.buildingManager;


        buildingManager.HubCreation();
    }
}
