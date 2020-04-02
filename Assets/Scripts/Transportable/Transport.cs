using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour, ITransportable {

    public GameObject prefabPosition;

    private GameObject prefabInstance;

    public Resource Resource {
        get; set;
    }


    void Start() {
        if(Resource == null) return;

        prefabInstance = Instantiate(Resource.prefab);

        Vector3 originalScale = prefabInstance.transform.localScale;

        prefabInstance.transform.parent = prefabPosition.transform;
        prefabInstance.transform.localPosition = Vector3.zero;
        prefabInstance.transform.localScale = originalScale;
    }

    public Resource OnPeek() {
        return Resource;
    }

    public void OnPickup() {
        Destroy(gameObject);
    }

    public void OnRejected() {
        //print("Transport was rejected from pickup");
    }

}
