using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmelterHUD : MonoBehaviour {

    public Smelter smelter;
    public CinemachineVirtualCamera cmCamera;

    public Image imagePower;
    public ItemSlot itemSlotFuel, itemSlotInput;

    private Canvas canvas;
    private Animator animator;

    private bool stateOpen;

    public bool IsOpen {
        get; private set;
    }

    public bool IsAnimating {
        get; set;
    }


    private void Awake() {
        canvas = GetComponent<Canvas>();
        animator = GetComponent<Animator>();

        canvas.worldCamera = Camera.main;
    }

    private void Start() {
        //Turn off the HUD
        canvas.enabled = false;

        //Set power button
        Smelter_OnTogglePower(false);

        smelter.OnInteractEvent += Smelter_OnInteractEvent;
        smelter.OnTogglePower += Smelter_OnTogglePower;

        smelter.OnItemSlotFuel += CalculateItemSlots;
        smelter.OnInput += CalculateItemSlots;
        smelter.OnProductCreated += CalculateItemSlots;
    }

    private void Smelter_OnInteractEvent() {
        //Is the animator already animating?
        if(IsAnimating) return;

        //Toggle the state
        stateOpen = !stateOpen;

        if(stateOpen) {
            ShowHUD();
            cmCamera.Priority = 100;
        } else {
            HideHUD();
            cmCamera.Priority = 0;
        }
    }

    private void Smelter_OnTogglePower(bool value) {
        if(value) {
            Color c = imagePower.color;
            c.a = 1.0f;
            imagePower.color = c;
        } else {
            Color c = imagePower.color;
            c.a = 0.4f;
            imagePower.color = c;
        }
    }

    private void CalculateItemSlots() {
        itemSlotFuel.Calculate(smelter.Fuel);
        itemSlotInput.Calculate(smelter.Input);
    }

    /// <summary>
    /// Called when the animation has stopped playing
    /// </summary>
    public void OnStopAnimating() {
        IsAnimating = false;

        if(!IsOpen)
            canvas.enabled = false;
    }

    public void ShowHUD() {
        IsOpen = true;
        IsAnimating = true;
        canvas.enabled = true;
        animator.SetTrigger("FadeIn");
    }

    public void HideHUD() {
        IsOpen = false;
        IsAnimating = true;
        animator.SetTrigger("FadeOut");
    }

    

}
