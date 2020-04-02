using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportSpawner : MonoBehaviour {

    public Resource resource;
    public int amount = 1;
    public GameObject prefab;


    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            GameObject obj = Instantiate(prefab);
            obj.transform.position = transform.position;

            Resource resourceInstance = Instantiate(resource);
            resourceInstance.amount = amount;

            Transport transport = obj.GetComponent<Transport>();
            transport.Resource = resourceInstance;
        }
    }
}
