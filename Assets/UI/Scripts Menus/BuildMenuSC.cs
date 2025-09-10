using UnityEngine;
using UnityEngine.UIElements;

public class BuildMenuSC : MonoBehaviour
{
    private UIDocument uI;
    public VisualElement panel, buildTools;

    private PlayerVariables player;
    private Preview previewSC;
    private Placement placementSC;
    private BuildingLibrary buildPriceLibrary;
    private UIManager uIManager;
    private TooltipSC tooltipSC;

    private Label extractorPrice, conveyorPrice, depotPrice, foundryPrice, builderPrice, coalGeneratorPrice, advancedExtractorPrice,
        junctionPrice, splitterPrice, mergerPrice, wallPrice;
    private Button extractorBuild, conveyorBuild, depotBuild, foundryBuild, builderBuild, coalGeneratorBuild, advancedExtractorBuild,
        junctionBuild, splitterBuild, mergerBuild, wallBuild, rotateBtn;




    private void Start()
    {
        uI = ReferenceHolder.instance.uIDocument;
        player = ReferenceHolder.instance.playervariable;
        previewSC = ReferenceHolder.instance.previewSC;
        placementSC = ReferenceHolder.instance.placementSC;
        buildPriceLibrary = ReferenceHolder.instance.buildingLibrary;
        uIManager = ReferenceHolder.instance.uIManager;
        tooltipSC = ReferenceHolder.instance.tooltipSC;

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

    //----------Méthodes de gestion du menu----------

    public void BuildMenuOnShow()
    {
        player.isInUI = true;
        player.buildMenu = true;
        buildTools.style.display = DisplayStyle.None;

    }

    public void BuildMenuOnHide()
    {   
        buildTools.style.display = DisplayStyle.None;
        player.isInUI = false;
        player.buildMenu = false;
        previewSC.DestroyInstance();

    }

}
