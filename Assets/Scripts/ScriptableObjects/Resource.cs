using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Scriptable Objects/Resource", order = 1)]
public class Resource : ScriptableObject {

    public string title;

    public string description;

    [Range(1, 99)]
    public int amount = 1;

    [Range(1, 99)]
    public int maxAmount = 99;

    [Range(0, 1f)]
    public float weight = 1;

    public GameObject prefab;

}
