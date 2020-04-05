using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour {
	public GameMode gameMode = GameMode.PlayMode;
	public GameObject prefab;

	private Camera cameraMain;
	private Vector3 hitPoint;

	public enum GameMode {
		PlayMode,
		EditMode
	}


	private void Awake() {
		DontDestroyOnLoad(gameObject);
		cameraMain = Camera.main;
	}

	private void Update() {
		switch(gameMode) {
			case GameMode.PlayMode:
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
				break;
			case GameMode.EditMode:
				Ray ray2 = cameraMain.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit2;
				if(Physics.Raycast(ray2, out hit2, Mathf.Infinity)) {
					if(!hit2.transform.gameObject.CompareTag("BuildGrid")) return;

					hitPoint = hit2.point;
					hitPoint.y = 0.0f;

					//Convert the hit point to grid position. Then convert back to world position
					int multiplierZ = Mathf.RoundToInt(hitPoint.z / 0.5f);
					hitPoint.z = multiplierZ * 0.5f;
					
					int multiplierX = Mathf.RoundToInt(hitPoint.x / 0.5f);
					hitPoint.x = multiplierX * 0.5f;
				}
				break;
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;

		if(gameMode == GameMode.EditMode) {
			if(hitPoint != null) {
				Gizmos.DrawWireCube(hitPoint, Vector3.one * 0.5f);
				//print("draw");
			}
		}
	}

}