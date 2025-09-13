using UnityEngine;
using UnityEngine.UIElements;

public class AbstractBuildingUI : MonoBehaviour
{
    public UIDocument uI;
    public UIManager uIManager;
    public PlayerVariables player;
    public VisualElement panel;
    public BuildingLibrary buildingLibrary;





    protected virtual void Start()
    {
        uIManager = ReferenceHolder.instance.uIManager;
        player = ReferenceHolder.instance.playervariable;
        uI = ReferenceHolder.instance.uIDocument;
        buildingLibrary = ReferenceHolder.instance.buildingLibrary;
    }


    public virtual void UIOnShow(BuildingBH building)
    {

    }



    public virtual void UIOnHide()
    {

    }

    public virtual void RefreshUI(BuildingBH building)
    {
        
    }
}
