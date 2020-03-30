using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : MonoBehaviour, IInteractable {

    public Resource fuel, input;

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
    public event Event OnInteractEvent, OnItemSlotFuel, OnItemSlotInput;

    public delegate void ToggleEvent(bool value);
    public event ToggleEvent OnTogglePower;

    /*public delegate void ItemSlotEvent(Resource resource);
    public event ItemSlotEvent ;*/
    

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

    public void ItemSlotFuelClick() {
        Resource resource = InventoryManager.Instance.pickup;

        if(resource == null) return;
        if(!resource.title.Equals("Coal")) return;

        AddCoal(resource);
    }

    public void AddCoal(Resource coal) {
        //If fuel is empty
        if(fuel == null) {
            fuel = Instantiate(coal);
        } else {
            fuel.amount += coal.amount;
            if(fuel.amount > fuel.maxAmount) fuel.amount = fuel.maxAmount;
        }

        OnItemSlotFuel();
    }

}
