using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : MonoBehaviour, IInteractable, IMachineInput {

    public InputTrigger inputTrigger;

    [Header("Debug")]
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
    public event Event OnInteractEvent, OnItemSlotFuel, OnItemSlotInput, OnInput;

    public delegate void ToggleEvent(bool value);
    public event ToggleEvent OnTogglePower;
    

    void Start() {
        inputTrigger.MachineInput = this;
    }

    void Update() {
        
    }

    [ContextMenu("Interact")]
    public void OnInteract() {
        OnInteractEvent?.Invoke();
    }

    public void OnMachineInput(ITransportable transportable) {
        Resource resource = transportable.OnPeek();

        //Is this resource not wood?
        if(!resource.title.Equals("Wood")) {
            transportable.OnRejected();
            return;
        }

        //Callback for picking up the transport
        transportable.OnPickup();

        if(input == null) {
            input = Instantiate(resource);
            input.amount = 0;
        }

        input.Add(ref resource);

        OnInput();
    }

    public void TogglePower() {
        IsOn = !IsOn;
    }

    /// <summary>
    /// Called by the item slot button when it is clicked
    /// </summary>
    public void OnClick_ItemSlotFuel() {
        Resource resource = InventoryManager.Instance.pickup;

        if(resource == null) {
            print("Nothing picked up");
            //Todo: pickup fuel here
            return;
        }
        if(!resource.title.Equals("Coal")) return;

        AddCoal(resource);
    }

    /// <summary>
    /// Called by the item slot button when it is clicked
    /// </summary>
    public void OnClick_ItemSlotInput() {
        
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
