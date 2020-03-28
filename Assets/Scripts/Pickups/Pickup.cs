using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IPickupable {

    [Header("Resource")]
    public Resource itemResource;
    public Resource ItemResource {
        get {
            return itemResource;
        }
        set {
            itemResource = value;
        }
    }

    public bool HasResource {
        get {
            return (ItemResource != null);
        }
    }

    [Range(1, 99)]
    public int amount = 1;


    void Start() {
        ItemResource = Instantiate(itemResource);
    }

    void Update() {

    }

    public void OnPickup() {
        
    }

}
