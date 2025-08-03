using UnityEngine;

public class MarketplaceSelect : MonoBehaviour
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

    private void marketPlaceBuild()
    {
        buildSC.BuildGuiClose();//Fermer le GUI

        previewSC.previewToUse = ReferenceHolder.instance.marketplacePreview;
        placementSC.currentBuild = ReferenceHolder.instance.marketplacePrefab; //Defeinir les batiements

        previewSC.DestroyInstance();
        previewSC.CreateInstance(); //Afficher la preview

        buildSC.BuildModeOn();   //


    }
}

