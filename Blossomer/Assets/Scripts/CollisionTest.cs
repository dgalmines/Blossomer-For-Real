using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private PlayerMovement player;
    public GameObject protagonist;


    void Start()
    {
        GetComponent<Rigidbody>();
        player = protagonist.GetComponent<PlayerMovement>();
        protagonist.GetComponent<CapsuleCollider>();
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        player.c = true;
        player.b = false;
        player.a = false;
    } 
    
}
