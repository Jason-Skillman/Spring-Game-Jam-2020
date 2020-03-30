using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterHUD : MonoBehaviour {

    public Smelter smelter;
    public CinemachineVirtualCamera cmCamera;

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
    }

    private void Start() {
        smelter.OnInteractEvent += Smelter_OnInteractEvent;
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

    /// <summary>
    /// Called when the animation has stopped playing
    /// </summary>
    public void OnStopAnimating() {
        IsAnimating = false;

        if(!IsOpen)
            canvas.enabled = false;
    }

}
