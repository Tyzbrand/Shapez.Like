using UnityEngine;
using UnityEngine.UIElements;

public class AdvancedExtractorUI : AbstractBuildingUI
{

    private ProgressBar processBar;
    public AdvancedExtractor activeExtractor = null;

    private Label resourcePerSecText, storageText, consumationText, warningText;
    public Toggle extractorToggle;

    protected override void Start()
    {
        base.Start();

        panel = uI.rootVisualElement.Q<VisualElement>("AExtractorUI");
        uIManager.RegisterPanel(panel, this);
        buildingLibrary.RegisterUIPanel(panel, UIManager.uIType.AdvancedExtractor);
        processBar = panel.Q<ProgressBar>("AEXProgressBar");


        resourcePerSecText = panel.Q<Label>("AEXResourcePerSecTxt");
        storageText = panel.Q<Label>("AEXStorageTxt");
        consumationText = panel.Q<Label>("AEXConsumationTxt");
        warningText = panel.Q<Label>("AEXElecWarningTxt");

        extractorToggle = panel.Q<Toggle>("AEXToggle");
       
        extractorToggle.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue)
            {
                ActiveBuilding();
            }
            else DisableBuilding();
        });


    }

    private void Update()
    {
        if (activeExtractor != null) UpdateUI();
    }

    private void UpdateUI()
    {
        resourcePerSecText.text = activeExtractor.ressourcesPerSecond + "/s";
        storageText.text = activeExtractor.currentStorage + "/" + activeExtractor.capacity;
        consumationText.text = activeExtractor.electricityConsomation + " kW";

        if (!activeExtractor.enoughtElectricity) warningText.style.display = DisplayStyle.Flex;
        else warningText.style.display = DisplayStyle.None;

        processBar.value = activeExtractor.GetProgress();
    }


    private void ActiveBuilding()
    {
        activeExtractor.IsActive = true;
        activeExtractor.BuildingOnEnable();
    }

    private void DisableBuilding()
    {
        activeExtractor.IsActive = false;
        activeExtractor.BuildingOnDisable();
    }

    public override void RefreshUI(BuildingBH building)
    {
        if (building is AdvancedExtractor extractor)
        {
            activeExtractor = extractor;
            extractorToggle.SetValueWithoutNotify(activeExtractor.IsActive);
        }

    }



    //----------Méthodes d'affichage----------

    public override void UIOnShow(BuildingBH building)
    {
        if (building is AdvancedExtractor extractor)
        {
            activeExtractor = extractor;
            extractorToggle.SetValueWithoutNotify(activeExtractor.IsActive);
            player.isInBuildingUI = true;
            activeExtractor.BuildingOnSelect();
        }

    }

    public override void UIOnHide()
    {   
        activeExtractor.BuildingOnDeselect();
        activeExtractor = null;
        player.isInBuildingUI = false;
        Debug.Log("ADV extyractor");
    }
}
