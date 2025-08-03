
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;




//-------------------------------------Gère tout les input liés a la construction sur la map (Seulement si build mode = true)-------------------------------------
//utilisation de "PlayerVariables" pour: -buildMode et -rotation

public class Placement : MonoBehaviour
{


    private Camera playerCam;
    private PlayerVariables player;
    private List<TileBase> restrictedTiles; //Liste des tuiles sur lesquelles je ne peux pas construire (NE FONCTIONNE PAS)

    [HideInInspector]
    public GameObject currentBuild;
    private GameObject Extractor;
    private GameObject conveyor;
    private GameObject marketPlace;
    private GameObject foundry;
    private Tilemap tilemap; 
    private bool allowConstruciton = false;


    //Dictionnaire
    private BuildPriceDictionnary prices;




    private void Start()
    {

        Extractor = ReferenceHolder.instance.extractorPrefab;
        conveyor = ReferenceHolder.instance.conveyorPrefab;
        marketPlace = ReferenceHolder.instance.marketplacePrefab;
        foundry = ReferenceHolder.instance.foundryPrefab;
        playerCam = ReferenceHolder.instance.mainCamera;
        restrictedTiles = new List<TileBase>(ReferenceHolder.instance.restrictedTiles);
        player = ReferenceHolder.instance.playervariable;

        prices = ReferenceHolder.instance.buildPriceDictionary;





    }


    private void Update()
    {
        if (allowConstruciton)
        {
            tilemap = FindFirstObjectByType<Tilemap>();


            if (currentBuild != null)
            {
                if (GetComponent<PlayerVariables>().buildMode == true)
                {

                    Vector3 mouseWorlPos = playerCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    mouseWorlPos.z = 0;

                    Vector3Int cellPos = tilemap.WorldToCell(mouseWorlPos);
                    Vector3 placementpos = tilemap.GetCellCenterWorld(cellPos);

                    int decalage = GetComponent<PlayerVariables>().rotation;
                    TileBase underTile = tilemap.GetTile(cellPos);


                    Collider2D colliderfound = Physics2D.OverlapCircle(placementpos, 0.2f, LayerMask.GetMask("Building", "Conveyor", "Interactable"));



                    if (!restrictedTiles.Contains(underTile) && underTile != null && colliderfound == null && !EventSystem.current.IsPointerOverGameObject())
                    {
                        Instantiate(currentBuild, placementpos, Quaternion.Euler(0, 0, decalage));
                        player.Money -= prices.GetPrice(currentBuild);
                    }
                }
            }
            allowConstruciton = false;
        }
    }







    private void Rotate(InputAction.CallbackContext context) //Permet la rotation des objets du preview et de la map
    {
        if (context.performed && GetComponent<PlayerVariables>().buildMode == true)
        {
            GetComponent<PlayerVariables>().rotation += 90;
        }
    }



    private void Build(InputAction.CallbackContext context) //Permet de poser l'objet en question au clic droit
    {

        if (context.performed)
        {
            if (prices.GetPrice(currentBuild) <= player.Money)
            {
                allowConstruciton = true;

            }
            else if (player.buildMode)
            {
                Debug.Log("Pas Assez d'argent");
            }
           
        }
                
    }
}

           
        
    


