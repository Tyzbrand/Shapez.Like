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
    private ExtractorUI extractorUI;
    private BuilderUI builderUI;
    private FoundryUI foundryUI;
    private AdvancedExtractorUI advancedExtractorUI;
    private UIManager uIManager;
    private CoalGeneratorUI coalGeneratorUI;

    private BuildingLibrary buildingLib;



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
        buildingLib = ReferenceHolder.instance.buildingLibrary;

    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode && !player.isInUI && !player.destructionMode)//Ouvrir le menu pause
        {
            pauseMenu.TogglePauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && player.buildMode)//quiiter le build mode ET le menu (retour normal)
        {
            uIManager.TogglePanel(buildMenuSC.panel, () => buildMenuSC.BuildMenuOnShow(), () => buildMenuSC.BuildMenuOnHide());
            player.buildMode = false;
            previewSC.DestroyInstance();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode && player.destructionMode) destructionSC.DestructionSet(); //quitter le mode destruction

        if (Input.GetMouseButtonDown(1) && !player.buildMenu && player.buildMode)//quitter le build mode et revenir au menu
        {
            uIManager.TogglePanel(buildMenuSC.panel, () => buildMenuSC.BuildMenuOnShow(), () => buildMenuSC.BuildMenuOnHide());
            player.buildMode = false;
            previewSC.DestroyInstance();
        }

        if (Input.GetMouseButtonDown(0) && !player.buildMode && !player.buildMenu && !player.destructionMode && !EventSystem.current.IsPointerOverGameObject()) //Toggle les ui des batiments
        {
            Vector2 mousePos2D = cam.ScreenToWorldPoint(Input.mousePosition);
            var buildingSelected = buildingManager.GetBuildingOnTile(mousePos2D);

            if (buildingSelected != null)
            {
                var currentPanel = uIManager.GetOpenPanel();
                var Type = buildingSelected.buildingType;
                var uI = buildingLib.GetBuildingUI(Type);

                if (uI != null)
                {
                    var OnHide = buildingLib.GetBuildingOnHide(Type);
                    var OnSHow = buildingLib.GetBuildingOnShow(Type);


                    if (currentPanel != uI) uIManager.TogglePanel(uI, OnSHow, OnHide);
                }

            }
            else if (player.isInUI) return;

        }

        if (Input.GetKeyDown(KeyCode.Escape) && player.isInUI && !player.isInPauseUI && !player.destructionMode) //fermer un UI ouvert
        {
            VisualElement currentPanel = uIManager.GetOpenPanel();
            if (currentPanel != null) hideAllPanel(currentPanel);

        }
    }


    private void hideAllPanel(VisualElement currentPanel)
    {
        if (currentPanel == hubUI.panel) uIManager.HidePanel(currentPanel, () => hubUI.HubUIOnHide());
        else if (currentPanel == extractorUI.panel) uIManager.HidePanel(currentPanel, () => extractorUI.ExtractorUIOnHide());
        else if (currentPanel == foundryUI.panel) uIManager.HidePanel(currentPanel, () => foundryUI.FoundryUIOnHide());
        else if (currentPanel == buildMenuSC.panel) uIManager.HidePanel(currentPanel, () => buildMenuSC.BuildMenuOnHide());
        else if (currentPanel == builderUI.panel) uIManager.HidePanel(currentPanel, () => builderUI.BuilderUIOnHide());
        else if (currentPanel == coalGeneratorUI.panel) uIManager.HidePanel(currentPanel, () => coalGeneratorUI.CoalGeneratorUIOnHide());
        else if (currentPanel == advancedExtractorUI.panel) uIManager.HidePanel(currentPanel, () => advancedExtractorUI.AExtractorUIOnHide());
    
    }




}

    

