using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Look into changing this into a generic once it works
public class CharacterCoasterPool : MonoBehaviour
{
    [SerializeField]
    CharacterCoaster characterCoaster;

    Queue<CharacterCoaster> gameObjectPool;
    int size;

    public static CharacterCoasterPool Instance { get; private set; }

    private void Start()
    {
        gameObjectPool = new Queue<CharacterCoaster>();
        size = 100;
        initializePool();
        Instance = this;
    }

    void initializePool()
    {
        for(int i = 0; i < size; i++)
        {
            CharacterCoaster obj = Instantiate(characterCoaster);
            obj.gameObject.SetActive(false);
            gameObjectPool.Enqueue(obj);
        }

    }

    public CharacterCoaster SpawnFromPool()
    {
        return SpawnFromPool(new Vector3(0,0,0), Quaternion.identity);
    }

    public CharacterCoaster SpawnFromPool(Vector3 pos, Quaternion rotation)
    {
        CharacterCoaster objectToSpawn = gameObjectPool.Dequeue();
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.gameObject.SetActive(true);

        gameObjectPool.Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
