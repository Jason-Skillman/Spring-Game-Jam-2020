using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{    public Inventory inventory;

    [SerializeField]
    private UI_Inventory uiInventory;
    [SerializeField]
    public Resource[] testResources;
    // Start is called before the first frame update
    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

       
    }

    void Start()
    {
        for (int x = 0; x < testResources.Length; x++)
            inventory.AddItem(testResources[x]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
