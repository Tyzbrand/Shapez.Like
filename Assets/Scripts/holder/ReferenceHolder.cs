using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;
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
    public Statistics playerStats;
    public MiscInput miscInput;


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
    public GameObject itemPrefab;
    public GameObject buildingPrefab;
   


    //Sprites de ressources
    [Header("Sprites")]
    public SpriteAtlas resourcesSprite;

    //Previews batiments
    [Header("Preview")]
    public GameObject extractorPreview;
    public GameObject conveyorPreview;
    public GameObject marketplacePreview;
    public GameObject foundryPreview;
    public GameObject builderPrview;
    public GameObject coalGeneratorPreview;
    public GameObject advancedExtractorPreview;
    public GameObject junctionPreview;
    public GameObject splitterPreview;
    public GameObject mergerPreview;


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
    public TooltipSC tooltipSC;
    



    void Awake()
    {
        instance = this;
    }
}
