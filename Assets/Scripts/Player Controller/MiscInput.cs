using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MiscInput : MonoBehaviour
{

    private PlayerVariables player;
    private Camera cam;
    private PlayerSwitchMode switchMode;
    private GameObject inventoryPanel;
    private BuildingManager buildingManager;
    private GameObject ingameOverlay;
    private PauseMenuSC pauseMenu;





    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        switchMode = ReferenceHolder.instance.playerSwitchMode;
        cam = ReferenceHolder.instance.mainCamera;
        inventoryPanel = ReferenceHolder.instance.inventoryMenu;
        buildingManager = ReferenceHolder.instance.buildingManager;
        ingameOverlay = ReferenceHolder.instance.ingameOverlay;
        pauseMenu = ReferenceHolder.instance.pauseMenu;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !player.buildMenu && !player.buildMode) //Selection UI batiment Ingame
        {
            Vector2 mousPos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (buildingManager.GetBuildingOnTile(mousPos) is Hub && !player.isInUI)
            {
                switchMode.InventoryUIToggle();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode)
        {
            pauseMenu.TogglePauseMenu();
        }



    }
    

}
