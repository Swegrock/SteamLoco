using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacking : MonoBehaviour
{
    [Header("Stack settings")]
    [Tooltip("The maximum size of the stack. -1 for infinite stack.")]
    public int MaxStack;
    [Tooltip("The offset for the entire stack.")]
    public Vector3 StackOffset;
    [Tooltip("Whether the stack will sway or not.")]
    public bool StaticStack;
    [Tooltip("If objects in the stack are collidable.")]
    public bool DisableCollisions;
    private List<GameObject> StackedObjects = new List<GameObject>();

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Item"){
            print("add");
            Add(other.gameObject);
        }
    }

    public void Add(GameObject addObject){
        Collider col = addObject.GetComponent<Collider>();
        Rigidbody rb = addObject.GetComponent<Rigidbody>();
        if (DisableCollisions && col != null)
            col.enabled = false;
        if (rb != null)
            rb.isKinematic = true;
        addObject.transform.SetParent(transform);
        StackedObjects.Add(addObject);
        OrganiseStack();
    }

    public void Remove(GameObject gameObject){

    }

    public void Remove(int index){

    }

    public void OrganiseStack(){
        float stackHeight = 0;
        for (int i = 0; i < StackedObjects.Count; i++){
            StackedObjects[i].transform.SetParent(null);
            float extent = GetExtent(StackedObjects[i]);
            if (i > 0){
                stackHeight += extent;
            }
            float x = StackOffset.x;
            float y = (StackOffset.y + stackHeight);
            float z = StackOffset.x;
            StackedObjects[i].transform.position = new Vector3(x, y, z);
            StackedObjects[i].transform.SetParent(transform);
            stackHeight += extent;
        }
    }

    private float GetExtent(GameObject gameObject){
        Collider col = gameObject.GetComponent<Collider>();
        Renderer ren = gameObject.GetComponent<Renderer>();
        return ren.bounds.extents.y + ren.bounds.center.y / 2;
    }
}
