using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[Serializable]
public class ItemPickup : MonoBehaviour, IPickupable {

    [Header("Resource")]
    public Resource itemResource;
    public Resource ItemResource {
        get {
            return itemResource;
        }
        set {
            itemResource = value;
        }
    }

    public bool HasResource {
        get {
            return (ItemResource != null);
        }
    }

    public GameObject prefabPosition;

    //[HideInInspector]
    [SerializeField]
    public GameObject prefabInstance;

    [Range(1, 99)]
    public int amount = 1;


    void Start() {
        if(!EditorApplication.isPlaying) {
            return;
        }

        if(itemResource != null) {
            ItemResource = Instantiate(itemResource);
            ItemResource.amount = amount;
        }

    }

    void Update() {
        if(!EditorApplication.isPlaying) {
            OnEditor();
            return;
        }
    }

    /// <summary>
    /// Runs in the editor only
    /// </summary>
    public void OnEditor() {
        //If you have removed the resource then destroy the left over prefab instance
        if(ItemResource == null) {
            if(prefabInstance != null) {
                DestroyImmediate(prefabInstance);
                prefabInstance = null;
                Debug.Log("Delete");
            }
        }

        if(ItemResource != null && prefabInstance == null && prefabPosition != null && ItemResource.prefab != null) {
            prefabInstance = Instantiate(ItemResource.prefab);
            prefabInstance.transform.parent = prefabPosition.transform;
            prefabInstance.transform.position = prefabPosition.transform.position;
        }
    }

    public void OnPickup() {
        
    }

}
