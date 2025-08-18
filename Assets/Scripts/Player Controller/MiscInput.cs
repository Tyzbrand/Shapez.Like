using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MiscInput : MonoBehaviour
{

    private PlayerVariables player;

    private PauseMenuSC pauseMenu;
    private BuildingManager buildingManager;
    private BuildMenuSC buildMenuSC;
    private Preview previewSC;
    private Destruction destructionSC;
    private HubUI hubUI;


    private Camera cam;







    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        pauseMenu = ReferenceHolder.instance.pauseMenu;
        buildMenuSC = ReferenceHolder.instance.buildMenu;
        previewSC = ReferenceHolder.instance.previewSC;
        destructionSC = ReferenceHolder.instance.destructionSC;
        hubUI = ReferenceHolder.instance.hubUI;
        buildingManager = ReferenceHolder.instance.buildingManager;
        cam = ReferenceHolder.instance.mainCamera;
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode && !player.isInUI && !player.destructionMode)//Ouvrir le menu pause
        {
            pauseMenu.TogglePauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && player.buildMenu) buildMenuSC.BuildMenuOff(); //Fermer le build menu
        else if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && player.buildMode)//quiiter le build mode ET le menu (retour normal)
        {
            buildMenuSC.BuildMenuOff();
            player.buildMode = false;
            previewSC.DestroyInstance();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode && player.destructionMode) destructionSC.DestructionSet(); //quitter le mode destruction

        if (Input.GetMouseButtonDown(1) && !player.buildMenu && player.buildMode)//quitter le build mode et revenir au menu
        {
            buildMenuSC.BuildMenuOn();
            player.buildMode = false;
            previewSC.DestroyInstance();
        }

        if (Input.GetMouseButtonDown(0) && !player.buildMode && !player.buildMenu && !player.destructionMode && !EventSystem.current.IsPointerOverGameObject()) //ouvrir l'ui du Hub
        {
            Vector2 mousePos2D = cam.ScreenToWorldPoint(Input.mousePosition);
            if (buildingManager.GetBuildingOnTile(mousePos2D) is Hub) hubUI.HubUIToggle();
        }






    }
    

}
