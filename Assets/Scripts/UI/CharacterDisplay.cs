using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour {

    [SerializeField]
    Image characterPortrait;

    public CharacterDisplay()
    {

    }

    public void ChangeCharacterArt(Sprite image) //Sprite image
    {
        characterPortrait.sprite = image;
    }
}
