using UnityEngine;

public class TextureHolder : MonoBehaviour
{
    public static TextureHolder instance;

    [Header("Batiments")]
    public Sprite foundryIdle;
    public Sprite foundryAction;

    public Sprite extractorIdle;
    public Sprite extractorCoal;
    public Sprite extractorStone;
    public Sprite extractorCopper;
    public Sprite extractorIron;

    public Sprite Conveyor;

    public Sprite merger;
    public Sprite splitter;
    public Sprite junction;

    public Sprite advancedExtractor;

    public Sprite builder;

    public Sprite coalGenerator;

    public Sprite marketplace;

    public Sprite hub;

    [Header("Ressources")]
    public Sprite Stone;
    public Sprite Coal;
    public Sprite iron;
    public Sprite copper;
    public Sprite ironIngot;
    public Sprite copperIngot;
    public Sprite copperPlate;
    public Sprite ironPlate;
    public Sprite gear;
    public Sprite wireCoil;

    void Awake()
    {
        instance = this;
    }

}
