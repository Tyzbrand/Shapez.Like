using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ReferenceHolder : MonoBehaviour
{
    //Référence principale
    public static ReferenceHolder instance;

    //Principaux éléments 
    [Header("Principaux éléments")]
    public Camera mainCamera;
    public UIDocument uIDocument;
    
    
    //Scripts
    [Header("Scripts")]
    public PlayerVariables playervariable;
    public Preview previewSC;
    public Placement placementSC;
    public Destruction destructionSC;
    public Inventory inventorySC;


    //Game Managers
    [Header("Game Managers")]
    public BuildingManager buildingManager;
    public ItemManager itemManager;
    public TimeManager timeManager;
    public UIManager uIManager;
    public ElectricityManager electricityManager;



    //Tuiles
    [Header("Tuiles")]
    public List<TileBase> restrictedTiles;



    //Prefabs
    [Header("Prefabs")]
    public GameObject extractorPrefab;
    public GameObject conveyorPrefab;
    public GameObject marketplacePrefab;
    public GameObject foundryPrefab;
    public GameObject itemPrefab;
    public GameObject hubPrefab;
    public GameObject builderPrefab;
    public GameObject coalGeneratorPrefab;
    public GameObject advancedExtractorPrefab;


    //Sprites de ressources
    [Header("Sprites")]
    public Sprite ironSprite;
    public Sprite copperSprite;
    public Sprite coalSprite;
    public Sprite ironIngotSprite;
    public Sprite copperIngotSprite;
    public Sprite copperPlateSprite;
    public Sprite ironPlateSPrite;
    public Sprite silverSprite;
    public Sprite silverIngotSprite;


    //Previews batiments
    [Header("Preview")]
    public GameObject extractorPreview;
    public GameObject conveyorPreview;
    public GameObject marketplacePreview;
    public GameObject foundryPreview;
    public GameObject builderPrview;
    public GameObject coalGeneratorPreview;
    public GameObject advancedExtractorPreview;


    //Datas
    [Header("Données")]
    public BuildingData buildingData;
    public RessourceData ressourceData;


    //Recettes
    [Header("Recettes")]
    public FoundryRecipe foundryRecipe;
    public BuilderRecipe builderRecipe;


    //Dictionnaires
    [Header("Dictionnaires")]
    public RessourceDictionnary ressourceDictionnary;
    public BuildingLibrary buildingLibrary;

    //Panel UI
    [Header("Panel UI")]
    public PauseMenuSC pauseMenu;
    public BuildMenuSC buildMenu;
    public HubUI hubUI;
    public ExtractorUI extractorUI;
    public AdvancedExtractorUI advancedExtractorUI;
    public FoundryUI foundryUI;
    public OverlaySC inGameOverlay;
    public DestructionOverlaySC overlayManager;
    public BuilderUI builderUI;
    public CoalGeneratorUI coalGeneratorUI;
    



    void Awake()
    {
        instance = this;
    }
}
