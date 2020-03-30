using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather : MonoBehaviour, IInteractable
{

    [Range(0, 10)]
    public int amountToGather = 1;

    public Resource resource;
    public GameObject itemPickup;

    [ContextMenu("Interact")]
    public void OnInteract()
    {
        SpawnResource();
    }

    void SpawnResource()
    {
        if (amountToGather > 0)
        {
            GameObject item = Instantiate(itemPickup, transform.position, transform.rotation);
            item.GetComponent<ItemPickup>().itemResource = resource;
            item.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(1, 4), Random.Range(2, 5), Random.Range(1, 5)));
            amountToGather--;
        }
    }
}
