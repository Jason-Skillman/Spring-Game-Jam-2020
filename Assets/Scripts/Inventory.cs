using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Resource> itemList;

    public Inventory()
    {
        itemList = new List<Resource>();
        Debug.Log("Inventory Count:" + itemList.Count);
    }


    public void AddItem(Resource resource)
    {
        itemList.Add(resource);
    }

    public List<Resource> GetItemList()
    {
        return itemList;
    }
}
