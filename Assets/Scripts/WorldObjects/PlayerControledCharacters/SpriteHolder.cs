using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolder : MonoBehaviour {

    public static SpriteHolder instance { get; private set; }

   

    [System.Serializable]
    public struct NamedImages
    {
        public string name;
        public Sprite characterIcon;
        public List<Sprite> sprites;
    }
    public NamedImages[] CharacterArtToLoad;

    Dictionary<int, List<Sprite>> characterArt = new Dictionary<int,List<Sprite>>();

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
        initCharacterArtDictionary();
    }

    private void initCharacterArtDictionary()
    {
        for (int i = 0; i < CharacterArtToLoad.Length; i++)
        {
            characterArt.Add(i, CharacterArtToLoad[i].sprites);
        }
    }

    public Sprite GetCharacterIcon(int index)
    {
        return CharacterArtToLoad[index].characterIcon;
    }

    public List<Sprite> GetCharacterPawnArtFromIDNumber(int index)
    {
       List<Sprite> test = new List<Sprite>();
        if (characterArt.TryGetValue(index, out test))
        {
            return test;
        }

        return test; 
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
