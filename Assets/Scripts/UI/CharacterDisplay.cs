using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour {

    [SerializeField]
    Image characterPortrait;

    [SerializeField]
    Image[] itemSprites;

    [SerializeField]
    DonenessTracker[] donessTrackers;

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
            donessTrackers[i].InitMeter(0);
        }
        SetAllHeldItemsToBlank();
    }

    public void ChangeCharacterArt(Sprite image)
    {
        characterPortrait.sprite = image;
    }

    public void ChangeHeldItemArt(List<iCaryable> caryables , int MaxItemsCharacterCanHold)
    {
        SetAllHeldItemsToBlank();

        for(int j = 0; j < MaxItemsCharacterCanHold; j++)
        {
            items[j].heldItemImage.transform.parent.gameObject.SetActive(true);
        }
        for (int i = 0; i < caryables.Count; i++)
        {
            items[i].inUse = true;
            items[i].heldItemImage.gameObject.SetActive(true);
            items[i].heldItemImage.sprite = caryables[i].CaryableObjectSprite;

            // Sets ones tens and hundreds of items
            for (int j = 0; j < items[i].quantityOfItemNumber.Length; j++)
            {
              items[i].quantityOfItemNumber[j].gameObject.SetActive(true);
              items[i].quantityOfItemNumber[j].sprite = SpriteHolder.instance.GetNumberArtFromIDNumber((int)(caryables[i].NumberOfItemsInSupply * Mathf.Pow(.10f, j) % 10));
            }

            if(caryables[i] is Food)
            {
                donessTrackers[i].gameObject.SetActive(true);
            }
            
        }
    }

    private void SetAllHeldItemsToBlank()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].heldItemImage.transform.parent.gameObject.SetActive(false);
            donessTrackers[i].gameObject.SetActive(false);
            items[i].heldItemImage.gameObject.SetActive(false);

            for (int j = 0; j < items[i].quantityOfItemNumber.Length; j++)
            {
                items[i].quantityOfItemNumber[j].gameObject.SetActive(false);
            }
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
