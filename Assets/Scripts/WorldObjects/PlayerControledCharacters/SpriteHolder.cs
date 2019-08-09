using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolder : MonoBehaviour {

    public static SpriteHolder instance { get; private set; }

    [SerializeField]
    Sprite[] CharacterArt;

    [SerializeField]
    Sprite[] BuildingArt;

    [SerializeField]
    Sprite[] FoodArt;

    [SerializeField]
    Sprite TextBubble;

    private void Awake()
    {
        instance = this;
    }

    public Sprite GetCharacterArtFromIDNumber(int index)
    {
        return CharacterArt[index];
    }

    public Sprite GetBuildingArtFromIDNumber(int index)
    {
        return BuildingArt[index];
    }

    public Sprite GetFoodArtFromIDNumber(int index)
    {
        return FoodArt[index];
    }

    public Sprite GetTextBubble()
    {
        return TextBubble;
    }
}
