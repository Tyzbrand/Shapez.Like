
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMapTeleport : MonoBehaviour
{
    private BuildingManager buildingManager;
    private Placement placementSC;

    private void Start()
    {   
        
        buildingManager = ReferenceHolder.instance.buildingManager;
        placementSC = ReferenceHolder.instance.placementSC;

        placementSC.currentBuild = ReferenceHolder.instance.hubPrefab;

        SceneManager.LoadScene(1);
        Application.targetFrameRate = 165;
        transform.position = new Vector2(45, 106);



        
        

        

    }
}
