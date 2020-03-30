using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : MonoBehaviour, IInteractable {

    public delegate void Event();
    public event Event OnInteractEvent;


    void Start() {
        
    }

    void Update() {
        
    }

    [ContextMenu("Interact")]
    public void OnInteract() {
        if(OnInteractEvent != null)
            OnInteractEvent();
    }

}
