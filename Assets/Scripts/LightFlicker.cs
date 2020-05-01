using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float normalLight;
    public float flickerAmount;
    void LateUpdate()
    {
        GetComponent<Light>().intensity = normalLight + Mathf.PerlinNoise(Time.time, Time.time) * flickerAmount;
    }
}
