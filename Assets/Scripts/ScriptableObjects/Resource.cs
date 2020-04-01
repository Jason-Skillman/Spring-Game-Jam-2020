using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Scriptable Objects/Resource", order = 1)]
public class Resource : ScriptableObject {

    public string title;

    public string description;

    public Sprite sprite;

    [Range(1, 99)]
    public int amount = 1;

    [Range(1, 99)]
    public int maxAmount = 99;

    public GameObject prefab;


    /// <summary>
    /// Adds inputed resource into this resource if possible
    /// </summary>
    /// <param name="resourceToAdd"></param>
    public void Add(ref Resource resourceToAdd) {
        //Is the resource null?
        if(resourceToAdd == null) return;
        //Is the resource empty
        if(resourceToAdd.amount <= 0) return;

        //Add the amount together
        int newAmount = amount + resourceToAdd.amount;

        //Is the amount more then the maximum amount
        if(newAmount > maxAmount) {
            //The amount that was taken from the input resource to fill this resource
            int amountTaken = maxAmount - amount;

            //Set the input resource to the new amount
            resourceToAdd.amount -= amountTaken;

            //Cap this resource at max
            amount = maxAmount;

            return;
        }

        amount = newAmount;
        resourceToAdd.amount = 0;
    }

}
