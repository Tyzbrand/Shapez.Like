using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Upgrade : MonoBehaviour
{
    private BuildingManager buildingManager;
    private Camera cam;
    private PlayerVariables player;


    private Vector3Int lasTilePos;


    private bool isHoldingUpgrade = false;


    //----------Méthodes UNITY----------

    private void Start()
    {
        buildingManager = ReferenceHolder.instance.buildingManager;
        cam = ReferenceHolder.instance.mainCamera;
        player = ReferenceHolder.instance.playervariable;
    }

    private void Update()
    {

        if (isHoldingUpgrade && player.upgradeMode)
        {   
            Vector3 mousePos3D = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos3D.z = 0;
            Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y);

            var tilemap = player.tilemap1;

            Vector3Int cellPos = tilemap.WorldToCell(mousePos2D);
            TileBase underTile = tilemap.GetTile(cellPos);

            var clickedBuilding = buildingManager.GetBuildingOnTile(mousePos2D);

            if (clickedBuilding is Conveyor conveyor && cellPos != lasTilePos)
            {
                lasTilePos = cellPos;
                if (conveyor.level > 2) return;

                conveyor.UpgradeLevel();
                Debug.Log("amélioré en continu!");
            }
        }
    }





    public void KeyUpgrade(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && player.upgradeMode) isHoldingUpgrade = true;
        else if (context.phase == InputActionPhase.Canceled && player.upgradeMode) isHoldingUpgrade = false;
    }
}
