using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Miner : MonoBehaviour
{
    public bool mineIron;
    public bool mineGold;
    public bool canMine;
    private bool turnedOn;
    private bool turnedOff;

    public float fuel = 0;

    public GameObject ironOre, goldOre;

    public Transform exitPoint;

    public float miningSpeed = .3f;




    // Start is called before the first frame update
    void Start()
    {
        exitPoint = transform.GetChild(0);
        TurnOn();
    }


    void TurnOn()
    {
        turnedOn = true;
        IsOn();
    }
    void TurnOff()
    {
        turnedOn = false;
        turnedOff = true;
    }

    void IsOn()
    {
        if (mineIron && mineGold.Equals(false))
        {
            MiningIron();
        }
        else if (mineGold && mineIron.Equals(false))
        {
            MiningGold();
        }
    }

    private void MiningGold()
    {
        if (canMine && fuel > 0)
        {          
                Instantiate(ironOre, new Vector3(exitPoint.transform.position.x + UnityEngine.Random.Range(-0.42f, .42f), exitPoint.transform.position.y, exitPoint.transform.position.z), transform.rotation);
                StartCoroutine(MiningCooldown(.1f));
                canMine = false;
        }
    }

    private void MiningIron()
    {
        if (canMine && fuel > 0)
        {
           
            Instantiate(ironOre, new Vector3(exitPoint.transform.position.x + UnityEngine.Random.Range(-0.42f, .42f), exitPoint.transform.position.y, exitPoint.transform.position.z), transform.rotation);
            StartCoroutine(MiningCooldown(.3f));
            canMine = false;
        }
    }

    private void Update()
    {
        if (turnedOn)
        {
            fuel -= .3f;
        }
    }


    IEnumerator MiningCooldown(float cooldown)
    {
        if (turnedOn)
        {
            yield return new WaitForSeconds(miningSpeed + cooldown);
            canMine = true;
            IsOn();
        }
    }

}
