using UnityEngine;

//-------------------------------------Gère toute les variables dynamiques simples du joueur -------------------------------------
//Utilisation dans : -Placement et -PlayerMove

public class PlayerVariables : MonoBehaviour
{   

    //build Références

    public bool buildMode = false;
    public bool buildMenu = false;
    public bool destructionMode = false;

    public int rotation = 0;

    public GameObject currentBuild;
    public GameObject currentPreview;

    //Données numériques visibles
    public int Money = 1000000;
    public int day = 1;
    public int seconds;
    public int minutes;
    

    //References
    public Inventory inventory;


    // Est dans un menu
    public bool movementBlock = false;

    //accélération
    public float timeScale = 1f;





    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }



}
