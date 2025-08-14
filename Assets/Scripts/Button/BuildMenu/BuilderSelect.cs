using UnityEngine;

public class BuilderSelect : MonoBehaviour
{
    private Preview previewSC;
    private Placement placementSC;
    private PlayerVariables player;
    private PlayerSwitchMode buildSC;
    



    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        placementSC = ReferenceHolder.instance.placementSC;
        previewSC = ReferenceHolder.instance.previewSC;
        buildSC = ReferenceHolder.instance.playerSwitchMode;

    }

    private void BuilderBuild()
    {
        buildSC.BuildGuiClose();//Fermer le GUI

        previewSC.previewToUse = ReferenceHolder.instance.builderPrview;
        placementSC.currentBuild = ReferenceHolder.instance.builderPrefab; //Defeinir les batiements
        placementSC.currentBuildingType = Placement.buildingType.builder;


        

        previewSC.DestroyInstance();
        previewSC.CreateInstance(); //Afficher la preview


        buildSC.BuildModeOn();   


    }
}
