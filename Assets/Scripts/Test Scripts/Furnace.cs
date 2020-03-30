using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Furnace : MonoBehaviour
{
    public bool debug;
    public GameObject spawnIron;
    public GameObject exitPoint, entryPoint;
    public GameObject fuelText;

    public bool turnedOn;
    public bool turnedOff;
    public bool canSmelt = true;
    public bool smeltIron = false;
    public bool smeltGold = false;

    public int ironOreAmount;
    public int goldOreAmount;

    public float fuel;


    // Start is called before the first frame update
    void Start()
    {
        exitPoint = transform.GetChild(1).gameObject;
        entryPoint = transform.GetChild(0).gameObject;
        fuelText = transform.GetChild(2).gameObject;
        TurnOn();
    }

    void TurnOn()
    {
        turnedOn = true;
        turnedOff = false;
        IsOn();
    }

    void TurnOff()
    {
        turnedOff = true;
        turnedOn = false;
    }

    void IsOn()
    {
        if (turnedOn)
        {
            if (ironOreAmount > 0)
            {
                Debug.Log("Attempting to smelt Iron");
                SmeltIron();
            }

            if (fuel <= 0)
            {
                TurnOff();
            }
            StartCoroutine(smeltCooldown(.1f));
        }
    }
    void SmeltIron()
    {
        if (canSmelt && ironOreAmount > 2)
        {
            Debug.Log("SmeltingIron");
            ironOreAmount -= 2;
            Instantiate(spawnIron, new Vector3(exitPoint.transform.position.x + UnityEngine.Random.Range(-0.42f, .42f), exitPoint.transform.position.y, exitPoint.transform.position.z), transform.rotation);
            StartCoroutine(smeltCooldown(.5f));
            canSmelt = false;
        }
    }

    IEnumerator smeltCooldown(float length)
    {
        yield return new WaitForSeconds(length);
        canSmelt = true;
        IsOn();
    }


    private void Update()
    {
        if (turnedOn)
        {
            if (fuel > 0)
            {
                fuel -= 0.02f;
            }
        }
        fuelText.GetComponent<TextMeshPro>().text = (int)fuel + "%";
    }
}
