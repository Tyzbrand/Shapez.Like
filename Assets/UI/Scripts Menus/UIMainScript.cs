using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private List<VisualElement> panels = new();
    public VisualElement activePanel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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


}
