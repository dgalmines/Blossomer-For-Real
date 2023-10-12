using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public HealthManager theHealthMan;

    public Renderer theRend;

    public Material cpOff;
    public Material cpOn;

    // Start is called before the first frame update
    void Start()
    {
        theHealthMan = FindObjectOfType<HealthManager>();
        GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckpointOn()
    {
        GetComponent<Renderer>();
        theRend.material = cpOn;
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint cp in checkpoints)
        {
            cp.CheckpointOff();
        }
    }

    public void CheckpointOff()
    {
        GetComponent<Renderer>();
        theRend.material = cpOff;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Player"))
        {
            GetComponent<Renderer>();
            //theHealthMan.SetSpawnPoint(transform.position);
            CheckpointOn();
        }
    } 
}
