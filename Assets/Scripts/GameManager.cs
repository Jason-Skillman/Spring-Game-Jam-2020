using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public GameMode gameMode = GameMode.PlayMode;
	
	[Header("Edit Mode")]
	public GameObject prefabHoverPos;
	public GameObject prefabHoverNeg;

	[Header("Debug")]
	public GameObject selectedBuild;

	private Camera cameraMain;
	private Ray ray;
	private Vector3 hoverPoint;
	private GameObject selectedBuildInstance;

	public enum GameMode {
		PlayMode,
		EditMode
	}


	private void Awake() {
		DontDestroyOnLoad(gameObject);
		cameraMain = Camera.main;
	}

	private void Start() {
		if(prefabHoverPos)
			prefabHoverPos = Instantiate(prefabHoverPos);
		if(prefabHoverNeg)
			prefabHoverNeg = Instantiate(prefabHoverNeg);
		
		//Todo: remove later
		if(selectedBuild != null)
			selectedBuildInstance = Instantiate(selectedBuild);
		
		
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
		} else if(Input.GetKeyDown(KeyCode.R)) {
			RotateBuild();
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
		
		//Turn off edit mode gameObjects
		if(prefabHoverPos) prefabHoverPos.SetActive(false);
		if(prefabHoverNeg) prefabHoverNeg.SetActive(false);
		if(selectedBuildInstance) selectedBuildInstance.SetActive(false);
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
				
		//Show the edit mode game objects
		if(prefabHoverPos) prefabHoverPos.SetActive(true);
		if(selectedBuildInstance) selectedBuildInstance.SetActive(true);
		
		//Move the hover objects to the hover point
		prefabHoverPos.transform.position = hoverPoint;
		prefabHoverNeg.transform.position = hoverPoint;
		selectedBuildInstance.transform.position = hoverPoint;
	}

	[ContextMenu("Toggle Game Mode")]
	public void ToggleGameMode() {
		if(gameMode == GameMode.PlayMode) gameMode = GameMode.EditMode;
		else gameMode = GameMode.PlayMode;
	}

	public void RotateBuild() {
		
		Vector3 vector = selectedBuildInstance.transform.forward * 90;


		selectedBuildInstance.transform.rotation =
			Quaternion.RotateTowards(selectedBuildInstance.transform.rotation, Quaternion.Euler(vector), 90.0f);
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