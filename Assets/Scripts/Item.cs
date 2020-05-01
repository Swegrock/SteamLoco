using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    public float Weight;
    public float Flammability = 2;
    private bool pickedUp;

    void Start(){
        Color color = new Color(Random.Range(0.4F,1F), Random.Range(0.4F, 1F), Random.Range(0.4F, 1F));
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Furnace")
        {
            other.gameObject.GetComponent<Furnace>().ItemBurned(gameObject);
        }
    }

    public void SetPickedUp(bool set){
        pickedUp = set;
    }

    public bool GetPickedUp(){
        return pickedUp;
    }
}
