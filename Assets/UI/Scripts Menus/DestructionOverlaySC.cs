using UnityEngine;
using UnityEngine.UIElements;

public class DestructionOverlaySC : MonoBehaviour
{
    [SerializeField] UIDocument uI;
    private VisualElement destructionOverlay;
    private VisualElement pauseOverlay;

    private void Start()
    {
        destructionOverlay = uI.rootVisualElement.Q<VisualElement>("DestructionOverlay");
        pauseOverlay = uI.rootVisualElement.Q<VisualElement>("PauseOverlay");
    }


    //Destruction Overlay
    public void DestructionOverlayToggle()
    {
        if (destructionOverlay.resolvedStyle.display == DisplayStyle.None) DestructionOverlayOn();
        else DestructionOverlayOff();
    }

    public void DestructionOverlayOn()
    {
        destructionOverlay.style.display = DisplayStyle.Flex;
    }

    public void DestructionOverlayOff()
    {
        destructionOverlay.style.display = DisplayStyle.None;
    }



    //Pause Overlay
    public void PauseOverlayToggle()
    {
        if (pauseOverlay.resolvedStyle.display == DisplayStyle.None) PauseOverlayOn();
        else PauseOverlayOff();
    }

    public void PauseOverlayOn()
    {
        pauseOverlay.style.display = DisplayStyle.Flex;
    }

    public void PauseOverlayOff()
    {
        pauseOverlay.style.display = DisplayStyle.None;

    }
    

}   


