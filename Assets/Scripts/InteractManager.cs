using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour {

    private Camera cameraMain;


    void Awake() {
        DontDestroyOnLoad(gameObject);
        cameraMain = Camera.main;
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();

                //Is the hit object not interactable?
                if(interactable == null) return;

                //Interact with the interactable
                interactable.OnInteract();
            }

        }
    }

}
