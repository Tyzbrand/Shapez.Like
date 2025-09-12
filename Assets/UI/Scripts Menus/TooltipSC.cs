using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TooltipSC : MonoBehaviour
{
    private UIDocument uI;

    private Label tooltip;

    [SerializeField] private int xOffset = -35;
    [SerializeField] private int yOffset = -50;

    private void Start()
    {

        uI = ReferenceHolder.instance.uIDocument;

        tooltip = uI.rootVisualElement.Q<Label>("TooltipTxt");

    }

    public void TooltipShow(String text)
    {
        tooltip.style.display = DisplayStyle.Flex;
        tooltip.text = text;
    }

    public void TooltipHide()
    {
        tooltip.style.display = DisplayStyle.None;
        tooltip.text = "";
    }

    private void Update()
    {
        if (tooltip.resolvedStyle.display == DisplayStyle.Flex)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            mousePos.y = Screen.height - mousePos.y;

            mousePos = uI.rootVisualElement.WorldToLocal(mousePos);

            mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width - tooltip.resolvedStyle.width);
            mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height - tooltip.resolvedStyle.height);

            tooltip.style.left = mousePos.x + xOffset;
            tooltip.style.top = mousePos.y + yOffset;

        }
    }
}
