using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    new public ParticleSystem particleSystem;
    public HUD Hud;
    private List<GameObject> items;
    private float currentFlameRate;

    void Start(){
        items = new List<GameObject>();
    }

    void FixedUpdate(){
        int i = 0;
        foreach (GameObject item in items){
            item.transform.position = Vector3.Lerp(item.transform.position, transform.position, Time.deltaTime);
            item.transform.localScale = Vector3.Slerp(item.transform.localScale, Vector3.zero, Time.deltaTime);
            if (items[i].transform.localScale.x <= 0.1){
                float flammability = item.GetComponent<Item>().Flammability;
                currentFlameRate += flammability;
                Hud.Fuel += flammability;
                items.Remove(item);
                Destroy(item);
                break;
            }
        }
    }

    void Update(){
        if (currentFlameRate > 0){
            currentFlameRate -= 0.1f;
        }
        if (currentFlameRate > 0 && !particleSystem.isPlaying){
            particleSystem.Play();
        } else if (currentFlameRate <= 0 && particleSystem.isPlaying){
            particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public void ItemBurned(GameObject item){
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.layer = LayerMask.NameToLayer("Held");
        items.Add(item);
    }
}
