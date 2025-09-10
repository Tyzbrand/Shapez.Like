using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private List<VisualElement> panels = new();
    private Dictionary<VisualElement, MonoBehaviour> uIScript;
    public VisualElement activePanel;
    public MiscInput miscInput;



    //UI ouvrables
    private HubUI hubUI;
    private ExtractorUI extractorUI;
    private FoundryUI foundryUI;
    private BuilderUI builderUI;
    private CoalGeneratorUI coalGeneratorUI;
    private AdvancedExtractorUI advancedExtractorUI;
    private BuildMenuSC buildMenuSC;


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
    }

    public void RegisterPanel(VisualElement panel)
    {
        if (!panels.Contains(panel)) panels.Add(panel);
    }

    public void HidePanel(VisualElement panelToHide, Action OnHide = null)
    {
        panelToHide.style.display = DisplayStyle.None;

        OnHide?.Invoke();
    }

    public void ShowPanel(VisualElement panelToShow, Action OnShow = null)
    {
        foreach (var panel in panels)
        {
            if (panel == panelToShow) panel.style.display = DisplayStyle.Flex;
            else panel.style.display = DisplayStyle.None;
        }

        OnShow?.Invoke();
    }

    public void TogglePanel(VisualElement panelToToggle, Action OnShow = null, Action OnHide = null)
    {
        if (panelToToggle.resolvedStyle.display == DisplayStyle.None)
        {
            ShowPanel(panelToToggle, OnShow);
        }
        else
        {
            HidePanel(panelToToggle, OnHide);
            miscInput.lastBuilding = null;
        }
    }
    public VisualElement GetOpenPanel()
    {
        foreach (var panel in panels)
        {
            if (panel.resolvedStyle.display == DisplayStyle.Flex) return panel;

        }
        return null;
    }

    public void hideOpenPanel()
    {
        var currentPanel = GetOpenPanel();

        if (currentPanel == hubUI.panel) HidePanel(currentPanel, () => hubUI.HubUIOnHide());
        else if (currentPanel == extractorUI.panel) HidePanel(currentPanel, () => extractorUI.ExtractorUIOnHide());
        else if (currentPanel == foundryUI.panel) HidePanel(currentPanel, () => foundryUI.FoundryUIOnHide());
        else if (currentPanel == buildMenuSC.panel) HidePanel(currentPanel, () => buildMenuSC.BuildMenuOnHide());
        else if (currentPanel == builderUI.panel) HidePanel(currentPanel, () => builderUI.BuilderUIOnHide());
        else if (currentPanel == coalGeneratorUI.panel) HidePanel(currentPanel, () => coalGeneratorUI.CoalGeneratorUIOnHide());
        else if (currentPanel == advancedExtractorUI.panel) HidePanel(currentPanel, () => advancedExtractorUI.AExtractorUIOnHide());

    }




}
