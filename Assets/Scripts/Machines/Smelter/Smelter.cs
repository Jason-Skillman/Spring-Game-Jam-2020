using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : MonoBehaviour, IInteractable {

    private Resource wood, coal;

    public bool IsOn {
        get; set;
    }

    public delegate void Event();
    public event Event OnInteractEvent;


    void Awake() {
        
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            IsOn = true;
        } else if(Input.GetKeyDown(KeyCode.Alpha2)) {
            IsOn = false;
        }
    }

    [ContextMenu("Interact")]
    public void OnInteract() {
        if(OnInteractEvent != null)
            OnInteractEvent();
    }

    public void AddCoal(Resource coal) {
        
    }

}
