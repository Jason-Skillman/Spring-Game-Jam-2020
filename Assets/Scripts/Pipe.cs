using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public bool isConnected;
    public bool hasFluid;

    public Resource fluid;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected)
        {
            if (this.transform.parent.GetComponent<Pipe>().hasFluid)
            {
                hasFluid = true;
            }
            else
            {
                hasFluid = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Pipe"))
        {
            Vector3 currentPos = other.transform.position;
            isConnected = true;
            other.transform.SetParent(this.transform);

            other.transform.position = currentPos;
        }
    }

}
