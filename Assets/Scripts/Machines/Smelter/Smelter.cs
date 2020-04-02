using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Smelter : MonoBehaviour, IInteractable, IMachineInput {

    [Header("Input")]
    [Tooltip("The required product")]
    public Resource productInput;
    [Tooltip("The amount required from the input")]
    public int inputAmount = 1;

    [Header("Output")]
    [Tooltip("The product to produce")]
    public Resource productOutput;
    [Tooltip("The amount to produce")]
    public int outputAmount = 1;

    

    [Header("Inner Gears")]
    public InputTrigger inputTrigger;
    public GameObject outputPosition;
    public GameObject prefabTransport;

    private Coroutine coroutineTimerOutput;

    public Resource Fuel {
        get; set;
    }
    public Resource Input {
        get; set;
    }

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
    public event Event OnInteractEvent, OnItemSlotFuel, OnItemSlotInput, OnInput, OnProductCreated;

    public delegate void ToggleEvent(bool value);
    public event ToggleEvent OnTogglePower;
    

    private void Start() {
        inputTrigger.MachineInput = this;

        coroutineTimerOutput = StartCoroutine(OutputTimerAsync());
    }

    public void OnInteract() {
        OnInteractEvent?.Invoke();
    }

    public void OnMachineInput(ITransportable transportable) {
        Resource resource = transportable.OnPeek();

        if(Input == null) {
            Input = Instantiate(resource);
            Input.amount = 0;
        }

        //Is this resource not wood?
        if(!resource.title.Equals("Wood")) {
            transportable.OnRejected();
            return;
        }
        //Is the input amount full
        if(Input.IsFull) {
            transportable.OnRejected();
            return;
        }

        //Callback for picking up the transport
        transportable.OnPickup();

        Input.Add(ref resource);

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
        if(Fuel == null) {
            Fuel = Instantiate(coal);
        } else {
            Fuel.amount += coal.amount;
            if(Fuel.amount > Fuel.maxAmount) Fuel.amount = Fuel.maxAmount;
        }

        OnItemSlotFuel();
    }

    public IEnumerator OutputTimerAsync() {
        while(true) {
            yield return new WaitForSeconds(1);

            if(Input == null) continue;
            if(Input.IsEmpty) continue;
            //Is their not enough in the input to create a product?
            if(Input.amount < inputAmount) continue;

            //Create the output transport
            GameObject outputGameObject = Instantiate(prefabTransport);
            outputGameObject.transform.position = outputPosition.transform.position;

            //Create the transport's resource
            Resource outputResource = Instantiate(productOutput);
            outputResource.amount = outputAmount;
            Input.amount -= inputAmount;

            //Load the resource onto the transport
            Transport transport = outputGameObject.GetComponent<Transport>();
            transport.Resource = outputResource;

            OnProductCreated();
        }
    }

}
