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
    private UIManager uIManager;
    private CoalGeneratorUI coalGeneratorUI;



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
        foundryUI = ReferenceHolder.instance.foundryUI;
        buildingManager = ReferenceHolder.instance.buildingManager;
        cam = ReferenceHolder.instance.mainCamera;
        uIManager = ReferenceHolder.instance.uIManager;
        coalGeneratorUI = ReferenceHolder.instance.coalGeneratorUI;

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
            var currentPanel = uIManager.GetOpenPanel();

            if (buildingSelected is Hub) uIManager.TogglePanel(hubUI.panel, () => hubUI.HubUIOnShow(), () => hubUI.HubUIOnHide());
            else if (buildingSelected is Extractor extractor)
            {
                if (currentPanel == extractorUI.panel) extractorUI.refreshUI(extractor);
                else uIManager.TogglePanel(extractorUI.panel, () => extractorUI.ExtractorUIOnShow(extractor), () => extractorUI.ExtractorUIOnHide());
            }
            else if (buildingSelected is Foundry foundry)
            {
                if (currentPanel == foundryUI.panel) foundryUI.refreshUI(foundry);
                else uIManager.TogglePanel(foundryUI.panel, () => foundryUI.FoundryUIOnShow(foundry), () => foundryUI.FoundryUIOnHide());
            }
            else if (buildingSelected is Builder builder)
            {
                if (currentPanel == builderUI.panel) builderUI.refreshUI(builder);
                else uIManager.TogglePanel(builderUI.panel, () => builderUI.BuilderUIOnShow(builder), () => builderUI.BuilderUIOnHide());
            }
            else if (buildingSelected is CoalGenerator coalGenerator)
            {
                if (currentPanel == coalGeneratorUI.panel) coalGeneratorUI.refreshUI(coalGenerator);
                else uIManager.TogglePanel(coalGeneratorUI.panel, () => coalGeneratorUI.CoalGeneratorUIOnShow(coalGenerator), () => coalGeneratorUI.CoalGeneratorUIOnHide());
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && player.isInUI && !player.isInPauseUI && !player.destructionMode) //fermer un UI ouvert
        {
            VisualElement currentPanel = uIManager.GetOpenPanel();
            if (currentPanel != null)
            {
                if (currentPanel == hubUI.panel) uIManager.HidePanel(currentPanel, () => hubUI.HubUIOnHide());
                else if (currentPanel == extractorUI.panel) uIManager.HidePanel(currentPanel, () => extractorUI.ExtractorUIOnHide());
                else if (currentPanel == foundryUI.panel) uIManager.HidePanel(currentPanel, () => foundryUI.FoundryUIOnHide());
                else if (currentPanel == buildMenuSC.panel) uIManager.HidePanel(currentPanel, () => buildMenuSC.BuildMenuOnHide());
                else if (currentPanel == builderUI.panel) uIManager.HidePanel(currentPanel, () => builderUI.BuilderUIOnHide());
                else if (currentPanel == coalGeneratorUI.panel) uIManager.HidePanel(currentPanel, () => coalGeneratorUI.CoalGeneratorUIOnHide());
            }


        }





    }
    

}
