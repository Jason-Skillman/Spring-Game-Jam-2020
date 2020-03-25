using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    public bool debug;
    public GameObject spawnIron;
    public GameObject exitPoint, entryPoint;
    public bool canSmelt = true;
    public bool smeltIron = false;
    public bool smeltGold = false;

    public int ironOreAmount;
    public int goldOreAmount;
    // Start is called before the first frame update
    void Start()
    {
        exitPoint = transform.GetChild(1).gameObject;
        entryPoint = transform.GetChild(0).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (ironOreAmount > 0)
        {
            SmeltIron();
        }
    }
    void SmeltIron()
    {
        if (canSmelt && ironOreAmount % 2 == 0)
        {
            ironOreAmount -= 2;
            Instantiate(spawnIron, new Vector3(exitPoint.transform.position.x + Random.Range(-0.42f, .42f), exitPoint.transform.position.y, exitPoint.transform.position.z), transform.rotation);
            StartCoroutine(smeltCooldown(.5f));
            canSmelt = false;
        }
    }

    IEnumerator smeltCooldown(float length)
    {
        yield return new WaitForSeconds(length);
        canSmelt = true;
    }




}
