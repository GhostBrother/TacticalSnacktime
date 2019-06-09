using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : iHeapItem<T>
{
    T[] items;
    int currentItemCount;

    public Heap(int maxSize)
    {
        items = new T[maxSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUpward(item);
        currentItemCount++;
    }

    public T RemoveHead()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDownward(items[0]);
        return firstItem;
    }

    public void UpdateItem(T item)
    {
        SortUpward(item);
    }

    public int Count { get { return currentItemCount; } }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    private void SortDownward(T item)
    {
        while(true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;


                if (childIndexRight < currentItemCount)
                {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }

                else
                    return;
            }

            else
                return;
        }
    }

    private void SortUpward(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap( item, parentItem);
            }
            else
                break;
        }
    }

    private void Swap(T item1, T item2)
    {
        items[item1.HeapIndex] = item2;
        items[item2.HeapIndex] = item1;

        int tempIndex = item1.HeapIndex;

        item1.HeapIndex = item2.HeapIndex;

        item2.HeapIndex = tempIndex;
    }
}
