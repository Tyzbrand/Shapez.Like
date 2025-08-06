
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System;




//-------------------------------------Gère tout les input liés a la construction sur la map (Seulement si build mode = true)-------------------------------------
//utilisation de "PlayerVariables" pour: -buildMode et -rotation

public class Placement : MonoBehaviour
{


    private Camera playerCam;
    private PlayerVariables player;
    private BuildingManager buildingManager;
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
    private PrefabBuildingBHDictionnary prefabDico;




    private void Start()
    {

        Extractor = ReferenceHolder.instance.extractorPrefab;
        conveyor = ReferenceHolder.instance.conveyorPrefab;
        marketPlace = ReferenceHolder.instance.marketplacePrefab;
        foundry = ReferenceHolder.instance.foundryPrefab;
        playerCam = ReferenceHolder.instance.mainCamera;
        restrictedTiles = new List<TileBase>(ReferenceHolder.instance.restrictedTiles);
        player = ReferenceHolder.instance.playervariable;
        buildingManager = ReferenceHolder.instance.buildingManager;


        prices = ReferenceHolder.instance.buildPriceDictionary;
        prefabDico = ReferenceHolder.instance.prefabBuildingBHDictionnary;






    }


    private void Update()
    {
        if (allowConstruciton)
        {

            if (currentBuild != null)
            {
                if (player.buildMode)
                {
                    tilemap = FindFirstObjectByType<Tilemap>();
                    Vector3 mousePos3D = playerCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    mousePos3D.z = 0;
                    Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y);

                    int décalage = player.rotation;
                    Vector3Int cellPos = tilemap.WorldToCell(mousePos2D);
                    TileBase underTile = tilemap.GetTile(cellPos);

                    if (!restrictedTiles.Contains(underTile) && underTile != null && !EventSystem.current.IsPointerOverGameObject())
                    {
                        Type type = prefabDico.GetPrefabType(currentBuild);

                        if (type != null)
                        {
                            BuildingBH buildingInstance = (BuildingBH)Activator.CreateInstance(type, mousePos2D);
                            buildingManager.AddBuilding(mousePos2D, buildingInstance, tilemap);
                            player.Money -= prices.GetPrice(currentBuild);
                        }
                        
                        
                        
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

           
        
    


