using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject HUD;
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetButton("Jump") || Input.GetButton("Shoot")){
            Menu.SetActive(false);
            HUD.SetActive(true);
        }
    }
}
