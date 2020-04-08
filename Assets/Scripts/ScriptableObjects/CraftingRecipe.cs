using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ItemAmount
{
    public Resource Item;
    [Range(1,999)]
    public int Amount;
}

[Serializable]
public struct CraftedItem
{
    public GameObject item;
    [Range(1, 999)]
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject {

    public List<ItemAmount> Materials;
    public List<CraftedItem> Results;

    public bool CanCraft(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            if (itemContainer.ItemCount(itemAmount.Item) < itemAmount.Amount)
            { 
                return false;
            }
        }

        return true;
    }

    public void Craft(IItemContainer itemContainer)
    {
        if (CanCraft(itemContainer))
        {
            foreach (ItemAmount itemAmount in Materials)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    itemContainer.RemoveItem(itemAmount.Item);
                }
            }

            foreach (CraftedItem craftedItem in Results)
            {
                for (int i = 0; i < craftedItem.Amount; i++)
                {
                    itemContainer.AddItem(craftedItem.item);
                }
            }

        }

    }
}
