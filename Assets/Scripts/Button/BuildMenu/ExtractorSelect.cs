using UnityEngine;

public class ExtractorSelect : MonoBehaviour
{

    public Preview previewSC;
    public Placement placementSC;
    private PlayerVariables player;
    private PlayerSwitchMode buildSC;



    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        placementSC = ReferenceHolder.instance.placementSC;
        previewSC = ReferenceHolder.instance.previewSC;
        buildSC = ReferenceHolder.instance.playerSwitchMode;
        
    }

    private void ExtractorBuild()
    {
        buildSC.BuildGuiClose();//Fermer le GUI

        previewSC.previewToUse = ReferenceHolder.instance.extractorPreview;
        placementSC.currentBuild = ReferenceHolder.instance.extractorPrefab; //Defeinir les batiements

        previewSC.DestroyInstance();
        previewSC.CreateInstance(); //Afficher la preview


        buildSC.BuildModeOn();   //


    }
}
