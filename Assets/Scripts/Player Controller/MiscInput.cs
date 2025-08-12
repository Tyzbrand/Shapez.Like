using UnityEngine;
using UnityEngine.InputSystem;

public class MiscInput : MonoBehaviour
{

    private PlayerVariables player;
    private Camera cam;
    private PlayerSwitchMode switchMode;
    private GameObject inventoryPanel;
    private BuildingManager buildingManager;



    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        switchMode = ReferenceHolder.instance.playerSwitchMode;
        cam = ReferenceHolder.instance.mainCamera;
        inventoryPanel = ReferenceHolder.instance.inventoryMenu;
        buildingManager = ReferenceHolder.instance.buildingManager;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !player.buildMenu && !player.buildMode) //Selection UI batiment Ingame
        {
            Vector2 mousPos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (buildingManager.GetBuildingOnTile(mousPos) is Hub)
            {
                switchMode.InventoryUIToggle();
            }

        if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode)
            {
                Application.Quit();
            }

            
            

                
            
        }
    }
}
