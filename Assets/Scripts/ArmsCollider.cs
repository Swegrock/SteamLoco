using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsCollider : MonoBehaviour
{
    public Player playerScript;
    public GameObject item;
    void Update()
    {
        if (Input.GetButton("Pickup") && item != null && !playerScript.HasItem(item) && playerScript.CanHoldItem()){
            playerScript.AddItem(item);
            item = null;
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Item" && !other.GetComponent<Item>().GetPickedUp()){
            item = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject == item){
            item = null;
        }
    }
}
