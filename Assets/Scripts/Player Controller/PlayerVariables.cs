using UnityEngine;
using UnityEngine.Tilemaps;

//-------------------------------------Gère toute les variables dynamiques simples du joueur -------------------------------------

public class PlayerVariables : MonoBehaviour
{

    //build Références

    public bool buildMode = false;
    public bool buildMenu = false;
    public bool destructionMode = false;
    public bool pickupMode = false;
    public bool lineBuild = false;

    public int rotation = 0;

    public GameObject currentBuild;
    public GameObject currentPreview;

    //Données numériques visibles
    public int Money = 1000000;
    public int day = 1;
    public int seconds;
    public int minutes;

    //Electricité
    public float electricityStorage = 1000f;
    public float production = 0f;
    public float consomation = 0f;
    public float electricityBalance = 0f;
    public float realElectricityBalance = 0f;
    public float electricityMaxStorage = 1000f;


    //References
    public Tilemap tilemap1;


    // Est dans un menu
    public bool isInUI = false;
    public bool isInPauseUI = false;
    public bool isInBuildingUI = false;

    //accélération
    public float timeScale = 1f;


    public void BuildModeOff()
    {
        buildMode = false;
    }





}
