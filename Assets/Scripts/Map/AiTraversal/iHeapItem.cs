using System;
using System.Collections;
using System.Collections.Generic;


public interface iHeapItem<T> : IComparable<T>
{
   int HeapIndex { get; set; }

}
