using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTestA : MonoBehaviour
{
    private PlayerMovement player;
    public GameObject protagonist;


    void Start()
    {
        GetComponent<Rigidbody>();
        player = protagonist.GetComponent<PlayerMovement>();
        protagonist.GetComponent<CapsuleCollider>();
    }

    //Moves this GameObject 2 units a second in the forward direction
    void Update()
    {

    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        player.a = true;
        player.b = false;
        player.c = false;
    }

}
