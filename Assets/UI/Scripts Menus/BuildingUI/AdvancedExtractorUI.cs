using UnityEngine;
using UnityEngine.UIElements;

public class AdvancedExtractorUI : MonoBehaviour
{
    private UIDocument uI;
    public VisualElement panel;
    private ProgressBar processBar;
    private UIManager uIManager;
    private PlayerVariables player;
    public AdvancedExtractor activeExtractor = null;

    private Label resourcePerSecText, storageText, consumationText, warningText;
    public Toggle extractorToggle;

    private void Start()
    {
        uIManager = ReferenceHolder.instance.uIManager;
        player = ReferenceHolder.instance.playervariable;

        uI = ReferenceHolder.instance.uIDocument;

        panel = uI.rootVisualElement.Q<VisualElement>("AExtractorUI");
        uIManager.RegisterPanel(panel);
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

    public void refreshUI(AdvancedExtractor extractor)
    {
        activeExtractor = extractor;
        extractorToggle.SetValueWithoutNotify(activeExtractor.IsActive);
    }



    //----------Méthodes d'affichage----------

    public void AExtractorUIOnShow(AdvancedExtractor extractor)
    {
        activeExtractor = extractor;
        player.isInUI = true;
        extractorToggle.SetValueWithoutNotify(activeExtractor.IsActive);
    }

    public void AExtractorUIOnHide()
    {
        activeExtractor = null;
        player.isInUI = false;
    }
}
