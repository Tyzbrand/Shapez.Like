using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BuildMenuSC : MonoBehaviour
{
    private UIDocument uI;
    public VisualElement panel, buildTools;

    private PlayerVariables player;
    private Preview previewSC;
    private Placement placementSC;
    private StackFeature stackFeature;
    private BuildingLibrary buildPriceLibrary;
    private UIManager uIManager;
    private TooltipSC tooltipSC;

    private Button extractorBuild, conveyorBuild, depotBuild, foundryBuild, builderBuild, coalGeneratorBuild, advancedExtractorBuild,
        junctionBuild, splitterBuild, mergerBuild, wallBuild, rotateBtn, pickupBtn, lineBuildBtn, undoBtn, redoBtn;
    private Label extractorPrice, conveyorPrice, depotPrice, foundryPrice, builderPrice, coalGeneratorPrice, advancedExtractorPrice,
        junctionPrice, splitterPrice, mergerPrice, wallPrice;




    private void Start()
    {
        uI = ReferenceHolder.instance.uIDocument;
        player = ReferenceHolder.instance.playervariable;
        previewSC = ReferenceHolder.instance.previewSC;
        placementSC = ReferenceHolder.instance.placementSC;
        buildPriceLibrary = ReferenceHolder.instance.buildingLibrary;
        uIManager = ReferenceHolder.instance.uIManager;
        tooltipSC = ReferenceHolder.instance.tooltipSC;
        stackFeature = ReferenceHolder.instance.stackFeature;

        panel = uI.rootVisualElement.Q<VisualElement>("BuildMenu");
        uIManager.RegisterPanel(panel);
        buildTools = uI.rootVisualElement.Q<VisualElement>("BuildTools");


        extractorPrice = panel.Q<Label>("ExtractorPriceTxt");
        conveyorPrice = panel.Q<Label>("ConveyorPriceTxt");
        depotPrice = panel.Q<Label>("MarketplacePriceTxt");
        foundryPrice = panel.Q<Label>("FoundryPriceTxt");
        builderPrice = panel.Q<Label>("BuilderPriceTxt");
        coalGeneratorPrice = panel.Q<Label>("CoalGeneratorPriceTxt");
        advancedExtractorPrice = panel.Q<Label>("AExtractorPriceTxt");
        junctionPrice = panel.Q<Label>("JunctionPriceTxt");
        splitterPrice = panel.Q<Label>("SplitterPriceTxt");
        mergerPrice = panel.Q<Label>("MergerPriceTxt");
        wallPrice = panel.Q<Label>("WallPriceTxt");

        extractorBuild = panel.Q<Button>("ExtractorBuildBtn");
        conveyorBuild = panel.Q<Button>("ConveyorBuildBtn");
        depotBuild = panel.Q<Button>("MarketplaceBuildBtn");
        foundryBuild = panel.Q<Button>("FoundryBuildBtn");
        builderBuild = panel.Q<Button>("BuilderBuildBtn");
        coalGeneratorBuild = panel.Q<Button>("CoalGeneratorBuildBtn");
        advancedExtractorBuild = panel.Q<Button>("AExtractorBuildBtn");
        junctionBuild = panel.Q<Button>("JunctionBuildBtn");
        splitterBuild = panel.Q<Button>("SplitterBuildBtn");
        mergerBuild = panel.Q<Button>("MergerBuildBtn");
        wallBuild = panel.Q<Button>("WallBuildBtn");

        rotateBtn = buildTools.Q<Button>("RotateBtn");
        pickupBtn = buildTools.Q<Button>("PickUpBtn");
        lineBuildBtn = buildTools.Q<Button>("LineBuildBtn");
        undoBtn = buildTools.Q<Button>("UndoBtn");
        redoBtn = buildTools.Q<Button>("RedoBtn");

        //Désabonnements
        extractorBuild.clicked -= ExtractorSelect;
        advancedExtractorBuild.clicked -= AdvancedExtractorSelect;
        conveyorBuild.clicked -= ConveyorSelect;
        depotBuild.clicked -= DepotSelect;
        foundryBuild.clicked -= FoundrySelect;
        builderBuild.clicked -= BuilderSelect;
        coalGeneratorBuild.clicked -= CoalGeneratorSelect;
        junctionBuild.clicked -= JunctionSelect;
        splitterBuild.clicked -= SplitterSelect;
        mergerBuild.clicked -= MergerSelect;
        wallBuild.clicked -= WallSelect;

        rotateBtn.clicked -= Rotate;
        pickupBtn.clicked -= pickup;
        lineBuildBtn.clicked -= LineBuild;
        undoBtn.clicked -= Undo;
        redoBtn.clicked -= Redo;

        //Abonnements
        extractorBuild.clicked += ExtractorSelect;
        advancedExtractorBuild.clicked += AdvancedExtractorSelect;
        conveyorBuild.clicked += ConveyorSelect;
        depotBuild.clicked += DepotSelect;
        foundryBuild.clicked += FoundrySelect;
        builderBuild.clicked += BuilderSelect;
        coalGeneratorBuild.clicked += CoalGeneratorSelect;
        junctionBuild.clicked += JunctionSelect;
        splitterBuild.clicked += SplitterSelect;
        mergerBuild.clicked += MergerSelect;
        wallBuild.clicked += WallSelect;

        rotateBtn.clicked += Rotate;
        pickupBtn.clicked += pickup;
        lineBuildBtn.clicked += LineBuild;
        undoBtn.clicked += Undo;
        redoBtn.clicked += Redo;


        //Assignation des valeures
        extractorPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Extractor) + " $";
        conveyorPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Conveyor) + " $";
        depotPrice.text = "Free";
        foundryPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Foundry) + " $";
        builderPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Extractor) + " $";
        coalGeneratorPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.CoalGenerator) + " $";
        advancedExtractorPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.AdvancedExtractor) + " $";
        junctionPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Junction) + " $";
        splitterPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Splitter) + " $";
        mergerPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Merger) + " $";
        wallPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Wall) + " $";


        //Tooltips
        rotateBtn.RegisterCallback<PointerEnterEvent>(evt => tooltipSC.TooltipShow("Rotate"));
        rotateBtn.RegisterCallback<PointerLeaveEvent>(evt => tooltipSC.TooltipHide());

        pickupBtn.RegisterCallback<PointerEnterEvent>(evt => tooltipSC.TooltipShow("Pick up"));
        pickupBtn.RegisterCallback<PointerLeaveEvent>(evt => tooltipSC.TooltipHide());

    }


    //---------Méthodes de sélection de build----------

    private void ExtractorSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        buildTools.style.display = DisplayStyle.Flex;
        previewSC.previewToUse = ReferenceHolder.instance.extractorPreview;
        placementSC.currentBuildingType = BuildingManager.buildingType.Extractor;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void AdvancedExtractorSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        buildTools.style.display = DisplayStyle.Flex;
        previewSC.previewToUse = ReferenceHolder.instance.advancedExtractorPreview;
        placementSC.currentBuildingType = BuildingManager.buildingType.AdvancedExtractor;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void ConveyorSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        buildTools.style.display = DisplayStyle.Flex;
        previewSC.previewToUse = ReferenceHolder.instance.conveyorPreview;
        placementSC.currentBuildingType = BuildingManager.buildingType.Conveyor;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.lineBuild = true;
        player.rotation = 0;
    }

    private void DepotSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        buildTools.style.display = DisplayStyle.Flex;
        previewSC.previewToUse = ReferenceHolder.instance.depotPreview;
        placementSC.currentBuildingType = BuildingManager.buildingType.Depot;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void FoundrySelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        buildTools.style.display = DisplayStyle.Flex;
        previewSC.previewToUse = ReferenceHolder.instance.foundryPreview;
        placementSC.currentBuildingType = BuildingManager.buildingType.Foundry;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void BuilderSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        buildTools.style.display = DisplayStyle.Flex;
        previewSC.previewToUse = ReferenceHolder.instance.builderPrview;
        placementSC.currentBuildingType = BuildingManager.buildingType.builder;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void CoalGeneratorSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        buildTools.style.display = DisplayStyle.Flex;
        previewSC.previewToUse = ReferenceHolder.instance.coalGeneratorPreview;
        placementSC.currentBuildingType = BuildingManager.buildingType.CoalGenerator;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void JunctionSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        buildTools.style.display = DisplayStyle.Flex;
        previewSC.previewToUse = ReferenceHolder.instance.junctionPreview;
        placementSC.currentBuildingType = BuildingManager.buildingType.Junction;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void SplitterSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        buildTools.style.display = DisplayStyle.Flex;
        previewSC.previewToUse = ReferenceHolder.instance.splitterPreview;
        placementSC.currentBuildingType = BuildingManager.buildingType.Splitter;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void MergerSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        buildTools.style.display = DisplayStyle.Flex;
        previewSC.previewToUse = ReferenceHolder.instance.mergerPreview;
        placementSC.currentBuildingType = BuildingManager.buildingType.Merger;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }
    
    private void WallSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        buildTools.style.display = DisplayStyle.Flex;
        previewSC.previewToUse = ReferenceHolder.instance.wallPreview;
        placementSC.currentBuildingType = BuildingManager.buildingType.Wall;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    //----------Button panel----------

    private void Rotate()
    {
        if (player.buildMode) player.rotation = (player.rotation + 90) % 360;
    }

    private void pickup()
    {
        if (!player.pickupMode) { player.pickupMode = true; previewSC.DestroyInstance(); player.buildMode = false; }
        else if (player.pickupMode)
        {
            player.pickupMode = false;
            uIManager.ShowPanel(panel, () => BuildMenuOnShow());
        }
    }

    private void LineBuild()
    {
        if (!player.lineBuild) { player.lineBuild = true; }
        else if (player.lineBuild){ player.lineBuild = false; }
    }

    private void Undo()
    {
        stackFeature.Undo();
    }

    private void Redo()
    {
        stackFeature.Redo();
    }

    //----------Gestion des Inputs----------
    public void pickupCTX(InputAction.CallbackContext context)
    {
        if (!context.performed || buildTools.resolvedStyle.display == DisplayStyle.None) return;
        if (!player.pickupMode) { player.pickupMode = true; previewSC.DestroyInstance(); player.buildMode = false; }
        else if (player.pickupMode)
        {
            player.pickupMode = false;
            uIManager.ShowPanel(panel, () => BuildMenuOnShow());
        }
    }

    public void LineBuildCTX(InputAction.CallbackContext context)
    {
        if (!context.performed || buildTools.resolvedStyle.display == DisplayStyle.None) return;
        if (!player.lineBuild) { player.lineBuild = true; }
        else if (player.lineBuild) { player.lineBuild = false; }
    }

    public void UndoCTX(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if(Keyboard.current.ctrlKey.isPressed) stackFeature.Undo();
    
        Debug.Log("UNDO !");
    }

    public void RedoCTX(InputAction.CallbackContext context)
    {   
        if (!context.performed) return;
        if(Keyboard.current.ctrlKey.isPressed) stackFeature.Redo();
        Debug.Log("Redo !");
    }



    //----------Méthodes de gestion du menu----------

    public void BuildMenuOnShow()
    {
        player.isInUI = true;
        player.buildMenu = true;
        buildTools.style.display = DisplayStyle.Flex;

    }

    public void BuildMenuOnHide()
    {
        if(!player.buildMode) buildTools.style.display = DisplayStyle.None;
        player.isInUI = false;
        player.buildMenu = false;
        player.pickupMode = false;
        player.lineBuild = false;
        previewSC.DestroyInstance();

    }


    //----------Gestion des états----------
    private void UpdateButtonState(Button button, bool state)
    {
        if (state) button.style.backgroundColor = new Color(100f / 255f, 100f / 255f, 90f / 255f, 1f);
        else button.style.backgroundColor = new Color(44f / 255f, 44f / 255f, 41f / 255f, 1f);
    }

    //----------Méthodes unity----------

    private void Update()
    {
        if (buildTools.resolvedStyle.display == DisplayStyle.Flex)
        {
            UpdateButtonState(pickupBtn, player.pickupMode);
            UpdateButtonState(lineBuildBtn, player.lineBuild);
        }
    }

}
