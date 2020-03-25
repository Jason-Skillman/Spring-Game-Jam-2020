using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Smelter : MonoBehaviour, IInteractable {

    public CinemachineVirtualCamera cmCamera;
    public HUDController hudController;

    private bool interactState;


    void Start() {

    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            OnInteract();
        }
    }

    [ContextMenu("Interact")]
    public void OnInteract() {
        if(hudController.IsAnimating) return;

        interactState = !interactState;
        if(interactState) {
            hudController.ShowHUD();
            cmCamera.Priority = 100;
        } else {
            hudController.HideHUD();
            cmCamera.Priority = 0;
        }
    }

}
