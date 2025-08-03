using System.Collections;
using UnityEngine;

public class ConveyorSelect : MonoBehaviour
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

    private void ConveyorBuild()
    {
        buildSC.BuildGuiClose();//Fermer le GUI

        previewSC.previewToUse = ReferenceHolder.instance.conveyorPreview;
        placementSC.currentBuild = ReferenceHolder.instance.conveyorPrefab; //Defeinir les batiements


        previewSC.DestroyInstance();
        previewSC.CreateInstance(); //Afficher la preview

        buildSC.BuildModeOn();   //


    }
    
    
}
