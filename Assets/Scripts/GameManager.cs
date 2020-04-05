using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public GameMode gameMode = GameMode.PlayMode;
	
	[Header("Edit Mode")]
	public GameObject prefabHoverPos;
	public GameObject prefabHoverNeg;

	private Camera cameraMain;
	private Ray ray;
	private Vector3 hoverPoint;
	private GameObject gameObjectHover;

	public enum GameMode {
		PlayMode,
		EditMode
	}


	private void Awake() {
		DontDestroyOnLoad(gameObject);
		cameraMain = Camera.main;
	}

	private void Start() {
		if(prefabHoverPos != null)
			prefabHoverPos = Instantiate(prefabHoverPos);
		if(prefabHoverNeg != null)
			prefabHoverNeg = Instantiate(prefabHoverNeg);
	}

	private void Update() {
		HandleInput();
		
		switch(gameMode) {
			case GameMode.PlayMode:
				OnPlayMode();
				break;
			case GameMode.EditMode:
				OnEditMode();
				break;
		}
	}

	private void HandleInput() {
		if(Input.GetKeyDown(KeyCode.Tab)) {
			ToggleGameMode();
		}
	}

	private void OnPlayMode() {
		if(Input.GetMouseButtonDown(0)) {
			ray = cameraMain.ScreenPointToRay(Input.mousePosition);
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

	private void OnEditMode() {
		ray = cameraMain.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray);
				
		//Interate through all of the hit points until the grid is found
		for(int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits[i];
					
			//If this object not the building grid try again.
			if(!hit.transform.gameObject.CompareTag("BuildGrid")) continue;
					
			hoverPoint = hit.point;
			//hitPoint.y = 0.25f;

			//Convert the hit point to grid position. Then convert back to world position
			int multiplierZ = Mathf.RoundToInt(hoverPoint.z / 0.5f);
			hoverPoint.z = multiplierZ * 0.5f;
					
			int multiplierX = Mathf.RoundToInt(hoverPoint.x / 0.5f);
			hoverPoint.x = multiplierX * 0.5f;

			break;
		}
				
		//Show the hover prefab
		prefabHoverPos.SetActive(true);
		prefabHoverPos.transform.position = hoverPoint;
	}

	[ContextMenu("Toggle Game Mode")]
	public void ToggleGameMode() {
		if(gameMode == GameMode.PlayMode) gameMode = GameMode.EditMode;
		else gameMode = GameMode.PlayMode;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;

		if(gameMode == GameMode.EditMode) {
			if(hoverPoint != null) {
				Vector3 offset = new Vector3(0, 0.25f, 0);
				Gizmos.DrawWireCube(hoverPoint + offset, Vector3.one * 0.5f);
				//print("draw");
			}
		}
	}
}