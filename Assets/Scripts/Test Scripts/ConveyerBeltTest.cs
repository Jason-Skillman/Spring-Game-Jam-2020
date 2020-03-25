using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBeltTest : MonoBehaviour
{
    public Transform endPoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        endPoint = this.transform.GetChild(0);
    }
    void OnTriggerStay(Collider col)
    {
        col.transform.position = Vector3.MoveTowards(col.transform.position, endPoint.transform.position,Time.deltaTime * speed);    
    }

}
