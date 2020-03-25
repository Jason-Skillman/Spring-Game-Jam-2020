using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Smelter : MonoBehaviour, IInteractable {

    public CinemachineVirtualCamera cmCamera;
    public GameObject hud;

    private bool interactState;


    void Start() {

    }

    void Update() {

    }

    [ContextMenu("Interact")]
    public void OnInteract() {
        interactState = !interactState;
        if(interactState) {
            ShowHUD();
            cmCamera.Priority = 100;
        } else {
            HideHUD();
            cmCamera.Priority = 0;
        }
    }

    public void ShowHUD() {
        hud.SetActive(true);
    }

    public void HideHUD() {
        hud.SetActive(false);
    }

}
