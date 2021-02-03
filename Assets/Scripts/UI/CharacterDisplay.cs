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

    public delegate void OnArrowStopMoving();
    public OnArrowStopMoving onStopMoving;

    heldItems[] items;

    List<iCaryable> _caryables;
    int _index;

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

        for(int j = 0; j < donessTrackers.Length; j++)
        {
            donessTrackers[j].InitMeter();
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

        _caryables = caryables;
        _index = 0;
        
        for (int j = 0; j < MaxItemsCharacterCanHold; j++)
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

            if (caryables[i] is Food)
            {
                Food food = (Food)caryables[i];
                donessTrackers[i].gameObject.SetActive(true);
            }

        }
    }

    public void SetDonenessTracks(List<iCaryable> caryables)
    {
        for (int i = 0; i < caryables.Count; i++)
        {
            if (caryables[i] is Food)
            {
                Food food = (Food)caryables[i];
                donessTrackers[i].SetMaxValue(food.DonenessesLevels[5]);
                donessTrackers[i].SetArrowOnDonessTracker(food.CurrentDoness);
            }
        }
    }

    public void UpdateDonenessTrackers(List<iCaryable> caryables, int i)
    {
            if (caryables[i] is Food)
            {
                if (donessTrackers[i].isActiveAndEnabled)
                {
                    Food food = (Food)caryables[i];

                    if (i == caryables.Count -1)
                        donessTrackers[i].onStopMoving = onStopMoving.Invoke;
                    else
                        donessTrackers[i].onStopMoving = MoveNextTracker;

                    donessTrackers[i].MoveArrowsAlongTrack(food.CurrentDoness);
                }
            }
    }

    void MoveNextTracker()
    {
        UpdateDonenessTrackers(_caryables, ++_index);
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
