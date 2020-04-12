using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildSelectButton : MonoBehaviour {

	public GameObject prefabBuild;
	public string title;
	public TextMeshProUGUI text;


	void Start() {
		if(!title.Equals("")) {
			text.text = title;
		}
	}

	public void SelectBuild() {
		GameManager.Instance.ChangeSelectedBuild(prefabBuild);
	}
	
}