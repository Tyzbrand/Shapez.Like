using UnityEngine;
using UnityEngine.UIElements;

public class BuildMenuSC : MonoBehaviour
{
    private UIDocument uI;
    private VisualElement panel;

    private PlayerVariables player;
    private Preview previewSC;
    private Placement placementSC;
    private BuildPriceDictionnary buildPriceLibrary;

    private Label extractorPrice, conveyorPrice, marketplacePrice, foundryPrice, builderPrice;
    private Button extractorBuild, conveyorBuild, marketplaceBuild, foundryBuild, builderBuild;




    private void Start()
    {
        uI = ReferenceHolder.instance.uIDocument;
        player = ReferenceHolder.instance.playervariable;
        previewSC = ReferenceHolder.instance.previewSC;
        placementSC = ReferenceHolder.instance.placementSC;
        buildPriceLibrary = ReferenceHolder.instance.buildPriceDictionary;

        panel = uI.rootVisualElement.Q<VisualElement>("BuildMenu");



        extractorPrice = panel.Q<Label>("ExtractorPriceTxt");
        conveyorPrice = panel.Q<Label>("ConveyorPriceTxt");
        marketplacePrice = panel.Q<Label>("MarketplacePriceTxt");
        foundryPrice = panel.Q<Label>("FoundryPriceTxt");
        builderPrice = panel.Q<Label>("BuilderPriceTxt");

        extractorBuild = panel.Q<Button>("ExtractorBuildBtn");
        conveyorBuild = panel.Q<Button>("ConveyorBuildBtn");
        marketplaceBuild = panel.Q<Button>("MarketplaceBuildBtn");
        foundryBuild = panel.Q<Button>("FoundryBuildBtn");
        builderBuild = panel.Q<Button>("BuilderBuildBtn");


        extractorBuild.clicked -= ExtractorSelect;
        conveyorBuild.clicked -= ConveyorSelect;
        marketplaceBuild.clicked -= MarketplaceSelect;
        foundryBuild.clicked -= FoundrySelect;
        builderBuild.clicked -= BuilderSelect;

        extractorBuild.clicked += ExtractorSelect;
        conveyorBuild.clicked += ConveyorSelect;
        marketplaceBuild.clicked += MarketplaceSelect;
        foundryBuild.clicked += FoundrySelect;
        builderBuild.clicked += BuilderSelect;

        extractorPrice.text = buildPriceLibrary.GetPrice(ReferenceHolder.instance.extractorPrefab) + " $";
        conveyorPrice.text = buildPriceLibrary.GetPrice(ReferenceHolder.instance.conveyorPrefab) + " $";
        marketplacePrice.text = "Free";
        foundryPrice.text = buildPriceLibrary.GetPrice(ReferenceHolder.instance.foundryPrefab) + " $";
        builderPrice.text = buildPriceLibrary.GetPrice(ReferenceHolder.instance.builderPrefab) + " $";

    }


    //---------Méthodes de sélection de build----------

    private void ExtractorSelect()
    {
        BuildMenuOff();
        previewSC.previewToUse = ReferenceHolder.instance.extractorPreview;
        placementSC.currentBuild = ReferenceHolder.instance.extractorPrefab;
        placementSC.currentBuildingType = Placement.buildingType.Extractor;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void ConveyorSelect()
    {
        BuildMenuOff();
        previewSC.previewToUse = ReferenceHolder.instance.conveyorPreview;
        placementSC.currentBuild = ReferenceHolder.instance.conveyorPrefab; 
        placementSC.currentBuildingType = Placement.buildingType.Conveyor;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void MarketplaceSelect()
    {
        BuildMenuOff();
        previewSC.previewToUse = ReferenceHolder.instance.marketplacePreview;
        placementSC.currentBuild = ReferenceHolder.instance.marketplacePrefab;
        placementSC.currentBuildingType = Placement.buildingType.marketplace;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void FoundrySelect()
    {
        BuildMenuOff();
        previewSC.previewToUse = ReferenceHolder.instance.foundryPreview;
        placementSC.currentBuild = ReferenceHolder.instance.foundryPrefab;
        placementSC.currentBuildingType = Placement.buildingType.Foundry;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }

    private void BuilderSelect()
    {
        BuildMenuOff();
        previewSC.previewToUse = ReferenceHolder.instance.builderPrview;
        placementSC.currentBuild = ReferenceHolder.instance.builderPrefab;
        placementSC.currentBuildingType = Placement.buildingType.builder;

        previewSC.DestroyInstance();
        previewSC.CreateInstance();

        player.buildMode = true;
        player.rotation = 0;
    }


    //----------Méthodes de gestion du menu----------

    public void BuildMenuToggle()
    {
        if (panel.style.display == DisplayStyle.None) BuildMenuOn();
        else BuildMenuOff();
    }

    public void BuildMenuOn()
    {
        panel.style.display = DisplayStyle.Flex;
        player.isInUI = true;
        player.buildMenu = true;

    }

    public void BuildMenuOff()
    {
        panel.style.display = DisplayStyle.None;
        player.isInUI = false;
        player.buildMenu = false;
        previewSC.DestroyInstance();

    }

}
