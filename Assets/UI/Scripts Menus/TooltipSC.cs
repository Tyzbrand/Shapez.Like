using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TooltipSC : MonoBehaviour
{
    private UIDocument uI;
    private VisualElement panel;

    private Label tooltipText;

    private int xOffset = -50;
    private int yOffset = -40;

    private void Start()
    {

        uI = ReferenceHolder.instance.uIDocument;

        panel = uI.rootVisualElement.Q<VisualElement>("Tooltips");

        tooltipText = panel.Q<Label>("TooltipTxt");

    }

    public void TooltipShow(String text)
    {
        panel.style.display = DisplayStyle.Flex;
        tooltipText.text = text;
    }

    public void TooltipHide()
    {
        panel.style.display = DisplayStyle.None;
        tooltipText.text = "";
    }

    private void Update()
    {
        if (panel.resolvedStyle.display == DisplayStyle.Flex)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            mousePos.y = Screen.height - mousePos.y;

            mousePos = uI.rootVisualElement.WorldToLocal(mousePos);

            mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width - panel.resolvedStyle.width);
            mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height - panel.resolvedStyle.height);

            panel.style.left = mousePos.x + xOffset;
            panel.style.top = mousePos.y + yOffset;

        }
    }
}
