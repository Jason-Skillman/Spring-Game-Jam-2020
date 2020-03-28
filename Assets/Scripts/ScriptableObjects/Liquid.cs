using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Liquid", menuName = "Scriptable Objects/Liquid", order = 2)]
public class Liquid : ScriptableObject {

    public string title;
    public Color color;
    public bool flamable = false;

}
