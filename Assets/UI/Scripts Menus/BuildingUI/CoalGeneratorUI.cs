using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CoalGeneratorUI : AbstractBuildingUI

{

    private Label productionText, storageText, fuelText;
    private Toggle coalGToggle;

    public CoalGenerator activeCoalGenerator;

    protected override void Start()
    {
        base.Start();

        panel = uI.rootVisualElement.Q<VisualElement>("CoalGeneratorUI");
        uIManager.RegisterPanel(panel, this);
        buildingLibrary.RegisterUIPanel(panel, UIManager.uIType.CoalGenerator);

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

    public override void RefreshUI(BuildingBH building)
    {
        if (building is CoalGenerator coalGenerator)
        {
            activeCoalGenerator = coalGenerator;
            coalGToggle.SetValueWithoutNotify(activeCoalGenerator.IsActive);
        }
    }

    public override void UIOnShow(BuildingBH building)
    {
        if (building is CoalGenerator coalGenerator)
        {
            activeCoalGenerator = coalGenerator;
            coalGToggle.SetValueWithoutNotify(activeCoalGenerator.IsActive);
            player.isInBuildingUI = true;
            activeCoalGenerator.BuildingOnSelect();
        }

    }

    public override void UIOnHide()
    {   
        activeCoalGenerator.BuildingOnDeselect();
        activeCoalGenerator = null;
        player.isInBuildingUI = false;
        Debug.Log("Coal gen");
    }
}
