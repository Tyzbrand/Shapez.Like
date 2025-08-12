using UnityEngine;

public class TrashButton : MonoBehaviour
{

    private ItemManager itemManager;


    private void Start()
    {
        itemManager = ReferenceHolder.instance.itemManager;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void DestroyItems()
    {
        itemManager.ClearItems();
    }
}
