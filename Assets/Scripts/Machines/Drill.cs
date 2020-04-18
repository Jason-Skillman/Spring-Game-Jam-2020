using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour {

	public Resource resourceToMine;
	public int amount = 1;
	public GameObject outputPos;
	public GameObject prefabTransport;

	private Coroutine coroutineMineTimer;


	void Start() {
		coroutineMineTimer = StartCoroutine(MineTimer());
	}

	private IEnumerator MineTimer() {
		while(true) {
			yield return new WaitForSeconds(1);

			GameObject obj = Instantiate(prefabTransport);
			obj.transform.position = outputPos.transform.position;
		
			Transport transport = obj.GetComponent<Transport>();
			transport.Resource = Instantiate(resourceToMine);
			transport.Resource.amount = amount;
		}
	}
}