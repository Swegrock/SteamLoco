using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public Player PlayerScript;

    void OnTriggerEnter(Collider other){
        PlayerScript.Grounded = true;
    }

    void OnTriggerExit(Collider other){
        PlayerScript.Grounded = false;
    }
}