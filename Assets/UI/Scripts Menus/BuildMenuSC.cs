using UnityEngine;
using UnityEngine.UIElements;

public class BuildMenuSC : MonoBehaviour
{
    private UIDocument uI;
    public VisualElement panel;

    private PlayerVariables player;
    private Preview previewSC;
    private Placement placementSC;
    private BuildingLibrary buildPriceLibrary;
    private UIManager uIManager;

    private Label extractorPrice, conveyorPrice, marketplacePrice, foundryPrice, builderPrice, coalGeneratorPrice, advancedExtractorPrice;
    private Button extractorBuild, conveyorBuild, marketplaceBuild, foundryBuild, builderBuild, coalGeneratorBuild, advancedExtractorBuild;




    private void Start()
    {
        uI = ReferenceHolder.instance.uIDocument;
        player = ReferenceHolder.instance.playervariable;
        previewSC = ReferenceHolder.instance.previewSC;
        placementSC = ReferenceHolder.instance.placementSC;
        buildPriceLibrary = ReferenceHolder.instance.buildingLibrary;
        uIManager = ReferenceHolder.instance.uIManager;

        panel = uI.rootVisualElement.Q<VisualElement>("BuildMenu");
        uIManager.RegisterPanel(panel);



        extractorPrice = panel.Q<Label>("ExtractorPriceTxt");
        conveyorPrice = panel.Q<Label>("ConveyorPriceTxt");
        marketplacePrice = panel.Q<Label>("MarketplacePriceTxt");
        foundryPrice = panel.Q<Label>("FoundryPriceTxt");
        builderPrice = panel.Q<Label>("BuilderPriceTxt");
        coalGeneratorPrice = panel.Q<Label>("CoalGeneratorPriceTxt");
        advancedExtractorPrice = panel.Q<Label>("AExtractorPriceTxt");

        extractorBuild = panel.Q<Button>("ExtractorBuildBtn");
        conveyorBuild = panel.Q<Button>("ConveyorBuildBtn");
        marketplaceBuild = panel.Q<Button>("MarketplaceBuildBtn");
        foundryBuild = panel.Q<Button>("FoundryBuildBtn");
        builderBuild = panel.Q<Button>("BuilderBuildBtn");
        coalGeneratorBuild = panel.Q<Button>("CoalGeneratorBuildBtn");
        advancedExtractorBuild = panel.Q<Button>("AExtractorBuildBtn");


        extractorBuild.clicked -= ExtractorSelect;
        advancedExtractorBuild.clicked -= AdvancedExtractorSelect;
        conveyorBuild.clicked -= ConveyorSelect;
        marketplaceBuild.clicked -= MarketplaceSelect;
        foundryBuild.clicked -= FoundrySelect;
        builderBuild.clicked -= BuilderSelect;
        coalGeneratorBuild.clicked -= CoalGeneratorSelect;

        extractorBuild.clicked += ExtractorSelect;
        advancedExtractorBuild.clicked += AdvancedExtractorSelect;
        conveyorBuild.clicked += ConveyorSelect;
        marketplaceBuild.clicked += MarketplaceSelect;
        foundryBuild.clicked += FoundrySelect;
        builderBuild.clicked += BuilderSelect;
        coalGeneratorBuild.clicked += CoalGeneratorSelect;

        extractorPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Extractor) + " $";
        conveyorPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Conveyor) + " $";
        marketplacePrice.text = "Free";
        foundryPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Foundry) + " $";
        builderPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.Extractor) + " $";
        coalGeneratorPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.CoalGenerator) + " $";
        advancedExtractorPrice.text = buildPriceLibrary.GetBuildingPrice(BuildingManager.buildingType.AdvancedExtractor) + " $";

    }


    //---------Méthodes de sélection de build----------

    private void ExtractorSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        previewSC.previewToUse = ReferenceHolder.instance.extractorPreview;
        placementSC.currentBuild = ReferenceHolder.instance.extractorPrefab;
        placementSC.currentBuildingType = BuildingManager.buildingType.Extractor;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }
    
    private void AdvancedExtractorSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        previewSC.previewToUse = ReferenceHolder.instance.advancedExtractorPreview;
        placementSC.currentBuild = ReferenceHolder.instance.advancedExtractorPrefab;
        placementSC.currentBuildingType = BuildingManager.buildingType.AdvancedExtractor;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void ConveyorSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        previewSC.previewToUse = ReferenceHolder.instance.conveyorPreview;
        placementSC.currentBuild = ReferenceHolder.instance.conveyorPrefab;
        placementSC.currentBuildingType = BuildingManager.buildingType.Conveyor;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void MarketplaceSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        previewSC.previewToUse = ReferenceHolder.instance.marketplacePreview;
        placementSC.currentBuild = ReferenceHolder.instance.marketplacePrefab;
        placementSC.currentBuildingType = BuildingManager.buildingType.marketplace;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void FoundrySelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        previewSC.previewToUse = ReferenceHolder.instance.foundryPreview;
        placementSC.currentBuild = ReferenceHolder.instance.foundryPrefab;
        placementSC.currentBuildingType = BuildingManager.buildingType.Foundry;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void BuilderSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        previewSC.previewToUse = ReferenceHolder.instance.builderPrview;
        placementSC.currentBuild = ReferenceHolder.instance.builderPrefab;
        placementSC.currentBuildingType = BuildingManager.buildingType.builder;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void CoalGeneratorSelect()
    {
        uIManager.TogglePanel(panel, () => BuildMenuOnShow(), () => BuildMenuOnHide());
        previewSC.previewToUse = ReferenceHolder.instance.coalGeneratorPreview;
        placementSC.currentBuild = ReferenceHolder.instance.coalGeneratorPrefab;
        placementSC.currentBuildingType = BuildingManager.buildingType.CoalGenerator;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }



    //----------Méthodes de gestion du menu----------

    public void BuildMenuOnShow()
    {
        player.isInUI = true;
        player.buildMenu = true;

    }

    public void BuildMenuOnHide()
    {
        player.isInUI = false;
        player.buildMenu = false;
        previewSC.DestroyInstance();

    }

}
