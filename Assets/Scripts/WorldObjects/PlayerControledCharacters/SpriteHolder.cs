using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SpriteHolder : MonoBehaviour {

    public static SpriteHolder instance { get; private set; }

    [System.Serializable]
    public struct NamedImages
    {
        public string name;
        public Sprite characterIcon;
        public List<Sprite> sprites;
        public RuntimeAnimatorController animations;
    }

    public NamedImages[] CharacterArtToLoad;


    [SerializeField]
    Sprite[] BuildingArt;

    [SerializeField]
    Sprite[] FoodArt;

    [SerializeField]
    Sprite[] NumberArt;

    [SerializeField]
    Sprite TextBubble;

    [SerializeField]
    Sprite SupplyBox;

    private void Awake()
    {
        instance = this;
    }

    public Sprite GetCharacterIcon(int index)
    {
        return CharacterArtToLoad[index].characterIcon;
    }

    public List<Sprite> GetCharacterPawnArtFromIDNumber(int index)
    {
       List<Sprite> test = new List<Sprite>();
        if (CharacterArtToLoad.Length > index)
        {
            return CharacterArtToLoad[index].sprites;
        }

        return test; 
    }

    public RuntimeAnimatorController GetWalkAnimationFromIDNumber(int index)
    {

        if (CharacterArtToLoad.Length > index)
        {
            return CharacterArtToLoad[index].animations;
        }
        return null;
    }

    public Sprite GetBuildingArtFromIDNumber(int index)
    {
        return BuildingArt[index];
    }

    public Sprite GetFoodArtFromIDNumber(int index)
    {
        return FoodArt[index];
    }

    public Sprite GetNumberArtFromIDNumber(int index)
    {
        return NumberArt[index];
    }

    public Sprite GetTextBubble()
    {
        return TextBubble;
    }

    public Sprite GetSupplyBox()
    {
        return SupplyBox;
    }
}
