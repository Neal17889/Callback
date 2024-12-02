using System;
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
        GameObjectManager.Instance.DestroyKeys += DestoryMyself;
    }

    private void DestoryMyself()
    {
        if (this.hasMaster)
            Destroy(this.gameObject);
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

    private void OnDestroy()
    {
        if (GameObjectManager.Instance != null)
            GameObjectManager.Instance.DestroyKeys -= DestoryMyself;
    }
}
