using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driller : MonoBehaviour
{
    public bool mineIron;
    public bool mineGold;

    public GameObject ironOre;
    public GameObject goldOre;

    public Transform exitPoint;

    public int miningSpeed;


    // Start is called before the first frame update
    void Start()
    {
        exitPoint = transform.GetChild(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
