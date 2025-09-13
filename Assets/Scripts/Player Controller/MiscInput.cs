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
    private Placement placement;
    private ExtractorUI extractorUI;
    private BuilderUI builderUI;
    private FoundryUI foundryUI;
    private AdvancedExtractorUI advancedExtractorUI;
    private UIManager uIManager;
    private CoalGeneratorUI coalGeneratorUI;

    public BuildingBH lastBuilding = null;



    private Camera cam;







    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        pauseMenu = ReferenceHolder.instance.pauseMenu;
        buildMenuSC = ReferenceHolder.instance.buildMenu;
        previewSC = ReferenceHolder.instance.previewSC;
        destructionSC = ReferenceHolder.instance.destructionSC;
        hubUI = ReferenceHolder.instance.hubUI;
        extractorUI = ReferenceHolder.instance.extractorUI;
        builderUI = ReferenceHolder.instance.builderUI;
        advancedExtractorUI = ReferenceHolder.instance.advancedExtractorUI;
        foundryUI = ReferenceHolder.instance.foundryUI;
        buildingManager = ReferenceHolder.instance.buildingManager;
        cam = ReferenceHolder.instance.mainCamera;
        uIManager = ReferenceHolder.instance.uIManager;
        coalGeneratorUI = ReferenceHolder.instance.coalGeneratorUI;
        placement = ReferenceHolder.instance.placementSC;


    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode && !player.isInMenu && !player.destructionMode && !player.pickupMode)//Ouvrir le menu pause
        {
            TogglePauseMenu();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && player.pickupMode && !placement.hasPickup) { QuitPickUpModeEmpty(); return; } //Quitter le pickup mode sans avoir choisi
        else if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && player.buildMode && !player.pickupMode)//quiiter le build mode ET le menu (retour normal)
        {
            QuiBuildFunction();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode && player.destructionMode && !player.pickupMode) QuitDestructionMode(); //quitter le mode destruction

        if (Input.GetMouseButtonDown(1) && !player.buildMenu && player.buildMode)//quitter le build mode et revenir au menu
        {
            ComeBackToBuildMenu();
        }

        if (Input.GetMouseButtonDown(0) && !player.buildMode && !player.buildMenu && !player.destructionMode && !EventSystem.current.IsPointerOverGameObject()) SelectBuilding(); //Selectionner les batiments


        if (Input.GetKeyDown(KeyCode.Escape) && !player.isInPauseUI && !player.destructionMode && !player.pickupMode) //fermer un UI ouvert
        {
            VisualElement currentPanel = uIManager.GetOpenPanel();
            uIManager.hideOpenPanel();

        }

    }


    //----------Méthodes de gestion d'ui----------

    private void TogglePauseMenu()
    {
        if (uIManager.GetOpenPanel() == null) pauseMenu.TogglePauseMenu();

    }

    private void QuiBuildFunction()
    {
        uIManager.TogglePanel(UIManager.uIType.BuildMenu, () => buildMenuSC.UIOnShow(null), () => buildMenuSC.UIOnHide());
        player.buildMode = false;
        placement.hasPickup = false;
        player.lineBuild = false;
        previewSC.DestroyInstance();
    }

    private void QuitDestructionMode()
    {
        destructionSC.DestructionSet();
    }

    public void ComeBackToBuildMenu()
    {
        uIManager.TogglePanel(UIManager.uIType.BuildMenu, () => buildMenuSC.UIOnShow(null), () => buildMenuSC.UIOnHide());
        player.buildMode = false;
        placement.hasPickup = false;
        player.lineBuild = false;
        previewSC.DestroyInstance();
    }

    public void QuitPickUpModeEmpty()
    {
        player.pickupMode = false;
        uIManager.ShowPanel(UIManager.uIType.BuildMenu, () => buildMenuSC.UIOnShow(null));
    }

    private void SelectBuilding()
    {
        Vector2 mousePos2D = cam.ScreenToWorldPoint(Input.mousePosition);
        var buildingSelected = buildingManager.GetBuildingOnTile(mousePos2D);

        var currentPanel = uIManager.GetOpenPanel();

        if (buildingSelected != null && buildingSelected.uIScript != null)
        {

            if (currentPanel == buildingSelected.uIScript.panel)
            {
                if (buildingSelected == lastBuilding) uIManager.TogglePanel(buildingSelected.buildingType, () => buildingSelected.uIScript.UIOnShow(buildingSelected), () => buildingSelected.uIScript.UIOnHide());
                else
                {
                    buildingSelected.uIScript.RefreshUI(buildingSelected);
                    lastBuilding.BuildingOnDeselect();
                    buildingSelected.BuildingOnSelect();
                    lastBuilding = buildingSelected;
                }
            }
            else
            {
                uIManager.TogglePanel(buildingSelected.buildingType, () => buildingSelected.uIScript.UIOnShow(buildingSelected), () => buildingSelected.uIScript.UIOnHide());
                lastBuilding = buildingSelected;
            }



        }
    }


}