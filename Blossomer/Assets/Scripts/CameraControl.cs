using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Transform target;

    public Vector3 offset;

    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    public Rigidbody cam;

    public float maxViewingAngle;
    public float minViewingAngle;

    public bool invertY;
    public bool invertX;

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
            offset = target.position - transform.position;

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;

        pivot.transform.parent = null;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        pivot.transform.position = target.transform.position;

        //get the X position of the c-stick and rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;

        if (invertX)
        {
            pivot.Rotate(0, -horizontal, 0);
        }
        else
        {
            pivot.Rotate(0, horizontal, 0);
        }

        //get the Y position of the c-stick and rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

        if (invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        //limit up/down camera rotation
        if (pivot.rotation.eulerAngles.x > maxViewingAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewingAngle, horizontal, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewingAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewingAngle, horizontal, 0);
        }

        //move the camera based on the current rotation of the target and the original offset
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;

        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
        }

        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        offset = new Vector3(0, 0, 3);
    }

    /*private void OnTriggerStay(Collider other)
    {
        offset = new Vector3(0, 0, 3);
    }*/

    private void OnTriggerExit(Collider other)
    {
        offset = new Vector3(0, 0, 10);
    }
}

