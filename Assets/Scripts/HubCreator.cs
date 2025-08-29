using UnityEngine;
using UnityEngine.Tilemaps;

public class HubCreator : MonoBehaviour
{
    private BuildingManager buildingManager;
    private ItemManager itemManager;
    private PlayerVariables player;
    public Tilemap tilemap;
    public Transform parentItem;
    public Transform parentBuilding;


    void Awake()
    {
        player = ReferenceHolder.instance.playervariable;
        player.tilemap1 = tilemap;


    }


    void Start()
    {
        buildingManager = ReferenceHolder.instance.buildingManager;
        itemManager = ReferenceHolder.instance.itemManager;
        itemManager.parent = parentItem;
        buildingManager.parent = parentBuilding;


        buildingManager.HubCreation();
        itemManager.PreInstanciateVisuals(100, parentItem);
        buildingManager.PreInstanciateVisual(18, parentBuilding);
    }
}
