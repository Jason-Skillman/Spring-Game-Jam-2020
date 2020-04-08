using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemContainer
{
    bool ContainsItem(Resource item);
    int ItemCount(Resource item);
    bool RemoveItem(Resource item);
    bool AddItem(GameObject item);
    bool isFull();
}
