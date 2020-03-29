using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour {

    private Animator animator;

    public bool IsOpen {
        get; private set;
    }

    public bool IsAnimating {
        get; set;
    }


    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void ShowHUD() {
        IsOpen = true;
        IsAnimating = true;
        gameObject.SetActive(true);
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
            gameObject.SetActive(false);
    }

}
