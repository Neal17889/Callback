using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointObject : MonoBehaviour
{
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 获取角色和空气墙之间的相对位置
            Vector2 direction = other.transform.position - transform.position;

            if (direction.x > 0)
            {
                
                Debug.Log("角色从右边碰到空气墙");

            }
            else if (direction.x < 0)
            {
                
                Debug.Log("角色从左边碰到空气墙");
                
            }
        }
    }
}
