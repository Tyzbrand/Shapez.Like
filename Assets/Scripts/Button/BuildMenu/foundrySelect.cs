using UnityEngine;

public class foundrySelect : MonoBehaviour
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

    private void FoundryBuild()
    {
        buildSC.BuildGuiClose();//Fermer le GUI

        previewSC.previewToUse = ReferenceHolder.instance.foundryPreview;
        placementSC.currentBuild = ReferenceHolder.instance.foundryPrefab; //Defeinir les batiements

        previewSC.DestroyInstance();
        previewSC.CreateInstance(); //Afficher la preview

        buildSC.BuildModeOn();   //


    }
}

