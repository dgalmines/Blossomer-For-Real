using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapLeaves : MonoBehaviour
{
    public PlayerMovement player;
    public bool hasJump;
    public bool hasBoost;
    public CharacterController Ctrl;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        Ctrl = GetComponent<CharacterController>();
        hasBoost = false;
        hasJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Ctrl.isGrounded == false)
            {
                player.moveDirection.y = player.jumpForce;
                hasJump = false;
            }

            if (Ctrl.isGrounded == false && hasJump == false)
            {
                player.jumpForce = 0f * player.jumpForce;
            }
        }

        if (Ctrl.isGrounded == true)
        {
            player.jumpForce = 27f;
            hasJump = false;
        }
    }

}
