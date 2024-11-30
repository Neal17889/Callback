using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerControllerSystem>().canOpenDoor)
            {
                Debug.Log("你已经集齐了三把钥匙，大门为你开启");
            }
            else
            {
                Debug.Log("你需要集齐三把钥匙，才能开启大门");
            }
        }
    }
}
