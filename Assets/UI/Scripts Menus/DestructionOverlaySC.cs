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
        if (destructionOverlay.style.display == DisplayStyle.None) destructionOverlay.style.display = DisplayStyle.Flex;
        else destructionOverlay.style.display = DisplayStyle.None;
    }



    //Pause Overlay
    public void PauseOverlayToggle()
    {
        if (pauseOverlay.style.display == DisplayStyle.None) PauseOverlayOn();
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


