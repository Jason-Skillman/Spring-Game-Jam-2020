using System;
using UnityEditor;
using UnityEngine;

[Obsolete()]
//[CustomEditor(typeof(ItemPickup))]
public class ItemPickupEditor : Editor {

    /*private ItemPickup instance;

    private SerializedObject myObj;
    //private GameObject gameObject;
    private SerializedProperty pro;

    void OnEnable() {
        instance = (ItemPickup)target;

        //myObj = new SerializedObject(instance);
        //gameObject = instance.prefabInstance as GameObject;

        myObj = new SerializedObject(target);
        pro = myObj.FindProperty("prefabInstance");
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        myObj.ApplyModifiedProperties();
        //pro.objectReferenceValue = new GameObject();
        pro.objectReferenceValue = instance.prefabInstance;

    }*/

}
