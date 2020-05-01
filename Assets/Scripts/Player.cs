using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpHeight;
    public float ShootPower;
    public Transform LeftArm;
    public Transform RightArm;
    public ArmsCollider itemGrabber;
    private List<GameObject> items;
    public bool Grounded;
    private bool pickingUp;
    private Vector3 movement, direction;
    private Rigidbody rb;
    private Animation anim;
    private float halfHeight;
    private bool itemShot;
    void Start(){
        items = new List<GameObject>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();

        movement = direction = transform.forward;
        halfHeight = GetComponent<Collider>().bounds.extents.y;

        anim["Hold"].layer = 1;
        anim["Grab"].layer = 1;
    }

    void Update(){
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        movement = new Vector3(hor, 0.0f, ver);       

        direction = Vector3.RotateTowards(transform.forward, movement, Time.deltaTime * 10, 0.0f);

        if (Input.GetButtonUp("Shoot") && items.Count > 0 && !itemShot){
            print("shot");
            ShootItem(items[0]);
            itemShot = true;
        }
    }

    void FixedUpdate()
    {
        itemShot = false;
        if (Input.GetButton("Jump") & Grounded){
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * JumpHeight, ForceMode.VelocityChange);
        }

        transform.rotation = Quaternion.LookRotation(direction);

        for (int i = 0; i < items.Count; i++){
            items[i].transform.SetPositionAndRotation(transform.position + transform.forward + transform.up * i, transform.rotation);
        }

        rb.velocity = new Vector3(movement.x * Speed, rb.velocity.y, movement.z * Speed);
    }

    void LateUpdate(){
        if (!Grounded){
            anim.CrossFade("Jump", 0.3f * Time.deltaTime);
        } else if (Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.z) > 0){
            anim.CrossFade("Walk", 0.3f * Time.deltaTime);
        } else {
            anim.CrossFade("Idle", 0.3f * Time.deltaTime);
        }
        
        if (items.Count == 0 && anim.IsPlaying("Hold")){
            anim.Stop("Hold");
        }
        else if (items.Count > 0) {
            anim.CrossFade("Hold", 0.3f * Time.deltaTime);
        }
        else if (Input.GetButtonDown("Pickup") && itemGrabber.item == null){
            anim.CrossFade("Grab", 0.3f * Time.deltaTime);
        }
    }

    public void AddItem(GameObject item){
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.layer = LayerMask.NameToLayer("Held");
        item.transform.SetParent(null);
        item.GetComponent<Item>().SetPickedUp(true);
        items.Add(item);
    }
    
    public void ShootItem(GameObject item){
        items.Remove(item);
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.layer = LayerMask.NameToLayer("Item");
        item.transform.SetParent(Camera.main.GetComponent<CameraController>().CurrentCart.transform);
        item.GetComponent<Item>().SetPickedUp(false);
        item.GetComponent<Rigidbody>().AddForce(transform.forward * ShootPower, ForceMode.Impulse);
    }

    public bool HasItem(GameObject item){
        return items.Contains(item);
    }

    public bool CanHoldItem(){
        return items.Count <= 2;
    }
}
