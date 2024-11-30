using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool hasMaster = false;
    private GameObject master;
    public Vector3 offSet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasMaster)
            return;
        this.transform.position = this.master.transform.position + offSet;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.master = other.gameObject;
            this.hasMaster = true;
            this.master.GetComponent<PlayerControllerSystem>().keyCount++;
        }
    }
}
