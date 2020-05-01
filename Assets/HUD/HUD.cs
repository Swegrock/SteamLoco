using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public float Fuel;
    public Transform FuelCover;
    //public TextMeshPro TextMesh;
    public float MaxSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FuelCover.localScale.y <= 0)
            Fuel = 0;
        FuelCover.localScale = new Vector3(1, Mathf.Lerp(FuelCover.localScale.y, MaxSize - (Fuel / 20) * MaxSize, Time.deltaTime * 3), 1);
    }
}
