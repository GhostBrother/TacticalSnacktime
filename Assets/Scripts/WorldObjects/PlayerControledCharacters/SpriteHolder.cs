using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolder : MonoBehaviour {

    public static SpriteHolder instance { get; private set; }

    [SerializeField]
    Sprite[] CharacterArt;

    private void Awake()
    {
        instance = this;
    }

    public Sprite GetArtFromIDNumber(int index)
    {
        return CharacterArt[index];
    }
}
