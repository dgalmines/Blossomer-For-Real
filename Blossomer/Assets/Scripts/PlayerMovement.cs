using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Ctrl;
    public CapsuleCollider hitbox;
    public float moveSpeed;
    public float jumpForce;

    public bool a;
    public bool b;
    public bool c;
    public GameObject checkpointA;
    public GameObject checkpointB;
    public GameObject checkpointC;

    public Vector3 Crouch;
    public Vector3 NotCrouch;
    public Vector3 moveDirection;
    public float gravityScale;

    public GameObject playerModel;

    public bool isGrounded;
    public bool isJump;
    public bool isJumpBoost;
    public bool isGliding;

    public Animator anim;
    public Transform pivot;
    public Transform cam;
    public float rotateSpeed;

    public float knockbackForce;
    public float knockbackTime;
    private float knockbackCounter;

    public HealthManager healthM;

    // Start is called before the first frame update
    void Start()
    {
        Ctrl = GetComponent<CharacterController>();
        hitbox = GetComponent<CapsuleCollider>();
        GetComponent<Rigidbody>();
        NotCrouch = new Vector3(1f, 1f, 1f);
        Crouch = new Vector3(1f, 0.5f, 1f);
        a = true;
        b = false;
        c = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 forwardRelative = camForward * verticalInput;
        Vector3 rightRelative = camRight * horizontalInput;

        //moveDirection = forwardRelative + rightRelative;

        //moveDirection = new Vector3(horizontalInput * moveSpeed, moveDirection.y, verticalInput * moveSpeed);
        if (knockbackCounter <= 0)
        {
            float yStore = moveDirection.y;
            //moveDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);
            moveDirection = forwardRelative + rightRelative;
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;

            if (Ctrl.isGrounded)
            {
                moveDirection.y = 0f;

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                    isJump = true;
                }
            }
        }else
        {
            knockbackCounter -= Time.deltaTime;
        }
        
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        Ctrl.Move(moveDirection * Time.deltaTime);

        /*if (IsGrounded() == true)
        {
            isJump = true;
            isJumpBoost = false;
            isGliding = false;
            isGrounded = true;
            moveSpeed = 6f;
            jumpForce = 10f;
            gravityScale = 1f;
        }

        if (IsGrounded() == false)
        {
            isGrounded = false;
        }*/

        if (isGrounded)
        {
            isJump = false;
        }
        else isJump = true;

        //crouching
        if (Input.GetButtonDown("Crouch"))
        {
            transform.localScale = Crouch;
        }

        if (Input.GetButtonUp("Crouch"))
        {
            transform.localScale = NotCrouch;
        }

        //move player in different directions when camera looks in different directions
        if (horizontalInput != 0 || verticalInput != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        anim.SetBool("isGrounded", Ctrl.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(verticalInput)) + Mathf.Abs(horizontalInput));

    }

    public void Knockback(Vector3 direction)
    {
        knockbackCounter = knockbackTime;

        moveDirection = direction * knockbackForce;
        moveDirection.y = knockbackForce;
    }

    /*public IEnumerator LeapLeaves()
    {
        isJump = true;

        //normal double jump
        if (Input.GetButtonDown("Jump") && moveDirection.y < 1f)
        {
            moveDirection.y = jumpForce;
            isJump = false;
        }

        //gliding
        if (Input.GetButton("Jump") && moveDirection.y < 0f && !isJump)
        {
            gravityScale = 0.5f;
            isGliding = true;
        }
        else gravityScale = 1f;

        //perfect double jump
        if (Input.GetButtonDown("Jump") && moveDirection.y < 1f && moveDirection.y > -0.9f)
        {
            moveDirection.y = jumpForce * 2f;
            isJump = false;
        }

        //double jump boost
        if (Input.GetButtonDown("Jump") && moveDirection.y > 1f)
        {
            moveDirection.y = jumpForce;
            moveDirection = moveDirection.normalized * moveSpeed * 2f;
            isJump = false;
            isJumpBoost = true;
        }

        if (isGrounded)
        {
            isJump = false;

            if (isJumpBoost)
            {
                moveDirection = moveDirection.normalized * moveSpeed * 2f;
                yield return new WaitForSeconds(2f);
                isJumpBoost = false;
                moveDirection = moveDirection.normalized * moveSpeed;
            }
        }
    } */

    /* bool IsGrounded()
     {
         return Physics.CheckSphere(groundCheck.position, .1f, ground);
     }*/

    /*public void OnTriggerEnter(Collider other)
    {

        if (gameObject.CompareTag("CheckpointA"))
        {
            a = true;

            b = false;
            c = false;
        }

        if (gameObject.CompareTag("CheckpointB"))
        {
            b = true;

            a = false;
            c = false;
        }

        if (gameObject.CompareTag("CheckpointC"))
        {
            c = true;

            a = false;
            b = false;
        } 
    } */

}
