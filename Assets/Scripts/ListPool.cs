using System;
using System.Collections.Generic;

// Look into changing this into a generic once it works
public class ListPool<T> : iPool<T> where T : class
{
    Func<T> produce;
    int capacity;
    List<T> objects;
    Func<T, bool> useTest;
    bool isExpandable;
    public ListPool(Func<T> factoryMethoid, int maxSize, Func<T,bool> inUse, bool expandable = true)
    {
        produce = factoryMethoid;
        capacity = maxSize;
        objects = new List<T>(maxSize);
        useTest = inUse;
        isExpandable = expandable;
    }
    public T GetInstance()
    {
        var count = objects.Count;
        foreach (var item in objects)
        {
            if (!useTest(item))
            {
                return item;
            }
        }

        if(count >= capacity && !isExpandable)
        {
            return null;  
        }

        var obj = produce();
        objects.Add(obj);
        return obj;
    }
}
