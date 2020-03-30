using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : MonoBehaviour, IInteractable {

    private Resource wood, coal;

    private bool isOn;


    public bool IsOn {
        get {
            return isOn;
        }
        set {
            isOn = value;
            OnTogglePower(isOn);
        }
    }

    public delegate void Event();
    public event Event OnInteractEvent;

    public delegate void ToggleEvent(bool value);
    public event ToggleEvent OnTogglePower;


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

    public void TogglePower() {
        IsOn = !IsOn;
    }

    public void AddCoal(Resource coal) {
        
    }

}
