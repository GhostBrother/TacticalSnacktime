using UnityEngine;

public class MonoPool : MonoBehaviour
{
    public int Capacity;

    ListPool<CharacterCoaster> CharacterCoasterpool { get; set; }
    public CharacterCoaster CharacterCoasterprototype;

    ListPool<DonenessTracker> DonenessTrackerPool { get; set; }
    public DonenessTracker DonenessTrackerPrototype;

    public bool Expandable = true;

    void Awake()
    {
        CharacterCoasterpool = new ListPool<CharacterCoaster>(() => Instantiate(CharacterCoasterprototype), Capacity, g => g.gameObject.activeInHierarchy == true, Expandable);

        DonenessTrackerPool = new ListPool<DonenessTracker>(() => Instantiate(DonenessTrackerPrototype), Capacity, g => g.gameObject.activeInHierarchy == true, Expandable);
    }

    public CharacterCoaster GetCharacterCoasterInstance()
    {
      CharacterCoaster toReturn = CharacterCoasterpool.GetInstance();
      toReturn.gameObject.SetActive(true);
        return toReturn;
    }

    public DonenessTracker GetDonenessTrackerInstance()
    {
        DonenessTracker toReturn = DonenessTrackerPool.GetInstance();
        toReturn.gameObject.SetActive(true);
        return toReturn;
    }

    public void PutInstanceBack(GameObject objectToPutAway)
    {
        objectToPutAway.SetActive(false);
    }

}
