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

        if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode && !player.isInUI && !player.destructionMode)//Ouvrir le menu pause
        {
            TogglePauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && player.buildMode)//quiiter le build mode ET le menu (retour normal)
        {
            QuiBuildFunction();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode && player.destructionMode) QuitDestructionMode(); //quitter le mode destruction

        if (Input.GetMouseButtonDown(1) && !player.buildMenu && player.buildMode)//quitter le build mode et revenir au menu
        {
            ComeBackToBuildMenu();
        }

        if (Input.GetMouseButtonDown(0) && !player.buildMode && !player.buildMenu && !player.destructionMode && !EventSystem.current.IsPointerOverGameObject()) //Toggle les ui des batiments
        {

            Vector2 mousePos2D = cam.ScreenToWorldPoint(Input.mousePosition);
            var buildingSelected = buildingManager.GetBuildingOnTile(mousePos2D);

            var currentPanel = uIManager.GetOpenPanel();

            if (buildingSelected is Hub) uIManager.TogglePanel(hubUI.panel, () => hubUI.HubUIOnShow(), () => hubUI.HubUIOnHide());
            else if (buildingSelected is Extractor extractor) //Extracteur
            {
                if (currentPanel == extractorUI.panel)
                {
                    if (buildingSelected == lastBuilding) uIManager.TogglePanel(extractorUI.panel, () => extractorUI.ExtractorUIOnShow(extractor), () => extractorUI.ExtractorUIOnHide());
                    else { extractorUI.refreshUI(extractor); lastBuilding = buildingSelected; }
                }
                else
                {
                    uIManager.TogglePanel(extractorUI.panel, () => extractorUI.ExtractorUIOnShow(extractor), () => extractorUI.ExtractorUIOnHide());
                    lastBuilding = buildingSelected;
                }
            }
            else if (buildingSelected is Foundry foundry) //Foundry
            {
                if (currentPanel == foundryUI.panel)
                {
                    if (buildingSelected == lastBuilding) uIManager.TogglePanel(foundryUI.panel, () => foundryUI.FoundryUIOnShow(foundry), () => foundryUI.FoundryUIOnHide());
                    else { foundryUI.refreshUI(foundry); lastBuilding = buildingSelected; }
                }
                else
                {
                    uIManager.TogglePanel(foundryUI.panel, () => foundryUI.FoundryUIOnShow(foundry), () => foundryUI.FoundryUIOnHide());
                    lastBuilding = buildingSelected;
                }
            }
            else if (buildingSelected is Builder builder) //Builder
            {
                if (currentPanel == builderUI.panel)
                {
                    if (buildingSelected == lastBuilding) uIManager.TogglePanel(builderUI.panel, () => builderUI.BuilderUIOnShow(builder), () => builderUI.BuilderUIOnHide());
                    else { builderUI.refreshUI(builder); lastBuilding = buildingSelected; }
                }
                else
                {
                    uIManager.TogglePanel(builderUI.panel, () => builderUI.BuilderUIOnShow(builder), () => builderUI.BuilderUIOnHide());
                    lastBuilding = buildingSelected;
                }
            }
            else if (buildingSelected is CoalGenerator coalGenerator) //coal Generator
            {
                if (currentPanel == coalGeneratorUI.panel)
                {
                    if (buildingSelected == lastBuilding) uIManager.TogglePanel(coalGeneratorUI.panel, () => coalGeneratorUI.CoalGeneratorUIOnShow(coalGenerator), () => coalGeneratorUI.CoalGeneratorUIOnHide());
                    else { coalGeneratorUI.refreshUI(coalGenerator); lastBuilding = buildingSelected; }
                }
                else
                {
                    uIManager.TogglePanel(coalGeneratorUI.panel, () => coalGeneratorUI.CoalGeneratorUIOnShow(coalGenerator), () => coalGeneratorUI.CoalGeneratorUIOnHide());
                    lastBuilding = buildingSelected;
                }
            }
            else if (buildingSelected is AdvancedExtractor advancedExtractor) //Advanced Extractor
            {
                if (currentPanel == advancedExtractorUI.panel)
                {
                    if (buildingSelected == lastBuilding) uIManager.TogglePanel(advancedExtractorUI.panel, () => advancedExtractorUI.AExtractorUIOnShow(advancedExtractor), () => advancedExtractorUI.AExtractorUIOnHide());
                    else { advancedExtractorUI.refreshUI(advancedExtractor); lastBuilding = buildingSelected; }
                }
                else
                {
                    uIManager.TogglePanel(advancedExtractorUI.panel, () => advancedExtractorUI.AExtractorUIOnShow(advancedExtractor), () => advancedExtractorUI.AExtractorUIOnHide());
                    lastBuilding = buildingSelected;
                }
            }
            else if (player.isInUI) return;

        }

        if (Input.GetKeyDown(KeyCode.Escape) && !player.isInPauseUI && !player.destructionMode) //fermer un UI ouvert
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

    //----------Méthodes de gestion d'ui----------

    private void TogglePauseMenu()
    {
        if (uIManager.GetOpenPanel() == null) pauseMenu.TogglePauseMenu();
    }

    private void QuiBuildFunction()
    {
        uIManager.TogglePanel(buildMenuSC.panel, () => buildMenuSC.BuildMenuOnShow(), () => buildMenuSC.BuildMenuOnHide());
        player.buildMode = false;
        placement.hasPickup = false;
        previewSC.DestroyInstance();
    }

    private void QuitDestructionMode()
    {
        destructionSC.DestructionSet();
    }

    public void ComeBackToBuildMenu()
    {
        uIManager.TogglePanel(buildMenuSC.panel, () => buildMenuSC.BuildMenuOnShow(), () => buildMenuSC.BuildMenuOnHide());
        player.buildMode = false;
        placement.hasPickup = false;
        previewSC.DestroyInstance();
    }



}

    

