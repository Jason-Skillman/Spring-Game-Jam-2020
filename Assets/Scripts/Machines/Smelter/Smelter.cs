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

    [Header("Fuel")]
    [Tooltip("The time in seconds of one peice of fuel")]
    public int fuelMax = 60;

    public int Fuel {
        get; private set;
    }
    

    [Header("Inner Gears")]
    public InputTrigger inputTrigger;
    public GameObject outputPosition;
    public GameObject prefabTransport;

    private Coroutine coroutineTimerOutput, coroutineFuelBurn;


    public Resource ResourceFuel {
        get; set;
    }
    public Resource ResourceInput {
        get; set;
    }

    private bool isOn;
    public bool IsOn {
        get {
            return isOn;
        }
        set {
            isOn = value;

            if(isOn) coroutineTimerOutput = StartCoroutine(OutputTimerAsync());
            else StopCoroutine(coroutineTimerOutput);

            if(isOn) coroutineFuelBurn = StartCoroutine(FuelBurnTimerAsync());
            else StopCoroutine(coroutineFuelBurn);

            OnPowerToggled(isOn);
        }
    }

    public delegate void Event();
    public event Event OnInteractEvent, OnItemSlotFuel, OnItemSlotInput, OnInput, OnProductCreated, OnFuelTick;

    public delegate void ToggleEvent(bool value);
    public event ToggleEvent OnPowerToggled;


    private void Awake() {
        //Debug
        //Fuel = 6;
    }

    private void Start() {
        inputTrigger.MachineInput = this;
    }

    public void OnInteract() {
        OnInteractEvent?.Invoke();
    }

    public void OnMachineInput(ITransportable transportable) {
        Resource resource = transportable.OnPeek();

        if(ResourceInput == null) {
            ResourceInput = Instantiate(resource);
            ResourceInput.amount = 0;
        }

        //Is this resource not wood?
        if(!resource.title.Equals("Wood")) {
            transportable.OnRejected();
            return;
        }
        //Is the input amount full
        if(ResourceInput.IsFull) {
            transportable.OnRejected();
            return;
        }

        //Callback for picking up the transport
        transportable.OnPickup();

        ResourceInput.Add(ref resource);

        OnInput();
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

    public void TogglePower() {
        IsOn = !IsOn;
    }

    public void AddCoal(Resource coal) {
        //If fuel is empty
        if(ResourceFuel == null) {
            ResourceFuel = Instantiate(coal);
        } else {
            ResourceFuel.amount += coal.amount;
            if(ResourceFuel.amount > ResourceFuel.maxAmount) ResourceFuel.amount = ResourceFuel.maxAmount;
        }

        OnItemSlotFuel();
    }

    public IEnumerator FuelBurnTimerAsync() {
        while(true) {
            yield return new WaitForSeconds(1);
            OnFuelTick();

            if(Fuel <= 0) {
                if(ResourceFuel.amount <= 0) continue;

                //Use the next block of fuel
                ResourceFuel.amount--;
                Fuel = fuelMax;
                //Fuel = 6;
                continue;
            }

            Fuel -= 1;
        }
    }

    public IEnumerator OutputTimerAsync() {
        while(true) {
            yield return new WaitForSeconds(1);
            
            if(ResourceInput == null) continue;
            if(ResourceInput.IsEmpty) continue;
            //Is their not enough in the input to create a product?
            if(ResourceInput.amount < inputAmount) continue;

            //Create the output transport
            GameObject outputGameObject = Instantiate(prefabTransport);
            outputGameObject.transform.position = outputPosition.transform.position;

            //Create the transport's resource
            Resource outputResource = Instantiate(productOutput);
            outputResource.amount = outputAmount;
            ResourceInput.amount -= inputAmount;

            //Load the resource onto the transport
            Transport transport = outputGameObject.GetComponent<Transport>();
            transport.Resource = outputResource;

            OnProductCreated();
        }
    }

}
