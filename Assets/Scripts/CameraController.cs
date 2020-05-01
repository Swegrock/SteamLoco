using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerPosition;
    public float Size = 18;
    public List<GameObject> Carts;
    public List<GameObject> CartTypes;
    public GameObject CurrentCart;
    private float halfSize;
    private int type;

    void Start(){
        halfSize = Size/2f;
    }

    void FixedUpdate()
    {
        int cartNumber = (int)Mathf.Floor(playerPosition.position.x / Size);
        if (cartNumber + 1 > Carts.Count){
            if (type > 2){
                type = 2;
            } else if (cartNumber % 4 == 0){
                type = 3; //Special cart!
            } else if (cartNumber % 2 == 0){
                type = 1;
            } else {
                type = 0;
            }
            GameObject newCart = Instantiate(CartTypes[type], new Vector3(cartNumber * Size, CartTypes[type].transform.position.y, CartTypes[type].transform.position.z), CartTypes[type].transform.rotation);
            Carts.Add(newCart);
            newCart.GetComponent<RoomGenerator>().enabled = true;
        }
        for (int i = 0; i < Carts.Count; i++){
            if (i == cartNumber){
                Carts[i].SetActive(true);
                CurrentCart = Carts[i];
            } else {
                Carts[i].SetActive(false);
            }
        }
        float currentX = cartNumber * Size;
        transform.position = Vector3.Lerp(transform.position, new Vector3(currentX + halfSize, transform.position.y, transform.position.z), Time.deltaTime * 8);
    }
}
