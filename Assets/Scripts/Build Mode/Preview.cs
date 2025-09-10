using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public class Preview : MonoBehaviour
{

    private Camera playerCam;
    private PlayerVariables player;
    [HideInInspector]
    public Tilemap tilemap;
    [HideInInspector]
    public GameObject previewToUse;
    private GameObject extractorPreview;
    private GameObject conveyorPreview;
    private GameObject depotPreview;
    private GameObject foundryPreview;
    
    [HideInInspector]
    public GameObject currentPreview; //instance d'objet
    private Vector3 placementpos;
    private int decalage;
    private List<TileBase> restrictedTiles;


    private void Start()
    {


        playerCam = ReferenceHolder.instance.mainCamera;
        extractorPreview = ReferenceHolder.instance.extractorPreview;
        conveyorPreview = ReferenceHolder.instance.conveyorPreview;
        depotPreview = ReferenceHolder.instance.depotPreview;
        foundryPreview = ReferenceHolder.instance.foundryPreview;
        player = ReferenceHolder.instance.playervariable;

        restrictedTiles = new List<TileBase>(ReferenceHolder.instance.restrictedTiles);



    

    }



    private void Update()
    {

        if (GetComponent<PlayerVariables>().buildMode == true && currentPreview != null)
        {
            decalage = GetComponent<PlayerVariables>().rotation;
            tilemap = FindFirstObjectByType<Tilemap>();

            Vector3 mouseWorlPos = playerCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorlPos.z = 0;

            Vector3Int cellPos = tilemap.WorldToCell(mouseWorlPos);
            placementpos = tilemap.GetCellCenterWorld(cellPos);

            TileBase underTile = tilemap.GetTile(cellPos);


            if (!restrictedTiles.Contains(underTile) && underTile != null)
            {
                currentPreview.transform.position = placementpos;
                currentPreview.transform.rotation = Quaternion.Euler(0, 0, decalage);


            }

           
        }

    }

    public void CreateInstance()
    {

        decalage = GetComponent<PlayerVariables>().rotation;
        tilemap = FindFirstObjectByType<Tilemap>();

        Vector3 mouseWorlPos = playerCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorlPos.z = 0;

        Vector3Int cellPos = tilemap.WorldToCell(mouseWorlPos);
        placementpos = tilemap.GetCellCenterWorld(cellPos);




        currentPreview = Instantiate(previewToUse, placementpos, Quaternion.Euler(0, 0, 0));
        currentPreview.GetComponent<SpriteRenderer>().sortingOrder = 1;
       
    }

    public void DestroyInstance()
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }
       
    }
}
