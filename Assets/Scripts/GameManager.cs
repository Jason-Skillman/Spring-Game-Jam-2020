using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; private set; }
	
	public GameMode gameMode = GameMode.PlayMode;
	
	[Header("Camera")]
	public Camera cameraMain;
	public CinemachineVirtualCamera cmCamera;
	
	[Header("Camera Edge Move")]
	public bool useCameraEdgeMove = true;
	public int screenEdgeBoundary = 50;
	public float cameraSpeed = 0.01f;

	[Header("Edit Mode")]
	public GameObject grid;
	public GameObject editModeHUD;

	public GameObject prefabHoverPos;
	public GameObject prefabHoverNeg;
	public GameObject prefabArrow;

	private Ray ray;
	private Vector3 hoverVector;
	private GameObject selectedBuild;
	private GameObject selectedBuildInstance;

	private Dictionary<Vector2, GameObject> gridObjects;

	public bool IsBuildSelected {
		get { return (selectedBuild != null); }
	}

	/// <summary>
	/// The current grid vector that the player is hovering over
	/// </summary>
	public Vector2 HoverGridVector { get; private set; }

	public enum GameMode {
		PlayMode,
		EditMode
	}


	private void Awake() {
		if(Instance == null) Instance = this;
		else Destroy((gameObject));
		
		DontDestroyOnLoad(gameObject);
		gridObjects = new Dictionary<Vector2, GameObject>();
	}

	private void Start() {
		if(prefabHoverPos)
			prefabHoverPos = Instantiate(prefabHoverPos);
		if(prefabHoverNeg)
			prefabHoverNeg = Instantiate(prefabHoverNeg);
		if(prefabArrow)
			prefabArrow = Instantiate(prefabArrow);
		
		editModeHUD.SetActive(false);
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
		//Keyboard input
		if(Input.GetKeyDown(KeyCode.R)) {
			RotateBuild();
		}
		
		if(gameMode == GameMode.EditMode) {
			if(Input.mousePosition.y > 0 + 200) {
				//Mouse input
				if(Input.GetMouseButtonDown(0)) {
					if(IsBuildSelected)
						AddBuilding();
					else {
						RemoveBuilding(HoverGridVector);
					}
				}
			}
		}
		
		//Keyboard input
		if(Input.GetKeyDown(KeyCode.Tab)) {
			ToggleGameMode();

		}
		
		//Edge of screen
		if(useCameraEdgeMove) {
			if(Input.mousePosition.y > Screen.height - screenEdgeBoundary) {
				//Top
				cmCamera.transform.localPosition = cmCamera.transform.localPosition + (Vector3.back * cameraSpeed) +
				                                   (Vector3.left * cameraSpeed);
			} else if(Input.mousePosition.y < 0 + screenEdgeBoundary) {
				//Bottom
				cmCamera.transform.localPosition = cmCamera.transform.localPosition + (Vector3.forward * cameraSpeed) +
				                                   (Vector3.right * cameraSpeed);
			}

			if(Input.mousePosition.x > Screen.width - screenEdgeBoundary) {
				//Right
				cmCamera.transform.localPosition = cmCamera.transform.localPosition + (Vector3.forward * cameraSpeed) +
				                                   (Vector3.left * cameraSpeed);
			} else if(Input.mousePosition.x < 0 + screenEdgeBoundary) {
				//Left
				cmCamera.transform.localPosition = cmCamera.transform.localPosition + (Vector3.back * cameraSpeed) +
				                                   (Vector3.right * cameraSpeed);
			}
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
		editModeHUD.SetActive(false);
		if(prefabHoverPos) prefabHoverPos.SetActive(false);
		if(prefabHoverNeg) prefabHoverNeg.SetActive(false);
		if(prefabArrow) prefabArrow.SetActive(false);
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

			hoverVector = hit.point;

			//Convert the hit point to grid position, then convert back to world position for each axis
			int gridX = Mathf.RoundToInt(hoverVector.x / 0.5f);
			hoverVector.x = gridX * 0.5f;
			int gridY = Mathf.RoundToInt(hoverVector.z / 0.5f);
			hoverVector.z = gridY * 0.5f;

			//Store the hovered vector
			HoverGridVector = new Vector2(gridX, gridY);

			break;
		}
		
		//Show the edit mode game objects
		editModeHUD.SetActive(true);
		if(selectedBuildInstance) selectedBuildInstance.SetActive(true);
		prefabArrow.SetActive(true);

		//Show to correct hover feedback
		if(IsBuildSelected) {
			//If the tile already has an object on it
			if(gridObjects.ContainsKey(HoverGridVector)) {
				//Negative
				prefabHoverPos.SetActive(false);
				prefabHoverNeg.SetActive(true);
			} else {
				//Positive
				prefabHoverPos.SetActive(true);
				prefabHoverNeg.SetActive(false);
			}
		} else {
			//If the tile does not have an object on it
			if(!gridObjects.ContainsKey(HoverGridVector)) {
				//Negative
				prefabHoverPos.SetActive(false);
				prefabHoverNeg.SetActive(true);
			} else {
				//Positive
				prefabHoverPos.SetActive(true);
				prefabHoverNeg.SetActive(false);
			}
		}

		//Move the hover objects to the hover point
		prefabHoverPos.transform.position = hoverVector;
		prefabHoverNeg.transform.position = hoverVector;
		prefabArrow.transform.position = hoverVector;
		if(selectedBuildInstance) selectedBuildInstance.transform.position = hoverVector;
	}

	[ContextMenu("Toggle Game Mode")]
	public void ToggleGameMode() {
		if(gameMode == GameMode.PlayMode) gameMode = GameMode.EditMode;
		else gameMode = GameMode.PlayMode;
	}

	/// <summary>
	/// Changes the current selected object to build.
	/// <para>Set null for remove selection</para>
	/// </summary>
	/// <param name="prefabBuild"></param>
	public void ChangeSelectedBuild(GameObject prefabBuild) {
		//Destroy the previous ghost
		if(selectedBuildInstance != null) Destroy(selectedBuildInstance);
		
		//Set the selection to empty
		if(prefabBuild == null) {
			selectedBuild = null;
			selectedBuildInstance = null;
			return;
		}
		
		selectedBuild = prefabBuild;
		selectedBuildInstance = Instantiate(selectedBuild);
		selectedBuildInstance.transform.position = hoverVector;
	}

	/// <summary>
	/// Rotates the selected build
	/// </summary>
	public void RotateBuild() {
		selectedBuildInstance.transform.Rotate(Vector3.up, 90.0f);
		prefabArrow.transform.Rotate(Vector3.up, 90.0f);
	}

	/// <summary>
	/// Places the build onto the grid
	/// </summary>
	public void AddBuilding() {
		//Create a copy of the hover clone
		GameObject createdBuild = Instantiate(selectedBuildInstance, grid.transform);

		//Is their already a object here?
		if(gridObjects.ContainsKey(HoverGridVector)) {
			print("There is already a placed object here");
			return;
		}

		//Add the object to the grid
		gridObjects.Add(HoverGridVector, createdBuild);
	}

	/// <summary>
	/// Removes a build in the grid
	/// </summary>
	public void RemoveBuilding(Vector2 gridPos) {
		//Is their no object here?
		if(!gridObjects.ContainsKey(gridPos)) {
			print("No building to remove");
			return;
		}
		
		GameObject objToRemove = gridObjects[gridPos];
		
		//Remove the object
		Destroy(objToRemove);
		gridObjects.Remove(gridPos);
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;

		if(gameMode == GameMode.EditMode) {
			if(hoverVector != null) {
				Vector3 offset = new Vector3(0, 0.25f, 0);
				Gizmos.DrawWireCube(hoverVector + offset, Vector3.one * 0.5f);
			}
		}
	}
}