using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private Dictionary<VisualElement, AbstractBuildingUI> panels = new();
    public MiscInput miscInput;



    //UI ouvrables
    private HubUI hubUI;
    private ExtractorUI extractorUI;
    private FoundryUI foundryUI;
    private BuilderUI builderUI;
    private CoalGeneratorUI coalGeneratorUI;
    private AdvancedExtractorUI advancedExtractorUI;
    private BuildMenuSC buildMenuSC;
    private BuildingLibrary buildingLibrary;


    public enum uIType
    {
        Extractor,
        AdvancedExtractor,
        Builder,
        CoalGenerator,
        Foundry,
        Hub,
        BuildMenu
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        miscInput = ReferenceHolder.instance.miscInput;
        hubUI = ReferenceHolder.instance.hubUI;
        extractorUI = ReferenceHolder.instance.extractorUI;
        builderUI = ReferenceHolder.instance.builderUI;
        advancedExtractorUI = ReferenceHolder.instance.advancedExtractorUI;
        foundryUI = ReferenceHolder.instance.foundryUI;
        coalGeneratorUI = ReferenceHolder.instance.coalGeneratorUI;
        buildMenuSC = ReferenceHolder.instance.buildMenu;
        buildingLibrary = ReferenceHolder.instance.buildingLibrary;
    }

    public void RegisterPanel(VisualElement panel, AbstractBuildingUI uIScript)
    {
        if (!panels.ContainsKey(panel)) panels.Add(panel, uIScript);
    }


    public void HidePanel(BuildingManager.buildingType type, Action OnHide = null)
    {
        var uI = buildingLibrary.GetBuildingUI(type);
        uI.style.display = DisplayStyle.None;

        OnHide?.Invoke();
    }
    

    public void HidePanel(VisualElement panel, Action OnHide = null)
    {
        panel.style.display = DisplayStyle.None;

        OnHide?.Invoke();
    }

    public void ShowPanel(BuildingManager.buildingType type, Action OnShow = null)
    {
        var uI = buildingLibrary.GetBuildingUI(type);
        foreach (var panel in panels.Keys)
        {
            if (panel == uI) panel.style.display = DisplayStyle.Flex;
            else panel.style.display = DisplayStyle.None;
        }
        if (OnShow == null) Debug.Log("Nada");

        OnShow?.Invoke();
    }

    public void ShowPanel(UIManager.uIType type, Action OnShow = null)
    {    
        var uI = buildingLibrary.GetBuildingUI(type);
        foreach (var panel in panels.Keys)
        {
            if (panel == uI) panel.style.display = DisplayStyle.Flex;
            else panel.style.display = DisplayStyle.None;
        }
        
        OnShow?.Invoke();
    }


    public void TogglePanel(BuildingManager.buildingType type, Action OnShow = null, Action OnHide = null)
    {
        var uI = buildingLibrary.GetBuildingUI(type);
        if (uI.resolvedStyle.display == DisplayStyle.None)
        {
            ShowPanel(type, OnShow);
        }
        else
        {
            HidePanel(uI, OnHide);
            miscInput.lastBuilding = null;
        }
    }

    public void TogglePanel(UIManager.uIType type, Action OnShow = null, Action OnHide = null)
    {
        var uI = buildingLibrary.GetBuildingUI(type);
        if (uI.resolvedStyle.display == DisplayStyle.None)
        {
            ShowPanel(type, OnShow);
        }
        else
        {
            HidePanel(uI, OnHide);
            miscInput.lastBuilding = null;
        }
    }
    public VisualElement GetOpenPanel()
    {
        foreach (var panel in panels.Keys)
        {
            if (panel.resolvedStyle.display == DisplayStyle.Flex) return panel;

        }
        return null;
    }

    public void hideOpenPanel()
    {
        var currentPanel = GetOpenPanel();
        panels.TryGetValue(currentPanel, out var uIScript);
        {
            HidePanel(currentPanel, () => uIScript.UIOnHide());
        }
        


    }




}
