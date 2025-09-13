using UnityEngine;
using UnityEngine.UIElements;

public class ExtractorUI : AbstractBuildingUI
{

    private ProgressBar processBar;
    public Extractor activeExtractor = null;

    private Label resourcePerSecText, storageText;
    public Toggle extractorToggle;
    

    protected override void Start()
    {
        base.Start();

        panel = uI.rootVisualElement.Q<VisualElement>("ExtractorUI");
        uIManager.RegisterPanel(panel, this);
        buildingLibrary.RegisterUIPanel(panel, UIManager.uIType.Extractor);
        processBar = panel.Q<ProgressBar>("EXProgressBar");


        resourcePerSecText = panel.Q<Label>("EXResourcePerSecTxt");
        storageText = panel.Q<Label>("EXStorageTxt");

        extractorToggle = panel.Q<Toggle>("EXToggle");
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
        if (building is Extractor extractor)
        {
            activeExtractor = extractor;
            extractorToggle.SetValueWithoutNotify(activeExtractor.IsActive);
        }

    }



    //----------Méthodes d'affichage----------

    public override void UIOnShow(BuildingBH building)
    {
        if (building is Extractor extractor)
        {
            activeExtractor = extractor;
            extractorToggle.SetValueWithoutNotify(activeExtractor.IsActive);
            player.isInBuildingUI = true;
        }

    }

    public override void UIOnHide()
    {
        activeExtractor = null;
        player.isInBuildingUI = false;
        Debug.Log("Extractor");
        Debug.Log("JE SUIS LE CRACK");
    }

}
