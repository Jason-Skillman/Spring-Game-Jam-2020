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

    private bool interactState;

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
        smelter.OnItemSlotFuel += Smelter_OnItemSlotFuel;
        smelter.OnInput += Smelter_OnInput;
    }

    private void Update() {
        
    }

    private void Smelter_OnInteractEvent() {
        interactState = !interactState;
        if(IsAnimating) return;

        if(interactState) {
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

    private void Smelter_OnItemSlotFuel() {
        itemSlotFuel.Calculate(smelter.fuel);
    }

    private void Smelter_OnInput() {
        itemSlotInput.Calculate(smelter.input);
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
