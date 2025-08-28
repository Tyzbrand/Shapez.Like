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

    void Awake()
    {
        instance = this;
    }

}
