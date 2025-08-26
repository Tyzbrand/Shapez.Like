using UnityEngine;
using UnityEngine.UIElements;

public class ExtractorUI : MonoBehaviour
{
    private UIDocument uI;
    public VisualElement panel;
    private ProgressBar processBar;
    private UIManager uIManager;
    private PlayerVariables player;
    public Extractor activeExtractor = null;

    private Label resourcePerSecText, storageText;
    public Toggle extractorToggle;

    private void Start()
    {
        uIManager = ReferenceHolder.instance.uIManager;
        player = ReferenceHolder.instance.playervariable;

        uI = ReferenceHolder.instance.uIDocument;

        panel = uI.rootVisualElement.Q<VisualElement>("ExtractorUI");
        uIManager.RegisterPanel(panel);
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

    public void refreshUI(Extractor extractor)
    {
        activeExtractor = extractor;
        extractorToggle.SetValueWithoutNotify(activeExtractor.IsActive);
    }



    //----------Méthodes d'affichage----------

    public void ExtractorUIOnShow(Extractor extractor)
    {   
        activeExtractor = extractor;
        player.isInUI = true;
        extractorToggle.SetValueWithoutNotify(activeExtractor.IsActive);
    }

    public void ExtractorUIOnHide()
    {
        activeExtractor = null;
        player.isInUI = false;
    }
    

    

}
