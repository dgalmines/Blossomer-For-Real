using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNoClipping : MonoBehaviour
{
    public CameraControl camer;
    private BoxCollider box;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        camer = cam.GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        camer.offset = new Vector3(0, 0, 5);
    }

    private void OnTriggerExit(Collider other)
    {
        camer.offset = new Vector3(0, 0, 10);
    }
}
