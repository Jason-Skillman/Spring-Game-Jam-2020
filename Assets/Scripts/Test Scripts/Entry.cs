using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public GameObject furnace;

    private void Start()
    {
        furnace = this.transform.parent.gameObject;
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Iron Ore")
        {
            if (furnace.GetComponent<Furnace>().turnedOn)
            {
                furnace.GetComponent<Furnace>().ironOreAmount++;
                Destroy(col.gameObject);
            }
        }
    }

}
