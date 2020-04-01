using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {

    public Image resourceSprite;
    public TextMeshProUGUI amountText;


    private void Start() {
        resourceSprite.enabled = false;
        amountText.enabled = false;
    }

    public void Calculate(Resource resource) {
        if(resource == null) {
            resourceSprite.enabled = false;
            amountText.enabled = false;
        } else {
            resourceSprite.enabled = true;
            amountText.enabled = true;

            resourceSprite.sprite = resource.sprite;
            amountText.text = resource.amount.ToString();
        }
    }

}
