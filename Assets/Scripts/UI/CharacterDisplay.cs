using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour {

    [SerializeField]
    Image characterPortrait;

    [SerializeField]
    Image[] itemSprites;

    heldItems[] items;

    struct heldItems
    {
        public bool inUse { get; set; }
        public int quantityOfItem { get; set; }
        public Image heldItemImage { get; set; }
        public Image[] quantityOfItemNumber { get; set; }
    }

    public void InitCharacterDisplay()
    {
        items = new heldItems[itemSprites.Length];
        for (int i = 0; i < itemSprites.Length; i++)
        {
            items[i].heldItemImage = itemSprites[i];
            items[i].quantityOfItemNumber = new Image[itemSprites[i].transform.childCount];
            for (int j = 0; j < itemSprites[i].transform.childCount; j++)
            {
                items[i].quantityOfItemNumber[j] = itemSprites[i].transform.GetChild(j).GetComponent<Image>();
            }
        }
        SetAllHeldItemsToBlank();
    }

    public void ChangeCharacterArt(Sprite image)
    {
        characterPortrait.sprite = image;
    }

    public void ChangeHeldItemArt(List<iCaryable> caryables)
    {
        SetAllHeldItemsToBlank();
        for (int i = 0; i< caryables.Count; i++)
        {
            items[i].heldItemImage.transform.parent.gameObject.SetActive(true);
            items[i].inUse = true;
            items[i].heldItemImage.sprite = caryables[i].CaryableObjectSprite;

            // Sets ones tens and hundreds of items
            for (int j = 0; j < items[i].quantityOfItemNumber.Length; j++)
            {  
              items[i].quantityOfItemNumber[j].sprite = SpriteHolder.instance.GetNumberArtFromIDNumber((int)(caryables[i].NumberOfItemsInSupply * Mathf.Pow(.10f, j) % 10));
            }
            
        }
    }

    private void SetAllHeldItemsToBlank()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].heldItemImage.transform.parent.gameObject.SetActive(false);
        }
    }

    public void SetHeldItemArt(Sprite image)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(!items[i].inUse)
            {
                items[i].heldItemImage.sprite = image;
                items[i].inUse = true;
                return;
            }
        }
    }


}
