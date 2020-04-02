using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour, ITransportable {

    public Resource resource;
    public int amount = 1;
    

    void Awake() {
        resource = Instantiate(resource);
        resource.amount = amount;
    }

    void Update() {

    }

    public Resource OnPeek() {
        return resource;
    }

    public void OnPickup() {
        Destroy(gameObject);
    }

    public void OnRejected() {
        //print("Cant pick this up");
    }

}
