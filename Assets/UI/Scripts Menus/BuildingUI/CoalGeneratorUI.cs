using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CoalGeneratorUI : MonoBehaviour
{
    private UIDocument uI;
    public VisualElement panel;

    private UIManager uIManager;
    private PlayerVariables player;

    private Label productionText, storageText, fuelText;
    private Toggle coalGToggle;

    public CoalGenerator activeCoalGenerator;

    private void Start()
    {
        uI = ReferenceHolder.instance.uIDocument;
        player = ReferenceHolder.instance.playervariable;
        uIManager = ReferenceHolder.instance.uIManager;

        panel = uI.rootVisualElement.Q<VisualElement>("CoalGeneratorUI");
        uIManager.RegisterPanel(panel);

        productionText = panel.Q<Label>("CGProductionTxt");
        storageText = panel.Q<Label>("CGStorageTxt");
        fuelText = panel.Q<Label>("CGFuelTxt");

        coalGToggle = panel.Q<Toggle>("CGToggle");

        coalGToggle.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue)
            {
                ActiveBuilding();
            }
            else DisableBuilding();
        });

    

    }

    private void DisableBuilding()
    {
        activeCoalGenerator.IsActive = false;
        activeCoalGenerator.BuildingOnDisable();
    }

    private void ActiveBuilding()
    {
        activeCoalGenerator.IsActive = true;
        activeCoalGenerator.BuildingOnEnable();
    }

    private void Update()
    {
        if (activeCoalGenerator != null) UpdateUI();

    }

    private void UpdateUI()
    {
        productionText.text = activeCoalGenerator.electricityProduction + " kW";
        storageText.text = activeCoalGenerator.currentStorage + "/" + activeCoalGenerator.capacity;
        fuelText.text = (activeCoalGenerator.burnTimer / activeCoalGenerator.burnMaxTime * 100f).ToString("0.0") + "%";

    }

    public void refreshUI(CoalGenerator coalGenerator)
    {
        activeCoalGenerator = coalGenerator;
        coalGToggle.SetValueWithoutNotify(activeCoalGenerator.IsActive);
    }

    public void CoalGeneratorUIOnShow(CoalGenerator coalGenerator)
    {
        activeCoalGenerator = coalGenerator;
        coalGToggle.SetValueWithoutNotify(activeCoalGenerator.IsActive);
    }

    public void CoalGeneratorUIOnHide()
    {
        activeCoalGenerator = null;
    }
}
