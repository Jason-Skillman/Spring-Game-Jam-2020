using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public static InventoryManager Instance {
        get; private set;
    }

    public Resource pickup;


    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start() {
        if(pickup != null)
            pickup = Instantiate(pickup);
    }

    public Resource GetPickup() {
        return pickup;
    }

}
